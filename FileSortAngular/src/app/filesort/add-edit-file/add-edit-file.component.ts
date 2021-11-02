import { Component, OnInit,Input } from '@angular/core';

@Component({
  selector: 'app-add-edit-file',
  templateUrl: './add-edit-file.component.html',
  styleUrls: ['./add-edit-file.component.css']
})
export class AddEditFileComponent implements OnInit {

  constructor() { }
  @Input() file:any;
  nameFile?:string;
  typeFile?:string;
  sizeFile?:string;
  dateCreatedFile?:string;

  ngOnInit(): void {
    this.nameFile = this.file.nameFile;
    this.typeFile = this.file.typeFile;
    this.sizeFile = this.file.sizeFile;
    this.dateCreatedFile = this.file.dateCreatedFile;
  }
  /*addFile(){

  }
  updateFile(){

  }*/
}
