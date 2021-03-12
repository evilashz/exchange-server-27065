using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000041 RID: 65
	internal class LoggableMonadCommand : MonadCommand
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000AED0 File Offset: 0x000090D0
		public LoggableMonadCommand() : this(null, null)
		{
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000AEDA File Offset: 0x000090DA
		public LoggableMonadCommand(string cmdText) : this(cmdText, null)
		{
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000AEE4 File Offset: 0x000090E4
		public LoggableMonadCommand(string cmdText, MonadConnection connection) : base(cmdText, connection)
		{
			this.RegisterListeners();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000AEF4 File Offset: 0x000090F4
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

		// Token: 0x06000317 RID: 791 RVA: 0x0000AF54 File Offset: 0x00009154
		private void RegisterListeners()
		{
			base.StartExecution += CommandLoggingSession.StartExecution;
			base.EndExecution += CommandLoggingSession.EndExecution;
			base.ErrorReport += CommandLoggingSession.ErrorReport;
			base.WarningReport += CommandLoggingSession.WarningReport;
		}
	}
}
