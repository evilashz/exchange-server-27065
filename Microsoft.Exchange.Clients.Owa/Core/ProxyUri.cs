using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000220 RID: 544
	public class ProxyUri
	{
		// Token: 0x0600124B RID: 4683 RVA: 0x0006FCC4 File Offset: 0x0006DEC4
		private ProxyUri(string uriString)
		{
			this.uriString = uriString;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0006FCDA File Offset: 0x0006DEDA
		private ProxyUri(string uriString, Uri failbackUrl)
		{
			this.uriString = uriString;
			this.failbackUrl = failbackUrl;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0006FCF7 File Offset: 0x0006DEF7
		public static ProxyUri Create(string uriString)
		{
			return ProxyUri.Create(uriString, null);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0006FD00 File Offset: 0x0006DF00
		public static ProxyUri Create(string uriString, Uri failbackUrl)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			return new ProxyUri(uriString, failbackUrl);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0006FD17 File Offset: 0x0006DF17
		internal bool Parse()
		{
			this.isParsed = true;
			this.uri = ProxyUtilities.TryCreateCasUri(this.uriString, this.needVdirValidation);
			if (this.uri == null)
			{
				return false;
			}
			this.isValid = true;
			return true;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0006FD4F File Offset: 0x0006DF4F
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x0006FD57 File Offset: 0x0006DF57
		public bool NeedVdirValidation
		{
			get
			{
				return this.needVdirValidation;
			}
			set
			{
				this.needVdirValidation = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0006FD60 File Offset: 0x0006DF60
		public bool IsParsed
		{
			get
			{
				return this.isParsed;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0006FD68 File Offset: 0x0006DF68
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0006FD70 File Offset: 0x0006DF70
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0006FD78 File Offset: 0x0006DF78
		public Uri FailbackUrl
		{
			get
			{
				return this.failbackUrl;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0006FD80 File Offset: 0x0006DF80
		// (set) Token: 0x06001257 RID: 4695 RVA: 0x0006FD88 File Offset: 0x0006DF88
		internal ProxyPingResult ProxyPingResult
		{
			get
			{
				return this.proxyPingResult;
			}
			set
			{
				this.proxyPingResult = value;
			}
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0006FD91 File Offset: 0x0006DF91
		public override string ToString()
		{
			return this.uriString;
		}

		// Token: 0x04000C94 RID: 3220
		private string uriString;

		// Token: 0x04000C95 RID: 3221
		private Uri uri;

		// Token: 0x04000C96 RID: 3222
		private Uri failbackUrl;

		// Token: 0x04000C97 RID: 3223
		private ProxyPingResult proxyPingResult;

		// Token: 0x04000C98 RID: 3224
		private bool isParsed;

		// Token: 0x04000C99 RID: 3225
		private bool isValid;

		// Token: 0x04000C9A RID: 3226
		private bool needVdirValidation = true;
	}
}
