using System;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics
{
	// Token: 0x02000017 RID: 23
	internal class ExceptionActionEventArgs : EventArgs
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00004C58 File Offset: 0x00002E58
		public ExceptionActionEventArgs(Exception exception, ExceptionAction requestedAction)
		{
			this.Exception = exception;
			this.RequestedAction = requestedAction;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004C6E File Offset: 0x00002E6E
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004C76 File Offset: 0x00002E76
		public ExceptionAction RequestedAction { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004C7F File Offset: 0x00002E7F
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004C87 File Offset: 0x00002E87
		public Exception Exception { get; private set; }
	}
}
