using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace RazorMvc.Tests
{
    public class StartupTest
    {
        [Fact]
        public void ShouldConvertUrlToHerokuString()
        {
            // Assume
            string url = "postgres://qblpfzhiltsbij:af2587ddf2f7bd6d57cf5278dc48b8aadd149524da0064f7fa9d00efcbd063aa@ec2-99-80-200-225.eu-west-1.compute.amazonaws.com:5432/d121khqjfr225k";

            // Act
            var herokuConnectionString = Startup.ConvertHerokuUrlToHerokuString(url);

            // Assert
            Assert.Equal("Server=ec2-99-80-200-225.eu-west-1.compute.amazonaws.com;Port=5432;Database=d121khqjfr225k;User Id=qblpfzhiltsbij; Password=af2587ddf2f7bd6d57cf5278dc48b8aadd149524da0064f7fa9d00efcbd063aa;Pooling=true;SSL Mode=Require;Trust Server Certificate=True;", herokuConnectionString);
        }


    }
}
