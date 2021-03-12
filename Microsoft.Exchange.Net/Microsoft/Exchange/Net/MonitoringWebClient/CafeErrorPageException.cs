using System;
using System.Text;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000778 RID: 1912
	internal class CafeErrorPageException : HttpWebResponseWrapperException
	{
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x0004FF14 File Offset: 0x0004E114
		// (set) Token: 0x060025EB RID: 9707 RVA: 0x0004FF1C File Offset: 0x0004E11C
		public CafeErrorPage CafeErrorPage { get; private set; }

		// Token: 0x060025EC RID: 9708 RVA: 0x0004FF25 File Offset: 0x0004E125
		public CafeErrorPageException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, CafeErrorPage cafeErrorPage) : base(message, request, response)
		{
			this.CafeErrorPage = cafeErrorPage;
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x0004FF38 File Offset: 0x0004E138
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}{1}", base.GetType().FullName, Environment.NewLine);
				stringBuilder.AppendFormat("{0}{1}", this.CafeErrorPage, Environment.NewLine);
				stringBuilder.Append(base.Message);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x0004FF94 File Offset: 0x0004E194
		public override string ExceptionHint
		{
			get
			{
				return string.Concat(new object[]
				{
					"CafeErrorPage: ",
					this.CafeErrorPage.FailureReason,
					" ",
					this.CafeErrorPage.RequestFailureContext.Error
				});
			}
		}
	}
}
