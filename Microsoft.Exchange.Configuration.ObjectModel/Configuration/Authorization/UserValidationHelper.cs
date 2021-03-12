using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000243 RID: 579
	internal static class UserValidationHelper
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0004C761 File Offset: 0x0004A961
		private static GlsDirectorySession GlsSession
		{
			get
			{
				if (UserValidationHelper.glsSession == null)
				{
					UserValidationHelper.glsSession = new GlsDirectorySession(GlsCallerId.Exchange);
				}
				return UserValidationHelper.glsSession;
			}
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0004C780 File Offset: 0x0004A980
		internal static bool ValidateFilteringOnlyUser(string domain, string username)
		{
			if (string.IsNullOrEmpty(domain) || !VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.ValidateFilteringOnlyUser.Enabled)
			{
				return false;
			}
			if (username.EndsWith(".exchangemon.net", StringComparison.InvariantCultureIgnoreCase))
			{
				AuthZLogger.SafeAppendGenericInfo("ValidateFilteringOnlyUser", string.Format("Bypass monitoring account {0} check.", username));
				return false;
			}
			bool result;
			try
			{
				bool flag = false;
				domain = domain.ToLower();
				if (!UserValidationHelper.filteringOnlyCache.TryGetValue(domain, out flag))
				{
					CustomerType customerType = CustomerType.None;
					Guid guid;
					string text;
					string text2;
					UserValidationHelper.GlsSession.GetFfoTenantSettingsByDomain(domain, out guid, out text, out text2, out customerType);
					flag = (customerType == CustomerType.FilteringOnly);
					UserValidationHelper.filteringOnlyCache.TryInsertAbsolute(domain, flag, UserValidationHelper.DefaultAbsoluteTimeout);
					ExTraceGlobals.PublicPluginAPITracer.TraceDebug(0L, "[UserValidationHelper.ValidateFilteringOnlyUser] Domain:{0} belongs to TenantId:{1}, Region:{2}, Version: {3}, CustomerType: {4}.", new object[]
					{
						domain,
						guid,
						text,
						text2,
						customerType
					});
					AuthZLogger.SafeAppendGenericInfo("ValidateFilteringOnlyUser", string.Format("Domain:{0} belongs to TenantId:{1}, Region:{2}, Version: {3}, CustomerType: {4}.", new object[]
					{
						domain,
						guid,
						text,
						text2,
						customerType
					}));
				}
				else
				{
					AuthZLogger.SafeAppendGenericInfo("ValidateFilteringOnlyUser", string.Format("HitCache Domain: {0} is filteringOnly: {1}.", domain, flag));
				}
				result = flag;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.PublicPluginAPITracer.TraceError<Exception>(0L, "[UserValidationHelper.ValidateFilteringOnlyUser] Exception:{0}", ex);
				AuthZLogger.SafeAppendGenericError("ValidateFilteringOnlyUser", ex, new Func<Exception, bool>(KnownException.IsUnhandledException));
				result = false;
			}
			return result;
		}

		// Token: 0x040005E7 RID: 1511
		private const int FilteringOnlyUserCacheSize = 10240;

		// Token: 0x040005E8 RID: 1512
		internal const string MultiTenantTestDomain = ".exchangemon.net";

		// Token: 0x040005E9 RID: 1513
		private static GlsDirectorySession glsSession;

		// Token: 0x040005EA RID: 1514
		private static readonly ExactTimeoutCache<string, bool> filteringOnlyCache = new ExactTimeoutCache<string, bool>(null, null, null, 10240, false);

		// Token: 0x040005EB RID: 1515
		private static readonly TimeSpan DefaultAbsoluteTimeout = TimeSpan.FromMinutes(30.0);
	}
}
