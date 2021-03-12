using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000833 RID: 2099
	internal interface IGenericSyncPropertyFactory
	{
		// Token: 0x06006823 RID: 26659
		object Create(object value, bool multiValued);

		// Token: 0x06006824 RID: 26660
		object GetDefault(bool multiValued);
	}
}
