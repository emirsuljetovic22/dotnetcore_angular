import { Article } from "./article";

export interface Tag {
  id: number;
  articleTagName: string;
  articles: Article[];
}
