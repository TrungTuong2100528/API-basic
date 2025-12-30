import { Component, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerDialogBoxComponent } from '../customer-dialog-box/customer-dialog-box.component';
import { HttpClient } from '@angular/common/http';
// import { NgForOf } from "../../../../node_modules/@angular/common/common_module.d-NEF7UaHr";
import { CommonModule, NgFor } from '@angular/common';
@Component({
  selector: 'app-customer',
  imports: [NgFor, CommonModule],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent {

  private modalService = inject(NgbModal)

  openCustomerDialog(){
    this.modalService.open(CustomerDialogBoxComponent).result.then(data =>{
      //lấy event từ dialog-box sao khi thêm xong để load bảng dữ liệu lại
      if(data.event=="closed")
        this.GetCustomerDetail();
    })
  }
 
  httpClien = inject(HttpClient)
  customerDetails: any;

  ngOnInit(){
    this.GetCustomerDetail();
  }

  GetCustomerDetail(){
    let aipUrl="https://localhost:7243/api/Customer";

    this.httpClien.get(aipUrl).subscribe(data=>{
      this.customerDetails = data;
      console.log(this.customerDetails);
    })
  }
}
