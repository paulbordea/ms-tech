namespace ServiceA.Services
{
    public interface IBitcoinService
    {
        Task<double> GetBitcoinValue();
    }
}
