import { Article } from "./article";

export interface ArticleImage {
  url: string;
  highlighted: boolean;
  article: Article[];
}
