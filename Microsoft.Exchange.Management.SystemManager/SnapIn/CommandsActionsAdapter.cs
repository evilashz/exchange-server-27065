using System;
using System.Collections.Generic;
using Microsoft.ManagementConsole;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000273 RID: 627
	public class CommandsActionsAdapter : IDisposable
	{
		// Token: 0x06001ACF RID: 6863 RVA: 0x00076327 File Offset: 0x00074527
		public CommandsActionsAdapter(IServiceProvider serviceProvider, ActionsPaneItemCollection actions, CommandCollection commands, bool showGroupsAsRegions, IExchangeSnapIn snapIn, ExchangeFormView view) : this(serviceProvider, actions, commands, showGroupsAsRegions, snapIn, view, false)
		{
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0007633C File Offset: 0x0007453C
		public CommandsActionsAdapter(IServiceProvider serviceProvider, ActionsPaneItemCollection actions, CommandCollection commands, bool showGroupsAsRegions, IExchangeSnapIn snapIn, ExchangeFormView view, bool createActionsAtBottom)
		{
			this.snapIn = snapIn;
			this.serviceProvider = serviceProvider;
			this.actions = actions;
			this.commands = commands;
			this.adapters = new List<CommandActionAdapter>(commands.Count);
			this.showGroupsAsRegions = showGroupsAsRegions;
			this.createActionsAtBottom = createActionsAtBottom;
			this.view = view;
			this.commands.CommandAdded += new CommandEventHandler(this.commands_CommandAdded);
			this.commands.CommandRemoved += new CommandEventHandler(this.commands_CommandRemoved);
			ActionsPaneItemCollection actionsPaneItemCollection = new ActionsPaneItemCollection();
			for (int i = 0; i < this.commands.Count; i++)
			{
				CommandActionAdapter commandActionAdapter = this.CreateAdapter(this.commands[i]);
				this.adapters.Add(commandActionAdapter);
				if (commandActionAdapter.Command.Visible)
				{
					actionsPaneItemCollection.Add(commandActionAdapter.ActionItem);
				}
			}
			this.actions.AddRange(actionsPaneItemCollection.ToArray());
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0007642C File Offset: 0x0007462C
		public void Dispose()
		{
			this.actions.Clear();
			for (int i = 0; i < this.adapters.Count; i++)
			{
				this.DisposeAdapter(this.adapters[i]);
			}
			this.commands.CommandAdded -= new CommandEventHandler(this.commands_CommandAdded);
			this.commands.CommandRemoved -= new CommandEventHandler(this.commands_CommandRemoved);
			this.commands = null;
			this.actions = null;
			this.snapIn = null;
			this.view = null;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000764B8 File Offset: 0x000746B8
		private CommandActionAdapter CreateAdapter(Command command)
		{
			command.EnabledChanged += this.DelayUpdates;
			command.VisibleChanged += this.Command_VisibleChanged;
			return new CommandActionAdapter(this.serviceProvider, command, this.showGroupsAsRegions, this.snapIn, this.view);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00076507 File Offset: 0x00074707
		private void DisposeAdapter(CommandActionAdapter adapter)
		{
			adapter.Command.EnabledChanged -= this.DelayUpdates;
			adapter.Command.VisibleChanged -= this.Command_VisibleChanged;
			adapter.Dispose();
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00076540 File Offset: 0x00074740
		private void commands_CommandAdded(object sender, CommandEventArgs e)
		{
			this.DelayUpdates(sender, e);
			this.adapters.Insert(this.commands.IndexOf(e.Command), this.CreateAdapter(e.Command));
			if (e.Command.Visible)
			{
				this.Command_VisibleChanged(e.Command, EventArgs.Empty);
			}
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0007659C File Offset: 0x0007479C
		private void commands_CommandRemoved(object sender, CommandEventArgs e)
		{
			this.DelayUpdates(sender, e);
			foreach (CommandActionAdapter commandActionAdapter in this.adapters)
			{
				if (commandActionAdapter.Command == e.Command)
				{
					if (this.actions.Contains(commandActionAdapter.ActionItem))
					{
						this.actions.Remove(commandActionAdapter.ActionItem);
					}
					this.DisposeAdapter(commandActionAdapter);
					this.adapters.Remove(commandActionAdapter);
					break;
				}
			}
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00076638 File Offset: 0x00074838
		private void Command_VisibleChanged(object sender, EventArgs e)
		{
			this.DelayUpdates(sender, e);
			Command command = (Command)sender;
			int num = this.commands.IndexOf(command);
			if (command.Visible)
			{
				this.actions.Insert(this.GetActionInsertPosition(num), this.adapters[num].ActionItem);
				return;
			}
			this.actions.Remove(this.adapters[num].ActionItem);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000766AC File Offset: 0x000748AC
		private int GetActionInsertPosition(int commandIndex)
		{
			int num = this.createActionsAtBottom ? 1 : -1;
			int num2 = commandIndex + num;
			while (num2 >= 0 && num2 < this.commands.Count)
			{
				if (this.commands[num2].Visible && this.actions.Contains(this.adapters[num2].ActionItem))
				{
					int num3 = this.actions.IndexOf(this.adapters[num2].ActionItem);
					if (!this.createActionsAtBottom)
					{
						num3++;
					}
					return num3;
				}
				num2 += num;
			}
			if (!this.createActionsAtBottom)
			{
				return 0;
			}
			return this.actions.Count;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00076753 File Offset: 0x00074953
		private void DelayUpdates(object sender, EventArgs e)
		{
			if (this.view != null)
			{
				this.view.DelayUpdates();
			}
		}

		// Token: 0x040009F8 RID: 2552
		private IExchangeSnapIn snapIn;

		// Token: 0x040009F9 RID: 2553
		private IServiceProvider serviceProvider;

		// Token: 0x040009FA RID: 2554
		private ActionsPaneItemCollection actions;

		// Token: 0x040009FB RID: 2555
		private CommandCollection commands;

		// Token: 0x040009FC RID: 2556
		private List<CommandActionAdapter> adapters;

		// Token: 0x040009FD RID: 2557
		private ExchangeFormView view;

		// Token: 0x040009FE RID: 2558
		private bool createActionsAtBottom;

		// Token: 0x040009FF RID: 2559
		private bool showGroupsAsRegions;
	}
}
