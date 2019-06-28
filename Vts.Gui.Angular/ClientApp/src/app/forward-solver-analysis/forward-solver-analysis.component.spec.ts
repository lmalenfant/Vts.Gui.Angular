import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed, async, ComponentFixture, inject, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { ViewChild, Component, NO_ERRORS_SCHEMA } from '@angular/core';
import { By, BrowserModule } from "@angular/platform-browser";
import { FormsModule } from '@angular/forms';
import { ForwardSolverAnalysisComponent } from './forward-solver-analysis.component';
import { PlotService } from '../services/plot.service';

let component: ForwardSolverAnalysisComponent;
let fixture: ComponentFixture<ForwardSolverAnalysisComponent>;

describe('forward-solver-analysis component', () => {
    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ForwardSolverAnalysisComponent],
        imports: [BrowserModule, FormsModule, HttpClientTestingModule],
        providers: [
          { provide: ComponentFixtureAutoDetect, useValue: true },
          PlotService
        ],
        schemas: [
          NO_ERRORS_SCHEMA
        ]    
      });
      fixture = TestBed.createComponent(ForwardSolverAnalysisComponent);
      component = fixture.componentInstance;
    }));

    it('should have a form named form1', async(() => {
      fixture.detectChanges();
      const heading = fixture.debugElement.query(By.css('form'));
      expect(heading.nativeElement.name).toEqual('form1'); //This is a bad test because the value is hard-coded
    }));
});
