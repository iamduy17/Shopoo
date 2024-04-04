import { CategoryModel } from "../response/category";

export class UpdateCategoryRequestModel {
    public id!: string;
    public category!: CategoryModel;

    /**
     *
     */
    constructor(id: string, category: CategoryModel) {
        this.id = id;
        this.category = category;
    }
}