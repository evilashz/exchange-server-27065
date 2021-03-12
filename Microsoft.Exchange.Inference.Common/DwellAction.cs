using System;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000023 RID: 35
	public enum DwellAction
	{
		// Token: 0x04000099 RID: 153
		MarkedAsClutter,
		// Token: 0x0400009A RID: 154
		MarkedAsNotClutter,
		// Token: 0x0400009B RID: 155
		DweltOn,
		// Token: 0x0400009C RID: 156
		MarkedAsRead,
		// Token: 0x0400009D RID: 157
		MarkedAsUnread,
		// Token: 0x0400009E RID: 158
		DweltOnInClutter,
		// Token: 0x0400009F RID: 159
		MarkedAsReadInClutter,
		// Token: 0x040000A0 RID: 160
		RepliedTo,
		// Token: 0x040000A1 RID: 161
		Forwarded,
		// Token: 0x040000A2 RID: 162
		Flagged,
		// Token: 0x040000A3 RID: 163
		Deleted,
		// Token: 0x040000A4 RID: 164
		MoveFromInbox,
		// Token: 0x040000A5 RID: 165
		MoveToInbox,
		// Token: 0x040000A6 RID: 166
		MoveFromClutter,
		// Token: 0x040000A7 RID: 167
		MoveToClutter,
		// Token: 0x040000A8 RID: 168
		DeleteFromInbox,
		// Token: 0x040000A9 RID: 169
		DeleteFromClutter
	}
}
