import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { By } from "@angular/platform-browser";
import { OptimizerTypeComponent } from './optimizer-type.component';
import { FormsModule } from '@angular/forms';
import { Component, ViewChild, NO_ERRORS_SCHEMA } from '@angular/core';

describe('optimizer-type component', () => {
    let testHostComponent: TestHostComponent;
    let testHostFixture: ComponentFixture<TestHostComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [OptimizerTypeComponent, TestHostComponent],
        imports: [FormsModule],
        schemas: [NO_ERRORS_SCHEMA]    
      })
      .compileComponents();
    }));

    beforeEach(async(() => {
        testHostFixture = TestBed.createComponent(TestHostComponent);
        testHostComponent = testHostFixture.componentInstance;
    }));

    it('should have a title of Optimizer Type', async(() => {
      testHostComponent.optimizerTypeComponent.optimizerType = { value: "levenbergmarquardt" };
      testHostFixture.detectChanges();
      expect(testHostComponent.optimizerTypeComponent.optimizerType.value).toBe('levenbergmarquardt');
    }));

    @Component({
        selector: `host-component`,
        template: `<app-optimizer-type></app-optimizer-type>`,
      })
      class TestHostComponent {
        @ViewChild(OptimizerTypeComponent)
        public optimizerTypeComponent: OptimizerTypeComponent;
      }
});
