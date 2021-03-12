using System;
using System.Text;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200077E RID: 1918
	internal class ScenarioTimeoutException : HttpWebResponseWrapperException
	{
		// Token: 0x06002603 RID: 9731 RVA: 0x000501FF File Offset: 0x0004E3FF
		public ScenarioTimeoutException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response) : base(message, request, response)
		{
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x0005020C File Offset: 0x0004E40C
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.GetType().FullName + Environment.NewLine);
				stringBuilder.Append(base.Message);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x00050250 File Offset: 0x0004E450
		public override string ExceptionHint
		{
			get
			{
				string str = (base.Request != null) ? base.Request.RequestUri.Host : string.Empty;
				return "ScenarioTimeout: " + str;
			}
		}
	}
}
