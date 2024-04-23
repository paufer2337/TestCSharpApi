var ann = Obj(new
{
    firstName = "Ann",
    lastName = "Adams"
});

var work = Obj(new
{
    jobTitle = "CEO",
    company = "Spreads Are Us"
});

var hobbies = Obj(new
{
    hobbies = Arr("fishing", "football"),
    activityLevel = "high"
});

var allAboutAnn = Obj(new
{
    ___ = ann,       // a spread
    ___2 = work,     // another spread
    ___3 = hobbies,  // a third spread
    lazy = false,    // another property
    stubborn = true  // another property
});

Log(allAboutAnn);
/* Expected output:
{
    "firstName": "Ann", 
    "lastName": "Adams", 
    "jobTitle": "CEO", 
    "company": "Spreads Are Us", 
    "hobbies": ["fishing", "football"], 
}
*/