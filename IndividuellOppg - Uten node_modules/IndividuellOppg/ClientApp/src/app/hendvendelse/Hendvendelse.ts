import { Kategori } from "../faq/Kategori";

export class Hendvendelse {
  id: number;
  kategori: Kategori;
  email: string;
  melding: string;
  status: boolean;
}
