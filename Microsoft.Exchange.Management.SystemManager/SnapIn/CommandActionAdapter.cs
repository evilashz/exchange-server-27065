using System;
using System.Threading;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementConsole;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000272 RID: 626
	public class CommandActionAdapter : IDisposable
	{
		// Token: 0x06001AC6 RID: 6854 RVA: 0x00075F70 File Offset: 0x00074170
		public CommandActionAdapter(IServiceProvider serviceProvider, Command command, bool treatCommandAsGroup, IExchangeSnapIn snapIn, ExchangeFormView view)
		{
			this.serviceProvider = serviceProvider;
			this.command = command;
			if (this.command.Name == "-")
			{
				this.actionItem = new ActionSeparator();
			}
			else
			{
				if (this.command.HasCommands || this.command.Style == 4 || treatCommandAsGroup)
				{
					ActionGroup actionGroup = new ActionGroup();
					actionGroup.RenderAsRegion = true;
					this.actionItem = actionGroup;
					this.groupItemsAdapter = new CommandsActionsAdapter(this.serviceProvider, actionGroup.Items, this.Command.Commands, false, snapIn, view);
				}
				else
				{
					Action action = new Action();
					this.actionItem = action;
					action.Triggered += new Action.ActionEventHandler(this.action_Triggered);
					this.Command.EnabledChanged += this.command_EnabledChanged;
					this.command_EnabledChanged(this.Command, EventArgs.Empty);
					this.Command.CheckedChanged += this.command_CheckedChanged;
					this.command_CheckedChanged(this.Command, EventArgs.Empty);
				}
				this.Command.TextChanged += this.command_TextChanged;
				this.command_TextChanged(this.Command, EventArgs.Empty);
				this.Command.DescriptionChanged += this.command_DescriptionChanged;
				this.command_DescriptionChanged(this.Command, EventArgs.Empty);
				if (snapIn != null)
				{
					(this.ActionItem as ActionsPaneExtendedItem).ImageIndex = snapIn.RegisterIcon(this.Command.Name, this.Command.Icon);
				}
				(this.ActionItem as ActionsPaneExtendedItem).LanguageIndependentName = this.ActionItem.GetHashCode().ToString();
			}
			this.ActionItem.Tag = this;
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x00076132 File Offset: 0x00074332
		public ActionsPaneItem ActionItem
		{
			get
			{
				return this.actionItem;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0007613A File Offset: 0x0007433A
		public Command Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00076144 File Offset: 0x00074344
		public void Dispose()
		{
			this.Command.TextChanged -= this.command_TextChanged;
			this.Command.DescriptionChanged -= this.command_DescriptionChanged;
			this.Command.EnabledChanged -= this.command_EnabledChanged;
			this.Command.CheckedChanged -= this.command_CheckedChanged;
			Action action = this.ActionItem as Action;
			if (action != null)
			{
				action.Triggered -= new Action.ActionEventHandler(this.action_Triggered);
				return;
			}
			if (this.groupItemsAdapter != null)
			{
				this.groupItemsAdapter.Dispose();
				this.groupItemsAdapter = null;
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000761EC File Offset: 0x000743EC
		private void command_EnabledChanged(object sender, EventArgs e)
		{
			Action action = this.ActionItem as Action;
			if (this.command.Enabled)
			{
				action.Enabled = true;
				return;
			}
			action.Enabled = false;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00076224 File Offset: 0x00074424
		private void command_CheckedChanged(object sender, EventArgs e)
		{
			Action action = this.ActionItem as Action;
			action.Checked = this.command.Checked;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00076250 File Offset: 0x00074450
		private void action_Triggered(object sender, ActionEventArgs e)
		{
			try
			{
				ExTraceGlobals.ProgramFlowTracer.TraceFunction<string>(0L, "*--CommandActionAdapter.action_Triggered: Invoking command {0}", this.Command.Name);
				SynchronizationContext synchronizationContext = (SynchronizationContext)this.serviceProvider.GetService(typeof(SynchronizationContext));
				SynchronizationContext.SetSynchronizationContext(synchronizationContext);
				this.Command.Invoke();
			}
			catch (Exception ex)
			{
				if (ExceptionHelper.IsUICriticalException(ex))
				{
					throw;
				}
				IUIService iuiservice = (IUIService)this.serviceProvider.GetService(typeof(IUIService));
				iuiservice.ShowError(ex);
			}
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000762E8 File Offset: 0x000744E8
		private void command_TextChanged(object sender, EventArgs e)
		{
			(this.ActionItem as ActionsPaneExtendedItem).DisplayName = this.Command.Text;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00076305 File Offset: 0x00074505
		private void command_DescriptionChanged(object sender, EventArgs e)
		{
			(this.ActionItem as ActionsPaneExtendedItem).Description = ExchangeUserControl.RemoveAccelerator(this.Command.Description);
		}

		// Token: 0x040009F4 RID: 2548
		private IServiceProvider serviceProvider;

		// Token: 0x040009F5 RID: 2549
		private ActionsPaneItem actionItem;

		// Token: 0x040009F6 RID: 2550
		private CommandsActionsAdapter groupItemsAdapter;

		// Token: 0x040009F7 RID: 2551
		private Command command;
	}
}
