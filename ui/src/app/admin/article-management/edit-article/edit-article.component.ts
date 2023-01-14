import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route } from '@angular/router';
import { Article } from 'src/app/_models/articles/article';
import { Tag } from 'src/app/_models/articles/articleTags';
import { Category } from 'src/app/_models/articles/category';
import { ArticleService } from 'src/app/_services/article.service';
import { HtmlEditorService } from 'src/app/_services/html-editor.service';

@Component({
  selector: 'app-edit-article',
  templateUrl: './edit-article.component.html',
  styleUrls: ['./edit-article.component.scss']
})
export class EditArticleComponent implements OnInit {

article: Article;
categories: Category[];
tags: Tag[];
url: string;
tagsFromForm: [];
categoryId: number;
selectedCategory: string;
editForm: FormGroup;
fromFormCategory: string;

public editorConfiguration = this.editorConfig.editorConfig;

  constructor(private articleService: ArticleService,
    private editorConfig: HtmlEditorService,
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.url = this.route.snapshot.paramMap.get('url');
    this.getArticleByUrl(this.url);
    this.getCategories();
    this.getTags();
  }

  getArticleByUrl(url) {
    this.articleService.getArticle(url).subscribe(article => {
      this.article = article;
      console.log(this.article);
      this.editForm = new FormGroup({
        title: new FormControl(''),
        shortDescription: new FormControl(''),
        url: new FormControl(''),
        content: new FormControl(''),
        category: new FormControl(''),
        tags: new FormControl([])
      });

      this.setFormValues();
    })
  }

  setFormValues() {
    this.editForm.setValue({
      title: this.article.title,
      shortDescription: this.article.shortDescription,
      url: this.article.url,
      content: this.article.content,
      category: this.article.categoryId,
      tags: this.article.tags.map(x => x.id)
    })
  }

  getCategories() {
    this.articleService.getCategories().subscribe(response => {
      this.categories = response;
      console.log(this.categories);
    })
  }

  getTags() {
    this.articleService.getAllTags().subscribe(response => {
      this.tags = response;
    })
  }

  save() {
    var tagsFromForm = this.editForm.value['tags'];
    this.editForm.patchValue({
      tags: this.editForm.value['tags'].map(function(tagsFromForm) {
        return {
          id: tagsFromForm
        }
      })
    });

    this.articleService.saveArticle(this.article.url, this.editForm.value).subscribe(response => {
      console.log(response);
    }, err => {
      console.log(err.message);
    });

    this.getArticleByUrl(this.url);

  }
}


