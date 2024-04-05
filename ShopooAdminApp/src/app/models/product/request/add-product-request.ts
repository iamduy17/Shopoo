import { ProductModel } from "../response/product";

export class AddProductRequestModel {
    public id!: string;
    public name!: string | null;
    public description!: string | null;
    public price!: number | null;
    public imageURL!: string | null;
    public createdDate!: string | null;
    public updatedDate!: string | null;
    public categoryId!: string;

    /**
     *
     */
    constructor(product: ProductModel = new ProductModel()) {
        this.name = product.name;
        this.description = product.description;
        this.price = product.price;
        this.imageURL = product.imageURL;
        this.createdDate = product.createdDate;
        this.updatedDate = product.updatedDate;
        this.categoryId = product.categoryId;
    }
}