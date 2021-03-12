using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000069 RID: 105
	public class TaskErrorEventArg
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x0000F245 File Offset: 0x0000D445
		public TaskErrorEventArg(Exception exception, bool? isUnknownException)
		{
			this.Exception = exception;
			this.IsUnknownException = isUnknownException;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000F25B File Offset: 0x0000D45B
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000F263 File Offset: 0x0000D463
		public Exception Exception { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000F26C File Offset: 0x0000D46C
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000F274 File Offset: 0x0000D474
		public bool ExceptionHandled { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000F27D File Offset: 0x0000D47D
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000F285 File Offset: 0x0000D485
		public bool? IsUnknownException { get; set; }
	}
}
