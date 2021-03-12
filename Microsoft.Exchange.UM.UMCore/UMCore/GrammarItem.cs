using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000154 RID: 340
	internal class GrammarItem : GrammarItemBase
	{
		// Token: 0x06000A26 RID: 2598 RVA: 0x0002B6C3 File Offset: 0x000298C3
		internal GrammarItem(string item, CultureInfo transcriptionLanguage) : this(item, string.Empty, transcriptionLanguage)
		{
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002B6D2 File Offset: 0x000298D2
		internal GrammarItem(string item, string tag, CultureInfo transcriptionLanguage) : this(item, tag, transcriptionLanguage, 1f)
		{
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002B6E4 File Offset: 0x000298E4
		internal GrammarItem(string item, string tag, CultureInfo transcriptionLanguage, float weight) : base(weight)
		{
			this.item = SpeechUtils.SrgsEncode(item);
			this.tag = SpeechUtils.SrgsEncode(tag);
			if (string.IsNullOrEmpty(Utils.TrimSpaces(this.item)))
			{
				this.item = string.Empty;
			}
			if (string.IsNullOrEmpty(Utils.TrimSpaces(this.tag)))
			{
				this.tag = string.Empty;
			}
			this.transcriptionLanguage = transcriptionLanguage;
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0002B752 File Offset: 0x00029952
		public override bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.item);
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002B760 File Offset: 0x00029960
		public override bool Equals(GrammarItemBase otherItemBase)
		{
			GrammarItem grammarItem = otherItemBase as GrammarItem;
			return grammarItem != null && string.Compare(grammarItem.item, this.item, true, this.transcriptionLanguage) == 0 && string.Equals(grammarItem.tag, this.tag, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002B7A8 File Offset: 0x000299A8
		protected override string GetInnerItem()
		{
			ExAssert.RetailAssert(!this.IsEmpty, "We should never be trying to get the XML for an empty grammar item");
			string str;
			if (string.IsNullOrEmpty(this.tag))
			{
				str = string.Format(CultureInfo.InvariantCulture, "\r\n                <item>{0}</item>", new object[]
				{
					this.item
				});
			}
			else
			{
				str = string.Format(CultureInfo.InvariantCulture, "\r\n                <item>{0}</item>\r\n                <tag>{1}</tag>", new object[]
				{
					this.item,
					this.tag
				});
			}
			return str + GrammarItem.customGrammarTrueTag;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002B82F File Offset: 0x00029A2F
		protected override int InternalGetHashCode()
		{
			return this.item.GetHashCode() ^ this.tag.GetHashCode();
		}

		// Token: 0x04000938 RID: 2360
		private static readonly string customGrammarTrueTag = string.Format(CultureInfo.InvariantCulture, "\r\n                <tag>out.{0} = {1};</tag>", new object[]
		{
			"customGrammarWords",
			"true"
		});

		// Token: 0x04000939 RID: 2361
		private string item;

		// Token: 0x0400093A RID: 2362
		private string tag;

		// Token: 0x0400093B RID: 2363
		private CultureInfo transcriptionLanguage;
	}
}
