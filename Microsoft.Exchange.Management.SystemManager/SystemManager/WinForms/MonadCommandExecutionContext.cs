using System;
using System.Data;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000E4 RID: 228
	internal class MonadCommandExecutionContext : CommandExecutionContext
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x0001CEC0 File Offset: 0x0001B0C0
		public override void Open(IUIService service)
		{
			this.commandInteractionHandler = ((service != null) ? new WinFormsCommandInteractionHandler(service) : new CommandInteractionHandler());
			this.connection = this.CreateMonadConnection(service, this.commandInteractionHandler);
			this.connection.Open();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001CEF6 File Offset: 0x0001B0F6
		protected virtual MonadConnection CreateMonadConnection(IUIService uiService, CommandInteractionHandler commandInteractionHandler)
		{
			return new MonadConnection("timeout=30", commandInteractionHandler, ADServerSettingsSingleton.GetInstance().CreateRunspaceServerSettingsObject(), PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo());
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0001CF17 File Offset: 0x0001B117
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0001CF1F File Offset: 0x0001B11F
		internal bool IsPropertyPage { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0001CF28 File Offset: 0x0001B128
		public override bool ShouldReload
		{
			get
			{
				return this.IsPropertyPage;
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001CF30 File Offset: 0x0001B130
		public override void Execute(TaskProfileBase profile, DataRow row, DataObjectStore store)
		{
			ReaderTaskProfile readerTaskProfile = profile as ReaderTaskProfile;
			if (readerTaskProfile != null && !readerTaskProfile.HasPermission() && this.IsPropertyPage)
			{
				return;
			}
			Stopwatch stopwatch = new Stopwatch();
			MonadCommand monadCommand = null;
			if (profile.Runner is MonadReadTask)
			{
				monadCommand = ((MonadReadTask)profile.Runner).Command;
			}
			else if (profile.Runner is GroupReadTask)
			{
				monadCommand = ((GroupReadTask)profile.Runner).Command;
			}
			else if (profile.Runner is MonadSaveTask)
			{
				monadCommand = ((MonadSaveTask)profile.Runner).Command;
			}
			else if (profile.Runner is MonadPipelineSaveTask)
			{
				monadCommand = ((MonadPipelineSaveTask)profile.Runner).Command;
			}
			else if (!(profile.Runner is CreateDataObjectReader))
			{
				if (profile.Runner is MembersSaveTask)
				{
					(profile.Runner as MembersSaveTask).UpdateConnection(this.connection);
				}
				else if (profile.Runner is GroupSaveTask)
				{
					(profile.Runner as GroupSaveTask).UpdateConnection(this.connection);
				}
				else if (profile.Runner is ClientPermissionSaveTask)
				{
					(profile.Runner as ClientPermissionSaveTask).UpdateConnection(this.connection);
				}
			}
			if (monadCommand != null)
			{
				monadCommand.Connection = this.connection;
			}
			try
			{
				if (monadCommand != null)
				{
					ExTraceGlobals.DataFlowTracer.TracePerformance<string, Guid, RunspaceState>((long)Thread.CurrentThread.ManagedThreadId, "MonadScriptExecutionContext.Execute: In runspace {1}[State:{2}], before executing command '{0}' .", monadCommand.CommandText, monadCommand.Connection.RunspaceProxy.InstanceId, monadCommand.Connection.RunspaceProxy.State);
				}
				stopwatch.Start();
				profile.Run(null, row, store);
			}
			catch (CommandExecutionException)
			{
				if (!profile.IgnoreException)
				{
					throw;
				}
			}
			catch (CmdletInvocationException)
			{
				if (!profile.IgnoreException)
				{
					throw;
				}
			}
			catch (PipelineStoppedException)
			{
				if (!profile.IgnoreException)
				{
					throw;
				}
			}
			finally
			{
				stopwatch.Stop();
				if (monadCommand != null)
				{
					ExTraceGlobals.DataFlowTracer.TracePerformance((long)Thread.CurrentThread.ManagedThreadId, "MonadScriptExecutionContext.Execute: In the runspace {1}[State:{2}], {3} Milliseconds are taken to finish the command {0}.", new object[]
					{
						monadCommand.CommandText,
						monadCommand.Connection.RunspaceProxy.InstanceId,
						monadCommand.Connection.RunspaceProxy.State,
						stopwatch.ElapsedMilliseconds
					});
				}
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001D198 File Offset: 0x0001B398
		public override void Close()
		{
			if (this.connection != null)
			{
				this.connection.Close();
			}
		}

		// Token: 0x040003EB RID: 1003
		protected CommandInteractionHandler commandInteractionHandler;

		// Token: 0x040003EC RID: 1004
		protected MonadConnection connection;
	}
}
