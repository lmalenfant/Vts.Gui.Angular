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

    it('should have a value of μa and μs', async(() => {
      testHostComponent.optimizationParametersComponent.optimizationParameters = { value: "muaandmusp" };
      testHostFixture.detectChanges();
      expect(testHostComponent.optimizationParametersComponent.optimizationParameters.value).toBe('muaandmusp');
    }));

    @Component({
        selector: `host-component`,
        template: `<app-optimization-parameters></app-optimization-parameters>`,
      })
      class TestHostComponent {
        @ViewChild(OptimizationParametersComponent)
        public optimizationParametersComponent: OptimizationParametersComponent;
      }
});
