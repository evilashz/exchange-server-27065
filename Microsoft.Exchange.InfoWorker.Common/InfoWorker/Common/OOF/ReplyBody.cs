using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000029 RID: 41
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class ReplyBody
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005416 File Offset: 0x00003616
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00005432 File Offset: 0x00003632
		[XmlElement]
		public string Message
		{
			get
			{
				if (this.SetByLegacyClient)
				{
					return TextUtil.ConvertPlainTextToHtml(this.message);
				}
				return this.message;
			}
			set
			{
				if (value != null && value.Length > ReplyBody.MaxMessageLength)
				{
					value = value.Substring(0, ReplyBody.MaxMessageLength);
				}
				this.message = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005459 File Offset: 0x00003659
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00005461 File Offset: 0x00003661
		[XmlIgnore]
		internal string RawMessage
		{
			get
			{
				return this.message;
			}
			set
			{
				if (value != null && value.Length > ReplyBody.MaxMessageLength)
				{
					value = value.Substring(0, ReplyBody.MaxMessageLength);
				}
				this.message = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005488 File Offset: 0x00003688
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00005490 File Offset: 0x00003690
		[XmlIgnore]
		internal bool SetByLegacyClient
		{
			get
			{
				return this.setByLegacyClient;
			}
			set
			{
				this.setByLegacyClient = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005499 File Offset: 0x00003699
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000054A1 File Offset: 0x000036A1
		[XmlAttribute("xml:lang")]
		public string LanguageTag
		{
			get
			{
				return this.languageTag;
			}
			set
			{
				this.languageTag = value;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000054AA File Offset: 0x000036AA
		internal static ReplyBody Create()
		{
			return new ReplyBody();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000054B4 File Offset: 0x000036B4
		internal static ReplyBody CreateDefault()
		{
			return new ReplyBody
			{
				RawMessage = string.Empty,
				SetByLegacyClient = false
			};
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000054DA File Offset: 0x000036DA
		private ReplyBody()
		{
		}

		// Token: 0x04000072 RID: 114
		private string message;

		// Token: 0x04000073 RID: 115
		private string languageTag;

		// Token: 0x04000074 RID: 116
		private bool setByLegacyClient;

		// Token: 0x04000075 RID: 117
		public static int MaxMessageLength = 128000;
	}
}
