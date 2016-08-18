using System;
using System.Diagnostics;
using Contextable;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Contextable
{
    [TestClass]
    public class UnitTestContext
    {
        private readonly Action<Dummy> _action = c => c.Guid = Guid.NewGuid();
        private readonly Action<Dummy, Guid> _set = (c, g) => c.Guid = g;

        [TestMethod]
        public void TestMethodDepth()
        {
            using (new Context<Dummy>(_action))
            {
                using (new Context<Dummy>(_action))
                {
                    Assert.IsTrue(Context<Dummy>.Depth == 0);
                }
            }
        }

        [TestMethod]
        public void TestMethodCount()
        {
            using (new Context<Dummy>(_action))
            {
                using (new Context<Dummy>(_action))
                {
                    Assert.IsTrue(Context<Dummy>.Count == 2);
                }
            }
        }

        [TestMethod]
        public void TestMethodCurrent()
        {
            var a = Guid.NewGuid();
            var b = Guid.NewGuid();
            var c = Guid.NewGuid();
            var d = Guid.NewGuid();

            using (new Context<Dummy>(z => _set(z, a)))
            {
                Assert.IsTrue(Context<Dummy>.Current.Guid == a);

                using (new Context<Dummy>(z => _set(z, b)))
                {
                    Assert.IsTrue(Context<Dummy>.Current.Guid == b);

                    using (new Context<Dummy>(z => _set(z, c)))
                    {
                        Assert.IsTrue(Context<Dummy>.Current.Guid == c);

                        using (new Context<Dummy>(z => _set(z, d)))
                        {
                            Assert.IsTrue(Context<Dummy>.Current.Guid == d);
                        }

                        Assert.IsTrue(Context<Dummy>.Current.Guid == c);
                    }

                    Assert.IsTrue(Context<Dummy>.Current.Guid == b);
                }

                Assert.IsTrue(Context<Dummy>.Current.Guid == a);
            }
        }

        [TestMethod]
        public void TestMethodParent()
        {
            var a = Guid.NewGuid();
            var b = Guid.NewGuid();
            var c = Guid.NewGuid();
            var d = Guid.NewGuid();

            using (new Context<Dummy>(z => _set(z, a)))
            {
                Assert.IsTrue(Context<Dummy>.Parent == null);

                using (new Context<Dummy>(z => _set(z, b)))
                {
                    Debug.Assert(Context<Dummy>.Parent != null, "Context<Dummy>.Parent != null");

                    Assert.IsTrue(Context<Dummy>.Parent.Guid == a);

                    using (new Context<Dummy>(z => _set(z, c)))
                    {
                        Assert.IsTrue(Context<Dummy>.Parent.Guid == b);

                        using (new Context<Dummy>(z => _set(z, d)))
                        {
                            Assert.IsTrue(Context<Dummy>.Parent.Guid == c);
                        }

                        Assert.IsTrue(Context<Dummy>.Parent.Guid == b);
                    }

                    Assert.IsTrue(Context<Dummy>.Parent.Guid == a);
                }

                Assert.IsTrue(Context<Dummy>.Parent == null);
            }
        }
    }
}