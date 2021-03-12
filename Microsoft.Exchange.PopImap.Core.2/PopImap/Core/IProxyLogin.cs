using System;
using System.Security;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000016 RID: 22
	internal interface IProxyLogin
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000142 RID: 322
		// (set) Token: 0x06000143 RID: 323
		string UserName { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000144 RID: 324
		// (set) Token: 0x06000145 RID: 325
		SecureString Password { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000146 RID: 326
		// (set) Token: 0x06000147 RID: 327
		string ClientIp { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000148 RID: 328
		// (set) Token: 0x06000149 RID: 329
		string ClientPort { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600014A RID: 330
		// (set) Token: 0x0600014B RID: 331
		string AuthenticationType { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600014C RID: 332
		// (set) Token: 0x0600014D RID: 333
		string AuthenticationError { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600014E RID: 334
		// (set) Token: 0x0600014F RID: 335
		string ProxyDestination { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000150 RID: 336
		// (set) Token: 0x06000151 RID: 337
		ILiveIdBasicAuthentication LiveIdBasicAuth { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000152 RID: 338
		// (set) Token: 0x06000153 RID: 339
		ADUser AdUser { get; set; }
	}
}
