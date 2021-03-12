using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200082F RID: 2095
	internal interface ISyncProperty
	{
		// Token: 0x170024CD RID: 9421
		// (get) Token: 0x06006802 RID: 26626
		bool HasValue { get; }

		// Token: 0x06006803 RID: 26627
		object GetValue();
	}
}
