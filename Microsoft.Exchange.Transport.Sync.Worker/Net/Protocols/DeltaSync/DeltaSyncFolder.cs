using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x0200006A RID: 106
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncFolder : DeltaSyncObject
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x00017323 File Offset: 0x00015523
		internal DeltaSyncFolder(Guid serverId) : base(serverId)
		{
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001732C File Offset: 0x0001552C
		internal DeltaSyncFolder(string clientId) : base(clientId)
		{
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00017335 File Offset: 0x00015535
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0001733D File Offset: 0x0001553D
		internal string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00017346 File Offset: 0x00015546
		internal static bool IsSystemFolder(string displayName)
		{
			return !string.IsNullOrEmpty(displayName) && displayName.StartsWith(".!!", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0400029C RID: 668
		internal const string RootServerId = "00000000-0000-0000-0000-000000000000";

		// Token: 0x0400029D RID: 669
		internal const string InboxServerId = "00000000-0000-0000-0000-000000000001";

		// Token: 0x0400029E RID: 670
		internal const string DeletedItemsServerId = "00000000-0000-0000-0000-000000000002";

		// Token: 0x0400029F RID: 671
		internal const string SentItemsServerId = "00000000-0000-0000-0000-000000000003";

		// Token: 0x040002A0 RID: 672
		internal const string DraftsServerId = "00000000-0000-0000-0000-000000000004";

		// Token: 0x040002A1 RID: 673
		internal const string JunkEmailServerId = "00000000-0000-0000-0000-000000000005";

		// Token: 0x040002A2 RID: 674
		private const string SystemFolderDisplayNamePrefix = ".!!";

		// Token: 0x040002A3 RID: 675
		internal static readonly DeltaSyncFolder DefaultRootFolder = new DeltaSyncFolder(new Guid("00000000-0000-0000-0000-000000000000"));

		// Token: 0x040002A4 RID: 676
		private string displayName;
	}
}
