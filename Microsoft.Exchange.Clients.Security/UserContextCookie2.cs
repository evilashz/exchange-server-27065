using System;
using System.Net;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000030 RID: 48
	public sealed class UserContextCookie2
	{
		// Token: 0x0600016C RID: 364 RVA: 0x0000B224 File Offset: 0x00009424
		private UserContextCookie2(string cookieId, string userContextId, string mailboxUniqueKey, bool isSecure)
		{
			this.cookieId = cookieId;
			this.userContextId = userContextId;
			this.mailboxUniqueKey = mailboxUniqueKey;
			this.isSecure = isSecure;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000B24C File Offset: 0x0000944C
		public HttpCookie HttpCookie
		{
			get
			{
				if (this.httpCookie == null)
				{
					this.httpCookie = new HttpCookie(this.CookieName, this.CookieValue);
					this.httpCookie.HttpOnly = true;
					this.httpCookie.Secure = this.isSecure;
				}
				return this.httpCookie;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000B29B File Offset: 0x0000949B
		internal Cookie NetCookie
		{
			get
			{
				if (this.netCookie == null)
				{
					this.netCookie = new Cookie(this.CookieName, this.CookieValue);
				}
				return this.netCookie;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000B2C4 File Offset: 0x000094C4
		internal string CookieName
		{
			get
			{
				string text = UserContextCookie2.UserContextCookiePrefix;
				if (this.cookieId != null)
				{
					text = text + "_" + this.cookieId;
				}
				return text;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000B2F2 File Offset: 0x000094F2
		internal string UserContextId
		{
			get
			{
				return this.userContextId;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000B2FA File Offset: 0x000094FA
		internal string MailboxUniqueKey
		{
			get
			{
				return this.mailboxUniqueKey;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000B304 File Offset: 0x00009504
		internal string CookieValue
		{
			get
			{
				if (this.cookieValue == null)
				{
					this.cookieValue = this.userContextId;
					if (this.mailboxUniqueKey != null)
					{
						UTF8Encoding utf8Encoding = new UTF8Encoding();
						byte[] bytes = utf8Encoding.GetBytes(this.mailboxUniqueKey);
						this.cookieValue = this.cookieValue + "&" + this.ValidTokenBase64Encode(bytes);
					}
				}
				return this.cookieValue;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000B363 File Offset: 0x00009563
		public override string ToString()
		{
			return this.CookieName + "=" + this.CookieValue;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000B37B File Offset: 0x0000957B
		internal static UserContextCookie2 Create(string cookieId, string userContextId, string mailboxUniqueKey, bool isSecure)
		{
			return new UserContextCookie2(cookieId, userContextId, mailboxUniqueKey, isSecure);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000B388 File Offset: 0x00009588
		private string ValidTokenBase64Encode(byte[] byteArray)
		{
			if (byteArray == null)
			{
				throw new ArgumentNullException("byteArray");
			}
			int num = (int)(1.3333333333333333 * (double)byteArray.Length);
			if (num % 4 != 0)
			{
				num += 4 - num % 4;
			}
			char[] array = new char[num];
			Convert.ToBase64CharArray(byteArray, 0, byteArray.Length, array, 0);
			int num2 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == '\\')
				{
					array[i] = '-';
				}
				else if (array[i] == '=')
				{
					num2++;
				}
			}
			return new string(array, 0, array.Length - num2);
		}

		// Token: 0x0400017D RID: 381
		internal const int UserContextIdLength = 32;

		// Token: 0x0400017E RID: 382
		public static readonly string UserContextCookiePrefix = "UC";

		// Token: 0x0400017F RID: 383
		private readonly bool isSecure;

		// Token: 0x04000180 RID: 384
		private readonly string userContextId;

		// Token: 0x04000181 RID: 385
		private readonly string mailboxUniqueKey;

		// Token: 0x04000182 RID: 386
		private readonly string cookieId;

		// Token: 0x04000183 RID: 387
		private HttpCookie httpCookie;

		// Token: 0x04000184 RID: 388
		private Cookie netCookie;

		// Token: 0x04000185 RID: 389
		private string cookieValue;
	}
}
