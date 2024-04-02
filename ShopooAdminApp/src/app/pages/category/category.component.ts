import { Component, OnInit } from '@angular/core';
import { TableTemplate } from 'src/app/components/table/table.component';
import { CategoryModel } from 'src/app/models/category/response/category';
import { AlertService } from 'src/app/services/alert.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  constructor(
    private _alert: AlertService,
    private _categoryService: CategoryService
  ) { }

  listHeader?: TableTemplate[];
  listData: CategoryModel[] = [];

  ngOnInit(): void {
    this.setHeaderTable();

    setTimeout(() => {
      this.getCategoryList();
    })
  }

  setHeaderTable(): void {
    this.listHeader = [
      { headerText: 'STT', property: 'no', length: 30 },
      { headerText: 'Category Name', property: 'name', length: 150 },
      { headerText: 'Description', property: 'description', length: 150  },
    ];
  }

  getCategoryList(): void {
    this._categoryService.getCategoryList().subscribe(res => {
      switch(res.returnCode) {
        case "success":
          if(res?.data?.categories.length > 0) {
            this.listData = res.data.categories.map((item, index) => ({...item, no: index + 1}));
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
