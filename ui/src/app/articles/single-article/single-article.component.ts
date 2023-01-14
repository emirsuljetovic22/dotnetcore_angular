import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route } from '@angular/router';
import { Article } from 'src/app/_models/articles/article';
import { ArticleService } from 'src/app/_services/article.service';

@Component({
  selector: 'app-single-article',
  templateUrl: './single-article.component.html',
  styleUrls: ['./single-article.component.css']
})
export class SingleArticleComponent implements OnInit {

  article: Article;

  constructor(private articleService: ArticleService, private activatedRoute: ActivatedRoute) { }

  url = this.activatedRoute.snapshot.params.url;

  ngOnInit(): void {
    this.loadArticle();
  }

  loadArticle() {
    this.articleService.getArticle(this.url).subscribe(article => {
      this.article = article;
    })
  }
}
