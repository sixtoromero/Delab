﻿using System.Text;

namespace Delab.Helpers;

public class UtilityTools : IUtilityTools
{
    public string GeneratePass(int longitud, string caracteres)
    {
        StringBuilder res = new();
        Random rnd = new();
        while (0 < longitud--)
        {
            res.Append(caracteres[rnd.Next(caracteres.Length)]);
        }
        return res.ToString();

    }
}
