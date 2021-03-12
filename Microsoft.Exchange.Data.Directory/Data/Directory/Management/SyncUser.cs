using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000767 RID: 1895
	[ProvisioningObjectTag("SyncUser")]
	[Serializable]
	public class SyncUser : User
	{
		// Token: 0x17002096 RID: 8342
		// (get) Token: 0x06005D19 RID: 23833 RVA: 0x00141ECB File Offset: 0x001400CB
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SyncUser.schema;
			}
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x00141ED2 File Offset: 0x001400D2
		public SyncUser()
		{
			base.SetObjectClass("user");
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x00141EE5 File Offset: 0x001400E5
		public SyncUser(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x00141EEE File Offset: 0x001400EE
		internal new static SyncUser FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new SyncUser(dataObject);
		}

		// Token: 0x17002097 RID: 8343
		// (get) Token: 0x06005D1D RID: 23837 RVA: 0x00141EFB File Offset: 0x001400FB
		// (set) Token: 0x06005D1E RID: 23838 RVA: 0x00141F0D File Offset: 0x0014010D
		[Parameter(Mandatory = false)]
		public string OnPremisesObjectId
		{
			get
			{
				return (string)this[SyncUserSchema.OnPremisesObjectId];
			}
			set
			{
				this[SyncUserSchema.OnPremisesObjectId] = value;
			}
		}

		// Token: 0x17002098 RID: 8344
		// (get) Token: 0x06005D1F RID: 23839 RVA: 0x00141F1B File Offset: 0x0014011B
		// (set) Token: 0x06005D20 RID: 23840 RVA: 0x00141F2D File Offset: 0x0014012D
		[Parameter(Mandatory = false)]
		public bool IsDirSynced
		{
			get
			{
				return (bool)this[SyncUserSchema.IsDirSynced];
			}
			set
			{
				this[SyncUserSchema.IsDirSynced] = value;
			}
		}

		// Token: 0x17002099 RID: 8345
		// (get) Token: 0x06005D21 RID: 23841 RVA: 0x00141F40 File Offset: 0x00140140
		// (set) Token: 0x06005D22 RID: 23842 RVA: 0x00141F52 File Offset: 0x00140152
		[Parameter(Mandatory = false)]
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)this[SyncUserSchema.ReleaseTrack];
			}
			set
			{
				this[SyncUserSchema.ReleaseTrack] = value;
			}
		}

		// Token: 0x1700209A RID: 8346
		// (get) Token: 0x06005D23 RID: 23843 RVA: 0x00141F65 File Offset: 0x00140165
		// (set) Token: 0x06005D24 RID: 23844 RVA: 0x00141F77 File Offset: 0x00140177
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncUserSchema.DirSyncAuthorityMetadata];
			}
			set
			{
				this[SyncUserSchema.DirSyncAuthorityMetadata] = value;
			}
		}

		// Token: 0x1700209B RID: 8347
		// (get) Token: 0x06005D25 RID: 23845 RVA: 0x00141F85 File Offset: 0x00140185
		// (set) Token: 0x06005D26 RID: 23846 RVA: 0x00141F97 File Offset: 0x00140197
		[Parameter(Mandatory = false)]
		public CountryInfo UsageLocation
		{
			get
			{
				return (CountryInfo)this[SyncUserSchema.UsageLocation];
			}
			set
			{
				this[SyncUserSchema.UsageLocation] = value;
			}
		}

		// Token: 0x1700209C RID: 8348
		// (get) Token: 0x06005D27 RID: 23847 RVA: 0x00141FA5 File Offset: 0x001401A5
		// (set) Token: 0x06005D28 RID: 23848 RVA: 0x00141FB7 File Offset: 0x001401B7
		[Parameter(Mandatory = false)]
		public RemoteRecipientType RemoteRecipientType
		{
			get
			{
				return (RemoteRecipientType)this[SyncUserSchema.RemoteRecipientType];
			}
			set
			{
				this[SyncUserSchema.RemoteRecipientType] = value;
			}
		}

		// Token: 0x1700209D RID: 8349
		// (get) Token: 0x06005D29 RID: 23849 RVA: 0x00141FCA File Offset: 0x001401CA
		// (set) Token: 0x06005D2A RID: 23850 RVA: 0x00141FDC File Offset: 0x001401DC
		[Parameter(Mandatory = false)]
		public bool AccountDisabled
		{
			get
			{
				return (bool)this[SyncUserSchema.AccountDisabled];
			}
			set
			{
				this[SyncUserSchema.AccountDisabled] = value;
			}
		}

		// Token: 0x1700209E RID: 8350
		// (get) Token: 0x06005D2B RID: 23851 RVA: 0x00141FEF File Offset: 0x001401EF
		// (set) Token: 0x06005D2C RID: 23852 RVA: 0x00142001 File Offset: 0x00140201
		[Parameter(Mandatory = false)]
		public DateTime? StsRefreshTokensValidFrom
		{
			get
			{
				return (DateTime?)this[SyncUserSchema.StsRefreshTokensValidFrom];
			}
			set
			{
				this[SyncUserSchema.StsRefreshTokensValidFrom] = value;
			}
		}

		// Token: 0x04003F03 RID: 16131
		private static SyncUserSchema schema = ObjectSchema.GetInstance<SyncUserSchema>();
	}
}
