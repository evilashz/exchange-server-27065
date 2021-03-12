using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;
using Microsoft.Exchange.UM.UMCommon.Exceptions;
using Microsoft.Exchange.UM.UMCommon.MessageContent;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200018C RID: 396
	internal static class Utils
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0002E988 File Offset: 0x0002CB88
		public static bool RunningInTestMode
		{
			get
			{
				if (Utils.runningInTestMode == null)
				{
					lock (Utils.lockObj)
					{
						if (Utils.runningInTestMode == null)
						{
							bool value = false;
							using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole", false))
							{
								if (registryKey != null)
								{
									string value2 = registryKey.GetValue("TestMode", "false") as string;
									bool.TryParse(value2, out value);
								}
							}
							Utils.runningInTestMode = new bool?(value);
						}
					}
				}
				return Utils.runningInTestMode.Value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002EA40 File Offset: 0x0002CC40
		public static bool OverrideGrammar
		{
			get
			{
				if (Utils.overrideGrammar == null)
				{
					Utils.overrideGrammar = new bool?(true);
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole", false))
					{
						if (registryKey != null)
						{
							string value = registryKey.GetValue("OverrideGrammar", "true") as string;
							bool value2 = true;
							bool.TryParse(value, out value2);
							Utils.overrideGrammar = new bool?(value2);
						}
					}
				}
				return Utils.overrideGrammar.Value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0002EACC File Offset: 0x0002CCCC
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x0002EAE4 File Offset: 0x0002CCE4
		internal static string UMTempPath
		{
			get
			{
				if (Utils.tempdirPath == null)
				{
					Utils.tempdirPath = Path.GetTempPath();
				}
				return Utils.tempdirPath;
			}
			set
			{
				Utils.tempdirPath = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0002EAEC File Offset: 0x0002CCEC
		internal static string TempPath
		{
			get
			{
				return Path.Combine(Utils.GetExchangeDirectory(), "UnifiedMessaging\\temp");
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002EAFD File Offset: 0x0002CCFD
		internal static string VoiceMailFilePath
		{
			get
			{
				return Path.Combine(Utils.GetExchangeDirectory(), "UnifiedMessaging\\voicemail");
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0002EB0E File Offset: 0x0002CD0E
		internal static string UMBadMailFilePath
		{
			get
			{
				return Path.Combine(Utils.GetExchangeDirectory(), "UnifiedMessaging\\badvoicemail");
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002EB1F File Offset: 0x0002CD1F
		public static bool IsUriValid(string uri, UMDialPlan dialPlan)
		{
			return Utils.IsUriValid(uri, dialPlan.URIType, dialPlan.NumberOfDigitsInExtension);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002EB34 File Offset: 0x0002CD34
		public static bool TryDiskOperation(Action operation, Action<Exception> exceptionHandler)
		{
			Exception ex = null;
			try
			{
				operation();
			}
			catch (UnauthorizedAccessException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				exceptionHandler(ex);
				return false;
			}
			return true;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002EB90 File Offset: 0x0002CD90
		public static bool IsUriValid(string uri, UMUriType uriType, int numDPExtnDigits)
		{
			switch (uriType)
			{
			case UMUriType.TelExtn:
				break;
			case UMUriType.E164:
				return !string.IsNullOrEmpty(uri) && uri.Length != 1 && uri[0] == '+' && Utils.IsNumber(uri.Substring(1));
			case UMUriType.SipName:
				try
				{
					return SmtpAddress.IsValidSmtpAddress(uri);
				}
				catch (ArgumentNullException)
				{
					return false;
				}
				break;
			default:
				return false;
			}
			return !string.IsNullOrEmpty(uri) && uri.Length == numDPExtnDigits && Utils.IsNumber(uri);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002EC20 File Offset: 0x0002CE20
		public static MultiValuedProperty<UMLanguage> ComputeUnionOfUmServerLanguages()
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.UM.UMDataCenterLanguages.Enabled)
			{
				return UMLanguage.Datacenterlanguages;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "ComputeUnionOfUmServerLanguages: Starting Computation of Union of languages", new object[0]);
			MultiValuedProperty<UMLanguage> multiValuedProperty = new MultiValuedProperty<UMLanguage>();
			ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
			IEnumerable<Server> allUMServers = adtopologyLookup.GetAllUMServers();
			if (allUMServers != null)
			{
				foreach (Server dataObject in allUMServers)
				{
					UMServer umserver = new UMServer(dataObject);
					foreach (UMLanguage item in umserver.Languages)
					{
						if (!multiValuedProperty.Contains(item))
						{
							multiValuedProperty.Add(item);
						}
					}
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "ComputeUnionOfUmServerLanguages: Finished Computation of Union of languages", new object[0]);
			return multiValuedProperty;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002ED34 File Offset: 0x0002CF34
		public static void ValidateExtensionsAndSipResourceIdentifier(IRecipientSession tenantLocalRecipientSession, IConfigurationSession tenantLocalConfigSession, bool isDatacenter, ADUser adUser, UMDialPlan dialPlan, string[] extensions, string[] userFriendlyExtensions, string sipResourceIdentifier, out LocalizedException validationError, out TelephoneNumberProcessStatus status)
		{
			validationError = null;
			status = TelephoneNumberProcessStatus.Success;
			switch (dialPlan.URIType)
			{
			case UMUriType.TelExtn:
				if (!string.IsNullOrEmpty(sipResourceIdentifier))
				{
					validationError = new SIPResourceIdNotNeededException();
					return;
				}
				if (extensions == null || extensions.Length == 0)
				{
					validationError = new CouldNotGenerateExtensionException();
					return;
				}
				break;
			case UMUriType.E164:
				if (string.IsNullOrEmpty(sipResourceIdentifier) && dialPlan.SipResourceIdentifierRequired)
				{
					validationError = new E164ResourceIdNeededException();
					return;
				}
				if (extensions == null || extensions.Length == 0)
				{
					validationError = new CouldNotGenerateExtensionException();
					return;
				}
				if (sipResourceIdentifier != null && !Utils.IsUriValid(sipResourceIdentifier, UMUriType.E164))
				{
					validationError = new InvalidE164ResourceIdException();
					return;
				}
				break;
			case UMUriType.SipName:
				if (string.IsNullOrEmpty(sipResourceIdentifier) || extensions == null || extensions.Length == 0)
				{
					validationError = new SipResourceIdAndExtensionsNeededException();
					return;
				}
				if (!Utils.IsUriValid(sipResourceIdentifier, UMUriType.SipName))
				{
					validationError = new InvalidSipNameResourceIdException();
					return;
				}
				break;
			}
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromExistingSession(tenantLocalRecipientSession, isDatacenter);
			IADSystemConfigurationLookup tenantConfigLookup = ADSystemConfigurationLookupFactory.CreateFromExistingSession(tenantLocalConfigSession, isDatacenter);
			for (int i = 0; i < extensions.Length; i++)
			{
				string text = extensions[i];
				string s = (userFriendlyExtensions != null) ? userFriendlyExtensions[i] : text;
				if (text.Length != dialPlan.NumberOfDigitsInExtension || !Utils.IsNumber(text))
				{
					validationError = new InvalidExtensionException(s, dialPlan.NumberOfDigitsInExtension);
					return;
				}
				Utils.IsPhoneNumberRegistered(iadrecipientLookup, tenantConfigLookup, adUser, dialPlan, text, out validationError, out status);
				if (validationError != null)
				{
					return;
				}
			}
			if (!string.IsNullOrEmpty(sipResourceIdentifier))
			{
				if (dialPlan.URIType == UMUriType.SipName)
				{
					ADRecipient adrecipient = iadrecipientLookup.LookupByEumSipResourceIdentifierPrefix(sipResourceIdentifier);
					if (adrecipient == null)
					{
						adrecipient = iadrecipientLookup.LookupBySipExtension(sipResourceIdentifier);
					}
					if (adrecipient != null && !adrecipient.Id.Equals(adUser.Id))
					{
						validationError = new SipUriAlreadyRegisteredException(sipResourceIdentifier, adrecipient.Name);
						status = TelephoneNumberProcessStatus.PhoneNumberUsedByOthers;
						return;
					}
				}
				else
				{
					Utils.IsPhoneNumberRegistered(iadrecipientLookup, tenantConfigLookup, adUser, dialPlan, sipResourceIdentifier, out validationError, out status);
				}
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
		public static void IsPhoneNumberRegistered(IADRecipientLookup tenantRecipientLookup, IADSystemConfigurationLookup tenantConfigLookup, ADUser adUser, UMDialPlan dialPlan, string phoneNumber, out LocalizedException validationError, out TelephoneNumberProcessStatus status)
		{
			validationError = null;
			status = TelephoneNumberProcessStatus.Success;
			if (Utils.IsUriValid(phoneNumber, UMUriType.E164) && Utils.IsPhoneNumberPilotNumberInAnyAAOrDP(tenantConfigLookup, phoneNumber))
			{
				validationError = new ExtensionAlreadyUsedAsPilotNumberException(phoneNumber, dialPlan.Name);
				status = TelephoneNumberProcessStatus.PhoneNumberUsedByOthers;
			}
			if (validationError == null)
			{
				ADRecipient adrecipient = tenantRecipientLookup.LookupByExtensionAndEquivalentDialPlan(phoneNumber, dialPlan);
				if (adrecipient != null)
				{
					if (adrecipient.PrimarySmtpAddress != adUser.PrimarySmtpAddress)
					{
						validationError = new ExtensionNotUniqueException(phoneNumber, dialPlan.Name);
						status = TelephoneNumberProcessStatus.PhoneNumberUsedByOthers;
						return;
					}
					status = TelephoneNumberProcessStatus.PhoneNumberAlreadyRegistered;
				}
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002EF5C File Offset: 0x0002D15C
		public static void UMPopulate(ADUser adUser, string sipResourceIdentifier, MultiValuedProperty<string> extensions, UMMailboxPolicy policy, UMDialPlan dialPlan)
		{
			adUser.UMMailboxPolicy = policy.Id;
			adUser.UMRecipientDialPlanId = policy.UMDialPlan;
			adUser.ClearEUMProxy(true, dialPlan);
			if (!string.IsNullOrEmpty(sipResourceIdentifier))
			{
				adUser.AddEUMProxyAddress(sipResourceIdentifier, dialPlan);
			}
			if (extensions != null)
			{
				adUser.AddEUMProxyAddress(extensions, dialPlan);
			}
			Utils.StampUmOcsIntegrationVoiceMailSettings(adUser, dialPlan);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002EFAF File Offset: 0x0002D1AF
		public static bool IsUserUMEnabledInGivenDialplan(ADUser user, ADObjectId dialPlanId)
		{
			return user != null && user.UMEnabled && user.UMRecipientDialPlanId != null && user.UMRecipientDialPlanId.Equals(dialPlanId);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002EFD8 File Offset: 0x0002D1D8
		public static IList<string> GetExtensionsInDialPlan(UMDialPlan dialPlan, ADRecipient recipient)
		{
			ValidateArgument.NotNull(dialPlan, "dialPlan");
			ValidateArgument.NotNull(recipient, "recipient");
			List<string> list = new List<string>();
			string phoneContext = dialPlan.PhoneContext;
			for (int i = 0; i < recipient.EmailAddresses.Count; i++)
			{
				ProxyAddress proxyAddress = recipient.EmailAddresses[i];
				if (proxyAddress.Prefix is EumProxyAddressPrefix)
				{
					EumProxyAddress value = proxyAddress as EumProxyAddress;
					EumAddress eumAddress = (EumAddress)value;
					if (eumAddress.IsValid && string.Compare(eumAddress.PhoneContext, phoneContext, StringComparison.OrdinalIgnoreCase) == 0)
					{
						list.Add(eumAddress.Extension);
					}
				}
			}
			return list;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002F070 File Offset: 0x0002D270
		public static IList<string> GetExtensionsInDialPlanValidForPAA(UMDialPlan dialPlan, ADRecipient recipient)
		{
			IList<string> extensionsInDialPlan = Utils.GetExtensionsInDialPlan(dialPlan, recipient);
			List<string> list = new List<string>();
			foreach (string text in extensionsInDialPlan)
			{
				bool flag;
				switch (dialPlan.URIType)
				{
				case UMUriType.TelExtn:
					flag = true;
					break;
				case UMUriType.E164:
					flag = Utils.IsUriValid(text, UMUriType.E164);
					break;
				case UMUriType.SipName:
					flag = Utils.IsUriValid(text, UMUriType.SipName);
					break;
				default:
					throw new ArgumentException("Invalid DialPlan Uri: " + dialPlan.URIType.ToString());
				}
				if (flag)
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002F128 File Offset: 0x0002D328
		public static bool IsIdenticalDialPlan(UMDialPlan dp1, UMDialPlan dp2)
		{
			return string.Compare(dp1.Id.DistinguishedName, dp2.Id.DistinguishedName, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002F150 File Offset: 0x0002D350
		public static void InitUMMailbox(ADUser adUser)
		{
			using (UMMailboxRecipient ummailboxRecipient = new UMMailboxRecipient(adUser))
			{
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = ummailboxRecipient.CreateSessionLock())
				{
					Utils.InitUMMailbox(mailboxSessionLock.Session, adUser);
				}
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002F1AC File Offset: 0x0002D3AC
		public static void InitUMMailbox(MailboxSession mbxSession, ADUser adUser)
		{
			using (UMSearchFolder umsearchFolder = UMSearchFolder.Get(mbxSession, UMSearchFolder.Type.VoiceMail))
			{
				using (UMSearchFolder umsearchFolder2 = UMSearchFolder.Get(mbxSession, UMSearchFolder.Type.Fax))
				{
					if (XsoUtil.IsOverSendQuota(mbxSession.Mailbox, 102400UL))
					{
						throw new QuotaExceededException(Strings.InsufficientSendQuotaForUMEnablement(adUser.LegacyExchangeDN));
					}
					umsearchFolder.CreateSearchFolder();
					umsearchFolder2.CreateSearchFolder();
				}
			}
			CommonConstants.UMOutlookUIFlags flags = CommonConstants.UMOutlookUIFlags.VoicemailForm | CommonConstants.UMOutlookUIFlags.VoicemailOptions;
			XsoUtil.SetUMOutlookUIFlags(adUser, flags);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002F238 File Offset: 0x0002D438
		public static void ResetUMMailbox(ADUser adUser, bool keepProperties)
		{
			ValidateArgument.NotNull(adUser, "adUser");
			using (UMMailboxRecipient ummailboxRecipient = new UMMailboxRecipient(adUser))
			{
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = ummailboxRecipient.CreateSessionLock())
				{
					Utils.ResetUMMailbox(adUser, keepProperties, mailboxSessionLock.Session);
				}
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002F2A0 File Offset: 0x0002D4A0
		public static void ResetUMMailbox(ADUser adUser, bool keepProperties, MailboxSession mbxSession)
		{
			ValidateArgument.NotNull(adUser, "adUser");
			ValidateArgument.NotNull(mbxSession, "mbxSession");
			XsoUtil.SetUMOutlookUIFlags(adUser, CommonConstants.UMOutlookUIFlags.None);
			if (!keepProperties)
			{
				using (UMSearchFolder umsearchFolder = UMSearchFolder.Get(mbxSession, UMSearchFolder.Type.VoiceMail))
				{
					using (UMSearchFolder umsearchFolder2 = UMSearchFolder.Get(mbxSession, UMSearchFolder.Type.Fax))
					{
						using (DisposeGuard disposeGuard = default(DisposeGuard))
						{
							umsearchFolder.DeleteSearchFolder();
							umsearchFolder2.DeleteSearchFolder();
							List<string> list = new List<string>();
							list.Add("Um.General");
							list.Add("Um.Password");
							list.Add("UM.E14.PersonalAutoAttendants");
							ICollection<UserConfiguration> collection = mbxSession.UserConfigurationManager.FindMailboxConfigurations("Um.CustomGreetings", UserConfigurationSearchFlags.Prefix);
							foreach (UserConfiguration userConfiguration in collection)
							{
								list.Add(userConfiguration.ConfigurationName);
								disposeGuard.Add<UserConfiguration>(userConfiguration);
							}
							mbxSession.UserConfigurationManager.DeleteMailboxConfigurations(list.ToArray());
						}
					}
				}
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002F3E0 File Offset: 0x0002D5E0
		public static PINInfo ValidateOrGeneratePIN(ADUser adUser, string pin, MailboxSession mbxSession, UMMailboxPolicy userUMMailboxPolicy)
		{
			ValidateArgument.NotNull(adUser, "adUser");
			ValidateArgument.NotNull(mbxSession, "mbxSession");
			ValidateArgument.NotNull(userUMMailboxPolicy, "userUMMailboxPolicy");
			PINInfo result;
			using (UMMailboxRecipient ummailboxRecipient = new UMMailboxRecipient(adUser, mbxSession))
			{
				UmPasswordManager passwordMgr = new UmPasswordManager(ummailboxRecipient, userUMMailboxPolicy);
				result = Utils.ValidateOrGeneratePIN(adUser, pin, ummailboxRecipient, passwordMgr);
			}
			return result;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002F448 File Offset: 0x0002D648
		public static PINInfo ValidateOrGeneratePIN(ADUser adUser, string pin, MailboxSession mbxSession)
		{
			ValidateArgument.NotNull(adUser, "adUser");
			ValidateArgument.NotNull(mbxSession, "mbxSession");
			PINInfo result;
			using (UMMailboxRecipient ummailboxRecipient = new UMMailboxRecipient(adUser, mbxSession))
			{
				UmPasswordManager passwordMgr = new UmPasswordManager(ummailboxRecipient);
				result = Utils.ValidateOrGeneratePIN(adUser, pin, ummailboxRecipient, passwordMgr);
			}
			return result;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002F4A4 File Offset: 0x0002D6A4
		private static PINInfo ValidateOrGeneratePIN(ADUser user, string pin, UMMailboxRecipient mailboxRecipient, UmPasswordManager passwordMgr)
		{
			PINInfo pininfo = new PINInfo();
			byte[] array;
			if (string.IsNullOrEmpty(pin))
			{
				array = passwordMgr.GenerateValidPassword();
				pininfo.IsValid = (array != null);
			}
			else
			{
				array = Encoding.ASCII.GetBytes(pin);
				EncryptedBuffer pwd = new EncryptedBuffer(array);
				pininfo.IsValid = passwordMgr.IsValidPassword(pwd);
			}
			pininfo.PIN = ((array != null) ? Encoding.ASCII.GetString(array) : null);
			pininfo.PinExpired = passwordMgr.IsExpired;
			pininfo.LockedOut = passwordMgr.IsLocked;
			pininfo.FirstTimeUser = mailboxRecipient.ConfigFolder.IsFirstTimeUser;
			return pininfo;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002F538 File Offset: 0x0002D738
		public static PINInfo ValidateOrGeneratePIN(ADUser user, string pin)
		{
			ValidateArgument.NotNull(user, "user");
			PINInfo result;
			using (UMMailboxRecipient ummailboxRecipient = new UMMailboxRecipient(user))
			{
				UmPasswordManager passwordMgr = new UmPasswordManager(ummailboxRecipient);
				result = Utils.ValidateOrGeneratePIN(user, pin, ummailboxRecipient, passwordMgr);
			}
			return result;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0002F588 File Offset: 0x0002D788
		public static PINInfo GetPINInfo(ADUser user)
		{
			ValidateArgument.NotNull(user, "user");
			PINInfo pininfo;
			using (UMSubscriber umsubscriber = new UMSubscriber(user))
			{
				pininfo = Utils.GetPINInfo(user, umsubscriber);
			}
			return pininfo;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002F5CC File Offset: 0x0002D7CC
		public static PINInfo GetPINInfo(ADUser user, MailboxSession mbxSession)
		{
			ValidateArgument.NotNull(user, "user");
			ValidateArgument.NotNull(mbxSession, "mbxSession");
			PINInfo pininfo;
			using (UMSubscriber umsubscriber = new UMSubscriber(user, mbxSession))
			{
				pininfo = Utils.GetPINInfo(user, umsubscriber);
			}
			return pininfo;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002F61C File Offset: 0x0002D81C
		private static PINInfo GetPINInfo(ADUser user, UMSubscriber umUser)
		{
			PINInfo pininfo = new PINInfo();
			UmPasswordManager umPasswordManager = new UmPasswordManager(umUser);
			pininfo.PinExpired = umPasswordManager.IsExpired;
			pininfo.LockedOut = umPasswordManager.IsLocked;
			pininfo.FirstTimeUser = umUser.ConfigFolder.IsFirstTimeUser;
			return pininfo;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002F660 File Offset: 0x0002D860
		public static void SetUserPassword(ADUser adUser, string pin, bool expired, bool lockedOut)
		{
			ValidateArgument.NotNull(adUser, "adUser");
			ValidateArgument.NotNullOrEmpty(pin, "pin");
			using (UMMailboxRecipient ummailboxRecipient = new UMMailboxRecipient(adUser))
			{
				UmPasswordManager pwdMgr = new UmPasswordManager(ummailboxRecipient);
				Utils.SetUserPassword(ummailboxRecipient, pwdMgr, adUser, pin, expired, lockedOut);
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002F6B8 File Offset: 0x0002D8B8
		public static void SetUserPassword(MailboxSession mbxSession, ADUser adUser, string pin, bool expired, bool lockedOut)
		{
			ValidateArgument.NotNull(adUser, "adUser");
			ValidateArgument.NotNullOrEmpty(pin, "pin");
			ValidateArgument.NotNull(mbxSession, "mbxSession");
			using (UMMailboxRecipient ummailboxRecipient = new UMMailboxRecipient(adUser, mbxSession))
			{
				UmPasswordManager pwdMgr = new UmPasswordManager(ummailboxRecipient);
				Utils.SetUserPassword(ummailboxRecipient, pwdMgr, adUser, pin, expired, lockedOut);
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002F720 File Offset: 0x0002D920
		public static void SetUserPassword(MailboxSession mbxSession, UMMailboxPolicy umMbxPolicy, ADUser adUser, string pin, bool expired, bool lockedOut)
		{
			ValidateArgument.NotNull(adUser, "adUser");
			ValidateArgument.NotNullOrEmpty(pin, "pin");
			ValidateArgument.NotNull(mbxSession, "mbxSession");
			ValidateArgument.NotNull(umMbxPolicy, "umMbxPolicy");
			using (UMMailboxRecipient ummailboxRecipient = new UMMailboxRecipient(adUser, mbxSession))
			{
				UmPasswordManager pwdMgr = new UmPasswordManager(ummailboxRecipient, umMbxPolicy);
				Utils.SetUserPassword(ummailboxRecipient, pwdMgr, adUser, pin, expired, lockedOut);
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002F794 File Offset: 0x0002D994
		public static void SendPasswordResetMail(ADUser adUser, string[] accessNumbers, string extension, string pin, string recipient)
		{
			ValidateArgument.NotNull(adUser, "adUser");
			ValidateArgument.NotNullOrEmpty(recipient, "recipient");
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(adUser);
			UMMailboxPolicy policyFromRecipient = iadsystemConfigurationLookup.GetPolicyFromRecipient(adUser);
			Utils.SendPinNotifyMail(adUser, Strings.PasswordResetSubject, Strings.PasswordResetHeader, accessNumbers, extension, pin, policyFromRecipient.ResetPINText, recipient, null);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002F7E4 File Offset: 0x0002D9E4
		public static void SendWelcomeMail(ADUser adUser, string[] accessNumbers, string extension, string pin, string recipient, MailboxSession mbxSession = null)
		{
			ValidateArgument.NotNull(adUser, "adUser");
			ValidateArgument.NotNullOrEmpty(recipient, "recipient");
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(adUser);
			UMMailboxPolicy policyFromRecipient = iadsystemConfigurationLookup.GetPolicyFromRecipient(adUser);
			Utils.SendPinNotifyMail(adUser, Strings.WelcomeMailSubject, Strings.WelcomeMailBodyHeader, accessNumbers, extension, pin, policyFromRecipient.UMEnabledText, recipient, mbxSession);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002F834 File Offset: 0x0002DA34
		public static bool UnifiedMessagingAvailable(ADUser user)
		{
			ValidateArgument.NotNull(user, "user");
			bool flag = true;
			if (Utils.IsDatacenterUser(user))
			{
				PIIMessage data = PIIMessage.Create(PIIType._User, user.DistinguishedName);
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, data, "Checking if UM is available for user _User", new object[0]);
				List<string> parameters = new List<string>
				{
					"UMMailboxPolicy"
				};
				string cmdletFullName = "Microsoft.Exchange.Management.PowerShell.E2010\\Enable-UMMailbox";
				flag = ExchangeRunspaceConfiguration.IsCmdletValidOnObject(cmdletFullName, parameters, user);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "User = {0}, UM Available = {1}", new object[]
			{
				user.DistinguishedName,
				flag
			});
			return flag;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002F8D8 File Offset: 0x0002DAD8
		internal static bool IsNumber(string input)
		{
			return input != null && Utils.numberRegex.IsMatch(input);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002F8EC File Offset: 0x0002DAEC
		internal static void SafelyReleaseAndCloseNamedSemaphore(Semaphore semaphore)
		{
			try
			{
				if (semaphore != null)
				{
					semaphore.Release();
					semaphore.Close();
				}
			}
			catch (SemaphoreFullException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "SafelyReleaseAndCloseNamedSemaphore() Exception: {0}", new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002F940 File Offset: 0x0002DB40
		internal static string RemoveSIPPrefix(string sipUri)
		{
			return Utils.RemoveSchemePrefix("SIP:", sipUri);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002F94D File Offset: 0x0002DB4D
		internal static string RemoveSchemePrefix(string prefix, string uri)
		{
			if (uri != null && uri.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			{
				uri = uri.Remove(0, prefix.Length);
			}
			return uri;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002F96C File Offset: 0x0002DB6C
		internal static string NormalizeNumber(string input, UMUriType uri)
		{
			if (uri != UMUriType.E164)
			{
				return input;
			}
			PhoneNumber phoneNumber;
			if (PhoneNumber.TryParse(input, out phoneNumber))
			{
				return input[0] + phoneNumber.Number;
			}
			return string.Empty;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
		internal static bool IsUriValid(string uri, UMUriType uriType)
		{
			return Utils.IsUriValid(uri, uriType, 0);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002F9B2 File Offset: 0x0002DBB2
		internal static UMUriType DetermineNumberType(string number)
		{
			if (number.IndexOf("@", StringComparison.InvariantCulture) != -1)
			{
				return UMUriType.SipName;
			}
			if (number.Length > 0 && number[0] == '+')
			{
				return UMUriType.E164;
			}
			return UMUriType.TelExtn;
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002F9DC File Offset: 0x0002DBDC
		internal static bool FindSipNameOrE164Extension(UMDialPlan originatingDialPlan, IADRecipient target, UMUriType uriType, out string number)
		{
			if (target == null)
			{
				number = null;
				return false;
			}
			string text = null;
			foreach (ProxyAddress proxyAddress in target.EmailAddresses)
			{
				if (proxyAddress.IsPrimaryAddress && proxyAddress.Prefix == ProxyAddressPrefix.UM)
				{
					string[] array = proxyAddress.AddressString.Split(new string[]
					{
						";phone-context="
					}, StringSplitOptions.RemoveEmptyEntries);
					if (array.Length == 2)
					{
						number = array[0];
						string a = array[1];
						if (a == originatingDialPlan.PhoneContext && Utils.DetermineNumberType(number) == uriType)
						{
							return true;
						}
						if (Utils.DetermineNumberType(number) == uriType)
						{
							text = number;
						}
					}
				}
			}
			if (text != null)
			{
				number = text;
				return true;
			}
			number = null;
			return false;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002FAB8 File Offset: 0x0002DCB8
		internal static bool IsThisAnIPAddress(string address, out IPAddress ip)
		{
			return IPAddress.TryParse(address, out ip);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002FAC4 File Offset: 0x0002DCC4
		public static void InitializePerformanceCounters(Type counterType)
		{
			foreach (FieldInfo fieldInfo in counterType.GetFields(BindingFlags.Static | BindingFlags.Public))
			{
				ExPerformanceCounter exPerformanceCounter = fieldInfo.GetValue(null) as ExPerformanceCounter;
				if (exPerformanceCounter != null)
				{
					exPerformanceCounter.RawValue = 0L;
				}
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002FB04 File Offset: 0x0002DD04
		internal static bool HasValidDNSRecord(string address, out IPHostEntry he)
		{
			bool result;
			try
			{
				he = Dns.GetHostEntry(address);
				IPAddress ipaddress;
				if (Utils.IsThisAnIPAddress(address, out ipaddress) && he.HostName.Equals(address))
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				he = null;
				result = false;
			}
			catch (ArgumentException)
			{
				he = null;
				result = false;
			}
			catch (SocketException)
			{
				he = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002FB7C File Offset: 0x0002DD7C
		internal static string GetLocalHostName()
		{
			return ComputerInformation.DnsHostName;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002FB83 File Offset: 0x0002DD83
		internal static string GetLocalHostFqdn()
		{
			return ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002FB8C File Offset: 0x0002DD8C
		internal static string GetOwnerHostFqdn()
		{
			string result = Utils.GetLocalHostFqdn();
			if (CommonConstants.UseDataCenterCallRouting)
			{
				UMSmartHost externalFqdn = Utils.GetExternalFqdn();
				if (externalFqdn != null)
				{
					result = externalFqdn.ToString();
				}
			}
			return result;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002FBC4 File Offset: 0x0002DDC4
		internal static IPAddress GetLocalIPv4Address()
		{
			IPAddress ipaddress = (from addr in Utils.GetLocalIPAddresses()
			where addr.AddressFamily == AddressFamily.InterNetwork
			select addr).FirstOrDefault<IPAddress>();
			if (ipaddress != null)
			{
				return ipaddress;
			}
			throw new ProtocolViolationException(Strings.NoIPv4Address(Utils.GetLocalHostFqdn()));
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002FC18 File Offset: 0x0002DE18
		internal static IPAddress GetLocalIPAddress()
		{
			IPAddress ipaddress = Utils.GetLocalIPAddresses().FirstOrDefault<IPAddress>();
			if (ipaddress != null)
			{
				return ipaddress;
			}
			throw new ProtocolViolationException(Strings.NoIPAddress(Utils.GetLocalHostFqdn()));
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002FC49 File Offset: 0x0002DE49
		public static void GetLocalIPv4IPv6Support(out bool supportsIPv4, out bool supportsIPv6)
		{
			supportsIPv4 = Socket.OSSupportsIPv4;
			supportsIPv6 = Socket.OSSupportsIPv6;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002FC5C File Offset: 0x0002DE5C
		public static IPAddress[] GetLoopbackIPAddresses()
		{
			if (!Socket.OSSupportsIPv6 || !Socket.OSSupportsIPv4)
			{
				return new IPAddress[]
				{
					Socket.OSSupportsIPv6 ? IPAddress.IPv6Loopback : IPAddress.Loopback
				};
			}
			return new IPAddress[]
			{
				IPAddress.Loopback,
				IPAddress.IPv6Loopback
			};
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002FCAE File Offset: 0x0002DEAE
		public static IPAddress GetLoopbackControlIPAddress()
		{
			if (!Socket.OSSupportsIPv4)
			{
				return IPAddress.IPv6Loopback;
			}
			return IPAddress.Loopback;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002FCDC File Offset: 0x0002DEDC
		public static IPAddress[] GetLocalIPAddresses()
		{
			List<IPAddress> list = new List<IPAddress>();
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in from a in allNetworkInterfaces
			where a.OperationalStatus == OperationalStatus.Up
			select a)
			{
				IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
				UnicastIPAddressInformationCollection unicastAddresses = ipproperties.UnicastAddresses;
				foreach (IPAddressInformation ipaddressInformation in unicastAddresses.OrderBy((UnicastIPAddressInformation ua) => ua.Address.AddressFamily))
				{
					if (!IPAddress.IsLoopback(ipaddressInformation.Address) && !ipaddressInformation.IsTransient)
					{
						if (ipaddressInformation.Address.AddressFamily == AddressFamily.InterNetwork && !ipproperties.GetIPv4Properties().IsAutomaticPrivateAddressingActive)
						{
							CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, null, "GetLocalIPAddresses: add IPv4 address {0}", new object[]
							{
								ipaddressInformation.Address
							});
							list.Add(ipaddressInformation.Address);
						}
						else if (ipaddressInformation.Address.AddressFamily == AddressFamily.InterNetworkV6 && !ipaddressInformation.Address.IsIPv6LinkLocal)
						{
							CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, null, "GetLocalIPAddresses: add IPv6 address {0}", new object[]
							{
								ipaddressInformation.Address
							});
							list.Add(ipaddressInformation.Address);
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002FEA0 File Offset: 0x0002E0A0
		internal static bool IsThisALocalIP(IPAddress address)
		{
			List<IPAddress> localIPAddresses = ComputerInformation.GetLocalIPAddresses();
			foreach (IPAddress ipaddress in localIPAddresses)
			{
				if (ipaddress.Equals(address))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002FF00 File Offset: 0x0002E100
		internal static IPAddress[] GetHostAddresses(string hostNameOrAddress)
		{
			IPAddress[] result = null;
			try
			{
				result = Dns.GetHostAddresses(hostNameOrAddress);
			}
			catch (SocketException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, null, "Could not obtain list of addresses for {0}. {1}.", new object[]
				{
					hostNameOrAddress,
					ex
				});
			}
			catch (ArgumentException ex2)
			{
				CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, null, "Could not obtain list of addresses for {0}. {1}.", new object[]
				{
					hostNameOrAddress,
					ex2
				});
			}
			return result;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002FF80 File Offset: 0x0002E180
		internal static string GetExchangeDirectory()
		{
			if (Utils.exchangeDirectory == null)
			{
				lock (Utils.lockObj)
				{
					if (Utils.exchangeDirectory == null)
					{
						bool writable = false;
						using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", writable))
						{
							Utils.exchangeDirectory = (string)registryKey.GetValue("MSIInstallPath");
						}
					}
				}
			}
			return Utils.exchangeDirectory;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0003000C File Offset: 0x0002E20C
		internal static string GetExchangeBinPath()
		{
			return Path.Combine(Utils.GetExchangeDirectory(), "bin");
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00030020 File Offset: 0x0002E220
		internal static UMSmartHost GetExternalFqdn()
		{
			UMSmartHost result = null;
			try
			{
				Server server = LocalServer.GetServer();
				if (server == null)
				{
					throw new ExchangeServerNotFoundException(Utils.GetLocalHostName());
				}
				if (server.IsUnifiedMessagingServer)
				{
					result = new UMServer(server).ExternalHostFqdn;
				}
				else
				{
					if (!server.IsCafeServer)
					{
						throw new ExchangeServerNotValidException(Utils.GetLocalHostName());
					}
					SIPFEServerConfiguration localCallRouterSettings = ADTopologyLookup.CreateLocalResourceForestLookup().GetLocalCallRouterSettings();
					if (localCallRouterSettings == null)
					{
						throw new ExchangeServerNotFoundException(Utils.GetLocalHostName());
					}
					result = localCallRouterSettings.ExternalHostFqdn;
				}
			}
			catch (Exception ex)
			{
				if (ex is ADOperationException || ex is ADTransientException)
				{
					throw new ExchangeServerNotFoundException(ex.Message, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000300C4 File Offset: 0x0002E2C4
		internal static string GetHostFqdn()
		{
			UMSmartHost externalFqdn;
			string result;
			if (CommonConstants.UseDataCenterCallRouting && (externalFqdn = Utils.GetExternalFqdn()) != null)
			{
				result = externalFqdn.ToString();
			}
			else
			{
				result = Utils.GetLocalHostFqdn();
			}
			return result;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000300F1 File Offset: 0x0002E2F1
		internal static string TryGetRedirectTargetFqdnForServer(UMServer umserver, string alternateRedirectTarget)
		{
			if (umserver.ExternalHostFqdn != null)
			{
				return umserver.ExternalHostFqdn.ToString();
			}
			return alternateRedirectTarget;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00030108 File Offset: 0x0002E308
		internal static string TryGetRedirectTargetFqdnForServer(Server server)
		{
			return Utils.TryGetRedirectTargetFqdnForServer(new UMServer(server), server.Fqdn);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0003011C File Offset: 0x0002E31C
		internal static byte[] GetValidPassword(ADUser adUser)
		{
			byte[] result = null;
			using (UMMailboxRecipient ummailboxRecipient = new UMMailboxRecipient(adUser))
			{
				UmPasswordManager umPasswordManager = new UmPasswordManager(ummailboxRecipient);
				result = umPasswordManager.GenerateValidPassword();
			}
			return result;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00030160 File Offset: 0x0002E360
		internal static void ResetPassword(UMSubscriber umUser, bool expired, LockOutResetMode lockOutResetMode)
		{
			UmPasswordManager umPasswordManager = new UmPasswordManager(umUser);
			byte[] array = umPasswordManager.GenerateValidPassword();
			if (array == null)
			{
				throw new ResetPINException();
			}
			EncryptedBuffer digits = new EncryptedBuffer(array);
			umPasswordManager.SetPassword(digits, expired, lockOutResetMode);
			Utils.SendEmailWithNewPin(umUser, Encoding.ASCII.GetString(array));
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000301A8 File Offset: 0x0002E3A8
		internal static bool VerifyPin(byte[] pin, ADUser adUser)
		{
			bool result = false;
			using (UMSubscriber umsubscriber = new UMSubscriber(adUser))
			{
				UmPasswordManager umPasswordManager = new UmPasswordManager(umsubscriber);
				EncryptedBuffer pwd = new EncryptedBuffer(pin);
				if (umPasswordManager.IsValidPassword(pwd))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000301F4 File Offset: 0x0002E3F4
		internal static string TrimSpaces(string input)
		{
			string text = null;
			if (!string.IsNullOrEmpty(input))
			{
				text = input.Trim();
			}
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00030220 File Offset: 0x0002E420
		internal static bool TryGetNumericExtension(UMDialPlan dialPlan, ADRecipient adUser, out string numericExtension)
		{
			numericExtension = adUser.UMExtension;
			if (dialPlan.URIType == UMUriType.SipName)
			{
				foreach (ProxyAddress proxyAddress in adUser.EmailAddresses)
				{
					if (!(proxyAddress.Prefix != ProxyAddressPrefix.UM) && EumAddress.IsValidEumAddress(proxyAddress.AddressString))
					{
						EumAddress eumAddress = EumAddress.Parse(proxyAddress.AddressString);
						if (string.Equals(eumAddress.PhoneContext, dialPlan.PhoneContext, StringComparison.InvariantCultureIgnoreCase))
						{
							PhoneNumber phoneNumber = null;
							if (PhoneNumber.TryParse(eumAddress.Extension, out phoneNumber) && phoneNumber.UriType != UMUriType.SipName)
							{
								numericExtension = eumAddress.Extension;
								break;
							}
						}
					}
				}
			}
			return !string.IsNullOrEmpty(numericExtension);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000302F0 File Offset: 0x0002E4F0
		internal static UMServer GetServerConfig(string serverName)
		{
			UMServer result = null;
			if (!Utils.TryGetUMServerConfig(serverName, out result))
			{
				throw new ExchangeServerNotFoundException(serverName);
			}
			return result;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00030311 File Offset: 0x0002E511
		internal static UMServer GetLocalUMServer()
		{
			return Utils.GetServerConfig(Utils.GetLocalHostName());
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00030320 File Offset: 0x0002E520
		internal static bool TryGetUMServerConfig(string serverName, out UMServer umServer)
		{
			umServer = null;
			bool result = false;
			ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
			Server serverFromName = adtopologyLookup.GetServerFromName(serverName);
			if (serverFromName != null && (serverFromName.CurrentServerRole & ServerRole.UnifiedMessaging) == ServerRole.UnifiedMessaging)
			{
				umServer = new UMServer(serverFromName);
				result = true;
			}
			return result;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0003035C File Offset: 0x0002E55C
		internal static IEnumerable<UMDialPlan> GetServerDialplans(UMServer serverConfig, bool throwIfNone)
		{
			List<UMDialPlan> list = new List<UMDialPlan>();
			ExAssert.RetailAssert(VariantConfiguration.InvariantNoFlightingSnapshot.UM.GetServerDialPlans.Enabled, "GetServerDialPlans is not supported in data center mode.");
			if (serverConfig.DialPlans.Count == 0)
			{
				if (throwIfNone)
				{
					throw new DialPlanNotFoundException(string.Empty);
				}
			}
			else
			{
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromRootOrg(true);
				foreach (ADObjectId dialPlanId in serverConfig.DialPlans)
				{
					UMDialPlan dialPlanFromId = iadsystemConfigurationLookup.GetDialPlanFromId(dialPlanId);
					if (dialPlanFromId != null)
					{
						list.Add(dialPlanFromId);
					}
				}
			}
			return list;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00030408 File Offset: 0x0002E608
		internal static IEnumerable<UMDialPlan> GetServerDialplans(string serverName, bool throwIfNone)
		{
			return Utils.GetServerDialplans(Utils.GetServerConfig(serverName), throwIfNone);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00030418 File Offset: 0x0002E618
		internal static bool IsUMServerLinkedToDialplan(UMServer server, UMDialPlan dialPlan)
		{
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.UM.GetServerDialPlans.Enabled)
			{
				return true;
			}
			if (dialPlan != null && server != null && dialPlan.UMServers != null && dialPlan.UMServers.Count != 0)
			{
				foreach (ADObjectId adobjectId in dialPlan.UMServers)
				{
					if (string.Compare(server.Id.DistinguishedName, adobjectId.DistinguishedName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000304BC File Offset: 0x0002E6BC
		internal static int DetermineLongestSuffixMatch(string word1, string word2)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(word1) && !string.IsNullOrEmpty(word2))
			{
				word1 = word1.ToLowerInvariant();
				word2 = word2.ToLowerInvariant();
				int num2 = word1.Length - 1;
				int num3 = word2.Length - 1;
				while (word1[num2] == word2[num3])
				{
					num++;
					num2--;
					num3--;
					if (num2 < 0 || num3 < 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00030554 File Offset: 0x0002E754
		internal static void GetIPGatewayList(string targetServerFqdn, bool outboundOnly, bool notSimulator, out List<UMIPGateway> secureGateways, out List<UMIPGateway> unsecureGateways)
		{
			secureGateways = new List<UMIPGateway>();
			unsecureGateways = new List<UMIPGateway>();
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromRootOrg(false);
			List<UMIPGateway> list = new List<UMIPGateway>();
			List<UMIPGateway> list2 = new List<UMIPGateway>();
			foreach (UMIPGateway umipgateway in iadsystemConfigurationLookup.GetAllIPGateways())
			{
				if (Utils.ShouldAddGateway(umipgateway, outboundOnly, notSimulator))
				{
					Utils.FillOutGatewayPort(umipgateway, UMVoIPSecurityType.Secured);
					Utils.NormalizeLoopbackAddress(umipgateway, targetServerFqdn);
					list.Add(umipgateway);
				}
			}
			foreach (UMIPGateway umipgateway2 in iadsystemConfigurationLookup.GetAllIPGateways())
			{
				if (Utils.ShouldAddGateway(umipgateway2, outboundOnly, notSimulator))
				{
					Utils.FillOutGatewayPort(umipgateway2, UMVoIPSecurityType.Unsecured);
					Utils.NormalizeLoopbackAddress(umipgateway2, targetServerFqdn);
					list2.Add(umipgateway2);
				}
			}
			IEnumerable<UMDialPlan> allDialPlans = iadsystemConfigurationLookup.GetAllDialPlans();
			foreach (UMDialPlan umdialPlan in allDialPlans)
			{
				using (MultiValuedProperty<ADObjectId>.Enumerator enumerator4 = umdialPlan.UMIPGateway.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						ADObjectId gatewayId = enumerator4.Current;
						if (umdialPlan.VoIPSecurity != UMVoIPSecurityType.Unsecured)
						{
							secureGateways.AddRange(from o in list
							where gatewayId.Equals(o.Id)
							select o);
						}
						else
						{
							unsecureGateways.AddRange(from o in list2
							where gatewayId.Equals(o.Id)
							select o);
						}
					}
				}
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00030724 File Offset: 0x0002E924
		internal static List<UMIPGateway> GetIPGatewayList(string targetServerFqdn, UMDialPlan dialPlan, bool outboundOnly, bool notSimulator)
		{
			List<UMIPGateway> list = new List<UMIPGateway>();
			if (dialPlan == null || dialPlan.UMIPGateway == null || dialPlan.UMIPGateway.Count == 0)
			{
				return list;
			}
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(dialPlan.OrganizationId, false);
			IEnumerable<UMIPGateway> gatewaysLinkedToDialPlan = iadsystemConfigurationLookup.GetGatewaysLinkedToDialPlan(dialPlan);
			foreach (UMIPGateway umipgateway in gatewaysLinkedToDialPlan)
			{
				if (Utils.ShouldAddGateway(umipgateway, outboundOnly, notSimulator))
				{
					Utils.FillOutGatewayPort(umipgateway, dialPlan.VoIPSecurity);
					Utils.NormalizeLoopbackAddress(umipgateway, targetServerFqdn);
					list.Add(umipgateway);
				}
			}
			return list;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000307C4 File Offset: 0x0002E9C4
		internal static int GetRedirectPort(int tcpPort, int tlsPort, bool callIsSecured)
		{
			if (!callIsSecured)
			{
				return tcpPort;
			}
			return tlsPort;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000307CC File Offset: 0x0002E9CC
		internal static int GetRedirectPort(bool callIsSecured)
		{
			return Utils.GetRedirectPort(5060, 5061, callIsSecured);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000307E0 File Offset: 0x0002E9E0
		internal static bool GetDatacenterLoggingEnabled()
		{
			bool result = false;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.UM.UMDataCenterLogging.Enabled)
			{
				result = true;
				ExTraceGlobals.UtilTracer.TraceDebug(0L, "Datacenter logging enabled.");
			}
			else
			{
				ExTraceGlobals.UtilTracer.TraceDebug(0L, "Datacenter logging disabled.");
			}
			return result;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00030830 File Offset: 0x0002EA30
		internal static bool GetDatacenterRoutingEnabled()
		{
			bool result = false;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.UM.UMDataCenterCallRouting.Enabled)
			{
				result = true;
				ExTraceGlobals.UtilTracer.TraceDebug(0L, "Datacenter routing enabled.");
			}
			else
			{
				ExTraceGlobals.UtilTracer.TraceDebug(0L, "Datacenter routing disabled.");
			}
			return result;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00030880 File Offset: 0x0002EA80
		internal static bool GetDatacenterADPresent()
		{
			bool result = false;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.UM.UMDataCenterAD.Enabled)
			{
				result = true;
				ExTraceGlobals.UtilTracer.TraceDebug(0L, "Datacenter AD present.");
			}
			else
			{
				ExTraceGlobals.UtilTracer.TraceDebug(0L, "Datacenter AD not present.");
			}
			return result;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000308D0 File Offset: 0x0002EAD0
		internal static int? GetMaxCallsAllowed()
		{
			int? maxCallsAllowed;
			if (CommonConstants.UseDataCenterCallRouting)
			{
				maxCallsAllowed = new int?(100);
			}
			else
			{
				UMServer localUMServer = Utils.GetLocalUMServer();
				maxCallsAllowed = localUMServer.MaxCallsAllowed;
			}
			return maxCallsAllowed;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000308FD File Offset: 0x0002EAFD
		internal static bool IsDatacenterUser(ADUser user)
		{
			return !user.OrganizationId.Equals(OrganizationId.ForestWideOrgId);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00030914 File Offset: 0x0002EB14
		internal static void KillThisProcess()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMKillCurrentProcess, null, new object[]
				{
					currentProcess.ProcessName,
					currentProcess.Id
				});
				Utils.KillProcess(currentProcess);
				try
				{
					currentProcess.WaitForExit();
				}
				catch (Win32Exception ex)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In KillProcess - encountered at exception = ", new object[]
					{
						ex.ToString() + "Exiting anyway, ignoring the exception"
					});
				}
				catch (InvalidOperationException ex2)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In KillProcess - encountered at exception = ", new object[]
					{
						ex2.ToString() + "Exiting anyway, ignoring the exception"
					});
				}
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00030A08 File Offset: 0x0002EC08
		internal static void KillProcess(Process p)
		{
			try
			{
				p.Kill();
			}
			catch (Win32Exception ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In KillProcess - encountered at exception = ", new object[]
				{
					ex.ToString() + "Dont care about the Process exiting. Hence ignoring the exception"
				});
			}
			catch (InvalidOperationException ex2)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In KillProcess - encountered at exception = ", new object[]
				{
					ex2.ToString() + "Dont care about the Process exiting. Hence ignoring the exception"
				});
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00030ACC File Offset: 0x0002ECCC
		internal static void CleanUMTempDirectory(string umTempDir)
		{
			if (string.IsNullOrEmpty(umTempDir))
			{
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "In Starting CleanUMTempDirectory", new object[0]);
			string[] array = Utils.DoDirectoryIOOperation<string[]>("Directory.GetDirectories", () => Directory.GetDirectories(umTempDir));
			if (array != null)
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string dir = array2[i];
					if (!File.Exists(Path.Combine(dir, "wp.active")))
					{
						Utils.DoDirectoryIOOperation<bool>("Directory.Delete", delegate
						{
							Directory.Delete(dir, true);
							return true;
						});
					}
				}
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00030B8C File Offset: 0x0002ED8C
		internal static void Shuffle<T>(IList<T> list, int startIndex)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (list.Count < 2 || startIndex >= list.Count - 1)
			{
				return;
			}
			Random random = new Random();
			for (int i = startIndex; i < list.Count; i++)
			{
				int index = random.Next(startIndex, list.Count);
				T value = list[index];
				list[index] = list[i];
				list[i] = value;
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00030C10 File Offset: 0x0002EE10
		internal static bool TryReadRegValue(string keyName, string valueName, out int outVal)
		{
			ValidateArgument.NotNullOrEmpty(keyName, "keyName");
			ValidateArgument.NotNullOrEmpty(valueName, "valueName");
			outVal = 0;
			Exception ex = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyName))
				{
					if (registryKey != null)
					{
						int? num = registryKey.GetValue(valueName) as int?;
						if (num != null && num.Value >= 0)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "Read from registrykey {0} value = {1}", new object[]
							{
								keyName + "\\" + valueName,
								num.Value
							});
							outVal = num.Value;
							return true;
						}
					}
				}
			}
			catch (ObjectDisposedException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			catch (UnauthorizedAccessException ex5)
			{
				ex = ex5;
			}
			finally
			{
				if (ex != null)
				{
					CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, 0, "Exception thrown when retrieving registry key='{0}', value ='{1}': exception='{2}'", new object[]
					{
						keyName,
						valueName,
						ex
					});
				}
			}
			return false;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00030D68 File Offset: 0x0002EF68
		internal static void DoUMEnablingSynchronousWork(ADUser adUser)
		{
			adUser.PopulateDtmfMap(true);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00030D74 File Offset: 0x0002EF74
		internal static AcceptedDomain GetDefaultAcceptedDomain(ADRecipient recipient)
		{
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(recipient.OrganizationId);
			AcceptedDomain defaultAcceptedDomain = iadsystemConfigurationLookup.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain == null)
			{
				throw new InvalidAcceptedDomainException(recipient.OrganizationId.ToString());
			}
			return defaultAcceptedDomain;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00030DAC File Offset: 0x0002EFAC
		internal static string ConcatenateMessagesOnException(Exception e)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			bool flag = true;
			while (e != null)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(" --> ");
				}
				stringBuilder.Append(e.Message);
				e = e.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00030DF9 File Offset: 0x0002EFF9
		internal static void ThreadPoolQueueUserWorkItem(WaitCallback callBack, object state)
		{
			if (!ThreadPool.QueueUserWorkItem(callBack, state))
			{
				Utils.KillThisProcess();
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00030E0C File Offset: 0x0002F00C
		internal static string GetUnProtectedVoiceAttachmentContentType(string fileName)
		{
			string result = null;
			if (AudioFile.IsProtectedMp3(fileName))
			{
				result = "audio/mp3";
			}
			else if (AudioFile.IsProtectedWma(fileName))
			{
				result = "audio/wma";
			}
			else if (AudioFile.IsProtectedWav(fileName))
			{
				result = "audio/wav";
			}
			else
			{
				CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, 0, "the contentype for the following filename is not supported {0}", new object[]
				{
					fileName
				});
			}
			return result;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00030E70 File Offset: 0x0002F070
		internal static string GetGrammarDirectoryNameForCulture(CultureInfo culture)
		{
			return Utils.GetCultureDirectory(AppConfig.Instance.GrammarDirectory.GrammarCultureToSubDirectoryMap, culture, true);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00030E95 File Offset: 0x0002F095
		internal static string GrammarPathFromCulture(CultureInfo culture)
		{
			return Path.Combine(AppConfig.Instance.GrammarDirectory.GrammarDir, Utils.GetGrammarDirectoryNameForCulture(culture)) + Path.DirectorySeparatorChar;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00030EC0 File Offset: 0x0002F0C0
		internal static string GetCommonGrammarFilePath(CultureInfo culture)
		{
			return Path.Combine(Utils.GrammarPathFromCulture(culture), "common.grxml");
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00030ED4 File Offset: 0x0002F0D4
		internal static string GetPeopleSearchGrammarTemplateFilePath(CultureInfo culture)
		{
			string result;
			try
			{
				result = Path.Combine(Utils.GrammarPathFromCulture(culture), "peoplesearchtemplate.grxml");
			}
			catch (ResourceDirectoryNotFoundException ex)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, 0, "GetPeopleSearchGrammarTemplateFilePath: message:{0}. Defaulting to en-US culture", new object[]
				{
					ex.Message
				});
				result = Path.Combine(Utils.GrammarPathFromCulture(CommonConstants.DefaultCulture), "peoplesearchtemplate.grxml");
			}
			return result;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00030F44 File Offset: 0x0002F144
		internal static string GetPromptDirectoryNameForCulture(CultureInfo culture)
		{
			return Utils.GetCultureDirectory(AppConfig.Instance.WaveDirectory.PromptCultureToSubDirectoryMap, culture, false);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00030F6C File Offset: 0x0002F16C
		internal static void CopyPeopleGrammarRules(XmlWriter namesGrammarWriter, CultureInfo culture)
		{
			using (XmlTextReader xmlTextReader = new XmlTextReader(Utils.GetPeopleSearchGrammarTemplateFilePath(culture)))
			{
				xmlTextReader.Namespaces = false;
				while (xmlTextReader.ReadToFollowing("rule"))
				{
					namesGrammarWriter.WriteRaw(xmlTextReader.ReadOuterXml());
				}
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00030FC4 File Offset: 0x0002F1C4
		public static string CheckString(string str)
		{
			return str ?? string.Empty;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00030FD0 File Offset: 0x0002F1D0
		public static float? GetNullableAudioQualityMetric(float metric)
		{
			if (metric == AudioQuality.UnknownValue)
			{
				return null;
			}
			return new float?(metric);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00030FF5 File Offset: 0x0002F1F5
		private static void StampUmOcsIntegrationVoiceMailSettings(ADUser user, UMDialPlan dialPlan)
		{
			user.VoiceMailSettings.Clear();
			if (Utils.IsDatacenterUser(user) && dialPlan != null && dialPlan.URIType == UMUriType.SipName)
			{
				user.VoiceMailSettings.Add("ExchangeHostedVoiceMail=1");
			}
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00031028 File Offset: 0x0002F228
		private static void SetUserPassword(UMMailboxRecipient umUser, UmPasswordManager pwdMgr, ADUser adUser, string pin, bool expired, bool lockedOut)
		{
			EncryptedBuffer digits = new EncryptedBuffer(Encoding.ASCII.GetBytes(pin));
			LockOutResetMode lockoutResetMode = lockedOut ? LockOutResetMode.LockedOut : LockOutResetMode.Reset;
			pwdMgr.SetPassword(digits, expired, lockoutResetMode);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0003105C File Offset: 0x0002F25C
		private static MailboxSession OpenMailboxSession(ADUser adUser, string recipientMailAddress, string connectionString)
		{
			ADSessionSettings adSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), adUser.OrganizationId, null, false);
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromProxyAddress(adSettings, recipientMailAddress, RemotingOptions.AllowCrossSite);
			return MailboxSessionEstablisher.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, connectionString);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00031094 File Offset: 0x0002F294
		private static void SendPinNotifyMail(ADUser adUser, LocalizedString messageSubject, LocalizedString messageHeader, string[] accessNumbers, string extension, string pin, string additionalText, string recipient, MailboxSession mbxSession = null)
		{
			using (UMSubscriber umsubscriber = new UMSubscriber(adUser))
			{
				MessageContentBuilder messageContentBuilder = MessageContentBuilder.Create(umsubscriber.MessageSubmissionCulture);
				if (umsubscriber.DialPlan.SubscriberType == UMSubscriberType.Enterprise)
				{
					messageContentBuilder.AddEnterpriseNotifyMailBody(messageHeader, accessNumbers, extension, pin, additionalText);
				}
				else
				{
					messageContentBuilder.AddConsumerNotifyMailBody(messageHeader, accessNumbers, extension, pin, additionalText);
				}
				Utils.SendNotifyMail(adUser, recipient, messageSubject.ToString(umsubscriber.MessageSubmissionCulture), messageContentBuilder.ToString(), mbxSession);
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00031118 File Offset: 0x0002F318
		private static void SendNotifyMail(ADUser adUser, string recipientMailAddress, string messageSubject, string messageBody, MailboxSession mbxSession = null)
		{
			if (mbxSession != null)
			{
				using (XSOUMUserMailboxAccessor xsoumuserMailboxAccessor = new XSOUMUserMailboxAccessor(adUser, mbxSession))
				{
					xsoumuserMailboxAccessor.SendEmail(recipientMailAddress, messageSubject, messageBody);
					return;
				}
			}
			using (IUMUserMailboxStorage umuserMailboxAccessor = InterServerMailboxAccessor.GetUMUserMailboxAccessor(adUser, false))
			{
				umuserMailboxAccessor.SendEmail(recipientMailAddress, messageSubject, messageBody);
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00031180 File Offset: 0x0002F380
		private static void SendEmailWithNewPin(UMSubscriber umUser, string newPin)
		{
			UMDialPlan dialPlan = umUser.DialPlan;
			ADUser aduser = umUser.ADRecipient as ADUser;
			if (aduser == null)
			{
				throw new InvalidADRecipientException();
			}
			string extension = null;
			if (!Utils.TryGetNumericExtension(dialPlan, umUser.ADRecipient, out extension))
			{
				extension = null;
			}
			Utils.SendPasswordResetMail(aduser, (dialPlan.AccessTelephoneNumbers != null) ? dialPlan.AccessTelephoneNumbers.ToArray() : null, extension, newPin, umUser.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000311F9 File Offset: 0x0002F3F9
		private static bool IsLoopbackAddress(UMSmartHost smartHost)
		{
			if (smartHost.IsIPAddress)
			{
				return IPAddress.IsLoopback(smartHost.Address);
			}
			return string.Equals(smartHost.ToString(), "localhost", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00031220 File Offset: 0x0002F420
		private static bool ShouldAddGateway(UMIPGateway gw, bool outboundOnly, bool notSimulator)
		{
			return !outboundOnly || (outboundOnly && gw.OutcallsAllowed) || !notSimulator || (notSimulator && !gw.Simulator);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00031243 File Offset: 0x0002F443
		private static void FillOutGatewayPort(UMIPGateway gw, UMVoIPSecurityType securityOption)
		{
			if (gw.Port == 0)
			{
				gw.propertyBag.DangerousSetValue(UMIPGatewaySchema.Port, Utils.GetRedirectPort(securityOption != UMVoIPSecurityType.Unsecured));
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00031270 File Offset: 0x0002F470
		private static void NormalizeLoopbackAddress(UMIPGateway gw, string targetServerFqdn)
		{
			if (Utils.IsLoopbackAddress(gw.Address))
			{
				string text = targetServerFqdn.ToLowerInvariant();
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "GetIPGatewayList: gw.Address {0} is the loopback address, using {1}:{2} instead.", new object[]
				{
					gw.Address,
					text,
					gw.Port
				});
				gw.propertyBag.DangerousSetValue(UMIPGatewaySchema.Address, new UMSmartHost(text));
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000312DC File Offset: 0x0002F4DC
		private static bool IsPhoneNumberPilotNumberInAnyAAOrDP(IADSystemConfigurationLookup globalConfigLookup, string phoneNumber)
		{
			UMAutoAttendant autoAttendantWithNoDialplanInformation = globalConfigLookup.GetAutoAttendantWithNoDialplanInformation(phoneNumber);
			UMDialPlan umdialPlan = null;
			if (autoAttendantWithNoDialplanInformation == null)
			{
				umdialPlan = globalConfigLookup.GetDialPlanFromPilotIdentifier(phoneNumber);
			}
			return autoAttendantWithNoDialplanInformation != null || umdialPlan != null;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003130C File Offset: 0x0002F50C
		private static TResult DoDirectoryIOOperation<TResult>(string operationName, Func<TResult> operation)
		{
			TResult result = default(TResult);
			try
			{
				result = operation();
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, 0, "DoDirectoryIOOpertaion: Operation Name:{0}. message:{1}", new object[]
				{
					operationName,
					ex.Message
				});
				if (!(ex is IOException) && !(ex is UnauthorizedAccessException))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0003137C File Offset: 0x0002F57C
		private static string GetCultureDirectory(Dictionary<CultureInfo, string> cultureToSubDirectoryMap, CultureInfo culture, bool isGrammar)
		{
			string text = null;
			AppConfig instance = AppConfig.Instance;
			if (!cultureToSubDirectoryMap.TryGetValue(culture, out text))
			{
				lock (cultureToSubDirectoryMap)
				{
					if (!cultureToSubDirectoryMap.TryGetValue(culture, out text))
					{
						string name = culture.Name;
						string twoLetterISOLanguageName = culture.TwoLetterISOLanguageName;
						string path = isGrammar ? instance.GrammarDirectory.GrammarDir : instance.WaveDirectory.WaveDir;
						string text2 = Path.Combine(path, name);
						string text3 = Path.Combine(path, twoLetterISOLanguageName);
						if (Directory.Exists(text2))
						{
							text = name;
							cultureToSubDirectoryMap[culture] = text;
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Added culture={0} directory='{1}' to cultureDirectoryMap", new object[]
							{
								culture.Name,
								text
							});
						}
						else if (Directory.Exists(text3))
						{
							text = twoLetterISOLanguageName;
							cultureToSubDirectoryMap[culture] = text;
							CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Added culture={0} directory='{1}' to cultureDirectoryMap", new object[]
							{
								culture.Name,
								text
							});
						}
						else
						{
							CallIdTracer.TraceError(ExTraceGlobals.ServiceStartTracer, 0, "Did not find prompt directory at paths: \"{0}\" OR \"{1}\" for culture={2}", new object[]
							{
								text2,
								text3,
								culture.Name
							});
							if (isGrammar)
							{
								throw new ResourceDirectoryNotFoundException(Strings.GrammarDirectoryNotFoundError(culture.DisplayName, text2, text3));
							}
							throw new ResourceDirectoryNotFoundException(Strings.PromptDirectoryNotFoundError(culture.DisplayName, text2, text3));
						}
					}
				}
			}
			return text;
		}

		// Token: 0x040006CA RID: 1738
		private const string ConnectionString = "Client=UM;Action=SendNotifyMail";

		// Token: 0x040006CB RID: 1739
		private static Regex numberRegex = new Regex("^\\d+$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040006CC RID: 1740
		private static string tempdirPath;

		// Token: 0x040006CD RID: 1741
		private static string exchangeDirectory;

		// Token: 0x040006CE RID: 1742
		private static bool? runningInTestMode = null;

		// Token: 0x040006CF RID: 1743
		private static bool? overrideGrammar = null;

		// Token: 0x040006D0 RID: 1744
		private static object lockObj = new object();
	}
}
