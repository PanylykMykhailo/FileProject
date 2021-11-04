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
    return this.http.get<any>(environment.APIUrl+ '/File');
  }
  getOnlyFile():Observable<any[]>
  {
    return this.http.get<any>(environment.APIFileUrl + '/FileSort/GetOnlyFile');
  }
  renameFile(val:any)
  {
    return this.http.put(environment.APIUrl + '/File/RenameFile',val)
  }
  addFile(val:any)
  {
    return this.http.post(environment.APIUrl + '/File',val)
  }
  deleteFile(val:any)
  {
    const options = {
      headers: new HttpHeaders({
         'Content-Type': 'application/json',
      }),
      body:val
   }
    return this.http.delete(environment.APIUrl + '/File',options)
  }
}
