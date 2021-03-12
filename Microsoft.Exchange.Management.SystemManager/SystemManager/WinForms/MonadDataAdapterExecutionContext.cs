using System;
using System.Data;
using System.Diagnostics;
using System.Management.Automation.Runspaces;
using System.Threading;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000E3 RID: 227
	internal class MonadDataAdapterExecutionContext : DataAdapterExecutionContext
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0001CA69 File Offset: 0x0001AC69
		public MonadConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001CA74 File Offset: 0x0001AC74
		public override void Open(IUIService service, WorkUnitCollection workUnits, bool enforceViewEntireForest, ResultsLoaderProfile profile)
		{
			this.isResultPane = !enforceViewEntireForest;
			this.workUnits = workUnits;
			this.commandInteractionHandler = ((service != null) ? new WinFormsCommandInteractionHandler(service) : new CommandInteractionHandler());
			RunspaceServerSettingsPresentationObject runspaceServerSettingsPresentationObject = ADServerSettingsSingleton.GetInstance().CreateRunspaceServerSettingsObject();
			if (enforceViewEntireForest && runspaceServerSettingsPresentationObject != null)
			{
				runspaceServerSettingsPresentationObject.ViewEntireForest = true;
			}
			this.connection = new MonadConnection(PSConnectionInfoSingleton.GetInstance().GetConnectionStringForScript(), this.commandInteractionHandler, runspaceServerSettingsPresentationObject, PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo(profile.SerializationLevel));
			this.connection.Open();
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		private void BeginExecute(AbstractDataTableFiller filler, ResultsLoaderProfile profile)
		{
			MonadAdapterFiller monadAdapterFiller = filler as MonadAdapterFiller;
			if (monadAdapterFiller != null)
			{
				this.AttachCommandToMonitorWarnings(monadAdapterFiller.Command);
				monadAdapterFiller.Command.PreservedObjectProperty = profile.WholeObjectProperty;
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001CB2C File Offset: 0x0001AD2C
		public override void Execute(AbstractDataTableFiller filler, DataTable table, ResultsLoaderProfile profile)
		{
			MonadAdapterFiller monadAdapterFiller = filler as MonadAdapterFiller;
			if (monadAdapterFiller != null && !monadAdapterFiller.HasPermission() && (this.isResultPane || monadAdapterFiller.IsResolving))
			{
				return;
			}
			Stopwatch stopwatch = new Stopwatch();
			this.BeginExecute(filler, profile);
			MonadCommand monadCommand = null;
			if (monadAdapterFiller != null)
			{
				monadCommand = monadAdapterFiller.Command;
			}
			else if (filler is SupervisionListFiller)
			{
				monadCommand = ((SupervisionListFiller)filler).Command;
			}
			if (monadCommand != null)
			{
				monadCommand.Connection = this.connection;
			}
			try
			{
				if (monadAdapterFiller != null)
				{
					ExTraceGlobals.DataFlowTracer.TracePerformance<string, Guid, RunspaceState>((long)Thread.CurrentThread.ManagedThreadId, "MonadScriptExecutionContext.Execute: In runspace {1}[State:{2}], before executing command '{0}' .", monadAdapterFiller.Command.CommandText, monadAdapterFiller.Command.Connection.RunspaceProxy.InstanceId, monadAdapterFiller.Command.Connection.RunspaceProxy.State);
					stopwatch.Start();
				}
				filler.Fill(table);
			}
			catch (MonadDataAdapterInvocationException ex)
			{
				if (!(ex.InnerException is ManagementObjectNotFoundException) && !(ex.InnerException is MapiObjectNotFoundException))
				{
					throw;
				}
			}
			finally
			{
				if (monadAdapterFiller != null)
				{
					stopwatch.Stop();
					ExTraceGlobals.DataFlowTracer.TracePerformance((long)Thread.CurrentThread.ManagedThreadId, "MonadScriptExecutionContext.Execute: In the runspace {1}[State:{2}], {3} Milliseconds are taken to finish the command {0}.", new object[]
					{
						monadAdapterFiller.Command.CommandText,
						monadAdapterFiller.Command.Connection.RunspaceProxy.InstanceId,
						monadAdapterFiller.Command.Connection.RunspaceProxy.State,
						stopwatch.ElapsedMilliseconds
					});
				}
				this.EndExecute(filler, profile);
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001CCD0 File Offset: 0x0001AED0
		private void EndExecute(AbstractDataTableFiller filler, ResultsLoaderProfile profile)
		{
			MonadAdapterFiller monadAdapterFiller = filler as MonadAdapterFiller;
			if (monadAdapterFiller != null)
			{
				this.DetachCommandFromMonitorWarnings(monadAdapterFiller.Command);
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001CCF3 File Offset: 0x0001AEF3
		public override void Close()
		{
			if (this.connection != null)
			{
				this.connection.Close();
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001CD08 File Offset: 0x0001AF08
		private void AttachCommandToMonitorWarnings(MonadCommand command)
		{
			lock (this.workUnits)
			{
				WorkUnit workUnit;
				if (!this.TryGetWorkUnit(command.CommandText, out workUnit))
				{
					workUnit = new WorkUnit();
					workUnit.Target = command;
					workUnit.Text = command.CommandText;
					this.workUnits.Add(workUnit);
					command.WarningReport += this.command_WarningReport;
				}
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001CD8C File Offset: 0x0001AF8C
		private void DetachCommandFromMonitorWarnings(MonadCommand command)
		{
			lock (this.workUnits)
			{
				WorkUnit workUnit;
				if (this.TryGetWorkUnit(command.CommandText, out workUnit))
				{
					workUnit.Status = WorkUnitStatus.Completed;
					command.WarningReport -= this.command_WarningReport;
				}
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001CDF0 File Offset: 0x0001AFF0
		private bool TryGetWorkUnit(string text, out WorkUnit workUnit)
		{
			workUnit = null;
			for (int i = 0; i < this.workUnits.Count; i++)
			{
				if (this.workUnits[i].Text == text)
				{
					workUnit = this.workUnits[i];
					break;
				}
			}
			return null != workUnit;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001CE48 File Offset: 0x0001B048
		private void command_WarningReport(object sender, WarningReportEventArgs e)
		{
			lock (this.workUnits)
			{
				WorkUnit workUnit;
				if (this.TryGetWorkUnit(e.Command.CommandText, out workUnit) && workUnit.Target == e.Command)
				{
					workUnit.Warnings.Add(e.WarningMessage);
				}
			}
		}

		// Token: 0x040003E7 RID: 999
		private CommandInteractionHandler commandInteractionHandler;

		// Token: 0x040003E8 RID: 1000
		private MonadConnection connection;

		// Token: 0x040003E9 RID: 1001
		private WorkUnitCollection workUnits;

		// Token: 0x040003EA RID: 1002
		private bool isResultPane;
	}
}
