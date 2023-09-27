package dmit2015.model;

import lombok.Data;

/**
 * This class is created to calculate income tax of canadians in alberta
 * @author Smriti Rani
 * @version 2023-02-01
 *
 */
@Data
public class CanadianPersonalIncomeTax {

    private int taxYear = 2023;

    private double taxableIncome = 50000;

    private String province = "AB";

    public CanadianPersonalIncomeTax() {

    }

    public CanadianPersonalIncomeTax(int taxYear, double taxableIncome, String province) {
        this.taxYear = taxYear;
        this.taxableIncome = taxableIncome;
        this.province = province;
    }

    public double federalIncomeTax() {
        if (taxableIncome < 53360) {
            return 0.15 * taxableIncome;
        } else if (taxableIncome < 106718) {

            return (0.15 * 53359) + (0.205 * (taxableIncome-53359));
        } else if (taxableIncome < 165431) {

            return (0.15 * 53359) + (0.205 * (106718-53360)) + (0.26 * (taxableIncome-106717));
        } else if (taxableIncome < 235676) {
            return (0.15 * 53359) + (0.205 * (106718-53360)) + (0.26 * (165431-106717)) +(0.29 * (taxableIncome-165430));
        } else {
            return (0.15 * 53359) + (0.205 * (106718-53360)) + (0.26 * (165431-106717)) +(0.29 * (235675-165430)) + (0.33 * (taxableIncome-235675));
        }


    }

    public double provincialIncomeTax() {
        if (taxableIncome < 142293) {
            return 0.10 * taxableIncome;
        } else if (taxableIncome < 170752) {
            return (0.10 * 142292) + (0.12 * (taxableIncome-142292)) ;
        } else if (taxableIncome < 227669) {
            return (0.10 * 142292) + (0.12 * (170751-142292)) + (0.13 * (taxableIncome-170751));
        } else if (taxableIncome < 341503) {
            return (0.10 * 142292) + (0.12 * (170751-142292)) + (0.13 * (227668-170751)) + (0.14 * (taxableIncome-227668));
        } else {
            return (0.10 * 142292) + (0.12 * (170751-142292)) + (0.13 * (227668-170751)) + (0.14 * (341503-227668)) +(0.15 * (taxableIncome-341502));
        }
    }

    public double totalIncomeTax() {
        return federalIncomeTax() + provincialIncomeTax();
    }
}
