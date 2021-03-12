using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000022 RID: 34
	internal interface IFastDocument
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009A RID: 154
		// (set) Token: 0x0600009B RID: 155
		int AttemptCount { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009C RID: 156
		// (set) Token: 0x0600009D RID: 157
		string CompositeItemId { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009E RID: 158
		Guid CorrelationId { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009F RID: 159
		// (set) Token: 0x060000A0 RID: 160
		long DocumentId { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A1 RID: 161
		// (set) Token: 0x060000A2 RID: 162
		int ErrorCode { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A3 RID: 163
		// (set) Token: 0x060000A4 RID: 164
		string ErrorMessage { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A5 RID: 165
		// (set) Token: 0x060000A6 RID: 166
		int FeedingVersion { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A7 RID: 167
		// (set) Token: 0x060000A8 RID: 168
		string FlowOperation { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A9 RID: 169
		// (set) Token: 0x060000AA RID: 170
		string FolderId { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AB RID: 171
		// (set) Token: 0x060000AC RID: 172
		long IndexId { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AD RID: 173
		// (set) Token: 0x060000AE RID: 174
		string IndexSystemName { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AF RID: 175
		// (set) Token: 0x060000B0 RID: 176
		string InstanceName { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B1 RID: 177
		// (set) Token: 0x060000B2 RID: 178
		bool IsLocalMdb { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B3 RID: 179
		// (set) Token: 0x060000B4 RID: 180
		bool IsMoveDestination { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B5 RID: 181
		// (set) Token: 0x060000B6 RID: 182
		Guid MailboxGuid { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B7 RID: 183
		// (set) Token: 0x060000B8 RID: 184
		int Port { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B9 RID: 185
		// (set) Token: 0x060000BA RID: 186
		int MessageFlags { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000BB RID: 187
		// (set) Token: 0x060000BC RID: 188
		Guid TenantId { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BD RID: 189
		// (set) Token: 0x060000BE RID: 190
		string TransportContextId { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BF RID: 191
		// (set) Token: 0x060000C0 RID: 192
		long Watermark { get; set; }
	}
}
