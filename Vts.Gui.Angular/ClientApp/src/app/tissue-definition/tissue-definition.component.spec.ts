import { Component, ViewChild, NO_ERRORS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { By } from "@angular/platform-browser";
import { TissueDefinitionComponent } from './tissue-definition.component';

describe('tissue-definition component', () => {
  let testHostComponent: TestHostComponent;
  let testHostFixture: ComponentFixture<TestHostComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        declarations: [TissueDefinitionComponent, TestHostComponent],
        imports: [FormsModule],
        schemas: [NO_ERRORS_SCHEMA]
      })
        .compileComponents();
    }));

  beforeEach(async(() => {
    testHostFixture = TestBed.createComponent(TestHostComponent);
    testHostComponent = testHostFixture.componentInstance;
    testHostComponent.tissueDefinitionComponent.tissueType = {
      display: "Liver",
      value: "Liver"
    };
    testHostFixture.detectChanges();
  }));

  it('should have a tissue type value of Liver', async(() => {
    testHostFixture.whenStable().then(() => {
      const testElement = testHostFixture.debugElement.query(By.css('#tissue-type'));
      expect(testElement.nativeElement.value).toBe('Liver');
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
