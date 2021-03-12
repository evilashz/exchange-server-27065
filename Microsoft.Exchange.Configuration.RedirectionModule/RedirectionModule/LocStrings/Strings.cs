using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.RedirectionModule.LocStrings
{
	// Token: 0x0200000C RID: 12
	internal static class Strings
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00003CBC File Offset: 0x00001EBC
		static Strings()
		{
			Strings.stringIDs.Add(1271656904U, "ExchangeAuthzAccessDenied");
			Strings.stringIDs.Add(3367785769U, "RemotePowerShellNotEnabled");
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003D20 File Offset: 0x00001F20
		public static LocalizedString ExchangeClientVersionBlocked(string serverVersion)
		{
			return new LocalizedString("ExchangeClientVersionBlocked", "ExF86F19", false, true, Strings.ResourceManager, new object[]
			{
				serverVersion
			});
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003D4F File Offset: 0x00001F4F
		public static LocalizedString ExchangeAuthzAccessDenied
		{
			get
			{
				return new LocalizedString("ExchangeAuthzAccessDenied", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003D6D File Offset: 0x00001F6D
		public static LocalizedString RemotePowerShellNotEnabled
		{
			get
			{
				return new LocalizedString("RemotePowerShellNotEnabled", "Ex6C03C5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003D8C File Offset: 0x00001F8C
		public static LocalizedString CannotRedirectCurrentRequest(Exception ex)
		{
			return new LocalizedString("CannotRedirectCurrentRequest", "Ex59C850", false, true, Strings.ResourceManager, new object[]
			{
				ex
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003DBC File Offset: 0x00001FBC
		public static LocalizedString AmbiguousTargetSite(string domainName, int minorPartnerId, string identities)
		{
			return new LocalizedString("AmbiguousTargetSite", "Ex3791C3", false, true, Strings.ResourceManager, new object[]
			{
				domainName,
				minorPartnerId,
				identities
			});
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public static LocalizedString FailedToResolveTargetSite(string domainName, int minorPartnerId)
		{
			return new LocalizedString("FailedToResolveTargetSite", "ExE8C84F", false, true, Strings.ResourceManager, new object[]
			{
				domainName,
				minorPartnerId
			});
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003E30 File Offset: 0x00002030
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000037 RID: 55
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x04000038 RID: 56
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Configuration.RedirectionModule.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200000D RID: 13
		public enum IDs : uint
		{
			// Token: 0x0400003A RID: 58
			ExchangeAuthzAccessDenied = 1271656904U,
			// Token: 0x0400003B RID: 59
			RemotePowerShellNotEnabled = 3367785769U
		}

		// Token: 0x0200000E RID: 14
		private enum ParamIDs
		{
			// Token: 0x0400003D RID: 61
			ExchangeClientVersionBlocked,
			// Token: 0x0400003E RID: 62
			CannotRedirectCurrentRequest,
			// Token: 0x0400003F RID: 63
			AmbiguousTargetSite,
			// Token: 0x04000040 RID: 64
			FailedToResolveTargetSite
		}
	}
}
