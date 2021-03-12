using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000058 RID: 88
	internal static class Strings
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x00006E44 File Offset: 0x00005044
		public static LocalizedString ErrorWLCDPartnerAccessException(string url, string message)
		{
			return new LocalizedString("ErrorWLCDPartnerAccessException", "ExE444F3", false, true, Strings.ResourceManager, new object[]
			{
				url,
				message
			});
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00006E78 File Offset: 0x00005078
		public static LocalizedString ErrorCertificateHasExpired(string certSubject)
		{
			return new LocalizedString("ErrorCertificateHasExpired", "Ex57D6EE", false, true, Strings.ResourceManager, new object[]
			{
				certSubject
			});
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00006EA8 File Offset: 0x000050A8
		public static LocalizedString ErrorInvalidPartnerCert(string message, string key)
		{
			return new LocalizedString("ErrorInvalidPartnerCert", "Ex5BF0B3", false, true, Strings.ResourceManager, new object[]
			{
				message,
				key
			});
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00006EDC File Offset: 0x000050DC
		public static LocalizedString ErrorReadingRegistryValue(string key, string value)
		{
			return new LocalizedString("ErrorReadingRegistryValue", "ExC62F87", false, true, Strings.ResourceManager, new object[]
			{
				key,
				value
			});
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00006F10 File Offset: 0x00005110
		public static LocalizedString ErrorCertificateNotFound(string certSubject)
		{
			return new LocalizedString("ErrorCertificateNotFound", "Ex03079B", false, true, Strings.ResourceManager, new object[]
			{
				certSubject
			});
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00006F40 File Offset: 0x00005140
		public static LocalizedString ErrorMemberNotAuthorized(string message)
		{
			return new LocalizedString("ErrorMemberNotAuthorized", "ExD08D8B", false, true, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00006F70 File Offset: 0x00005170
		public static LocalizedString ErrorInvalidPartnerSpecified(string message, string key)
		{
			return new LocalizedString("ErrorInvalidPartnerSpecified", "ExB9BD8A", false, true, Strings.ResourceManager, new object[]
			{
				message,
				key
			});
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00006FA4 File Offset: 0x000051A4
		public static LocalizedString ErrorInvalidManagementCertificate(string message)
		{
			return new LocalizedString("ErrorInvalidManagementCertificate", "ExA3B8DA", false, true, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00006FD4 File Offset: 0x000051D4
		public static LocalizedString ErrorReadingRegistryKey(string key)
		{
			return new LocalizedString("ErrorReadingRegistryKey", "Ex14EFF8", false, true, Strings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00007004 File Offset: 0x00005204
		public static LocalizedString ErrorCertificateHasNoPrivateKey(string certSubject)
		{
			return new LocalizedString("ErrorCertificateHasNoPrivateKey", "ExE96793", false, true, Strings.ResourceManager, new object[]
			{
				certSubject
			});
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00007034 File Offset: 0x00005234
		public static LocalizedString ErrorOpeningCertificateStore(string store)
		{
			return new LocalizedString("ErrorOpeningCertificateStore", "Ex907F81", false, true, Strings.ResourceManager, new object[]
			{
				store
			});
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00007064 File Offset: 0x00005264
		public static LocalizedString ErrorPartnerNotAuthorized(string message)
		{
			return new LocalizedString("ErrorPartnerNotAuthorized", "Ex514400", false, true, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x040000B8 RID: 184
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Live.DomainServices.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000059 RID: 89
		private enum ParamIDs
		{
			// Token: 0x040000BA RID: 186
			ErrorWLCDPartnerAccessException,
			// Token: 0x040000BB RID: 187
			ErrorCertificateHasExpired,
			// Token: 0x040000BC RID: 188
			ErrorInvalidPartnerCert,
			// Token: 0x040000BD RID: 189
			ErrorReadingRegistryValue,
			// Token: 0x040000BE RID: 190
			ErrorCertificateNotFound,
			// Token: 0x040000BF RID: 191
			ErrorMemberNotAuthorized,
			// Token: 0x040000C0 RID: 192
			ErrorInvalidPartnerSpecified,
			// Token: 0x040000C1 RID: 193
			ErrorInvalidManagementCertificate,
			// Token: 0x040000C2 RID: 194
			ErrorReadingRegistryKey,
			// Token: 0x040000C3 RID: 195
			ErrorCertificateHasNoPrivateKey,
			// Token: 0x040000C4 RID: 196
			ErrorOpeningCertificateStore,
			// Token: 0x040000C5 RID: 197
			ErrorPartnerNotAuthorized
		}
	}
}
