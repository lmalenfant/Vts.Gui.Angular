import { Component, NO_ERRORS_SCHEMA, ViewChild } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { By } from "@angular/platform-browser";
import { Liver, Skin } from './absorber-list';
import { TissueDefinitionComponent } from './tissue-definition.component';
import { TissueTypeList } from './tissue-list';

describe('tissue-definition component', () => {
  let testHostComponent: TestHostComponent;
  let testHostFixture: ComponentFixture<TestHostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [TissueDefinitionComponent, TestHostComponent],
      imports: [FormsModule],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
    testHostFixture = TestBed.createComponent(TestHostComponent);
    testHostComponent = testHostFixture.componentInstance;
    testHostComponent.tissueDefinitionComponent.tissueTypeList = TissueTypeList;
    testHostComponent.tissueDefinitionComponent.tissueType = {
      display: "Liver",
      value: "Liver"
    };
    testHostComponent.tissueDefinitionComponent.absorberConcentration = Liver;
    testHostComponent.tissueDefinitionComponent.bloodConcentration = { totalHb: 190, bloodVolume: 0.0817, stO2: 0.65, visible: true };
    testHostComponent.tissueDefinitionComponent.scattererType = { value: 'PowerLaw', display: 'PowerLaw [A*λ^(-b)]' };
    testHostComponent.tissueDefinitionComponent.powerLaw = { a: 1.2, b: 1.42, show: true };
    testHostComponent.tissueDefinitionComponent.intralipid = { volumeFraction: 0.01, show: false };
    testHostComponent.tissueDefinitionComponent.mieParticle = { particleRadius: 0.5, ParticleRefractiveIndexMismatch: 1.4, MediumRefractiveIndexMismatch: 1, volumeFraction: 0.01, show: false };
    testHostFixture.detectChanges();
  }));

  it('should have a tissue type value of Liver', async(() => {
    testHostFixture.whenStable().then(() => {
      const testElement = testHostFixture.debugElement.query(By.css('#tissue-type'));
      console.log(testHostFixture);
      console.log(testElement);
      expect(testElement.nativeElement.value).toBe('Liver');
    });
  }));

  it('should change the absorber values when tissue type is changed to Skin', async(() => {
    testHostFixture.whenStable().then(() => {
      const testElement = testHostFixture.debugElement.query(By.css('#tissue-type'));
      testElement.nativeElement.value = 'Skin';
      testElement.nativeElement.dispatchEvent(new Event('change'));
      testHostFixture.detectChanges();
      expect(testHostComponent.tissueDefinitionComponent.tissueType.value).toBe('Skin');
      //expect(testHostComponent.tissueDefinitionComponent.tissueType.display).toBe('Skin');
      expect(testHostComponent.tissueDefinitionComponent.absorberConcentration).toBe(Skin);
    });
  }));

  @Component({
    selector: `host-component`,
    template: `<app-tissue-definition></app-tissue-definition>`,
  })
  class TestHostComponent {
    @ViewChild(TissueDefinitionComponent)
    public tissueDefinitionComponent: TissueDefinitionComponent;
  }
});
