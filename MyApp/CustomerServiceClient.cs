namespace MyApp
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "MyApp.IUserService")]
    public interface ICustomerService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://just-another-fake-service.de/ICustomerService/GetLimit", ReplyAction = "http://just-another-fake-service.de/ICustomerService/GetLimitResponse")]
        int GetLimit(string firstname, string surname, System.DateTime dateOfBirth);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICustomerServiceChannel : ICustomerService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomerServiceClient : System.ServiceModel.ClientBase<ICustomerService>, ICustomerService
    {

        public CustomerServiceClient()
        {
        }

        public CustomerServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public CustomerServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public CustomerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public CustomerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public int GetLimit(string firstname, string surname, System.DateTime dateOfBirth)
        {
            return base.Channel.GetLimit(firstname, surname, dateOfBirth);
        }
    }
}
