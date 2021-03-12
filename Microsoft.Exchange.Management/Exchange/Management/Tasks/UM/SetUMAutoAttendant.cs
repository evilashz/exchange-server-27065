using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D45 RID: 3397
	[Cmdlet("Set", "UMAutoAttendant", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class SetUMAutoAttendant : SetSystemConfigurationObjectTask<UMAutoAttendantIdParameter, UMAutoAttendant>
	{
		// Token: 0x17002873 RID: 10355
		// (get) Token: 0x06008232 RID: 33330 RVA: 0x0021464D File Offset: 0x0021284D
		// (set) Token: 0x06008233 RID: 33331 RVA: 0x00214664 File Offset: 0x00212864
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

		// Token: 0x17002874 RID: 10356
		// (get) Token: 0x06008234 RID: 33332 RVA: 0x00214677 File Offset: 0x00212877
		// (set) Token: 0x06008235 RID: 33333 RVA: 0x0021468E File Offset: 0x0021288E
		[Parameter(Mandatory = false)]
		public AddressListIdParameter ContactAddressList
		{
			get
			{
				return (AddressListIdParameter)base.Fields["ContactAddressList"];
			}
			set
			{
				base.Fields["ContactAddressList"] = value;
			}
		}

		// Token: 0x17002875 RID: 10357
		// (get) Token: 0x06008236 RID: 33334 RVA: 0x002146A1 File Offset: 0x002128A1
		// (set) Token: 0x06008237 RID: 33335 RVA: 0x002146B8 File Offset: 0x002128B8
		[Parameter(Mandatory = false)]
		public OrganizationalUnitIdParameter ContactRecipientContainer
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields["ContactRecipientContainer"];
			}
			set
			{
				base.Fields["ContactRecipientContainer"] = value;
			}
		}

		// Token: 0x17002876 RID: 10358
		// (get) Token: 0x06008238 RID: 33336 RVA: 0x002146CB File Offset: 0x002128CB
		// (set) Token: 0x06008239 RID: 33337 RVA: 0x002146F1 File Offset: 0x002128F1
		[Parameter]
		public SwitchParameter ForceUpgrade
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceUpgrade"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForceUpgrade"] = value;
			}
		}

		// Token: 0x17002877 RID: 10359
		// (get) Token: 0x0600823A RID: 33338 RVA: 0x00214709 File Offset: 0x00212909
		// (set) Token: 0x0600823B RID: 33339 RVA: 0x00214720 File Offset: 0x00212920
		[Parameter(Mandatory = false)]
		public string TimeZone
		{
			get
			{
				return (string)base.Fields["TimeZone"];
			}
			set
			{
				base.Fields["TimeZone"] = value;
			}
		}

		// Token: 0x17002878 RID: 10360
		// (get) Token: 0x0600823C RID: 33340 RVA: 0x00214733 File Offset: 0x00212933
		// (set) Token: 0x0600823D RID: 33341 RVA: 0x0021474A File Offset: 0x0021294A
		[Parameter(Mandatory = false)]
		public UMTimeZone TimeZoneName
		{
			get
			{
				return (UMTimeZone)base.Fields["TimeZoneName"];
			}
			set
			{
				base.Fields["TimeZoneName"] = value;
			}
		}

		// Token: 0x17002879 RID: 10361
		// (get) Token: 0x0600823E RID: 33342 RVA: 0x0021475D File Offset: 0x0021295D
		// (set) Token: 0x0600823F RID: 33343 RVA: 0x00214774 File Offset: 0x00212974
		[Parameter(Mandatory = false)]
		public MailboxIdParameter DefaultMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["DefaultMailbox"];
			}
			set
			{
				base.Fields["DefaultMailbox"] = value;
			}
		}

		// Token: 0x1700287A RID: 10362
		// (get) Token: 0x06008240 RID: 33344 RVA: 0x00214787 File Offset: 0x00212987
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetUMAutoAttendant(this.Identity.ToString());
			}
		}

		// Token: 0x1700287B RID: 10363
		// (get) Token: 0x06008241 RID: 33345 RVA: 0x00214799 File Offset: 0x00212999
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008242 RID: 33346 RVA: 0x0021479C File Offset: 0x0021299C
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return true;
		}

		// Token: 0x06008243 RID: 33347 RVA: 0x0021479F File Offset: 0x0021299F
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || ValidationHelper.IsKnownException(exception);
		}

		// Token: 0x06008244 RID: 33348 RVA: 0x002147B8 File Offset: 0x002129B8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			UMAutoAttendant umautoAttendant = (UMAutoAttendant)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (base.Fields.IsModified("ContactRecipientContainer") && base.Fields.IsModified("ContactAddressList"))
			{
				base.WriteError(new InvalidALParameterException(), ErrorCategory.NotSpecified, null);
				TaskLogger.LogExit();
				return null;
			}
			if (base.Fields.IsModified("TimeZone") && base.Fields.IsModified("TimeZoneName"))
			{
				base.WriteError(new InvalidParameterException(Strings.InvalidTimeZoneParameters), ErrorCategory.NotSpecified, null);
			}
			if (base.Fields.IsModified("ContactRecipientContainer"))
			{
				OrganizationalUnitIdParameter contactRecipientContainer = this.ContactRecipientContainer;
				if (contactRecipientContainer != null)
				{
					bool useConfigNC = this.ConfigurationSession.UseConfigNC;
					this.ConfigurationSession.UseConfigNC = false;
					ADConfigurationObject adconfigurationObject = (ADConfigurationObject)base.GetDataObject<ExchangeOrganizationalUnit>(contactRecipientContainer, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(this.ContactRecipientContainer.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(this.ContactRecipientContainer.ToString())));
					this.ConfigurationSession.UseConfigNC = useConfigNC;
					if (!base.HasErrors)
					{
						umautoAttendant.ContactAddressList = adconfigurationObject.Id;
					}
				}
				else
				{
					umautoAttendant.ContactAddressList = null;
				}
			}
			if (base.Fields.IsModified("ContactAddressList"))
			{
				AddressListIdParameter contactAddressList = this.ContactAddressList;
				if (contactAddressList != null)
				{
					IEnumerable<AddressBookBase> objects = contactAddressList.GetObjects<AddressBookBase>(null, this.ConfigurationSession);
					using (IEnumerator<AddressBookBase> enumerator = objects.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							umautoAttendant.ContactAddressList = (ADObjectId)enumerator.Current.Identity;
						}
						goto IL_193;
					}
				}
				umautoAttendant.ContactAddressList = null;
			}
			IL_193:
			if (base.Fields.IsModified("DTMFFallbackAutoAttendant"))
			{
				UMAutoAttendantIdParameter dtmffallbackAutoAttendant = this.DTMFFallbackAutoAttendant;
				if (dtmffallbackAutoAttendant != null)
				{
					this.fallbackAA = (UMAutoAttendant)base.GetDataObject<UMAutoAttendant>(dtmffallbackAutoAttendant, this.ConfigurationSession, null, new LocalizedString?(Strings.NonExistantAutoAttendant(dtmffallbackAutoAttendant.ToString())), new LocalizedString?(Strings.MultipleAutoAttendantsWithSameId(dtmffallbackAutoAttendant.ToString())));
					umautoAttendant.DTMFFallbackAutoAttendant = this.fallbackAA.Id;
				}
				else
				{
					umautoAttendant.DTMFFallbackAutoAttendant = null;
				}
			}
			if (base.Fields.IsModified("DefaultMailbox"))
			{
				if (this.DefaultMailbox == null)
				{
					umautoAttendant.DefaultMailbox = null;
					umautoAttendant.DefaultMailboxLegacyDN = null;
				}
				else
				{
					IRecipientSession recipientSessionScopedToOrganization = Utility.GetRecipientSessionScopedToOrganization(umautoAttendant.OrganizationId, true);
					LocalizedString value = Strings.InvalidMailbox(this.DefaultMailbox.ToString(), "DefaultMailbox");
					umautoAttendant.DefaultMailbox = (ADUser)base.GetDataObject<ADUser>(this.DefaultMailbox, recipientSessionScopedToOrganization, null, null, new LocalizedString?(value), new LocalizedString?(value));
					umautoAttendant.DefaultMailboxLegacyDN = umautoAttendant.DefaultMailbox.LegacyExchangeDN;
				}
			}
			if (!base.HasErrors)
			{
				if (base.Fields.IsModified("TimeZone"))
				{
					umautoAttendant.TimeZone = this.TimeZone;
				}
				if (base.Fields.IsModified("TimeZoneName"))
				{
					umautoAttendant.TimeZoneName = this.TimeZoneName;
				}
			}
			TaskLogger.LogExit();
			return umautoAttendant;
		}

		// Token: 0x06008245 RID: 33349 RVA: 0x00214AB0 File Offset: 0x00212CB0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				UMDialPlan dialPlan = this.DataObject.GetDialPlan();
				if (dialPlan == null)
				{
					base.WriteError(new DialPlanNotFoundException(this.DataObject.UMDialPlan.Name), ErrorCategory.NotSpecified, null);
				}
				int numberOfDigitsInExtension = dialPlan.NumberOfDigitsInExtension;
				MultiValuedProperty<string> multiValuedProperty = null;
				multiValuedProperty = this.DataObject.PilotIdentifierList;
				if (this.DataObject.IsChanged(UMAutoAttendantSchema.PilotIdentifierList) && multiValuedProperty != null)
				{
					LocalizedException ex = ValidationHelper.ValidateDialedNumbers(this.DataObject.PilotIdentifierList, dialPlan);
					if (ex != null)
					{
						base.WriteError(ex, ErrorCategory.NotSpecified, this.DataObject);
					}
					foreach (string text in this.DataObject.PilotIdentifierList)
					{
						UMAutoAttendant umautoAttendant = UMAutoAttendant.FindAutoAttendantByPilotIdentifierAndDialPlan(text, this.DataObject.UMDialPlan);
						if (umautoAttendant != null && !umautoAttendant.Guid.Equals(this.DataObject.Guid))
						{
							base.WriteError(new AutoAttendantExistsException(text, this.DataObject.UMDialPlan.Name), ErrorCategory.NotSpecified, null);
						}
					}
					if (dialPlan.URIType == UMUriType.SipName)
					{
						Utility.CheckForPilotIdentifierDuplicates(this.DataObject, this.ConfigurationSession, multiValuedProperty, new Task.TaskErrorLoggingDelegate(base.WriteError));
					}
				}
				string timeZone = this.DataObject.TimeZone;
				if (this.DataObject.IsChanged(UMAutoAttendantSchema.BusinessHourFeatures))
				{
					ValidationHelper.ValidateTimeZone(timeZone);
				}
				string property;
				try
				{
					property = UMAutoAttendantSchema.BusinessHoursKeyMapping.ToString();
					MultiValuedProperty<CustomMenuKeyMapping> multiValuedProperty2 = this.DataObject.BusinessHoursKeyMapping;
					if (multiValuedProperty2 != null && multiValuedProperty2.Count > 0)
					{
						bool flag;
						ValidationHelper.ValidateCustomMenu(Strings.BusinessHoursSettings, this.ConfigurationSession, property, multiValuedProperty2, numberOfDigitsInExtension, this.DataObject, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), out flag);
						if (flag)
						{
							this.DataObject.BusinessHoursKeyMapping = multiValuedProperty2;
						}
					}
					property = UMAutoAttendantSchema.AfterHoursKeyMapping.ToString();
					multiValuedProperty2 = this.DataObject.AfterHoursKeyMapping;
					if (multiValuedProperty2 != null && multiValuedProperty2.Count > 0)
					{
						bool flag2;
						ValidationHelper.ValidateCustomMenu(Strings.AfterHoursSettings, this.ConfigurationSession, property, multiValuedProperty2, numberOfDigitsInExtension, this.DataObject, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), out flag2);
						if (flag2)
						{
							this.DataObject.AfterHoursKeyMapping = multiValuedProperty2;
						}
					}
				}
				catch (LocalizedException exception)
				{
					base.WriteError(exception, ErrorCategory.NotSpecified, null);
				}
				bool speechEnabled = this.DataObject.SpeechEnabled;
				StatusEnum status = this.DataObject.Status;
				property = UMAutoAttendantSchema.DTMFFallbackAutoAttendant.ToString();
				if (this.fallbackAA != null)
				{
					ValidationHelper.ValidateDtmfFallbackAA(this.DataObject, dialPlan, this.fallbackAA);
				}
				ADObjectId adobjectId = null;
				property = UMAutoAttendantSchema.AutomaticSpeechRecognitionEnabled.ToString();
				if (this.DataObject.IsChanged(UMAutoAttendantSchema.AutomaticSpeechRecognitionEnabled) && speechEnabled && ValidationHelper.IsFallbackAAInDialPlan(this.ConfigurationSession, this.DataObject, out adobjectId))
				{
					base.WriteError(new InvalidDtmfFallbackAutoAttendantException(Strings.InvalidSpeechEnabledAutoAttendant(adobjectId.ToString())), ErrorCategory.NotSpecified, null);
				}
				property = UMAutoAttendantSchema.Language.ToString();
				if (this.DataObject.IsChanged(UMAutoAttendantSchema.Language))
				{
					UMLanguage language = this.DataObject.Language;
					if (!Utility.IsUMLanguageAvailable(language))
					{
						base.WriteError(new InvalidLanguageIdException(language.ToString()), ErrorCategory.NotSpecified, null);
					}
				}
				bool flag3 = this.IsBusinessHours();
				if (!this.DataObject.NameLookupEnabled && !this.DataObject.CallSomeoneEnabled && ((flag3 && !this.DataObject.BusinessHoursTransferToOperatorEnabled && !this.DataObject.BusinessHoursKeyMappingEnabled) || (!flag3 && !this.DataObject.AfterHoursTransferToOperatorEnabled && !this.DataObject.AfterHoursKeyMappingEnabled)))
				{
					base.WriteError(new InvalidAutoAttendantException(Strings.InvalidMethodToDisableAA), ErrorCategory.NotSpecified, null);
				}
				LocalizedString empty = LocalizedString.Empty;
				if (!DialGroupEntry.ValidateGroup(dialPlan.ConfiguredInCountryOrRegionGroups, this.DataObject.AllowedInCountryOrRegionGroups, true, out empty))
				{
					base.WriteError(new Exception(empty), ErrorCategory.WriteError, this.DataObject);
				}
				if (!DialGroupEntry.ValidateGroup(dialPlan.ConfiguredInternationalGroups, this.DataObject.AllowedInternationalGroups, false, out empty))
				{
					base.WriteError(new Exception(empty), ErrorCategory.WriteError, this.DataObject);
				}
				if (this.DataObject.ForwardCallsToDefaultMailbox && string.IsNullOrEmpty(this.DataObject.DefaultMailboxLegacyDN))
				{
					base.WriteError(new InvalidParameterException(Strings.DefaultMailboxRequiredWhenForwardTrue), ErrorCategory.NotSpecified, null);
				}
				if (this.DataObject.IsModified(UMAutoAttendantSchema.ContactScope) && this.DataObject.ContactScope == DialScopeEnum.DialPlan && dialPlan.SubscriberType == UMSubscriberType.Consumer)
				{
					base.WriteError(new InvalidParameterException(Strings.InvalidAutoAttendantScopeSetting), (ErrorCategory)1000, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008246 RID: 33350 RVA: 0x00214F54 File Offset: 0x00213154
		protected override void InternalProcessRecord()
		{
			if (!base.IsUpgrading || this.ForceUpgrade || base.ShouldContinue(Strings.ShouldUpgradeObjectVersion("UMAutoAttendant")))
			{
				base.InternalProcessRecord();
			}
		}

		// Token: 0x06008247 RID: 33351 RVA: 0x00214F84 File Offset: 0x00213184
		private bool IsBusinessHours()
		{
			bool result = false;
			foreach (ScheduleInterval scheduleInterval in this.DataObject.BusinessHoursSchedule)
			{
				if (scheduleInterval.Length > TimeSpan.Zero)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x04003F3A RID: 16186
		private UMAutoAttendant fallbackAA;
	}
}
