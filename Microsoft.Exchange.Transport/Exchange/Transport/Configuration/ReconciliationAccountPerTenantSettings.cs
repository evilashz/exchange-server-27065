using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002E7 RID: 743
	internal sealed class ReconciliationAccountPerTenantSettings : TenantConfigurationCacheableItem<JournalingReconciliationAccount>
	{
		// Token: 0x06002110 RID: 8464 RVA: 0x0007D525 File Offset: 0x0007B725
		public ReconciliationAccountPerTenantSettings()
		{
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0007D52D File Offset: 0x0007B72D
		public ReconciliationAccountPerTenantSettings(List<JournalingReconciliationAccount> reconciliationAccounts) : base(true)
		{
			this.SetInternalData(reconciliationAccounts.ToArray());
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0007D544 File Offset: 0x0007B744
		public override void ReadData(IConfigurationSession session)
		{
			JournalingReconciliationAccount[] internalData = (JournalingReconciliationAccount[])session.Find<JournalingReconciliationAccount>(null, null, true, null);
			this.SetInternalData(internalData);
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x0007D568 File Offset: 0x0007B768
		public ReconciliationAccountPerTenantSettings.ReconciliationAccountData[] AccountDataArray
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.accountDataArray;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x0007D578 File Offset: 0x0007B778
		public override long ItemSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				if (this.accountDataArray == null)
				{
					return 0L;
				}
				long num = 0L;
				foreach (ReconciliationAccountPerTenantSettings.ReconciliationAccountData reconciliationAccountData in this.accountDataArray)
				{
					num += reconciliationAccountData.ItemSize;
				}
				return num + 18L;
			}
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0007D5C4 File Offset: 0x0007B7C4
		private void SetInternalData(ICollection<JournalingReconciliationAccount> reconciliationAccounts)
		{
			if (reconciliationAccounts == null)
			{
				this.accountDataArray = null;
				return;
			}
			this.accountDataArray = new ReconciliationAccountPerTenantSettings.ReconciliationAccountData[reconciliationAccounts.Count];
			int num = 0;
			foreach (JournalingReconciliationAccount adObject in reconciliationAccounts)
			{
				this.accountDataArray[num++] = new ReconciliationAccountPerTenantSettings.ReconciliationAccountData(adObject);
			}
		}

		// Token: 0x0400114C RID: 4428
		private ReconciliationAccountPerTenantSettings.ReconciliationAccountData[] accountDataArray;

		// Token: 0x020002E8 RID: 744
		public sealed class ReconciliationAccountData
		{
			// Token: 0x06002116 RID: 8470 RVA: 0x0007D638 File Offset: 0x0007B838
			public ReconciliationAccountData(JournalingReconciliationAccount adObject)
			{
				if (adObject.ArchiveUri != null)
				{
					this.archiveUri = adObject.ArchiveUri.ToString();
				}
				if (adObject.Mailboxes != null)
				{
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(adObject.OrganizationId), 86, ".ctor", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Configuration\\ReconciliationAccountPerTenantSettings.cs");
					this.mailboxes = new string[adObject.Mailboxes.Count];
					for (int i = 0; i < adObject.Mailboxes.Count; i++)
					{
						ADRecipient adrecipient = tenantOrRootOrgRecipientSession.Read(adObject.Mailboxes[i]);
						SmtpAddress primarySmtpAddress = adrecipient.PrimarySmtpAddress;
						this.mailboxes[i] = adrecipient.PrimarySmtpAddress.ToString();
					}
				}
				this.objectGuid = adObject.Guid;
			}

			// Token: 0x17000A84 RID: 2692
			// (get) Token: 0x06002117 RID: 8471 RVA: 0x0007D706 File Offset: 0x0007B906
			public Uri ArchiveUri
			{
				get
				{
					return new Uri(this.archiveUri);
				}
			}

			// Token: 0x17000A85 RID: 2693
			// (get) Token: 0x06002118 RID: 8472 RVA: 0x0007D713 File Offset: 0x0007B913
			public string[] Mailboxes
			{
				get
				{
					return this.mailboxes;
				}
			}

			// Token: 0x17000A86 RID: 2694
			// (get) Token: 0x06002119 RID: 8473 RVA: 0x0007D71B File Offset: 0x0007B91B
			public Guid Guid
			{
				get
				{
					return this.objectGuid;
				}
			}

			// Token: 0x17000A87 RID: 2695
			// (get) Token: 0x0600211A RID: 8474 RVA: 0x0007D724 File Offset: 0x0007B924
			public long ItemSize
			{
				get
				{
					int num = ReconciliationAccountPerTenantSettings.ReconciliationAccountData.GetStringLength(this.archiveUri);
					num += 18;
					foreach (string str in this.mailboxes)
					{
						num += ReconciliationAccountPerTenantSettings.ReconciliationAccountData.GetStringLength(str);
					}
					num += 18;
					num += 16;
					return (long)num;
				}
			}

			// Token: 0x0600211B RID: 8475 RVA: 0x0007D770 File Offset: 0x0007B970
			private static int GetStringLength(string str)
			{
				return str.Length * 2 + 18;
			}

			// Token: 0x0400114D RID: 4429
			private const int FixedObjectOverhead = 18;

			// Token: 0x0400114E RID: 4430
			private string archiveUri;

			// Token: 0x0400114F RID: 4431
			private string[] mailboxes;

			// Token: 0x04001150 RID: 4432
			private Guid objectGuid;
		}
	}
}
