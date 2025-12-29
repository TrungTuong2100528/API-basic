import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-customer-dialog-box',
  imports: [FormsModule, CommonModule],
  standalone: true,
  templateUrl: './customer-dialog-box.component.html',
  styleUrl: './customer-dialog-box.component.css'
})
export class CustomerDialogBoxComponent {
  httpClient = inject(HttpClient)
  // Đóng modal
  modal = inject(NgbActiveModal)

  customerDetails={
    customerId: "",
    firstName: "",
    lastName: "",
    registrationDate:"",
    phone: "",
    email: ""
  }
  onSubmit(){

    let aipUrl="https://localhost:7243/api/Customer";

    let httpOptions={ 
        headers: new HttpHeaders({
          Authorization: "my-auth-token", 
          "Content-Type": "application/json" 
        })
    }
    this.httpClient.post(aipUrl, this.customerDetails, httpOptions).subscribe(
      {
        next:v=>console.log(v),
        error:e=>console.log(e),
        complete:()=>{
          alert("Customer details saved successfully: "+JSON.stringify(this.customerDetails));
          this.modal.close();
        }
      }
    )
  }
}
