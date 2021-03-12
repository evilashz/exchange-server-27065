using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200001A RID: 26
	internal interface IConnection
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A8 RID: 168
		string ActAsLegacyDN { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A9 RID: 169
		ClientSecurityContext AccessingClientSecurityContext { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AA RID: 170
		ConnectionFlags ConnectionFlags { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AB RID: 171
		OrganizationId OrganizationId { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AC RID: 172
		IPAddress ServerIpAddress { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AD RID: 173
		string ProtocolSequence { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AE RID: 174
		bool IsWebService { get; }

		// Token: 0x060000AF RID: 175
		void BackoffConnect(Exception reason);

		// Token: 0x060000B0 RID: 176
		ExchangePrincipal FindExchangePrincipalByLegacyDN(string legacyDN);

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B1 RID: 177
		MiniRecipient MiniRecipient { get; }

		// Token: 0x060000B2 RID: 178
		void InvalidateCachedUserInfo();

		// Token: 0x060000B3 RID: 179
		void MarkAsDeadAndDropAllAsyncCalls();

		// Token: 0x060000B4 RID: 180
		void ExecuteInContext<T>(T input, Action<T> action);

		// Token: 0x1700001E RID: 30
		// (set) Token: 0x060000B5 RID: 181
		Fqdn TargetServer { set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B6 RID: 182
		bool IsEncrypted { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B7 RID: 183
		CultureInfo CultureInfo { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B8 RID: 184
		int CodePageId { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B9 RID: 185
		ClientInfo ClientInformation { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BA RID: 186
		string RpcServerTarget { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BB RID: 187
		bool IsFederatedSystemAttendant { get; }
	}
}
