using System;
using System.Web;
using Microsoft.Exchange.Clients.Common.FBL;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000063 RID: 99
	public class SvmFeedback : OwaPage
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00012EE5 File Offset: 0x000110E5
		public bool IsProcessingSuccessful
		{
			get
			{
				return SvmFeedback.isProcessingSuccessful;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00012EEC File Offset: 0x000110EC
		public bool? IsClassifyRequest
		{
			get
			{
				return SvmFeedback.isClassifyRequest;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00012EF3 File Offset: 0x000110F3
		public bool? IsOptIn
		{
			get
			{
				return SvmFeedback.isOptIn;
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00012EFA File Offset: 0x000110FA
		protected override void OnLoad(EventArgs e)
		{
			SvmFeedback.ProcessSvmFeedbackRequest();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00012F04 File Offset: 0x00011104
		private static void ProcessSvmFeedbackRequest()
		{
			HttpRequest request = HttpContext.Current.Request;
			if (request.IsSecureConnection)
			{
				string query = request.Url.Query;
				OwaFblHandler owaFblHandler = new OwaFblHandler();
				try
				{
					SvmFeedback.isProcessingSuccessful = owaFblHandler.TryProcessFbl(query, out SvmFeedback.isClassifyRequest, out SvmFeedback.isOptIn);
				}
				catch (Exception)
				{
					SvmFeedback.isProcessingSuccessful = false;
				}
			}
		}

		// Token: 0x0400021D RID: 541
		private static bool isProcessingSuccessful;

		// Token: 0x0400021E RID: 542
		private static bool? isClassifyRequest;

		// Token: 0x0400021F RID: 543
		private static bool? isOptIn;
	}
}
