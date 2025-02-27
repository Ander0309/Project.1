namespace POOproces;

public class Time
{
    private int _hours;
    private int _minutes;
    private int _seconds;
    private int _milliseconds;

    public int Hours
    {
        get => _hours;
        set
        {
            ValidateHour(value);
            _hours = value;
        }
    }

    public int Minutes
    {
        get => _minutes;
        set
        {
            ValidateMinute(value);
            _minutes = value;
        }
    }

    public int Seconds
    {
        get => _seconds;
        set
        {
            ValidateSecond(value);
            _seconds = value;
        }
    }

    public int Milliseconds
    {
        get => _milliseconds;
        set
        {
            ValidateMillisecond(value);
            _milliseconds = value;
        }
    }

    public Time() : this(0, 0, 0, 0) { }
    public Time(int h) : this(h, 0, 0, 0) { }
    public Time(int h, int m) : this(h, m, 0, 0) { }
    public Time(int h, int m, int s) : this(h, m, s, 0) { }
    public Time(int h, int m, int s, int ms)
    {
        ValidateHour(h);
        ValidateMinute(m);
        ValidateSecond(s);
        ValidateMillisecond(ms);
        _hours = h;
        _minutes = m;
        _seconds = s;
        _milliseconds = ms;
    }

    private void ValidateHour(int h)
    {
        if (h < 0 || h > 23) throw new ArgumentException($"The hour: {h}, is not valid.");
    }

    private void ValidateMinute(int m)
    {
        if (m < 0 || m > 59) throw new ArgumentException($"The minutes: {m}, is not valid.");
    }

    private void ValidateSecond(int s)
    {
        if (s < 0 || s > 59) throw new ArgumentException($"The seconds: {s}, is not valid.");
    }

    private void ValidateMillisecond(int ms)
    {
        if (ms < 0 || ms > 999) throw new ArgumentException($"The milliseconds: {ms}, is not valid.");
    }

    public long ToMilliseconds() => (_hours * 3600000L) + (_minutes * 60000L) + (_seconds * 1000L) + _milliseconds;
    public long ToSeconds() => (_hours * 3600L) + (_minutes * 60L) + _seconds;
    public long ToMinutes() => (_hours * 60L) + _minutes;

    public Time Add(Time other)
    {
        int totalMilliseconds = this._milliseconds + other._milliseconds;

        int newHours = (totalMilliseconds / (1000 * 3600)) % 24;
        totalMilliseconds %= (1000 * 3600);
        int newMinutes = totalMilliseconds / (1000 * 60);
        totalMilliseconds %= (1000 * 60);
        int newSeconds = totalMilliseconds / 1000;
        int newMilliseconds = totalMilliseconds % 1000;

        return new Time(newHours, newMinutes, newSeconds, newMilliseconds);
    }

    public bool IsOtherDay(Time other)
    {
        return (this.ToMilliseconds() + other.ToMilliseconds()) >= 86400000;
    }

    public override string ToString()
    {
        int displayHour = (_hours == 0 || _hours == 12) ? 12 : _hours % 12;
        string period = _hours < 12 ? "AM" : "PM";
        return $"{displayHour:00}:{_minutes:00}:{_seconds:00}.{_milliseconds:000} {period}";
    }
}

