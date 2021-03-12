using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DA3 RID: 3491
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PublishingUrl
	{
		// Token: 0x060077F0 RID: 30704 RVA: 0x002114CC File Offset: 0x0020F6CC
		protected PublishingUrl(Uri uri, SharingDataType dataType, string cachedIdentity)
		{
			Util.ThrowOnNullArgument(uri, "uri");
			Util.ThrowOnNullArgument(dataType, "dataType");
			Util.ThrowOnNullOrEmptyArgument(cachedIdentity, "cachedIdentity");
			this.uri = uri;
			this.dataType = dataType;
			this.cachedIdentity = cachedIdentity;
		}

		// Token: 0x060077F1 RID: 30705 RVA: 0x0021150C File Offset: 0x0020F70C
		public static PublishingUrl Create(string url)
		{
			Util.ThrowOnNullOrEmptyArgument(url, "url");
			ObscureUrl result = null;
			if (ObscureUrl.TryParse(url, out result))
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string>(0L, "PublishingUrl.Create(): Get ObscureUrl from url {0}.", url);
				return result;
			}
			PublicUrl result2 = null;
			if (PublicUrl.TryParse(url, out result2))
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string>(0L, "PublishingUrl.Create(): Get PublicUrl from url {0}.", url);
				return result2;
			}
			ExTraceGlobals.SharingTracer.TraceError<string>(0L, "PublishingUrl.Create(): Cannot parse url {0} for PublishingUrl", url);
			throw new ArgumentException("Url is not valid: " + url);
		}

		// Token: 0x060077F2 RID: 30706 RVA: 0x00211588 File Offset: 0x0020F788
		internal static bool IsAbsoluteUriString(string urlString, out Uri absoluteUri)
		{
			absoluteUri = null;
			try
			{
				Uri uri = new Uri(urlString, UriKind.Absolute);
				if (uri.HostNameType != UriHostNameType.Unknown && uri.HostNameType != UriHostNameType.Basic)
				{
					absoluteUri = uri;
				}
			}
			catch (UriFormatException)
			{
			}
			return absoluteUri != null;
		}

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x060077F3 RID: 30707 RVA: 0x002115D4 File Offset: 0x0020F7D4
		internal string Identity
		{
			get
			{
				return this.cachedIdentity;
			}
		}

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x060077F4 RID: 30708 RVA: 0x002115DC File Offset: 0x0020F7DC
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x060077F5 RID: 30709 RVA: 0x002115E4 File Offset: 0x0020F7E4
		public SharingDataType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x060077F6 RID: 30710
		public abstract string Domain { get; }

		// Token: 0x060077F7 RID: 30711
		internal abstract SharingAnonymousIdentityCacheKey CreateKey();

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x060077F8 RID: 30712
		internal abstract string TraceInfo { get; }

		// Token: 0x060077F9 RID: 30713 RVA: 0x002115EC File Offset: 0x0020F7EC
		public override string ToString()
		{
			return this.Uri.OriginalString;
		}

		// Token: 0x04005308 RID: 21256
		internal const string BrowseUrlType = ".html";

		// Token: 0x04005309 RID: 21257
		internal const string ICalUrlType = ".ics";

		// Token: 0x0400530A RID: 21258
		private readonly string cachedIdentity;

		// Token: 0x0400530B RID: 21259
		private readonly Uri uri;

		// Token: 0x0400530C RID: 21260
		private readonly SharingDataType dataType;
	}
}
