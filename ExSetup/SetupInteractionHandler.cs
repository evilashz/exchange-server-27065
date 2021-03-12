using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Setup;
using Microsoft.Exchange.Setup.Common;

namespace Microsoft.Exchange.Setup.ExSetup
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SetupInteractionHandler : CommandInteractionHandler
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002883 File Offset: 0x00000A83
		protected string CurrentCommandText
		{
			get
			{
				return this.currentCommandText;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000288B File Offset: 0x00000A8B
		protected string CurrentActivityText
		{
			get
			{
				return this.currentActivityText;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002893 File Offset: 0x00000A93
		protected bool IsNewActivity
		{
			get
			{
				return this.isNewActivity;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000289B File Offset: 0x00000A9B
		public override void ReportErrors(ErrorReportEventArgs e)
		{
			if (e != null && e.ErrorRecord != null)
			{
				ExTraceGlobals.TraceTracer.TraceError(0L, e.ErrorRecord.ToString());
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000028BF File Offset: 0x00000ABF
		public override void ReportException(Exception e)
		{
			SetupLogger.LogError(e);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028C8 File Offset: 0x00000AC8
		public override void ReportProgress(ProgressReportEventArgs e)
		{
			if (e.Command.ToString() != this.currentCommandText)
			{
				this.currentCommandText = e.Command.ToString();
			}
			if (e.ProgressRecord.Activity != this.currentActivityText)
			{
				this.currentActivityText = e.ProgressRecord.Activity;
				this.isNewActivity = true;
				return;
			}
			this.isNewActivity = false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002936 File Offset: 0x00000B36
		public override void ReportVerboseOutput(string message)
		{
			base.ReportVerboseOutput(message);
		}

		// Token: 0x04000005 RID: 5
		private string currentCommandText;

		// Token: 0x04000006 RID: 6
		private string currentActivityText;

		// Token: 0x04000007 RID: 7
		private bool isNewActivity;
	}
}
