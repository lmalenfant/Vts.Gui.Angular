import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { By } from "@angular/platform-browser";
import { ForwardSolverEngineComponent } from './forward-solver-engine.component';
import { FormsModule } from '@angular/forms';
import { Component, ViewChild, NO_ERRORS_SCHEMA } from '@angular/core';

describe('forward-solver-engine component', () => {
    let testHostComponent: TestHostComponent;
    let testHostFixture: ComponentFixture<TestHostComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [ForwardSolverEngineComponent, TestHostComponent],
        imports: [FormsModule],
        schemas: [NO_ERRORS_SCHEMA]    
      })
      .compileComponents();
      testHostFixture = TestBed.createComponent(TestHostComponent);
      testHostComponent = testHostFixture.componentInstance;
      testHostComponent.forwardSolverEngineComponent.forwardSolverEngine = { 
        value: 'DistributedPointSourceSDA', 
        display: 'Standard Diffusion (Analytic: Distributed Point Source)' 
      };
      testHostComponent.forwardSolverEngineComponent.gaussianBeam = {
        show: false,
        diameter: 0.1
      };
      testHostFixture.detectChanges();
    }));

    it('should have a forward solver value of DistributedPointSourceSDA', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('#forwardSolverEngine'));
        expect(testElement.nativeElement.value).toBe('DistributedPointSourceSDA');
      });
    }));

    it('should show the Gaussian diameter when the forward solver is changed to DistributedGaussianSourceSDA', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('#forwardSolverEngine'));
        testElement.nativeElement.value = 'DistributedGaussianSourceSDA';
        testElement.nativeElement.dispatchEvent(new Event('change'));
        testHostFixture.detectChanges();
        expect(testHostComponent.forwardSolverEngineComponent.forwardSolverEngine.value).toBe('DistributedGaussianSourceSDA');
        expect(testHostComponent.forwardSolverEngineComponent.gaussianBeam.show).toBe(true);
      });
    }));

    it('should be able to change the Gaussian diameter to 2 when it is visible', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('#forwardSolverEngine'));
        testElement.nativeElement.value = 'DistributedGaussianSourceSDA';
        testElement.nativeElement.dispatchEvent(new Event('change'));
        testHostFixture.detectChanges();
        expect(testHostComponent.forwardSolverEngineComponent.forwardSolverEngine.value).toBe('DistributedGaussianSourceSDA');
        expect(testHostComponent.forwardSolverEngineComponent.gaussianBeam.show).toBe(true);
        testHostFixture.whenStable().then(() => {
          const gaussianTestElement = testHostFixture.debugElement.query(By.css('#gaussianBeamDiameter'));
          gaussianTestElement.nativeElement.value = 2;
          gaussianTestElement.nativeElement.dispatchEvent(new Event('input'));
          testHostFixture.detectChanges();
          expect(testHostComponent.forwardSolverEngineComponent.gaussianBeam.diameter.toString()).toBe('2');
        });
      });
    }));

    @Component({
        selector: `host-component`,
        template: `<app-forward-solver-engine></app-forward-solver-engine>`,
      })
      class TestHostComponent {
        @ViewChild(ForwardSolverEngineComponent)
        public forwardSolverEngineComponent: ForwardSolverEngineComponent;
      }
});
