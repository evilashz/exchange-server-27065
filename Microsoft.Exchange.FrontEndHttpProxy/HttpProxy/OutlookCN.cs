using System;
using System.Web.Configuration;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200005F RID: 95
	public class OutlookCN : OwaPage
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00012B40 File Offset: 0x00010D40
		protected string IcpLink
		{
			get
			{
				if (OutlookCN.icpLink == null)
				{
					OutlookCN.icpLink = WebConfigurationManager.AppSettings["GallatinIcpLink"];
				}
				return OutlookCN.icpLink;
			}
		}

		// Token: 0x040001E9 RID: 489
		private const string IcpLinkAppSetting = "GallatinIcpLink";

		// Token: 0x040001EA RID: 490
		private static string icpLink;
	}
}
