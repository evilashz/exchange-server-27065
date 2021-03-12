using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200001A RID: 26
	internal class UrlAnchorMailbox : AnchorMailbox
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00005488 File Offset: 0x00003688
		public UrlAnchorMailbox(Uri url, IRequestContext requestContext) : base(AnchorSource.Url, url, requestContext)
		{
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005494 File Offset: 0x00003694
		public Uri Url
		{
			get
			{
				return (Uri)base.SourceObject;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000054A1 File Offset: 0x000036A1
		public override BackEndServer TryDirectBackEndCalculation()
		{
			return new BackEndServer(this.Url.Host, Server.E15MinVersion);
		}
	}
}
