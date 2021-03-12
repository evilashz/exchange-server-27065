using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x020001AF RID: 431
	internal class StoreTagData : AdTagData
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x00030ED0 File Offset: 0x0002F0D0
		internal StoreTagData()
		{
			base.ContentSettings = new SortedDictionary<Guid, ContentSetting>();
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00030EE4 File Offset: 0x0002F0E4
		internal StoreTagData(AdTagData adTagData)
		{
			base.Tag = adTagData.Tag;
			base.ContentSettings = adTagData.ContentSettings;
			this.optedInto = false;
			if (base.Tag.Type == ElcFolderType.Personal)
			{
				this.isVisible = true;
				return;
			}
			this.isVisible = false;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00030F34 File Offset: 0x0002F134
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x00030F3C File Offset: 0x0002F13C
		internal bool OptedInto
		{
			get
			{
				return this.optedInto;
			}
			set
			{
				this.optedInto = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00030F45 File Offset: 0x0002F145
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x00030F4D File Offset: 0x0002F14D
		internal bool IsVisible
		{
			get
			{
				return this.isVisible;
			}
			set
			{
				this.isVisible = value;
			}
		}

		// Token: 0x04000884 RID: 2180
		private bool optedInto;

		// Token: 0x04000885 RID: 2181
		private bool isVisible;
	}
}
