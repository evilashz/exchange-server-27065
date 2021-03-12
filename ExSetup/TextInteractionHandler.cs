using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.ExSetup
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TextInteractionHandler : SetupInteractionHandler
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002F64 File Offset: 0x00001164
		public override void ReportProgress(ProgressReportEventArgs e)
		{
			base.ReportProgress(e);
			if (e.ProgressRecord.RecordType == ProgressRecordType.Processing)
			{
				if (base.IsNewActivity)
				{
					string activity = e.ProgressRecord.Activity;
					Console.Write(activity);
					return;
				}
			}
			else
			{
				string arg = this.hasErrors ? Strings.ExecutionFailed : Strings.ExecutionCompleted;
				Console.WriteLine(" ... {0}", arg);
				this.hasErrors = false;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002FCC File Offset: 0x000011CC
		public override void ReportErrors(ErrorReportEventArgs e)
		{
			base.ReportErrors(e);
			this.hasErrors = true;
			Console.Error.WriteLine(e.ErrorRecord);
			e.Handled = true;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002FF3 File Offset: 0x000011F3
		public override void ReportException(Exception e)
		{
			base.ReportException(e);
			this.hasErrors = true;
			Console.Error.WriteLine(e.InnerException.Message);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003018 File Offset: 0x00001218
		public override void ReportWarning(WarningReportEventArgs e)
		{
			base.ReportWarning(e);
			Console.WriteLine(e.WarningMessage);
		}

		// Token: 0x04000011 RID: 17
		private bool hasErrors;
	}
}
