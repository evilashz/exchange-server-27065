using System;
using System.Net;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000BD RID: 189
	public class Imap4Connection : TcpConnection
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x000261E8 File Offset: 0x000243E8
		public Imap4Connection(IPEndPoint endpoint) : base(endpoint)
		{
			this.tag = 0;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x000261F8 File Offset: 0x000243F8
		public int NextTag
		{
			get
			{
				return ++this.tag;
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00026216 File Offset: 0x00024416
		public TcpResponse SendRequest(string request, string expectedTag)
		{
			base.SendData(request);
			return base.GetResponse(120, expectedTag, false);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00026229 File Offset: 0x00024429
		public TcpResponse GetResponse(int timeout, string expectedTag)
		{
			return base.GetResponse(timeout, expectedTag, false);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00026234 File Offset: 0x00024434
		public override bool LastLineReceived(string responseString, string expectedTag, bool multiLine)
		{
			if (expectedTag == null)
			{
				expectedTag = ((this.tag > 0) ? this.tag.ToString() : "*");
			}
			int num = responseString.LastIndexOf("\r\n");
			if (num <= 0)
			{
				return false;
			}
			if (responseString.StartsWith("+", StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}
			num = responseString.LastIndexOf("\r\n", num - 1);
			if (num != -1)
			{
				num += 2;
				responseString = responseString.Substring(num, responseString.Length - num);
			}
			return responseString.StartsWith("+", StringComparison.InvariantCultureIgnoreCase) || responseString.StartsWith(expectedTag, StringComparison.InvariantCultureIgnoreCase) || responseString.StartsWith("* BYE", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000262D5 File Offset: 0x000244D5
		public override TcpResponse CreateResponse(string responseString)
		{
			return new Imap4Response(responseString);
		}

		// Token: 0x04000415 RID: 1045
		private const string SyncResponse = "+";

		// Token: 0x04000416 RID: 1046
		private const string ByeTag = "* BYE";

		// Token: 0x04000417 RID: 1047
		private int tag;
	}
}
