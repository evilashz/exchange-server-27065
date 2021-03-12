using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000BE RID: 190
	public class Imap4Response : TcpResponse
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x000262DD File Offset: 0x000244DD
		public Imap4Response(string responseString) : base(responseString)
		{
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x000262E6 File Offset: 0x000244E6
		public string[] ResponseLines
		{
			get
			{
				return this.responseLines;
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000262F0 File Offset: 0x000244F0
		public override ResponseType ParseResponseType(string responseString)
		{
			if (responseString.StartsWith("+", StringComparison.InvariantCultureIgnoreCase))
			{
				return ResponseType.sendMore;
			}
			this.responseLines = base.ResponseString.Trim().Replace("\r", string.Empty).Split(TcpResponse.LineDelimiters);
			string[] array = this.responseLines[this.responseLines.Length - 1].Trim().Split(TcpResponse.WordDelimiters, 3);
			if (array.Length < 1)
			{
				return ResponseType.unknown;
			}
			if (array[0] == "+")
			{
				if (array.Length == 2)
				{
					base.ResponseMessage = array[1];
				}
				return ResponseType.sendMore;
			}
			if (array.Length < 2)
			{
				return ResponseType.unknown;
			}
			string strA = array[1].ToLower();
			if (array.Length == 3)
			{
				base.ResponseMessage = array[2];
				int num = base.ResponseMessage.IndexOf(']');
				if (base.ResponseMessage[0] == '[' && num > 0)
				{
					base.ResponseMessage = base.ResponseMessage.Substring(num + 2);
				}
			}
			if (string.Compare(strA, "ok", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return ResponseType.success;
			}
			if (string.Compare(strA, "no", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return ResponseType.failure;
			}
			if (string.Compare(strA, "bad", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return ResponseType.error;
			}
			if (string.Compare(strA, "bye", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return ResponseType.bye;
			}
			return ResponseType.unknown;
		}

		// Token: 0x04000418 RID: 1048
		public const string SyncResponse = "+";

		// Token: 0x04000419 RID: 1049
		private string[] responseLines;
	}
}
