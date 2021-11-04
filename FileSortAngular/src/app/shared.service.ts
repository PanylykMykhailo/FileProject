import { Injectable } from '@angular/core';
import{HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class SharedService {
  constructor(private http:HttpClient) { }

  getFileList():Observable<any[]>
  { 
    return this.http.get<any>(environment.APIUrl+ '/FileSort');
  }
  getOnlyFile():Observable<any[]>
  {
    return this.http.get<any>(environment.APIFileUrl + '/FileSort/GetOnlyFile');
  }
  renameFile(val:any)
  {
    return this.http.post(environment.APIUrl + '/FileSort/RenameFile',val)
  }
  addFile(val:any)
  {
    return this.http.post(environment.APIUrl + '/FileSort',val)
  }
  deleteFile(val:any)
  {
    const options = {
      headers: new HttpHeaders({
         'Content-Type': 'application/json',
      }),
      body:val
   }
    return this.http.delete(environment.APIUrl + '/FileSort/DeleteFile',options)
  }
}
