using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200006F RID: 111
	internal class PropertyBag : Collection<Property>
	{
		// Token: 0x06000321 RID: 801 RVA: 0x000098DB File Offset: 0x00007ADB
		public PropertyBag()
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000098E3 File Offset: 0x00007AE3
		public PropertyBag(IList<Property> list) : base(list)
		{
		}
	}
}
