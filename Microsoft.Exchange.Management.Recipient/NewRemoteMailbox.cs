using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000B4 RID: 180
	[Cmdlet("New", "RemoteMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "EnabledUser")]
	public sealed class NewRemoteMailbox : NewMailUserBase
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x0003042C File Offset: 0x0002E62C
		public NewRemoteMailbox()
		{
			base.NumberofCalls = ProvisioningCounters.NumberOfNewRemoteMailboxCalls;
			base.NumberofSuccessfulCalls = ProvisioningCounters.NumberOfSuccessfulNewRemoteMailboxCalls;
			base.AverageTimeTaken = ProvisioningCounters.AverageNewRemoteMailboxResponseTime;
			base.AverageBaseTimeTaken = ProvisioningCounters.AverageNewRemoteMailboxResponseTimeBase;
			base.AverageTimeTakenWithCache = ProvisioningCounters.AverageNewRemoteMailboxResponseTimeWithCache;
			base.AverageBaseTimeTakenWithCache = ProvisioningCounters.AverageNewRemoteMailboxResponseTimeBaseWithCache;
			base.AverageTimeTakenWithoutCache = ProvisioningCounters.AverageNewRemoteMailboxResponseTimeWithoutCache;
			base.AverageBaseTimeTakenWithoutCache = ProvisioningCounters.AverageNewRemoteMailboxResponseTimeBaseWithoutCache;
			base.TotalResponseTime = ProvisioningCounters.TotalNewRemoteMailboxResponseTime;
			base.CacheActivePercentage = ProvisioningCounters.NewRemoteMailboxCacheActivePercentage;
			base.CacheActiveBasePercentage = ProvisioningCounters.NewRemoteMailboxCacheActivePercentageBase;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x000304B8 File Offset: 0x0002E6B8
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x000304C0 File Offset: 0x0002E6C0
		[Parameter]
		public ProxyAddress RemoteRoutingAddress
		{
			get
			{
				return this.ExternalEmailAddress;
			}
			set
			{
				this.ExternalEmailAddress = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x000304C9 File Offset: 0x0002E6C9
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x000304D1 File Offset: 0x0002E6D1
		[Parameter]
		public OrganizationalUnitIdParameter OnPremisesOrganizationalUnit
		{
			get
			{
				return this.OrganizationalUnit;
			}
			set
			{
				this.OrganizationalUnit = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x000304DA File Offset: 0x0002E6DA
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00030500 File Offset: 0x0002E700
		[Parameter]
		public SwitchParameter Archive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Archive"] ?? false);
			}
			set
			{
				base.Fields["Archive"] = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00030518 File Offset: 0x0002E718
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0003053E File Offset: 0x0002E73E
		[Parameter(Mandatory = true, ParameterSetName = "Room")]
		public SwitchParameter Room
		{
			get
			{
				return (SwitchParameter)(base.Fields["Room"] ?? false);
			}
			set
			{
				base.Fields["Room"] = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00030556 File Offset: 0x0002E756
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0003057C File Offset: 0x0002E77C
		[Parameter(Mandatory = true, ParameterSetName = "Equipment")]
		public SwitchParameter Equipment
		{
			get
			{
				return (SwitchParameter)(base.Fields["Equipment"] ?? false);
			}
			set
			{
				base.Fields["Equipment"] = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00030594 File Offset: 0x0002E794
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x000305BA File Offset: 0x0002E7BA
		[Parameter(Mandatory = true, ParameterSetName = "Shared")]
		public SwitchParameter Shared
		{
			get
			{
				return (SwitchParameter)(base.Fields["Shared"] ?? false);
			}
			set
			{
				base.Fields["Shared"] = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x000305D2 File Offset: 0x0002E7D2
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x000305F8 File Offset: 0x0002E7F8
		[Parameter(Mandatory = true, ParameterSetName = "DisabledUser")]
		public SwitchParameter AccountDisabled
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisabledUser"] ?? false);
			}
			set
			{
				base.Fields["DisabledUser"] = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00030610 File Offset: 0x0002E810
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00030636 File Offset: 0x0002E836
		[Parameter(Mandatory = false)]
		public SwitchParameter ACLableSyncedObjectEnabled
		{
			get
			{
				return (SwitchParameter)(base.Fields["ACLableSyncedObjectEnabled"] ?? false);
			}
			set
			{
				base.Fields["ACLableSyncedObjectEnabled"] = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0003064E File Offset: 0x0002E84E
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00030656 File Offset: 0x0002E856
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = true, ParameterSetName = "EnabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		public override string UserPrincipalName
		{
			get
			{
				return base.UserPrincipalName;
			}
			set
			{
				base.UserPrincipalName = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0003065F File Offset: 0x0002E85F
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00030667 File Offset: 0x0002E867
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = true, ParameterSetName = "EnabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
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

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00030670 File Offset: 0x0002E870
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(RemoteMailbox).FullName;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00030681 File Offset: 0x0002E881
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x00030689 File Offset: 0x0002E889
		public override bool UsePreferMessageFormat
		{
			get
			{
				return base.UsePreferMessageFormat;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00030690 File Offset: 0x0002E890
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x00030698 File Offset: 0x0002E898
		public override MessageFormat MessageFormat
		{
			get
			{
				return base.MessageFormat;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0003069F File Offset: 0x0002E89F
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x000306A7 File Offset: 0x0002E8A7
		public override MessageBodyFormat MessageBodyFormat
		{
			get
			{
				return base.MessageBodyFormat;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x000306AE File Offset: 0x0002E8AE
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x000306B6 File Offset: 0x0002E8B6
		public override MacAttachmentFormat MacAttachmentFormat
		{
			get
			{
				return base.MacAttachmentFormat;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x000306BD File Offset: 0x0002E8BD
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x000306C5 File Offset: 0x0002E8C5
		public override Capability SKUCapability
		{
			get
			{
				return base.SKUCapability;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x000306CC File Offset: 0x0002E8CC
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x000306D4 File Offset: 0x0002E8D4
		public override MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return base.AddOnSKUCapability;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x000306DB File Offset: 0x0002E8DB
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x000306E3 File Offset: 0x0002E8E3
		public override bool SKUAssigned
		{
			get
			{
				return base.SKUAssigned;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x000306EA File Offset: 0x0002E8EA
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x000306F2 File Offset: 0x0002E8F2
		private new ProxyAddress ExternalEmailAddress
		{
			get
			{
				return base.ExternalEmailAddress;
			}
			set
			{
				base.ExternalEmailAddress = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x000306FB File Offset: 0x0002E8FB
		// (set) Token: 0x06000B70 RID: 2928 RVA: 0x00030703 File Offset: 0x0002E903
		private new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return base.OrganizationalUnit;
			}
			set
			{
				base.OrganizationalUnit = value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0003070C File Offset: 0x0002E90C
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x00030714 File Offset: 0x0002E914
		private new WindowsLiveId WindowsLiveID
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

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0003071D File Offset: 0x0002E91D
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x00030725 File Offset: 0x0002E925
		private new WindowsLiveId MicrosoftOnlineServicesID
		{
			get
			{
				return base.MicrosoftOnlineServicesID;
			}
			set
			{
				base.MicrosoftOnlineServicesID = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0003072E File Offset: 0x0002E92E
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x00030736 File Offset: 0x0002E936
		private new SwitchParameter UseExistingLiveId
		{
			get
			{
				return base.UseExistingLiveId;
			}
			set
			{
				base.UseExistingLiveId = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0003073F File Offset: 0x0002E93F
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x00030747 File Offset: 0x0002E947
		private new NetID NetID
		{
			get
			{
				return base.NetID;
			}
			set
			{
				base.NetID = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00030750 File Offset: 0x0002E950
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x00030758 File Offset: 0x0002E958
		private new SwitchParameter ImportLiveId
		{
			get
			{
				return base.ImportLiveId;
			}
			set
			{
				base.ImportLiveId = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00030761 File Offset: 0x0002E961
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x00030769 File Offset: 0x0002E969
		private new SwitchParameter BypassLiveId
		{
			get
			{
				return base.BypassLiveId;
			}
			set
			{
				base.BypassLiveId = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x00030772 File Offset: 0x0002E972
		// (set) Token: 0x06000B7E RID: 2942 RVA: 0x0003077A File Offset: 0x0002E97A
		private new SwitchParameter EvictLiveId
		{
			get
			{
				return base.EvictLiveId;
			}
			set
			{
				base.EvictLiveId = value;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00030783 File Offset: 0x0002E983
		// (set) Token: 0x06000B80 RID: 2944 RVA: 0x0003078B File Offset: 0x0002E98B
		private new string FederatedIdentity
		{
			get
			{
				return base.FederatedIdentity;
			}
			set
			{
				base.FederatedIdentity = value;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00030794 File Offset: 0x0002E994
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x0003079C File Offset: 0x0002E99C
		private new string ExternalDirectoryObjectId
		{
			get
			{
				return base.ExternalDirectoryObjectId;
			}
			set
			{
				base.ExternalDirectoryObjectId = value;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x000307A5 File Offset: 0x0002E9A5
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x000307AD File Offset: 0x0002E9AD
		private new OrganizationIdParameter Organization
		{
			get
			{
				return base.Organization;
			}
			set
			{
				base.Organization = value;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x000307B6 File Offset: 0x0002E9B6
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x000307BD File Offset: 0x0002E9BD
		private new CountryInfo UsageLocation
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x000307C4 File Offset: 0x0002E9C4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewRemoteMailbox(base.Name.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x000307EC File Offset: 0x0002E9EC
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x000307F4 File Offset: 0x0002E9F4
		private bool primarySmtpAddressAssginedByExternalEmailAddress { get; set; }

		// Token: 0x06000B8A RID: 2954 RVA: 0x000307FD File Offset: 0x0002E9FD
		protected override bool GetEmailAddressPolicyEnabledDefaultValue(IConfigurable dataObject)
		{
			return base.GetEmailAddressPolicyEnabledDefaultValue(dataObject) || this.primarySmtpAddressAssginedByExternalEmailAddress;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00030810 File Offset: 0x0002EA10
		protected override void PrepareRecipientObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(user);
			if (null == this.RemoteRoutingAddress)
			{
				if (this.remoteRoutingAddressGenerator == null)
				{
					this.remoteRoutingAddressGenerator = new RemoteRoutingAddressGenerator(this.ConfigurationSession);
				}
				user.ExternalEmailAddress = this.remoteRoutingAddressGenerator.GenerateRemoteRoutingAddress(user.Alias, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (user.ExternalEmailAddress != null)
			{
				bool flag = false;
				foreach (ProxyAddress a in user.EmailAddresses)
				{
					if (a == user.ExternalEmailAddress)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					if (user.PrimarySmtpAddress == SmtpAddress.Empty)
					{
						this.primarySmtpAddressAssginedByExternalEmailAddress = true;
					}
					user.EmailAddresses.Add(user.ExternalEmailAddress.ToSecondary());
				}
			}
			user.RemoteRecipientType = RemoteRecipientType.ProvisionMailbox;
			RemoteMailboxType remoteMailboxType = (RemoteMailboxType)((ulong)int.MinValue);
			if (this.Room.IsPresent)
			{
				remoteMailboxType = RemoteMailboxType.Room;
			}
			else if (this.Equipment.IsPresent)
			{
				remoteMailboxType = RemoteMailboxType.Equipment;
			}
			else if (this.Shared.IsPresent)
			{
				remoteMailboxType = RemoteMailboxType.Shared;
			}
			user.UpdateRemoteMailboxType(remoteMailboxType, this.ACLableSyncedObjectEnabled);
			if (this.Archive.IsPresent)
			{
				user.ArchiveGuid = Guid.NewGuid();
				user.ArchiveName = new MultiValuedProperty<string>(Strings.ArchiveNamePrefix + (string.IsNullOrEmpty(user.DisplayName) ? user.Name : user.DisplayName));
				user.RemoteRecipientType |= RemoteRecipientType.ProvisionArchive;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000309E0 File Offset: 0x0002EBE0
		protected override void StampChangesAfterSettingPassword()
		{
			base.StampChangesAfterSettingPassword();
			if (base.ParameterSetName == "Room" || base.ParameterSetName == "Equipment" || base.ParameterSetName == "Shared")
			{
				this.DataObject.UserAccountControl = (UserAccountControlFlags.AccountDisabled | UserAccountControlFlags.NormalAccount);
				if (!base.ResetPasswordOnNextLogon && (this.Password == null || this.Password.Length == 0))
				{
					this.DataObject.UserAccountControl |= UserAccountControlFlags.DoNotExpirePassword;
				}
			}
			if (this.DataObject.LegacyExchangeDN != null)
			{
				this.DataObject.LegacyExchangeDN = this.DataObject.LegacyExchangeDN.Replace("Exchange Administrative Group (FYDIBOHF23SPDLT)", "External (FYDIBOHF25SPDLT)");
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00030AA0 File Offset: 0x0002ECA0
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			RemoteMailbox result2 = new RemoteMailbox((ADUser)result);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00030ADB File Offset: 0x0002ECDB
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return RemoteMailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x04000275 RID: 629
		public const string ParameterACLableSyncedEnabled = "ACLableSyncedObjectEnabled";

		// Token: 0x04000276 RID: 630
		private RemoteRoutingAddressGenerator remoteRoutingAddressGenerator;
	}
}
