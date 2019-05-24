import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { InverseSolverComponent } from './inverse-solver.component';

let component: InverseSolverComponent;
let fixture: ComponentFixture<InverseSolverComponent>;

describe('inverse-solver component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ InverseSolverComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(InverseSolverComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
