using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Autodiscover.ConfigurationSettings;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000087 RID: 135
	[MessageContract]
	public abstract class AutodiscoverRequestMessage
	{
		// Token: 0x0600037F RID: 895 RVA: 0x00015D8C File Offset: 0x00013F8C
		public AutodiscoverRequestMessage()
		{
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00015D94 File Offset: 0x00013F94
		internal static bool ValidateRequest<T>(AutodiscoverRequest request, Collection<T> identities, RequestedSettingCollection requestedSettingCollection, ExchangeVersion? requestedVersion, LazyMember<int> maxIdentities, string maxIdentitiesString, out string errorMessage, out ExchangeVersion requestVersion)
		{
			bool result = false;
			if (!AutodiscoverRequestMessage.TryGetRequestVersion(out requestVersion))
			{
				errorMessage = Strings.MissingOrInvalidRequestedServerVersion;
			}
			else if (request == null)
			{
				errorMessage = Strings.InvalidRequest;
			}
			else if (identities == null || identities.Count == 0)
			{
				errorMessage = Strings.NoUsersRequested;
			}
			else if (requestedSettingCollection == null || requestedSettingCollection.Count == 0)
			{
				errorMessage = Strings.NoSettingsRequested;
			}
			else if (identities.Count > maxIdentities.Member)
			{
				errorMessage = string.Format(maxIdentitiesString, maxIdentities.Member);
			}
			else if (requestedVersion != null && !AutodiscoverRequestMessage.IsValidExchangeRequestedVersion(requestedVersion.Value))
			{
				errorMessage = Strings.InvalidRequestedVersion;
			}
			else
			{
				errorMessage = Strings.NoError;
				result = true;
			}
			return result;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00015E64 File Offset: 0x00014064
		// (set) Token: 0x06000382 RID: 898 RVA: 0x00015E6C File Offset: 0x0001406C
		[MessageHeader(MustUnderstand = true)]
		public string RequestedServerVersion { get; set; }

		// Token: 0x06000383 RID: 899 RVA: 0x00015E78 File Offset: 0x00014078
		internal static bool HasBinarySecretHeader(out string binarySecretHeader)
		{
			binarySecretHeader = null;
			int num = 0;
			MessageHeaders incomingMessageHeaders = OperationContext.Current.IncomingMessageHeaders;
			try
			{
				num = incomingMessageHeaders.FindHeader("BinarySecret", "http://schemas.microsoft.com/exchange/2010/Autodiscover");
			}
			catch (MessageHeaderException)
			{
				return false;
			}
			if (num < 0)
			{
				return false;
			}
			binarySecretHeader = incomingMessageHeaders.GetHeader<string>(num);
			return true;
		}

		// Token: 0x06000384 RID: 900
		internal abstract AutodiscoverResponseMessage Execute();

		// Token: 0x06000385 RID: 901 RVA: 0x00015ED0 File Offset: 0x000140D0
		private static bool IsValidExchangeRequestedVersion(ExchangeVersion version)
		{
			switch (version)
			{
			case ExchangeVersion.Exchange2010:
			case ExchangeVersion.Exchange2010_SP1:
			case ExchangeVersion.Exchange2010_SP2:
			case ExchangeVersion.Exchange2012:
			case ExchangeVersion.Exchange2013:
			case ExchangeVersion.Exchange2013_SP1:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00015F04 File Offset: 0x00014104
		private static bool TryGetRequestVersion(out ExchangeVersion requestVersion)
		{
			requestVersion = ExchangeVersion.Exchange2010;
			bool result;
			try
			{
				MessageHeaders incomingMessageHeaders = OperationContext.Current.IncomingMessageHeaders;
				string header = incomingMessageHeaders.GetHeader<string>("RequestedServerVersion", "http://schemas.microsoft.com/exchange/2010/Autodiscover");
				AutodiscoverRequestMessage.RemapEquivalentRequestedExchangeVersion(ref header);
				result = EnumValidator<ExchangeVersion>.TryParse(header, EnumParseOptions.Default, out requestVersion);
			}
			catch (MessageHeaderException)
			{
				result = false;
			}
			catch (SerializationException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00015F6C File Offset: 0x0001416C
		private static void RemapEquivalentRequestedExchangeVersion(ref string version)
		{
			string text;
			if (!string.IsNullOrEmpty(version) && AutodiscoverRequestMessage.equivalentRequestedExchangeVersionStrings.TryGetValue(version, out text))
			{
				version = text;
			}
		}

		// Token: 0x04000317 RID: 791
		private static Dictionary<string, string> equivalentRequestedExchangeVersionStrings = new Dictionary<string, string>(1)
		{
			{
				"Exchange2012",
				"Exchange2013"
			}
		};

		// Token: 0x04000318 RID: 792
		internal static LazyMember<List<UserConfigurationSettingName>> RestrictedSettings = new LazyMember<List<UserConfigurationSettingName>>(delegate()
		{
			List<UserConfigurationSettingName> list = new List<UserConfigurationSettingName>();
			if (Common.IsPartnerHostedOnly || VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.RestrictedSettings.Enabled)
			{
				list.Add(UserConfigurationSettingName.ActiveDirectoryServer);
				list.Add(UserConfigurationSettingName.InternalOABUrl);
				list.Add(UserConfigurationSettingName.InternalUMUrl);
				list.Add(UserConfigurationSettingName.InternalPop3Connections);
				list.Add(UserConfigurationSettingName.InternalImap4Connections);
				list.Add(UserConfigurationSettingName.InternalSmtpConnections);
				list.Add(UserConfigurationSettingName.InternalWebClientUrls);
				list.Add(UserConfigurationSettingName.InternalEwsUrl);
				list.Add(UserConfigurationSettingName.InternalEmwsUrl);
				list.Add(UserConfigurationSettingName.InternalEcpUrl);
				list.Add(UserConfigurationSettingName.InternalEcpConnectUrl);
				list.Add(UserConfigurationSettingName.InternalEcpPhotoUrl);
				list.Add(UserConfigurationSettingName.InternalEcpDeliveryReportUrl);
				list.Add(UserConfigurationSettingName.InternalEcpEmailSubscriptionsUrl);
				list.Add(UserConfigurationSettingName.InternalEcpPublishingUrl);
				list.Add(UserConfigurationSettingName.InternalEcpRetentionPolicyTagsUrl);
				list.Add(UserConfigurationSettingName.InternalEcpTextMessagingUrl);
				list.Add(UserConfigurationSettingName.InternalEcpVoicemailUrl);
				list.Add(UserConfigurationSettingName.InternalEcpExtensionInstallationUrl);
				list.Add(UserConfigurationSettingName.InternalPhotosUrl);
				list.Add(UserConfigurationSettingName.PublicFolderServer);
			}
			return list;
		});
	}
}
