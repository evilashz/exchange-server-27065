using System;
using System.Net;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200003E RID: 62
	public sealed class UserContextCookie
	{
		// Token: 0x060001DA RID: 474 RVA: 0x0000A5B8 File Offset: 0x000087B8
		private UserContextCookie(string cookieId, string userContextId, string mailboxUniqueKey)
		{
			this.cookieId = cookieId;
			this.userContextId = userContextId;
			this.mailboxUniqueKey = mailboxUniqueKey;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A5D5 File Offset: 0x000087D5
		internal HttpCookie HttpCookie
		{
			get
			{
				if (this.httpCookie == null)
				{
					this.httpCookie = new HttpCookie(this.CookieName, this.CookieValue);
				}
				return this.httpCookie;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000A5FC File Offset: 0x000087FC
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000A624 File Offset: 0x00008824
		internal string CookieName
		{
			get
			{
				string text = "UserContext";
				if (this.cookieId != null)
				{
					text = text + "_" + this.cookieId;
				}
				return text;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000A652 File Offset: 0x00008852
		internal string UserContextId
		{
			get
			{
				return this.userContextId;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000A65A File Offset: 0x0000885A
		internal string MailboxUniqueKey
		{
			get
			{
				return this.mailboxUniqueKey;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000A664 File Offset: 0x00008864
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
						this.cookieValue = this.cookieValue + "&" + UserContextCookie.ValidTokenBase64Encode(bytes);
					}
				}
				return this.cookieValue;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A6C4 File Offset: 0x000088C4
		public static string ValidTokenBase64Encode(byte[] byteArray)
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

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A748 File Offset: 0x00008948
		public static byte[] ValidTokenBase64Decode(string tokenValidBase64String)
		{
			if (tokenValidBase64String == null)
			{
				throw new ArgumentNullException("tokenValidBase64String");
			}
			long num = (long)tokenValidBase64String.Length;
			if (tokenValidBase64String.Length % 4 != 0)
			{
				num += (long)(4 - tokenValidBase64String.Length % 4);
			}
			char[] array = new char[num];
			tokenValidBase64String.CopyTo(0, array, 0, tokenValidBase64String.Length);
			for (long num2 = 0L; num2 < (long)tokenValidBase64String.Length; num2 += 1L)
			{
				checked
				{
					if (array[(int)((IntPtr)num2)] == '-')
					{
						array[(int)((IntPtr)num2)] = '\\';
					}
				}
			}
			for (long num3 = (long)tokenValidBase64String.Length; num3 < (long)array.Length; num3 += 1L)
			{
				array[(int)(checked((IntPtr)num3))] = '=';
			}
			return Convert.FromBase64CharArray(array, 0, array.Length);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A7E4 File Offset: 0x000089E4
		public static bool IsValidGuid(string guid)
		{
			if (guid == null || guid.Length != 32)
			{
				return false;
			}
			for (int i = 0; i < 32; i++)
			{
				if (!UserContextCookie.IsHexChar(guid[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A81E File Offset: 0x00008A1E
		public static bool IsHexChar(char c)
		{
			return char.IsDigit(c) || (char.ToUpperInvariant(c) >= 'A' && char.ToUpperInvariant(c) <= 'F');
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A843 File Offset: 0x00008A43
		public override string ToString()
		{
			return this.CookieName + "=" + this.CookieValue;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A85B File Offset: 0x00008A5B
		internal static UserContextCookie Create(string cookieId, string userContextId, string mailboxUniqueKey)
		{
			return new UserContextCookie(cookieId, userContextId, mailboxUniqueKey);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A868 File Offset: 0x00008A68
		internal static UserContextCookie TryCreateFromHttpCookie(HttpCookie cookie)
		{
			string text = null;
			string text2 = null;
			if (string.IsNullOrEmpty(cookie.Value))
			{
				return null;
			}
			if (!UserContextCookie.TryParseCookieValue(cookie.Value, out text, out text2))
			{
				return null;
			}
			string text3 = null;
			if (!UserContextCookie.TryParseCookieName(cookie.Name, out text3))
			{
				return null;
			}
			return UserContextCookie.Create(text3, text, text2);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		internal static UserContextCookie TryCreateFromNetCookie(Cookie cookie)
		{
			string text = null;
			string text2 = null;
			if (string.IsNullOrEmpty(cookie.Value))
			{
				return null;
			}
			if (!UserContextCookie.TryParseCookieValue(cookie.Value, out text, out text2))
			{
				return null;
			}
			string text3 = null;
			if (!UserContextCookie.TryParseCookieName(cookie.Name, out text3))
			{
				return null;
			}
			return UserContextCookie.Create(text3, text, text2);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A908 File Offset: 0x00008B08
		internal static bool TryParseCookieValue(string cookieValue, out string userContextId, out string mailboxUniqueKey)
		{
			userContextId = null;
			mailboxUniqueKey = null;
			if (cookieValue.Length == 32)
			{
				userContextId = cookieValue;
			}
			else
			{
				if (cookieValue.Length < 34)
				{
					return false;
				}
				int num = cookieValue.IndexOf('&');
				if (num != 32)
				{
					return false;
				}
				num++;
				userContextId = cookieValue.Substring(0, num - 1);
				string tokenValidBase64String = cookieValue.Substring(num, cookieValue.Length - num);
				byte[] bytes = null;
				try
				{
					bytes = UserContextCookie.ValidTokenBase64Decode(tokenValidBase64String);
				}
				catch (FormatException)
				{
					return false;
				}
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				mailboxUniqueKey = utf8Encoding.GetString(bytes);
			}
			return UserContextCookie.IsValidUserContextId(userContextId);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		internal static bool TryParseCookieName(string cookieName, out string cookieId)
		{
			cookieId = null;
			if (!cookieName.StartsWith("UserContext", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			int length = "UserContext".Length;
			if (cookieName.Length == length)
			{
				return true;
			}
			cookieId = cookieName.Substring(length + 1, cookieName.Length - length - 1);
			return UserContextCookie.IsValidGuid(cookieId);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A9FF File Offset: 0x00008BFF
		private static bool IsValidUserContextId(string userContextId)
		{
			return UserContextCookie.IsValidGuid(userContextId);
		}

		// Token: 0x040000F0 RID: 240
		public const string UserContextCookiePrefix = "UserContext";

		// Token: 0x040000F1 RID: 241
		internal const int UserContextIdLength = 32;

		// Token: 0x040000F2 RID: 242
		private readonly string userContextId;

		// Token: 0x040000F3 RID: 243
		private readonly string mailboxUniqueKey;

		// Token: 0x040000F4 RID: 244
		private readonly string cookieId;

		// Token: 0x040000F5 RID: 245
		private HttpCookie httpCookie;

		// Token: 0x040000F6 RID: 246
		private Cookie netCookie;

		// Token: 0x040000F7 RID: 247
		private string cookieValue;
	}
}
