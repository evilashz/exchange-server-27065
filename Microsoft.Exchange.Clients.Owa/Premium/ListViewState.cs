using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004BE RID: 1214
	public abstract class ListViewState
	{
		// Token: 0x04001FFA RID: 8186
		public const string SourceContainerIdName = "sId";

		// Token: 0x04001FFB RID: 8187
		public const string SortedColumnIdName = "sC";

		// Token: 0x04001FFC RID: 8188
		public const string SortDirectionName = "sO";

		// Token: 0x04001FFD RID: 8189
		public const string MultiLineName = "mL";

		// Token: 0x04001FFE RID: 8190
		public const string StartRangeName = "sR";

		// Token: 0x04001FFF RID: 8191
		public const string EndRangeName = "eR";

		// Token: 0x04002000 RID: 8192
		public const string TotalCountName = "tC";

		// Token: 0x04002001 RID: 8193
		[OwaEventField("sId")]
		public ObjectId SourceContainerId;

		// Token: 0x04002002 RID: 8194
		[OwaEventField("sC")]
		public ColumnId SortedColumnId;

		// Token: 0x04002003 RID: 8195
		[OwaEventField("sO")]
		public SortOrder SortDirection;

		// Token: 0x04002004 RID: 8196
		[OwaEventField("mL")]
		public bool IsMultiLine;

		// Token: 0x04002005 RID: 8197
		[OwaEventField("sR")]
		public int StartRange;

		// Token: 0x04002006 RID: 8198
		[OwaEventField("eR")]
		public int EndRange;

		// Token: 0x04002007 RID: 8199
		[OwaEventField("tC")]
		public int TotalCount;
	}
}
