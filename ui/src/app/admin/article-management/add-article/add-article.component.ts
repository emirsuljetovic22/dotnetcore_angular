import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Tag } from 'src/app/_models/articles/articleTags';
import { Category } from 'src/app/_models/articles/category';
import { ArticleService } from 'src/app/_services/article.service';
import { HtmlEditorService } from 'src/app/_services/html-editor.service';

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html',
  styleUrls: ['./add-article.component.css']
})
export class AddArticleComponent implements OnInit {

  addArticleForm: FormGroup;
  public editorConfiguration = this.editorConfig.editorConfig;

  categories: Category[];
  tags: Tag[];
  srcResult: any;
  selectedFiles: FileList | [] = [];
  myFiles: string [] = [];
  filesToUpload: FileList;
  formData = new FormData();
  tagsToSend: string [] = [];

  constructor(private articleService: ArticleService,
    private editorConfig: HtmlEditorService,) { }

  ngOnInit(): void {
    this.getCategories();
    this.getTags();
    this.formInit();
  }

  formInit() {
    this.addArticleForm = new FormGroup({
      title: new FormControl(''),
      shortDescription: new FormControl(''),
      url: new FormControl(''),
      content: new FormControl(''),
      category: new FormControl(''),
      tags: new FormControl([])
    });
  }
  getCategories() {
    this.articleService.getCategories().subscribe(response => {
      this.categories = response;
      //console.log(this.categories);
    })
  }

  getTags() {
    this.articleService.getAllTags().subscribe(response => {
      this.tags = response;
    })
  }

  onFileChange(event:any) {
    for (var i = 0; i < event.target.files.length; i++) {
        this.myFiles.push(event.target.files[i]);
        this.formData.append('files', event.target.files[i]);
    }
  }

  save2() {
    this.tagsToSend = this.addArticleForm.value['tags'];

    const tags = Object.values(this.tagsToSend);

    for (const tag of tags) {
      console.log(tag);
      this.formData.append('tags', tag);
    }

    const values2 = this.formData.getAll('tags');
    console.log(values2);
  }

  save() {
    this.tagsToSend = this.addArticleForm.value['tags'];

    const tags = Object.values(this.tagsToSend);

    for (const tag of tags) {
      console.log(tag);
      this.formData.append('tags', tag);
    }

    this.formData.append('title', this.addArticleForm.get('title').value);
    this.formData.append('shortDescription', this.addArticleForm.get('shortDescription').value);
    this.formData.append('url', this.addArticleForm.get('url').value);
    this.formData.append('content', this.addArticleForm.get('content').value);
    this.formData.append('category', this.addArticleForm.get('category').value);

    this.articleService.addArticle(this.formData).subscribe(response => {
      console.log(response);
    }, err => {
      console.log(err.message);
    });

  }

}
