using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200011D RID: 285
	public struct DiagnosableParameters
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x000217F4 File Offset: 0x0001F9F4
		private DiagnosableParameters(string argument, bool allowRestrictedData, bool unlimited, string userIdentity)
		{
			this.argument = argument;
			this.allowRestrictedData = allowRestrictedData;
			this.unlimited = unlimited;
			this.userIdentity = userIdentity;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00021813 File Offset: 0x0001FA13
		public string Argument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0002181B File Offset: 0x0001FA1B
		public bool AllowRestrictedData
		{
			get
			{
				return this.allowRestrictedData;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00021823 File Offset: 0x0001FA23
		public bool Unlimited
		{
			get
			{
				return this.unlimited;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0002182B File Offset: 0x0001FA2B
		public string UserIdentity
		{
			get
			{
				return this.userIdentity;
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00021833 File Offset: 0x0001FA33
		public static DiagnosableParameters Create(string argument, bool allowRestrictedData, bool unlimited, string userIdentity)
		{
			return new DiagnosableParameters(argument ?? string.Empty, allowRestrictedData, unlimited, userIdentity ?? string.Empty);
		}

		// Token: 0x040005A8 RID: 1448
		private readonly string argument;

		// Token: 0x040005A9 RID: 1449
		private readonly bool allowRestrictedData;

		// Token: 0x040005AA RID: 1450
		private readonly bool unlimited;

		// Token: 0x040005AB RID: 1451
		private readonly string userIdentity;
	}
}
