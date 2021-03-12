using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000067 RID: 103
	public interface ITaskEvent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000422 RID: 1058
		// (remove) Token: 0x06000423 RID: 1059
		event EventHandler<EventArgs> PreInit;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000424 RID: 1060
		// (remove) Token: 0x06000425 RID: 1061
		event EventHandler<EventArgs> InitCompleted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000426 RID: 1062
		// (remove) Token: 0x06000427 RID: 1063
		event EventHandler<EventArgs> PreIterate;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000428 RID: 1064
		// (remove) Token: 0x06000429 RID: 1065
		event EventHandler<EventArgs> IterateCompleted;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600042A RID: 1066
		// (remove) Token: 0x0600042B RID: 1067
		event EventHandler<EventArgs> PreRelease;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600042C RID: 1068
		// (remove) Token: 0x0600042D RID: 1069
		event EventHandler<EventArgs> Release;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600042E RID: 1070
		// (remove) Token: 0x0600042F RID: 1071
		event EventHandler<EventArgs> PreStop;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000430 RID: 1072
		// (remove) Token: 0x06000431 RID: 1073
		event EventHandler<EventArgs> Stop;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000432 RID: 1074
		// (remove) Token: 0x06000433 RID: 1075
		event EventHandler<GenericEventArg<TaskErrorEventArg>> Error;
	}
}
