import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-join-room',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './join-room.component.html',
  styleUrls: ['./join-room.component.scss']
})
export class JoinRoomComponent implements OnInit {

  joinRoomForm!: FormGroup;
  fb = inject(FormBuilder);
  router = inject(Router);
  chatService = inject(ChatService);

  ngOnInit(): void {
    this.joinRoomForm = this.fb.group({
      user: ['', Validators.required],
      room: ['', Validators.required]
    });
  }

  joinRoom() {
    const { user, room } = this.joinRoomForm.value;
    sessionStorage.setItem("user", user);
    sessionStorage.setItem("room", room);
    this.chatService.joinRoom(user, room)
      .then(() => {
        this.router.navigate(['chat']);
        console.log('joined')
      }).catch((err) => {
        console.log(err);
      })
  }
}