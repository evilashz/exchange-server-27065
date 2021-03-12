using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200000F RID: 15
	public interface IIntegrityCheckJob
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000041 RID: 65
		Guid JobGuid { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000042 RID: 66
		Guid RequestGuid { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000043 RID: 67
		Guid MailboxGuid { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000044 RID: 68
		int MailboxNumber { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000045 RID: 69
		TaskId TaskId { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000046 RID: 70
		bool DetectOnly { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000047 RID: 71
		DateTime CreationTime { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000048 RID: 72
		JobSource Source { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73
		JobPriority Priority { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004A RID: 74
		JobState State { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004B RID: 75
		short Progress { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76
		TimeSpan TimeInServer { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004D RID: 77
		DateTime? CompletedTime { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004E RID: 78
		DateTime? LastExecutionTime { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004F RID: 79
		int CorruptionsDetected { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000050 RID: 80
		int CorruptionsFixed { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000051 RID: 81
		ErrorCode Error { get; }

		// Token: 0x06000052 RID: 82
		Properties GetProperties(StorePropTag[] propTags);
	}
}
