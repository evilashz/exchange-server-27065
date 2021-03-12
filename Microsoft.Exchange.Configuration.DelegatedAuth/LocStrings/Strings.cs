using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication.LocStrings
{
	// Token: 0x0200000C RID: 12
	internal static class Strings
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000043F0 File Offset: 0x000025F0
		static Strings()
		{
			Strings.stringIDs.Add(121065743U, "CannotResolveWindowsIdentityException");
			Strings.stringIDs.Add(2246886311U, "DelegatedAccessDeniedException");
			Strings.stringIDs.Add(1563788146U, "SecurityTokenExpired");
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004467 File Offset: 0x00002667
		public static LocalizedString CannotResolveWindowsIdentityException
		{
			get
			{
				return new LocalizedString("CannotResolveWindowsIdentityException", "ExCD01B5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004488 File Offset: 0x00002688
		public static LocalizedString CannotResolveSidToADAccountException(string userId)
		{
			return new LocalizedString("CannotResolveSidToADAccountException", "ExA87173", false, true, Strings.ResourceManager, new object[]
			{
				userId
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000044B8 File Offset: 0x000026B8
		public static LocalizedString CannotResolveUserTenantException(string userId)
		{
			return new LocalizedString("CannotResolveUserTenantException", "Ex26165F", false, true, Strings.ResourceManager, new object[]
			{
				userId
			});
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000044E7 File Offset: 0x000026E7
		public static LocalizedString DelegatedAccessDeniedException
		{
			get
			{
				return new LocalizedString("DelegatedAccessDeniedException", "Ex007002", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004508 File Offset: 0x00002708
		public static LocalizedString DelegatedServerErrorException(Exception ex)
		{
			return new LocalizedString("DelegatedServerErrorException", "Ex622435", false, true, Strings.ResourceManager, new object[]
			{
				ex
			});
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004537 File Offset: 0x00002737
		public static LocalizedString SecurityTokenExpired
		{
			get
			{
				return new LocalizedString("SecurityTokenExpired", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004558 File Offset: 0x00002758
		public static LocalizedString CannotResolveCurrentKeyException(bool currentKey)
		{
			return new LocalizedString("CannotResolveCurrentKeyException", "Ex78D2C1", false, true, Strings.ResourceManager, new object[]
			{
				currentKey
			});
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000458C File Offset: 0x0000278C
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400004C RID: 76
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(3);

		// Token: 0x0400004D RID: 77
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Configuration.DelegatedAuthentication.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200000D RID: 13
		public enum IDs : uint
		{
			// Token: 0x0400004F RID: 79
			CannotResolveWindowsIdentityException = 121065743U,
			// Token: 0x04000050 RID: 80
			DelegatedAccessDeniedException = 2246886311U,
			// Token: 0x04000051 RID: 81
			SecurityTokenExpired = 1563788146U
		}

		// Token: 0x0200000E RID: 14
		private enum ParamIDs
		{
			// Token: 0x04000053 RID: 83
			CannotResolveSidToADAccountException,
			// Token: 0x04000054 RID: 84
			CannotResolveUserTenantException,
			// Token: 0x04000055 RID: 85
			DelegatedServerErrorException,
			// Token: 0x04000056 RID: 86
			CannotResolveCurrentKeyException
		}
	}
}
