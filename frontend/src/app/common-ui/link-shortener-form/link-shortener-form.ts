import {Component, inject} from '@angular/core';
import {LinkShortenerService} from '../../data/services/link-shortener.service';
import {
  AbstractControl,
  FormBuilder,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';

@Component({
  selector: 'app-link-shortener-form',
  imports: [
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './link-shortener-form.html',
  styleUrl: './link-shortener-form.scss',
})
export class LinkShortenerForm {
  private fb = inject(FormBuilder)
  private linkShortenerService = inject(LinkShortenerService)

  form = this.fb.nonNullable.group({
    originalUrl: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.maxLength(2048),
      LinkShortenerForm.urlValidator()
    ]),
    shortUrl: this.fb.nonNullable.control({
      value: '',
      disabled: true
    })
  })

  onSubmit() {
    if (this.form.valid) {
      const shortUlr = this.linkShortenerService.shortTheLink(this.form.controls.originalUrl.value);
    }
  }

  private static urlValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      try {
        new URL(control.value);
        return null;
      } catch {
        return { invalidUrl: true };
      }
    };
  }
}
