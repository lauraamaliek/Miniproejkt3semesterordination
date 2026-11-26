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
    public void Vaelglaegemiddel()
    {
        
    }

    [TestMethod]
    public void Beregning()
    {
        
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
    public void OpretPN()
    {
        
    }

    [TestMethod]
    public void OpretDagligSkaev()
    {
        
    }

    [TestMethod]
    public void ValideringAfDatoer()
    {
        
    }

    [TestMethod]
    public void Dosisvalidering()
    {
        
    }

    [TestMethod]
    public void VisDagligSkaev()
    {
        
    }

    [TestMethod]
    public void Afslutning()
    {
        
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
    }
}