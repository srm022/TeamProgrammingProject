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

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastsManager,
    private vcr: ViewContainerRef) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.selectedGroupId = params['id'];
      this.selectedNoteId = params['id2'];
      console.log( params['id']);
      console.log( params['id2']);
    })
    this.token = localStorage.getItem('id_token');
    this.displayGroupNotes();


  };

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
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.token
      })
    };
    this.http
      .get('https://pzproject.azurewebsites.net/groups/' + this.selectedGroupId + '/notes', httpOptions)
      .subscribe(
        result => {
          this.addNotesInfoToArray(result);
          console.log(result);
          console.log(this.noteInfoArray);
        },
        error => console.error(error)
      );
  }

  updateNote(noteId: any) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.token
      })
    };

    const bodyOptions = {
      'NoteId': noteId,
      'NoteName': this.noteName,
      'NoteDescription': this.noteDescription
    };
    this.http.put('https://pzproject.azurewebsites.net/groups/' + this.selectedGroupId +'/notes/edit', bodyOptions, httpOptions).subscribe(result => {
      this.router.navigate(['/groups/' + this.selectedGroupId + '/notes']);
    }, error => console.error(error));
  }

}
