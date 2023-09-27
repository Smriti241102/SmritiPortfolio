package dmit2015.model;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvFileSource;

import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class CanadianPersonalIncomeTaxTest {

    @ParameterizedTest
    @CsvFileSource(resources = "/FederalIncomeTax_TestData.csv", numLinesToSkip = 1)
    void federalIncomeTax(String text, double taxableIncome,double expectedIncome )
    {
        CanadianPersonalIncomeTax instance = new CanadianPersonalIncomeTax();

        instance.setTaxableIncome(taxableIncome);
        assertEquals(expectedIncome, instance.federalIncomeTax(), 0.50);
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/ProvincialIncomeTax_TestData.csv", numLinesToSkip = 1)
    void provincialIncomeTax(String text, double taxableIncome,double expectedIncome )
    {
        CanadianPersonalIncomeTax instance = new CanadianPersonalIncomeTax();

        instance.setTaxableIncome(taxableIncome);
        assertEquals(expectedIncome, instance.provincialIncomeTax(), 0.50);
    }
}

