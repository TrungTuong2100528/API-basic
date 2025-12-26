import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogBoxComponent } from '../../AppComponent/dialog-box/dialog-box.component';
@Component({
  selector: 'app-inventory',
  imports: [FormsModule, CommonModule],
  templateUrl: './inventory.component.html',
  styleUrl: './inventory.component.css'
})
export class InventoryComponent {
  //Lấy service HttpClient để Gọi API ; tương đương với constructor(private httpClient: HttpClient) {}
  httpClient = inject(HttpClient)
  productIDToDelete: number =0;
  private modalService = inject(NgbModal)

  //Get
  inventoryDetails: any; //Lưu dữ liệu API trả về

  ngOnInit(){ //Hàm chạy khi component được load
     this.getInventoryDetails();
  }

  getInventoryDetails(){
    let aipUrl="https://localhost:7243/api/Inventory";

     this.httpClient.get(aipUrl).subscribe(data=>{ //Angular gửi HTTP GET; subscribe() để: Nhận dữ liệu async
      this.inventoryDetails = data; //Gán dữ liệu cho biến
      console.log(this.inventoryDetails) //Log để debug
     })
  }

  //post
    inventoryData = {
    productID:"",
    productName:"",
    availableQty:0,
    reOderPoint:0
  }


  oSubmit(): void{ //Chạy khi bấm nút Submit
    
    let aipUrl="https://localhost:7243/api/Inventory";
    //B1.1 Request thực tế gửi đi (qua asp.net bước tiếp theo)
    let httpOptions={ //Cấu hình HTTP Header
        headers: new HttpHeaders({
          Authorization: "my-auth-token", //Token (demo)
          "Content-Type": "application/json" //Báo cho API biết gửi JSON
        })
    }
      
  //B1 Angular gửi HTTP POST lên API:
    this.httpClient.post(aipUrl, this.inventoryData,httpOptions).subscribe({
      next: v=> console.log(v), //API trả dữ liệu thành công
      error: e=> console.log(e), //API lỗi (400 / 500)
      complete: () => { //Request hoàn tất
        alert("Form Submitted successfully" + JSON.stringify(this.inventoryData));
        this.getInventoryDetails();
      }
    });
  }

  // Delete
  openConfirmDialog(productID: number){
    this.productIDToDelete = productID;
    console.log(this.productIDToDelete);
      this.modalService.open(DialogBoxComponent).result.then(data=>{
        if(data.event == "confirm"){
          this.deleteInventory();
        }
      })
  }
  deleteInventory():void{
    let aipUrl="https://localhost:7243/api/Inventory?productId="+this.productIDToDelete;

    this.httpClient.delete(aipUrl).subscribe(data=>{
      this.getInventoryDetails();
    });
  }
}
