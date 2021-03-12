using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000006 RID: 6
	internal static class Strings
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002D70 File Offset: 0x00000F70
		static Strings()
		{
			Strings.stringIDs.Add(1005127777U, "NoError");
			Strings.stringIDs.Add(467439996U, "OutlookAddressNotFound");
			Strings.stringIDs.Add(2670476165U, "ExternalUrlNotFound");
			Strings.stringIDs.Add(3070945727U, "RedirectUrlForUser");
			Strings.stringIDs.Add(2828903911U, "InvalidBinarySecretHeader");
			Strings.stringIDs.Add(4170881310U, "MobileSyncBadAddress");
			Strings.stringIDs.Add(2743507733U, "ActiveDirectoryFailure");
			Strings.stringIDs.Add(1980408852U, "NoSettingsRequested");
			Strings.stringIDs.Add(813325346U, "SettingIsNotAvailable");
			Strings.stringIDs.Add(3025903418U, "InternalServerError");
			Strings.stringIDs.Add(934963490U, "RedirectAddress");
			Strings.stringIDs.Add(3758180504U, "OutlookBadAddress");
			Strings.stringIDs.Add(3863897719U, "ProviderIsNotAvailable");
			Strings.stringIDs.Add(3959337510U, "InvalidRequest");
			Strings.stringIDs.Add(2553117850U, "MobileSyncAddressNotFound");
			Strings.stringIDs.Add(2888966445U, "NotFederated");
			Strings.stringIDs.Add(1987653008U, "OutlookInvalidEMailAddress");
			Strings.stringIDs.Add(3403459873U, "InvalidDomain");
			Strings.stringIDs.Add(3720978267U, "MobileMailBoxNotFound");
			Strings.stringIDs.Add(554711641U, "MaxDomainsPerGetDomainSettingsRequestExceeded");
			Strings.stringIDs.Add(163658367U, "OutlookServerTooOld");
			Strings.stringIDs.Add(3929050450U, "InvalidUser");
			Strings.stringIDs.Add(3986215214U, "InvalidUserSetting");
			Strings.stringIDs.Add(1668664063U, "NoUsersRequested");
			Strings.stringIDs.Add(2706529289U, "InvalidPartnerTokenRequest");
			Strings.stringIDs.Add(2878595352U, "ServerBusy");
			Strings.stringIDs.Add(12114365U, "MaxUsersPerGetUserSettingsRequestExceeded");
			Strings.stringIDs.Add(2779025613U, "RedirectUrl");
			Strings.stringIDs.Add(1022239443U, "InvalidRequestedVersion");
			Strings.stringIDs.Add(2676368715U, "ADUnavailable");
			Strings.stringIDs.Add(2120454641U, "SoapEndpointNotSupported");
			Strings.stringIDs.Add(964474549U, "NoSettingsToReturn");
			Strings.stringIDs.Add(1797171863U, "MissingOrInvalidRequestedServerVersion");
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003040 File Offset: 0x00001240
		public static LocalizedString NoError
		{
			get
			{
				return new LocalizedString("NoError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000305E File Offset: 0x0000125E
		public static LocalizedString OutlookAddressNotFound
		{
			get
			{
				return new LocalizedString("OutlookAddressNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000307C File Offset: 0x0000127C
		public static LocalizedString ExternalUrlNotFound
		{
			get
			{
				return new LocalizedString("ExternalUrlNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000309A File Offset: 0x0000129A
		public static LocalizedString RedirectUrlForUser
		{
			get
			{
				return new LocalizedString("RedirectUrlForUser", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000030B8 File Offset: 0x000012B8
		public static LocalizedString InvalidBinarySecretHeader
		{
			get
			{
				return new LocalizedString("InvalidBinarySecretHeader", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000030D6 File Offset: 0x000012D6
		public static LocalizedString MobileSyncBadAddress
		{
			get
			{
				return new LocalizedString("MobileSyncBadAddress", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000030F4 File Offset: 0x000012F4
		public static LocalizedString ActiveDirectoryFailure
		{
			get
			{
				return new LocalizedString("ActiveDirectoryFailure", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003112 File Offset: 0x00001312
		public static LocalizedString NoSettingsRequested
		{
			get
			{
				return new LocalizedString("NoSettingsRequested", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003130 File Offset: 0x00001330
		public static LocalizedString SettingIsNotAvailable
		{
			get
			{
				return new LocalizedString("SettingIsNotAvailable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000314E File Offset: 0x0000134E
		public static LocalizedString InternalServerError
		{
			get
			{
				return new LocalizedString("InternalServerError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000316C File Offset: 0x0000136C
		public static LocalizedString RedirectAddress
		{
			get
			{
				return new LocalizedString("RedirectAddress", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000318A File Offset: 0x0000138A
		public static LocalizedString OutlookBadAddress
		{
			get
			{
				return new LocalizedString("OutlookBadAddress", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000031A8 File Offset: 0x000013A8
		public static LocalizedString ProviderIsNotAvailable
		{
			get
			{
				return new LocalizedString("ProviderIsNotAvailable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000031C6 File Offset: 0x000013C6
		public static LocalizedString InvalidRequest
		{
			get
			{
				return new LocalizedString("InvalidRequest", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000031E4 File Offset: 0x000013E4
		public static LocalizedString MobileSyncAddressNotFound
		{
			get
			{
				return new LocalizedString("MobileSyncAddressNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003202 File Offset: 0x00001402
		public static LocalizedString NotFederated
		{
			get
			{
				return new LocalizedString("NotFederated", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003220 File Offset: 0x00001420
		public static LocalizedString OutlookInvalidEMailAddress
		{
			get
			{
				return new LocalizedString("OutlookInvalidEMailAddress", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000323E File Offset: 0x0000143E
		public static LocalizedString InvalidDomain
		{
			get
			{
				return new LocalizedString("InvalidDomain", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000325C File Offset: 0x0000145C
		public static LocalizedString MobileMailBoxNotFound
		{
			get
			{
				return new LocalizedString("MobileMailBoxNotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000327A File Offset: 0x0000147A
		public static LocalizedString MaxDomainsPerGetDomainSettingsRequestExceeded
		{
			get
			{
				return new LocalizedString("MaxDomainsPerGetDomainSettingsRequestExceeded", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003298 File Offset: 0x00001498
		public static LocalizedString OutlookServerTooOld
		{
			get
			{
				return new LocalizedString("OutlookServerTooOld", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000032B6 File Offset: 0x000014B6
		public static LocalizedString InvalidUser
		{
			get
			{
				return new LocalizedString("InvalidUser", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000032D4 File Offset: 0x000014D4
		public static LocalizedString InvalidUserSetting
		{
			get
			{
				return new LocalizedString("InvalidUserSetting", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000032F2 File Offset: 0x000014F2
		public static LocalizedString NoUsersRequested
		{
			get
			{
				return new LocalizedString("NoUsersRequested", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003310 File Offset: 0x00001510
		public static LocalizedString InvalidPartnerTokenRequest
		{
			get
			{
				return new LocalizedString("InvalidPartnerTokenRequest", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000332E File Offset: 0x0000152E
		public static LocalizedString ServerBusy
		{
			get
			{
				return new LocalizedString("ServerBusy", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000334C File Offset: 0x0000154C
		public static LocalizedString MaxUsersPerGetUserSettingsRequestExceeded
		{
			get
			{
				return new LocalizedString("MaxUsersPerGetUserSettingsRequestExceeded", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000336A File Offset: 0x0000156A
		public static LocalizedString RedirectUrl
		{
			get
			{
				return new LocalizedString("RedirectUrl", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003388 File Offset: 0x00001588
		public static LocalizedString InvalidRequestedVersion
		{
			get
			{
				return new LocalizedString("InvalidRequestedVersion", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000033A6 File Offset: 0x000015A6
		public static LocalizedString ADUnavailable
		{
			get
			{
				return new LocalizedString("ADUnavailable", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000033C4 File Offset: 0x000015C4
		public static LocalizedString SoapEndpointNotSupported
		{
			get
			{
				return new LocalizedString("SoapEndpointNotSupported", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000033E2 File Offset: 0x000015E2
		public static LocalizedString NoSettingsToReturn
		{
			get
			{
				return new LocalizedString("NoSettingsToReturn", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003400 File Offset: 0x00001600
		public static LocalizedString MissingOrInvalidRequestedServerVersion
		{
			get
			{
				return new LocalizedString("MissingOrInvalidRequestedServerVersion", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000341E File Offset: 0x0000161E
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400001B RID: 27
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(33);

		// Token: 0x0400001C RID: 28
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Autodiscover.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000007 RID: 7
		public enum IDs : uint
		{
			// Token: 0x0400001E RID: 30
			NoError = 1005127777U,
			// Token: 0x0400001F RID: 31
			OutlookAddressNotFound = 467439996U,
			// Token: 0x04000020 RID: 32
			ExternalUrlNotFound = 2670476165U,
			// Token: 0x04000021 RID: 33
			RedirectUrlForUser = 3070945727U,
			// Token: 0x04000022 RID: 34
			InvalidBinarySecretHeader = 2828903911U,
			// Token: 0x04000023 RID: 35
			MobileSyncBadAddress = 4170881310U,
			// Token: 0x04000024 RID: 36
			ActiveDirectoryFailure = 2743507733U,
			// Token: 0x04000025 RID: 37
			NoSettingsRequested = 1980408852U,
			// Token: 0x04000026 RID: 38
			SettingIsNotAvailable = 813325346U,
			// Token: 0x04000027 RID: 39
			InternalServerError = 3025903418U,
			// Token: 0x04000028 RID: 40
			RedirectAddress = 934963490U,
			// Token: 0x04000029 RID: 41
			OutlookBadAddress = 3758180504U,
			// Token: 0x0400002A RID: 42
			ProviderIsNotAvailable = 3863897719U,
			// Token: 0x0400002B RID: 43
			InvalidRequest = 3959337510U,
			// Token: 0x0400002C RID: 44
			MobileSyncAddressNotFound = 2553117850U,
			// Token: 0x0400002D RID: 45
			NotFederated = 2888966445U,
			// Token: 0x0400002E RID: 46
			OutlookInvalidEMailAddress = 1987653008U,
			// Token: 0x0400002F RID: 47
			InvalidDomain = 3403459873U,
			// Token: 0x04000030 RID: 48
			MobileMailBoxNotFound = 3720978267U,
			// Token: 0x04000031 RID: 49
			MaxDomainsPerGetDomainSettingsRequestExceeded = 554711641U,
			// Token: 0x04000032 RID: 50
			OutlookServerTooOld = 163658367U,
			// Token: 0x04000033 RID: 51
			InvalidUser = 3929050450U,
			// Token: 0x04000034 RID: 52
			InvalidUserSetting = 3986215214U,
			// Token: 0x04000035 RID: 53
			NoUsersRequested = 1668664063U,
			// Token: 0x04000036 RID: 54
			InvalidPartnerTokenRequest = 2706529289U,
			// Token: 0x04000037 RID: 55
			ServerBusy = 2878595352U,
			// Token: 0x04000038 RID: 56
			MaxUsersPerGetUserSettingsRequestExceeded = 12114365U,
			// Token: 0x04000039 RID: 57
			RedirectUrl = 2779025613U,
			// Token: 0x0400003A RID: 58
			InvalidRequestedVersion = 1022239443U,
			// Token: 0x0400003B RID: 59
			ADUnavailable = 2676368715U,
			// Token: 0x0400003C RID: 60
			SoapEndpointNotSupported = 2120454641U,
			// Token: 0x0400003D RID: 61
			NoSettingsToReturn = 964474549U,
			// Token: 0x0400003E RID: 62
			MissingOrInvalidRequestedServerVersion = 1797171863U
		}
	}
}
