using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000612 RID: 1554
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class UMMailboxPolicy : MailboxPolicy
	{
		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x06004976 RID: 18806 RVA: 0x00110DF8 File Offset: 0x0010EFF8
		internal override ADObjectSchema Schema
		{
			get
			{
				return UMMailboxPolicy.schema;
			}
		}

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x06004977 RID: 18807 RVA: 0x00110DFF File Offset: 0x0010EFFF
		internal override string MostDerivedObjectClass
		{
			get
			{
				return UMMailboxPolicy.mostDerivedClass;
			}
		}

		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x06004978 RID: 18808 RVA: 0x00110E06 File Offset: 0x0010F006
		internal override ADObjectId ParentPath
		{
			get
			{
				return UMMailboxPolicy.parentPath;
			}
		}

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x06004979 RID: 18809 RVA: 0x00110E0D File Offset: 0x0010F00D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x00110E14 File Offset: 0x0010F014
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x00110E18 File Offset: 0x0010F018
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(UMMailboxPolicySchema.PINLifetime))
			{
				this.PINLifetime = EnhancedTimeSpan.FromDays(60.0);
			}
			if (!base.IsModified(UMMailboxPolicySchema.LogonFailuresBeforePINReset))
			{
				this.LogonFailuresBeforePINReset = 5;
			}
			if (!base.IsModified(UMMailboxPolicySchema.MaxLogonAttempts))
			{
				this.MaxLogonAttempts = 15;
			}
			if (!base.IsModified(UMMailboxPolicySchema.AllowMissedCallNotifications))
			{
				this.AllowMissedCallNotifications = true;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x00110E98 File Offset: 0x0010F098
		internal override bool CheckForAssociatedUsers()
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, base.Id.DistinguishedName),
				new ExistsFilter(UMMailboxPolicySchema.AssociatedUsers)
			});
			UMMailboxPolicy[] array = base.Session.Find<UMMailboxPolicy>(null, QueryScope.SubTree, filter, null, 1);
			return array != null && array.Length > 0;
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x00110EF8 File Offset: 0x0010F0F8
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!this.LogonFailuresBeforePINReset.IsUnlimited && !this.MaxLogonAttempts.IsUnlimited && this.LogonFailuresBeforePINReset.Value >= this.MaxLogonAttempts.Value)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorLogonFailuresBeforePINReset(this.LogonFailuresBeforePINReset.Value, this.MaxLogonAttempts.ToString()), this.Identity, string.Empty));
			}
			if (this.AllowFax && string.IsNullOrEmpty(this.FaxServerURI))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.FaxServerURINoValue, UMMailboxPolicySchema.FaxServerURI, this));
			}
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x00110FB4 File Offset: 0x0010F1B4
		internal UMDialPlan GetDialPlan()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(this.UMDialPlan);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 663, "GetDialPlan", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\UMMailboxPolicy.cs");
			return tenantOrTopologyConfigurationSession.Read<UMDialPlan>(this.UMDialPlan);
		}

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x0600497F RID: 18815 RVA: 0x00110FFA File Offset: 0x0010F1FA
		// (set) Token: 0x06004980 RID: 18816 RVA: 0x0011100C File Offset: 0x0010F20C
		[Parameter(Mandatory = false)]
		public int MaxGreetingDuration
		{
			get
			{
				return (int)this[UMMailboxPolicySchema.MaxGreetingDuration];
			}
			set
			{
				this[UMMailboxPolicySchema.MaxGreetingDuration] = value;
			}
		}

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x06004981 RID: 18817 RVA: 0x0011101F File Offset: 0x0010F21F
		// (set) Token: 0x06004982 RID: 18818 RVA: 0x00111031 File Offset: 0x0010F231
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxLogonAttempts
		{
			get
			{
				return (Unlimited<int>)this[UMMailboxPolicySchema.MaxLogonAttempts];
			}
			set
			{
				this[UMMailboxPolicySchema.MaxLogonAttempts] = value;
			}
		}

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x00111044 File Offset: 0x0010F244
		// (set) Token: 0x06004984 RID: 18820 RVA: 0x00111056 File Offset: 0x0010F256
		[Parameter(Mandatory = false)]
		public bool AllowCommonPatterns
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowCommonPatterns];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowCommonPatterns] = value;
			}
		}

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x00111069 File Offset: 0x0010F269
		// (set) Token: 0x06004986 RID: 18822 RVA: 0x0011107B File Offset: 0x0010F27B
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> PINLifetime
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)this[UMMailboxPolicySchema.PINLifetime];
			}
			set
			{
				this[UMMailboxPolicySchema.PINLifetime] = value;
			}
		}

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x06004987 RID: 18823 RVA: 0x0011108E File Offset: 0x0010F28E
		// (set) Token: 0x06004988 RID: 18824 RVA: 0x001110A0 File Offset: 0x0010F2A0
		[Parameter(Mandatory = false)]
		public int PINHistoryCount
		{
			get
			{
				return (int)this[UMMailboxPolicySchema.PINHistoryCount];
			}
			set
			{
				this[UMMailboxPolicySchema.PINHistoryCount] = value;
			}
		}

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x001110B3 File Offset: 0x0010F2B3
		// (set) Token: 0x0600498A RID: 18826 RVA: 0x001110C5 File Offset: 0x0010F2C5
		[Parameter(Mandatory = false)]
		public bool AllowSMSNotification
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowSMSNotification];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowSMSNotification] = value;
			}
		}

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x001110D8 File Offset: 0x0010F2D8
		// (set) Token: 0x0600498C RID: 18828 RVA: 0x001110EA File Offset: 0x0010F2EA
		[Parameter(Mandatory = false)]
		public DRMProtectionOptions ProtectUnauthenticatedVoiceMail
		{
			get
			{
				return (DRMProtectionOptions)this[UMMailboxPolicySchema.ProtectUnauthenticatedVoiceMail];
			}
			set
			{
				this[UMMailboxPolicySchema.ProtectUnauthenticatedVoiceMail] = value;
			}
		}

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x001110FD File Offset: 0x0010F2FD
		// (set) Token: 0x0600498E RID: 18830 RVA: 0x0011110F File Offset: 0x0010F30F
		[Parameter(Mandatory = false)]
		public DRMProtectionOptions ProtectAuthenticatedVoiceMail
		{
			get
			{
				return (DRMProtectionOptions)this[UMMailboxPolicySchema.ProtectAuthenticatedVoiceMail];
			}
			set
			{
				this[UMMailboxPolicySchema.ProtectAuthenticatedVoiceMail] = value;
			}
		}

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x00111122 File Offset: 0x0010F322
		// (set) Token: 0x06004990 RID: 18832 RVA: 0x00111134 File Offset: 0x0010F334
		[Parameter(Mandatory = false)]
		public string ProtectedVoiceMailText
		{
			get
			{
				return (string)this[UMMailboxPolicySchema.ProtectedVoiceMailText];
			}
			set
			{
				this[UMMailboxPolicySchema.ProtectedVoiceMailText] = value;
			}
		}

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x06004991 RID: 18833 RVA: 0x00111142 File Offset: 0x0010F342
		// (set) Token: 0x06004992 RID: 18834 RVA: 0x00111154 File Offset: 0x0010F354
		[Parameter(Mandatory = false)]
		public bool RequireProtectedPlayOnPhone
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.RequireProtectedPlayOnPhone];
			}
			set
			{
				this[UMMailboxPolicySchema.RequireProtectedPlayOnPhone] = value;
			}
		}

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x06004993 RID: 18835 RVA: 0x00111167 File Offset: 0x0010F367
		// (set) Token: 0x06004994 RID: 18836 RVA: 0x00111179 File Offset: 0x0010F379
		[Parameter(Mandatory = false)]
		public int MinPINLength
		{
			get
			{
				return (int)this[UMMailboxPolicySchema.MinPINLength];
			}
			set
			{
				this[UMMailboxPolicySchema.MinPINLength] = value;
			}
		}

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x06004995 RID: 18837 RVA: 0x0011118C File Offset: 0x0010F38C
		// (set) Token: 0x06004996 RID: 18838 RVA: 0x0011119E File Offset: 0x0010F39E
		[Parameter(Mandatory = false)]
		public string FaxMessageText
		{
			get
			{
				return (string)this[UMMailboxPolicySchema.FaxMessageText];
			}
			set
			{
				this[UMMailboxPolicySchema.FaxMessageText] = value;
			}
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x06004997 RID: 18839 RVA: 0x001111AC File Offset: 0x0010F3AC
		// (set) Token: 0x06004998 RID: 18840 RVA: 0x001111BE File Offset: 0x0010F3BE
		[Parameter(Mandatory = false)]
		public string UMEnabledText
		{
			get
			{
				return (string)this[UMMailboxPolicySchema.UMEnabledText];
			}
			set
			{
				this[UMMailboxPolicySchema.UMEnabledText] = value;
			}
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x06004999 RID: 18841 RVA: 0x001111CC File Offset: 0x0010F3CC
		// (set) Token: 0x0600499A RID: 18842 RVA: 0x001111DE File Offset: 0x0010F3DE
		[Parameter(Mandatory = false)]
		public string ResetPINText
		{
			get
			{
				return (string)this[UMMailboxPolicySchema.ResetPINText];
			}
			set
			{
				this[UMMailboxPolicySchema.ResetPINText] = value;
			}
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x0600499B RID: 18843 RVA: 0x001111EC File Offset: 0x0010F3EC
		// (set) Token: 0x0600499C RID: 18844 RVA: 0x001111FE File Offset: 0x0010F3FE
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> SourceForestPolicyNames
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMMailboxPolicySchema.SourceForestPolicyNames];
			}
			set
			{
				this[UMMailboxPolicySchema.SourceForestPolicyNames] = value;
			}
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x0600499D RID: 18845 RVA: 0x0011120C File Offset: 0x0010F40C
		// (set) Token: 0x0600499E RID: 18846 RVA: 0x0011121E File Offset: 0x0010F41E
		[Parameter(Mandatory = false)]
		public string VoiceMailText
		{
			get
			{
				return (string)this[UMMailboxPolicySchema.VoiceMailText];
			}
			set
			{
				this[UMMailboxPolicySchema.VoiceMailText] = value;
			}
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x0011122C File Offset: 0x0010F42C
		// (set) Token: 0x060049A0 RID: 18848 RVA: 0x0011123E File Offset: 0x0010F43E
		public ADObjectId UMDialPlan
		{
			get
			{
				return (ADObjectId)this[UMMailboxPolicySchema.UMDialPlan];
			}
			set
			{
				this[UMMailboxPolicySchema.UMDialPlan] = value;
			}
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x0011124C File Offset: 0x0010F44C
		// (set) Token: 0x060049A2 RID: 18850 RVA: 0x0011125E File Offset: 0x0010F45E
		[Parameter(Mandatory = false)]
		public string FaxServerURI
		{
			get
			{
				return (string)this[UMMailboxPolicySchema.FaxServerURI];
			}
			set
			{
				this[UMMailboxPolicySchema.FaxServerURI] = value;
			}
		}

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x060049A3 RID: 18851 RVA: 0x0011126C File Offset: 0x0010F46C
		// (set) Token: 0x060049A4 RID: 18852 RVA: 0x0011127E File Offset: 0x0010F47E
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AllowedInCountryOrRegionGroups
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMMailboxPolicySchema.AllowedInCountryOrRegionGroups];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowedInCountryOrRegionGroups] = value;
			}
		}

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x060049A5 RID: 18853 RVA: 0x0011128C File Offset: 0x0010F48C
		// (set) Token: 0x060049A6 RID: 18854 RVA: 0x0011129E File Offset: 0x0010F49E
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AllowedInternationalGroups
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMMailboxPolicySchema.AllowedInternationalGroups];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowedInternationalGroups] = value;
			}
		}

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x001112AC File Offset: 0x0010F4AC
		// (set) Token: 0x060049A8 RID: 18856 RVA: 0x001112BE File Offset: 0x0010F4BE
		[Parameter(Mandatory = false)]
		public bool AllowDialPlanSubscribers
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowDialPlanSubscribers];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowDialPlanSubscribers] = value;
			}
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x060049A9 RID: 18857 RVA: 0x001112D1 File Offset: 0x0010F4D1
		// (set) Token: 0x060049AA RID: 18858 RVA: 0x001112E3 File Offset: 0x0010F4E3
		[Parameter(Mandatory = false)]
		public bool AllowExtensions
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowExtensions];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowExtensions] = value;
			}
		}

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x060049AB RID: 18859 RVA: 0x001112F6 File Offset: 0x0010F4F6
		// (set) Token: 0x060049AC RID: 18860 RVA: 0x00111308 File Offset: 0x0010F508
		[Parameter(Mandatory = false)]
		public Unlimited<int> LogonFailuresBeforePINReset
		{
			get
			{
				return (Unlimited<int>)this[UMMailboxPolicySchema.LogonFailuresBeforePINReset];
			}
			set
			{
				this[UMMailboxPolicySchema.LogonFailuresBeforePINReset] = value;
			}
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x060049AD RID: 18861 RVA: 0x0011131B File Offset: 0x0010F51B
		// (set) Token: 0x060049AE RID: 18862 RVA: 0x0011132D File Offset: 0x0010F52D
		[Parameter(Mandatory = false)]
		public bool AllowMissedCallNotifications
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowMissedCallNotifications];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowMissedCallNotifications] = value;
			}
		}

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x060049AF RID: 18863 RVA: 0x00111340 File Offset: 0x0010F540
		// (set) Token: 0x060049B0 RID: 18864 RVA: 0x00111352 File Offset: 0x0010F552
		[Parameter(Mandatory = false)]
		public bool AllowFax
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowFax];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowFax] = value;
			}
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x060049B1 RID: 18865 RVA: 0x00111365 File Offset: 0x0010F565
		// (set) Token: 0x060049B2 RID: 18866 RVA: 0x00111377 File Offset: 0x0010F577
		[Parameter(Mandatory = false)]
		public bool AllowTUIAccessToCalendar
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowTUIAccessToCalendar];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowTUIAccessToCalendar] = value;
			}
		}

		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x0011138A File Offset: 0x0010F58A
		// (set) Token: 0x060049B4 RID: 18868 RVA: 0x0011139C File Offset: 0x0010F59C
		[Parameter(Mandatory = false)]
		public bool AllowTUIAccessToEmail
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowTUIAccessToEmail];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowTUIAccessToEmail] = value;
			}
		}

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x060049B5 RID: 18869 RVA: 0x001113AF File Offset: 0x0010F5AF
		// (set) Token: 0x060049B6 RID: 18870 RVA: 0x001113C1 File Offset: 0x0010F5C1
		[Parameter(Mandatory = false)]
		public bool AllowSubscriberAccess
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowSubscriberAccess];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowSubscriberAccess] = value;
			}
		}

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x060049B7 RID: 18871 RVA: 0x001113D4 File Offset: 0x0010F5D4
		// (set) Token: 0x060049B8 RID: 18872 RVA: 0x001113E6 File Offset: 0x0010F5E6
		[Parameter(Mandatory = false)]
		public bool AllowTUIAccessToDirectory
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowTUIAccessToDirectory];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowTUIAccessToDirectory] = value;
			}
		}

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x060049B9 RID: 18873 RVA: 0x001113F9 File Offset: 0x0010F5F9
		// (set) Token: 0x060049BA RID: 18874 RVA: 0x0011140B File Offset: 0x0010F60B
		[Parameter(Mandatory = false)]
		public bool AllowTUIAccessToPersonalContacts
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowTUIAccessToPersonalContacts];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowTUIAccessToPersonalContacts] = value;
			}
		}

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x0011141E File Offset: 0x0010F61E
		// (set) Token: 0x060049BC RID: 18876 RVA: 0x00111430 File Offset: 0x0010F630
		[Parameter(Mandatory = false)]
		public bool AllowAutomaticSpeechRecognition
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowASR];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowASR] = value;
			}
		}

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x060049BD RID: 18877 RVA: 0x00111443 File Offset: 0x0010F643
		// (set) Token: 0x060049BE RID: 18878 RVA: 0x00111455 File Offset: 0x0010F655
		[Parameter(Mandatory = false)]
		public bool AllowPlayOnPhone
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowPlayOnPhone];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowPlayOnPhone] = value;
			}
		}

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x060049BF RID: 18879 RVA: 0x00111468 File Offset: 0x0010F668
		// (set) Token: 0x060049C0 RID: 18880 RVA: 0x0011147A File Offset: 0x0010F67A
		[Parameter(Mandatory = false)]
		public bool AllowVoiceMailPreview
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowVoiceMailPreview];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowVoiceMailPreview] = value;
			}
		}

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x060049C1 RID: 18881 RVA: 0x0011148D File Offset: 0x0010F68D
		// (set) Token: 0x060049C2 RID: 18882 RVA: 0x0011149F File Offset: 0x0010F69F
		[Parameter(Mandatory = false)]
		public bool AllowCallAnsweringRules
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowPersonalAutoAttendant];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowPersonalAutoAttendant] = value;
			}
		}

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x060049C3 RID: 18883 RVA: 0x001114B2 File Offset: 0x0010F6B2
		// (set) Token: 0x060049C4 RID: 18884 RVA: 0x001114C4 File Offset: 0x0010F6C4
		[Parameter(Mandatory = false)]
		public bool AllowMessageWaitingIndicator
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowMessageWaitingIndicator];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowMessageWaitingIndicator] = value;
			}
		}

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x060049C5 RID: 18885 RVA: 0x001114D7 File Offset: 0x0010F6D7
		// (set) Token: 0x060049C6 RID: 18886 RVA: 0x001114E9 File Offset: 0x0010F6E9
		[Parameter(Mandatory = false)]
		public bool AllowPinlessVoiceMailAccess
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowPinlessVoiceMailAccess];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowPinlessVoiceMailAccess] = value;
			}
		}

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x001114FC File Offset: 0x0010F6FC
		// (set) Token: 0x060049C8 RID: 18888 RVA: 0x0011150E File Offset: 0x0010F70E
		[Parameter(Mandatory = false)]
		public bool AllowVoiceResponseToOtherMessageTypes
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowVoiceResponseToOtherMessageTypes];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowVoiceResponseToOtherMessageTypes] = value;
			}
		}

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x060049C9 RID: 18889 RVA: 0x00111521 File Offset: 0x0010F721
		// (set) Token: 0x060049CA RID: 18890 RVA: 0x00111533 File Offset: 0x0010F733
		[Parameter(Mandatory = false)]
		public bool AllowVoiceMailAnalysis
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowVoiceMailAnalysis];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowVoiceMailAnalysis] = value;
			}
		}

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x00111546 File Offset: 0x0010F746
		// (set) Token: 0x060049CC RID: 18892 RVA: 0x00111558 File Offset: 0x0010F758
		[Parameter(Mandatory = false)]
		public bool AllowVoiceNotification
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowVoiceNotification];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowVoiceNotification] = value;
			}
		}

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x0011156B File Offset: 0x0010F76B
		// (set) Token: 0x060049CE RID: 18894 RVA: 0x0011157D File Offset: 0x0010F77D
		[Parameter(Mandatory = false)]
		public bool InformCallerOfVoiceMailAnalysis
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.InformCallerOfVoiceMailAnalysis];
			}
			set
			{
				this[UMMailboxPolicySchema.InformCallerOfVoiceMailAnalysis] = value;
			}
		}

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x00111590 File Offset: 0x0010F790
		// (set) Token: 0x060049D0 RID: 18896 RVA: 0x001115A2 File Offset: 0x0010F7A2
		internal bool AllowVirtualNumber
		{
			get
			{
				return (bool)this[UMMailboxPolicySchema.AllowVirtualNumber];
			}
			set
			{
				this[UMMailboxPolicySchema.AllowVirtualNumber] = value;
			}
		}

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x060049D1 RID: 18897 RVA: 0x001115B5 File Offset: 0x0010F7B5
		// (set) Token: 0x060049D2 RID: 18898 RVA: 0x001115C7 File Offset: 0x0010F7C7
		[Parameter(Mandatory = false)]
		public SmtpAddress? VoiceMailPreviewPartnerAddress
		{
			get
			{
				return (SmtpAddress?)this[UMMailboxPolicySchema.VoiceMailPreviewPartnerAddress];
			}
			set
			{
				this[UMMailboxPolicySchema.VoiceMailPreviewPartnerAddress] = value;
			}
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x060049D3 RID: 18899 RVA: 0x001115DA File Offset: 0x0010F7DA
		// (set) Token: 0x060049D4 RID: 18900 RVA: 0x001115EC File Offset: 0x0010F7EC
		[Parameter(Mandatory = false)]
		public string VoiceMailPreviewPartnerAssignedID
		{
			get
			{
				return (string)this[UMMailboxPolicySchema.VoiceMailPreviewPartnerAssignedID];
			}
			set
			{
				this[UMMailboxPolicySchema.VoiceMailPreviewPartnerAssignedID] = value;
			}
		}

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x001115FA File Offset: 0x0010F7FA
		// (set) Token: 0x060049D6 RID: 18902 RVA: 0x0011160C File Offset: 0x0010F80C
		[Parameter(Mandatory = false)]
		public int VoiceMailPreviewPartnerMaxMessageDuration
		{
			get
			{
				return (int)this[UMMailboxPolicySchema.VoiceMailPreviewPartnerMaxMessageDuration];
			}
			set
			{
				this[UMMailboxPolicySchema.VoiceMailPreviewPartnerMaxMessageDuration] = value;
			}
		}

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x0011161F File Offset: 0x0010F81F
		// (set) Token: 0x060049D8 RID: 18904 RVA: 0x00111631 File Offset: 0x0010F831
		[Parameter(Mandatory = false)]
		public int VoiceMailPreviewPartnerMaxDeliveryDelay
		{
			get
			{
				return (int)this[UMMailboxPolicySchema.VoiceMailPreviewPartnerMaxDeliveryDelay];
			}
			set
			{
				this[UMMailboxPolicySchema.VoiceMailPreviewPartnerMaxDeliveryDelay] = value;
			}
		}

		// Token: 0x04003318 RID: 13080
		private static UMMailboxPolicySchema schema = ObjectSchema.GetInstance<UMMailboxPolicySchema>();

		// Token: 0x04003319 RID: 13081
		private static string mostDerivedClass = "msExchUMRecipientTemplate";

		// Token: 0x0400331A RID: 13082
		private static ADObjectId parentPath = new ADObjectId("CN=UM Mailbox Policies");
	}
}
