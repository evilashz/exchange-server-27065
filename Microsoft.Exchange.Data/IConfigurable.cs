using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200000C RID: 12
	public interface IConfigurable
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000049 RID: 73
		ObjectId Identity { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004A RID: 74
		bool IsValid { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004B RID: 75
		ObjectState ObjectState { get; }

		// Token: 0x0600004C RID: 76
		ValidationError[] Validate();

		// Token: 0x0600004D RID: 77
		void CopyChangesFrom(IConfigurable source);

		// Token: 0x0600004E RID: 78
		void ResetChangeTracking();
	}
}
