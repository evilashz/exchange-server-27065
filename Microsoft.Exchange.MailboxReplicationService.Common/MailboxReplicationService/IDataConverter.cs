using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000228 RID: 552
	internal interface IDataConverter<TNative, TData>
	{
		// Token: 0x06001DD1 RID: 7633
		TNative GetNativeRepresentation(TData data);

		// Token: 0x06001DD2 RID: 7634
		TData GetDataRepresentation(TNative src);
	}
}
