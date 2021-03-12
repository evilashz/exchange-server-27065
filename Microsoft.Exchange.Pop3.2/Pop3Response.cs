using System;
using System.Text;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x0200001A RID: 26
	internal class Pop3Response : ProtocolResponse
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000586B File Offset: 0x00003A6B
		internal Pop3Response(Pop3Response.Type pop3ResponseType) : this(null, pop3ResponseType, null)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005876 File Offset: 0x00003A76
		internal Pop3Response(ProtocolRequest request, Pop3Response.Type pop3ResponseType) : this(request, pop3ResponseType, null)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005881 File Offset: 0x00003A81
		internal Pop3Response(Pop3Response.Type pop3ResponseType, string input) : this(null, pop3ResponseType, input)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000588C File Offset: 0x00003A8C
		internal Pop3Response(ProtocolRequest request, Pop3Response.Type pop3ResponseType, string input) : base(request, input)
		{
			this.pop3ResponseType = pop3ResponseType;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000589D File Offset: 0x00003A9D
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000058A5 File Offset: 0x00003AA5
		public Pop3Response.Type ResponseType
		{
			get
			{
				return this.pop3ResponseType;
			}
			set
			{
				this.pop3ResponseType = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000058AE File Offset: 0x00003AAE
		public override bool IsCommandFailedResponse
		{
			get
			{
				return this.pop3ResponseType == Pop3Response.Type.err;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000058BC File Offset: 0x00003ABC
		public override string DataToSend
		{
			get
			{
				string text;
				switch (this.pop3ResponseType)
				{
				case Pop3Response.Type.ok:
					text = "+OK";
					break;
				case Pop3Response.Type.err:
					text = "-ERR";
					break;
				default:
					return base.DataToSend;
				}
				string text2 = base.DataToSend;
				StringBuilder authError = base.GetAuthError();
				if (authError != null)
				{
					authError.Length = Math.Min(authError.Length, 512 - (text.Length + text2.Length + 3 + 2));
					text2 = text2 + " [" + authError.ToString() + "]";
				}
				if (string.IsNullOrEmpty(text2))
				{
					return text;
				}
				if (text2.StartsWith("\r\n"))
				{
					return text + text2;
				}
				return string.Format("{0} {1}", text, text2);
			}
		}

		// Token: 0x0400006C RID: 108
		internal const string Ok = "+OK";

		// Token: 0x0400006D RID: 109
		internal const string Err = "-ERR";

		// Token: 0x0400006E RID: 110
		internal static readonly byte[] OkBuf = Encoding.ASCII.GetBytes("+ok");

		// Token: 0x0400006F RID: 111
		internal static readonly byte[] ErrBuf = Encoding.ASCII.GetBytes("-err");

		// Token: 0x04000070 RID: 112
		private Pop3Response.Type pop3ResponseType;

		// Token: 0x0200001B RID: 27
		internal enum Type
		{
			// Token: 0x04000072 RID: 114
			ok,
			// Token: 0x04000073 RID: 115
			err,
			// Token: 0x04000074 RID: 116
			unknown
		}
	}
}
