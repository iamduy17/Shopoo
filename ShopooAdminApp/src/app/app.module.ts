import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from './materials/material.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { HeaderComponent } from './layouts/header/header.component';
import { FooterComponent } from './layouts/footer/footer.component';
import { NavBarComponent } from './layouts/nav-bar/nav-bar.component';

import { TableComponent } from './components/table/table.component';
import { ImageUploadComponent } from './components/image-upload/image-upload.component';

import { CategoryComponent } from './pages/category/category.component';
import { ProductComponent } from './pages/product/product.component';
import { AddCategoryComponent } from './pages/category/add-category/add-category.component';
import { CategoryDetailComponent } from './pages/category/category-detail/category-detail.component';
import { ConfirmDeleteCategoryComponent } from './pages/category/confirm-delete-category/confirm-delete-category.component';
import { AddProductComponent } from './pages/product/add-product/add-product.component';
import { ProductDetailComponent } from './pages/product/product-detail/product-detail.component';

import { ImageUploaderDirective } from './directives/image-uploader.directive';

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    HeaderComponent,
    FooterComponent,
    NavBarComponent,
    TableComponent,
    CategoryComponent,
    ProductComponent,
    AddCategoryComponent,
    CategoryDetailComponent,
    ConfirmDeleteCategoryComponent,
    AddProductComponent,
    ProductDetailComponent,
    ImageUploaderDirective,
    ImageUploadComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
