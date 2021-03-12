using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001C1 RID: 449
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IReadableUserConfiguration : IDisposable
	{
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001837 RID: 6199
		string ConfigurationName { get; }

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001838 RID: 6200
		UserConfigurationTypes DataTypes { get; }

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001839 RID: 6201
		StoreObjectId FolderId { get; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600183A RID: 6202
		StoreObjectId Id { get; }

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600183B RID: 6203
		ExDateTime LastModifiedTime { get; }

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600183C RID: 6204
		VersionedId VersionedId { get; }

		// Token: 0x0600183D RID: 6205
		IDictionary GetDictionary();

		// Token: 0x0600183E RID: 6206
		Stream GetXmlStream();
	}
}
