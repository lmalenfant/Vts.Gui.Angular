import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { TissueDefinitionComponent } from './tissue-definition.component';

let component: TissueDefinitionComponent;
let fixture: ComponentFixture<TissueDefinitionComponent>;

describe('tissue-definition component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ TissueDefinitionComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(TissueDefinitionComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
