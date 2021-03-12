using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000026 RID: 38
	internal interface IUser : WatsonHelper.IProvideWatsonReportData
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FC RID: 252
		string LegacyDistinguishedName { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FD RID: 253
		string DisplayName { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FE RID: 254
		OrganizationId OrganizationId { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FF RID: 255
		SecurityIdentifier MasterAccountSid { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000100 RID: 256
		SecurityIdentifier ConnectAsSid { get; }

		// Token: 0x06000101 RID: 257
		void BackoffConnect(Exception reason);

		// Token: 0x06000102 RID: 258
		void CheckCanConnect();

		// Token: 0x06000103 RID: 259
		void RegisterActivity();

		// Token: 0x06000104 RID: 260
		int AddReference();

		// Token: 0x06000105 RID: 261
		void Release();

		// Token: 0x06000106 RID: 262
		void InvalidatePrincipalCache();

		// Token: 0x06000107 RID: 263
		ExchangePrincipal GetExchangePrincipal(string legacyDN);

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000108 RID: 264
		MiniRecipient MiniRecipient { get; }

		// Token: 0x06000109 RID: 265
		void CorrelateIdentityWithLegacyDN(ClientSecurityContext clientSecurityContext);

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600010A RID: 266
		ExOrgInfoFlags ExchangeOrganizationInfo { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600010B RID: 267
		MapiVersionRanges MapiBlockOutlookVersions { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600010C RID: 268
		bool MapiBlockOutlookRpcHttp { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600010D RID: 269
		bool MapiEnabled { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600010E RID: 270
		bool MapiCachedModeRequired { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010F RID: 271
		bool IsFederatedSystemAttendant { get; }
	}
}
