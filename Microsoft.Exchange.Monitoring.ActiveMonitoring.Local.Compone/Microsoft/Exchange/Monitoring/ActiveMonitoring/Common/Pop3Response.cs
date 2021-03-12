using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000C0 RID: 192
	public class Pop3Response : TcpResponse
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x0002647A File Offset: 0x0002467A
		public Pop3Response(string responseString) : base(responseString)
		{
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00026484 File Offset: 0x00024684
		public override ResponseType ParseResponseType(string responseString)
		{
			string[] array = base.ResponseString.Trim().Split(TcpResponse.AllDelimiters, 2);
			if (array.Length == 2)
			{
				base.ResponseMessage = array[1].Trim();
			}
			array[0] = array[0].Trim();
			if (array[0] == "+")
			{
				return ResponseType.sendMore;
			}
			if (array.Length == 0)
			{
				return ResponseType.unknown;
			}
			if (string.Compare(array[0], "+OK", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return ResponseType.success;
			}
			if (string.Compare(array[0], "-ERR", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return ResponseType.failure;
			}
			return ResponseType.unknown;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00026504 File Offset: 0x00024704
		public bool ParseSTATResponse(out int numMessages, out long mailboxSize)
		{
			return this.ParseTwoNumberResponse(out numMessages, out mailboxSize);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00026510 File Offset: 0x00024710
		internal bool ParseTwoNumberResponse(out int numMessages, out long mailboxSize)
		{
			numMessages = 0;
			mailboxSize = 0L;
			string[] array = base.ResponseString.Trim().Split(TcpResponse.WordDelimiters);
			return array.Length == 3 && int.TryParse(array[1], out numMessages) && long.TryParse(array[2], out mailboxSize);
		}

		// Token: 0x0400041C RID: 1052
		internal const string SendMore = "+";

		// Token: 0x020000C1 RID: 193
		internal enum MultilineParse
		{
			// Token: 0x0400041E RID: 1054
			size,
			// Token: 0x0400041F RID: 1055
			uid
		}
	}
}
