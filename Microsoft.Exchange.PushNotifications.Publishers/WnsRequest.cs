using System;
using System.Net;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000F2 RID: 242
	internal class WnsRequest : HttpSessionConfig, IDisposable
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x000180A9 File Offset: 0x000162A9
		public WnsRequest()
		{
			base.Method = "POST";
			base.Pipelined = false;
			base.AllowAutoRedirect = false;
			base.KeepAlive = false;
			base.Headers = new WebHeaderCollection();
			base.Expect100Continue = new bool?(false);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x000180E8 File Offset: 0x000162E8
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x000180F0 File Offset: 0x000162F0
		public Uri Uri { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x000180F9 File Offset: 0x000162F9
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0001810B File Offset: 0x0001630B
		public string WnsType
		{
			get
			{
				return base.Headers["X-WNS-Type"];
			}
			set
			{
				base.Headers["X-WNS-Type"] = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001811E File Offset: 0x0001631E
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x00018130 File Offset: 0x00016330
		public string WnsCachePolicy
		{
			get
			{
				return base.Headers["X-WNS-Cache-Policy"];
			}
			set
			{
				base.Headers["X-WNS-Cache-Policy"] = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00018143 File Offset: 0x00016343
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x00018155 File Offset: 0x00016355
		public string WnsTimeToLive
		{
			get
			{
				return base.Headers["X-WNS-TTL"];
			}
			set
			{
				base.Headers["X-WNS-TTL"] = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00018168 File Offset: 0x00016368
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0001817A File Offset: 0x0001637A
		public string WnsTag
		{
			get
			{
				return base.Headers["X-WNS-Tag"];
			}
			set
			{
				base.Headers["X-WNS-Tag"] = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0001818D File Offset: 0x0001638D
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0001819C File Offset: 0x0001639C
		public string Authorization
		{
			get
			{
				return base.Headers[HttpRequestHeader.Authorization];
			}
			set
			{
				base.Headers[HttpRequestHeader.Authorization] = value;
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000181AC File Offset: 0x000163AC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000181BB File Offset: 0x000163BB
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing && base.RequestStream != null)
				{
					base.RequestStream.Close();
				}
				base.RequestStream = null;
				this.isDisposed = true;
			}
		}

		// Token: 0x0400043E RID: 1086
		public const string WnsTypeBadge = "wns/badge";

		// Token: 0x0400043F RID: 1087
		public const string WnsTypeTile = "wns/tile";

		// Token: 0x04000440 RID: 1088
		public const string WnsTypeToast = "wns/toast";

		// Token: 0x04000441 RID: 1089
		public const string WnsTypeRaw = "wns/raw";

		// Token: 0x04000442 RID: 1090
		public const string WnsContentType = "text/xml";

		// Token: 0x04000443 RID: 1091
		public const string WnsRawContentType = "application/octet-stream";

		// Token: 0x04000444 RID: 1092
		private const string WnsHeaderType = "X-WNS-Type";

		// Token: 0x04000445 RID: 1093
		private const string WnsHeaderCachePolicy = "X-WNS-Cache-Policy";

		// Token: 0x04000446 RID: 1094
		private const string WnsHeaderTimeToLive = "X-WNS-TTL";

		// Token: 0x04000447 RID: 1095
		private const string WnsHeaderTag = "X-WNS-Tag";

		// Token: 0x04000448 RID: 1096
		private const string WnsHeaderRequestForStatus = "X-WNS-RequestForStatus";

		// Token: 0x04000449 RID: 1097
		private bool isDisposed;
	}
}
