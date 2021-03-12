using System;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000E5 RID: 229
	internal class WebServiceUri
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x00019ED8 File Offset: 0x000180D8
		internal WebServiceUri(string url, string protocol, UriSource source, int serverVersion)
		{
			url = WebServiceUri.urlPool.Intern(url);
			protocol = WebServiceUri.protocolPool.Intern(protocol);
			this.uri = new Uri(url);
			if (protocol != null)
			{
				this.protocol = protocol;
			}
			this.source = source;
			this.serverVersion = serverVersion;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00019F38 File Offset: 0x00018138
		internal WebServiceUri(WebServiceUri uri, DateTime whenCached)
		{
			this.whenCached = whenCached;
			this.credentials = uri.credentials;
			this.uri = uri.Uri;
			this.protocol = uri.Protocol;
			this.source = uri.Source;
			this.emailAddress = uri.EmailAddress;
			this.serverVersion = uri.ServerVersion;
			this.AutodiscoverFailedExceptionString = uri.AutodiscoverFailedExceptionString;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00019FB4 File Offset: 0x000181B4
		internal WebServiceUri(WebServiceUri uri, NetworkCredential credentials, EmailAddress emailAddress)
		{
			this.credentials = credentials;
			this.whenCached = uri.WhenCached;
			this.uri = uri.Uri;
			this.protocol = uri.Protocol;
			this.source = this.Source;
			this.emailAddress = emailAddress;
			this.serverVersion = uri.ServerVersion;
			this.AutodiscoverFailedExceptionString = uri.AutodiscoverFailedExceptionString;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001A028 File Offset: 0x00018228
		internal WebServiceUri(NetworkCredential credentials, LocalizedString exceptionString, EmailAddress emailAddress)
		{
			this.credentials = credentials;
			this.AutodiscoverFailedExceptionString = exceptionString;
			this.emailAddress = emailAddress;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001A050 File Offset: 0x00018250
		internal Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001A058 File Offset: 0x00018258
		internal string Protocol
		{
			get
			{
				return this.protocol;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001A060 File Offset: 0x00018260
		internal UriSource Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001A068 File Offset: 0x00018268
		internal DateTime WhenCached
		{
			get
			{
				return this.whenCached;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001A070 File Offset: 0x00018270
		internal NetworkCredential Credentials
		{
			get
			{
				return this.credentials;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001A078 File Offset: 0x00018278
		internal EmailAddress EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001A080 File Offset: 0x00018280
		internal int ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001A088 File Offset: 0x00018288
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0001A090 File Offset: 0x00018290
		internal LocalizedString AutodiscoverFailedExceptionString { get; private set; }

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001A09C File Offset: 0x0001829C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"WebServiceUri ",
				this.Uri,
				" protocol ",
				this.protocol,
				" version ",
				this.ServerVersion,
				" ExceptionStringID ",
				this.AutodiscoverFailedExceptionString.StringId,
				" created at ",
				this.whenCached
			});
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001A11F File Offset: 0x0001831F
		internal bool Expired(DateTime utcNow)
		{
			return Configuration.RemoteUriInvalidCacheDurationInSeconds.Value < utcNow - this.whenCached;
		}

		// Token: 0x04000367 RID: 871
		private static StringPool urlPool = new StringPool(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000368 RID: 872
		private static StringPool protocolPool = new StringPool(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000369 RID: 873
		private Uri uri;

		// Token: 0x0400036A RID: 874
		private string protocol = string.Empty;

		// Token: 0x0400036B RID: 875
		private DateTime whenCached;

		// Token: 0x0400036C RID: 876
		private UriSource source;

		// Token: 0x0400036D RID: 877
		private NetworkCredential credentials;

		// Token: 0x0400036E RID: 878
		private EmailAddress emailAddress;

		// Token: 0x0400036F RID: 879
		private int serverVersion;
	}
}
