using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Contract
{
	// Token: 0x0200000F RID: 15
	[ServiceContract(Namespace = "http://schemas.microsoft.com/informationprotection/computefabric")]
	public interface IMessageReceiver
	{
		// Token: 0x06000022 RID: 34
		[OperationContract]
		Task<byte[][]> ReceiveMessagesAsync(byte[][] messageBlobs);
	}
}
