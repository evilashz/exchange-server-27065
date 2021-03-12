using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Configuration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000C4 RID: 196
	internal sealed class AggregatedBaseConfiguration : ConfigurationBase
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x0001A10C File Offset: 0x0001830C
		public static AggregatedBaseConfiguration ConfigurationFromData(OwaConfigurationBaseData data)
		{
			AllowOfflineOnEnum allowOfflineOn;
			Enum.TryParse<AllowOfflineOnEnum>(data.AllowOfflineOn, out allowOfflineOn);
			InstantMessagingTypeOptions instantMessagingType;
			Enum.TryParse<InstantMessagingTypeOptions>(data.InstantMessagingType, out instantMessagingType);
			OutboundCharsetOptions outboundCharset;
			Enum.TryParse<OutboundCharsetOptions>(data.OutboundCharset, out outboundCharset);
			return new AggregatedBaseConfiguration
			{
				AllowCopyContactsToDeviceAddressBook = data.AllowCopyContactsToDeviceAddressBook,
				AllowOfflineOn = allowOfflineOn,
				AttachmentPolicy = AggregatedBaseConfiguration.AttachmentPolicyFromData(data.AttachmentPolicy),
				DefaultTheme = data.DefaultTheme,
				InstantMessagingEnabled = data.InstantMessagingEnabled,
				InstantMessagingType = instantMessagingType,
				OutboundCharset = outboundCharset,
				PlacesEnabled = data.PlacesEnabled,
				WeatherEnabled = data.WeatherEnabled,
				RecoverDeletedItemsEnabled = data.RecoverDeletedItemsEnabled,
				SegmentationFlags = data.SegmentationFlags,
				UseGB18030 = data.UseGB18030,
				UseISO885915 = data.UseISO885915
			};
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001A1F0 File Offset: 0x000183F0
		public static OwaConfigurationBaseData DataFromConfiguration(ConfigurationBase config)
		{
			return new OwaConfigurationBaseData
			{
				AttachmentPolicy = config.AttachmentPolicy.PolicyData,
				AllowCopyContactsToDeviceAddressBook = config.AllowCopyContactsToDeviceAddressBook,
				AllowOfflineOn = config.AllowOfflineOn.ToString(),
				DefaultTheme = config.DefaultTheme,
				InstantMessagingEnabled = config.InstantMessagingEnabled,
				InstantMessagingType = config.InstantMessagingType.ToString(),
				OutboundCharset = config.OutboundCharset.ToString(),
				PlacesEnabled = config.PlacesEnabled,
				WeatherEnabled = config.WeatherEnabled,
				RecoverDeletedItemsEnabled = config.RecoverDeletedItemsEnabled,
				SegmentationFlags = config.SegmentationFlags,
				UseGB18030 = config.UseGB18030,
				UseISO885915 = config.UseISO885915
			};
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001A2C8 File Offset: 0x000184C8
		public static AttachmentPolicy AttachmentPolicyFromData(OwaAttachmentPolicyData data)
		{
			AttachmentPolicyLevel treatUnknownTypeAs;
			Enum.TryParse<AttachmentPolicyLevel>(data.TreatUnknownTypeAs, out treatUnknownTypeAs);
			return new AttachmentPolicy(data.BlockFileTypes, data.BlockMimeTypes, data.ForceSaveFileTypes, data.ForceSaveMimeTypes, data.AllowFileTypes, data.AllowMimeTypes, treatUnknownTypeAs, data.DirectFileAccessOnPublicComputersEnabled, data.DirectFileAccessOnPrivateComputersEnabled, data.ForceWacViewingFirstOnPublicComputers, data.ForceWacViewingFirstOnPrivateComputers, data.WacViewingOnPublicComputersEnabled, data.WacViewingOnPrivateComputersEnabled, data.ForceWebReadyDocumentViewingFirstOnPublicComputers, data.ForceWebReadyDocumentViewingFirstOnPrivateComputers, data.WebReadyDocumentViewingOnPublicComputersEnabled, data.WebReadyDocumentViewingOnPrivateComputersEnabled, data.WebReadyFileTypes, data.WebReadyMimeTypes, data.WebReadyDocumentViewingSupportedFileTypes, data.WebReadyDocumentViewingSupportedMimeTypes, data.WebReadyDocumentViewingForAllSupportedTypes);
		}
	}
}
