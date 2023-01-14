import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Article } from 'src/app/_models/articles/article';
import { Pagination } from 'src/app/_models/pagination';
import { ArticleService } from 'src/app/_services/article.service';

@Component({
  selector: 'app-list-articles',
  templateUrl: './list-articles.component.html',
  styleUrls: ['./list-articles.component.css']
})
export class ListArticlesComponent implements OnInit {
  articles: Partial<Article[]>;
  pageNumber = 1;
  pageSize = 3;
  pagination: Pagination;
  length = 100;
  pageSizeOptions: number[] = [3, 20, 40];

  // MatPaginator Output
  pageEvent: PageEvent;

  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this.pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
    }
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private articleService: ArticleService) {}

  ngOnInit(): void {
    this.getArticles();
  }

  getArticles() {
    this.articleService.getArticles(this.pageNumber, this.pageSize).subscribe(response => {
      this.articles = response.result;
      console.log(this.articles);
      this.pagination = response.pagination;
    })
  }

  pageChanged(event: any) {
    console.log("test");
    this.pageNumber = event.page;
    this.getArticles();
  }

      /**
   * Set the paginator and sort after the view init since this component will
   * be able to query its view for the initialized paginator and sort.
   */
       ngAfterViewInit() {

      }

      applyFilter(filterValue: string) {
        filterValue = filterValue.trim(); // Remove whitespace
        filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
      }
    }
