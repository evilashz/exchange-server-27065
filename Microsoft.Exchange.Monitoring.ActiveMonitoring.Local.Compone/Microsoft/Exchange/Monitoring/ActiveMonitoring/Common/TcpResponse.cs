using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000BC RID: 188
	public abstract class TcpResponse
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x0002613C File Offset: 0x0002433C
		public TcpResponse(string responseString)
		{
			this.responseString = responseString;
			this.responseMessage = null;
			this.responseType = this.ParseResponseType(responseString);
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0002615F File Offset: 0x0002435F
		public static char[] AllDelimiters
		{
			get
			{
				return TcpResponse.allDelimiters;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00026166 File Offset: 0x00024366
		public static char[] LineDelimiters
		{
			get
			{
				return TcpResponse.lineDelimiters;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0002616D File Offset: 0x0002436D
		public static char[] WordDelimiters
		{
			get
			{
				return TcpResponse.wordDelimiters;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00026174 File Offset: 0x00024374
		public string ResponseString
		{
			get
			{
				return this.responseString;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0002617C File Offset: 0x0002437C
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00026184 File Offset: 0x00024384
		public string ResponseMessage
		{
			get
			{
				return this.responseMessage;
			}
			set
			{
				this.responseMessage = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0002618D File Offset: 0x0002438D
		public ResponseType ResponseType
		{
			get
			{
				return this.responseType;
			}
		}

		// Token: 0x06000670 RID: 1648
		public abstract ResponseType ParseResponseType(string responseString);

		// Token: 0x06000671 RID: 1649 RVA: 0x00026195 File Offset: 0x00024395
		public override string ToString()
		{
			return this.responseString;
		}

		// Token: 0x0400040F RID: 1039
		private readonly string responseString;

		// Token: 0x04000410 RID: 1040
		private static char[] allDelimiters = new char[]
		{
			' ',
			'\n'
		};

		// Token: 0x04000411 RID: 1041
		private static char[] lineDelimiters = new char[]
		{
			'\n'
		};

		// Token: 0x04000412 RID: 1042
		private static char[] wordDelimiters = new char[]
		{
			' '
		};

		// Token: 0x04000413 RID: 1043
		private ResponseType responseType;

		// Token: 0x04000414 RID: 1044
		private string responseMessage;
	}
}
