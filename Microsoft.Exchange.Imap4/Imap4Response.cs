using System;
using System.Text;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000012 RID: 18
	internal class Imap4Response : ProtocolResponse
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000071E8 File Offset: 0x000053E8
		internal Imap4Response(Imap4Request request, Imap4Response.Type imap4ResponseType) : this(request, imap4ResponseType, null)
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000071F3 File Offset: 0x000053F3
		internal Imap4Response(Imap4Request request, Imap4Response.Type imap4ResponseType, string input) : base(request, input)
		{
			this.imap4ResponseType = imap4ResponseType;
		}

		// Token: 0x17000051 RID: 81
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00007204 File Offset: 0x00005404
		public string ResponseCodes
		{
			set
			{
				this.responseCodes = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000720D File Offset: 0x0000540D
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00007215 File Offset: 0x00005415
		public Imap4Response.Type ResponseType
		{
			get
			{
				return this.imap4ResponseType;
			}
			set
			{
				this.imap4ResponseType = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000721E File Offset: 0x0000541E
		public override bool IsCommandFailedResponse
		{
			get
			{
				return this.imap4ResponseType == Imap4Response.Type.no || this.imap4ResponseType == Imap4Response.Type.bad;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00007234 File Offset: 0x00005434
		public override string DataToSend
		{
			get
			{
				if (base.Request != null)
				{
					return this.AddNotificationsAndTag(base.DataToSend);
				}
				return base.DataToSend;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007254 File Offset: 0x00005454
		private string AddNotificationsAndTag(string responseString)
		{
			string value = string.Empty;
			Imap4Request imap4Request = base.Request as Imap4Request;
			if (imap4Request != null && imap4Request.Factory.SelectedMailbox != null)
			{
				value = imap4Request.Factory.SelectedMailbox.GetNotifications(imap4Request.AllowsExpungeNotifications);
			}
			string value2 = (imap4Request != null) ? imap4Request.Tag : "*";
			string text = string.Empty;
			switch (this.imap4ResponseType)
			{
			case Imap4Response.Type.ok:
				text = "OK";
				break;
			case Imap4Response.Type.no:
				text = "NO";
				break;
			case Imap4Response.Type.bad:
				text = "BAD";
				break;
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.Append(value);
			if (this.imap4ResponseType == Imap4Response.Type.unknown)
			{
				if (!string.IsNullOrEmpty(responseString))
				{
					stringBuilder.Append(responseString);
				}
				return stringBuilder.ToString();
			}
			stringBuilder.Append(value2);
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendFormat(" {0}", text);
			}
			StringBuilder authError = base.GetAuthError();
			if (authError != null)
			{
				if (!string.IsNullOrEmpty(this.responseCodes))
				{
					throw new ApplicationException(string.Format("Unexpected condition: both loginResponseCodes {} and responseCodes {1} are not null", authError, this.responseCodes));
				}
				stringBuilder.AppendFormat(" [{0}]", authError.ToString());
			}
			else if (!string.IsNullOrEmpty(this.responseCodes))
			{
				stringBuilder.AppendFormat(" {0}", this.responseCodes);
			}
			if (responseString.IndexOf("[*]", StringComparison.Ordinal) > -1)
			{
				return responseString.Replace("[*]", stringBuilder.ToString());
			}
			if (!string.IsNullOrEmpty(responseString))
			{
				stringBuilder.AppendFormat(" {0}", responseString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000092 RID: 146
		internal const string Untagged = "*";

		// Token: 0x04000093 RID: 147
		internal const string TagPlaceHolder = "[*]";

		// Token: 0x04000094 RID: 148
		internal const string Ok = "OK";

		// Token: 0x04000095 RID: 149
		internal const string No = "NO";

		// Token: 0x04000096 RID: 150
		internal const string Bad = "BAD";

		// Token: 0x04000097 RID: 151
		internal const string TryCreate = "[TRYCREATE]";

		// Token: 0x04000098 RID: 152
		private Imap4Response.Type imap4ResponseType;

		// Token: 0x04000099 RID: 153
		private string responseCodes;

		// Token: 0x02000013 RID: 19
		internal enum Type
		{
			// Token: 0x0400009B RID: 155
			ok,
			// Token: 0x0400009C RID: 156
			no,
			// Token: 0x0400009D RID: 157
			bad,
			// Token: 0x0400009E RID: 158
			unknown
		}
	}
}
