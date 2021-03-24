using System;

namespace RazorDockerTest1
{
    internal class PathString
    {
        private string v;

        public PathString(string v)
        {
            this.v = v;
        }

        public static implicit operator Microsoft.AspNetCore.Http.PathString(PathString v)
        {
            throw new NotImplementedException();
        }
    }
}