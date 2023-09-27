package dmit2015.model;

import java.time.LocalDate;
import java.time.Month;

import static java.time.temporal.ChronoUnit.YEARS;
import lombok.Data;


/**
 * This class is created for Findiding the astrological animal and astrological sign of a person
 * @author Smriti Rani
 * @version 2023-02-01
 *
 */
@Data
public class Person {

    //    variables created
    private String firstname;
    private String lastname;
    private java.time.LocalDate birthDate;
    private String birthDateString;

    public java.time.LocalDate today = java.time.LocalDate.now();

    public String getFirstname() {
        return firstname;
    }

    public void setFirstname(String firstName) {
        this.firstname = firstName;
    }

    public String getLastName() {
        return lastname;
    }

    public void setLastName(String lastName) {
        this.lastname = lastName;
    }

    public LocalDate getBirthDate() {
        return birthDate;
    }

    public void setBirthDate(LocalDate birthDate) {
        this.birthDate = birthDate;
    }

    public String getBirthDateString() {
        return birthDateString;
    }

    public void setBirthDateString(String birthDateString) {
        this.birthDateString = birthDateString;

        birthDate = LocalDate.parse(birthDateString);
    }

    public Person() {
        this.firstname = "DMIT2015";
        this.lastname = "Student";
        this.birthDate = LocalDate.now();
    }

    public Person(String firstName, String lastname, LocalDate birthDateString) {
        this.firstname = firstName;
        this.lastname = lastname;
        setBirthDateString(birthDateString.toString());
    }

    public long currentAge() {


        java.time.LocalDate birthDate1 = java.time.LocalDate.parse(birthDateString);

        return YEARS.between(birthDate1, today);
    }

    public long ageOn(java.time.LocalDate onDate) {
        return YEARS.between(birthDate, onDate);
    }

    public String chineseZodiac() {
        int zodiacNumber = birthDate.getYear() % 12;
        String animal;

        if (zodiacNumber == 0) {
            animal = "Monkey";
        } else if (zodiacNumber == 1) {
            animal = "Rooster";
        } else if (zodiacNumber == 2) {
            animal = "Dog";
        } else if (zodiacNumber == 3) {
            animal = "Pig";
        } else if (zodiacNumber == 4) {
            animal = "Rat";
        } else if (zodiacNumber == 5) {
            animal = "Ox";
        } else if (zodiacNumber == 6) {
            animal = "Tiger";
        } else if (zodiacNumber == 7) {
            animal = "Rabbit";
        } else if (zodiacNumber == 8) {
            animal = "Dragon";
        } else if (zodiacNumber == 9) {
            animal = "Snake";
        } else if (zodiacNumber == 10) {
            animal = "Horse";
        } else {
            animal = "Sheep";
        }

        return animal;
    }

    public String astrologicalSign() {
        String sign;
        java.time.Month month = birthDate.getMonth();
        int day = birthDate.getDayOfMonth();


        //setting sign accordingly
        if (month == Month.JANUARY) {
            if (day < 20)
                sign = "Capricorn";
            else
                sign = "Aquarius";
        } else if (month == Month.FEBRUARY) {
            if (day < 19)
                sign = "Aquarius";
            else
                sign = "Pisces";
        } else if (month == Month.MARCH) {
            if (day < 21)
                sign = "Pisces";
            else
                sign = "Aries";
        } else if (month == Month.APRIL) {
            if (day < 20)
                sign = "Aries";
            else
                sign = "Taurus";
        } else if (month == Month.MAY) {
            if (day < 21)
                sign = "Taurus";
            else
                sign = "Gemini";
        } else if (month == Month.JUNE) {
            if (day < 22)
                sign = "Gemini";
            else
                sign = "Cancer";
        } else if (month == Month.JULY) {
            if (day < 23)
                sign = "Cancer";
            else
                sign = "Leo";
        } else if (month == Month.AUGUST) {
            if (day < 23)
                sign = "Leo";
            else
                sign = "Virgo";
        } else if (month == Month.SEPTEMBER) {
            if (day < 23)
                sign = "Virgo";
            else
                sign = "Libra";
        } else if (month == Month.OCTOBER) {
            if (day < 23)
                sign = "Libra";
            else
                sign = "Scorpio";
        } else if (month == Month.NOVEMBER) {
            if (day < 23)
                sign = "scorpio";
            else
                sign = "Sagittarius";
        } else {
            if (day < 22)
                sign = "Sagittarius";
            else
                sign = "Capricorn";
        }


        return sign;
    }

    public static void main(String[] args) {

    }
}
