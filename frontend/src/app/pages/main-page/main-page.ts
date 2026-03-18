import { Component } from '@angular/core';
import {LinkShortenerForm} from "../../common-ui/link-shortener-form/link-shortener-form";
import {LinksDashboard} from "../../common-ui/links-dashboard/links-dashboard";

@Component({
  selector: 'app-main-page',
    imports: [
        LinkShortenerForm,
        LinksDashboard
    ],
  templateUrl: './main-page.html',
  styleUrl: './main-page.scss',
})
export class MainPage {

}
