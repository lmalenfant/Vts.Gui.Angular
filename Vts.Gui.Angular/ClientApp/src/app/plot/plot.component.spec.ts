import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { PlotComponent } from './plot.component';

let component: PlotComponent;
let fixture: ComponentFixture<PlotComponent>;

describe('plot component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ PlotComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(PlotComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
