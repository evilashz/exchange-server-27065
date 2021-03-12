using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200035D RID: 861
	public class DropDownListItem
	{
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x000BBF11 File Offset: 0x000BA111
		public string ItemValue
		{
			get
			{
				return this.itemValue;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x000BBF19 File Offset: 0x000BA119
		public bool IsBold
		{
			get
			{
				return this.isBold;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x000BBF21 File Offset: 0x000BA121
		public string Display
		{
			get
			{
				if (this.displayIDs != -1018465893)
				{
					return LocalizedStrings.GetNonEncoded(this.displayIDs);
				}
				return this.displayText;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x000BBF42 File Offset: 0x000BA142
		public string ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x000BBF4A File Offset: 0x000BA14A
		public bool IsDisplayTextHtmlEncoded
		{
			get
			{
				return this.isDisplayTextHtmlEncoded;
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000BBF54 File Offset: 0x000BA154
		public DropDownListItem(object itemValue, Strings.IDs display)
		{
			if (itemValue == null)
			{
				throw new ArgumentNullException("itemValue");
			}
			if (display == -1018465893)
			{
				throw new ArgumentException("display cannot be equal to Strings.IDs.NotSet");
			}
			this.itemValue = itemValue.ToString();
			this.displayIDs = display;
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000BBFA6 File Offset: 0x000BA1A6
		public DropDownListItem(object itemValue, string display) : this(itemValue, display, false)
		{
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000BBFB4 File Offset: 0x000BA1B4
		public DropDownListItem(object itemValue, string display, bool isDisplayTextHtmlEncoded)
		{
			if (itemValue == null)
			{
				throw new ArgumentNullException("itemValue");
			}
			if (string.IsNullOrEmpty(display))
			{
				throw new ArgumentException("display cannot be null or empty string");
			}
			this.itemValue = itemValue.ToString();
			this.displayText = display;
			this.isDisplayTextHtmlEncoded = isDisplayTextHtmlEncoded;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000BC00D File Offset: 0x000BA20D
		public DropDownListItem(object itemValue, Strings.IDs display, bool isBold) : this(itemValue, display)
		{
			this.isBold = isBold;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000BC01E File Offset: 0x000BA21E
		public DropDownListItem(object itemValue, Strings.IDs display, string id, bool isBold) : this(itemValue, display)
		{
			this.isBold = isBold;
			this.itemId = id;
		}

		// Token: 0x0400176A RID: 5994
		private string itemValue;

		// Token: 0x0400176B RID: 5995
		private Strings.IDs displayIDs = -1018465893;

		// Token: 0x0400176C RID: 5996
		private string displayText;

		// Token: 0x0400176D RID: 5997
		private bool isBold;

		// Token: 0x0400176E RID: 5998
		private string itemId;

		// Token: 0x0400176F RID: 5999
		private bool isDisplayTextHtmlEncoded;
	}
}
