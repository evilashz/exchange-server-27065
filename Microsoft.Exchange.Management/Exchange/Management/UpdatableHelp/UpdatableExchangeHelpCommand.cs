using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02000BF4 RID: 3060
	[Cmdlet("Update", "ExchangeHelp")]
	public sealed class UpdatableExchangeHelpCommand : Task
	{
		// Token: 0x17002383 RID: 9091
		// (get) Token: 0x0600736B RID: 29547 RVA: 0x001D58A7 File Offset: 0x001D3AA7
		// (set) Token: 0x0600736C RID: 29548 RVA: 0x001D58AF File Offset: 0x001D3AAF
		[Parameter]
		public SwitchParameter Force { get; set; }

		// Token: 0x0600736D RID: 29549 RVA: 0x001D58B8 File Offset: 0x001D3AB8
		internal void HandleProgressChanged(object sender, UpdatableExchangeHelpProgressEventArgs e)
		{
			base.WriteProgress(e.Activity, e.ProgressStatus, e.PercentCompleted);
		}

		// Token: 0x0600736E RID: 29550 RVA: 0x001D58D4 File Offset: 0x001D3AD4
		internal UpdatableExchangeHelpCommand()
		{
			Random random = new Random();
			this.activityId = random.Next();
			this.helpUpdater = new HelpUpdater(this);
		}

		// Token: 0x17002384 RID: 9092
		// (get) Token: 0x0600736F RID: 29551 RVA: 0x001D5905 File Offset: 0x001D3B05
		// (set) Token: 0x06007370 RID: 29552 RVA: 0x001D590D File Offset: 0x001D3B0D
		internal bool Abort { get; private set; }

		// Token: 0x06007371 RID: 29553 RVA: 0x001D5916 File Offset: 0x001D3B16
		protected override void InternalStopProcessing()
		{
			TaskLogger.LogEnter();
			this.Abort = true;
			TaskLogger.LogExit();
		}

		// Token: 0x06007372 RID: 29554 RVA: 0x001D592C File Offset: 0x001D3B2C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			UpdatableExchangeHelpSystemException ex = null;
			try
			{
				this.helpUpdater.LoadConfiguration();
			}
			catch (Exception ex2)
			{
				if (ex2.GetType() == typeof(UpdatableExchangeHelpSystemException))
				{
					ex = (UpdatableExchangeHelpSystemException)ex2;
				}
				else
				{
					ex = new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateGeneralExceptionErrorID, UpdatableHelpStrings.UpdateGeneralException, ErrorCategory.InvalidOperation, null, ex2);
				}
			}
			if (ex != null)
			{
				this.WriteError(ex, ex.ErrorCategory, ex.TargetObject, false);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007373 RID: 29555 RVA: 0x001D59B0 File Offset: 0x001D3BB0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			UpdatableExchangeHelpSystemException ex = null;
			try
			{
				ex = this.helpUpdater.UpdateHelp();
			}
			catch (Exception ex2)
			{
				if (ex2.GetType() == typeof(UpdatableExchangeHelpSystemException))
				{
					ex = (UpdatableExchangeHelpSystemException)ex2;
				}
				else
				{
					ex = new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateGeneralExceptionErrorID, UpdatableHelpStrings.UpdateGeneralException, ErrorCategory.InvalidOperation, null, ex2);
				}
			}
			if (ex != null)
			{
				this.WriteError(ex, ex.ErrorCategory, ex.TargetObject, false);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003A95 RID: 14997
		internal int activityId;

		// Token: 0x04003A96 RID: 14998
		private HelpUpdater helpUpdater;
	}
}
