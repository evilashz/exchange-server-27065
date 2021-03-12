using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D35 RID: 3381
	[Cmdlet("New", "UMAutoAttendant", SupportsShouldProcess = true)]
	public class NewUMAutoAttendant : NewMultitenancySystemConfigurationObjectTask<UMAutoAttendant>
	{
		// Token: 0x17002843 RID: 10307
		// (get) Token: 0x060081A4 RID: 33188 RVA: 0x002120C0 File Offset: 0x002102C0
		// (set) Token: 0x060081A5 RID: 33189 RVA: 0x002120CD File Offset: 0x002102CD
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> PilotIdentifierList
		{
			get
			{
				return this.DataObject.PilotIdentifierList;
			}
			set
			{
				this.DataObject.PilotIdentifierList = value;
			}
		}

		// Token: 0x17002844 RID: 10308
		// (get) Token: 0x060081A6 RID: 33190 RVA: 0x002120DB File Offset: 0x002102DB
		// (set) Token: 0x060081A7 RID: 33191 RVA: 0x002120F2 File Offset: 0x002102F2
		[Parameter(Mandatory = true)]
		public UMDialPlanIdParameter UMDialPlan
		{
			get
			{
				return (UMDialPlanIdParameter)base.Fields["UMDialPlan"];
			}
			set
			{
				base.Fields["UMDialPlan"] = value;
			}
		}

		// Token: 0x17002845 RID: 10309
		// (get) Token: 0x060081A8 RID: 33192 RVA: 0x00212105 File Offset: 0x00210305
		// (set) Token: 0x060081A9 RID: 33193 RVA: 0x0021212B File Offset: 0x0021032B
		[Parameter(Mandatory = false)]
		public SwitchParameter SharedUMDialPlan
		{
			get
			{
				return (SwitchParameter)(base.Fields["SharedUMDialPlan"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SharedUMDialPlan"] = value;
			}
		}

		// Token: 0x17002846 RID: 10310
		// (get) Token: 0x060081AA RID: 33194 RVA: 0x00212143 File Offset: 0x00210343
		// (set) Token: 0x060081AB RID: 33195 RVA: 0x0021215A File Offset: 0x0021035A
		[Parameter(Mandatory = false)]
		public StatusEnum Status
		{
			get
			{
				return (StatusEnum)base.Fields["Status"];
			}
			set
			{
				base.Fields["Status"] = value;
			}
		}

		// Token: 0x17002847 RID: 10311
		// (get) Token: 0x060081AC RID: 33196 RVA: 0x00212172 File Offset: 0x00210372
		// (set) Token: 0x060081AD RID: 33197 RVA: 0x00212189 File Offset: 0x00210389
		[Parameter(Mandatory = false)]
		public bool SpeechEnabled
		{
			get
			{
				return (bool)base.Fields["SpeechEnabled"];
			}
			set
			{
				base.Fields["SpeechEnabled"] = value;
			}
		}

		// Token: 0x17002848 RID: 10312
		// (get) Token: 0x060081AE RID: 33198 RVA: 0x002121A1 File Offset: 0x002103A1
		// (set) Token: 0x060081AF RID: 33199 RVA: 0x002121B8 File Offset: 0x002103B8
		[Parameter(Mandatory = false)]
		public UMAutoAttendantIdParameter DTMFFallbackAutoAttendant
		{
			get
			{
				return (UMAutoAttendantIdParameter)base.Fields["DTMFFallbackAutoAttendant"];
			}
			set
			{
				base.Fields["DTMFFallbackAutoAttendant"] = value;
			}
		}

		// Token: 0x17002849 RID: 10313
		// (get) Token: 0x060081B0 RID: 33200 RVA: 0x002121CB File Offset: 0x002103CB
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewUMAutoAttendant(base.Name.ToString(), base.FormatMultiValuedProperty(this.PilotIdentifierList), this.UMDialPlan.ToString());
			}
		}

		// Token: 0x060081B1 RID: 33201 RVA: 0x002121F4 File Offset: 0x002103F4
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || ValidationHelper.IsKnownException(exception);
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x0021220C File Offset: 0x0021040C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			UMAutoAttendant umautoAttendant = (UMAutoAttendant)base.PrepareDataObject();
			umautoAttendant.SetId((IConfigurationSession)base.DataSession, base.Name);
			UMDialPlanIdParameter umdialPlan = this.UMDialPlan;
			UMDialPlan umdialPlan2 = (UMDialPlan)base.GetDataObject<UMDialPlan>(umdialPlan, this.GetDialPlanSession(), null, new LocalizedString?(Strings.NonExistantDialPlan(umdialPlan.ToString())), new LocalizedString?(Strings.MultipleDialplansWithSameId(umdialPlan.ToString())));
			umautoAttendant.SetDialPlan(umdialPlan2.Id);
			this.dialPlan = umdialPlan2;
			if (base.HasErrors)
			{
				return null;
			}
			if (base.Fields.IsModified("DTMFFallbackAutoAttendant"))
			{
				UMAutoAttendantIdParameter dtmffallbackAutoAttendant = this.DTMFFallbackAutoAttendant;
				if (dtmffallbackAutoAttendant != null)
				{
					this.fallbackAA = (UMAutoAttendant)base.GetDataObject<UMAutoAttendant>(dtmffallbackAutoAttendant, base.DataSession, null, new LocalizedString?(Strings.NonExistantAutoAttendant(dtmffallbackAutoAttendant.ToString())), new LocalizedString?(Strings.MultipleAutoAttendantsWithSameId(dtmffallbackAutoAttendant.ToString())));
					umautoAttendant.DTMFFallbackAutoAttendant = this.fallbackAA.Id;
				}
				else
				{
					umautoAttendant.DTMFFallbackAutoAttendant = null;
				}
			}
			TaskLogger.LogExit();
			return umautoAttendant;
		}

		// Token: 0x060081B3 RID: 33203 RVA: 0x00212310 File Offset: 0x00210510
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.Name.Length > 64)
			{
				base.WriteError(new InvalidAutoAttendantException(Strings.AANameTooLong), ErrorCategory.NotSpecified, null);
			}
			LocalizedException ex = ValidationHelper.ValidateDialedNumbers(this.DataObject.PilotIdentifierList, this.dialPlan);
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.NotSpecified, this.DataObject);
			}
			foreach (string text in this.DataObject.PilotIdentifierList)
			{
				UMAutoAttendant umautoAttendant = UMAutoAttendant.FindAutoAttendantByPilotIdentifierAndDialPlan(text, this.DataObject.UMDialPlan);
				if (umautoAttendant != null)
				{
					base.WriteError(new AutoAttendantExistsException(text, this.DataObject.UMDialPlan.Name), ErrorCategory.NotSpecified, null);
				}
			}
			if (this.dialPlan.URIType == UMUriType.SipName && this.DataObject.PilotIdentifierList != null)
			{
				Utility.CheckForPilotIdentifierDuplicates(this.DataObject, this.ConfigurationSession, this.DataObject.PilotIdentifierList, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.DataObject.DTMFFallbackAutoAttendant != null)
			{
				ValidationHelper.ValidateDtmfFallbackAA(this.DataObject, this.dialPlan, this.fallbackAA);
			}
			StatusEnum status = StatusEnum.Disabled;
			if (base.Fields["Status"] != null)
			{
				status = (StatusEnum)base.Fields["Status"];
			}
			this.DataObject.SetStatus(status);
			if (base.Fields["SpeechEnabled"] != null && this.SpeechEnabled)
			{
				this.DataObject.SpeechEnabled = true;
			}
			else
			{
				this.DataObject.SpeechEnabled = false;
			}
			this.DataObject.NameLookupEnabled = true;
			this.DataObject.OperatorExtension = null;
			this.DataObject.InfoAnnouncementEnabled = InfoAnnouncementEnabledEnum.False;
			this.DataObject.InfoAnnouncementFilename = null;
			this.DataObject.CallSomeoneEnabled = true;
			this.DataObject.SendVoiceMsgEnabled = false;
			if (this.dialPlan.SubscriberType == UMSubscriberType.Consumer)
			{
				this.DataObject.ContactScope = DialScopeEnum.GlobalAddressList;
				this.DataObject.AllowDialPlanSubscribers = false;
				this.DataObject.AllowExtensions = false;
			}
			else
			{
				this.DataObject.ContactScope = DialScopeEnum.DialPlan;
				this.DataObject.AllowDialPlanSubscribers = true;
				this.DataObject.AllowExtensions = true;
			}
			this.DataObject.BusinessHoursWelcomeGreetingEnabled = false;
			this.DataObject.BusinessHoursWelcomeGreetingFilename = null;
			this.DataObject.BusinessHoursMainMenuCustomPromptEnabled = false;
			this.DataObject.BusinessHoursMainMenuCustomPromptFilename = null;
			this.DataObject.BusinessHoursTransferToOperatorEnabled = false;
			this.DataObject.AfterHoursWelcomeGreetingEnabled = false;
			this.DataObject.AfterHoursWelcomeGreetingFilename = null;
			this.DataObject.AfterHoursMainMenuCustomPromptEnabled = false;
			this.DataObject.AfterHoursMainMenuCustomPromptFilename = null;
			this.DataObject.AfterHoursTransferToOperatorEnabled = false;
			this.DataObject.TimeZone = ExTimeZone.CurrentTimeZone.Id;
			this.DataObject.Language = UMLanguage.DefaultLanguage;
			TaskLogger.LogExit();
		}

		// Token: 0x060081B4 RID: 33204 RVA: 0x00212608 File Offset: 0x00210808
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.CreateParentContainerIfNeeded(this.DataObject);
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AutoAttendantCreated, null, new object[]
				{
					this.DataObject.Id.DistinguishedName,
					this.DataObject.UMDialPlan
				});
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060081B5 RID: 33205 RVA: 0x00212674 File Offset: 0x00210874
		private IConfigurationSession GetDialPlanSession()
		{
			IConfigurationSession result = (IConfigurationSession)base.DataSession;
			if (this.SharedUMDialPlan)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				result = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 369, "GetDialPlanSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\new_umautoattendant.cs");
			}
			return result;
		}

		// Token: 0x04003F2A RID: 16170
		private const string ParameterSharedUMDialPlan = "SharedUMDialPlan";

		// Token: 0x04003F2B RID: 16171
		private UMDialPlan dialPlan;

		// Token: 0x04003F2C RID: 16172
		private UMAutoAttendant fallbackAA;
	}
}
