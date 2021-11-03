import { Component, OnInit,Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-add-edit-file',
  templateUrl: './add-edit-file.component.html',
  styleUrls: ['./add-edit-file.component.css']
})
export class AddEditFileComponent implements OnInit {

  constructor(private service:SharedService) { }
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
  addFile(){
    var val = {
      nameFile:this.nameFile,
      typeFile:this.typeFile,
      sizeFile:this.sizeFile,
      dateCreatedFile:this.dateCreatedFile
    };
    this.service.addFile(val).subscribe(res=>{
      alert(res.toString())
    })
  }
  updateFile(){
    var val = {
      nameFile:this.nameFile,
      typeFile:this.typeFile,
      sizeFile:this.sizeFile,
      dateCreatedFile:this.dateCreatedFile
    };
    this.service.UploadFile(val).subscribe(res=>{
      alert(res.toString())
    })
  }
}
