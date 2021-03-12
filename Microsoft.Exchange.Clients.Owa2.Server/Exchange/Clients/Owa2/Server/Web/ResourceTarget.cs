using System;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200048A RID: 1162
	public static class ResourceTarget
	{
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x000929E1 File Offset: 0x00090BE1
		public static ResourceTarget.Filter Any
		{
			get
			{
				return (UserAgent userAgent) => true;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600278A RID: 10122 RVA: 0x00092A0B File Offset: 0x00090C0B
		public static ResourceTarget.Filter MouseOnly
		{
			get
			{
				return (UserAgent userAgent) => userAgent.Layout == LayoutType.Mouse;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x00092A40 File Offset: 0x00090C40
		public static ResourceTarget.Filter TouchOnly
		{
			get
			{
				return (UserAgent userAgent) => userAgent.Layout == LayoutType.TouchNarrow || userAgent.Layout == LayoutType.TouchWide;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x00092A6A File Offset: 0x00090C6A
		public static ResourceTarget.Filter NarrowOnly
		{
			get
			{
				return (UserAgent userAgent) => userAgent.Layout == LayoutType.TouchNarrow;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x00092A94 File Offset: 0x00090C94
		public static ResourceTarget.Filter WideOnly
		{
			get
			{
				return (UserAgent userAgent) => userAgent.Layout == LayoutType.TouchWide;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x00092ABE File Offset: 0x00090CBE
		public static ResourceTarget.Filter NarrowHighResolution
		{
			get
			{
				return (UserAgent userAgent) => userAgent.Layout == LayoutType.TouchNarrow;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600278F RID: 10127 RVA: 0x00092AE8 File Offset: 0x00090CE8
		public static ResourceTarget.Filter WideHighResolution
		{
			get
			{
				return (UserAgent userAgent) => userAgent.Layout == LayoutType.TouchWide;
			}
		}

		// Token: 0x0200048B RID: 1163
		// (Invoke) Token: 0x06002798 RID: 10136
		public delegate bool Filter(UserAgent userAgent);
	}
}
