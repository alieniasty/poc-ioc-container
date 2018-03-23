using FluentAssertions;
using Xunit;

namespace poc_ioc_container.Tests
{

    public class InjectorTest
    {
        private readonly Injector _injector;

        public InjectorTest()
        {
            _injector = new Injector();
        }

        [Fact]
        public void Should_ReturnConcreteObject_When_RegisteredWithInstance()
        {
            _injector.Bind(new StubClass());

            var registeredObject = _injector.Resolve(typeof(StubClass));

            registeredObject.Should().BeOfType<StubClass>();
        }

        [Fact]
        public void Should_ReturnConcreteObject_When_RegisteredWithType()
        {
            _injector.Bind<IStubInterface, StubClass>();

            var registeredObject = _injector.Resolve(typeof(StubClass));

            registeredObject.Should().BeOfType<StubClass>();
        }

        [Fact]
        public void Should_ReturnConcreteObjectThatReturnsFive_When_RegisteredWithType()
        {
            _injector.Bind<IStubInterface, StubClass>();

            var registeredObject = _injector.Resolve(typeof(StubClass));

            registeredObject.Should().BeOfType<StubClass>();

            var unpackedObject = (StubClass) registeredObject;

            var result = unpackedObject.ReturnFive();

            result.Should().Be(5);
        }

        [Fact]
        public void Should_ReturnConcreteObjectThatReturnsFive_When_RegisteredWithInstance()
        {
            _injector.Bind(new StubClass());

            var registeredObject = _injector.Resolve(typeof(StubClass));

            registeredObject.Should().BeOfType<StubClass>();

            var unpackedObject = (StubClass)registeredObject;

            var result = unpackedObject.ReturnFive();

            result.Should().Be(5);
        }
    }
}
