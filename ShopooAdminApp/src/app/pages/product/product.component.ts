import { Component, OnInit } from '@angular/core';
import { TableTemplate } from 'src/app/components/table/table.component';
import { ProductModel } from 'src/app/models/product/response/product';
import { AlertService } from 'src/app/services/alert.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  constructor(
    private _alert: AlertService,
    private _productService: ProductService
  ) { }

  listHeader?: TableTemplate[];
  listData: ProductModel[] = [];

  ngOnInit(): void {
    this.setHeaderTable();

    setTimeout(() => {
      this.getProductList();
    })
  }

  setHeaderTable(): void {
    this.listHeader = [
      { headerText: 'No', property: 'no', length: 30 },
      { headerText: 'Product Name', property: 'name', length: 150 },
      { headerText: 'Description', property: 'description', length: 150  },
      { headerText: 'Price', property: 'price', length: 100  },
      { headerText: 'Created Date', property: 'createdDate', length: 150  },
      { headerText: 'Updated Date', property: 'updatedDate', length: 150  },
      { headerText: 'Category Name', property: 'categoryName', length: 150  },
    ];
  }

  getProductList(): void {
    this._productService.getProductList().subscribe(res => {
      switch(res.returnCode) {
        case "success":
          if(res?.data?.products.length > 0) {
            this.listData = res.data.products.map((item, index) => ({...item, no: index + 1, categoryName: item.category?.name}));
          } else {
            this._alert.showNotFound();
          }
          break;
        default:
          this._alert.showError();
          break;    
      }
    })
  }

}
