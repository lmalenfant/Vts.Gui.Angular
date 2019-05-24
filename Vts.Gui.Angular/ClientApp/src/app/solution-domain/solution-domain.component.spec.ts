import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { SolutionDomainComponent } from './solution-domain.component';

let component: SolutionDomainComponent;
let fixture: ComponentFixture<SolutionDomainComponent>;

describe('solution-domain component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ SolutionDomainComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(SolutionDomainComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
