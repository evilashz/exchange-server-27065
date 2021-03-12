using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004D8 RID: 1240
	[OwaEventObjectId(typeof(ADObjectId))]
	[OwaEventNamespace("RP")]
	internal sealed class RoomPickerEventHandler : DirectoryVirtualListViewEventHandler
	{
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x0011068B File Offset: 0x0010E88B
		protected override ViewType ViewType
		{
			get
			{
				return ViewType.RoomPicker;
			}
		}

		// Token: 0x04002122 RID: 8482
		public const string EventNamespace = "RP";
	}
}
