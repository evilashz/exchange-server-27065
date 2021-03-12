using System;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public struct UserSmtpAddress
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000029EF File Offset: 0x00000BEF
		public UserSmtpAddress(string localPart, string domainPart)
		{
			this.address = localPart + "@" + domainPart;
			this.local = localPart;
			this.domain = domainPart;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002A11 File Offset: 0x00000C11
		public string Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002A19 File Offset: 0x00000C19
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002A21 File Offset: 0x00000C21
		public string Local
		{
			get
			{
				return this.local;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002A29 File Offset: 0x00000C29
		public static explicit operator string(UserSmtpAddress address)
		{
			return address.address ?? string.Empty;
		}

		// Token: 0x04000079 RID: 121
		private readonly string address;

		// Token: 0x0400007A RID: 122
		private readonly string domain;

		// Token: 0x0400007B RID: 123
		private readonly string local;
	}
}
