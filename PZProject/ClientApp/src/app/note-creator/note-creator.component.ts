import { Component, OnInit, ViewContainerRef } from '@angular/core';

import { Router, ActivatedRoute } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ValidationPatterns } from '../models/validation.patterns';

@Component({
  selector: 'app-note-creator',
  templateUrl: './note-creator.component.html',
  styleUrls: ['./note-creator.component.css']
})
export class NoteCreatorComponent implements OnInit {
  token: any;
  noteName: string;
  noteDescription: string;
  selectedGroupId: any;
  patterns = new ValidationPatterns();
  isLoading = false;
  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastsManager,
    private vcr: ViewContainerRef
  ) {
    this.toastr.setRootViewContainerRef(vcr);
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.selectedGroupId = params['id'];
    });
    this.token = localStorage.getItem('id_token');
  }

  createNote() {
    this.isLoading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.token
      })
    };

    const bodyOptions = {
      NoteName: this.noteName,
      NoteDescription: this.noteDescription
    };
    this.http
      .post(
        'https://pzproject.azurewebsites.net/groups/' +
          this.selectedGroupId +
          '/notes/create',
        bodyOptions,
        httpOptions
      )
      .subscribe(
        result => {
          this.showSuccessAlert();
          setTimeout(() => {
            this.isLoading = false;
            this.router.navigate([
              '/groups/' + this.selectedGroupId + '/notes'
            ]);
          }, 2000);
        },
        error => {
          this.showGroupNameErrorAlert(error);
        }
      );
  }

  showSuccessAlert() {
    this.toastr.success('Note created successfully');
  }

  showGroupNameErrorAlert(error: any) {
    console.error(error);
    this.isLoading = false;
    this.toastr.error('Error');
  }

  backToNotes() {
    this.router.navigate(['/groups/' + this.selectedGroupId + '/notes']);
  }
}
