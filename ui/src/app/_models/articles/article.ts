import { ArticleImage } from "./articleImage";
import { Tag } from "./articleTags";

export interface Article {
  id: number;
  title: string;
  content: string;
  shortDescription: string;
  url: string;
  created: Date;
  updated: Date;
  author: string;
  status: string;
  category: string;
  categoryId: number;
  tags: Tag[];
  articleImages: ArticleImage[];
}
