using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003AA RID: 938
	public class LanguageDropDownListItem : DropDownListItem
	{
		// Token: 0x06002350 RID: 9040 RVA: 0x000CB68F File Offset: 0x000C988F
		public LanguageDropDownListItem(object itemValue, string display, bool isRtl) : base(itemValue, display)
		{
			if (itemValue == null)
			{
				throw new ArgumentNullException("itemValue");
			}
			if (string.IsNullOrEmpty(display))
			{
				throw new ArgumentException("display cannot be null or empty string");
			}
			this.isRtl = isRtl;
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000CB6C1 File Offset: 0x000C98C1
		public bool IsRtl
		{
			get
			{
				return this.isRtl;
			}
		}

		// Token: 0x040018AD RID: 6317
		private bool isRtl;
	}
}
