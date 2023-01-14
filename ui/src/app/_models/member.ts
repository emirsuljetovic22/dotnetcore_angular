import { Photo } from "./photo";

export interface Member {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  dateOfBirth: number;
  createdAt: Date;
  lastActive: Date;
  photoUrl: string;
  about: string;
  city: string;
  region: string;
  country: string;
  photos: Photo[];
}
