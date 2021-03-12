using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics.Components.Monad;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x0200019F RID: 415
	internal class CommandInteractionHandler
	{
		// Token: 0x06000EF3 RID: 3827 RVA: 0x0002B6D6 File Offset: 0x000298D6
		public CommandInteractionHandler()
		{
			ExTraceGlobals.HostTracer.Information((long)this.GetHashCode(), "new CommandInteractionHandler()");
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0002B6F4 File Offset: 0x000298F4
		public virtual ConfirmationChoice ShowConfirmationDialog(string message, ConfirmationChoice defaultChoice)
		{
			ExTraceGlobals.HostTracer.Information<string>((long)this.GetHashCode(), "CommandInteractionHandler.ShowConfirmationDialog({0})", message);
			return defaultChoice;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0002B70E File Offset: 0x0002990E
		public virtual void ReportProgress(ProgressReportEventArgs e)
		{
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002B710 File Offset: 0x00029910
		public virtual void ReportErrors(ErrorReportEventArgs e)
		{
			ExTraceGlobals.HostTracer.Information<ErrorRecord>((long)this.GetHashCode(), "CommandInteractionHandler.ReportErrors({0})", e.ErrorRecord);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002B72E File Offset: 0x0002992E
		public virtual void ReportException(Exception e)
		{
			ExTraceGlobals.HostTracer.Information<Exception>((long)this.GetHashCode(), "CommandInteractionHandler.ReportException({0})", e);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002B747 File Offset: 0x00029947
		public virtual void ReportWarning(WarningReportEventArgs e)
		{
			ExTraceGlobals.HostTracer.Information<string>((long)this.GetHashCode(), "CommandInteractionHandler.ReportWarning({0})", e.WarningMessage);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0002B765 File Offset: 0x00029965
		public virtual void ReportVerboseOutput(string message)
		{
			ExTraceGlobals.HostTracer.Information<string>((long)this.GetHashCode(), "CommandInteractionHandler.ReportVerboseOutput({0})", message);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0002B77E File Offset: 0x0002997E
		public virtual Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0002B785 File Offset: 0x00029985
		public virtual string WrapText(string text)
		{
			return text;
		}
	}
}
