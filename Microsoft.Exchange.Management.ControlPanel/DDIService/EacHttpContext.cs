using System;
using System.Web;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200013B RID: 315
	public class EacHttpContext : IEacHttpContext
	{
		// Token: 0x060020FD RID: 8445 RVA: 0x00063FD1 File Offset: 0x000621D1
		private EacHttpContext()
		{
		}

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x00063FD9 File Offset: 0x000621D9
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x00063FE0 File Offset: 0x000621E0
		public static IEacHttpContext Instance { get; internal set; } = new EacHttpContext();

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x00063FE8 File Offset: 0x000621E8
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x00064003 File Offset: 0x00062203
		public ShouldContinueContext ShouldContinueContext
		{
			get
			{
				return HttpContext.Current.Items["ShouldContinueContext"] as ShouldContinueContext;
			}
			set
			{
				HttpContext.Current.Items["ShouldContinueContext"] = value;
			}
		}

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x0006401C File Offset: 0x0006221C
		public bool PostHydrationActionPresent
		{
			get
			{
				HttpCookie httpCookie = HttpContext.Current.Request.Cookies["PostHydrationAction"];
				return httpCookie != null && httpCookie.Value == "1";
			}
		}
	}
}
