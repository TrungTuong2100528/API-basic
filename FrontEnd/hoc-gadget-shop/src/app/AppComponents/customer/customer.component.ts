import { Component, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerDialogBoxComponent } from '../customer-dialog-box/customer-dialog-box.component';

@Component({
  selector: 'app-customer',
  imports: [],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent {
  private modalService = inject(NgbModal)
  
  openCustomerDialog(){
    this.modalService.open(CustomerDialogBoxComponent)
  }
}
