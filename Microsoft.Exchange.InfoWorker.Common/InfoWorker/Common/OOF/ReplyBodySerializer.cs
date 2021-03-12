using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x0200002A RID: 42
	public class ReplyBodySerializer
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x000054F0 File Offset: 0x000036F0
		internal static ReplyBodySerializer Serialize(ReplyBody replyBody)
		{
			return new ReplyBodySerializer
			{
				Message = replyBody.RawMessage,
				LanguageTag = replyBody.LanguageTag
			};
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000551C File Offset: 0x0000371C
		internal ReplyBody Deserialize()
		{
			ReplyBody replyBody = ReplyBody.Create();
			replyBody.RawMessage = this.Message;
			replyBody.LanguageTag = this.LanguageTag;
			return replyBody;
		}

		// Token: 0x04000076 RID: 118
		[XmlElement]
		public string Message = string.Empty;

		// Token: 0x04000077 RID: 119
		[XmlElement]
		public string LanguageTag;
	}
}
