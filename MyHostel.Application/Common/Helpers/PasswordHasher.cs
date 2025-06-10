namespace MyHostel.Application.Common.Helpers;

/*
 * Clase estática que encapsula la lógica de hash de contraseñas
 * BCrypt es un algoritmo de hashing diseñado específicamente para contraseñas.
 * Internamente incluye un salt aleatorio, lo que significa que cada vez que generás un hash para la misma contraseña, el resultado será diferente.
 * Está diseñado para ser computacionalmente costoso, lo que dificulta ataques de fuerza bruta.
 */

public static class PasswordHasher
{
    /// <summary>
    /// Genera un hash seguro para una contraseña de texto plano utilizando el algoritmo BCrypt.
    /// </summary>
    /// <param name="password">Contraseña en texto plano</param>
    /// <returns>Contraseña encriptada (hash)</returns>
    public static string Hash(string password)
    {
        // Usa BCrypt para generar un hash con salt embebido de forma automática
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Verifica si una contraseña en texto plano coincide con un hash almacenado.
    /// </summary>
    /// <param name="password">Contraseña en texto plano</param>
    /// <param name="hash">Hash previamente almacenado</param>
    /// <returns>True si coinciden, false en caso contrario</returns>
    public static bool Verify(string password, string hash)
    {
        // Compara la contraseña ingresada con el hash usando BCrypt
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}