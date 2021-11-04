import { Component, OnInit,Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { ShowFileComponent } from '../show-file/show-file.component';
@Component({
  selector: 'app-add-edit-file',
  templateUrl: './add-edit-file.component.html',
  styleUrls: ['./add-edit-file.component.css']
})
export class AddEditFileComponent implements OnInit {

  constructor(private service:SharedService,public getproperty:ShowFileComponent) { }
  @Input() file:any;
  nameFile?:string;
  typeFile?:string;
  sizeFile?:string;
  newNameFile?:string;
  dateCreatedFile?:string;
  modalTitle?:string;
  ngOnInit(): void {
    this.nameFile = this.file.nameFile;
    this.typeFile = this.file.typeFile;
    this.sizeFile = this.file.sizeFile;
    this.newNameFile = this.file.newNameFile;
    this.dateCreatedFile = this.file.dateCreatedFile;
    this.modalTitle = this.getproperty.ModalTitle;
  }
  addFile(){
    var val = {
      nameFile:this.nameFile,
      typeFile:this.typeFile,
      sizeFile:"0bytes",
      dateCreatedFile:"nothing"
    };
    this.service.addFile(val).subscribe(res=>{
      alert(res.toString())
    })
  }
  updateFile(){

  }
  renameFile(){
     var val = {
      nameFile:this.nameFile,
      typeFile:this.typeFile,
      newNameFile:this.newNameFile
    };
    this.service.renameFile(val).subscribe(res=>{
      alert(res.toString());
  })}
}
