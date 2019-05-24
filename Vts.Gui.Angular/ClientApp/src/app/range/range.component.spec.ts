import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { RangeComponent } from './range.component';

let component: RangeComponent;
let fixture: ComponentFixture<RangeComponent>;

describe('range component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ RangeComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(RangeComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
