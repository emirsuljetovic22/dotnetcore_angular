import { Injectable } from '@angular/core';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Injectable({
  providedIn: 'root'
})
export class HtmlEditorService {

  constructor() { }

  editorConfig: AngularEditorConfig = {
    editable: true,
      height: 'auto',
      minHeight: '350px',
      width: 'auto',
      enableToolbar: true,
      showToolbar: true,
      customClasses: [
      {
        name: 'quote',
        class: 'quote',
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: 'titleText',
        class: 'titleText',
        tag: 'h1',
      },
    ],
    sanitize: true,
    toolbarPosition: 'top',
    toolbarHiddenButtons: [
      ['bold', 'italic'],
      ['fontSize']
    ]
  };
}
