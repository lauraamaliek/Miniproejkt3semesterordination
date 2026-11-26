namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void FindPatient()
    {
        Assert.IsNotNull(service.GetPatienter());
    }
    
    [TestMethod]
    public void PatientOgLægemiddel1()
    {
        //Arrange
        string cprNr = "011064-1522";

        
        //Act 
        Patient? patient = service.GetPatienter()
            .FirstOrDefault(p => p.cprnr == cprNr);
        
        //Assert
        
        Assert.IsNotNull(patient);
        Assert.AreEqual("Ulla Nielsen",patient.navn);
    }
    
    
    [TestMethod]
    public void PatientOgLægemiddel2()
    {
        //Arrange
        string cprNr = "121001-6666";
        
        //Act 
        Patient? patient = service.GetPatienter()
            .FirstOrDefault(p => p.cprnr == cprNr);
        
        //Assert
        Assert.IsNull(patient);
    }
    
    [TestMethod]
    public void PatientOgLægemiddel3()
    {
        //Arrange
        string medicin = "Paracetamol";

        //Act 
        Laegemiddel? laegemiddel = service.GetLaegemidler()
            .FirstOrDefault(l => l.navn == medicin);
        //Assert
        Assert.AreEqual(medicin,laegemiddel.navn);
    }
    
    [TestMethod]
    public void PatientOgLægemiddel4()
    {
        //Arrange
        string medicin = "Panodil";

        //Act 
        Laegemiddel? laegemiddel = service.GetLaegemidler()
            .FirstOrDefault(l => l.navn == medicin);
        
        //Assert
        Assert.IsNull(laegemiddel);
    }
    
    [TestMethod]
    public void PatientVægt1()
    {
        //Arrange
        string cprNr = "011064-1522"; //Ulla
        
        string medicin = "Acetylsalicylsyre";

        double forventet = 8.985;
        
        //Act 
        
        Patient? patient = service.GetPatienter()
            .FirstOrDefault(p => p.cprnr == cprNr);
        
        Laegemiddel? laegemiddel = service.GetLaegemidler()
            .FirstOrDefault(l => l.navn == medicin);

        double resultat = service.GetAnbefaletDosisPerDøgn(patient.PatientId, laegemiddel.LaegemiddelId);

        //Assert
        Assert.AreEqual(forventet,resultat);
    }
    
    [TestMethod]
    public void PNOprettelse1()
    {
        //Arrange
        string cprNr = "011064-1522"; //Ulla
        string medicin = "Paracetamol";

        double antal = 3;
        DateTime start = new DateTime(2026, 12, 1);
        DateTime slut = new DateTime(2026, 12, 15);
        
        int antalFør = service.GetPNs().Count;

        //Act 
        Patient? patient = service.GetPatienter()
            .FirstOrDefault(p => p.cprnr == cprNr);
        
        Laegemiddel? laegemiddel = service.GetLaegemidler()
            .FirstOrDefault(l => l.navn == medicin);

        PN nyPN = service.OpretPN(patient.PatientId, laegemiddel.LaegemiddelId, antal, start, slut);
        
        
        //Assert
        Assert.AreEqual(antalFør + 1, service.GetPNs().Count);
        
    }
    
    [TestMethod]
    public void PNOprettelse2()
    {
        //Arrange
       string cprNr = "011064-1522"; //Ulla
       string medicin = "Paracetamol";
       
       DateTime slut = new DateTime(2026, 12, 1);
       DateTime start = new DateTime(2026, 12, 15);
       
       int antalFør = service.GetPNs().Count;

       //Act 
       Patient? patient = service.GetPatienter()
           .FirstOrDefault(p => p.cprnr == cprNr);
       
       Laegemiddel? laegemiddel = service.GetLaegemidler()
           .FirstOrDefault(l => l.navn == medicin);
       
       
       //Assert
       Assert.ThrowsException<ArgumentException>(() =>
           service.OpretPN(patient.PatientId, laegemiddel.LaegemiddelId, 2, start, slut));

       // ... og ordinationen oprettes ikke
       Assert.AreEqual(antalFør, service.GetPNs().Count);
       
    }
    
    
    [TestMethod]
    public void PNOprettelse3()
    {
         //Arrange
             string cprNr = "011064-1522"; //Ulla
             string medicin = "Paracetamol";
             
             DateTime start = new DateTime(2026, 12, 1);
             DateTime slut = new DateTime(2026, 12, 1);
             
             int antalFør = service.GetPNs().Count;

             //Act 
             Patient? patient = service.GetPatienter()
                 .FirstOrDefault(p => p.cprnr == cprNr);
             
             Laegemiddel? laegemiddel = service.GetLaegemidler()
                 .FirstOrDefault(l => l.navn == medicin);
             
             
             //Assert
             Assert.ThrowsException<ArgumentException>(() =>
                 service.OpretPN(patient.PatientId, laegemiddel.LaegemiddelId, 2, start, slut));

             // ... og ordinationen oprettes ikke
             Assert.AreEqual(antalFør, service.GetPNs().Count);
             
    }
    
    
    /*
    
    [TestMethod]
    public void DagligFastOprettelse1()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligFastOprettelse2()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligFastOprettelse3()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligFastOprettelse4()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligFastOprettelse5()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligFastOprettelse6()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligFastOprettelse7()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligSkævOprettelse1()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligSkævOprettelse2()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligSkævOprettelse3()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligSkævOprettelse4()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligSkævOprettelse5()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligSkævOprettelse6()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligSkævOprettelse7()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void DagligSkævOprettelse8()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void KontrolAfDosis1()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void KontrolAfDosis2()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void KontrolAfDosis3()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void KontrolAfDosis4()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void KontrolAfDosis5()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void AfslutOprettelse1()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void AfslutOprettelse2()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void AfslutOprettelse3()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void AfslutOprettelse4()
    {
        //Arrange
        
        
        //Act 

        //Assert
    }
    
    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }
    

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.

        // Hvis koden _ikke_ smider en exception,
        // så fejler testen.

        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }*/
}