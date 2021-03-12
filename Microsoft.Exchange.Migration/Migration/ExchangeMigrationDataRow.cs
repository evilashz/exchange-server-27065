using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200009B RID: 155
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangeMigrationDataRow : IMigrationDataRow
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x000265CB File Offset: 0x000247CB
		public ExchangeMigrationDataRow(int rowIndex, string identifier, MigrationUserRecipientType recipientType = MigrationUserRecipientType.Mailbox)
		{
			this.CursorPosition = rowIndex;
			this.RemoteIdentifier = identifier;
			this.RecipientType = recipientType;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x000265E8 File Offset: 0x000247E8
		public MigrationType MigrationType
		{
			get
			{
				return MigrationType.ExchangeOutlookAnywhere;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x000265EB File Offset: 0x000247EB
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x000265F3 File Offset: 0x000247F3
		public virtual MigrationUserRecipientType RecipientType { get; private set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x000265FC File Offset: 0x000247FC
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00026604 File Offset: 0x00024804
		public int CursorPosition { get; internal set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0002660D File Offset: 0x0002480D
		public string Identifier
		{
			get
			{
				if (this.TargetIdentifier != null)
				{
					return this.TargetIdentifier;
				}
				return this.RemoteIdentifier;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00026624 File Offset: 0x00024824
		public string LocalMailboxIdentifier
		{
			get
			{
				return this.Identifier;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002662C File Offset: 0x0002482C
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00026634 File Offset: 0x00024834
		public string RemoteIdentifier { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0002663D File Offset: 0x0002483D
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00026645 File Offset: 0x00024845
		public string TargetIdentifier { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0002664E File Offset: 0x0002484E
		public bool SupportsRemoteIdentifier
		{
			get
			{
				return this.TargetIdentifier != null;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0002665C File Offset: 0x0002485C
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x00026664 File Offset: 0x00024864
		public string EncryptedPassword { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0002666D File Offset: 0x0002486D
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x00026675 File Offset: 0x00024875
		public bool ForceChangePassword { get; set; }
	}
}
