using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000A1 RID: 161
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AmBcsContext
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x0001F4FF File Offset: 0x0001D6FF
		public AmBcsContext(Guid dbGuid, AmServerName sourceServerName, IAmBcsErrorLogger errorLogger)
		{
			this.DatabaseGuid = dbGuid;
			this.SourceServerName = sourceServerName;
			this.IsSourceServerAllowedForMount = false;
			this.ErrorLogger = errorLogger;
			this.StatusTable = new Dictionary<AmServerName, RpcDatabaseCopyStatus2>();
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001F52E File Offset: 0x0001D72E
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0001F536 File Offset: 0x0001D736
		public Guid DatabaseGuid { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001F53F File Offset: 0x0001D73F
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001F547 File Offset: 0x0001D747
		public IADDatabase Database
		{
			get
			{
				return this.m_database;
			}
			set
			{
				this.m_database = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001F550 File Offset: 0x0001D750
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001F558 File Offset: 0x0001D758
		public bool DatabaseNeverMounted { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001F561 File Offset: 0x0001D761
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0001F569 File Offset: 0x0001D769
		public bool SortCopiesByActivationPreference { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001F572 File Offset: 0x0001D772
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0001F57A File Offset: 0x0001D77A
		public AmBcsSkipFlags SkipValidationChecks { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001F583 File Offset: 0x0001D783
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x0001F58B File Offset: 0x0001D78B
		public bool IsSourceServerAllowedForMount { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001F594 File Offset: 0x0001D794
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0001F59C File Offset: 0x0001D79C
		public AmDbActionCode ActionCode { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001F5A5 File Offset: 0x0001D7A5
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0001F5AD File Offset: 0x0001D7AD
		public AmServerName SourceServerName { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001F5B6 File Offset: 0x0001D7B6
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0001F5BE File Offset: 0x0001D7BE
		public Dictionary<AmServerName, RpcDatabaseCopyStatus2> StatusTable { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001F5C7 File Offset: 0x0001D7C7
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x0001F5CF File Offset: 0x0001D7CF
		public ComponentStateWrapper ComponentStateWrapper { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0001F5E0 File Offset: 0x0001D7E0
		public IAmBcsErrorLogger ErrorLogger { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001F5E9 File Offset: 0x0001D7E9
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x0001F5F1 File Offset: 0x0001D7F1
		public string InitiatingComponent { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001F5FA File Offset: 0x0001D7FA
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0001F602 File Offset: 0x0001D802
		public AmDbAction.PrepareSubactionArgsDelegate PrepareSubaction { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001F60B File Offset: 0x0001D80B
		public bool ShouldLogSubactionEvent
		{
			get
			{
				return this.PrepareSubaction != null;
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001F61C File Offset: 0x0001D81C
		public string GetDatabaseNameOrGuid()
		{
			if (this.Database == null)
			{
				return this.DatabaseGuid.ToString();
			}
			return this.Database.Name;
		}

		// Token: 0x040002CC RID: 716
		private IADDatabase m_database;
	}
}
