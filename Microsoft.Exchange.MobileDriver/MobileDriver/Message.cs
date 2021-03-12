using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000035 RID: 53
	internal class Message
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00006AAC File Offset: 0x00004CAC
		public Message(string text) : this(new ProportionedText[]
		{
			new ProportionedText(text)
		})
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public Message(IList<ProportionedText> texts) : this(texts, DateTime.MaxValue.ToUniversalTime(), DateTime.UtcNow)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006AF8 File Offset: 0x00004CF8
		public Message(string text, DateTime expiryTimeUtc, DateTime deferralTimeUtc) : this(new ProportionedText[]
		{
			new ProportionedText(text)
		}, expiryTimeUtc, deferralTimeUtc)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006B1E File Offset: 0x00004D1E
		public Message(IList<ProportionedText> texts, DateTime expiryTimeUtc, DateTime deferralTimeUtc)
		{
			if (texts == null)
			{
				throw new ArgumentNullException("texts");
			}
			if (!texts.IsReadOnly)
			{
				texts = new ReadOnlyCollection<ProportionedText>(texts);
			}
			this.ProportionedTexts = texts;
			this.ExpiryTimeUtc = expiryTimeUtc;
			this.DeferralTimeUtc = deferralTimeUtc;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00006B59 File Offset: 0x00004D59
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00006B61 File Offset: 0x00004D61
		public IList<ProportionedText> ProportionedTexts { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00006B6A File Offset: 0x00004D6A
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00006B72 File Offset: 0x00004D72
		public DateTime ExpiryTimeUtc { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00006B7B File Offset: 0x00004D7B
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00006B83 File Offset: 0x00004D83
		public DateTime DeferralTimeUtc { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00006B8C File Offset: 0x00004D8C
		public string OriginalText
		{
			get
			{
				if (this.originalText == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (ProportionedText value in this.ProportionedTexts)
					{
						stringBuilder.Append(value);
					}
					this.originalText = stringBuilder.ToString();
				}
				return this.originalText;
			}
		}

		// Token: 0x040000A7 RID: 167
		private string originalText;
	}
}
