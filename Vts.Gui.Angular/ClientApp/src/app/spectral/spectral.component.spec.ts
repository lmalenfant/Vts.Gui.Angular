import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, ComponentFixtureAutoDetect, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { BrowserModule, By } from "@angular/platform-browser";
import { PlotService } from '../services/plot.service';
import { SpectralComponent } from './spectral.component';

let component: SpectralComponent;
let fixture: ComponentFixture<SpectralComponent>;

describe('spectral component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
          declarations: [SpectralComponent],
          imports: [BrowserModule, FormsModule, HttpClientTestingModule],
            providers: [
              { provide: ComponentFixtureAutoDetect, useValue: true },
              PlotService
          ],
          schemas: [
            NO_ERRORS_SCHEMA
          ]
        });
        fixture = TestBed.createComponent(SpectralComponent);
        component = fixture.componentInstance;
    }));

  it('should have a form named form1', async(() => {
    fixture.detectChanges();
    const heading = fixture.debugElement.query(By.css('form'));
    expect(heading.nativeElement.name).toEqual('form1'); //This is a bad test because the value is hard-coded
  }));
});
