import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { CategoryComponent } from './pages/category/category.component';
import { ProductComponent } from './pages/product/product.component';

const routes: Routes = [
  // { path: 'login', component: LoginComponent },
  {
    path: '', component: MainLayoutComponent,
    children: [
      { path: 'category', component: CategoryComponent },
      { path: 'product', component: ProductComponent },
      { path: '', redirectTo: '/category', pathMatch: 'full' },
      { path: '**', redirectTo: '/category'},  // redirect unknown route to deposit home page
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
