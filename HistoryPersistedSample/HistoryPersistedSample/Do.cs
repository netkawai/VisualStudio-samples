using System;
using System.Collections.Generic;
using System.Text;

namespace HistoryPersistedSample
{
    public static class Do
    {
#if false

        public static void Test<T>(
            T value,
            Function<bool, T> testFn,
            Procedure<T> ifTrueFn,
            Procedure<T> ifFalseFn)
        {
            (testFn(value) ? ifTrueFn : ifFalseFn)(value);
        }

        public static void GenerateTest<T>(
            Function<T> generate,
            Function<bool, T> test,
            Procedure<T> ifTrue,
            Procedure<T> ifFalse)
        {
            Test(generate(), test, ifTrue, ifFalse);
        }

        public static bool TryBool(Procedure actionProcedure)
        {
            try
            {
                actionProcedure();
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

#endif
    }
}
