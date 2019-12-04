import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { By } from "@angular/platform-browser";
import { SolutionDomainComponent } from './solution-domain.component';
import { FormsModule } from '@angular/forms';
import { Component, ViewChild, NO_ERRORS_SCHEMA } from '@angular/core';

describe('solution-domain component', () => {
    let testHostComponent: TestHostComponent;
    let testHostFixture: ComponentFixture<TestHostComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [SolutionDomainComponent, TestHostComponent],
        imports: [FormsModule],
        schemas: [NO_ERRORS_SCHEMA]    
      })
      .compileComponents();
      testHostFixture = TestBed.createComponent(TestHostComponent);
      testHostComponent = testHostFixture.componentInstance;
      testHostComponent.solutionDomainComponent.solutionDomain = { value: "ROfRho" };
      testHostComponent.solutionDomainComponent.range = {
        title: 'Detector Positions',
        startLabel: 'Begin',
        startLabelUnits: 'mm',
        start: 0.5,
        endLabel: 'End',
        endLabelUnits: 'mm',
        stop: 9.5,
        numberLabel: 'Number',
        count: 19
      };
      testHostComponent.solutionDomainComponent.independentAxes = {
        show: false,
        first: 'ρ',
        second: 't',
        label: 't',
        value: 0.05,
        units: 'ns',
        firstUnits: 'mm',
        secondUnits: 'ns'
      };
      testHostComponent.solutionDomainComponent.id = "Test";
      testHostFixture.detectChanges();
    }));

    it('should have a solution domain value of R(ρ)', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('input[ng-reflect-name="SolutionDomainTest"]'));
        expect(testElement.nativeElement.value).toBe('ROfRho');
      });
    }));

    it('should allow the model analysis type to be changed to R(fx, t)', async(() => {
      testHostFixture.whenStable().then(() => {
        let options = testHostFixture.debugElement.queryAll(By.css('input[ng-reflect-name="SolutionDomainTest"]'));
        options[3].triggerEventHandler('change', { target: options[3].nativeElement });
        options[3].nativeElement.click();
        testHostFixture.detectChanges();
        expect(testHostComponent.solutionDomainComponent.solutionDomain.value).toBe('ROfFxAndTime');
      });
    }));

    it('should show the independent axis when R(fx, ft) is clicked', async(() => {
      testHostFixture.whenStable().then(() => {
        let options = testHostFixture.debugElement.queryAll(By.css('input[ng-reflect-name="SolutionDomainTest"]'));
        options[5].triggerEventHandler('change', { target: options[5].nativeElement });
        options[5].nativeElement.click();
        testHostFixture.detectChanges();
        expect(testHostComponent.solutionDomainComponent.solutionDomain.value).toBe('ROfFxAndFt');
        expect(testHostComponent.solutionDomainComponent.independentAxes.show).toBe(true);
      });
    }));

    it('should have independent axes of fx and ft when R(fx, ft) is clicked', async(() => {
      testHostFixture.whenStable().then(() => {
        let options = testHostFixture.debugElement.queryAll(By.css('input[ng-reflect-name="SolutionDomainTest"]'));
        options[5].triggerEventHandler('change', { target: options[5].nativeElement });
        options[5].nativeElement.click();
        testHostFixture.detectChanges();
        options = testHostFixture.debugElement.queryAll(By.css('input[name="IndependentAxis"]'));   
        expect(options[0].attributes["ng-reflect-value"]).toBe('ft');
        expect(options[1].attributes["ng-reflect-value"]).toBe('fx');
      });
    }));

    it('should have independent axes of ρ and t and t has value 0.05 ns', async(() => {
      testHostFixture.whenStable().then(() => {
        let options = testHostFixture.debugElement.queryAll(By.css('input[ng-reflect-name="SolutionDomainTest"]'));
        options[2].triggerEventHandler('change', { target: options[2].nativeElement });
        options[2].nativeElement.click();
        testHostFixture.detectChanges();
        options = testHostFixture.debugElement.queryAll(By.css('input[name="IndependentAxis"]'));   
        expect(options[0].attributes["ng-reflect-value"]).toBe('t');
        expect(options[1].attributes["ng-reflect-value"]).toBe('ρ');
        testHostFixture.whenStable().then(() => {
          let testElement = testHostFixture.debugElement.query(By.css('#independentAxisValue'));
          expect(testElement.nativeElement.value).toBe('0.05');
          testElement = testHostFixture.debugElement.query(By.css('#independentAxisLabel'));
          expect(testElement.nativeElement.innerText).toEqual('t');
          testElement = testHostFixture.debugElement.query(By.css('#independentAxisUnits'));
          expect(testElement.nativeElement.innerText).toEqual('ns');
        });
      });
    }));

    @Component({
        selector: `host-component`,
        template: `<app-solution-domain></app-solution-domain>`,
      })
      class TestHostComponent {
        @ViewChild(SolutionDomainComponent)
        public solutionDomainComponent: SolutionDomainComponent;
      }
});
