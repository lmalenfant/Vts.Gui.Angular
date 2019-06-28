import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { ModelAnalysisTypeComponent } from './model-analysis-type.component';
import { FormsModule } from '@angular/forms';
import { Component, ViewChild } from '@angular/core';

describe('model-analysis-type component', () => {
    let testHostComponent: TestHostComponent;
    let testHostFixture: ComponentFixture<TestHostComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ModelAnalysisTypeComponent, TestHostComponent],
        imports: [FormsModule]
      })
      .compileComponents();
      testHostFixture = TestBed.createComponent(TestHostComponent);
      testHostComponent = testHostFixture.componentInstance;
      testHostComponent.modelAnalysisTypeComponent.modelAnalysisType = { value: 'R' };
      testHostFixture.detectChanges();
  }));

    it('should have value of checked for R', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('input[name="ModelAnalysis"]'));
        expect(testElement.nativeElement.value).toBe('R');
      });
    }));

    it('should allow the model analysis type to be changed to ∂R/∂μs`', async(() => {
      testHostFixture.whenStable().then(() => {
        let options = testHostFixture.debugElement.queryAll(By.css('input[name="ModelAnalysis"]'));
        options[2].triggerEventHandler('change', { target: options[2].nativeElement });
        testHostFixture.detectChanges();
        expect(testHostComponent.modelAnalysisTypeComponent.modelAnalysisType.value).toBe('dRdMusp');
      });
    }));

    @Component({
        selector: `host-component`,
        template: `<app-model-analysis-type></app-model-analysis-type>`,
      })
      class TestHostComponent {
        @ViewChild(ModelAnalysisTypeComponent)
        public modelAnalysisTypeComponent: ModelAnalysisTypeComponent;
      }
});
