import { Category } from "./category";

export interface SubCategory {
  id: number;
  parentCategoryId: number;
  categoryDescription: string;
  categoryName: string;
  category: Category[];
  subCategories: SubCategory[];
}
