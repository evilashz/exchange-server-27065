using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Monad;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001D3 RID: 467
	public sealed class MonadPipelineProxy : PowerShellProxy, IDisposable
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060010B3 RID: 4275 RVA: 0x00033318 File Offset: 0x00031518
		// (remove) Token: 0x060010B4 RID: 4276 RVA: 0x00033350 File Offset: 0x00031550
		internal event EventHandler<ProgressReportEventArgs> ProgressReport;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060010B5 RID: 4277 RVA: 0x00033388 File Offset: 0x00031588
		// (remove) Token: 0x060010B6 RID: 4278 RVA: 0x000333C0 File Offset: 0x000315C0
		internal event EventHandler<ErrorReportEventArgs> ErrorReport;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060010B7 RID: 4279 RVA: 0x000333F8 File Offset: 0x000315F8
		// (remove) Token: 0x060010B8 RID: 4280 RVA: 0x00033430 File Offset: 0x00031630
		internal event EventHandler<WarningReportEventArgs> WarningReport;

		// Token: 0x060010B9 RID: 4281 RVA: 0x00033465 File Offset: 0x00031665
		public MonadPipelineProxy(RunspaceProxy proxy, PSCommand commands) : base(proxy, commands)
		{
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0003346F File Offset: 0x0003166F
		public MonadPipelineProxy(RunspaceProxy proxy, IEnumerable input, PSCommand commands) : this(proxy, commands)
		{
			this.InitInput(input);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00033480 File Offset: 0x00031680
		public MonadPipelineProxy(RunspaceProxy proxy, IEnumerable input, PSCommand commands, WorkUnit[] workUnits) : base(proxy, workUnits, commands)
		{
			this.InitInput(input);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00033494 File Offset: 0x00031694
		private void InitInput(IEnumerable input)
		{
			if (input == null)
			{
				return;
			}
			if (input.GetType().IsAssignableFrom(typeof(PSDataCollection<object>)))
			{
				this.input = (PSDataCollection<object>)input;
				if (this.input.IsOpen)
				{
					this.input.Complete();
					return;
				}
			}
			else
			{
				this.input = new PSDataCollection<object>();
				foreach (object item in input)
				{
					this.input.Add(item);
				}
				this.input.Complete();
				using (input as IDisposable)
				{
				}
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00033560 File Offset: 0x00031760
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x00033568 File Offset: 0x00031768
		internal CommandInteractionHandler InteractionHandler
		{
			get
			{
				return this.commandInteractionHandler;
			}
			set
			{
				this.commandInteractionHandler = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00033571 File Offset: 0x00031771
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00033579 File Offset: 0x00031779
		internal MonadCommand Command
		{
			get
			{
				return this.command;
			}
			set
			{
				this.command = value;
				if (this.command != null)
				{
					MonadHost.InitializeMonadHostConnection(base.GetRunspaceHost(), this.Command.Connection);
				}
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x000335A0 File Offset: 0x000317A0
		internal IEnumerable Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x000335A8 File Offset: 0x000317A8
		protected override bool RegisterHostListener
		{
			get
			{
				return base.RegisterHostListener || this.commandInteractionHandler != null;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x000335C0 File Offset: 0x000317C0
		public ErrorRecord LastUnhandledError
		{
			get
			{
				return this.lastUnhandledError;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000335C8 File Offset: 0x000317C8
		public void Dispose()
		{
			if (this.input != null)
			{
				this.input.Dispose();
				this.input = null;
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x000335E4 File Offset: 0x000317E4
		protected override IAsyncResult InternalBeginInvoke(AsyncCallback asyncCallback, object asyncState)
		{
			PSDataCollection<PSObject> output = new PSDataCollection<PSObject>();
			IAsyncResult psAsyncResult = base.PowerShell.BeginInvoke<object, PSObject>(this.input, output);
			return new MonadAsyncResult(this.Command, asyncCallback, asyncState, psAsyncResult, output);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0003361C File Offset: 0x0003181C
		protected override Collection<PSObject> InternalEndInvoke(IAsyncResult results)
		{
			MonadAsyncResult monadAsyncResult = results as MonadAsyncResult;
			if (monadAsyncResult == null)
			{
				throw new ArgumentException("results");
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadCommand.EndExecute()");
			if (monadAsyncResult.RunningCommand != this.Command)
			{
				throw new ArgumentException("Parameter does not correspond to this command.", "asyncResult");
			}
			Collection<PSObject> result = null;
			ErrorRecord errorRecord = null;
			try
			{
				result = this.ClosePipeline(monadAsyncResult);
				if (this.pipelineStateAtClose == PSInvocationState.Stopped)
				{
					errorRecord = new ErrorRecord(new PipelineStoppedException(), string.Empty, ErrorCategory.OperationStopped, null);
				}
			}
			catch (CmdletInvocationException ex)
			{
				errorRecord = ex.ErrorRecord;
				throw;
			}
			catch (CommandExecutionException ex2)
			{
				errorRecord = new ErrorRecord(ex2.InnerException, string.Empty, ErrorCategory.InvalidOperation, null);
				throw;
			}
			catch (Exception exception)
			{
				errorRecord = new ErrorRecord(exception, string.Empty, ErrorCategory.InvalidOperation, null);
				throw;
			}
			finally
			{
				if (base.WorkUnits != null)
				{
					foreach (WorkUnit workUnit in base.WorkUnits)
					{
						workUnit.ExecutedCommandText = this.Command.ToString();
						if (workUnit.CurrentStatus == 1)
						{
							if (errorRecord != null)
							{
								workUnit.Errors.Add(errorRecord);
								workUnit.CurrentStatus = 3;
							}
							else if (workUnit.Errors.Count > 0)
							{
								workUnit.CurrentStatus = 3;
							}
							else
							{
								workUnit.CurrentStatus = 2;
							}
						}
						if (workUnit.CurrentStatus == null && workUnit.Errors.Count > 0)
						{
							workUnit.CurrentStatus = 3;
						}
					}
				}
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.EndExecute()");
			return result;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000337D4 File Offset: 0x000319D4
		private Collection<PSObject> ClosePipeline(MonadAsyncResult asyncResult)
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadCommand.ClosePipeline()");
			if (base.PowerShell == null)
			{
				throw new InvalidOperationException("The command is not currently executing.");
			}
			Exception ex = null;
			Collection<PSObject> result = new Collection<PSObject>();
			ExTraceGlobals.VerboseTracer.Information((long)this.GetHashCode(), "\tWaiting for the pipeline to finish.");
			try
			{
				base.PowerShell.EndInvoke(asyncResult.PowerShellIAsyncResult);
			}
			catch (Exception)
			{
				ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "\tPipeline End Invoke Fired an Exception.");
				if (base.PowerShell.InvocationStateInfo.Reason == null)
				{
					throw;
				}
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "\tPipeline finished.");
			if (base.PowerShell.InvocationStateInfo.State == PSInvocationState.Completed && base.PowerShell.Streams.Error.Count == 0)
			{
				result = asyncResult.Output.ReadAll();
			}
			else if (base.PowerShell.InvocationStateInfo.State == PSInvocationState.Stopped || base.PowerShell.InvocationStateInfo.State == PSInvocationState.Failed || base.PowerShell.Streams.Error.Count > 0)
			{
				ex = MonadCommand.DeserializeException(base.PowerShell.InvocationStateInfo.Reason);
				if (ex != null && (this.IsHandledException(ex) || base.PowerShell.InvocationStateInfo.State == PSInvocationState.Stopped))
				{
					ThrowTerminatingErrorException ex2 = ex as ThrowTerminatingErrorException;
					ErrorRecord errorRecord;
					if (ex2 != null)
					{
						errorRecord = ex2.ErrorRecord;
					}
					else
					{
						errorRecord = new ErrorRecord(ex, LocalizedException.GenerateErrorCode(ex).ToString("X"), ErrorCategory.InvalidOperation, null);
					}
					if (base.WorkUnits != null)
					{
						for (int i = 0; i < base.WorkUnits.Length; i++)
						{
							if (base.WorkUnits[i].CurrentStatus == 2)
							{
								if (base.PowerShell.InvocationStateInfo.State != PSInvocationState.Stopped)
								{
									this.lastUnhandledError = errorRecord;
									break;
								}
							}
							else
							{
								base.ReportError(errorRecord, i);
								base.WorkUnits[i].CurrentStatus = 3;
							}
						}
					}
					else
					{
						base.ReportError(errorRecord, -1);
					}
					ex = null;
				}
				if (ex == null)
				{
					result = asyncResult.Output.ReadAll();
					base.DrainErrorStream(-1);
				}
				asyncResult.Output.Complete();
				base.PowerShell.Streams.Error.Complete();
			}
			this.pipelineStateAtClose = base.PowerShell.InvocationStateInfo.State;
			if (ex != null && !(ex is PipelineStoppedException))
			{
				ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), ex.ToString());
				if (!(ex is CmdletInvocationException))
				{
					int innerErrorCode = LocalizedException.GenerateErrorCode(ex);
					ex = new CommandExecutionException(innerErrorCode, this.Command.ToString(), ex);
				}
				this.InteractionHandler.ReportException(ex);
				throw ex;
			}
			if (this.LastUnhandledError != null)
			{
				throw new MonadDataAdapterInvocationException(this.LastUnhandledError, this.Command.ToString());
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadCommand.ClosePipeline()");
			return result;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00033AC0 File Offset: 0x00031CC0
		private bool IsHandledException(Exception e)
		{
			if (this.Command != null && this.Command.Connection.IsRemote)
			{
				return e is ThrowTerminatingErrorException || typeof(LocalizedException).IsInstanceOfType(e);
			}
			return e is ThrowTerminatingErrorException;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00033B00 File Offset: 0x00031D00
		protected override Collection<ErrorRecord> RetrieveCurrentErrors()
		{
			if (this.Command != null && this.Command.Connection.IsRemote)
			{
				Collection<ErrorRecord> collection = new Collection<ErrorRecord>();
				foreach (ErrorRecord errorRecord in base.PowerShell.Streams.Error.ReadAll())
				{
					collection.Add(this.ResolveErrorRecord(errorRecord));
				}
				return collection;
			}
			return base.RetrieveCurrentErrors();
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00033B8C File Offset: 0x00031D8C
		private ErrorRecord ResolveErrorRecord(ErrorRecord errorRecord)
		{
			RemoteException ex = errorRecord.Exception as RemoteException;
			if (ex != null)
			{
				return new ErrorRecord(MonadCommand.DeserializeException(ex), errorRecord.FullyQualifiedErrorId, errorRecord.CategoryInfo.Category, errorRecord.TargetObject);
			}
			return errorRecord;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00033BCC File Offset: 0x00031DCC
		protected override void OnWorkUnitReportProgress(ProgressRecord progressRecord)
		{
			ProgressReportEventArgs e = new ProgressReportEventArgs(progressRecord, this.Command);
			EventHandler<ProgressReportEventArgs> progressReport = this.ProgressReport;
			if (progressReport != null)
			{
				progressReport(this, e);
			}
			this.InteractionHandler.ReportProgress(e);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00033C04 File Offset: 0x00031E04
		protected override void OnWorkUnitReportWarning(string warning, int currentIndex)
		{
			WarningReportEventArgs warningReportEventArgs = new WarningReportEventArgs(this.Command.CommandGuid, warning, currentIndex, this.Command);
			EventHandler<WarningReportEventArgs> warningReport = this.WarningReport;
			this.InteractionHandler.ReportWarning(warningReportEventArgs);
			if (warningReport != null)
			{
				warningReport(this, warningReportEventArgs);
				return;
			}
			warningReportEventArgs.Dispose();
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00033C54 File Offset: 0x00031E54
		protected override void OnWorkUnitReportError(ErrorRecord error)
		{
			ErrorReportEventArgs errorReportEventArgs;
			if (base.WorkUnits == null)
			{
				errorReportEventArgs = new ErrorReportEventArgs(this.Command.CommandGuid, error, base.CurrentWorkUnit, this.Command);
			}
			else
			{
				errorReportEventArgs = new ErrorReportEventArgs(this.Command.CommandGuid, error, base.CurrentWorkUnit, this.Command);
				errorReportEventArgs.Handled = true;
			}
			EventHandler<ErrorReportEventArgs> errorReport = this.ErrorReport;
			if (errorReport != null)
			{
				errorReport(this, errorReportEventArgs);
			}
			this.InteractionHandler.ReportErrors(errorReportEventArgs);
			if (!errorReportEventArgs.Handled)
			{
				this.lastUnhandledError = error;
			}
		}

		// Token: 0x040003A5 RID: 933
		private CommandInteractionHandler commandInteractionHandler;

		// Token: 0x040003A6 RID: 934
		private MonadCommand command;

		// Token: 0x040003A7 RID: 935
		private PSInvocationState pipelineStateAtClose;

		// Token: 0x040003A8 RID: 936
		private PSDataCollection<object> input;

		// Token: 0x040003A9 RID: 937
		private ErrorRecord lastUnhandledError;
	}
}
