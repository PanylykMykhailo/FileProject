import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service'; 
//import { runInThisContext } from 'vm';
@Component({
  selector: 'app-show-file',
  templateUrl: './show-file.component.html',
  styleUrls: ['./show-file.component.css']
})
export class ShowFileComponent implements OnInit {

  constructor(private service:SharedService) { }

  FilesortList:any = [];

  ModalTitle?:string;
  ActivateAddEditFileComp:boolean=false;
  file:any;
  ngOnInit(): void {
    this.refreshFileSortList();
  }

  addClick()
  {
    this.file =
    {
      NameFile:null,
      DateCreatedFile:""
    }
    //checking status
    this.ModalTitle = "Add File";
    this.ActivateAddEditFileComp = true;

  }
  editClick(item: any){
    console.log(item.nameFile);
    this.file = item;
    this.ModalTitle = "Edit File";
    this.ActivateAddEditFileComp = true;
  }
  closeClick(){
    this.ActivateAddEditFileComp = false;
    this.refreshFileSortList();
  }

  deleteClick(item:any){
    if(confirm('Are you sure??'))
    {
      let upItem = {
        nameFile:item.nameFile,
        typeFile:item.typeFile,
        newNameFile:""
      }
      //console.log(upItem);
      this.service.deleteFile(upItem).subscribe(data=>{
        //alert(data);
        this.refreshFileSortList();
      })
    }
  }
  refreshFileSortList()
  {
    this.service.getFileList().subscribe(data=>
      {
          this.FilesortList = data;
      }
      )
  }
}
