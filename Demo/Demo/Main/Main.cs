using System;

namespace Demo
{
#if WINDOWS || XBOX
    static class Program
    {

        public static Game g;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static void Main(string[] args)
        {
            using (g = new Game())
            {
                g.Run();
            }
        }
    }
#endif
}

