using System;
using System.Collections.Specialized;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000E06 RID: 3590
	internal class ODataRawQueryOptions
	{
		// Token: 0x06005D0D RID: 23821 RVA: 0x00122738 File Offset: 0x00120938
		public ODataRawQueryOptions(NameValueCollection queryString)
		{
			ArgumentValidator.ThrowIfNull("queryString", queryString);
			foreach (string text in queryString.AllKeys)
			{
				string text2 = queryString[text];
				string key;
				switch (key = text)
				{
				case "$filter":
					this.Filter = text2;
					break;
				case "$orderby":
					this.OrderBy = text2;
					break;
				case "$top":
					this.Top = text2;
					break;
				case "$skip":
					this.Skip = text2;
					break;
				case "$select":
					this.Select = text2;
					break;
				case "$inlinecount":
					this.InlineCount = text2;
					break;
				case "$expand":
					this.Expand = text2;
					break;
				case "$format":
					this.Format = text2;
					break;
				case "$skiptoken":
					this.SkipToken = text2;
					break;
				}
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x06005D0E RID: 23822 RVA: 0x00122897 File Offset: 0x00120A97
		// (set) Token: 0x06005D0F RID: 23823 RVA: 0x0012289F File Offset: 0x00120A9F
		public string Filter { get; private set; }

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x06005D10 RID: 23824 RVA: 0x001228A8 File Offset: 0x00120AA8
		// (set) Token: 0x06005D11 RID: 23825 RVA: 0x001228B0 File Offset: 0x00120AB0
		public string OrderBy { get; private set; }

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x06005D12 RID: 23826 RVA: 0x001228B9 File Offset: 0x00120AB9
		// (set) Token: 0x06005D13 RID: 23827 RVA: 0x001228C1 File Offset: 0x00120AC1
		public string Top { get; private set; }

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x06005D14 RID: 23828 RVA: 0x001228CA File Offset: 0x00120ACA
		// (set) Token: 0x06005D15 RID: 23829 RVA: 0x001228D2 File Offset: 0x00120AD2
		public string Skip { get; private set; }

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x06005D16 RID: 23830 RVA: 0x001228DB File Offset: 0x00120ADB
		// (set) Token: 0x06005D17 RID: 23831 RVA: 0x001228E3 File Offset: 0x00120AE3
		public string Select { get; private set; }

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x06005D18 RID: 23832 RVA: 0x001228EC File Offset: 0x00120AEC
		// (set) Token: 0x06005D19 RID: 23833 RVA: 0x001228F4 File Offset: 0x00120AF4
		public string Expand { get; private set; }

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x06005D1A RID: 23834 RVA: 0x001228FD File Offset: 0x00120AFD
		// (set) Token: 0x06005D1B RID: 23835 RVA: 0x00122905 File Offset: 0x00120B05
		public string InlineCount { get; private set; }

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x06005D1C RID: 23836 RVA: 0x0012290E File Offset: 0x00120B0E
		// (set) Token: 0x06005D1D RID: 23837 RVA: 0x00122916 File Offset: 0x00120B16
		public string Format { get; private set; }

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x06005D1E RID: 23838 RVA: 0x0012291F File Offset: 0x00120B1F
		// (set) Token: 0x06005D1F RID: 23839 RVA: 0x00122927 File Offset: 0x00120B27
		public string SkipToken { get; internal set; }
	}
}
