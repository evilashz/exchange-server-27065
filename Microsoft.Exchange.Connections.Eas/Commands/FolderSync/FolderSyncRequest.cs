using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.FolderHierarchy;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderSync
{
	// Token: 0x0200003F RID: 63
	[XmlRoot(ElementName = "FolderSync", Namespace = "FolderHierarchy", IsNullable = false)]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class FolderSyncRequest : FolderSync
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004980 File Offset: 0x00002B80
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00004987 File Offset: 0x00002B87
		[XmlIgnore]
		internal static FolderSyncRequest InitialSyncRequest { get; set; } = new FolderSyncRequest
		{
			SyncKey = "0"
		};

		// Token: 0x04000130 RID: 304
		internal const string PrimingFolderSyncKey = "0";
	}
}
