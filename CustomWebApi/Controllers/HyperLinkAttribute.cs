using System;

namespace CustomWebApi.Controllers
{
    internal class HyperLinkAttribute : Attribute
    {
        public HyperLinkAttribute(string msg)
        {
        }
    }
}