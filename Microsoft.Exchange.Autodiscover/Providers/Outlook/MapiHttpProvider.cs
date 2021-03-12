using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Autodiscover.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Autodiscover.Providers.Outlook
{
	// Token: 0x0200004F RID: 79
	internal class MapiHttpProvider
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000DA36 File Offset: 0x0000BC36
		public MapiHttpProvider()
		{
			this.loggingStrategy = null;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000DA45 File Offset: 0x0000BC45
		public MapiHttpProvider(Action<string, object> loggingStrategy)
		{
			this.loggingStrategy = loggingStrategy;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000DA54 File Offset: 0x0000BC54
		public bool ShouldWriteMapiHttpProtocolNode(UserConfigurationSettings settings, int clientMapiHttpResponseVersion, bool? mapiHttpOverrideRegistryValue, Version minimumMapiHttpAutodiscoverVersion, bool useMapiHttpADSettingFlightEnabled, bool mapiHttpFlightEnabled, bool mapiHttpForOutlook14FlightEnabled, Version callingOutlookVersion)
		{
			bool flag = false;
			bool flag2 = false;
			if (clientMapiHttpResponseVersion > 0)
			{
				if (mapiHttpOverrideRegistryValue != null)
				{
					flag = mapiHttpOverrideRegistryValue.Value;
					if (flag)
					{
						flag = this.VerifyMapiHttpServerVersion(settings, minimumMapiHttpAutodiscoverVersion, "MapiHttpRegKeySetting");
					}
					else
					{
						this.Log("ForceDisabled(Registry)");
					}
				}
				else if (useMapiHttpADSettingFlightEnabled)
				{
					bool? flag3 = settings.Get<bool?>(UserConfigurationSettingName.MapiHttpEnabledForUser);
					flag = ((flag3 != null) ? flag3.Value : settings.Get<bool>(UserConfigurationSettingName.MapiHttpEnabled));
					if (flag)
					{
						flag = this.VerifyMapiHttpServerVersion(settings, MapiHttpProvider.MinimumMapiHttpServerVersion, "MapiHttpADSetting");
					}
					else
					{
						this.Log("ForceDisabled(AD)");
					}
				}
				else if (callingOutlookVersion != null)
				{
					if (callingOutlookVersion.Major == 14)
					{
						flag = mapiHttpForOutlook14FlightEnabled;
						this.Log(flag ? "Flighted(Outlook 14)" : "FlightedDisabled(Outlook 14)");
					}
					else if (callingOutlookVersion.Major >= 15)
					{
						flag = mapiHttpFlightEnabled;
						this.Log(flag ? "Flighted" : "FlightedDisabled");
					}
					else
					{
						this.Log("VersionTooLow");
					}
				}
				else
				{
					this.Log("ParseFailed");
				}
				if (flag)
				{
					MapiHttpProtocolUrls mapiHttpProtocolUrls = settings.Get<MapiHttpProtocolUrls>(UserConfigurationSettingName.MapiHttpUrls);
					flag2 = (mapiHttpProtocolUrls != null && mapiHttpProtocolUrls.HasUrls);
				}
			}
			else
			{
				this.Log("NotRequested");
			}
			return flag && flag2;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000DB88 File Offset: 0x0000BD88
		private bool VerifyMapiHttpServerVersion(UserConfigurationSettings settings, Version minimumVersion, string logPrefix)
		{
			Version version = new Version(settings.Get<string>(UserConfigurationSettingName.MailboxVersion));
			bool flag = version >= minimumVersion;
			string value = string.Format("{0}({1}): Compared (minimum: {2}; target: {3})", new object[]
			{
				flag ? "Enabled" : "IncompatibleVersion",
				logPrefix,
				minimumVersion.ToString(),
				version.ToString()
			});
			this.Log(value);
			return flag;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
		private void Log(object value)
		{
			if (this.loggingStrategy != null)
			{
				this.loggingStrategy("MapiHttpEnabledSource", value);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		public bool TryWriteMapiHttpNodes(UserConfigurationSettings settings, XmlWriter xmlFragment, int clientMapiHttpResponseVersion, ClientAccessModes clientAccessMode)
		{
			MapiHttpProtocolUrls mapiHttpProtocolUrls = settings.Get<MapiHttpProtocolUrls>(UserConfigurationSettingName.MapiHttpUrls);
			bool flag = mapiHttpProtocolUrls != null && mapiHttpProtocolUrls.HasUrls;
			if (flag)
			{
				xmlFragment.WriteStartElement("Protocol", "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a");
				xmlFragment.WriteAttributeString("Type", null, "mapiHttp");
				xmlFragment.WriteAttributeString("Version", null, Math.Min(clientMapiHttpResponseVersion, 1).ToString());
				this.WriteMapiHttpProtocolUrls(clientAccessMode, MapiHttpProtocolUrls.Protocol.Emsmdb, mapiHttpProtocolUrls, xmlFragment);
				this.WriteMapiHttpProtocolUrls(clientAccessMode, MapiHttpProtocolUrls.Protocol.Nspi, mapiHttpProtocolUrls, xmlFragment);
				xmlFragment.WriteEndElement();
				return true;
			}
			return false;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000DC90 File Offset: 0x0000BE90
		private void WriteMapiHttpProtocolUrls(ClientAccessModes clientAccessMode, MapiHttpProtocolUrls.Protocol protocol, MapiHttpProtocolUrls urls, XmlWriter xmlFragment)
		{
			xmlFragment.WriteStartElement(MapiHttpProvider.ProtocolNameToElementName[protocol], "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a");
			Uri uri;
			if ((clientAccessMode & ClientAccessModes.InternalAccess) != ClientAccessModes.None && urls.TryGetProtocolUrl(ClientAccessType.Internal, protocol, out uri))
			{
				xmlFragment.WriteElementString("InternalUrl", "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a", uri.ToString());
			}
			if ((clientAccessMode & ClientAccessModes.ExternalAccess) != ClientAccessModes.None && urls.TryGetProtocolUrl(ClientAccessType.External, protocol, out uri))
			{
				xmlFragment.WriteElementString("ExternalUrl", "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a", uri.ToString());
			}
			xmlFragment.WriteEndElement();
		}

		// Token: 0x04000250 RID: 592
		public const int ServerMapiHttpResponseVersion = 1;

		// Token: 0x04000251 RID: 593
		private const string MapiHttpAttributeName = "mapiHttp";

		// Token: 0x04000252 RID: 594
		private const string VersionAttributeName = "Version";

		// Token: 0x04000253 RID: 595
		private const string EmsmdbElementName = "MailStore";

		// Token: 0x04000254 RID: 596
		private const string NspiElementName = "AddressBook";

		// Token: 0x04000255 RID: 597
		private const string InternalUrlElementName = "InternalUrl";

		// Token: 0x04000256 RID: 598
		private const string ExternalUrlElementName = "ExternalUrl";

		// Token: 0x04000257 RID: 599
		private const string LoggingKey = "MapiHttpEnabledSource";

		// Token: 0x04000258 RID: 600
		public static readonly Version MinimumMapiHttpServerVersion = new Version(15, 0, 847, 0);

		// Token: 0x04000259 RID: 601
		private static readonly Dictionary<MapiHttpProtocolUrls.Protocol, string> ProtocolNameToElementName = new Dictionary<MapiHttpProtocolUrls.Protocol, string>
		{
			{
				MapiHttpProtocolUrls.Protocol.Emsmdb,
				"MailStore"
			},
			{
				MapiHttpProtocolUrls.Protocol.Nspi,
				"AddressBook"
			}
		};

		// Token: 0x0400025A RID: 602
		private readonly Action<string, object> loggingStrategy;
	}
}
