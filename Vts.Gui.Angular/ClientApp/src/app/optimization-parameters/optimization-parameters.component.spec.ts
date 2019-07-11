import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { By } from "@angular/platform-browser";
import { OptimizationParametersComponent } from './optimization-parameters.component';
import { FormsModule } from '@angular/forms';
import { Component, ViewChild, NO_ERRORS_SCHEMA } from '@angular/core';

describe('optimization-parameters component', () => {
    let testHostComponent: TestHostComponent;
    let testHostFixture: ComponentFixture<TestHostComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [OptimizationParametersComponent, TestHostComponent],
        imports: [FormsModule],
        schemas: [NO_ERRORS_SCHEMA]    
      })
      .compileComponents();
    }));

    beforeEach(async(() => {
        testHostFixture = TestBed.createComponent(TestHostComponent);
        testHostComponent = testHostFixture.componentInstance;
    }));

    it('should have a title of Optimization Parameters', async(() => {
      testHostComponent.optimizationParametersComponent.solutionDomain = { value: "muaandmusp" };
      testHostFixture.detectChanges();
      const heading = testHostFixture.debugElement.query(By.css('.heading'));
      expect(heading.nativeElement.innerText).toEqual('Optimization Parameters'); //This is a bad test because the value is hard-coded
    }));

    @Component({
        selector: `host-component`,
        template: `<app-solution-domain></app-optimization-parameters>`,
      })
      class TestHostComponent {
        @ViewChild(OptimizationParametersComponent)
        public optimizationParametersComponent: OptimizationParametersComponent;
      }
});
