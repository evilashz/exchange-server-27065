using System;
using System.Security.Permissions;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001EC RID: 492
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class OwaPageCached : OwaPage
	{
		// Token: 0x06000FFB RID: 4091 RVA: 0x0006356F File Offset: 0x0006176F
		public OwaPageCached() : base(false)
		{
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00063578 File Offset: 0x00061778
		protected static int StoreObjectTypeCalendarItem
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0006357C File Offset: 0x0006177C
		protected static int StoreObjectTypeMessage
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00063580 File Offset: 0x00061780
		protected override bool IsTextHtml
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00063583 File Offset: 0x00061783
		protected override void OnInit(EventArgs e)
		{
			Utilities.MakePageCacheable(base.Response);
			base.OnInit(e);
		}
	}
}
