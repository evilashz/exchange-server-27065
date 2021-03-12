using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E00 RID: 3584
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncState
	{
		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x06007B3D RID: 31549
		int? BackendVersion { get; }

		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x06007B3E RID: 31550
		// (set) Token: 0x06007B3F RID: 31551
		int Version { get; set; }

		// Token: 0x17002104 RID: 8452
		ICustomSerializableBuilder this[string key]
		{
			get;
			set;
		}

		// Token: 0x06007B42 RID: 31554
		bool Contains(string key);

		// Token: 0x06007B43 RID: 31555
		void Remove(string key);
	}
}
