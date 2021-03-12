using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200001F RID: 31
	public class TextComparer : ITextComparer, ISupportTextComparer
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000794B File Offset: 0x00005B4B
		public virtual int Compare(object x, object y, ICustomFormatter customFormatter, IFormatProvider formatProvider, string formatString, string defaultEmptyText)
		{
			return string.Compare(this.Format(x, customFormatter, formatProvider, formatString, defaultEmptyText), this.Format(y, customFormatter, formatProvider, formatString, defaultEmptyText), StringComparison.CurrentCulture);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007970 File Offset: 0x00005B70
		public virtual string Format(object item, ICustomFormatter customFormatter, IFormatProvider formatProvider, string formatString, string defaultEmptyText)
		{
			string text = string.Empty;
			if (customFormatter != null)
			{
				text = customFormatter.Format(formatString, item, formatProvider);
			}
			else
			{
				text = item.ToUserFriendText();
			}
			if (string.IsNullOrEmpty(text))
			{
				text = defaultEmptyText;
			}
			return text;
		}

		// Token: 0x0400006F RID: 111
		public static TextComparer DefaultTextComparer = new TextComparer();
	}
}
