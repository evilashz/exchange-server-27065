using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.CssConverter
{
	// Token: 0x020000C0 RID: 192
	internal class CssFragment
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x0001C53F File Offset: 0x0001A73F
		public CssFragment(IList<string> mediaDevices, string cssText)
		{
			this.MediaDevices = mediaDevices;
			this.CssText = cssText;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0001C555 File Offset: 0x0001A755
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0001C55D File Offset: 0x0001A75D
		public IList<string> MediaDevices { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001C566 File Offset: 0x0001A766
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0001C56E File Offset: 0x0001A76E
		public string CssText { get; private set; }
	}
}
