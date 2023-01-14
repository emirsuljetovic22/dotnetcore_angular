import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  maxDate: Date;
  validationErrors: string[] = [];
  hide = true;

  constructor(private snackBar: MatSnackBar, private accountService: AccountService /* private toastr: ToastrService*/, private fb: FormBuilder, private router: Router, public dialogRef: MatDialogRef<RegisterComponent>) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() -18);
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      dateofbirth: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    })
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value
      ? null : {isMatching: true}
    }
  }

  register() {
    console.log(this.registerForm.value);
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/jobs');
      this.dialogRef.close();
      //this.snackBar.open("dialog closed test");
    }, error => {
      this.validationErrors = error;
    })
  }

  closeModal() {
    throw new Error('Method not implemented.');
  }
}

export function openEditArticleDialog(dialog: MatDialog) {
  const config = new MatDialogConfig();
  config.disableClose = true;
  config.autoFocus = true;
  config.minWidth = '800px';

  const dialogRef = dialog.open(RegisterComponent, config);
  return dialogRef.afterClosed();
}
