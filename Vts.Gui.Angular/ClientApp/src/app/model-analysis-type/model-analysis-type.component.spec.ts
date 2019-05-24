import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { ModelAnalysisTypeComponent } from './model-analysis-type.component';

let component: ModelAnalysisTypeComponent;
let fixture: ComponentFixture<ModelAnalysisTypeComponent>;

describe('model-analysis-type component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ ModelAnalysisTypeComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(ModelAnalysisTypeComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
