using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000076 RID: 118
	[Cmdlet("Undo", "SoftDeletedMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "SoftDeletedMailbox")]
	public sealed class UndoSoftDeletedMailbox : NewMailboxOrSyncMailbox
	{
		// Token: 0x0600088A RID: 2186 RVA: 0x000260A4 File Offset: 0x000242A4
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			ADUser dataObject = (ADUser)result;
			Mailbox result2 = new Mailbox(dataObject);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000260E4 File Offset: 0x000242E4
		public UndoSoftDeletedMailbox()
		{
			base.NumberofCalls = ProvisioningCounters.NumberOfUndoSoftDeletedMailboxCalls;
			base.NumberofSuccessfulCalls = ProvisioningCounters.NumberOfSuccessfulUndoSoftDeletedMailboxCalls;
			base.AverageTimeTaken = ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTime;
			base.AverageBaseTimeTaken = ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTimeBase;
			base.AverageTimeTakenWithCache = ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTimeWithCache;
			base.AverageBaseTimeTakenWithCache = ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTimeBaseWithCache;
			base.AverageTimeTakenWithoutCache = ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTimeWithoutCache;
			base.AverageBaseTimeTakenWithoutCache = ProvisioningCounters.AverageUndoSoftDeletedMailboxResponseTimeBaseWithoutCache;
			base.TotalResponseTime = ProvisioningCounters.TotalUndoSoftDeletedMailboxResponseTime;
			base.CacheActivePercentage = ProvisioningCounters.UndoSoftDeletedMailboxCacheActivePercentage;
			base.CacheActiveBasePercentage = ProvisioningCounters.UndoSoftDeletedMailboxCacheActivePercentageBase;
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00026170 File Offset: 0x00024370
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0002617D File Offset: 0x0002437D
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "SoftDeletedMailbox", ValueFromPipeline = true)]
		public new MailboxIdParameter SoftDeletedObject
		{
			get
			{
				return (MailboxIdParameter)base.SoftDeletedObject;
			}
			set
			{
				base.SoftDeletedObject = value;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x00026186 File Offset: 0x00024386
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x0002618E File Offset: 0x0002438E
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailbox")]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00026197 File Offset: 0x00024397
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0002619F File Offset: 0x0002439F
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailbox")]
		public new WindowsLiveId WindowsLiveID
		{
			get
			{
				return base.WindowsLiveID;
			}
			set
			{
				base.WindowsLiveID = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000261A8 File Offset: 0x000243A8
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x000261B0 File Offset: 0x000243B0
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailbox")]
		public override SecureString Password
		{
			get
			{
				return base.Password;
			}
			set
			{
				base.Password = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x000261B9 File Offset: 0x000243B9
		private new SwitchParameter AccountDisabled
		{
			get
			{
				return base.AccountDisabled;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x000261C1 File Offset: 0x000243C1
		private new MailboxPolicyIdParameter ActiveSyncMailboxPolicy
		{
			get
			{
				return base.ActiveSyncMailboxPolicy;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x000261C9 File Offset: 0x000243C9
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x000261D1 File Offset: 0x000243D1
		private new AddressBookMailboxPolicyIdParameter AddressBookPolicy
		{
			get
			{
				return base.AddressBookPolicy;
			}
			set
			{
				base.AddressBookPolicy = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x000261DA File Offset: 0x000243DA
		private new string Alias
		{
			get
			{
				return base.Alias;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x000261E2 File Offset: 0x000243E2
		private new SwitchParameter Arbitration
		{
			get
			{
				return base.Arbitration;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x000261EA File Offset: 0x000243EA
		private new MailboxIdParameter ArbitrationMailbox
		{
			get
			{
				return base.ArbitrationMailbox;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x000261F2 File Offset: 0x000243F2
		private new SwitchParameter Archive
		{
			get
			{
				return base.Archive;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x000261FA File Offset: 0x000243FA
		private new DatabaseIdParameter ArchiveDatabase
		{
			get
			{
				return base.ArchiveDatabase;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00026202 File Offset: 0x00024402
		private new SmtpDomain ArchiveDomain
		{
			get
			{
				return base.ArchiveDomain;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0002620A File Offset: 0x0002440A
		private new SwitchParameter BypassLiveId
		{
			get
			{
				return base.BypassLiveId;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00026212 File Offset: 0x00024412
		private new DatabaseIdParameter Database
		{
			get
			{
				return base.Database;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0002621A File Offset: 0x0002441A
		private new SwitchParameter Discovery
		{
			get
			{
				return base.Discovery;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00026222 File Offset: 0x00024422
		private new SwitchParameter Equipment
		{
			get
			{
				return base.Equipment;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0002622A File Offset: 0x0002442A
		private new SwitchParameter EvictLiveId
		{
			get
			{
				return base.EvictLiveId;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00026232 File Offset: 0x00024432
		private new string ExternalDirectoryObjectId
		{
			get
			{
				return base.ExternalDirectoryObjectId;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0002623A File Offset: 0x0002443A
		private new string FederatedIdentity
		{
			get
			{
				return base.FederatedIdentity;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00026242 File Offset: 0x00024442
		private new string FirstName
		{
			get
			{
				return base.FirstName;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0002624A File Offset: 0x0002444A
		private new SwitchParameter Force
		{
			get
			{
				return base.Force;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00026252 File Offset: 0x00024452
		private new SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
		{
			get
			{
				return base.ForestWideDomainControllerAffinityByExecutingUser;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0002625A File Offset: 0x0002445A
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00026262 File Offset: 0x00024462
		private new SwitchParameter HoldForMigration
		{
			get
			{
				return base.HoldForMigration;
			}
			set
			{
				base.HoldForMigration = value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0002626B File Offset: 0x0002446B
		private new string ImmutableId
		{
			get
			{
				return base.ImmutableId;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00026273 File Offset: 0x00024473
		private new SwitchParameter ImportLiveId
		{
			get
			{
				return base.ImportLiveId;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0002627B File Offset: 0x0002447B
		private new string Initials
		{
			get
			{
				return base.Initials;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00026283 File Offset: 0x00024483
		private new string LastName
		{
			get
			{
				return base.LastName;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0002628B File Offset: 0x0002448B
		private new PSCredential LinkedCredential
		{
			get
			{
				return base.LinkedCredential;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00026293 File Offset: 0x00024493
		private new string LinkedDomainController
		{
			get
			{
				return base.LinkedDomainController;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0002629B File Offset: 0x0002449B
		private new UserIdParameter LinkedMasterAccount
		{
			get
			{
				return base.LinkedMasterAccount;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x000262A3 File Offset: 0x000244A3
		private new MailboxPlanIdParameter MailboxPlan
		{
			get
			{
				return base.MailboxPlan;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x000262AB File Offset: 0x000244AB
		private new Guid MailboxContainerGuid
		{
			get
			{
				return base.MailboxContainerGuid;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x000262B3 File Offset: 0x000244B3
		private new WindowsLiveId MicrosoftOnlineServicesID
		{
			get
			{
				return base.MicrosoftOnlineServicesID;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x000262BB File Offset: 0x000244BB
		private new MultiValuedProperty<ModeratorIDParameter> ModeratedBy
		{
			get
			{
				return base.ModeratedBy;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x000262C3 File Offset: 0x000244C3
		private new bool ModerationEnabled
		{
			get
			{
				return base.ModerationEnabled;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x000262CB File Offset: 0x000244CB
		private new NetID NetID
		{
			get
			{
				return base.NetID;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x000262D3 File Offset: 0x000244D3
		private new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return base.OrganizationalUnit;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x000262DB File Offset: 0x000244DB
		private new SwitchParameter OverrideRecipientQuotas
		{
			get
			{
				return base.OverrideRecipientQuotas;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x000262E3 File Offset: 0x000244E3
		private new SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return base.PrimarySmtpAddress;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x000262EB File Offset: 0x000244EB
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x000262F3 File Offset: 0x000244F3
		private new SwitchParameter PublicFolder
		{
			get
			{
				return base.PublicFolder;
			}
			set
			{
				base.PublicFolder = value;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x000262FC File Offset: 0x000244FC
		private new bool QueryBaseDNRestrictionEnabled
		{
			get
			{
				return base.QueryBaseDNRestrictionEnabled;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00026304 File Offset: 0x00024504
		private new RemoteAccountPolicyIdParameter RemoteAccountPolicy
		{
			get
			{
				return base.RemoteAccountPolicy;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0002630C File Offset: 0x0002450C
		private new SwitchParameter RemoteArchive
		{
			get
			{
				return base.RemoteArchive;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00026314 File Offset: 0x00024514
		private new bool RemotePowerShellEnabled
		{
			get
			{
				return base.RemotePowerShellEnabled;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0002631C File Offset: 0x0002451C
		private new RemovedMailboxIdParameter RemovedMailbox
		{
			get
			{
				return base.RemovedMailbox;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00026324 File Offset: 0x00024524
		private new bool ResetPasswordOnNextLogon
		{
			get
			{
				return base.ResetPasswordOnNextLogon;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0002632C File Offset: 0x0002452C
		private new MailboxPolicyIdParameter RetentionPolicy
		{
			get
			{
				return base.RetentionPolicy;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00026334 File Offset: 0x00024534
		private new MailboxPolicyIdParameter RoleAssignmentPolicy
		{
			get
			{
				return base.RoleAssignmentPolicy;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0002633C File Offset: 0x0002453C
		private new SwitchParameter Room
		{
			get
			{
				return base.Room;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00026344 File Offset: 0x00024544
		private new string SamAccountName
		{
			get
			{
				return base.SamAccountName;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0002634C File Offset: 0x0002454C
		private new TransportModerationNotificationFlags SendModerationNotifications
		{
			get
			{
				return base.SendModerationNotifications;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00026354 File Offset: 0x00024554
		private new SwitchParameter Shared
		{
			get
			{
				return base.Shared;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0002635C File Offset: 0x0002455C
		private new SharingPolicyIdParameter SharingPolicy
		{
			get
			{
				return base.SharingPolicy;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00026364 File Offset: 0x00024564
		private new bool SKUAssigned
		{
			get
			{
				return base.SKUAssigned;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0002636C File Offset: 0x0002456C
		private new MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return base.AddOnSKUCapability;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00026374 File Offset: 0x00024574
		private new Capability SKUCapability
		{
			get
			{
				return base.SKUCapability;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0002637C File Offset: 0x0002457C
		private new SwitchParameter TargetAllMDBs
		{
			get
			{
				return base.TargetAllMDBs;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00026384 File Offset: 0x00024584
		private new ThrottlingPolicyIdParameter ThrottlingPolicy
		{
			get
			{
				return base.ThrottlingPolicy;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0002638C File Offset: 0x0002458C
		private new CountryInfo UsageLocation
		{
			get
			{
				return base.UsageLocation;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00026394 File Offset: 0x00024594
		private new SwitchParameter UseExistingLiveId
		{
			get
			{
				return base.UseExistingLiveId;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0002639C File Offset: 0x0002459C
		private new string UserPrincipalName
		{
			get
			{
				return base.UserPrincipalName;
			}
		}
	}
}
