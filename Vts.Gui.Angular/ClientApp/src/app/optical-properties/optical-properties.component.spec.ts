import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { OpticalPropertiesComponent } from './optical-properties.component';

let component: OpticalPropertiesComponent;
let fixture: ComponentFixture<OpticalPropertiesComponent>;

describe('optical-properties component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ OpticalPropertiesComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(OpticalPropertiesComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
