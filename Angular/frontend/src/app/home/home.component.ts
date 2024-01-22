import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Inboxes } from '../api/model/inboxes';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{

  sent = false;
  validating = false;
  error = "";
  Form: FormGroup;
  saved = false;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private modelInbox: Inboxes,
  ) { }
  
  get f() { return this.Form.controls; }
  
  ngOnInit(): void {
    this.error = '';
    //formulario
    this.Form = this.formBuilder.group({
      subject: ['', [Validators.required]],
      message: ['', [Validators.required]],
      id: [Guid.create().toString()]
    });
    
  }

  onsubmit() {
    
    this.sent = true;
    this.saved = false;
    if (this.Form.invalid) {
      this.error = "Please complete the form.";
      return;
    }
    
    this.validating = true;
    this.sent = false;
    
    this.modelInbox.post(this.Form.value).subscribe((data:any)=>{
      this.validating = false;
      if(data.status == 400){
        this.error = "Por favor completa el formulario.";
      }else if(data.status == 201){
        this.error = "";
        this.saved = true;
        this.Form.reset();
        this.Form.get("id")?.setValue(Guid.create().toString());
        setTimeout(() => {
          this.saved = false;
        }, 3000);
      }else{
        this.error = "Request error.";
      }
    });
    
    
  }

}
