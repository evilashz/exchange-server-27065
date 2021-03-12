using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000148 RID: 328
	internal class LoggableMonadCommand : MonadCommand
	{
		// Token: 0x06000D3B RID: 3387 RVA: 0x00030952 File Offset: 0x0002EB52
		public LoggableMonadCommand() : this(null, null)
		{
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003095C File Offset: 0x0002EB5C
		public LoggableMonadCommand(string cmdText) : this(cmdText, null)
		{
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00030966 File Offset: 0x0002EB66
		public LoggableMonadCommand(string cmdText, MonadConnection connection) : base(cmdText, connection)
		{
			this.RegisterListeners();
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00030978 File Offset: 0x0002EB78
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				base.StartExecution -= CommandLoggingSession.StartExecution;
				base.EndExecution -= CommandLoggingSession.EndExecution;
				base.ErrorReport -= CommandLoggingSession.ErrorReport;
				base.WarningReport -= CommandLoggingSession.WarningReport;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000309D8 File Offset: 0x0002EBD8
		private void RegisterListeners()
		{
			base.StartExecution += CommandLoggingSession.StartExecution;
			base.EndExecution += CommandLoggingSession.EndExecution;
			base.ErrorReport += CommandLoggingSession.ErrorReport;
			base.WarningReport += CommandLoggingSession.WarningReport;
		}
	}
}
