using Tracer.TestClasses;
using Tracer.Tracer;

class Program
{
    static private TracerClass _tracer;
    static private TracerClass _tracer1;
    static private TracerClass _tracer2;

    static void Main()
    {
        _tracer1 = new TracerClass(Thread.CurrentThread.ManagedThreadId);

        Thread thread1 = new Thread(Thread1);
        Thread thread2 = new Thread(Thread2);

        thread1.Start();
        thread2.Start();

        Foo foo = new Foo(_tracer1);
        foo.MyMethod();

        thread1.Join();
        thread2.Join();

        _tracer2.GetTraceResult();
        _tracer.GetTraceResult();
        _tracer1.GetTraceResult();

        _tracer.GetMultiThreadResult("..//..//..//outputJSON.txt", "..//..//..//outputXML.txt");
    }

    static public void Thread1()
    {
        _tracer = new TracerClass(Thread.CurrentThread.ManagedThreadId);
        Foo foo = new Foo(_tracer);

        foo.MyMethod();
        foo.MySecondMethod();
    }
    static public void Thread2()
    {
        _tracer2 = new TracerClass(Thread.CurrentThread.ManagedThreadId);
        Foo foo = new Foo(_tracer2);
        Bar bar = new Bar(_tracer2);

        foo.MyMethod();
        bar.InnerMethod();
    }

}
