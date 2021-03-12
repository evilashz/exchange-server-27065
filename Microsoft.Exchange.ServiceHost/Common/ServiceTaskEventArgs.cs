using System;

namespace Microsoft.Exchange.ServiceHost.Common
{
	// Token: 0x02000014 RID: 20
	internal class ServiceTaskEventArgs : EventArgs
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003D48 File Offset: 0x00001F48
		public ServiceTaskEventArgs(ServiceTaskBase task) : this(task, null)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003D52 File Offset: 0x00001F52
		public ServiceTaskEventArgs(ServiceTaskBase task, Exception error)
		{
			this.Task = task;
			this.Error = error;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003D68 File Offset: 0x00001F68
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003D70 File Offset: 0x00001F70
		public ServiceTaskBase Task { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003D79 File Offset: 0x00001F79
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003D81 File Offset: 0x00001F81
		public Exception Error { get; private set; }
	}
}
