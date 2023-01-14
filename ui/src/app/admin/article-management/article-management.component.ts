import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Article } from 'src/app/_models/articles/article';
import { Pagination } from 'src/app/_models/pagination';
import { ArticleService } from 'src/app/_services/article.service';
import { MatTableDataSource } from '@angular/material/table';
import { EditArticleComponent } from './edit-article/edit-article.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-article-management',
  templateUrl: './article-management.component.html',
  styleUrls: ['./article-management.component.css']
})
export class ArticleManagementComponent implements OnInit {
  articles: Partial<Article[]>;
  article: Article;

  dataSource;
  displayedColumns = ['id', 'title', 'url', 'button-group'];

  pagination: Pagination;
  pageNumber = 0;
  pageSize = 10;
  length = 100;
  pageIndex = 0;
  totalItems = 0;
  pageSizeOptions: number[] = [10, 25, 50];

  // MatPaginator Output
  pageEvent: PageEvent;
  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    console.log(this.pageEvent);
    this.length = this.pagination.totalItems;
    this.pageSize = e.pageSize;
    this.pageNumber = e.pageIndex;
    this.getArticles();
  }

  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this.pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
    }
  }

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private articleService: ArticleService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getArticles();
  }

  getArticles() {
    this.articleService.getArticles(this.pageNumber + 1, this.pageSize).subscribe(response => {
      this.articles = response.result;
      this.pagination = response.pagination;
      this.length = this.pagination.totalItems;
      this.pageSize = this.pagination.itemsPerPage;
      this.pageIndex = this.pagination.currentPage -1;
      this.dataSource = new MatTableDataSource(this.articles);
      this.dataSource.sort = this.sort;
      console.log(this.pageSize);
    })
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }
}

