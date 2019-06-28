import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { RangeComponent } from './range.component';
import { Component, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';

describe('range component', () => {
    let testHostComponent: TestHostComponent;
    let testHostFixture: ComponentFixture<TestHostComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [RangeComponent, TestHostComponent],
        imports: [FormsModule]
      })
      .compileComponents();
    }));

    beforeEach(async(() => {
        testHostFixture = TestBed.createComponent(TestHostComponent);
        testHostComponent = testHostFixture.componentInstance;
        testHostComponent.rangeComponent.range = {
          title: 'Detector Positions',
          startLabel: 'Begin',
          startLabelUnits: 'mm',
          startValue: 0.5,
          endLabel: 'End',
          endLabelUnits: 'mm',
          endValue: 9.5,
          numberLabel: 'Number',
          numberValue: 19
        };
        testHostFixture.detectChanges();
      }));

    it('should have a title of Detector Positions', async(() => {
      const testElement = testHostFixture.debugElement.query(By.css('.heading'));
      expect(testElement.nativeElement.innerText).toEqual('Detector Positions');
    }));

    it('should have a start label of Begin', async(() => {
      const testElement = testHostFixture.debugElement.query(By.css('#startLabel'));
      expect(testElement.nativeElement.innerText).toEqual('Begin');
    }));

    it('should have a begin value of 0.5', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('#startValue'));
        expect(testElement.nativeElement.value).toBe('0.5');
      });
    }));

    it('should have mm as the start units', async(() => {
      const testElement = testHostFixture.debugElement.query(By.css('#startUnits'));
      expect(testElement.nativeElement.innerText).toEqual('mm');
    }));

    it('should have an end label of End', async(() => {
      const testElement = testHostFixture.debugElement.query(By.css('#endLabel'));
      expect(testElement.nativeElement.innerText).toEqual('End');
    }));

    it('should have an end value of 9.5', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('#endValue'));
        expect(testElement.nativeElement.value).toBe('9.5');
      });
    }));

    it('should have mm as the end units', async(() => {
      const testElement = testHostFixture.debugElement.query(By.css('#endUnits'));
      expect(testElement.nativeElement.innerText).toEqual('mm');
    }));

    it('should have a number label of Number', async(() => {
      const testElement = testHostFixture.debugElement.query(By.css('#numberLabel'));
      expect(testElement.nativeElement.innerText).toEqual('Number');
    }));

    it('should have a number value of 19', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('#numberValue'));
        expect(testElement.nativeElement.value).toBe('19');
      });
    }));

    it('should allow the start value to be changed to 0.25', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('#startValue'));
        testElement.nativeElement.value = '0.25';
        testElement.nativeElement.dispatchEvent(new Event('input'));
        testHostFixture.detectChanges();
        expect(testHostComponent.rangeComponent.range.startValue).toBe('0.25');
      });
    }));

    it('should allow the end value to be changed to 5.25', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('#endValue'));
        testElement.nativeElement.value = '5.25';
        testElement.nativeElement.dispatchEvent(new Event('input'));
        testHostFixture.detectChanges();
        expect(testHostComponent.rangeComponent.range.endValue).toBe('5.25');
      });
    }));

    it('should allow the number value to be changed to 29', async(() => {
      testHostFixture.whenStable().then(() => {
        const testElement = testHostFixture.debugElement.query(By.css('#numberValue'));
        testElement.nativeElement.value = '29';
        testElement.nativeElement.dispatchEvent(new Event('input'));
        testHostFixture.detectChanges();
        expect(testHostComponent.rangeComponent.range.numberValue).toBe('29');
      });
    }));

    @Component({
        selector: `host-component`,
        template: `<app-range></app-range>`,
      })
      class TestHostComponent {
        @ViewChild(RangeComponent)
        public rangeComponent: RangeComponent;
      }
});
