import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { TableTemplate } from 'src/app/components/table/table.component';
import { CategoryModel } from 'src/app/models/category/response/category';
import { AlertService } from 'src/app/services/alert.service';
import { CategoryService } from 'src/app/services/category.service';
import { AddCategoryComponent } from './add-category/add-category.component';
import { CategoryDetailComponent } from './category-detail/category-detail.component';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],
  providers: [
    {
      provide: MatDialogRef,
      useValue: {}
    }
  ]
})
export class CategoryComponent implements OnInit {

  constructor(
    private _alert: AlertService,
    private _categoryService: CategoryService,
    private _dialog: MatDialog,
    private _dialogRefAdd: MatDialogRef<AddCategoryComponent>,
    private _dialogRefDetail: MatDialogRef<CategoryDetailComponent>
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
      { headerText: 'No', property: 'no', length: 30 },
      { headerText: 'Category Name', property: 'name', length: 150, onClick: this.showDetail.bind(this) },
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

  addCategory(): void {
    this._dialogRefAdd = this._dialog.open(AddCategoryComponent, {
      panelClass: ['light-theme'],
      autoFocus: true
    });

    this._dialogRefAdd.afterClosed().subscribe((result: boolean) => {
      if(result) {
        this.getCategoryList();
      }
    })
  }

  showDetail(column: any, data: CategoryModel): void {
    this._dialogRefDetail = this._dialog.open(CategoryDetailComponent, {
      data: data,
      panelClass: ['light-theme'],
      autoFocus: true
    });

    this._dialogRefDetail.afterClosed().subscribe((result: boolean) => {
      if(result) {
        this.getCategoryList();
      }
    })
  }

}
