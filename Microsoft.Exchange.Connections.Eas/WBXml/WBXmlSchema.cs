using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.WBXml
{
	// Token: 0x02000080 RID: 128
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class WBXmlSchema
	{
		// Token: 0x06000272 RID: 626 RVA: 0x00008C14 File Offset: 0x00006E14
		protected WBXmlSchema(int airSyncVersion)
		{
			this.Version = airSyncVersion;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00008C23 File Offset: 0x00006E23
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00008C2B File Offset: 0x00006E2B
		internal int Version { get; set; }

		// Token: 0x06000275 RID: 629
		internal abstract string GetName(int tag);

		// Token: 0x06000276 RID: 630
		internal abstract string GetNameSpace(int tag);

		// Token: 0x06000277 RID: 631
		internal abstract int GetTag(string nameSpace, string name);

		// Token: 0x06000278 RID: 632
		internal abstract bool IsTagSecure(int tag);

		// Token: 0x06000279 RID: 633
		internal abstract bool IsTagAnOpaqueBlob(int tag);
	}
}
