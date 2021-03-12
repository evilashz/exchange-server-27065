using System;

namespace Microsoft.Exchange.Data.Storage.Compliance.DAR
{
	// Token: 0x02000455 RID: 1109
	public interface IStoreObject
	{
		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06003144 RID: 12612
		// (set) Token: 0x06003145 RID: 12613
		string Id { get; set; }

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06003146 RID: 12614
		DateTime LastModifiedTime { get; }
	}
}
