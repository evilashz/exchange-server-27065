using System;
using System.Web;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation
{
	// Token: 0x020001FD RID: 509
	internal class HttpCallContext : IContextPlugin
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0003D214 File Offset: 0x0003B414
		public static IContextPlugin Singleton
		{
			get
			{
				return HttpCallContext.singleton;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0003D21C File Offset: 0x0003B41C
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0003D25C File Offset: 0x0003B45C
		public Guid? LocalId
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null && httpContext.Items != null)
				{
					return (Guid?)httpContext.Items["MSExchangeLocalId"];
				}
				return null;
			}
			set
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null && httpContext.Items != null)
				{
					if (value != null)
					{
						httpContext.Items["MSExchangeLocalId"] = value;
						return;
					}
					httpContext.Items.Remove("MSExchangeLocalId");
				}
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0003D2AA File Offset: 0x0003B4AA
		public bool IsContextPresent
		{
			get
			{
				return HttpCallContext.IsHttpContextValid(HttpContext.Current);
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003D2B6 File Offset: 0x0003B4B6
		public void SetId()
		{
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0003D2B8 File Offset: 0x0003B4B8
		public bool CheckId()
		{
			return HttpCallContext.IsHttpContextValid(HttpContext.Current);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0003D2C4 File Offset: 0x0003B4C4
		public void Clear()
		{
			HttpContext httpContext = HttpContext.Current;
			if (HttpCallContext.IsHttpContextValid(httpContext))
			{
				httpContext.Items.Remove("MSExchangeLocalId");
				httpContext.Items.Remove("SingleContextIdKey");
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0003D2FF File Offset: 0x0003B4FF
		private static bool IsHttpContextValid(HttpContext current)
		{
			return current != null && current.Items != null;
		}

		// Token: 0x04000AAB RID: 2731
		private static IContextPlugin singleton = new HttpCallContext();
	}
}
