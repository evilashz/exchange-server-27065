using System;
using System.Globalization;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class Cookie
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public Cookie(string baseDN)
		{
			this.baseDN = baseDN;
			this.lastUpdated = DateTime.MinValue;
			this.cookieValue = null;
			this.domainController = null;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002100 File Offset: 0x00000300
		public string BaseDN
		{
			get
			{
				return this.baseDN;
			}
			set
			{
				this.baseDN = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002109 File Offset: 0x00000309
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002111 File Offset: 0x00000311
		public byte[] CookieValue
		{
			get
			{
				return this.cookieValue;
			}
			set
			{
				this.cookieValue = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000211A File Offset: 0x0000031A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002122 File Offset: 0x00000322
		public string DomainController
		{
			get
			{
				return this.domainController;
			}
			set
			{
				this.domainController = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000212B File Offset: 0x0000032B
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002133 File Offset: 0x00000333
		public DateTime LastUpdated
		{
			get
			{
				return this.lastUpdated;
			}
			set
			{
				this.lastUpdated = value;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213C File Offset: 0x0000033C
		public static bool TryDeserialize(string record, out Cookie cookie)
		{
			cookie = null;
			string[] array = record.Split(Cookie.RecordSeparator, 4);
			if (array.Length != 4)
			{
				return false;
			}
			DateTime dateTime;
			if (!DateTime.TryParse(array[2], CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
			{
				return false;
			}
			byte[] array2;
			try
			{
				array2 = Convert.FromBase64String(array[3]);
			}
			catch (FormatException)
			{
				return false;
			}
			cookie = new Cookie(array[0]);
			cookie.DomainController = array[1];
			cookie.LastUpdated = dateTime;
			cookie.CookieValue = array2;
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021BC File Offset: 0x000003BC
		public static Cookie Deserialize(string record)
		{
			Cookie result;
			if (!Cookie.TryDeserialize(record, out result))
			{
				throw new FormatException("Cookie is not correctly formatted or is corrupted: " + record);
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E8 File Offset: 0x000003E8
		public static Cookie Clone(Cookie cookie)
		{
			Cookie cookie2 = new Cookie(cookie.baseDN);
			cookie2.DomainController = cookie.DomainController;
			cookie2.LastUpdated = cookie.LastUpdated;
			if (cookie.CookieValue != null && cookie.CookieValue.Length > 0)
			{
				cookie2.CookieValue = new byte[cookie.CookieValue.Length];
				Buffer.BlockCopy(cookie.CookieValue, 0, cookie2.CookieValue, 0, cookie.CookieValue.Length);
			}
			return cookie2;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000225C File Offset: 0x0000045C
		public string Serialize()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0};{1};{2};{3}", new object[]
			{
				this.baseDN,
				this.domainController,
				this.lastUpdated,
				Convert.ToBase64String(this.cookieValue)
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022B0 File Offset: 0x000004B0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "DN=<{0}>; DC=<{1}>; LastUpdated=<{2}>; CookieLength=<{3}>", new object[]
			{
				this.baseDN,
				this.domainController,
				this.lastUpdated,
				(this.cookieValue == null) ? "<null>" : this.cookieValue.Length.ToString()
			});
		}

		// Token: 0x04000001 RID: 1
		private static readonly char[] RecordSeparator = new char[]
		{
			';'
		};

		// Token: 0x04000002 RID: 2
		private string baseDN;

		// Token: 0x04000003 RID: 3
		private byte[] cookieValue;

		// Token: 0x04000004 RID: 4
		private string domainController;

		// Token: 0x04000005 RID: 5
		private DateTime lastUpdated;
	}
}
