package dmit2015.model;

import org.junit.platform.suite.api.IncludeClassNamePatterns;
import org.junit.platform.suite.api.SelectPackages;
import org.junit.platform.suite.api.Suite;
import org.junit.platform.suite.api.SuiteDisplayName;

@Suite
@SuiteDisplayName("JUnit 5 Platform Suite")
@SelectPackages("dmit2015.model")
@IncludeClassNamePatterns(".*Test")

@org.junit.runners.Suite.SuiteClasses({
        PersonTest.class,
        CanadianPersonalIncomeTax.class
})

public class TestSuite {
}
