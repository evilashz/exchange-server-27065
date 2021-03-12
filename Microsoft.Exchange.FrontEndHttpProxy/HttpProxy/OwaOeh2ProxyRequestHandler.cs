using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000C3 RID: 195
	internal class OwaOeh2ProxyRequestHandler : OwaProxyRequestHandler
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0002B6E5 File Offset: 0x000298E5
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.Internal;
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0002B6E8 File Offset: 0x000298E8
		protected override Uri GetTargetBackEndServerUrl()
		{
			Uri targetBackEndServerUrl = base.GetTargetBackEndServerUrl();
			return UrlUtilities.FixIntegratedAuthUrlForBackEnd(targetBackEndServerUrl);
		}
	}
}
