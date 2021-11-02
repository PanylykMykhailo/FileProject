import { Injectable } from '@angular/core';
import{HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl = "https://localhost:5001/api";
readonly FileUrl = "https://localhost:5001/Test";
  constructor(private http:HttpClient) { }

  getFileList():Observable<any[]>
  { 
    return this.http.get<any>(this.APIUrl + '/FileSort');
  }

  UploadFile(val:any)
  {
    return this.http.post(this.APIUrl + '/FileSort/SaveFile',val)
  }
  /*addFile(val:any)
  {
    return this.http.post(this.APIUrl + "/FileSort",val)
  }*/
}
