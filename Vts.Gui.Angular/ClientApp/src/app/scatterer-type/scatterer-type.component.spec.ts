import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { ScattererTypeComponent } from './scatterer-type.component';

let component: ScattererTypeComponent;
let fixture: ComponentFixture<ScattererTypeComponent>;

describe('scatterer-type component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ ScattererTypeComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(ScattererTypeComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
