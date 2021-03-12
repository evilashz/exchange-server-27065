using System;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Flighting;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003DD RID: 989
	internal static class E4eHelper
	{
		// Token: 0x06002D49 RID: 11593 RVA: 0x000B4F14 File Offset: 0x000B3114
		internal static E4eEncryptionHelper GetE4eEncryptionHelper(MiniRecipient miniRecipient)
		{
			E4eEncryptionHelper instance = E4eEncryptionHelper.Instance;
			string s = string.Empty;
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(miniRecipient.GetContext(null), null, null);
			IVersion version = snapshot.E4E.Version;
			if (version != null)
			{
				s = version.VersionNum;
			}
			int num;
			if (int.TryParse(s, out num))
			{
				if (num == 1)
				{
					instance = E4eEncryptionHelper.Instance;
				}
				else if (num == 2)
				{
					instance = E4eEncryptionHelperV2.Instance;
				}
			}
			return instance;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000B4F7C File Offset: 0x000B317C
		internal static E4eDecryptionHelper GetE4eDecryptionHelper(string messageVersion)
		{
			if (string.IsNullOrWhiteSpace(messageVersion))
			{
				return E4eDecryptionHelper.Instance;
			}
			int num;
			if (!int.TryParse(messageVersion, out num))
			{
				throw new E4eException(string.Format("messageVersion: {0} could not be converted to an integer.", messageVersion));
			}
			if (num == 1)
			{
				return E4eDecryptionHelper.Instance;
			}
			if (num == 2)
			{
				return E4eDecryptionHelperV2.Instance;
			}
			throw new E4eException(string.Format("messageVersion: {0} is not valid.", messageVersion));
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000B4FD8 File Offset: 0x000B31D8
		internal static void RunUnderExceptionHandler(string messageId, E4eHelper.E4eDelegate method, E4eHelper.CompleteProcessDelegate completeProcessDelegate, out Exception exception, out bool isTransientException)
		{
			exception = null;
			isTransientException = false;
			try
			{
				method();
			}
			catch (E4eException ex)
			{
				exception = ex;
				isTransientException = false;
			}
			catch (InvalidRpmsgFormatException ex2)
			{
				exception = ex2;
				isTransientException = false;
			}
			catch (RightsManagementException ex3)
			{
				exception = ex3;
				if (!ex3.IsPermanent)
				{
					isTransientException = true;
				}
			}
			catch (ExchangeConfigurationException ex4)
			{
				exception = ex4;
				isTransientException = true;
			}
			catch (CryptographicException ex5)
			{
				exception = ex5;
				isTransientException = false;
			}
			catch (SecurityException ex6)
			{
				exception = ex6;
				isTransientException = false;
			}
			catch (FormatException ex7)
			{
				exception = ex7;
				isTransientException = false;
			}
			catch (EncoderFallbackException ex8)
			{
				exception = ex8;
				isTransientException = false;
			}
			catch (TransientException ex9)
			{
				exception = ex9;
				isTransientException = true;
			}
			catch (MessageConversionException ex10)
			{
				exception = ex10;
				isTransientException = false;
			}
			catch (Exception ex11)
			{
				E4eLog.Instance.LogError(messageId, "Encountered a unknown exception: {0}. Re-throwing the exception.", new object[]
				{
					ex11.ToString()
				});
				if (completeProcessDelegate != null)
				{
					completeProcessDelegate(null);
				}
				throw;
			}
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000B5134 File Offset: 0x000B3334
		internal static MailDirectionality GetDirectionality(MailItem mailItem)
		{
			TransportMailItemWrapper transportMailItemWrapper = mailItem as TransportMailItemWrapper;
			if (transportMailItemWrapper != null && transportMailItemWrapper.TransportMailItem != null)
			{
				return transportMailItemWrapper.TransportMailItem.Directionality;
			}
			return MailDirectionality.Undefined;
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000B5160 File Offset: 0x000B3360
		internal static string GetP2From(EmailMessage emailMessage)
		{
			if (emailMessage.From == null)
			{
				return string.Empty;
			}
			return emailMessage.From.SmtpAddress;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000B517C File Offset: 0x000B337C
		internal static string GetCurrentSender(MailItem mailItem)
		{
			string text = mailItem.FromAddress.ToString();
			if (string.IsNullOrWhiteSpace(text) || mailItem.FromAddress.Equals(RoutingAddress.NullReversePath))
			{
				E4eLog.Instance.LogInfo(mailItem.Message.MessageId, "P1.From Address is not found for sender, using P2.From Address", new object[0]);
				text = E4eHelper.GetP2From(mailItem.Message);
			}
			return text;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000B51E8 File Offset: 0x000B33E8
		internal static string GetOriginalSender(MailItem mailItem)
		{
			Header xheader = Utils.GetXHeader(mailItem.Message, "X-MS-Exchange-Organization-E4eMessageOriginalSender");
			string text = string.Empty;
			if (xheader != null)
			{
				text = xheader.Value;
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				text = E4eHelper.GetCurrentSender(mailItem);
			}
			return text;
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000B5228 File Offset: 0x000B3428
		internal static string GetCurrentSender(IReadOnlyMailItem mailItem)
		{
			string text = mailItem.From.ToString();
			if (string.IsNullOrWhiteSpace(text) || mailItem.From.Equals(RoutingAddress.NullReversePath))
			{
				E4eLog.Instance.LogInfo(mailItem.Message.MessageId, "P1.From Address is not found for sender, using P2.From Address", new object[0]);
				text = E4eHelper.GetP2From(mailItem.Message);
			}
			return text;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000B5294 File Offset: 0x000B3494
		internal static string GetOriginalSender(IReadOnlyMailItem mailItem)
		{
			Header xheader = Utils.GetXHeader(mailItem.Message, "X-MS-Exchange-Organization-E4eMessageOriginalSender");
			string text = string.Empty;
			if (xheader != null)
			{
				text = xheader.Value;
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				text = E4eHelper.GetCurrentSender(mailItem);
			}
			return text;
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000B52D4 File Offset: 0x000B34D4
		internal static OrganizationId GetOriginalSenderOrgId(MailItem mailItem)
		{
			Header xheader = Utils.GetXHeader(mailItem.Message, "X-MS-Exchange-Organization-E4eMessageOriginalSenderOrgId");
			OrganizationId organizationId = null;
			if (xheader != null)
			{
				organizationId = E4eHelper.FromBase64String(xheader.Value);
			}
			if (organizationId == null)
			{
				organizationId = Utils.OrgIdFromMailItem(mailItem);
			}
			return organizationId;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000B5314 File Offset: 0x000B3514
		internal static void GetCultureInfo(EmailMessage message, out string charsetName, out CultureInfo cultureInfo, out Encoding encoding)
		{
			if (!Utils.TryGetCultureInfoAndEncoding(message, out charsetName, out cultureInfo, out encoding))
			{
				throw new MessageConversionException(Strings.InvalidCharset(message.Body.CharsetName), false);
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000B5338 File Offset: 0x000B3538
		internal static void LogAllE4eHeaders(EmailMessage message, string agentIdentifier)
		{
			E4eHelper.LogHeaderValue(message, "X-MS-Exchange-Organization-E4eMessageOriginalSender", agentIdentifier);
			E4eHelper.LogHeaderValue(message, "X-MS-Exchange-Organization-E4eMessageOriginalSenderOrgId", agentIdentifier);
			E4eHelper.LogHeaderValue(message, "X-MS-Exchange-Organization-E4eEncryptMessage", agentIdentifier);
			E4eHelper.LogHeaderValue(message, "X-MS-Exchange-Organization-E4eMessageEncrypted", agentIdentifier);
			E4eHelper.LogHeaderValue(message, "X-MS-Exchange-Organization-E4eHtmlFileGenerated", agentIdentifier);
			E4eHelper.LogHeaderValue(message, "X-MS-Exchange-Organization-E4eDecryptMessage", agentIdentifier);
			E4eHelper.LogHeaderValue(message, "X-MS-Exchange-Organization-E4eMessageDecrypted", agentIdentifier);
			E4eHelper.LogHeaderValue(message, "X-MS-Exchange-Organization-E4ePortal", agentIdentifier);
			E4eHelper.LogHeaderValue(message, "X-MS-Exchange-Organization-E4eReEncryptMessage", agentIdentifier);
			E4eHelper.LogHeaderValue(message, "X-MS-Exchange-OMEMessageEncrypted", agentIdentifier);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000B53C0 File Offset: 0x000B35C0
		internal static void LogHeaderValue(EmailMessage message, string headerName, string agentIdentifier)
		{
			Header xheader = Utils.GetXHeader(message, headerName);
			if (xheader == null)
			{
				E4eLog.Instance.LogInfo(message.MessageId, "{0}Header '{1}' -- not found.", new object[]
				{
					agentIdentifier,
					headerName
				});
				return;
			}
			if (xheader.Value == null)
			{
				E4eLog.Instance.LogInfo(message.MessageId, "{0}Header '{1}' -- value is null", new object[]
				{
					agentIdentifier,
					headerName
				});
				return;
			}
			E4eLog.Instance.LogInfo(message.MessageId, "{0}Header '{1}' -- has value '{2}'", new object[]
			{
				agentIdentifier,
				headerName,
				xheader.Value
			});
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000B545C File Offset: 0x000B365C
		internal static OrganizationId GetOriginalSenderOrgId(IReadOnlyMailItem mailItem)
		{
			Header xheader = Utils.GetXHeader(mailItem.Message, "X-MS-Exchange-Organization-E4eMessageOriginalSenderOrgId");
			OrganizationId organizationId = null;
			if (xheader != null)
			{
				organizationId = E4eHelper.FromBase64String(xheader.Value);
			}
			if (organizationId == null)
			{
				organizationId = mailItem.OrganizationId;
			}
			return organizationId;
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000B549C File Offset: 0x000B369C
		internal static bool IsHeaderSetToTrue(EmailMessage emailMessage, string headerName)
		{
			Header xheader = Utils.GetXHeader(emailMessage, headerName);
			return xheader != null && xheader.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000B54C7 File Offset: 0x000B36C7
		internal static void RemoveHeader(EmailMessage emailMessage, string headerName)
		{
			emailMessage.MimeDocument.RootPart.Headers.RemoveAll(headerName);
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000B54E0 File Offset: 0x000B36E0
		internal static void OverrideMime(MailItem mailItem, EmailMessage emailMessage)
		{
			try
			{
				using (Stream mimeWriteStream = mailItem.GetMimeWriteStream())
				{
					emailMessage.MimeDocument.RootPart.WriteTo(mimeWriteStream);
				}
			}
			finally
			{
				if (emailMessage != null)
				{
					((IDisposable)emailMessage).Dispose();
				}
			}
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000B553C File Offset: 0x000B373C
		internal static byte[] ReadStreamToEnd(Stream stream)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000B557C File Offset: 0x000B377C
		internal static string ToBase64String(OrganizationId orgId)
		{
			return Convert.ToBase64String(orgId.GetBytes(Encoding.UTF8));
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000B5590 File Offset: 0x000B3790
		internal static OrganizationId FromBase64String(string orgId)
		{
			OrganizationId result;
			try
			{
				if (!OrganizationId.TryCreateFromBytes(Convert.FromBase64String(orgId), Encoding.UTF8, out result))
				{
					throw new E4eException("Could not create OrganizationId from base64 string.");
				}
			}
			catch (ADTransientException innerException)
			{
				throw new E4eException("Could not create OrganizationId from base64 string.", innerException);
			}
			return result;
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000B55DC File Offset: 0x000B37DC
		internal static bool IsFlightingFeatureEnabledForSender(MailItem mailItem, string originalSenderAddress, OrganizationId originalSenderOrganizationId)
		{
			return E4eHelper.IsFlightingFeatureEnabled(E4eHelper.GetMiniRecipient(mailItem, originalSenderAddress, originalSenderOrganizationId));
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000B55EC File Offset: 0x000B37EC
		internal static bool IsFlightingFeatureEnabled(MiniRecipient miniRecipient)
		{
			bool result = false;
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(miniRecipient.GetContext(null), null, null);
			IFeature e4E = snapshot.E4E.E4E;
			if (e4E != null)
			{
				result = e4E.Enabled;
			}
			return result;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000B5624 File Offset: 0x000B3824
		internal static bool IsOTPEnabledForSender(string originalSenderAddress, OrganizationId originalSenderOrganizationId)
		{
			return E4eHelper.IsOTPEnabled(E4eHelper.CreateMiniRecipient(originalSenderAddress, originalSenderOrganizationId));
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000B5634 File Offset: 0x000B3834
		internal static bool IsOTPEnabled(MiniRecipient miniRecipient)
		{
			bool result = false;
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(miniRecipient.GetContext(null), null, null);
			IFeature otp = snapshot.E4E.OTP;
			if (otp != null)
			{
				result = otp.Enabled;
			}
			return result;
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000B566C File Offset: 0x000B386C
		internal static MiniRecipient GetMiniRecipient(MailItem mailItem, string originalSenderAddress, OrganizationId originalSenderOrganizationId)
		{
			ProxyAddress proxyAddress = ProxyAddress.Parse(originalSenderAddress);
			ADRecipientCache<TransportMiniRecipient> adrecipientCache = (ADRecipientCache<TransportMiniRecipient>)mailItem.RecipientCache;
			Result<TransportMiniRecipient> result = adrecipientCache.FindAndCacheRecipient(proxyAddress);
			MiniRecipient result2;
			if (result.Data == null)
			{
				E4eLog.Instance.LogInfo(mailItem.Message.MessageId, "Unable to find transport mini recipient for sender: {0}.", new object[]
				{
					originalSenderAddress
				});
				result2 = E4eHelper.CreateMiniRecipient(originalSenderAddress, originalSenderOrganizationId);
			}
			else
			{
				result2 = result.Data;
			}
			return result2;
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000B56DC File Offset: 0x000B38DC
		internal static MiniRecipient CreateMiniRecipient(string originalSenderAddress, OrganizationId originalSenderOrganizationId)
		{
			MiniRecipient miniRecipient = new MiniRecipient();
			miniRecipient[MiniRecipientSchema.UserPrincipalName] = originalSenderAddress;
			miniRecipient[ADObjectSchema.OrganizationId] = originalSenderOrganizationId;
			miniRecipient[MiniRecipientSchema.Languages] = new MultiValuedProperty<CultureInfo>();
			return miniRecipient;
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000B5718 File Offset: 0x000B3918
		internal static string GetDefaultAcceptedDomainName(OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			string text;
			if (E4eHelper.defaultAcceptedDomainTable.TryGetValue(organizationId, out text))
			{
				return text;
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 763, "GetDefaultAcceptedDomainName", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\RightsManagement\\E4eHelper.cs");
			Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain defaultAcceptedDomain = tenantOrTopologyConfigurationSession.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain != null)
			{
				text = defaultAcceptedDomain.DomainName.ToString();
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				E4eHelper.defaultAcceptedDomainTable.Add(organizationId, text);
			}
			return text;
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000B5798 File Offset: 0x000B3998
		internal static InboundConversionOptions GetInboundConversionOptions(OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			InboundConversionOptions inboundConversionOptions = new InboundConversionOptions(E4eHelper.GetDefaultAcceptedDomainName(organizationId));
			inboundConversionOptions.LoadPerOrganizationCharsetDetectionOptions(organizationId);
			return inboundConversionOptions;
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000B57D0 File Offset: 0x000B39D0
		internal static OutboundConversionOptions GetOutboundConversionOptions(OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 818, "GetOutboundConversionOptions", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\RightsManagement\\E4eHelper.cs");
			return new OutboundConversionOptions(E4eHelper.GetDefaultAcceptedDomainName(organizationId))
			{
				ClearCategories = false,
				AllowPartialStnefConversion = true,
				DemoteBcc = true,
				UserADSession = tenantOrRootOrgRecipientSession
			};
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000B583C File Offset: 0x000B3A3C
		internal static string GetCertificateName()
		{
			if (!string.IsNullOrEmpty(E4eHelper.cachedE4eCertificateName))
			{
				return E4eHelper.cachedE4eCertificateName;
			}
			string result;
			lock (E4eHelper.cacheLock)
			{
				if (!string.IsNullOrEmpty(E4eHelper.cachedE4eCertificateName))
				{
					result = E4eHelper.cachedE4eCertificateName;
				}
				else
				{
					object obj2 = E4eHelper.ReadRegistryKey("SOFTWARE\\Microsoft\\ExchangeLabs", "E4eCertificateDistinguishedName");
					if (obj2 == null)
					{
						result = "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US";
					}
					else
					{
						string value = obj2.ToString();
						if (string.IsNullOrEmpty(value))
						{
							result = "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US";
						}
						else
						{
							E4eHelper.cachedE4eCertificateName = value;
							result = E4eHelper.cachedE4eCertificateName;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000B58DC File Offset: 0x000B3ADC
		internal static object ReadRegistryKey(string keyPath, string valueName)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyPath))
			{
				if (registryKey != null)
				{
					return registryKey.GetValue(valueName, null);
				}
			}
			return null;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000B5924 File Offset: 0x000B3B24
		internal static void GetTransportPLAndULAndLicenseUri(MailItem mailItem, out string publishingLicense, out string useLicense, out Uri licenseUri)
		{
			object obj;
			mailItem.Properties.TryGetValue("Microsoft.Exchange.Encryption.TransportDecryptionPL", out obj);
			publishingLicense = (string)obj;
			mailItem.Properties.TryGetValue("Microsoft.Exchange.Encryption.TransportDecryptionUL", out obj);
			useLicense = (string)obj;
			licenseUri = null;
			if (mailItem.Properties.TryGetValue("Microsoft.Exchange.Encryption.TransportDecryptionLicenseUri", out obj) && obj != null)
			{
				Uri.TryCreate((string)obj, UriKind.Absolute, out licenseUri);
			}
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000B5990 File Offset: 0x000B3B90
		internal static void GetTransportPLAndULAndLicenseUri(IReadOnlyMailItem mailItem, out string publishingLicense, out string useLicense, out Uri licenseUri)
		{
			mailItem.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Encryption.TransportDecryptionPL", out publishingLicense);
			mailItem.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Encryption.TransportDecryptionUL", out useLicense);
			licenseUri = null;
			string text;
			if (mailItem.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Encryption.TransportDecryptionLicenseUri", out text) && !string.IsNullOrWhiteSpace(text))
			{
				Uri.TryCreate(text, UriKind.Absolute, out licenseUri);
			}
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000B59EC File Offset: 0x000B3BEC
		internal static void SetTransportPLAndULAndLicenseUri(MailItem mailItem, string publishingLicense, string useLicense, Uri licenseUri)
		{
			mailItem.Properties["Microsoft.Exchange.Encryption.TransportDecryptionPL"] = publishingLicense;
			mailItem.Properties["Microsoft.Exchange.Encryption.TransportDecryptionUL"] = useLicense;
			mailItem.Properties["Microsoft.Exchange.Encryption.TransportDecryptionLicenseUri"] = ((licenseUri == null) ? string.Empty : licenseUri.OriginalString);
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000B5A44 File Offset: 0x000B3C44
		internal static bool IsPipelineDecrypted(IReadOnlyMailItem mailItem)
		{
			string value = null;
			mailItem.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Encryption.TransportDecryptionPL", out value);
			return !string.IsNullOrWhiteSpace(value);
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000B5A70 File Offset: 0x000B3C70
		internal static bool IsPipelineDecrypted(MailItem mailItem)
		{
			object obj;
			mailItem.Properties.TryGetValue("Microsoft.Exchange.Encryption.TransportDecryptionPL", out obj);
			string value = (string)obj;
			return !string.IsNullOrWhiteSpace(value);
		}

		// Token: 0x0400166D RID: 5741
		internal const string EncryptedAttachmentFileName = "message.html";

		// Token: 0x0400166E RID: 5742
		internal const string VersionInputName = "version";

		// Token: 0x0400166F RID: 5743
		internal const string MetadataInputName = "metadata";

		// Token: 0x04001670 RID: 5744
		internal const string SignatureInputName = "signature";

		// Token: 0x04001671 RID: 5745
		internal const string CertificateInputName = "certificate";

		// Token: 0x04001672 RID: 5746
		internal const string OTPButtonInputName = "OTPButton";

		// Token: 0x04001673 RID: 5747
		internal const string RpmsgInputName = "rpmsg";

		// Token: 0x04001674 RID: 5748
		internal const int MaxDeferrals = 3;

		// Token: 0x04001675 RID: 5749
		internal const int MetadataLength = 5;

		// Token: 0x04001676 RID: 5750
		internal const char MetadataSplitChar = '|';

		// Token: 0x04001677 RID: 5751
		internal const string MicrosoftHostedKeyPath = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x04001678 RID: 5752
		private const string DefaultCertificateName = "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US";

		// Token: 0x04001679 RID: 5753
		private static string cachedE4eCertificateName = string.Empty;

		// Token: 0x0400167A RID: 5754
		private static object cacheLock = new object();

		// Token: 0x0400167B RID: 5755
		private static MruDictionaryCache<OrganizationId, string> defaultAcceptedDomainTable = new MruDictionaryCache<OrganizationId, string>(5, 50000, 5);

		// Token: 0x020003DE RID: 990
		// (Invoke) Token: 0x06002D6F RID: 11631
		internal delegate void E4eDelegate();

		// Token: 0x020003DF RID: 991
		// (Invoke) Token: 0x06002D73 RID: 11635
		internal delegate void CompleteProcessDelegate(AgentAsyncState agentAsyncState);

		// Token: 0x020003E0 RID: 992
		internal enum MetaDataIndex
		{
			// Token: 0x0400167D RID: 5757
			OriginalSender,
			// Token: 0x0400167E RID: 5758
			Recipient,
			// Token: 0x0400167F RID: 5759
			OriginalSenderOrgId,
			// Token: 0x04001680 RID: 5760
			SentTime,
			// Token: 0x04001681 RID: 5761
			MessageId
		}
	}
}
