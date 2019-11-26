import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { ScattererTypeComponent } from './scatterer-type.component';
import { Component, ViewChild, NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';

describe('scatterer-type component', () => {
  let testHostComponent: TestHostComponent;
  let testHostFixture: ComponentFixture<TestHostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ScattererTypeComponent, TestHostComponent],
      imports: [FormsModule],
      schemas: [NO_ERRORS_SCHEMA]
    });
    testHostFixture = TestBed.createComponent(TestHostComponent);
    testHostComponent = testHostFixture.componentInstance;
  }));

  it('Scatter Type should be PowerLaw', async(() => {
    testHostComponent.scattererTypeComponent.intralipid = { volumeFraction: 0.01, show: false };
    testHostComponent.scattererTypeComponent.powerLaw = { a: 1.2, b: 1.42, show: true };
    testHostComponent.scattererTypeComponent.mieParticle = { particleRadius: 0.5, ParticleRefractiveIndexMismatch: 1.4, MediumRefractiveIndexMismatch: 1, volumeFraction: 0.01, show: false };
    testHostComponent.scattererTypeComponent.scattererType = { value: 'PowerLaw', display: 'PowerLaw [A*λ^(-b)]' };
    testHostFixture.detectChanges();
    expect(testHostComponent.scattererTypeComponent.scattererType.value).toEqual('PowerLaw');
  }));

  @Component({
    selector: `host-component`,
    template: `<app-scatterer-type></app-scatterer-type>`,
  })
  class TestHostComponent {
    @ViewChild(ScattererTypeComponent)
    public scattererTypeComponent: ScattererTypeComponent;
  }
});
