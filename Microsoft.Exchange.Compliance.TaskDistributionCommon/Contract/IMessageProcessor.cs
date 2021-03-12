using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Contract
{
	// Token: 0x02000007 RID: 7
	[ServiceContract(Namespace = "http://schemas.microsoft.com/informationprotection/computefabric")]
	public interface IMessageProcessor
	{
		// Token: 0x0600000A RID: 10
		[OperationContract]
		Task<byte[]> ProcessMessageAsync(byte[] message);
	}
}
