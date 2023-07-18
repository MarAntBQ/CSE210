class TaxCalculator
{
    private const float EcuadorTaxRate = 0.12f;

    public float CalculateTaxes(float subtotal)
    {
        return subtotal * EcuadorTaxRate;
    }
}