using System;

public class DateCalendar
{

     private int CurrentDisplayMonth;
     private int CurrentDisplayYear;
     private int weekDay;
     private int selectedDay;
     private int NumberOfDays;
	public DateCalendar()
	{
        currentDisplayMonth = DateTime.Now.Month;
        currentDisplayYear = DateTime.Now.Year;
        selectedDay = DateTime.Now.Day;
        weekday = DateTime.Now.DayOfWeek();
        NumberOfDays = GetDays(currentDisplayMonth, currentDisplayYear);
	}
    public int CurrentDisplayMonth   // property
    {
        get { return currentDisplayMonth; }   // get method
        set { currentDisplayMonth = value; }  // set method
    }
    public int CurrentDisplayYear   // property
    {
        get { return currentDisplayYear; }   // get method
        set { currentDisplayYear = value; }  // set method
    }
    public int SelectedDay   // property
    {
        get { return selectedDay; }   // get method
        set { selectedDay = value; }  // set method
    }
    public int Weekday// property
    {
        get { return weekday; }   // get method
        set { weekday = value; }  // set method
    }
    public int NumberOfDays// property
    {
        get { return NumberOfDays; }   // get method
        set { NumberOfDays = value; }  // set method
    }
    public int GetDays(int month, int Year)
    {
        int NumberOfDays = 0;
        switch (month)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:

                NumberOfDays = 31;
                break;
            case 4:
            case 6:
            case 9:
            case 11:
                NumberOfDays = 30;
                break;
            case 2:
                if (((Year % 4 == 0) &&
                     !(Year % 100 == 0))
                     || (Year % 400 == 0))
                    NumberOfDays = 29;
                else
                    NumberOfDays = 28;
                break;
        }
        return NumberOfDays;
    }
}
