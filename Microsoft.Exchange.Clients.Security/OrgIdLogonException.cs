using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000017 RID: 23
	public abstract class OrgIdLogonException : LocalizedException
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000065 RID: 101
		protected abstract string ErrorMessageFormatString { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000066 RID: 102
		public abstract Strings.IDs ErrorMessageStringId { get; }

		// Token: 0x06000067 RID: 103 RVA: 0x00003257 File Offset: 0x00001457
		protected OrgIdLogonException(string userName, string logoutLink) : base(new LocalizedString(null), null)
		{
			if (string.IsNullOrWhiteSpace(userName))
			{
				throw new ArgumentNullException("userName");
			}
			this.userName = userName;
			this.logoutLink = logoutLink;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003288 File Offset: 0x00001488
		public override string Message
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, this.ErrorMessageFormatString, new object[]
				{
					this.userName,
					this.logoutLink
				});
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000032BF File Offset: 0x000014BF
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000032C7 File Offset: 0x000014C7
		public string LogoutLink
		{
			get
			{
				return this.logoutLink;
			}
		}

		// Token: 0x0400003E RID: 62
		private readonly string userName;

		// Token: 0x0400003F RID: 63
		private readonly string logoutLink;
	}
}
