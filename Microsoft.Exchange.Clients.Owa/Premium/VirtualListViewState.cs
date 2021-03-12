using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000496 RID: 1174
	public abstract class VirtualListViewState
	{
		// Token: 0x04001DF3 RID: 7667
		public const string SourceContainerIdName = "sId";

		// Token: 0x04001DF4 RID: 7668
		public const string MultiLineName = "mL";

		// Token: 0x04001DF5 RID: 7669
		public const string SortedColumnName = "sC";

		// Token: 0x04001DF6 RID: 7670
		public const string SortOrderName = "sO";

		// Token: 0x04001DF7 RID: 7671
		[OwaEventField("sId")]
		public ObjectId SourceContainerId;

		// Token: 0x04001DF8 RID: 7672
		[OwaEventField("mL")]
		public bool IsMultiLine;

		// Token: 0x04001DF9 RID: 7673
		[OwaEventField("sC")]
		public ColumnId SortedColumn;

		// Token: 0x04001DFA RID: 7674
		[OwaEventField("sO")]
		public SortOrder SortOrder;
	}
}
