import { SubCategory } from "./subCategory";

export interface Category {
  id: number;
  parentCategoryId: number;
  categoryDescription: string;
  categoryName: string;
  subCategories: SubCategory[];
}
