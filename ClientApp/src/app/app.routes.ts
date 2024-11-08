import { Routes } from '@angular/router';
import { JoinRoomComponent } from './components/join-room/join-room.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { ChatComponent } from './components/chat/chat.component';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'join-room'
    },
    {
        path: 'join-room',
        component: JoinRoomComponent,
    },
    {
        path: 'welcome',
        component: WelcomeComponent,
    },
    {
        path: 'chat',
        component: ChatComponent,
    }
];
