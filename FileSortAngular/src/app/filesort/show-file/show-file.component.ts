import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { SharedService } from 'src/app/shared.service'; 
import { environment } from 'src/environments/environment';
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
    
    this.refreshFileSortList(location.href.split('/').slice(-1)[0]);
  }

  addClick()
  {
    this.file =
    {
      NameFile:null,
      DateCreatedFile:""
    }
    this.ModalTitle = "Add File";
    this.ActivateAddEditFileComp = true;

  }
  editClick(item: any){
    this.file = item;
    this.ModalTitle = "Edit File";
    this.ActivateAddEditFileComp = true;
  }
  closeClick(){
    this.ActivateAddEditFileComp = false;
    this.refreshFileSortList("true");
  }
  renameClick(item:any)
  {
    this.file = item;
    this.ModalTitle = "Rename File";
    this.ActivateAddEditFileComp = true;
  }
  deleteClick(item:any){
    if(confirm('Are you sure??'))
    {
      let upItem = {
        nameFile:item.nameFile,
        typeFile:item.typeFile,
        newNameFile:""
      }
      this.service.deleteFile(upItem).subscribe(data=>{
        this.refreshFileSortList("true");
      })
    }
  }
  refreshFileSortList(val:string)
  {
    switch(val)
    {
      case "file":
        this.service.getOnlyFile().subscribe(data=>
          {
            this.FilesortList = data;
          });
        break;
      case "video":
        this.service.getOnlyVideo().subscribe(data=>
          {
            this.FilesortList = data;
          });
        break;
      case "photo":
        this.service.getOnlyPhoto().subscribe(data=>
          {
            this.FilesortList = data;
          });
          break;
      case "filesort":
        this.service.getFileList().subscribe(data=>
          {
            this.FilesortList = data;
          });
          break;
      default:
        console.log("Not Found");
        break;
    }
  }
}
