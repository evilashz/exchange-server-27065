using System;
using System.Net;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000BF RID: 191
	public class Pop3Connection : TcpConnection
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x00026418 File Offset: 0x00024618
		public Pop3Connection(IPEndPoint endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00026421 File Offset: 0x00024621
		public TcpResponse SendRequest(string request, bool multiLine)
		{
			base.SendData(request);
			return base.GetResponse(120, null, multiLine);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00026434 File Offset: 0x00024634
		public override bool LastLineReceived(string responseString, string expectedTag, bool multiLine)
		{
			int num = responseString.LastIndexOf("\r\n");
			return num > 0 && (!multiLine || !responseString.StartsWith("+OK", StringComparison.InvariantCultureIgnoreCase) || responseString.EndsWith("\r\n.\r\n", StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00026472 File Offset: 0x00024672
		public override TcpResponse CreateResponse(string responseString)
		{
			return new Pop3Response(responseString);
		}

		// Token: 0x0400041A RID: 1050
		private const string MultiLineStart = "+OK";

		// Token: 0x0400041B RID: 1051
		private const string MultiLineEnd = "\r\n.\r\n";
	}
}
