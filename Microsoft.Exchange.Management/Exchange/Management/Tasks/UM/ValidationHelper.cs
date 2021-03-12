using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D66 RID: 3430
	public class ValidationHelper
	{
		// Token: 0x06008385 RID: 33669 RVA: 0x002192C0 File Offset: 0x002174C0
		public static LocalizedException ValidateE164Entries(ADConfigurationObject dataObject, MultiValuedProperty<string> e164NumberList)
		{
			LocalizedException result = null;
			if (e164NumberList == null || e164NumberList.Count == 0)
			{
				return null;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>(e164NumberList.Count);
			for (int i = 0; i < e164NumberList.Count; i++)
			{
				string text = e164NumberList[i];
				if (!Utils.IsUriValid(text, UMUriType.E164))
				{
					result = new InvalidUMPilotIdentifierListEntry(text);
					break;
				}
				if (dictionary.ContainsKey(text))
				{
					result = new DuplicateE164PilotIdentifierListEntryException(dataObject.Name);
					break;
				}
				dictionary[text] = 1;
			}
			return result;
		}

		// Token: 0x06008386 RID: 33670 RVA: 0x00219334 File Offset: 0x00217534
		public static LocalizedException ValidateDialedNumbers(MultiValuedProperty<string> dialedNumbers, UMDialPlan dialPlan)
		{
			int numberOfDigitsInExtension = dialPlan.NumberOfDigitsInExtension;
			UMUriType uritype = dialPlan.URIType;
			LocalizedException ex = null;
			foreach (string text in dialedNumbers)
			{
				switch (uritype)
				{
				case UMUriType.TelExtn:
					try
					{
						ValidationHelper.ValidateExtension(UMAutoAttendantSchema.PilotIdentifierList.ToString(), text, numberOfDigitsInExtension);
					}
					catch (InvalidPilotIdentiferException ex2)
					{
						ex = ex2;
					}
					catch (NumericArgumentLengthInvalidException ex3)
					{
						ex = ex3;
					}
					break;
				case UMUriType.E164:
					if (!Utils.IsUriValid(text, UMUriType.E164))
					{
						ex = new InvalidPilotIdentiferException(Strings.ErrorUMInvalidE164AddressFormat(text));
					}
					break;
				case UMUriType.SipName:
					ex = ValidationHelper.ValidateE164Entries(dialPlan, dialedNumbers);
					break;
				}
				if (ex != null)
				{
					break;
				}
			}
			return ex;
		}

		// Token: 0x06008387 RID: 33671 RVA: 0x00219404 File Offset: 0x00217604
		public static void ValidateExtension(string property, string extension, int numberOfDigitsInExtension)
		{
			if (!ValidationHelper.numberRegex.IsMatch(extension))
			{
				throw new InvalidPilotIdentiferException(Strings.ErrorUMInvalidExtensionFormat(extension));
			}
			if (extension.Length < numberOfDigitsInExtension)
			{
				throw new NumericArgumentLengthInvalidException(extension, property, numberOfDigitsInExtension);
			}
		}

		// Token: 0x06008388 RID: 33672 RVA: 0x00219434 File Offset: 0x00217634
		public static void ValidateTimeZone(string timeZoneKeyName)
		{
			ExTimeZone exTimeZone = null;
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(timeZoneKeyName, out exTimeZone))
			{
				throw new InvalidTimeZoneException(timeZoneKeyName);
			}
		}

		// Token: 0x06008389 RID: 33673 RVA: 0x0021945C File Offset: 0x0021765C
		internal static void ValidateCustomMenu(LocalizedString setting, IConfigurationSession session, string property, MultiValuedProperty<CustomMenuKeyMapping> customMenu, int numberOfDigitsInExtension, UMAutoAttendant containingAutoAttendant, DataAccessHelper.GetDataObjectDelegate getUniqueObject, out bool serializeAgain)
		{
			serializeAgain = false;
			new List<string>();
			IRecipientSession recipientSessionScopedToOrganization = Utility.GetRecipientSessionScopedToOrganization(containingAutoAttendant.OrganizationId, true);
			foreach (CustomMenuKeyMapping customMenuKeyMapping in customMenu)
			{
				if (!string.IsNullOrEmpty(customMenuKeyMapping.AutoAttendantName))
				{
					ValidationHelper.ValidateLinkedAutoAttendant(session, customMenuKeyMapping.AutoAttendantName, containingAutoAttendant.Status == StatusEnum.Enabled, containingAutoAttendant);
				}
				string text = Utils.TrimSpaces(customMenuKeyMapping.PromptFileName);
				if (text != null)
				{
					ValidationHelper.ValidateWavFile(text);
				}
				if (!string.IsNullOrEmpty(customMenuKeyMapping.LeaveVoicemailFor))
				{
					string legacyDNToUseForLeaveVoicemailFor;
					ValidationHelper.ValidateMailbox(setting, customMenuKeyMapping.LeaveVoicemailFor, containingAutoAttendant.UMDialPlan, recipientSessionScopedToOrganization, getUniqueObject, out legacyDNToUseForLeaveVoicemailFor);
					customMenuKeyMapping.LegacyDNToUseForLeaveVoicemailFor = legacyDNToUseForLeaveVoicemailFor;
					serializeAgain = true;
				}
				if (!string.IsNullOrEmpty(customMenuKeyMapping.TransferToMailbox))
				{
					string legacyDNToUseForTransferToMailbox;
					ValidationHelper.ValidateMailbox(setting, customMenuKeyMapping.TransferToMailbox, containingAutoAttendant.UMDialPlan, recipientSessionScopedToOrganization, getUniqueObject, out legacyDNToUseForTransferToMailbox);
					customMenuKeyMapping.LegacyDNToUseForTransferToMailbox = legacyDNToUseForTransferToMailbox;
					serializeAgain = true;
				}
			}
			CustomMenuKeyMapping[] array = customMenu.ToArray();
			Dictionary<string, int> dictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			Dictionary<CustomMenuKey, bool> dictionary2 = new Dictionary<CustomMenuKey, bool>();
			for (int i = 0; i < array.Length; i++)
			{
				CustomMenuKeyMapping customMenuKeyMapping2 = array[i];
				try
				{
					dictionary.Add(customMenuKeyMapping2.Description, i);
				}
				catch (ArgumentException)
				{
					throw new InvalidCustomMenuException(Strings.DuplicateMenuName(customMenuKeyMapping2.Description));
				}
				if (!string.IsNullOrEmpty(customMenuKeyMapping2.AsrPhrases) && customMenuKeyMapping2.AsrPhrases.Length > 256)
				{
					throw new InvalidCustomMenuException(Strings.MaxAsrPhraseLengthExceeded(customMenuKeyMapping2.Description));
				}
				string[] asrPhraseList = customMenuKeyMapping2.AsrPhraseList;
				if (asrPhraseList != null)
				{
					if (asrPhraseList.Length > 9)
					{
						throw new InvalidCustomMenuException(Strings.MaxAsrPhraseCountExceeded(customMenuKeyMapping2.Description));
					}
					for (int j = 0; j < asrPhraseList.Length; j++)
					{
						if (string.IsNullOrEmpty(asrPhraseList[j]))
						{
							throw new InvalidCustomMenuException(Strings.EmptyASRPhrase(customMenuKeyMapping2.Description));
						}
						try
						{
							dictionary.Add(asrPhraseList[j], -1);
						}
						catch (ArgumentException)
						{
							if (dictionary[asrPhraseList[j]] != i)
							{
								throw new InvalidCustomMenuException(Strings.DuplicateASRPhrase(asrPhraseList[j]));
							}
						}
					}
				}
				try
				{
					if (customMenuKeyMapping2.MappedKey != CustomMenuKey.NotSpecified)
					{
						dictionary2.Add(customMenuKeyMapping2.MappedKey, true);
					}
				}
				catch (ArgumentException)
				{
					throw new InvalidCustomMenuException(Strings.DuplicateKeys(customMenuKeyMapping2.Key));
				}
			}
		}

		// Token: 0x0600838A RID: 33674 RVA: 0x002196D0 File Offset: 0x002178D0
		public static void ValidateWavFile(string file)
		{
			string text = Utils.TrimSpaces(file);
			if (text == null)
			{
				throw new ArgumentNullException("file");
			}
			string extension = Path.GetExtension(text);
			if (!string.Equals(extension, ".wav", StringComparison.OrdinalIgnoreCase) && !string.Equals(extension, ".wma", StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidParameterException(Strings.InvalidAAFileExtension(text));
			}
		}

		// Token: 0x0600838B RID: 33675 RVA: 0x00219724 File Offset: 0x00217924
		public static void ValidateLinkedAutoAttendant(IConfigDataProvider session, string autoAttendantName, bool checkEnabled, UMAutoAttendant referringAutoAttendant)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (autoAttendantName == null)
			{
				throw new ArgumentNullException("autoAttendantName");
			}
			if (referringAutoAttendant == null)
			{
				throw new ArgumentNullException("referringAutoAttendant");
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, autoAttendantName),
				new ComparisonFilter(ComparisonOperator.Equal, UMAutoAttendantSchema.UMDialPlan, referringAutoAttendant.UMDialPlan)
			});
			UMAutoAttendant umautoAttendant = null;
			UMAutoAttendant[] array = (UMAutoAttendant[])session.Find<UMAutoAttendant>(filter, referringAutoAttendant.Id.Parent, false, null);
			if (array != null && array.Length == 1)
			{
				umautoAttendant = array[0];
			}
			if (umautoAttendant == null)
			{
				throw new InvalidAutoAttendantException(Strings.InvalidAutoAttendantInDialPlan(autoAttendantName, referringAutoAttendant.UMDialPlan.Name));
			}
			if (checkEnabled && umautoAttendant.Status != StatusEnum.Enabled)
			{
				throw new InvalidAutoAttendantException(Strings.DisabledLinkedAutoAttendant(autoAttendantName, referringAutoAttendant.Id.ToString()));
			}
		}

		// Token: 0x0600838C RID: 33676 RVA: 0x002197F4 File Offset: 0x002179F4
		public static bool IsFallbackAAInDialPlan(IConfigDataProvider dataSession, UMAutoAttendant dataObject, out ADObjectId targetAA)
		{
			bool result = false;
			targetAA = null;
			IEnumerable<UMAutoAttendant> autoAttendantsInSameDialPlan = ValidationHelper.GetAutoAttendantsInSameDialPlan(dataSession, dataObject, dataObject.UMDialPlan);
			if (autoAttendantsInSameDialPlan == null)
			{
				return false;
			}
			foreach (UMAutoAttendant umautoAttendant in autoAttendantsInSameDialPlan)
			{
				if (!umautoAttendant.Id.ObjectGuid.Equals(dataObject.Id.ObjectGuid) && umautoAttendant.DTMFFallbackAutoAttendant != null && umautoAttendant.DTMFFallbackAutoAttendant.ObjectGuid.Equals(dataObject.Guid))
				{
					result = true;
					targetAA = umautoAttendant.Id;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600838D RID: 33677 RVA: 0x002198A0 File Offset: 0x00217AA0
		public static void ValidateDtmfFallbackAA(UMAutoAttendant dataObject, UMDialPlan aaDialPlan, UMAutoAttendant dtmfFallbackAA)
		{
			if (dtmfFallbackAA.Guid.Equals(dataObject.Guid))
			{
				throw new InvalidDtmfFallbackAutoAttendantException(Strings.InvalidDtmfFallbackAutoAttendantSelf(dtmfFallbackAA.Identity.ToString()));
			}
			if (!dtmfFallbackAA.UMDialPlan.ObjectGuid.Equals(aaDialPlan.Guid))
			{
				throw new InvalidDtmfFallbackAutoAttendantException(Strings.InvalidDtmfFallbackAutoAttendantDialPlan(dtmfFallbackAA.Identity.ToString()));
			}
			if (dtmfFallbackAA.SpeechEnabled)
			{
				throw new InvalidDtmfFallbackAutoAttendantException(Strings.InvalidDtmfFallbackAutoAttendant(dtmfFallbackAA.Identity.ToString()));
			}
			if (dataObject.SpeechEnabled && dataObject.Status == StatusEnum.Enabled && dtmfFallbackAA.Status == StatusEnum.Disabled)
			{
				throw new InvalidDtmfFallbackAutoAttendantException(Strings.InvalidDtmfFallbackAutoAttendant_Disabled(dtmfFallbackAA.Identity.ToString()));
			}
		}

		// Token: 0x0600838E RID: 33678 RVA: 0x00219958 File Offset: 0x00217B58
		public static bool IsKnownException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			return exception is NumericArgumentLengthInvalidException || exception is InvalidPilotIdentiferException || exception is InvalidTimeZoneException || exception is InvalidCustomMenuException || exception is InvalidParameterException || exception is InvalidAutoAttendantException || exception is InvalidDtmfFallbackAutoAttendantException || exception is DefaultAutoAttendantInDialPlanException;
		}

		// Token: 0x0600838F RID: 33679 RVA: 0x002199C4 File Offset: 0x00217BC4
		internal static void ValidateDisabledAA(IConfigDataProvider dataSession, UMDialPlan dialPlanConfig, UMAutoAttendant disabledAutoAttendant)
		{
			if (dialPlanConfig.ContactScope == CallSomeoneScopeEnum.AutoAttendantLink)
			{
				ADObjectId umautoAttendant = dialPlanConfig.UMAutoAttendant;
				if (umautoAttendant != null && umautoAttendant.ObjectGuid.Equals(disabledAutoAttendant.Guid))
				{
					throw new DefaultAutoAttendantInDialPlanException(dialPlanConfig.Id.ToString());
				}
			}
			ValidationHelper.CheckLinkWithOtherAAsInDialPlan(dataSession, disabledAutoAttendant);
		}

		// Token: 0x06008390 RID: 33680 RVA: 0x00219A14 File Offset: 0x00217C14
		private static void CheckLinkWithOtherAAsInDialPlan(IConfigDataProvider dataSession, UMAutoAttendant dataObject)
		{
			IEnumerable<UMAutoAttendant> autoAttendantsInSameDialPlan = ValidationHelper.GetAutoAttendantsInSameDialPlan(dataSession, dataObject, dataObject.UMDialPlan);
			if (autoAttendantsInSameDialPlan == null)
			{
				return;
			}
			foreach (UMAutoAttendant umautoAttendant in autoAttendantsInSameDialPlan)
			{
				if (umautoAttendant != null && !umautoAttendant.Guid.Equals(dataObject.Guid))
				{
					if (umautoAttendant.DTMFFallbackAutoAttendant != null && umautoAttendant.DTMFFallbackAutoAttendant.ObjectGuid.Equals(dataObject.Guid))
					{
						throw new InvalidDtmfFallbackAutoAttendantException(Strings.CannotDisableAutoAttendant(umautoAttendant.Id.ToString()));
					}
					if (umautoAttendant.BusinessHoursKeyMapping != null && umautoAttendant.BusinessHoursKeyMapping.Count > 0)
					{
						foreach (CustomMenuKeyMapping customMenuKeyMapping in umautoAttendant.BusinessHoursKeyMapping)
						{
							if (!string.IsNullOrEmpty(customMenuKeyMapping.AutoAttendantName) && string.Equals(dataObject.Name, customMenuKeyMapping.AutoAttendantName, StringComparison.OrdinalIgnoreCase))
							{
								throw new InvalidDtmfFallbackAutoAttendantException(Strings.CannotDisableAutoAttendant_KeyMapping(umautoAttendant.Id.ToString()));
							}
						}
					}
					if (umautoAttendant.AfterHoursKeyMapping != null && umautoAttendant.AfterHoursKeyMapping.Count > 0)
					{
						foreach (CustomMenuKeyMapping customMenuKeyMapping2 in umautoAttendant.AfterHoursKeyMapping)
						{
							if (!string.IsNullOrEmpty(customMenuKeyMapping2.AutoAttendantName) && string.Equals(dataObject.Name, customMenuKeyMapping2.AutoAttendantName, StringComparison.OrdinalIgnoreCase))
							{
								throw new InvalidDtmfFallbackAutoAttendantException(Strings.CannotDisableAutoAttendant_KeyMapping(umautoAttendant.Id.ToString()));
							}
						}
					}
				}
			}
		}

		// Token: 0x06008391 RID: 33681 RVA: 0x00219C04 File Offset: 0x00217E04
		private static IEnumerable<UMAutoAttendant> GetAutoAttendantsInSameDialPlan(IConfigDataProvider session, UMAutoAttendant autoAttendant, ADObjectId dialPlanId)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (autoAttendant == null)
			{
				throw new ArgumentNullException("autoAttendant");
			}
			if (dialPlanId == null)
			{
				throw new ArgumentNullException("dialPlanId");
			}
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, UMAutoAttendantSchema.UMDialPlan, dialPlanId);
			ADObjectId parent = autoAttendant.Id.Parent;
			return session.FindPaged<UMAutoAttendant>(filter, parent, false, null, 0);
		}

		// Token: 0x06008392 RID: 33682 RVA: 0x00219C60 File Offset: 0x00217E60
		private static void ValidateMailbox(LocalizedString setting, string mailbox, ADObjectId dialPlanId, IRecipientSession recipSession, DataAccessHelper.GetDataObjectDelegate getUniqueObject, out string legacyDN)
		{
			legacyDN = null;
			ADRecipient adrecipient = (ADRecipient)getUniqueObject(new MailboxIdParameter(mailbox), recipSession, null, null, new LocalizedString?(Strings.InvalidMailbox(mailbox, setting)), new LocalizedString?(Strings.InvalidMailbox(mailbox, setting)));
			ADUser aduser = adrecipient as ADUser;
			if (!Utils.IsUserUMEnabledInGivenDialplan(aduser, dialPlanId))
			{
				throw new InvalidCustomMenuException(Strings.InvalidMailbox(mailbox, setting));
			}
			legacyDN = aduser.LegacyExchangeDN;
		}

		// Token: 0x04003FCA RID: 16330
		private static Regex numberRegex = new Regex("^\\d+$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
	}
}
