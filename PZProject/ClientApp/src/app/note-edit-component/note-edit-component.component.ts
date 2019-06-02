import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';

@Component({
  selector: 'app-note-edit-component',
  templateUrl: './note-edit-component.component.html',
  styleUrls: ['./note-edit-component.component.css']
})
export class NoteEditComponentComponent implements OnInit {
  selectedGroupId: number;
  selectedNoteId: number;
  iterator = 0;
  noteInfoArray = [];
  token: any;
  noteName: any;
  noteDescription: any;
  isLoading = false;
  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastsManager,
    private vcr: ViewContainerRef
  ) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.selectedGroupId = params['id'];
      this.selectedNoteId = params['id2'];
    });
    this.token = localStorage.getItem('id_token');
    this.displayGroupNotes();
  }

  addNotesInfoToArray(result: Object | { [x: string]: any }[]) {
    while (result[this.iterator]) {
      if (result[this.iterator]['id'] == this.selectedNoteId) {
        this.noteName = result[this.iterator]['name'];
        this.noteDescription = result[this.iterator]['description'];
        this.noteInfoArray.push({
          NoteId: result[this.iterator]['id'],
          NoteName: result[this.iterator]['name'],
          NoteDescription: result[this.iterator]['description']
        });
      }

      this.iterator++;
    }
  }

  displayGroupNotes() {
    this.isLoading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.token
      })
    };
    this.http
      .get(
        'https://pzproject.azurewebsites.net/groups/' +
          this.selectedGroupId +
          '/notes',
        httpOptions
      )
      .subscribe(
        result => {
          this.addNotesInfoToArray(result);
          this.isLoading = false;
        },
        error => console.error(error)
      );
  }

  updateNote(noteId: any) {
    this.isLoading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.token
      })
    };

    const bodyOptions = {
      NoteId: noteId,
      NoteName: this.noteName,
      NoteDescription: this.noteDescription
    };
    this.http
      .put(
        'https://pzproject.azurewebsites.net/groups/' +
          this.selectedGroupId +
          '/notes/edit',
        bodyOptions,
        httpOptions
      )
      .subscribe(
        result => {
          this.showSuccessAlert();
          setTimeout(() => {
            this.isLoading = false;
            this.router.navigate(['/groups/' + this.selectedGroupId + '/notes']);
          }, 2000);
        },
        error => this.showErrorAlert(error)
      );
  }

  showSuccessAlert() {
    this.toastr.success('Update successful');
  }

  showErrorAlert(error: any) {
    console.error(error);
    this.isLoading = false;
    this.toastr.error('Error');
  }

  backToNotes() {
    this.router.navigate(['/groups/' + this.selectedGroupId + '/notes']);
  }
}
