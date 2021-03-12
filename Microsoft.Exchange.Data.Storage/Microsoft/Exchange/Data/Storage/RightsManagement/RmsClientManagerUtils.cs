using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;
using System.Xml.Linq;
using Microsoft.com.IPC.WSServerLicensingService;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Data.Storage.OfflineRms;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.RightsManagementServices.Core;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B4F RID: 2895
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RmsClientManagerUtils
	{
		// Token: 0x060068EB RID: 26859 RVA: 0x001C10A0 File Offset: 0x001BF2A0
		public static void ThrowOnNullOrEmptyArrayArgument(string argumentName, object[] argument)
		{
			if (argument == null || argument.Length == 0)
			{
				throw new ArgumentNullException(argumentName);
			}
			for (int i = 0; i < argument.Length; i++)
			{
				if (argument[i] == null)
				{
					throw new ArgumentNullException(string.Concat(new object[]
					{
						argumentName,
						"[",
						i,
						"]"
					}));
				}
			}
		}

		// Token: 0x060068EC RID: 26860 RVA: 0x001C1100 File Offset: 0x001BF300
		public static Guid GetTenantGuidFromOrgId(OrganizationId orgId)
		{
			Guid result = Guid.Empty;
			if (orgId != null && orgId != OrganizationId.ForestWideOrgId && orgId.ConfigurationUnit != null)
			{
				result = orgId.ConfigurationUnit.ObjectGuid;
			}
			return result;
		}

		// Token: 0x060068ED RID: 26861 RVA: 0x001C1140 File Offset: 0x001BF340
		public static LicenseIdentity GetLicenseIdentity(RmsClientManagerContext context, string recipientAddress)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNullOrEmpty("recipientAddress", recipientAddress);
			ADRawEntry adrawEntry;
			try
			{
				adrawEntry = context.ResolveRecipient(recipientAddress);
			}
			catch (ADTransientException innerException)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadUserConfig(recipientAddress), innerException);
			}
			catch (ADOperationException innerException2)
			{
				throw new RightsManagementException(RightsManagementFailureCode.ADUserNotFound, ServerStrings.FailedToReadUserConfig(recipientAddress), innerException2);
			}
			if (adrawEntry == null)
			{
				throw new RightsManagementException(RightsManagementFailureCode.ADUserNotFound, ServerStrings.FailedToReadUserConfig(recipientAddress));
			}
			List<string> federatedEmailAddresses = RmsClientManagerUtils.GetFederatedEmailAddresses(context.OrgId, (ProxyAddressCollection)adrawEntry[ADRecipientSchema.EmailAddresses]);
			if (federatedEmailAddresses.Count == 0)
			{
				RmsClientManager.TraceFail(null, context.SystemProbeId, "GetLicenseIdentity: User {0} doesn't have any SMTP proxy address from a domain that is federated.", new object[]
				{
					recipientAddress
				});
				throw new RightsManagementException(RightsManagementFailureCode.ADUserNotFederated, DirectoryStrings.UserHasNoSmtpProxyAddressWithFederatedDomain);
			}
			return new LicenseIdentity(federatedEmailAddresses[0], federatedEmailAddresses.ToArray());
		}

		// Token: 0x060068EE RID: 26862 RVA: 0x001C1224 File Offset: 0x001BF424
		private static List<string> GetFederatedEmailAddresses(OrganizationId orgId, ProxyAddressCollection proxyAddresses)
		{
			if (proxyAddresses == null || proxyAddresses.Count == 0)
			{
				return (List<string>)RmsClientManagerUtils.EmptyProxyList;
			}
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(orgId);
			if (organizationIdCacheValue == null || organizationIdCacheValue.FederatedDomains == null)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError<OrganizationId>(0L, "GetFederatedEmailAddresses: Organization {0} does not have any federated domains.", orgId);
				throw new RightsManagementException(RightsManagementFailureCode.FederationNotEnabled, ServerStrings.FederationNotEnabled);
			}
			List<string> list = new List<string>(proxyAddresses.Count);
			List<string> list2 = new List<string>(organizationIdCacheValue.FederatedDomains);
			if (list2.Count > 50)
			{
				list2.RemoveRange(50, list2.Count - 50);
			}
			foreach (ProxyAddress proxyAddress in proxyAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.Smtp)
				{
					SmtpAddress arg = new SmtpAddress(proxyAddress.AddressString);
					if (list2.Contains(arg.Domain, StringComparer.OrdinalIgnoreCase))
					{
						Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug<SmtpAddress>(0L, "Adding address {0} to the list of email addresses", arg);
						list.Add(proxyAddress.AddressString);
					}
				}
			}
			return list;
		}

		// Token: 0x060068EF RID: 26863 RVA: 0x001C1344 File Offset: 0x001BF544
		public static LicenseIdentity GetFederatedLicenseIdentity(OrganizationId organizationId)
		{
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
			FederatedOrganizationId federatedOrganizationId = (organizationIdCacheValue != null) ? organizationIdCacheValue.FederatedOrganizationId : null;
			if (organizationIdCacheValue == null || organizationIdCacheValue.FederatedDomains == null || federatedOrganizationId == null || !federatedOrganizationId.Enabled || federatedOrganizationId.AccountNamespace == null || federatedOrganizationId.AccountNamespace.Domain == null)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError<OrganizationId>(0L, "GetFederatedLicenseIdentity: Organization {0} does not have any federated domains.", organizationId);
				throw new RightsManagementException(RightsManagementFailureCode.FederationNotEnabled, ServerStrings.FederationNotEnabled);
			}
			string tenantFederatedMailbox = RmsClientManager.IRMConfig.GetTenantFederatedMailbox(organizationId);
			if (string.IsNullOrEmpty(tenantFederatedMailbox))
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError<OrganizationId>(0L, "GetFederatedLicenseIdentity: Organization {0} doesn't have federated identity set.", organizationId);
				throw new RightsManagementException(RightsManagementFailureCode.FederatedMailboxNotSet, ServerStrings.FederatedMailboxNotSet(organizationId.ToString()));
			}
			string[] array = organizationIdCacheValue.FederatedDomains.ToArray<string>();
			int num = 0;
			while (num < array.Length && num < 50)
			{
				array[num] = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
				{
					"@",
					array[num]
				});
				num++;
			}
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				return new LicenseIdentity(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
				{
					new SmtpAddress(tenantFederatedMailbox).Local,
					"@",
					federatedOrganizationId.AccountNamespace.Domain
				}), array);
			}
			return new LicenseIdentity(tenantFederatedMailbox, array);
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x001C14B0 File Offset: 0x001BF6B0
		public static OrganizationId OrgIdFromPublishingLicenseOrDefault(string publishingLicense, OrganizationId defaultOrgId)
		{
			Guid guid;
			return RmsClientManagerUtils.OrgIdFromPublishingLicenseOrDefault(publishingLicense, defaultOrgId, out guid);
		}

		// Token: 0x060068F1 RID: 26865 RVA: 0x001C14C8 File Offset: 0x001BF6C8
		public static OrganizationId OrgIdFromPublishingLicenseOrDefault(string publishingLicense, OrganizationId defaultOrgId, out Guid externalDirectoryOrgId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("publishingLicense", publishingLicense);
			ArgumentValidator.ThrowIfNull("defaultOrgId", defaultOrgId);
			externalDirectoryOrgId = Guid.Empty;
			Guid guid;
			if (!RmsClientManagerUtils.ExtractExternalOrgIdFromPublishingLicense(publishingLicense, out guid))
			{
				return defaultOrgId;
			}
			Guid b;
			if (!RmsClientManager.IRMConfig.GetTenantExternalDirectoryOrgId(defaultOrgId, out b))
			{
				return defaultOrgId;
			}
			if (guid == b)
			{
				return defaultOrgId;
			}
			OrganizationId result;
			if (ADOperationResult.Success == RmsClientManagerUtils.TryGetOrganizationIdFromExternalDirectoryOrgId(guid, out result))
			{
				externalDirectoryOrgId = guid;
				return result;
			}
			return defaultOrgId;
		}

		// Token: 0x060068F2 RID: 26866 RVA: 0x001C1538 File Offset: 0x001BF738
		public static List<string> GetDelegatedEmailAddressesFromB2BUseLicense(string useLicense)
		{
			XmlDocument firstNodeXdoc = RmsClientManagerUtils.GetFirstNodeXdoc(useLicense);
			if (firstNodeXdoc == null)
			{
				return null;
			}
			XmlNode xmlNode = firstNodeXdoc.SelectSingleNode("XrML/BODY[@type=\"LICENSE\"]/ISSUEDPRINCIPALS/PRINCIPAL/OBJECT[@type=\"Group-Identity\"]/ADDRESS[@type=\"DelegatedEmailAddress\"]/text()");
			List<string> list = new List<string>();
			if (RmsClientManagerUtils.ValidateDelegatedEmailAddressOrAliasNode(xmlNode))
			{
				list.Add(xmlNode.Value);
			}
			XmlNodeList xmlNodeList = firstNodeXdoc.SelectNodes("XrML/BODY[@type=\"LICENSE\"]/ISSUEDPRINCIPALS/PRINCIPAL/OBJECT[@type=\"Group-Identity\"]/ADDRESS[@type=\"email_alias\"]/text()");
			foreach (object obj in xmlNodeList)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if (RmsClientManagerUtils.ValidateDelegatedEmailAddressOrAliasNode(xmlNode2))
				{
					list.Add(xmlNode2.Value);
				}
			}
			return list;
		}

		// Token: 0x060068F3 RID: 26867 RVA: 0x001C15E4 File Offset: 0x001BF7E4
		public static ExDateTime GetUseLicenseExpiryTime(LicenseResponse response)
		{
			ArgumentValidator.ThrowIfNull("response", response);
			return RmsClientManagerUtils.GetUseLicenseExpiryTime(response.License, (response.UsageRights != null) ? response.UsageRights.Value : ContentRight.None);
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x001C1628 File Offset: 0x001BF828
		public static ExDateTime GetUseLicenseExpiryTime(string license, ContentRight usageRights)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("license", license);
			XmlDocument firstNodeXdoc = RmsClientManagerUtils.GetFirstNodeXdoc(license);
			if (firstNodeXdoc == null)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Failed to parse the use license. Returning DateTime.MaxValue");
				return ExDateTime.MaxValue;
			}
			XmlNode xmlNode = firstNodeXdoc.SelectSingleNode("XrML/BODY[@type=\"LICENSE\"]/ISSUEDTIME/text()");
			if (xmlNode == null || string.IsNullOrEmpty(xmlNode.Value))
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError(0L, "Failed to parse the use license to get the issued Time. Returning DateTime.MaxValue");
				return ExDateTime.MaxValue;
			}
			DateTime d;
			if (!DateTime.TryParseExact(xmlNode.Value, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError<string>(0L, "Invalid IssuedTime entry {0}. Returning DateTime.MaxValue", xmlNode.Value);
				return ExDateTime.MaxValue;
			}
			if (usageRights.IsUsageRightGranted(ContentRight.Owner))
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "User has OWNER rights - trying to get the expiry time from owner node");
				XmlNode xmlNode2 = firstNodeXdoc.SelectSingleNode("XrML/BODY[@type=\"LICENSE\"]/WORK/RIGHTSGROUP[@name=\"Main-Rights\"]/RIGHTSLIST/OWNER/CONDITIONLIST/TIME/INTERVALTIME/@days");
				if (xmlNode2 != null)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug<string>(0L, "Found the owner node {0}", xmlNode2.Value);
					int num;
					if (int.TryParse(xmlNode2.Value, out num))
					{
						return new ExDateTime(ExTimeZone.TimeZoneFromKind(DateTimeKind.Utc), d + TimeSpan.FromDays((double)num));
					}
				}
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Couldn't find the OWNER node - either OWNER has no expiry or the value is not parsable");
				return ExDateTime.MaxValue;
			}
			if (usageRights.IsUsageRightGranted(ContentRight.View))
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "User has VIEW rights - trying to get the expiry time from VIEW node");
				XmlNode xmlNode3 = firstNodeXdoc.SelectSingleNode("XrML/BODY[@type=\"LICENSE\"]/WORK/RIGHTSGROUP[@name=\"Main-Rights\"]/RIGHTSLIST/VIEW/CONDITIONLIST/TIME/INTERVALTIME/@days");
				if (xmlNode3 != null)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug<string>(0L, "Found the view node {0}", xmlNode3.Value);
					int num2;
					if (int.TryParse(xmlNode3.Value, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2))
					{
						return new ExDateTime(ExTimeZone.TimeZoneFromKind(DateTimeKind.Utc), d + TimeSpan.FromDays((double)num2));
					}
				}
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Couldn't find the VIEW node - either VIEW rights has no expiry or the value is not parsable");
			}
			return ExDateTime.MaxValue;
		}

		// Token: 0x060068F5 RID: 26869 RVA: 0x001C17D8 File Offset: 0x001BF9D8
		public static LicenseeIdentity[] ConvertToLicenseeIdentities(RmsClientManagerContext context, string[] identities)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			RmsClientManagerUtils.ThrowOnNullOrEmptyArrayArgument("identities", identities);
			LicenseeIdentity[] array = new LicenseeIdentity[identities.Length];
			for (int i = 0; i < identities.Length; i++)
			{
				if (!SmtpAddress.IsValidSmtpAddress(identities[i]))
				{
					throw new RightsManagementException(RightsManagementFailureCode.InvalidRecipient, ServerStrings.RecipientAddressInvalid(identities[i]));
				}
				ADRawEntry adrawEntry;
				try
				{
					adrawEntry = context.ResolveRecipient(identities[i]);
				}
				catch (ADTransientException innerException)
				{
					throw new ExchangeConfigurationException(ServerStrings.FailedToReadUserConfig(identities[i]), innerException);
				}
				catch (ADOperationException innerException2)
				{
					throw new RightsManagementException(RightsManagementFailureCode.ADUserNotFound, ServerStrings.FailedToReadUserConfig(identities[i]), innerException2);
				}
				IList<string> list = RmsClientManagerUtils.EmptyProxyList;
				string text = null;
				bool flag = false;
				if (adrawEntry != null)
				{
					ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)adrawEntry[ADRecipientSchema.EmailAddresses];
					if (proxyAddressCollection != null && proxyAddressCollection.Count != 0)
					{
						list = new List<string>(proxyAddressCollection.Count);
						foreach (ProxyAddress proxyAddress in proxyAddressCollection)
						{
							list.Add(proxyAddress.ValueString);
						}
					}
					SmtpAddress smtpAddress = (SmtpAddress)adrawEntry[ADRecipientSchema.PrimarySmtpAddress];
					if (smtpAddress.IsValidAddress)
					{
						text = smtpAddress.ToString();
					}
					flag = RmsClientManagerUtils.TreatRecipientAsRMSSuperuser(context.OrgId, (RecipientTypeDetails)adrawEntry[ADRecipientSchema.RecipientTypeDetails]);
				}
				array[i] = new LicenseeIdentity(string.IsNullOrEmpty(text) ? identities[i] : text, list, flag);
			}
			return array;
		}

		// Token: 0x060068F6 RID: 26870 RVA: 0x001C1970 File Offset: 0x001BFB70
		internal static bool TreatRecipientAsRMSSuperuser(OrganizationId organizationId, RecipientTypeDetails userType)
		{
			return (userType & RecipientTypeDetails.DiscoveryMailbox) == RecipientTypeDetails.DiscoveryMailbox && RmsClientManager.IRMConfig.IsEDiscoverySuperUserEnabledForTenant(organizationId);
		}

		// Token: 0x060068F7 RID: 26871 RVA: 0x001C1990 File Offset: 0x001BFB90
		public static LicenseResponse[] GetLicenseResponses(UseLicenseResult[] endUseLicenses, Uri licenseUri)
		{
			RmsClientManagerUtils.ThrowOnNullOrEmptyArrayArgument("endUseLicenses", endUseLicenses);
			LicenseResponse[] array = new LicenseResponse[endUseLicenses.Length];
			for (int i = 0; i < endUseLicenses.Length; i++)
			{
				UseLicenseResult useLicenseResult = endUseLicenses[i];
				if (useLicenseResult.Error != null)
				{
					array[i] = new LicenseResponse(new RightsManagementException(RightsManagementFailureCode.OfflineRmsServerFailure, ServerStrings.FailedToAcquireUseLicense(licenseUri), useLicenseResult.Error));
				}
				else
				{
					ContentRight? usageRights;
					try
					{
						usageRights = new ContentRight?(DrmClientUtils.GetUsageRightsFromLicense(useLicenseResult.EndUseLicense));
					}
					catch (RightsManagementException arg)
					{
						Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError<int, RightsManagementException>(0L, "Failed to get usage rights from license for recipient index {0}. Error {1}", i, arg);
						usageRights = null;
					}
					array[i] = new LicenseResponse(useLicenseResult.EndUseLicense, usageRights);
				}
			}
			return array;
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x001C1A40 File Offset: 0x001BFC40
		public static ChannelFactory<IWSServerLicensingServiceChannel> CreateServerLicensingChannelFactory(Uri targetUri, EndpointAddress epa, LicenseIdentity identity, OrganizationId organizationId, int numLicenseRequests, IRmsLatencyTracker latencyTracker)
		{
			CustomBinding binding = RmsClientManagerUtils.CreateCustomBinding(organizationId, numLicenseRequests);
			ChannelFactory<IWSServerLicensingServiceChannel> channelFactory = new ChannelFactory<IWSServerLicensingServiceChannel>(binding, epa);
			channelFactory.Endpoint.Behaviors.Remove(typeof(ClientCredentials));
			channelFactory.Endpoint.Behaviors.Add(new SamlClientCredentials(identity, organizationId, targetUri, Offer.IPCServerLicensingSTS, latencyTracker));
			return channelFactory;
		}

		// Token: 0x060068F9 RID: 26873 RVA: 0x001C1A9C File Offset: 0x001BFC9C
		public static ChannelFactory<IWSCertificationServiceChannel> CreateCertificationChannelFactory(Uri targetUri, EndpointAddress epa, LicenseIdentity identity, OrganizationId organizationId, IRmsLatencyTracker latencyTracker)
		{
			CustomBinding binding = RmsClientManagerUtils.CreateCustomBinding(organizationId, 0);
			ChannelFactory<IWSCertificationServiceChannel> channelFactory = new ChannelFactory<IWSCertificationServiceChannel>(binding, epa);
			channelFactory.Endpoint.Behaviors.Remove(typeof(ClientCredentials));
			channelFactory.Endpoint.Behaviors.Add(new SamlClientCredentials(identity, organizationId, targetUri, Offer.IPCCertificationSTS, latencyTracker));
			return channelFactory;
		}

		// Token: 0x060068FA RID: 26874 RVA: 0x001C1AF4 File Offset: 0x001BFCF4
		public static bool ShouldMarkWCFErrorAsNegative(RightsManagementFailureCode failureCode)
		{
			return failureCode == RightsManagementFailureCode.ServiceNotFound || failureCode == RightsManagementFailureCode.ActionNotSupported;
		}

		// Token: 0x060068FB RID: 26875 RVA: 0x001C1B18 File Offset: 0x001BFD18
		public static Uri GetTargetUriFromResponse(Stream stream)
		{
			ArgumentValidator.ThrowIfNull("stream", stream);
			string text;
			try
			{
				long length = stream.Length;
				if (length > 51200L)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError<long>(0L, "Stream returned from MEx is larger than expected. Length = {0}. Ignoring stream", length);
					return null;
				}
				using (StreamReader streamReader = new StreamReader(stream))
				{
					text = streamReader.ReadToEnd();
				}
			}
			finally
			{
				if (stream != null)
				{
					((IDisposable)stream).Dispose();
				}
			}
			Exception ex = null;
			XDocument xdocument;
			try
			{
				xdocument = XDocument.Parse(text);
			}
			catch (XmlException ex2)
			{
				ex = ex2;
				return null;
			}
			catch (FormatException ex3)
			{
				ex = ex3;
				return null;
			}
			catch (NotSupportedException ex4)
			{
				ex = ex4;
				return null;
			}
			finally
			{
				if (ex != null)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError<Exception, string>(0L, "Failed to parse the document from the MEx page. Error {0}, Document {1}", ex, text);
				}
			}
			try
			{
				XNamespace ns = "http://schemas.xmlsoap.org/ws/2004/09/policy";
				IEnumerable<XElement> source = xdocument.Descendants(ns + "AppliesTo");
				if (!source.Any<XElement>())
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError(0L, "Failed to find the AppliesToElement in the MEx data");
					return null;
				}
				XElement xelement = null;
				XElement xelement2 = source.First<XElement>();
				if (xelement2 != null)
				{
					XNamespace ns2 = "http://www.w3.org/2005/08/addressing";
					IEnumerable<XElement> source2 = xelement2.Descendants(ns2 + "EndpointReference");
					if (!source2.Any<XElement>())
					{
						Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError(0L, "Failed to find the EndPointReferenceElements within the AppliesTo Element in the MEx data");
						return null;
					}
					xelement = source2.First<XElement>();
				}
				if (xelement != null)
				{
					Uri result = null;
					if (Uri.TryCreate(xelement.Value, UriKind.RelativeOrAbsolute, out result))
					{
						return result;
					}
				}
			}
			catch (XmlException arg)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError<XmlException, XDocument>(0L, "Failed while processing MEx XML data. Error {0}, XML {1}", arg, xdocument);
			}
			return null;
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x001C1D04 File Offset: 0x001BFF04
		public static string GetUniqueFileNameForProcess(string fileSuffix)
		{
			return RmsClientManagerUtils.GetUniqueFileNameForProcess(fileSuffix, false);
		}

		// Token: 0x060068FD RID: 26877 RVA: 0x001C1D10 File Offset: 0x001BFF10
		public static string GetUniqueFileNameForProcess(string fileSuffix, bool includeProcessId)
		{
			string result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				if (string.Equals(currentProcess.ProcessName, "w3wp", StringComparison.OrdinalIgnoreCase) && currentProcess.StartInfo != null && currentProcess.StartInfo.EnvironmentVariables != null && currentProcess.StartInfo.EnvironmentVariables.ContainsKey("APP_POOL_ID"))
				{
					string text = currentProcess.StartInfo.EnvironmentVariables["APP_POOL_ID"];
					if (!string.IsNullOrEmpty(text))
					{
						return string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", new object[]
						{
							"w3wp",
							text,
							fileSuffix
						});
					}
				}
				string text2;
				if (includeProcessId)
				{
					text2 = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", new object[]
					{
						currentProcess.ProcessName,
						currentProcess.Id,
						fileSuffix
					});
				}
				else
				{
					text2 = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
					{
						currentProcess.ProcessName,
						fileSuffix
					});
				}
				result = text2;
			}
			return result;
		}

		// Token: 0x060068FE RID: 26878 RVA: 0x001C1EE8 File Offset: 0x001C00E8
		public static AsyncCallback WrapCallbackWithUnhandledExceptionHandlerAndUpdatePoisonContext(AsyncCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return delegate(IAsyncResult asyncResult)
			{
				RightsManagementAsyncResult asyncResultRM = null;
				if (asyncResult != null && asyncResult.AsyncState != null)
				{
					asyncResultRM = (asyncResult.AsyncState as RightsManagementAsyncResult);
				}
				try
				{
					if (asyncResultRM != null)
					{
						asyncResultRM.InvokeSaveContextCallback();
					}
					callback(asyncResult);
				}
				catch (Exception exception)
				{
					ExWatson.SendReportAndCrashOnAnotherThread(exception, ReportOptions.None, delegate(Exception param0, int param1)
					{
						if (asyncResultRM != null)
						{
							asyncResultRM.InvokeSaveContextCallback();
						}
					}, null);
					throw;
				}
			};
		}

		// Token: 0x060068FF RID: 26879 RVA: 0x001C1FCC File Offset: 0x001C01CC
		public static CancelableAsyncCallback WrapCancellableCallbackWithUnhandledExceptionHandlerAndUpdatePoisonContext(CancelableAsyncCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return delegate(ICancelableAsyncResult asyncResult)
			{
				RightsManagementAsyncResult asyncResultRM = null;
				if (asyncResult != null && asyncResult.AsyncState != null)
				{
					asyncResultRM = (asyncResult.AsyncState as RightsManagementAsyncResult);
				}
				try
				{
					if (asyncResultRM != null)
					{
						asyncResultRM.InvokeSaveContextCallback();
					}
					callback(asyncResult);
				}
				catch (Exception exception)
				{
					ExWatson.SendReportAndCrashOnAnotherThread(exception, ReportOptions.None, delegate(Exception param0, int param1)
					{
						if (asyncResultRM != null)
						{
							asyncResultRM.InvokeSaveContextCallback();
						}
					}, null);
					throw;
				}
			};
		}

		// Token: 0x06006900 RID: 26880 RVA: 0x001C1FFC File Offset: 0x001C01FC
		public static WebProxy GetLocalServerProxy(bool isEndPointExternal)
		{
			Uri localServerProxyAddress = RmsClientManagerUtils.GetLocalServerProxyAddress();
			if (localServerProxyAddress == null)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "No proxy is configured. Using system defaults");
				return null;
			}
			if (isEndPointExternal)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug<Uri>(0L, "InternetWeb proxy is configured {0}. Using that to connect to external endpoint", localServerProxyAddress);
				return new WebProxy(localServerProxyAddress, true);
			}
			Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug<Uri>(0L, "InternetWeb proxy is configured {0} but the end point is internal. Connecting directly", localServerProxyAddress);
			return RmsClientManagerUtils.EmptyWebProxy;
		}

		// Token: 0x06006901 RID: 26881 RVA: 0x001C2060 File Offset: 0x001C0260
		internal static bool ExtractExternalOrgIdFromPublishingLicense(string publishingLicense, out Guid externalDirectoryOrgId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("publishingLicense", publishingLicense);
			externalDirectoryOrgId = Guid.Empty;
			try
			{
				XmlDocument firstNodeXdoc = RmsClientManagerUtils.GetFirstNodeXdoc(publishingLicense);
				if (firstNodeXdoc == null)
				{
					return false;
				}
				XmlNodeList xmlNodeList = firstNodeXdoc.SelectNodes("/XrML/BODY/ISSUEDPRINCIPALS/PRINCIPAL");
				if (xmlNodeList == null || xmlNodeList.Count == 0)
				{
					return false;
				}
				foreach (object obj in xmlNodeList[0])
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!(xmlNode.Name != "SECURITYLEVEL") && xmlNode.Attributes != null)
					{
						bool flag = false;
						string text = null;
						foreach (object obj2 in xmlNode.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj2;
							if (xmlAttribute.Name == "name" && xmlAttribute.Value == "Tenant-ID")
							{
								flag = true;
							}
							else if (xmlAttribute.Name == "value")
							{
								text = xmlAttribute.Value;
							}
						}
						if (flag && !string.IsNullOrEmpty(text))
						{
							return Guid.TryParse(text, out externalDirectoryOrgId);
						}
					}
				}
			}
			catch (XmlException arg)
			{
				Microsoft.Exchange.Diagnostics.Components.Transport.ExTraceGlobals.RightsManagementTracer.TraceError<XmlException>(0L, "Exception while extracting MSODS tenant ID from publishing license: {0}", arg);
			}
			return false;
		}

		// Token: 0x06006902 RID: 26882 RVA: 0x001C2284 File Offset: 0x001C0484
		private static ADOperationResult TryGetOrganizationIdFromExternalDirectoryOrgId(Guid externalDirectoryOrgId, out OrganizationId organizationId)
		{
			RmsClientManagerUtils.<>c__DisplayClass10 CS$<>8__locals1 = new RmsClientManagerUtils.<>c__DisplayClass10();
			CS$<>8__locals1.externalDirectoryOrgId = externalDirectoryOrgId;
			if (CS$<>8__locals1.externalDirectoryOrgId == Guid.Empty)
			{
				throw new ArgumentException("Guid.Empty is not a valid external directory org id", "externalDirectoryOrgId");
			}
			RmsClientManagerUtils.<>c__DisplayClass10 CS$<>8__locals2 = CS$<>8__locals1;
			OrganizationId localOrgId;
			organizationId = (localOrgId = null);
			CS$<>8__locals2.localOrgId = localOrgId;
			try
			{
				ADNotificationAdapter.RunADOperation(delegate()
				{
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, CS$<>8__locals1.externalDirectoryOrgId, 1406, "TryGetOrganizationIdFromExternalDirectoryOrgId", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\rightsmanagement\\RmsClientManagerUtils.cs");
					if (tenantConfigurationSession == null)
					{
						return;
					}
					Organization orgContainer = tenantConfigurationSession.GetOrgContainer();
					if (orgContainer != null)
					{
						CS$<>8__locals1.localOrgId = orgContainer.OrganizationId;
						return;
					}
					throw new DataSourceOperationException(ServerStrings.FailedToGetOrgContainer(CS$<>8__locals1.externalDirectoryOrgId));
				});
			}
			catch (TransientException ex)
			{
				RmsClientManager.TraceFail(0, Guid.Empty, "Cannot resolve the External OrgID {0} - exception is {1}", new object[]
				{
					CS$<>8__locals1.externalDirectoryOrgId,
					ex
				});
				return new ADOperationResult(ADOperationErrorCode.RetryableError, ex);
			}
			catch (CannotResolveExternalDirectoryOrganizationIdException ex2)
			{
				RmsClientManager.TraceFail(0, Guid.Empty, "Cannot resolve the External OrgID {0} - exception {1}", new object[]
				{
					CS$<>8__locals1.externalDirectoryOrgId,
					ex2
				});
				return new ADOperationResult(ADOperationErrorCode.RetryableError, ex2);
			}
			catch (DataSourceOperationException ex3)
			{
				RmsClientManager.TraceFail(0, Guid.Empty, "Cannot resolve the External OrgID {0} - exception {1}", new object[]
				{
					CS$<>8__locals1.externalDirectoryOrgId,
					ex3
				});
				return new ADOperationResult(ADOperationErrorCode.PermanentError, ex3);
			}
			organizationId = CS$<>8__locals1.localOrgId;
			return ADOperationResult.Success;
		}

		// Token: 0x06006903 RID: 26883 RVA: 0x001C23EC File Offset: 0x001C05EC
		private static Uri GetLocalServerProxyAddress()
		{
			Uri result;
			try
			{
				Server localServer = LocalServerCache.LocalServer;
				if (localServer != null && localServer.InternetWebProxy != null)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug<Uri>(0L, "Using custom InternetWebProxy {0}", localServer.InternetWebProxy);
					result = localServer.InternetWebProxy;
				}
				else
				{
					result = null;
				}
			}
			catch (ADTransientException innerException)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadLocalServer, innerException);
			}
			catch (ADOperationException innerException2)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadLocalServer, innerException2);
			}
			return result;
		}

		// Token: 0x06006904 RID: 26884 RVA: 0x001C246C File Offset: 0x001C066C
		private static CustomBinding CreateCustomBinding(OrganizationId organizationId, int numSecondsToAddInBaseTimeout)
		{
			ExternalAuthentication current = ExternalAuthentication.GetCurrent();
			SecurityTokenService securityTokenService = null;
			if (current.Enabled)
			{
				securityTokenService = current.GetSecurityTokenService(organizationId);
			}
			IssuedSecurityTokenParameters issuedSecurityTokenParameters = new IssuedSecurityTokenParameters
			{
				DefaultMessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10,
				KeyType = SecurityKeyType.SymmetricKey,
				RequireDerivedKeys = false
			};
			if (securityTokenService != null && securityTokenService.TokenIssuerEndpoint != null)
			{
				issuedSecurityTokenParameters.IssuerAddress = new EndpointAddress(securityTokenService.TokenIssuerEndpoint.ToString());
			}
			issuedSecurityTokenParameters.TokenType = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";
			WSFederationHttpBinding wsfederationHttpBinding = new WSFederationHttpBinding();
			Uri localServerProxyAddress = RmsClientManagerUtils.GetLocalServerProxyAddress();
			if (localServerProxyAddress != null)
			{
				wsfederationHttpBinding.ProxyAddress = localServerProxyAddress;
				wsfederationHttpBinding.UseDefaultWebProxy = false;
				wsfederationHttpBinding.BypassProxyOnLocal = true;
			}
			issuedSecurityTokenParameters.IssuerBinding = wsfederationHttpBinding;
			TransportSecurityBindingElement transportSecurityBindingElement = SecurityBindingElement.CreateIssuedTokenOverTransportBindingElement(issuedSecurityTokenParameters);
			transportSecurityBindingElement.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDes;
			transportSecurityBindingElement.KeyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;
			transportSecurityBindingElement.IncludeTimestamp = true;
			transportSecurityBindingElement.SecurityHeaderLayout = SecurityHeaderLayout.Strict;
			TextMessageEncodingBindingElement textMessageEncodingBindingElement = new TextMessageEncodingBindingElement
			{
				ReaderQuotas = 
				{
					MaxStringContentLength = 2097152
				}
			};
			HttpsTransportBindingElement httpsTransportBindingElement = new HttpsTransportBindingElement
			{
				AuthenticationScheme = AuthenticationSchemes.Anonymous,
				HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
				MaxReceivedMessageSize = 2097152L
			};
			if (localServerProxyAddress != null)
			{
				httpsTransportBindingElement.ProxyAddress = localServerProxyAddress;
				httpsTransportBindingElement.UseDefaultWebProxy = false;
				httpsTransportBindingElement.BypassProxyOnLocal = true;
			}
			CustomBinding customBinding = new CustomBinding(new BindingElement[]
			{
				transportSecurityBindingElement,
				textMessageEncodingBindingElement,
				httpsTransportBindingElement
			});
			TimeSpan timeSpan = Constants.BindingTimeout;
			if (numSecondsToAddInBaseTimeout > 0)
			{
				timeSpan = timeSpan.Add(TimeSpan.FromSeconds((double)numSecondsToAddInBaseTimeout));
			}
			customBinding.CloseTimeout = timeSpan;
			customBinding.OpenTimeout = timeSpan;
			customBinding.ReceiveTimeout = timeSpan;
			customBinding.SendTimeout = timeSpan;
			return customBinding;
		}

		// Token: 0x06006905 RID: 26885 RVA: 0x001C2618 File Offset: 0x001C0818
		private static bool ValidateDelegatedEmailAddressOrAliasNode(XmlNode delegatedNode)
		{
			if (delegatedNode == null || string.IsNullOrEmpty(delegatedNode.Value))
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError<XmlNode>(0L, "Failed to get the delegated email address node. UseLicense {0}", delegatedNode);
				return false;
			}
			if (SmtpAddress.IsValidSmtpAddress(delegatedNode.Value))
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug<string>(0L, "Found the delegated email address node. Address: {0}", delegatedNode.Value);
				return true;
			}
			Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceDebug<string>(0L, "The value in the delegated email address is not valid. Address: {0}", delegatedNode.Value);
			return false;
		}

		// Token: 0x06006906 RID: 26886 RVA: 0x001C2688 File Offset: 0x001C0888
		private static XmlDocument GetFirstNodeXdoc(string license)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("license", license);
			int num = license.IndexOf("<XrML", StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError(0L, "Could not find the XrML Begin tag");
				return null;
			}
			int num2 = license.IndexOf("</XrML>", StringComparison.OrdinalIgnoreCase);
			if (num2 == -1)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError(0L, "Could not find the XrML End tag");
				return null;
			}
			int length = num2 - num + "</XrML>".Length;
			string text = license.Substring(num, length);
			if (text.Length > 65536)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError(0L, "Invalid node - larger than expected");
				return null;
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			try
			{
				xmlDocument.LoadXml(text);
			}
			catch (XmlException arg)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Storage.ExTraceGlobals.RightsManagementTracer.TraceError<XmlException, string>(0L, "Failed to parse the license. Error {0}, license {1}", arg, license);
				return null;
			}
			return xmlDocument;
		}

		// Token: 0x04003BA6 RID: 15270
		private const int MaxUseLicenseNodeLength = 65536;

		// Token: 0x04003BA7 RID: 15271
		private const string AppliesToElement = "AppliesTo";

		// Token: 0x04003BA8 RID: 15272
		private const string EndpointReferenceElement = "EndpointReference";

		// Token: 0x04003BA9 RID: 15273
		private const string IISWorkerProcess = "w3wp";

		// Token: 0x04003BAA RID: 15274
		private const string AppPoolId = "APP_POOL_ID";

		// Token: 0x04003BAB RID: 15275
		private const string DelegatedEmailAddressXPath = "XrML/BODY[@type=\"LICENSE\"]/ISSUEDPRINCIPALS/PRINCIPAL/OBJECT[@type=\"Group-Identity\"]/ADDRESS[@type=\"DelegatedEmailAddress\"]/text()";

		// Token: 0x04003BAC RID: 15276
		private const string EmailAddressAliasXPath = "XrML/BODY[@type=\"LICENSE\"]/ISSUEDPRINCIPALS/PRINCIPAL/OBJECT[@type=\"Group-Identity\"]/ADDRESS[@type=\"email_alias\"]/text()";

		// Token: 0x04003BAD RID: 15277
		private const string IssuedTimeXPath = "XrML/BODY[@type=\"LICENSE\"]/ISSUEDTIME/text()";

		// Token: 0x04003BAE RID: 15278
		private const string ViewNodeXPath = "XrML/BODY[@type=\"LICENSE\"]/WORK/RIGHTSGROUP[@name=\"Main-Rights\"]/RIGHTSLIST/VIEW/CONDITIONLIST/TIME/INTERVALTIME/@days";

		// Token: 0x04003BAF RID: 15279
		private const string OwnerNodeXPath = "XrML/BODY[@type=\"LICENSE\"]/WORK/RIGHTSGROUP[@name=\"Main-Rights\"]/RIGHTSLIST/OWNER/CONDITIONLIST/TIME/INTERVALTIME/@days";

		// Token: 0x04003BB0 RID: 15280
		private const string PrincipalNodeXPath = "/XrML/BODY/ISSUEDPRINCIPALS/PRINCIPAL";

		// Token: 0x04003BB1 RID: 15281
		private const string IssuedTimeFormat = "yyyy-MM-ddTHH:mm";

		// Token: 0x04003BB2 RID: 15282
		private const string XrmlBeginTag = "<XrML";

		// Token: 0x04003BB3 RID: 15283
		private const string XrmlEndTag = "</XrML>";

		// Token: 0x04003BB4 RID: 15284
		private const int MaxProxyAddressInDelegationToken = 50;

		// Token: 0x04003BB5 RID: 15285
		private static readonly WebProxy EmptyWebProxy = new WebProxy();

		// Token: 0x04003BB6 RID: 15286
		private static readonly IList<string> EmptyProxyList = new List<string>(0);
	}
}
