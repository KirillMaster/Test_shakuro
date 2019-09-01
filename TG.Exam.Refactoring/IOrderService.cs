using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace TG.Exam.Refactoring
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        [FaultContract(typeof(ApplicationFault))]
        Order LoadOrder(string orderId);
    }


    [DataContract]
    public class Order
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int OrderCustomerId { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }
    }

    [DataContract]
    public class ApplicationFault
    {
        [DataMember]
        public string Message { get; set;}

        public ApplicationFault(string message)
        {
            Message = message;
        }
    }
}
