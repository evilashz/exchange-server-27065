using System;
using System.Text;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007CE RID: 1998
	internal class OwaErrorPageException : HttpWebResponseWrapperException
	{
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x0005893C File Offset: 0x00056B3C
		// (set) Token: 0x06002949 RID: 10569 RVA: 0x00058944 File Offset: 0x00056B44
		public OwaErrorPage OwaErrorPage { get; private set; }

		// Token: 0x0600294A RID: 10570 RVA: 0x0005894D File Offset: 0x00056B4D
		public OwaErrorPageException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, OwaErrorPage owaErrorPage) : base(message, request, response)
		{
			this.OwaErrorPage = owaErrorPage;
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x00058960 File Offset: 0x00056B60
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}{1}", base.GetType().FullName, Environment.NewLine);
				stringBuilder.AppendFormat("{0}{1}", this.OwaErrorPage, Environment.NewLine);
				stringBuilder.Append(base.Message);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000589B9 File Offset: 0x00056BB9
		public override string ExceptionHint
		{
			get
			{
				return "OwaErrorPage: " + this.OwaErrorPage.ExceptionType;
			}
		}
	}
}
