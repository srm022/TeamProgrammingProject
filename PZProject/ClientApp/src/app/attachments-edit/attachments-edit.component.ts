import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastsManager } from 'ng2-toastr';

@Component({
  selector: 'app-attachments-edit',
  templateUrl: './attachments-edit.component.html',
  styleUrls: ['./attachments-edit.component.css']
})
export class AttachmentsEditComponent implements OnInit {
  selectedGroupId: number;
  selectedNoteId: number;
  iterator = 0;
  token: any;
  attachmentName: string;
  isLoading = false;
  url: string;
  noteAttachment: File;
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
      this.selectedNoteId = params['id3'];
    });
    this.token = localStorage.getItem('id_token');
    this.displayGroupNotes();
  }

  handleFileInput(files) {
    this.noteAttachment = files[0];
    console.log(this.noteAttachment)
  }

  getAttachmentName(result: Object | { [x: string]: any }[]) {
    while (result[this.iterator]) {
      if (result[this.iterator]['id'] == this.selectedNoteId) {
        this.attachmentName = result[this.iterator]['attachmentIdentity'];
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
          this.getAttachmentName(result);
          this.url = 'https://pzprojectstorage.blob.core.windows.net:443/pzproject-blobstorage/' + this.attachmentName;
          this.isLoading = false;
        },
        error => console.error(error)
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

  deleteAttachment() {
    this.isLoading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.token
      })
    };

    this.http
      .delete('https://pzproject.azurewebsites.net/groups/' +
      this.selectedGroupId +
      '/notes/' +
      this.selectedNoteId +
      '/attachment/delete', httpOptions)
      .subscribe(
        result => {
          window.location.reload();
          this.isLoading = false;
        },
        error => {
         this.showError(error);
        }
      );
  }

  showError(error: any) {
    console.log(error);
    this.isLoading = false;
    this.toastr.error('Error');
  }

  addAttachment() {
    this.isLoading = true;
    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.token
      })
    };
    let file: File = this.noteAttachment;
    let formData: FormData = new FormData();
    formData.append('file', file);

    this.http
      .post('https://pzproject.azurewebsites.net/groups/' +
        this.selectedGroupId +
        '/notes/' +
        this.selectedNoteId +
        '/attachment/create',
        formData,
        httpOptions
      )
      .subscribe(
        result => {
          window.location.reload();
          this.isLoading = false;
        },
        error => this.showErrorAlert(error)
      );
  }
}
