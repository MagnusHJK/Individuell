import { Kategori } from "./Kategori";

export class FAQ {
  id: number;
  kategori: Kategori;
  sporsmal: string;
  svar: string;
  oppstemmer: number;
  nedstemmer: number;
}
