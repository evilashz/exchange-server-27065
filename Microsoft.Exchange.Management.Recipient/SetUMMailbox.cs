using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.SQM;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000F1 RID: 241
	[Cmdlet("Set", "UMMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetUMMailbox : SetUMMailboxBase<MailboxIdParameter, UMMailbox>
	{
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x0004218F File Offset: 0x0004038F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetUMMailbox(this.Identity.ToString());
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000421A1 File Offset: 0x000403A1
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x000421B8 File Offset: 0x000403B8
		[Parameter(Mandatory = false)]
		public string PhoneNumber
		{
			get
			{
				return (string)base.Fields["PhoneNumber"];
			}
			set
			{
				base.Fields["PhoneNumber"] = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x000421CB File Offset: 0x000403CB
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x000421E2 File Offset: 0x000403E2
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AirSyncNumbers
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["AirSyncNumbers"];
			}
			set
			{
				base.Fields["AirSyncNumbers"] = value;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x000421F5 File Offset: 0x000403F5
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x0004221B File Offset: 0x0004041B
		[Parameter(Mandatory = false)]
		public SwitchParameter VerifyGlobalRoutingEntry
		{
			get
			{
				return (SwitchParameter)(base.Fields["VerifyGlobalRoutingEntry"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["VerifyGlobalRoutingEntry"] = value;
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00042234 File Offset: 0x00040434
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, false))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1000, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x000422A4 File Offset: 0x000404A4
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			if (base.UMMailboxPolicy != null)
			{
				this.newMailboxPolicy = (UMMailboxPolicy)base.GetDataObject<UMMailboxPolicy>(base.UMMailboxPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorManagedFolderMailboxPolicyNotFound(base.UMMailboxPolicy.ToString())), new LocalizedString?(Strings.ErrorManagedFolderMailboxPolicyNotUnique(base.UMMailboxPolicy.ToString())));
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00042308 File Offset: 0x00040508
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			if ((aduser.UMEnabledFlags & UMEnabledFlags.UMEnabled) != UMEnabledFlags.UMEnabled)
			{
				base.WriteError(new RecipientTaskException(Strings.MailboxNotUmEnabled(this.Identity.ToString())), (ErrorCategory)1000, aduser);
			}
			if (base.UMMailboxPolicy != null)
			{
				IConfigurationSession configurationSession = this.ConfigurationSession;
				ADObjectId ummailboxPolicy = aduser.UMMailboxPolicy;
				if (ummailboxPolicy != null)
				{
					MailboxPolicyIdParameter id = new MailboxPolicyIdParameter(ummailboxPolicy);
					UMMailboxPolicy ummailboxPolicy2 = (UMMailboxPolicy)base.GetDataObject<UMMailboxPolicy>(id, configurationSession, null, new LocalizedString?(Strings.ErrorManagedFolderMailboxPolicyNotFound(ummailboxPolicy.ToString())), new LocalizedString?(Strings.ErrorManagedFolderMailboxPolicyNotUnique(ummailboxPolicy.ToString())));
					if (!ummailboxPolicy2.UMDialPlan.Equals(this.newMailboxPolicy.UMDialPlan))
					{
						base.WriteError(new RecipientTaskException(Strings.NewPolicyMustBeInTheSameDialPlanAsOldPolicy(ummailboxPolicy2.UMDialPlan.Name)), (ErrorCategory)1000, aduser);
					}
				}
				aduser.UMMailboxPolicy = this.newMailboxPolicy.Id;
			}
			if (base.Fields.IsModified("PhoneNumber"))
			{
				this.SetPhoneNumber(aduser);
			}
			if (base.Fields.IsModified("AirSyncNumbers"))
			{
				this.SetAirSyncNumber(aduser);
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0004242C File Offset: 0x0004062C
		private void SetAirSyncNumber(ADUser user)
		{
			UMMailboxPolicy ummailboxPolicy = this.ReadPolicyObject(user);
			UMDialPlan dialPlan = ummailboxPolicy.GetDialPlan();
			MultiValuedProperty<string> extensionsFromCollection = UMMailbox.GetExtensionsFromCollection(user.UMAddresses, ProxyAddressPrefix.ASUM, dialPlan.PhoneContext);
			Hashtable hashtable = new Hashtable();
			List<string> list = new List<string>();
			foreach (string key in extensionsFromCollection)
			{
				hashtable.Add(key, false);
			}
			foreach (string text in this.AirSyncNumbers)
			{
				if (hashtable.ContainsKey(text))
				{
					hashtable[text] = true;
				}
				else
				{
					list.Add(text);
				}
			}
			foreach (object obj in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!(bool)dictionaryEntry.Value)
				{
					TelephoneNumberProcessStatus status;
					AirSyncUtils.RemoveAirSyncPhoneNumber(user, (string)dictionaryEntry.Key, out status);
					ArgumentException ex = this.HandleResult(status, (string)dictionaryEntry.Key);
					if (ex != null)
					{
						base.WriteError(ex, (ErrorCategory)1001, null);
					}
				}
			}
			foreach (string text2 in list)
			{
				TelephoneNumberProcessStatus status2;
				AirSyncUtils.AddAirSyncPhoneNumber(user, text2, out status2);
				ArgumentException ex2 = this.HandleResult(status2, text2);
				if (ex2 != null)
				{
					base.WriteError(ex2, (ErrorCategory)1001, null);
				}
			}
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00042608 File Offset: 0x00040808
		private void SetPhoneNumber(ADUser user)
		{
			UMMailboxPolicy ummailboxPolicy = this.ReadPolicyObject(user);
			UMDialPlan dialPlan = ummailboxPolicy.GetDialPlan();
			if (dialPlan.URIType != UMUriType.E164 || dialPlan.SubscriberType != UMSubscriberType.Consumer)
			{
				base.WriteError(new ArgumentException(Strings.PhoneNumberAllowedOnlyOnE164ConsumerDialplan, "PhoneNumber"), (ErrorCategory)1000, null);
			}
			if (string.IsNullOrEmpty(dialPlan.CountryOrRegionCode))
			{
				base.WriteError(new ArgumentException(Strings.PhoneNumberAllowedOnlyWithDialplanWithCountryCode, "PhoneNumber"), (ErrorCategory)1000, null);
			}
			if (this.PhoneNumber == string.Empty)
			{
				Utils.UMPopulate(user, null, null, ummailboxPolicy, dialPlan);
				return;
			}
			PhoneNumber phoneNumber;
			if (!Microsoft.Exchange.UM.UMCommon.PhoneNumber.TryParse(this.PhoneNumber, out phoneNumber) || phoneNumber.UriType != UMUriType.TelExtn)
			{
				base.WriteError(new ArgumentException(Strings.PhoneNumberNotANumber(dialPlan.NumberOfDigitsInExtension), this.PhoneNumber), (ErrorCategory)1000, null);
			}
			string sipResourceIdentifier = "+" + dialPlan.CountryOrRegionCode + phoneNumber.Number;
			IRecipientSession tenantLocalRecipientSession = RecipientTaskHelper.GetTenantLocalRecipientSession(user.OrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId);
			LocalizedException ex = null;
			TelephoneNumberProcessStatus telephoneNumberProcessStatus;
			Utils.ValidateExtensionsAndSipResourceIdentifier(tenantLocalRecipientSession, this.ConfigurationSession, CommonConstants.DataCenterADPresent, user, dialPlan, new string[]
			{
				phoneNumber.Number
			}, new string[]
			{
				this.PhoneNumber
			}, sipResourceIdentifier, out ex, out telephoneNumberProcessStatus);
			if (ex != null)
			{
				base.WriteError(ex, (ErrorCategory)1000, null);
			}
			if (telephoneNumberProcessStatus != TelephoneNumberProcessStatus.PhoneNumberAlreadyRegistered)
			{
				Utils.UMPopulate(user, sipResourceIdentifier, new MultiValuedProperty<string>(phoneNumber.Number), ummailboxPolicy, dialPlan);
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00042780 File Offset: 0x00040980
		private UMMailboxPolicy ReadPolicyObject(ADUser user)
		{
			return (UMMailboxPolicy)base.GetDataObject<UMMailboxPolicy>(new MailboxPolicyIdParameter(user.UMMailboxPolicy), this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorManagedFolderMailboxPolicyNotFound(user.UMMailboxPolicy.ToString())), new LocalizedString?(Strings.ErrorManagedFolderMailboxPolicyNotUnique(user.UMMailboxPolicy.ToString())));
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000427D4 File Offset: 0x000409D4
		private ArgumentException HandleResult(TelephoneNumberProcessStatus status, string phoneNumber)
		{
			switch (status)
			{
			case TelephoneNumberProcessStatus.DialPlanNotSupported:
				return new ArgumentException(Strings.PhoneNumberAllowedOnlyOnE164ConsumerDialplanWithCountryCode, "AirSyncNumbers");
			case TelephoneNumberProcessStatus.PhoneNumberAlreadyRegistered:
				return new ArgumentException(Strings.PhoneNumberAlreadyRegistered, phoneNumber);
			case TelephoneNumberProcessStatus.PhoneNumberReachQuota:
				return new ArgumentException(Strings.PhoneNumberReachQuota, phoneNumber);
			case TelephoneNumberProcessStatus.PhoneNumberUsedByOthers:
				return new ArgumentException(Strings.PhoneNumberUsedByOthers, phoneNumber);
			case TelephoneNumberProcessStatus.PhoneNumberInvalidFormat:
				return new ArgumentException(Strings.PhoneNumberIsNotE164, phoneNumber);
			case TelephoneNumberProcessStatus.PhoneNumberInvalidCountryCode:
				return new ArgumentException(Strings.PhoneNumberInvalidCountryCode, phoneNumber);
			case TelephoneNumberProcessStatus.PhoneNumberInvalidLength:
				return new ArgumentException(Strings.PhoneNumberInvalidLength, phoneNumber);
			default:
				return null;
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00042888 File Offset: 0x00040A88
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			UMMailbox ummailbox = UMMailbox.FromDataObject(this.DataObject);
			SmsSqmDataPointHelper.AddNotificationConfigDataPoint(SmsSqmSession.Instance, ummailbox.Id, ummailbox.LegacyExchangeDN, SmsSqmDataPointHelper.TranslateEnumForSqm<UMSMSNotificationOptions>(ummailbox.UMSMSNotificationOption));
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000428C8 File Offset: 0x00040AC8
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return UMMailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x0400037B RID: 891
		private const string VerifyGlobalRoutingEntryName = "VerifyGlobalRoutingEntry";

		// Token: 0x0400037C RID: 892
		private const string PhoneNumberName = "PhoneNumber";

		// Token: 0x0400037D RID: 893
		private const string AirSyncNumbersName = "AirSyncNumbers";

		// Token: 0x0400037E RID: 894
		private UMMailboxPolicy newMailboxPolicy;

		// Token: 0x0400037F RID: 895
		private static TimeSpan VerifyGlobalRoutingEntryTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000380 RID: 896
		private static TimeSpan VerifyGlobalRoutingEntryPollingInterval = TimeSpan.FromSeconds(5.0);
	}
}
