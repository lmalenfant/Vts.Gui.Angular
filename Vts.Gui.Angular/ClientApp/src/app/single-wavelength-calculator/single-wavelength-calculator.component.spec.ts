import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { SingleWavelengthCalculatorComponent } from './single-wavelength-calculator.component';

let component: SingleWavelengthCalculatorComponent;
let fixture: ComponentFixture<SingleWavelengthCalculatorComponent>;

describe('single-wavelength-calculator component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ SingleWavelengthCalculatorComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(SingleWavelengthCalculatorComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
