import { CategoryModel } from "../../category/response/category";

export class ProductModel {
    public name!: string | null;
    public description!: string | null;
    public price!: number | null;
    public imageURL!: string | null;
    public createdDate!: string | null;
    public updatedDate!: string | null;
    public ratingPoint!: number | null;
    public categoryId!: string;
    public category!: CategoryModel | null;
}