using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000043 RID: 67
	public interface IOperationExecutionTrackingKey
	{
		// Token: 0x0600048F RID: 1167
		int GetTrackingKeyHashValue();

		// Token: 0x06000490 RID: 1168
		int GetSimpleHashValue();

		// Token: 0x06000491 RID: 1169
		bool IsTrackingKeyEqualTo(IOperationExecutionTrackingKey other);

		// Token: 0x06000492 RID: 1170
		string TrackingKeyToString();
	}
}
