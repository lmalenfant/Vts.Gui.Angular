import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { ForwardSolverEngineComponent } from './forward-solver-engine.component';

let component: ForwardSolverEngineComponent;
let fixture: ComponentFixture<ForwardSolverEngineComponent>;

describe('forward-solver-engine component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ ForwardSolverEngineComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(ForwardSolverEngineComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
