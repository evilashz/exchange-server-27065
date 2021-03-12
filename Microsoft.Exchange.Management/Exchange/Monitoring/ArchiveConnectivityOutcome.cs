using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000502 RID: 1282
	[Serializable]
	public class ArchiveConnectivityOutcome : ConfigurableObject
	{
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x000B7AF5 File Offset: 0x000B5CF5
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ArchiveConnectivityOutcome.schema;
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000B7AFC File Offset: 0x000B5CFC
		private new bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000B7B00 File Offset: 0x000B5D00
		public ArchiveConnectivityOutcome(string userSmtp, string primaryFAI, string primaryLastProcessedTime, string archiveDomain, string archiveDatabase, string archiveFAI, string archiveLastProcessedTime, string complianceConfiguration, string mrmProperties) : base(new SimpleProviderPropertyBag())
		{
			this.Identity = userSmtp;
			this.PrimaryMRMConfiguration = primaryFAI;
			this.PrimaryLastProcessedTime = primaryLastProcessedTime;
			this.ArchiveDomain = archiveDomain;
			this.ArchiveDatabase = archiveDatabase;
			this.ArchiveMRMConfiguration = archiveFAI;
			this.ArchiveLastProcessedTime = archiveLastProcessedTime;
			this.ComplianceConfiguration = complianceConfiguration;
			this.ItemMRMProperties = mrmProperties;
			this.Result = new ArchiveConnectivityResult(ArchiveConnectivityResultEnum.Undefined);
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000B7B74 File Offset: 0x000B5D74
		internal void Update(ArchiveConnectivityResultEnum resultEnum, string error)
		{
			lock (this.thisLock)
			{
				this.Result = new ArchiveConnectivityResult(resultEnum);
				this.Error = (error ?? string.Empty);
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x000B7BCC File Offset: 0x000B5DCC
		// (set) Token: 0x06002DF5 RID: 11765 RVA: 0x000B7BDE File Offset: 0x000B5DDE
		public new string Identity
		{
			get
			{
				return (string)this[ArchiveConnectivityResultOutcomeSchema.UserSmtp];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.UserSmtp] = value;
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x000B7BEC File Offset: 0x000B5DEC
		// (set) Token: 0x06002DF7 RID: 11767 RVA: 0x000B7BFE File Offset: 0x000B5DFE
		public string PrimaryMRMConfiguration
		{
			get
			{
				return (string)this[ArchiveConnectivityResultOutcomeSchema.PrimaryMRMConfiguration];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.PrimaryMRMConfiguration] = value;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x000B7C0C File Offset: 0x000B5E0C
		// (set) Token: 0x06002DF9 RID: 11769 RVA: 0x000B7C1E File Offset: 0x000B5E1E
		public string PrimaryLastProcessedTime
		{
			get
			{
				return (string)this[ArchiveConnectivityResultOutcomeSchema.PrimaryLastProcessedTime];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.PrimaryLastProcessedTime] = value;
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000B7C2C File Offset: 0x000B5E2C
		// (set) Token: 0x06002DFB RID: 11771 RVA: 0x000B7C3E File Offset: 0x000B5E3E
		public string ArchiveDomain
		{
			get
			{
				return (string)this[ArchiveConnectivityResultOutcomeSchema.ArchiveDomain];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.ArchiveDomain] = value;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x000B7C4C File Offset: 0x000B5E4C
		// (set) Token: 0x06002DFD RID: 11773 RVA: 0x000B7C5E File Offset: 0x000B5E5E
		public string ArchiveDatabase
		{
			get
			{
				return (string)this[ArchiveConnectivityResultOutcomeSchema.ArchiveDatabase];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.ArchiveDatabase] = value;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x000B7C6C File Offset: 0x000B5E6C
		// (set) Token: 0x06002DFF RID: 11775 RVA: 0x000B7C7E File Offset: 0x000B5E7E
		public string ArchiveMRMConfiguration
		{
			get
			{
				return (string)this[ArchiveConnectivityResultOutcomeSchema.ArchiveMRMConfiguration];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.ArchiveMRMConfiguration] = value;
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x000B7C8C File Offset: 0x000B5E8C
		// (set) Token: 0x06002E01 RID: 11777 RVA: 0x000B7C9E File Offset: 0x000B5E9E
		public string ArchiveLastProcessedTime
		{
			get
			{
				return (string)this[ArchiveConnectivityResultOutcomeSchema.ArchiveLastProcessedTime];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.ArchiveLastProcessedTime] = value;
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000B7CAC File Offset: 0x000B5EAC
		// (set) Token: 0x06002E03 RID: 11779 RVA: 0x000B7CBE File Offset: 0x000B5EBE
		public string ComplianceConfiguration
		{
			get
			{
				return (string)this[ArchiveConnectivityResultOutcomeSchema.ComplianceConfiguration];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.ComplianceConfiguration] = value;
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000B7CCC File Offset: 0x000B5ECC
		// (set) Token: 0x06002E05 RID: 11781 RVA: 0x000B7CDE File Offset: 0x000B5EDE
		public string ItemMRMProperties
		{
			get
			{
				return (string)this[ArchiveConnectivityResultOutcomeSchema.ItemMRMProperties];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.ItemMRMProperties] = value;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x000B7CEC File Offset: 0x000B5EEC
		// (set) Token: 0x06002E07 RID: 11783 RVA: 0x000B7CFE File Offset: 0x000B5EFE
		public ArchiveConnectivityResult Result
		{
			get
			{
				return (ArchiveConnectivityResult)this[ArchiveConnectivityResultOutcomeSchema.Result];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.Result] = value;
			}
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x000B7D0C File Offset: 0x000B5F0C
		// (set) Token: 0x06002E09 RID: 11785 RVA: 0x000B7D1E File Offset: 0x000B5F1E
		public string Error
		{
			get
			{
				return (string)this[ArchiveConnectivityResultOutcomeSchema.Error];
			}
			internal set
			{
				this[ArchiveConnectivityResultOutcomeSchema.Error] = value;
			}
		}

		// Token: 0x040020EF RID: 8431
		[NonSerialized]
		private object thisLock = new object();

		// Token: 0x040020F0 RID: 8432
		private static ArchiveConnectivityResultOutcomeSchema schema = ObjectSchema.GetInstance<ArchiveConnectivityResultOutcomeSchema>();
	}
}
