import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent {

  title="Register"
  

  registerForm!:FormGroup;
  
  isSubmitted=false;

  constructor(private formBuilder:FormBuilder) { 


  }
  ngOnInit(){
    //validations

    this.registerForm=this.formBuilder.group({
        fullName:['',Validators.required],
        bloodGroup:['',Validators.required],
        age:['',[Validators.required,Validators.pattern(/^[0-9]\d*$/)]],
        gender:['',[Validators.required]],
        email: ['',[Validators.email,Validators.required]],
        location:['',[Validators.required]],
        mobileNumber:['',[Validators.required,Validators.pattern(/^[0-9]\d*$/),Validators.minLength(10),Validators.maxLength(10)]]

    })
  }
  onSubmit(){
    this.isSubmitted=true;
    if(this.registerForm.invalid){
      return
    }
    alert("success");
  }

}
