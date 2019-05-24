import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { ForwardSolverAnalysisComponent } from './forward-solver-analysis.component';

let component: ForwardSolverAnalysisComponent;
let fixture: ComponentFixture<ForwardSolverAnalysisComponent>;

describe('forward-solver-analysis component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ ForwardSolverAnalysisComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(ForwardSolverAnalysisComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
