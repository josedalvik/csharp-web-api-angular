import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import * as Constants from '../config/constants';

@Injectable({
  providedIn: 'root'
})
export class Inboxes {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    }),
    observe: 'response'
  };
  
  controller = "Inboxes/";
  
  constructor(private httpClient: HttpClient) {
  }

  public post(inbox:any){
    return this.httpClient.post(
      Constants.apiserver+this.controller, 
      inbox, 
      this.httpOptions as any
    );
  }
  

}