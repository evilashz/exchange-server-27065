using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D33 RID: 3379
	[Cmdlet("New", "UMDialPlan", SupportsShouldProcess = true)]
	public sealed class NewUMDialPlan : NewMultitenancySystemConfigurationObjectTask<UMDialPlan>
	{
		// Token: 0x17002831 RID: 10289
		// (get) Token: 0x06008173 RID: 33139 RVA: 0x0021156B File Offset: 0x0020F76B
		// (set) Token: 0x06008174 RID: 33140 RVA: 0x00211578 File Offset: 0x0020F778
		[Parameter(Mandatory = true)]
		public int NumberOfDigitsInExtension
		{
			get
			{
				return this.DataObject.NumberOfDigitsInExtension;
			}
			set
			{
				this.DataObject.NumberOfDigitsInExtension = value;
			}
		}

		// Token: 0x17002832 RID: 10290
		// (get) Token: 0x06008175 RID: 33141 RVA: 0x00211586 File Offset: 0x0020F786
		// (set) Token: 0x06008176 RID: 33142 RVA: 0x00211593 File Offset: 0x0020F793
		[Parameter(Mandatory = false)]
		public UMUriType URIType
		{
			get
			{
				return this.DataObject.URIType;
			}
			set
			{
				this.DataObject.URIType = value;
			}
		}

		// Token: 0x17002833 RID: 10291
		// (get) Token: 0x06008177 RID: 33143 RVA: 0x002115A1 File Offset: 0x0020F7A1
		// (set) Token: 0x06008178 RID: 33144 RVA: 0x002115AE File Offset: 0x0020F7AE
		[Parameter(Mandatory = false)]
		public UMSubscriberType SubscriberType
		{
			get
			{
				return this.DataObject.SubscriberType;
			}
			set
			{
				this.DataObject.SubscriberType = value;
			}
		}

		// Token: 0x17002834 RID: 10292
		// (get) Token: 0x06008179 RID: 33145 RVA: 0x002115BC File Offset: 0x0020F7BC
		// (set) Token: 0x0600817A RID: 33146 RVA: 0x002115C9 File Offset: 0x0020F7C9
		[Parameter(Mandatory = false)]
		public UMVoIPSecurityType VoIPSecurity
		{
			get
			{
				return this.DataObject.VoIPSecurity;
			}
			set
			{
				this.DataObject.VoIPSecurity = value;
			}
		}

		// Token: 0x17002835 RID: 10293
		// (get) Token: 0x0600817B RID: 33147 RVA: 0x002115D7 File Offset: 0x0020F7D7
		// (set) Token: 0x0600817C RID: 33148 RVA: 0x002115E4 File Offset: 0x0020F7E4
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AccessTelephoneNumbers
		{
			get
			{
				return this.DataObject.AccessTelephoneNumbers;
			}
			set
			{
				this.DataObject.AccessTelephoneNumbers = value;
			}
		}

		// Token: 0x17002836 RID: 10294
		// (get) Token: 0x0600817D RID: 33149 RVA: 0x002115F2 File Offset: 0x0020F7F2
		// (set) Token: 0x0600817E RID: 33150 RVA: 0x002115FF File Offset: 0x0020F7FF
		[Parameter(Mandatory = false)]
		public bool FaxEnabled
		{
			get
			{
				return this.DataObject.FaxEnabled;
			}
			set
			{
				this.DataObject.FaxEnabled = value;
			}
		}

		// Token: 0x17002837 RID: 10295
		// (get) Token: 0x0600817F RID: 33151 RVA: 0x0021160D File Offset: 0x0020F80D
		// (set) Token: 0x06008180 RID: 33152 RVA: 0x0021161A File Offset: 0x0020F81A
		[Parameter(Mandatory = false)]
		public bool SipResourceIdentifierRequired
		{
			get
			{
				return this.DataObject.SipResourceIdentifierRequired;
			}
			set
			{
				this.DataObject.SipResourceIdentifierRequired = value;
			}
		}

		// Token: 0x17002838 RID: 10296
		// (get) Token: 0x06008181 RID: 33153 RVA: 0x00211628 File Offset: 0x0020F828
		// (set) Token: 0x06008182 RID: 33154 RVA: 0x00211635 File Offset: 0x0020F835
		[Parameter(Mandatory = false)]
		public string DefaultOutboundCallingLineId
		{
			get
			{
				return this.DataObject.DefaultOutboundCallingLineId;
			}
			set
			{
				this.DataObject.DefaultOutboundCallingLineId = value;
			}
		}

		// Token: 0x17002839 RID: 10297
		// (get) Token: 0x06008183 RID: 33155 RVA: 0x00211643 File Offset: 0x0020F843
		// (set) Token: 0x06008184 RID: 33156 RVA: 0x0021165A File Offset: 0x0020F85A
		[Parameter(Mandatory = false)]
		public bool GenerateUMMailboxPolicy
		{
			get
			{
				return (bool)base.Fields["GenerateUMMailboxPolicy"];
			}
			set
			{
				base.Fields["GenerateUMMailboxPolicy"] = value;
			}
		}

		// Token: 0x1700283A RID: 10298
		// (get) Token: 0x06008185 RID: 33157 RVA: 0x00211672 File Offset: 0x0020F872
		// (set) Token: 0x06008186 RID: 33158 RVA: 0x0021167F File Offset: 0x0020F87F
		[Parameter(Mandatory = true)]
		public string CountryOrRegionCode
		{
			get
			{
				return this.DataObject.CountryOrRegionCode;
			}
			set
			{
				this.DataObject.CountryOrRegionCode = value;
			}
		}

		// Token: 0x1700283B RID: 10299
		// (get) Token: 0x06008187 RID: 33159 RVA: 0x0021168D File Offset: 0x0020F88D
		// (set) Token: 0x06008188 RID: 33160 RVA: 0x0021169A File Offset: 0x0020F89A
		[Parameter(Mandatory = false)]
		public UMGlobalCallRoutingScheme GlobalCallRoutingScheme
		{
			get
			{
				return this.DataObject.GlobalCallRoutingScheme;
			}
			set
			{
				this.DataObject.GlobalCallRoutingScheme = value;
			}
		}

		// Token: 0x1700283C RID: 10300
		// (get) Token: 0x06008189 RID: 33161 RVA: 0x002116A8 File Offset: 0x0020F8A8
		// (set) Token: 0x0600818A RID: 33162 RVA: 0x002116B5 File Offset: 0x0020F8B5
		[Parameter(Mandatory = false)]
		public UMLanguage DefaultLanguage
		{
			get
			{
				return this.DataObject.DefaultLanguage;
			}
			set
			{
				this.DataObject.DefaultLanguage = value;
			}
		}

		// Token: 0x1700283D RID: 10301
		// (get) Token: 0x0600818B RID: 33163 RVA: 0x002116C4 File Offset: 0x0020F8C4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewUMDialPlan(base.Name.ToString(CultureInfo.InvariantCulture), this.NumberOfDigitsInExtension.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x0600818C RID: 33164 RVA: 0x002116FC File Offset: 0x0020F8FC
		protected override IConfigurable PrepareDataObject()
		{
			UMDialPlan umdialPlan = (UMDialPlan)base.PrepareDataObject();
			umdialPlan.SetId((IConfigurationSession)base.DataSession, base.Name);
			return umdialPlan;
		}

		// Token: 0x0600818D RID: 33165 RVA: 0x00211730 File Offset: 0x0020F930
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (string.IsNullOrEmpty(this.CountryOrRegionCode))
				{
					base.WriteError(new InvalidParameterException(Strings.EmptyCountryOrRegionCode), ErrorCategory.InvalidArgument, null);
				}
				if (base.Fields["GenerateUMMailboxPolicy"] == null || (bool)base.Fields["GenerateUMMailboxPolicy"])
				{
					this.defaultPolicyName = this.GetValidPolicyName();
					if (this.defaultPolicyName == null)
					{
						base.WriteError(new DefaultPolicyCreationException(string.Empty), ErrorCategory.InvalidArgument, null);
					}
					else if (this.defaultPolicyName.Length > 64)
					{
						base.WriteError(new DefaultPolicyCreationException(Strings.DefaultPolicyCreationNameTooLong(this.DataObject.Name)), ErrorCategory.InvalidArgument, null);
					}
				}
				if (!string.IsNullOrEmpty(this.DataObject.DefaultOutboundCallingLineId) && !Utils.IsUriValid(this.DataObject.DefaultOutboundCallingLineId, this.DataObject))
				{
					base.WriteError(new InvalidParameterException(Strings.InvalidDefaultOutboundCallingLineId), ErrorCategory.WriteError, this.DataObject);
				}
				if (!this.DataObject.IsModified(UMDialPlanSchema.GlobalCallRoutingScheme))
				{
					if (CommonConstants.UseDataCenterCallRouting)
					{
						this.GlobalCallRoutingScheme = UMGlobalCallRoutingScheme.GatewayGuid;
					}
					else
					{
						this.GlobalCallRoutingScheme = UMGlobalCallRoutingScheme.None;
					}
				}
				if (this.DataObject.IsModified(UMDialPlanSchema.DefaultLanguage) && !Utility.IsUMLanguageAvailable(this.DefaultLanguage))
				{
					base.WriteError(new InvalidParameterException(Strings.DefaultLanguageNotAvailable(this.DefaultLanguage.DisplayName)), ErrorCategory.WriteError, this.DataObject);
				}
				if (!this.DataObject.IsModified(UMDialPlanSchema.VoIPSecurity))
				{
					if (CommonConstants.UseDataCenterCallRouting)
					{
						this.VoIPSecurity = UMVoIPSecurityType.Secured;
					}
					else
					{
						this.VoIPSecurity = UMVoIPSecurityType.Unsecured;
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600818E RID: 33166 RVA: 0x002118D0 File Offset: 0x0020FAD0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.CreateParentContainerIfNeeded(this.DataObject);
			if (CommonConstants.UseDataCenterCallRouting)
			{
				this.DataObject.PhoneContext = base.Name + "." + Guid.NewGuid().ToString("D");
			}
			else
			{
				this.DataObject.PhoneContext = base.Name + "." + ADForest.GetLocalForest().Fqdn;
			}
			if (this.DataObject.SubscriberType == UMSubscriberType.Consumer)
			{
				this.DataObject.CallSomeoneEnabled = false;
				this.DataObject.SendVoiceMsgEnabled = false;
			}
			this.DataObject.AudioCodec = AudioCodecEnum.Mp3;
			UMMailboxPolicy ummailboxPolicy = null;
			if (base.Fields["GenerateUMMailboxPolicy"] == null || (bool)base.Fields["GenerateUMMailboxPolicy"])
			{
				base.DataSession.Save(this.DataObject);
				ummailboxPolicy = this.AutoGeneratePolicy();
			}
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_NewDialPlanCreated, null, new object[]
				{
					base.Name
				});
			}
			else if (ummailboxPolicy != null)
			{
				base.DataSession.Delete(ummailboxPolicy);
				base.DataSession.Delete(this.DataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600818F RID: 33167 RVA: 0x00211A18 File Offset: 0x0020FC18
		private UMMailboxPolicy AutoGeneratePolicy()
		{
			UMMailboxPolicy ummailboxPolicy = new UMMailboxPolicy();
			ummailboxPolicy.UMDialPlan = this.DataObject.Id;
			if (this.DataObject.SubscriberType == UMSubscriberType.Consumer)
			{
				ummailboxPolicy.AllowDialPlanSubscribers = false;
				ummailboxPolicy.AllowExtensions = false;
			}
			ADObjectId descendantId = base.CurrentOrgContainerId.GetDescendantId(new ADObjectId("CN=UM Mailbox Policies", Guid.Empty));
			AdName adName = new AdName("CN", this.defaultPolicyName);
			ADObjectId descendantId2 = descendantId.GetDescendantId(new ADObjectId(adName.ToString(), Guid.Empty));
			ummailboxPolicy.SetId(descendantId2);
			if (base.CurrentOrganizationId != null)
			{
				ummailboxPolicy.OrganizationId = base.CurrentOrganizationId;
			}
			else
			{
				ummailboxPolicy.OrganizationId = base.ExecutingUserOrganizationId;
			}
			ummailboxPolicy.SourceForestPolicyNames.Add(adName.EscapedName);
			base.CreateParentContainerIfNeeded(ummailboxPolicy);
			base.DataSession.Save(ummailboxPolicy);
			return ummailboxPolicy;
		}

		// Token: 0x06008190 RID: 33168 RVA: 0x00211AF0 File Offset: 0x0020FCF0
		private string GetValidPolicyName()
		{
			int num = 0;
			bool flag = false;
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			string text = Strings.DefaultUMMailboxPolicyName(this.DataObject.Name).ToString();
			string text2 = text;
			do
			{
				if (configurationSession.FindMailboxPolicyByName<UMMailboxPolicy>(text2) == null)
				{
					flag = true;
				}
				else
				{
					num++;
					text2 = text + num.ToString(CultureInfo.InvariantCulture);
				}
			}
			while (!flag && num <= 10);
			if (flag)
			{
				return text2;
			}
			return null;
		}

		// Token: 0x04003F27 RID: 16167
		private const string GenerateUMMailboxPolicyField = "GenerateUMMailboxPolicy";

		// Token: 0x04003F28 RID: 16168
		private string defaultPolicyName;
	}
}
