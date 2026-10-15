

class Program
{
    static void main()
    {
        string f = "studenti.csv";
        List<string> reg = caricacsv(f);

        bool esci = false;
        while (!esci)
        {
            Console.WriteLine("\nregistro scolastico");
            Console.WriteLine("1. mostra tutti gli studenti");
            Console.WriteLine("2. aggiungi nuovo studente");
            Console.WriteLine("3. salva e aggiorna file csv");
            Console.WriteLine("4. esci");
            Console.Write("scegli un'opzione: ");

            // leggo input e poi trim
            string sc = Console.ReadLine();
            sc = sc.Trim();

            if (sc == "1")
            {
                mostraregistro(reg);
            }
            else if (sc == "2")
            {
                aggiungistudente(reg);
            }
            else if (sc == "3")
            {
                aggiornacsv(f, reg);
                Console.WriteLine("file csv aggiornato!");
            }
            else if (sc == "4")
            {
                aggiornacsv(f, reg);
                esci = true;
            }
            else
            {
                Console.WriteLine("opzione non valida.");
            }
        }
    }

    // legge il csv
    static List<string> caricacsv(string path)
    {
        List<string> lst = new List<string>();

        if (File.Exists(path))
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    // riga e poi trim
                    string r = sr.ReadLine();
                    r = r.Trim();

                    if (r != "")
                    {
                        lst.Add(r);
                    }
                }
            }
        }

        return lst;
    }

    // stampa e media
    static void mostraregistro(List<string> reg)
    {
        Console.WriteLine("\nelenco studenti");
        for (int i = 0; i < reg.Count; i++)
        {
            string[] c = reg[i].Split(',');

            // variabile e trim
            string n = c[0];
            n = n.Trim();

            string cg = c[1];
            cg = cg.Trim();

            double s = 0;
            int cnt = 0;

            // scorro voti
            for (int j = 2; j < c.Length; j++)
            {
                string vt = c[j];
                vt = vt.Trim();

                if (vt != "")
                {
                    double v = double.Parse(vt);
                    s += v;
                    cnt++;
                }
            }

            double m = 0;
            if (cnt > 0)
            {
                m = s / cnt;
            }

            Console.WriteLine($"studente: {n} {cg} | voti presenti: {cnt} | media: {m:F2}");
        }
    }

    // aggiunge studente
    static void aggiungistudente(List<string> reg)
    {
        Console.Write("inserisci il nome: ");
        string n = Console.ReadLine();
        n = n.Trim();

        Console.Write("inserisci il cognome: ");
        string cg = Console.ReadLine();
        cg = cg.Trim();

        string riga = n + "," + cg;

        for (int i = 1; i <= 4; i++)
        {
            Console.Write($"inserisci il voto {i} (lascia vuoto se manca): ");
            string vt = Console.ReadLine();
            vt = vt.Trim();

            if (vt != "")
            {
                double v = double.Parse(vt);
                if (v >= 2.0 && v <= 10.0)
                {
                    riga += "," + vt;
                }
                else
                {
                    Console.WriteLine("voto non valido! salvato vuoto.");
                    riga += ",";
                }
            }
            else
            {
                riga += ",";
            }
        }

        reg.Add(riga);
        Console.WriteLine("studente aggiunto in ram!");
    }

    // salva file csv
    static void aggiornacsv(string path, List<string> reg)
    {
        using (StreamWriter sw = new StreamWriter(path, false))
        {
            for (int i = 0; i < reg.Count; i++)
            {
                sw.WriteLine(reg[i]);
            }
        }
    }
}
