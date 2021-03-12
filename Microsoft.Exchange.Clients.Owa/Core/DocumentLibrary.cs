using System;
using Microsoft.Exchange.Data.DocumentLibrary;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000104 RID: 260
	internal class DocumentLibrary : IComparable
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0003F2FA File Offset: 0x0003D4FA
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x0003F302 File Offset: 0x0003D502
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0003F30B File Offset: 0x0003D50B
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0003F313 File Offset: 0x0003D513
		public string SiteName
		{
			get
			{
				return this.siteName;
			}
			set
			{
				this.siteName = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0003F31C File Offset: 0x0003D51C
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0003F324 File Offset: 0x0003D524
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0003F32D File Offset: 0x0003D52D
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0003F335 File Offset: 0x0003D535
		public UriFlags Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0003F340 File Offset: 0x0003D540
		public int CompareTo(object value)
		{
			DocumentLibrary documentLibrary = value as DocumentLibrary;
			if (documentLibrary == null)
			{
				throw new ArgumentException("object is not a DocumentLibrary");
			}
			if (this.displayName != null && documentLibrary.DisplayName != null)
			{
				return this.displayName.CompareTo(documentLibrary.DisplayName);
			}
			if (this.displayName == null && documentLibrary.DisplayName != null)
			{
				return -1;
			}
			if (this.displayName != null && documentLibrary.DisplayName == null)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04000618 RID: 1560
		private string displayName;

		// Token: 0x04000619 RID: 1561
		private string siteName;

		// Token: 0x0400061A RID: 1562
		private string uri;

		// Token: 0x0400061B RID: 1563
		private UriFlags type = UriFlags.DocumentLibrary;
	}
}
