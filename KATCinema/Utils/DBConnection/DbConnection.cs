using Npgsql;
using System.Security.Cryptography.X509Certificates;

class DBConnection
{
    public NpgsqlConnection Connect(string connString)
    {
        var conn = new NpgsqlConnection(connString);
        conn.ProvideClientCertificatesCallback += new ProvideClientCertificatesCallback(MyClientCertificates);

        try
        {
            conn.Open();
        }
        catch(Exception ex)
        {
            throw(new Exception($"Connection error: {ex.Message}"));
        }
        
        Console.WriteLine("Connection is succesfully established");

        return conn;
    }

    private void MyClientCertificates(X509CertificateCollection certificates)
    {
        var cert = new X509Certificate("Utils\\DBConnection\\root.crt");
        certificates.Add(cert);
    }
}
