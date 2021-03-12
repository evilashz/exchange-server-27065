using System;
using Microsoft.Exchange.Rpc.MailSubmission;

namespace Microsoft.Exchange.Transport.MessageResubmission
{
	// Token: 0x02000133 RID: 307
	internal interface IMessageResubmissionRpcServer
	{
		// Token: 0x06000D86 RID: 3462
		byte[] GetResubmitRequest(byte[] rawRequest);

		// Token: 0x06000D87 RID: 3463
		byte[] SetResubmitRequest(byte[] requestRaw);

		// Token: 0x06000D88 RID: 3464
		byte[] RemoveResubmitRequest(byte[] requestRaw);

		// Token: 0x06000D89 RID: 3465
		AddResubmitRequestStatus AddMdbResubmitRequest(Guid requestGuid, Guid mdbGuid, long startTimeInTicks, long endTimeInTicks, string[] unresponsivePrimaryServers, byte[] reservedBytes);

		// Token: 0x06000D8A RID: 3466
		AddResubmitRequestStatus AddConditionalResubmitRequest(Guid requestGuid, long startTimeInTicks, long endTimeInTicks, string conditions, string[] unresponsivePrimaryServers, byte[] reservedBytes);
	}
}
