import { ProductModel } from "../response/product";
import { AddProductRequestModel } from "./add-product-request";

export class UpdateProductRequestModel {
    public id!: string;
    public product!: ProductModel;

    /**
     *
     */
    constructor(id: string, product: ProductModel) {
        this.id = id;
        this.product = product;
    }
}