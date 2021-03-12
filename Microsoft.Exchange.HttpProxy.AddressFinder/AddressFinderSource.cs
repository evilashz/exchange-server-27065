using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x02000008 RID: 8
	internal sealed class AddressFinderSource
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000026B0 File Offset: 0x000008B0
		public AddressFinderSource(IDictionary items, NameValueCollection headers, NameValueCollection queryString, Uri url, string applicationPath, string filePath, HttpCookieCollection cookies)
		{
			ArgumentValidator.ThrowIfNull("items", items);
			ArgumentValidator.ThrowIfNull("headers", headers);
			ArgumentValidator.ThrowIfNull("queryString", queryString);
			ArgumentValidator.ThrowIfNull("url", url);
			ArgumentValidator.ThrowIfNull("applicationPath", applicationPath);
			ArgumentValidator.ThrowIfNull("filePath", filePath);
			ArgumentValidator.ThrowIfNull("cookies", cookies);
			this.Items = items;
			this.Headers = headers;
			this.QueryString = queryString;
			this.Url = url;
			this.ApplicationPath = applicationPath;
			this.FilePath = filePath;
			this.Cookies = cookies;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002749 File Offset: 0x00000949
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002751 File Offset: 0x00000951
		public IDictionary Items { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000275A File Offset: 0x0000095A
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002762 File Offset: 0x00000962
		public NameValueCollection Headers { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000276B File Offset: 0x0000096B
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002773 File Offset: 0x00000973
		public NameValueCollection QueryString { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000277C File Offset: 0x0000097C
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002784 File Offset: 0x00000984
		public Uri Url { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000278D File Offset: 0x0000098D
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002795 File Offset: 0x00000995
		public string ApplicationPath { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000279E File Offset: 0x0000099E
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000027A6 File Offset: 0x000009A6
		public string FilePath { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000027AF File Offset: 0x000009AF
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000027B7 File Offset: 0x000009B7
		public HttpCookieCollection Cookies { get; private set; }
	}
}
