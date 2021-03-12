using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D40 RID: 3392
	[Cmdlet("Set", "UMDialPlan", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class SetUMDialPlan : SetSystemConfigurationObjectTask<UMDialPlanIdParameter, UMDialPlan>
	{
		// Token: 0x1700285E RID: 10334
		// (get) Token: 0x060081F9 RID: 33273 RVA: 0x002137A7 File Offset: 0x002119A7
		// (set) Token: 0x060081FA RID: 33274 RVA: 0x002137BE File Offset: 0x002119BE
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

		// Token: 0x1700285F RID: 10335
		// (get) Token: 0x060081FB RID: 33275 RVA: 0x002137D1 File Offset: 0x002119D1
		// (set) Token: 0x060081FC RID: 33276 RVA: 0x002137E8 File Offset: 0x002119E8
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

		// Token: 0x17002860 RID: 10336
		// (get) Token: 0x060081FD RID: 33277 RVA: 0x002137FB File Offset: 0x002119FB
		// (set) Token: 0x060081FE RID: 33278 RVA: 0x00213812 File Offset: 0x00211A12
		[Parameter(Mandatory = false)]
		public UMAutoAttendantIdParameter UMAutoAttendant
		{
			get
			{
				return (UMAutoAttendantIdParameter)base.Fields["UMAutoAttendant"];
			}
			set
			{
				base.Fields["UMAutoAttendant"] = value;
			}
		}

		// Token: 0x17002861 RID: 10337
		// (get) Token: 0x060081FF RID: 33279 RVA: 0x00213825 File Offset: 0x00211A25
		// (set) Token: 0x06008200 RID: 33280 RVA: 0x0021383C File Offset: 0x00211A3C
		[Parameter(Mandatory = false)]
		public string CountryOrRegionCode
		{
			get
			{
				return (string)base.Fields["CountryOrRegionCode"];
			}
			set
			{
				base.Fields["CountryOrRegionCode"] = value;
			}
		}

		// Token: 0x17002862 RID: 10338
		// (get) Token: 0x06008201 RID: 33281 RVA: 0x0021384F File Offset: 0x00211A4F
		// (set) Token: 0x06008202 RID: 33282 RVA: 0x00213875 File Offset: 0x00211A75
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

		// Token: 0x17002863 RID: 10339
		// (get) Token: 0x06008203 RID: 33283 RVA: 0x0021388D File Offset: 0x00211A8D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetUMDialPlan(this.Identity.ToString());
			}
		}

		// Token: 0x17002864 RID: 10340
		// (get) Token: 0x06008204 RID: 33284 RVA: 0x0021389F File Offset: 0x00211A9F
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008205 RID: 33285 RVA: 0x002138A2 File Offset: 0x00211AA2
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return true;
		}

		// Token: 0x06008206 RID: 33286 RVA: 0x002138A8 File Offset: 0x00211AA8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			UMDialPlan umdialPlan = (UMDialPlan)base.PrepareDataObject();
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
			if (base.Fields.IsModified("CountryOrRegionCode"))
			{
				if (string.IsNullOrEmpty(this.CountryOrRegionCode))
				{
					base.WriteError(new InvalidParameterException(Strings.EmptyCountryOrRegionCode), ErrorCategory.InvalidArgument, null);
				}
				else
				{
					umdialPlan.CountryOrRegionCode = this.CountryOrRegionCode;
				}
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
						umdialPlan.ContactAddressList = adconfigurationObject.Id;
					}
				}
				else
				{
					umdialPlan.ContactAddressList = null;
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
							umdialPlan.ContactAddressList = (ADObjectId)enumerator.Current.Identity;
						}
						goto IL_19C;
					}
				}
				umdialPlan.ContactAddressList = null;
			}
			IL_19C:
			if (umdialPlan.ContactScope != CallSomeoneScopeEnum.AddressList)
			{
				umdialPlan.ContactAddressList = null;
			}
			if (base.Fields.IsModified("UMAutoAttendant"))
			{
				UMAutoAttendantIdParameter umautoAttendant = this.UMAutoAttendant;
				if (umautoAttendant != null)
				{
					UMAutoAttendant umautoAttendant2 = (UMAutoAttendant)base.GetDataObject<UMAutoAttendant>(umautoAttendant, this.ConfigurationSession, null, new LocalizedString?(Strings.NonExistantAutoAttendant(umautoAttendant.ToString())), new LocalizedString?(Strings.MultipleAutoAttendantsWithSameId(umautoAttendant.ToString())));
					if (!base.HasErrors)
					{
						umdialPlan.UMAutoAttendant = umautoAttendant2.Id;
					}
				}
				else
				{
					umdialPlan.UMAutoAttendant = null;
				}
			}
			TaskLogger.LogExit();
			return umdialPlan;
		}

		// Token: 0x06008207 RID: 33287 RVA: 0x00213AEC File Offset: 0x00211CEC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			LocalizedString empty = LocalizedString.Empty;
			if (!DialGroupEntry.ValidateGroup(this.DataObject.ConfiguredInCountryOrRegionGroups, this.DataObject.AllowedInCountryOrRegionGroups, true, out empty))
			{
				base.WriteError(new Exception(empty), ErrorCategory.WriteError, this.DataObject);
			}
			if (!DialGroupEntry.ValidateGroup(this.DataObject.ConfiguredInternationalGroups, this.DataObject.AllowedInternationalGroups, false, out empty))
			{
				base.WriteError(new Exception(empty), ErrorCategory.WriteError, this.DataObject);
			}
			if (!string.IsNullOrEmpty(this.DataObject.DefaultOutboundCallingLineId) && !Utils.IsUriValid(this.DataObject.DefaultOutboundCallingLineId, this.DataObject))
			{
				base.WriteError(new InvalidParameterException(Strings.InvalidDefaultOutboundCallingLineId), ErrorCategory.WriteError, this.DataObject);
			}
			if (this.DataObject.IsModified(UMDialPlanSchema.DefaultLanguage) && !Utility.IsUMLanguageAvailable(this.DataObject.DefaultLanguage))
			{
				base.WriteError(new InvalidParameterException(Strings.DefaultLanguageNotAvailable(this.DataObject.DefaultLanguage.DisplayName)), ErrorCategory.WriteError, this.DataObject);
			}
			MultiValuedProperty<string> pilotIdentifierList = this.DataObject.PilotIdentifierList;
			if (this.DataObject.IsChanged(UMDialPlanSchema.PilotIdentifierList) && pilotIdentifierList != null)
			{
				LocalizedException ex = ValidationHelper.ValidateE164Entries(this.DataObject, this.DataObject.PilotIdentifierList);
				if (ex != null)
				{
					base.WriteError(ex, ErrorCategory.NotSpecified, this.DataObject);
				}
				if (this.DataObject.URIType == UMUriType.SipName)
				{
					Utility.CheckForPilotIdentifierDuplicates(this.DataObject, this.ConfigurationSession, pilotIdentifierList, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008208 RID: 33288 RVA: 0x00213C8A File Offset: 0x00211E8A
		protected override void InternalProcessRecord()
		{
			if (!base.IsUpgrading || this.ForceUpgrade || base.ShouldContinue(Strings.ShouldUpgradeObjectVersion("UMDialPlan")))
			{
				base.InternalProcessRecord();
			}
		}

		// Token: 0x04003F38 RID: 16184
		private const string CountryOrRegionCodeName = "CountryOrRegionCode";
	}
}
