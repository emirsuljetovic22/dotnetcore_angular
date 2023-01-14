import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Article } from '../_models/articles/article';
import { Tag } from '../_models/articles/articleTags';
import { Category } from '../_models/articles/category';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})

export class ArticleService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  /* TAGS */

  getAllTags() {
    return this.http.get<Tag[]>(this.baseUrl + 'tag');
  }

  addTag() {
    return this.http.post<Tag>(this.baseUrl + 'tag', {});
  }

  addTagToArticle() {
    this.http.post<Tag>(this.baseUrl + 'tag', {});
  }

  removeTagFromArticle() {
    this.http.delete<Tag>(this.baseUrl + 'tag', {});
  }

  getTagById(tagId) {
    this.http.get<Tag>(this.baseUrl + 'tag/' + tagId);
  }

  saveTag(tagId) {
    this.http.put<Tag>(this.baseUrl + 'tag/' + tagId, {})
  }

  deleteTag(tagId) {
    this.http.delete<Tag>(this.baseUrl + 'tag/' + tagId);
  }

  /* CATEGORIES */

  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'category');
  }

  addCategory() {
    return this.http.post<Category>(this.baseUrl + 'category', {});
  }

  addCategoryToArticle() {
    return this.http.post<Category>(this.baseUrl + 'category/add-to-article', {});
  }

  removeCategoryFromArticle() {
    return this.http.delete<Category>(this.baseUrl + 'category/remove-from-article', {});
  }

  saveCategory(categoryId) {
    return this.http.put<Category>(this.baseUrl + 'category/' + categoryId, {});
  }

  deleteCategory(categoryId) {
    return this.http.delete<Category>(this.baseUrl + 'category' + categoryId)
  }

  /* ARTICLES */

  getArticles(pageNumber, pageSize) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    return getPaginatedResult<Article[]>(this.baseUrl + 'articles', params, this.http);
  }

  getArticle(url) {
    return this.http.get<Article>(this.baseUrl + 'articles/' + url);
  }

  saveArticle(url, article: Article) {
    return this.http.put<Article>(this.baseUrl + 'articles/' + url, article);
  }

  addArticle(formData: any) {
    return this.http.post<Article>(this.baseUrl + 'articles', formData);
  }

  deleteArticle(articleUrl) {
    return this.http.delete<Article>(this.baseUrl + 'articles/' + articleUrl);
  }
}
