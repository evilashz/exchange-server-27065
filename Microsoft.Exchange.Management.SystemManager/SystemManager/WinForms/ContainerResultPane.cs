using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.Services;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001C0 RID: 448
	public class ContainerResultPane : AbstractResultPane, IResultPaneSelectionService, IServiceProvider
	{
		// Token: 0x06001263 RID: 4707 RVA: 0x0004A3AC File Offset: 0x000485AC
		public ContainerResultPane()
		{
			this.RegisterCommandsEvent(base.ResultPaneCommands);
			this.RegisterCommandsEvent(base.SelectionCommands);
			this.ResultPanes.ListChanging += this.ResultPanes_ListChanging;
			this.ResultPanes.ListChanged += this.ResultPanes_ListChanged;
			this.ResultPanesActiveToContainer.ListChanging += this.ResultPanesActiveToContainer_ListChanging;
			this.ResultPanesActiveToContainer.ListChanged += this.ResultPanesActiveToContainer_ListChanged;
			ISelectionService serviceInstance = new Selection();
			ServiceContainer serviceContainer = new ServiceContainer(this);
			serviceContainer.AddService(typeof(ISelectionService), serviceInstance);
			this.components = new ServicedContainer(serviceContainer);
			base.Name = "ContainerResultPane";
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0004A48C File Offset: 0x0004868C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ResultPanesActiveToContainer.ListChanging -= this.ResultPanesActiveToContainer_ListChanging;
				this.ResultPanesActiveToContainer.ListChanged -= this.ResultPanesActiveToContainer_ListChanged;
				this.ResultPanes.ListChanging -= this.ResultPanes_ListChanging;
				this.ResultPanes.ListChanged -= this.ResultPanes_ListChanged;
				this.UnregisterCommandsEvent(base.SelectionCommands);
				this.UnregisterCommandsEvent(base.ResultPaneCommands);
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004A522 File Offset: 0x00048722
		private void RegisterCommandsEvent(CommandCollection commands)
		{
			commands.CommandAdded += new CommandEventHandler(this.Commands_CommandAdded);
			commands.CommandRemoved += new CommandEventHandler(this.Commands_CommandRemoved);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004A548 File Offset: 0x00048748
		private void UnregisterCommandsEvent(CommandCollection commands)
		{
			commands.CommandAdded -= new CommandEventHandler(this.Commands_CommandAdded);
			commands.CommandRemoved -= new CommandEventHandler(this.Commands_CommandRemoved);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0004A56E File Offset: 0x0004876E
		private void Commands_CommandAdded(object sender, CommandEventArgs e)
		{
			e.Command.Executing += this.Command_Executing;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004A587 File Offset: 0x00048787
		private void Commands_CommandRemoved(object sender, CommandEventArgs e)
		{
			e.Command.Executing -= this.Command_Executing;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0004A5A0 File Offset: 0x000487A0
		private void Command_Executing(object sender, CancelEventArgs e)
		{
			this.SelectResultPaneByCommand(sender as Command);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0004A5F0 File Offset: 0x000487F0
		private void SelectResultPaneByCommand(Command command)
		{
			Predicate<AbstractResultPane> selectionCondition = delegate(AbstractResultPane resultPane)
			{
				bool result = false;
				if (resultPane.ResultPaneCommands.Contains(command) || resultPane.SelectionCommands.Contains(command))
				{
					result = true;
				}
				return result;
			};
			this.SelectResultPane(selectionCondition);
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0004A61F File Offset: 0x0004881F
		public ChangeNotifyingCollection<AbstractResultPane> ResultPanes
		{
			get
			{
				return this.resultPanes;
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0004A628 File Offset: 0x00048828
		private void ResultPanes_ListChanging(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				this.RemovingResultPaneAt(e.NewIndex);
				return;
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				for (int i = this.ResultPanes.Count - 1; i >= 0; i--)
				{
					this.RemovingResultPaneAt(i);
				}
			}
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0004A672 File Offset: 0x00048872
		private void ResultPanes_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				this.InsertedResultPaneAt(e.NewIndex);
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004A68C File Offset: 0x0004888C
		private void InsertedResultPaneAt(int index)
		{
			AbstractResultPane abstractResultPane = this.ResultPanes[index];
			if (abstractResultPane == null)
			{
				throw new InvalidOperationException("Cannot insert null to ResultPanes.");
			}
			if (abstractResultPane.ContainerResultPane != null)
			{
				throw new InvalidOperationException("The result pane has been inserted into anonther ContanerResultPane as Contained result pane.");
			}
			if (abstractResultPane.IsActive)
			{
				throw new InvalidOperationException("The inserted result pane can not be Active.");
			}
			this.components.Add(abstractResultPane);
			abstractResultPane.ContainerResultPane = this;
			abstractResultPane.SharedSettings = base.SharedSettings;
			abstractResultPane.IsModifiedChanged += this.ResultPane_IsModifiedChanged;
			this.ResultPane_IsModifiedChanged(abstractResultPane, EventArgs.Empty);
			abstractResultPane.SetActive += this.ResultPane_SetActive;
			abstractResultPane.KillingActive += this.ResultPane_KillingActive;
			abstractResultPane.EnabledChanging += this.ResultPane_EnabledChanging;
			abstractResultPane.EnabledChanged += this.ResultPane_EnabledChanged;
			this.TryToBindEnabledResultPaneToContainer(abstractResultPane);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0004A768 File Offset: 0x00048968
		private void RemovingResultPaneAt(int index)
		{
			AbstractResultPane abstractResultPane = this.ResultPanes[index];
			if (abstractResultPane.IsActive)
			{
				abstractResultPane.OnKillActive();
			}
			else if (this.IsActiveToContainer(abstractResultPane))
			{
				this.ResultPanesActiveToContainer.Remove(abstractResultPane);
			}
			this.TryToUnbindEnabledResultPaneFromContainer(abstractResultPane);
			abstractResultPane.EnabledChanging -= this.ResultPane_EnabledChanging;
			abstractResultPane.EnabledChanged -= this.ResultPane_EnabledChanged;
			abstractResultPane.SetActive -= this.ResultPane_SetActive;
			abstractResultPane.KillingActive -= this.ResultPane_KillingActive;
			abstractResultPane.IsModifiedChanged -= this.ResultPane_IsModifiedChanged;
			this.ResultPane_IsModifiedChanged(abstractResultPane, EventArgs.Empty);
			this.components.Remove(abstractResultPane);
			abstractResultPane.ContainerResultPane = null;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0004A82C File Offset: 0x00048A2C
		private void ResultPane_IsModifiedChanged(object sender, EventArgs e)
		{
			bool flag = false;
			foreach (AbstractResultPane abstractResultPane in this.ResultPanes)
			{
				flag |= abstractResultPane.IsModified;
			}
			base.IsModified = (base.IsModified || flag);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004A88C File Offset: 0x00048A8C
		private void ResultPane_SetActive(object sender, EventArgs e)
		{
			if (!base.IsActive)
			{
				throw new InvalidOperationException("container result pane must be activated at the first");
			}
			AbstractResultPane abstractResultPane = sender as AbstractResultPane;
			if (!this.isInSetActive && !this.IsActiveToContainer(abstractResultPane))
			{
				this.ResultPanesActiveToContainer.Add(abstractResultPane);
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0004A8D0 File Offset: 0x00048AD0
		private void ResultPane_KillingActive(object sender, EventArgs e)
		{
			if (!base.IsActive)
			{
				throw new InvalidOperationException("container result pane must be de-activated at the last");
			}
			AbstractResultPane abstractResultPane = sender as AbstractResultPane;
			if (!this.isInKillingActive && this.IsActiveToContainer(abstractResultPane))
			{
				this.ResultPanesActiveToContainer.Remove(abstractResultPane);
			}
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0004A920 File Offset: 0x00048B20
		private void ResultPane_EnabledChanging(object sender, EventArgs e)
		{
			if (this.ResultPanes.Count((AbstractResultPane childResultPane) => childResultPane.Enabled) == 0)
			{
				base.Enabled = true;
			}
			AbstractResultPane abstractResultPane = sender as AbstractResultPane;
			if (abstractResultPane.Enabled && this.SelectedResultPane == abstractResultPane)
			{
				this.SelectedResultPane = (abstractResultPane.DependedResultPane ?? abstractResultPane);
			}
			this.TryToUnbindEnabledResultPaneFromContainer(abstractResultPane);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0004A998 File Offset: 0x00048B98
		private void ResultPane_EnabledChanged(object sender, EventArgs e)
		{
			AbstractResultPane resultPane = sender as AbstractResultPane;
			this.TryToBindEnabledResultPaneToContainer(resultPane);
			if (this.ResultPanes.Count((AbstractResultPane childResultPane) => childResultPane.Enabled) == 0)
			{
				base.Enabled = false;
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0004A9E5 File Offset: 0x00048BE5
		private bool TryToBindEnabledResultPaneToContainer(AbstractResultPane resultPane)
		{
			return resultPane.Enabled && (this.TryToBindToResultPaneCommandsOfContainer(resultPane) | this.TryToBindToDependedResultPaneInContainer(resultPane) | this.TryToBindDependentResultPanesInContainerTo(resultPane) | this.TryToBindToStatusOfContainer(resultPane) | this.TryToBindToSelectionCommandsOfContainer(resultPane) | this.TryToBindToSelectionOfContainer(resultPane));
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0004AA20 File Offset: 0x00048C20
		private bool TryToUnbindEnabledResultPaneFromContainer(AbstractResultPane resultPane)
		{
			return resultPane.Enabled && (this.TryToUnbindFromStatusOfContainer(resultPane) | this.TryToUnbindFromSelectionCommandsOfContainer(resultPane) | this.TryToUnbindFromSelectionOfContainer(resultPane) | this.TryToUnbindDependentResultPanesInContainerFrom(resultPane) | this.TryToUnbindFromDependedResultPaneInContainer(resultPane) | this.TryToUnbindFromResultPaneCommandsOfContainer(resultPane));
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x0004AA5B File Offset: 0x00048C5B
		// (set) Token: 0x06001278 RID: 4728 RVA: 0x0004AA64 File Offset: 0x00048C64
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AbstractResultPane SelectedResultPane
		{
			get
			{
				return this.selectedResultPane;
			}
			set
			{
				if (this.SelectedResultPane != value)
				{
					if (value != null)
					{
						if (this.ResultPanes.IndexOf(value) == -1)
						{
							throw new ArgumentOutOfRangeException();
						}
						if (base.IsActive && !value.IsActive)
						{
							value.OnSetActive();
						}
						else if (!this.IsActiveToContainer(value))
						{
							this.ResultPanesActiveToContainer.Add(value);
						}
					}
					if (this.SelectedResultPane != null)
					{
						this.TryToUnbindActiveResultPaneFromContainer(this.SelectedResultPane);
					}
					this.selectedResultPane = value;
					if (this.SelectedResultPane != null)
					{
						this.TryToBindActiveResultPaneToContainer(this.SelectedResultPane);
					}
					this.OnSelectedResultPaneChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06001279 RID: 4729 RVA: 0x0004AAFE File Offset: 0x00048CFE
		// (remove) Token: 0x0600127A RID: 4730 RVA: 0x0004AB11 File Offset: 0x00048D11
		public event EventHandler SelectedResultPaneChanged
		{
			add
			{
				base.Events.AddHandler(ContainerResultPane.EventSelectedResultPaneChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ContainerResultPane.EventSelectedResultPaneChanged, value);
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0004AB24 File Offset: 0x00048D24
		protected virtual void OnSelectedResultPaneChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ContainerResultPane.EventSelectedResultPaneChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0004AB54 File Offset: 0x00048D54
		public ResultPane SelectedAtomResultPane
		{
			get
			{
				ContainerResultPane containerResultPane = this.SelectedResultPane as ContainerResultPane;
				if (containerResultPane == null)
				{
					return this.SelectedResultPane as ResultPane;
				}
				return containerResultPane.SelectedAtomResultPane;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0004AB82 File Offset: 0x00048D82
		public ChangeNotifyingCollection<AbstractResultPane> ResultPanesActiveToContainer
		{
			get
			{
				return this.resultPanesActiveToContainer;
			}
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0004AB8A File Offset: 0x00048D8A
		public bool IsActiveToContainer(AbstractResultPane resultPane)
		{
			return this.ResultPanesActiveToContainer.Contains(resultPane);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0004AB98 File Offset: 0x00048D98
		private void ResultPanesActiveToContainer_ListChanging(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				this.RemovingActiveResultPaneAt(e.NewIndex);
				return;
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				for (int i = this.ResultPanesActiveToContainer.Count - 1; i >= 0; i--)
				{
					this.RemovingActiveResultPaneAt(i);
				}
			}
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0004ABE2 File Offset: 0x00048DE2
		private void ResultPanesActiveToContainer_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				this.InsertedActiveResultPaneAt(e.NewIndex);
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0004ABFC File Offset: 0x00048DFC
		private void InsertedActiveResultPaneAt(int index)
		{
			AbstractResultPane abstractResultPane = this.ResultPanesActiveToContainer[index];
			if (!this.ResultPanes.Contains(abstractResultPane))
			{
				throw new InvalidOperationException("The result pane inserted into ResultPanesActiveToContainer must be contained in this.ResultPanes.");
			}
			if (base.IsActive && !abstractResultPane.IsActive)
			{
				throw new InvalidOperationException("when current ContainerResultPane is active, resultPane must be active before inserting it into ResultPanesActiveToContainer");
			}
			this.TryToBindActiveResultPaneToContainer(abstractResultPane);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0004AC54 File Offset: 0x00048E54
		protected void RemovingActiveResultPaneAt(int index)
		{
			AbstractResultPane abstractResultPane = this.ResultPanesActiveToContainer[index];
			if (base.IsActive && !abstractResultPane.IsActive)
			{
				throw new InvalidOperationException("when current ContainerResultPane is active, resultPane must be active before removing it from ResultPanesActiveToContainer");
			}
			if (this.SelectedResultPane == abstractResultPane)
			{
				this.SelectedResultPane = null;
			}
			this.TryToUnbindActiveResultPaneFromContainer(abstractResultPane);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0004ACA1 File Offset: 0x00048EA1
		private bool TryToBindActiveResultPaneToContainer(AbstractResultPane resultPane)
		{
			return this.IsActiveToContainer(resultPane) && (this.TryToBindToStatusOfContainer(resultPane) | this.TryToBindToExportListCommandsOfContainer(resultPane) | this.TryToBindToViewModeCommandsOfContainer(resultPane) | this.TryToBindToSelectionCommandsOfContainer(resultPane) | this.TryToBindToRefreshableDataSourceOfContainer(resultPane) | this.TryToBindToSelectionOfContainer(resultPane));
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0004ACDD File Offset: 0x00048EDD
		private bool TryToUnbindActiveResultPaneFromContainer(AbstractResultPane resultPane)
		{
			return this.IsActiveToContainer(resultPane) && (this.TryToUnbindFromSelectionOfContainer(resultPane) | this.TryToUnbindFromRefreshableDataSourceOfContainer(resultPane) | this.TryToUnbindFromSelectionCommandsOfContainer(resultPane) | this.TryToUnbindFromViewModeCommandsOfContainer(resultPane) | this.TryToUnbindFromExportListCommandsOfContainer(resultPane) | this.TryToUnbindFromStatusOfContainer(resultPane));
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0004AD19 File Offset: 0x00048F19
		private bool HasImpactOnDependedResultPaneInContainer(AbstractResultPane resultPane)
		{
			return this.ResultPanes.Contains(resultPane) && resultPane.Enabled && resultPane.DependedResultPane != null && this.ResultPanes.Contains(resultPane.DependedResultPane);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0004AD4C File Offset: 0x00048F4C
		private bool TryToBindToDependedResultPaneInContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnDependedResultPaneInContainer(resultPane))
			{
				this.BindResultPaneCommandsToDependedResultPane(resultPane);
				return true;
			}
			return false;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0004AD61 File Offset: 0x00048F61
		private bool TryToUnbindFromDependedResultPaneInContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnDependedResultPaneInContainer(resultPane))
			{
				this.UnbindResultPaneCommandsFromDependedResultPane(resultPane);
				return true;
			}
			return false;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0004AD76 File Offset: 0x00048F76
		private bool HasImpactFromDependentResultPanes(AbstractResultPane resultPane)
		{
			return this.ResultPanes.Contains(resultPane) && resultPane.Enabled && resultPane is ResultPane;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0004AD9C File Offset: 0x00048F9C
		private bool TryToBindDependentResultPanesInContainerTo(AbstractResultPane resultPane)
		{
			if (this.HasImpactFromDependentResultPanes(resultPane))
			{
				ResultPane resultPane2 = resultPane as ResultPane;
				resultPane2.DependentResultPaneCommands.CommandAdded += new CommandEventHandler(this.DependentResultPaneCommandsOfResultPane_CommandAdded);
				resultPane2.DependentResultPaneCommands.CommandRemoved += new CommandEventHandler(this.DependentResultPaneCommandsOfResultPane_CommandRemoved);
				List<AbstractResultPane> directEnabledDependentResultPanesInContainer = this.GetDirectEnabledDependentResultPanesInContainer(resultPane2);
				foreach (AbstractResultPane resultPane3 in directEnabledDependentResultPanesInContainer)
				{
					this.BindResultPaneCommandsToDependedResultPane(resultPane3);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0004AE34 File Offset: 0x00049034
		private bool TryToUnbindDependentResultPanesInContainerFrom(AbstractResultPane resultPane)
		{
			if (this.HasImpactFromDependentResultPanes(resultPane))
			{
				ResultPane resultPane2 = resultPane as ResultPane;
				List<AbstractResultPane> directEnabledDependentResultPanesInContainer = this.GetDirectEnabledDependentResultPanesInContainer(resultPane2);
				foreach (AbstractResultPane resultPane3 in directEnabledDependentResultPanesInContainer)
				{
					this.UnbindResultPaneCommandsFromDependedResultPane(resultPane3);
				}
				resultPane2.DependentResultPaneCommands.CommandAdded -= new CommandEventHandler(this.DependentResultPaneCommandsOfResultPane_CommandAdded);
				resultPane2.DependentResultPaneCommands.CommandRemoved -= new CommandEventHandler(this.DependentResultPaneCommandsOfResultPane_CommandRemoved);
				return true;
			}
			return false;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0004AECC File Offset: 0x000490CC
		private void BindResultPaneCommandsToDependedResultPane(AbstractResultPane resultPane)
		{
			ContainerResultPane.BindResultPaneCommandsToCommands(resultPane.DependedResultPane.DependentResultPaneCommands, resultPane.DependedResultPane.DependentResultPanes, resultPane, new CommandEventHandler(this.ResultPaneCommandsOfDependentResultPane_CommandAdded), new CommandEventHandler(this.ResultPaneCommandsOfDependentResultPane_CommandRemoved));
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0004AF02 File Offset: 0x00049102
		private void UnbindResultPaneCommandsFromDependedResultPane(AbstractResultPane resultPane)
		{
			ContainerResultPane.UnbindResultPaneCommandsFromCommands(resultPane.DependedResultPane.DependentResultPaneCommands, resultPane.DependedResultPane.DependentResultPanes, resultPane, new CommandEventHandler(this.ResultPaneCommandsOfDependentResultPane_CommandAdded), new CommandEventHandler(this.ResultPaneCommandsOfDependentResultPane_CommandRemoved));
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0004AF38 File Offset: 0x00049138
		private void ResultPaneCommandsOfDependentResultPane_CommandAdded(object sender, CommandEventArgs e)
		{
			CommandCollection resultPaneCommands = sender as CommandCollection;
			AbstractResultPane abstractResultPane = this.FindResultPaneWithResultPaneCommandsInContainer(resultPaneCommands);
			ContainerResultPane.AddResultPaneCommandToCommands(abstractResultPane.DependedResultPane.DependentResultPaneCommands, abstractResultPane.DependedResultPane.DependentResultPanes, abstractResultPane, e.Command);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0004AF78 File Offset: 0x00049178
		private void ResultPaneCommandsOfDependentResultPane_CommandRemoved(object sender, CommandEventArgs e)
		{
			CommandCollection resultPaneCommands = sender as CommandCollection;
			AbstractResultPane abstractResultPane = this.FindResultPaneWithResultPaneCommandsInContainer(resultPaneCommands);
			ContainerResultPane.RemoveResultPaneCommandFromCommands(abstractResultPane.DependedResultPane.DependentResultPaneCommands, abstractResultPane.DependedResultPane.DependentResultPanes, abstractResultPane, e.Command);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0004AFB6 File Offset: 0x000491B6
		private void DependentResultPaneCommandsOfResultPane_CommandAdded(object sender, CommandEventArgs e)
		{
			e.Command.Executing += this.DependentResultPaneCommand_Executing;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0004AFCF File Offset: 0x000491CF
		private void DependentResultPaneCommandsOfResultPane_CommandRemoved(object sender, CommandEventArgs e)
		{
			e.Command.Executing -= this.DependentResultPaneCommand_Executing;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0004AFE8 File Offset: 0x000491E8
		private void DependentResultPaneCommand_Executing(object sender, CancelEventArgs e)
		{
			this.SelectResultPaneByResultPaneCommand(sender as Command);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0004B028 File Offset: 0x00049228
		private void SelectResultPaneByResultPaneCommand(Command command)
		{
			Predicate<AbstractResultPane> selectionCondition = delegate(AbstractResultPane resultPane)
			{
				bool result = false;
				if (resultPane.ResultPaneCommands.Contains(command))
				{
					result = true;
				}
				return result;
			};
			this.SelectResultPane(selectionCondition);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0004B058 File Offset: 0x00049258
		protected override void OnSharedSettingsChanged()
		{
			foreach (AbstractResultPane abstractResultPane in this.ResultPanes)
			{
				abstractResultPane.SharedSettings = base.SharedSettings;
			}
			base.OnSharedSettingsChanged();
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0004B0B0 File Offset: 0x000492B0
		public override void LoadComponentSettings()
		{
			base.LoadComponentSettings();
			foreach (AbstractResultPane abstractResultPane in this.ResultPanes)
			{
				abstractResultPane.SettingsKey = abstractResultPane.Name;
				abstractResultPane.LoadComponentSettings();
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0004B110 File Offset: 0x00049310
		public override void ResetComponentSettings()
		{
			base.ResetComponentSettings();
			foreach (AbstractResultPane abstractResultPane in this.ResultPanes)
			{
				abstractResultPane.ResetComponentSettings();
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0004B164 File Offset: 0x00049364
		public override void SaveComponentSettings()
		{
			foreach (AbstractResultPane abstractResultPane in this.ResultPanes)
			{
				abstractResultPane.SaveComponentSettings();
			}
			base.SaveComponentSettings();
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0004B1B8 File Offset: 0x000493B8
		private bool HasImpactOnStatusOfContainer(AbstractResultPane resultPane)
		{
			if (this.ResultPanes.Contains(resultPane) && resultPane.Enabled && this.IsActiveToContainer(resultPane) && this.SelectedResultPane != null)
			{
				if (resultPane == this.SelectedResultPane)
				{
					return true;
				}
				if (ResultPane.IsDependedResultPane(resultPane, this.SelectedResultPane))
				{
					return this.GetTopEnabledResultPaneInContainer(resultPane, new Predicate<AbstractResultPane>(this.IsActiveToContainer)) == resultPane;
				}
			}
			return false;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0004B230 File Offset: 0x00049430
		private bool TryToBindToStatusOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnStatusOfContainer(resultPane))
			{
				AbstractResultPane abstractResultPane;
				if (resultPane != this.SelectedResultPane)
				{
					abstractResultPane = this.GetTopEnabledResultPaneInContainer(this.SelectedResultPane, new Predicate<AbstractResultPane>(this.IsActiveToContainer), (AbstractResultPane dependedResultPane) => dependedResultPane == resultPane);
				}
				else
				{
					abstractResultPane = null;
				}
				AbstractResultPane abstractResultPane2 = abstractResultPane;
				AbstractResultPane topResultPane = (resultPane == this.SelectedResultPane) ? this.GetTopEnabledResultPaneInContainer(this.SelectedResultPane, new Predicate<AbstractResultPane>(this.IsActiveToContainer)) : resultPane;
				if (abstractResultPane2 != null)
				{
					this.UnbindTopResultPaneFromStatusOfContainer(abstractResultPane2);
				}
				this.BindTopResultPaneToStatusOfContainer(topResultPane);
				return true;
			}
			return false;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0004B2EC File Offset: 0x000494EC
		private bool TryToUnbindFromStatusOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnStatusOfContainer(resultPane))
			{
				AbstractResultPane topResultPane = (resultPane == this.SelectedResultPane) ? this.GetTopEnabledResultPaneInContainer(this.SelectedResultPane, new Predicate<AbstractResultPane>(this.IsActiveToContainer)) : resultPane;
				AbstractResultPane abstractResultPane;
				if (resultPane != this.SelectedResultPane)
				{
					abstractResultPane = this.GetTopEnabledResultPaneInContainer(this.SelectedResultPane, new Predicate<AbstractResultPane>(this.IsActiveToContainer), (AbstractResultPane dependedResultPane) => dependedResultPane == resultPane);
				}
				else
				{
					abstractResultPane = null;
				}
				AbstractResultPane abstractResultPane2 = abstractResultPane;
				this.UnbindTopResultPaneFromStatusOfContainer(topResultPane);
				if (abstractResultPane2 != null)
				{
					this.BindTopResultPaneToStatusOfContainer(abstractResultPane2);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0004B394 File Offset: 0x00049594
		private void BindTopResultPaneToStatusOfContainer(AbstractResultPane topResultPane)
		{
			topResultPane.StatusChanged += this.TopResultPane_StatusChanged;
			this.TopResultPane_StatusChanged(topResultPane, EventArgs.Empty);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0004B3B4 File Offset: 0x000495B4
		private void UnbindTopResultPaneFromStatusOfContainer(AbstractResultPane topResultPane)
		{
			topResultPane.StatusChanged -= this.TopResultPane_StatusChanged;
			base.Status = string.Empty;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004B3D4 File Offset: 0x000495D4
		private void TopResultPane_StatusChanged(object sender, EventArgs e)
		{
			AbstractResultPane abstractResultPane = sender as AbstractResultPane;
			base.Status = abstractResultPane.Status;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0004B3F4 File Offset: 0x000495F4
		private bool HasImpactOnResultPaneCommandsOfContainer(AbstractResultPane resultPane)
		{
			return this.ResultPanes.Contains(resultPane) && resultPane.Enabled && this.GetTopEnabledResultPaneInContainer(resultPane) == resultPane;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0004B438 File Offset: 0x00049638
		private bool TryToBindToResultPaneCommandsOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnResultPaneCommandsOfContainer(resultPane))
			{
				if (resultPane is ResultPane)
				{
					List<AbstractResultPane> enabledDependentResultPanesInContainer = this.GetEnabledDependentResultPanesInContainer(resultPane as ResultPane, (AbstractResultPane dependentResultPane) => this.GetTopEnabledResultPaneInContainer(dependentResultPane) == resultPane);
					foreach (AbstractResultPane resultPane2 in enabledDependentResultPanesInContainer)
					{
						this.UnbindResultPaneCommandsFromResultPaneCommandsOfContainer(resultPane2);
					}
				}
				this.BindResultPaneCommandsToResultPaneCommandsOfContainer(resultPane);
				return true;
			}
			return false;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0004B50C File Offset: 0x0004970C
		private bool TryToUnbindFromResultPaneCommandsOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnResultPaneCommandsOfContainer(resultPane))
			{
				this.UnbindResultPaneCommandsFromResultPaneCommandsOfContainer(resultPane);
				if (resultPane is ResultPane)
				{
					List<AbstractResultPane> enabledDependentResultPanesInContainer = this.GetEnabledDependentResultPanesInContainer(resultPane as ResultPane, (AbstractResultPane dependentResultPane) => this.GetTopEnabledResultPaneInContainer(dependentResultPane) == resultPane);
					foreach (AbstractResultPane resultPane2 in enabledDependentResultPanesInContainer)
					{
						this.BindResultPaneCommandsToResultPaneCommandsOfContainer(resultPane2);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0004B5C0 File Offset: 0x000497C0
		private void BindResultPaneCommandsToResultPaneCommandsOfContainer(AbstractResultPane resultPane)
		{
			ContainerResultPane.BindResultPaneCommandsToCommands(base.ResultPaneCommands, this.ResultPanes, resultPane, new CommandEventHandler(this.ResultPaneCommandsOfResultPane_CommandAdded), new CommandEventHandler(this.ResultPaneCommandsOfResultPane_CommandRemoved));
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0004B5EC File Offset: 0x000497EC
		private void UnbindResultPaneCommandsFromResultPaneCommandsOfContainer(AbstractResultPane resultPane)
		{
			ContainerResultPane.UnbindResultPaneCommandsFromCommands(base.ResultPaneCommands, this.ResultPanes, resultPane, new CommandEventHandler(this.ResultPaneCommandsOfResultPane_CommandAdded), new CommandEventHandler(this.ResultPaneCommandsOfResultPane_CommandRemoved));
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0004B618 File Offset: 0x00049818
		private void ResultPaneCommandsOfResultPane_CommandAdded(object sender, CommandEventArgs e)
		{
			CommandCollection resultPaneCommands = sender as CommandCollection;
			AbstractResultPane resultPane = this.FindResultPaneWithResultPaneCommandsInContainer(resultPaneCommands);
			ContainerResultPane.AddResultPaneCommandToCommands(base.ResultPaneCommands, this.ResultPanes, resultPane, e.Command);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0004B64C File Offset: 0x0004984C
		private void ResultPaneCommandsOfResultPane_CommandRemoved(object sender, CommandEventArgs e)
		{
			CommandCollection resultPaneCommands = sender as CommandCollection;
			AbstractResultPane resultPane = this.FindResultPaneWithResultPaneCommandsInContainer(resultPaneCommands);
			ContainerResultPane.RemoveResultPaneCommandFromCommands(base.ResultPaneCommands, this.ResultPanes, resultPane, e.Command);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0004B680 File Offset: 0x00049880
		private bool HasImpactOnExportListCommandsOfContainer(AbstractResultPane resultPane)
		{
			return this.ResultPanes.Contains(resultPane) && resultPane == this.SelectedResultPane;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0004B69B File Offset: 0x0004989B
		private bool TryToBindToExportListCommandsOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnExportListCommandsOfContainer(resultPane))
			{
				ContainerResultPane.BindCommandsOfSelectedResultPaneToCommandsOfContainer(this.SelectedResultPane.ExportListCommands, base.ExportListCommands, new CommandEventHandler(this.ExportListCommandsOfSelectedResultPane_CommandAdded), new CommandEventHandler(this.ExportListCommandsOfSelectedResultPane_CommandRemoved));
				return true;
			}
			return false;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0004B6D7 File Offset: 0x000498D7
		private bool TryToUnbindFromExportListCommandsOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnExportListCommandsOfContainer(resultPane))
			{
				ContainerResultPane.UnbindCommandsOfSelectedResultPaneFromCommandsOfContainer(this.SelectedResultPane.ExportListCommands, base.ExportListCommands, new CommandEventHandler(this.ExportListCommandsOfSelectedResultPane_CommandAdded), new CommandEventHandler(this.ExportListCommandsOfSelectedResultPane_CommandRemoved));
				return true;
			}
			return false;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0004B713 File Offset: 0x00049913
		private void ExportListCommandsOfSelectedResultPane_CommandAdded(object sender, CommandEventArgs e)
		{
			ContainerResultPane.CommandsOfSelectedResultPane_CommandAdded(this.SelectedResultPane.ExportListCommands, base.ExportListCommands, e);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004B72C File Offset: 0x0004992C
		private void ExportListCommandsOfSelectedResultPane_CommandRemoved(object sender, CommandEventArgs e)
		{
			base.ExportListCommands.Remove(e.Command);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0004B73F File Offset: 0x0004993F
		private bool HasImpactOnViewModeCommandsOfContainer(AbstractResultPane resultPane)
		{
			return this.ResultPanes.Contains(resultPane) && resultPane == this.SelectedResultPane;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0004B75A File Offset: 0x0004995A
		private bool TryToBindToViewModeCommandsOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnViewModeCommandsOfContainer(resultPane))
			{
				ContainerResultPane.BindCommandsOfSelectedResultPaneToCommandsOfContainer(this.SelectedResultPane.ViewModeCommands, base.ViewModeCommands, new CommandEventHandler(this.ViewModeCommandsOfSelectedResultPane_CommandAdded), new CommandEventHandler(this.ViewModeCommandsOfSelectedResultPane_CommandRemoved));
				return true;
			}
			return false;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0004B796 File Offset: 0x00049996
		private bool TryToUnbindFromViewModeCommandsOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnViewModeCommandsOfContainer(resultPane))
			{
				ContainerResultPane.UnbindCommandsOfSelectedResultPaneFromCommandsOfContainer(this.SelectedResultPane.ViewModeCommands, base.ViewModeCommands, new CommandEventHandler(this.ViewModeCommandsOfSelectedResultPane_CommandAdded), new CommandEventHandler(this.ViewModeCommandsOfSelectedResultPane_CommandRemoved));
				return true;
			}
			return false;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0004B7D2 File Offset: 0x000499D2
		private void ViewModeCommandsOfSelectedResultPane_CommandAdded(object sender, CommandEventArgs e)
		{
			ContainerResultPane.CommandsOfSelectedResultPane_CommandAdded(this.SelectedResultPane.ViewModeCommands, base.ViewModeCommands, e);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0004B7EB File Offset: 0x000499EB
		private void ViewModeCommandsOfSelectedResultPane_CommandRemoved(object sender, CommandEventArgs e)
		{
			base.ViewModeCommands.Remove(e.Command);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0004B800 File Offset: 0x00049A00
		private bool HasImpactOnSelectionCommandsOfContainer(AbstractResultPane resultPane)
		{
			return this.ResultPanes.Contains(resultPane) && resultPane.Enabled && this.IsActiveToContainer(resultPane) && this.SelectedResultPane != null && (resultPane == this.SelectedResultPane || ResultPane.IsDependedResultPane(this.SelectedResultPane, resultPane) || ResultPane.IsDependedResultPane(resultPane, this.SelectedResultPane));
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0004B85C File Offset: 0x00049A5C
		private bool TryToBindToSelectionCommandsOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnSelectionCommandsOfContainer(resultPane))
			{
				if ((resultPane is ContainerResultPane && resultPane.DependedResultPane == null) || (resultPane is ResultPane && resultPane.DependedResultPane == null && (resultPane as ResultPane).DependentResultPanes.Count == 0))
				{
					ContainerResultPane.BindCommandsOfSelectedResultPaneToCommandsOfContainer(this.SelectedResultPane.SelectionCommands, base.SelectionCommands, new CommandEventHandler(this.SelectionCommandsOfSelectedResultPane_CommandAdded), new CommandEventHandler(this.SelectionCommandsOfSelectedResultPane_CommandRemoved));
				}
				else
				{
					List<AbstractResultPane> allResultPanesHavingImpactOnSelectionCommandsOfContainer = this.GetAllResultPanesHavingImpactOnSelectionCommandsOfContainer();
					List<AbstractResultPane> list = new List<AbstractResultPane>();
					list.Add(resultPane);
					if (resultPane == this.SelectedResultPane)
					{
						list = allResultPanesHavingImpactOnSelectionCommandsOfContainer;
					}
					for (int i = 0; i < list.Count; i++)
					{
						AbstractResultPane abstractResultPane = list[i];
						int positionInSelectionCommandsOfContainer = base.SelectionCommands.Count;
						int num = allResultPanesHavingImpactOnSelectionCommandsOfContainer.IndexOf(resultPane);
						if (num > 0)
						{
							positionInSelectionCommandsOfContainer = base.SelectionCommands.IndexOf(this.GetOwnerSelectionCommandInContainer(allResultPanesHavingImpactOnSelectionCommandsOfContainer[num - 1].SelectionCommands)) + 1;
						}
						else if (allResultPanesHavingImpactOnSelectionCommandsOfContainer.Count > 1 && this.GetOwnerSelectionCommandInContainer(allResultPanesHavingImpactOnSelectionCommandsOfContainer[1].SelectionCommands) != null)
						{
							positionInSelectionCommandsOfContainer = base.SelectionCommands.IndexOf(this.GetOwnerSelectionCommandInContainer(allResultPanesHavingImpactOnSelectionCommandsOfContainer[1].SelectionCommands));
						}
						this.CreateOwnerSelectionCommandInContainer(abstractResultPane, positionInSelectionCommandsOfContainer);
						abstractResultPane.SelectionChanged += this.ResultPaneHavingImpactOnSelectionCommandsOfContainer_SelectionChanged;
						this.ResultPaneHavingImpactOnSelectionCommandsOfContainer_SelectionChanged(abstractResultPane, EventArgs.Empty);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0004B9C0 File Offset: 0x00049BC0
		private bool TryToUnbindFromSelectionCommandsOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnSelectionCommandsOfContainer(resultPane))
			{
				if ((resultPane is ContainerResultPane && resultPane.DependedResultPane == null) || (resultPane is ResultPane && resultPane.DependedResultPane == null && (resultPane as ResultPane).DependentResultPanes.Count == 0))
				{
					ContainerResultPane.UnbindCommandsOfSelectedResultPaneFromCommandsOfContainer(this.SelectedResultPane.SelectionCommands, base.SelectionCommands, new CommandEventHandler(this.SelectionCommandsOfSelectedResultPane_CommandAdded), new CommandEventHandler(this.SelectionCommandsOfSelectedResultPane_CommandRemoved));
				}
				else
				{
					List<AbstractResultPane> list = new List<AbstractResultPane>();
					list.Add(resultPane);
					if (resultPane == this.SelectedResultPane)
					{
						list = this.GetAllResultPanesHavingImpactOnSelectionCommandsOfContainer();
					}
					foreach (AbstractResultPane abstractResultPane in list)
					{
						this.RemoveOwnerSelectionCommandInContainer(abstractResultPane);
						abstractResultPane.SelectionChanged -= this.ResultPaneHavingImpactOnSelectionCommandsOfContainer_SelectionChanged;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0004BAB0 File Offset: 0x00049CB0
		private void SelectionCommandsOfSelectedResultPane_CommandAdded(object sender, CommandEventArgs e)
		{
			ContainerResultPane.CommandsOfSelectedResultPane_CommandAdded(this.SelectedResultPane.SelectionCommands, base.SelectionCommands, e);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0004BAC9 File Offset: 0x00049CC9
		private void SelectionCommandsOfSelectedResultPane_CommandRemoved(object sender, CommandEventArgs e)
		{
			base.SelectionCommands.Remove(e.Command);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0004BADC File Offset: 0x00049CDC
		private List<AbstractResultPane> GetAllResultPanesHavingImpactOnSelectionCommandsOfContainer()
		{
			List<AbstractResultPane> list = new List<AbstractResultPane>();
			list.AddRange(this.GetEnabledDependedResultPanesInContainer(this.SelectedResultPane, new Predicate<AbstractResultPane>(this.IsActiveToContainer)));
			list.Add(this.SelectedResultPane);
			if (this.SelectedResultPane is ResultPane)
			{
				list.AddRange(this.GetEnabledDependentResultPanesInContainer(this.SelectedResultPane as ResultPane, new Predicate<AbstractResultPane>(this.IsActiveToContainer)));
			}
			return list;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0004BB4C File Offset: 0x00049D4C
		private void ResultPaneHavingImpactOnSelectionCommandsOfContainer_SelectionChanged(object sender, EventArgs e)
		{
			AbstractResultPane abstractResultPane = sender as AbstractResultPane;
			Command ownerSelectionCommandInContainer = this.GetOwnerSelectionCommandInContainer(abstractResultPane.SelectionCommands);
			string text = abstractResultPane.SelectionDataObject.GetText();
			ownerSelectionCommandInContainer.Visible = abstractResultPane.HasSelection;
			ownerSelectionCommandInContainer.Text = ((text == null || string.IsNullOrEmpty(text.Trim())) ? new LocalizedString(ownerSelectionCommandInContainer.Name) : new LocalizedString(this.EscapeAccelerators(text)));
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0004BBB9 File Offset: 0x00049DB9
		private string EscapeAccelerators(string originalText)
		{
			return originalText.Replace("&", "&&");
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0004BBCC File Offset: 0x00049DCC
		private void CreateOwnerSelectionCommandInContainer(AbstractResultPane resultPane, int positionInSelectionCommandsOfContainer)
		{
			Command command = new Command();
			command.Style = 4;
			this.SetOwnerSelectionCommandInContainer(resultPane.SelectionCommands, command);
			command.Commands.CommandAdded += new CommandEventHandler(this.CommandsOfOwnerSelectionCommandInContainer_CommandAdded);
			command.Commands.CommandRemoved += new CommandEventHandler(this.CommandsOfOwnerSelectionCommandInContainer_CommandRemoved);
			command.Commands.AddRange(resultPane.SelectionCommands);
			resultPane.SelectionCommands.CommandAdded += new CommandEventHandler(this.SelectionCommandsOfResultPane_CommandAdded);
			resultPane.SelectionCommands.CommandRemoved += new CommandEventHandler(this.SelectionCommandsOfResultPane_CommandRemoved);
			base.SelectionCommands.Insert(positionInSelectionCommandsOfContainer, command);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0004BC70 File Offset: 0x00049E70
		private void RemoveOwnerSelectionCommandInContainer(AbstractResultPane resultPane)
		{
			resultPane.SelectionCommands.CommandAdded -= new CommandEventHandler(this.SelectionCommandsOfResultPane_CommandAdded);
			resultPane.SelectionCommands.CommandRemoved -= new CommandEventHandler(this.SelectionCommandsOfResultPane_CommandRemoved);
			Command ownerSelectionCommandInContainer = this.GetOwnerSelectionCommandInContainer(resultPane.SelectionCommands);
			this.SetOwnerSelectionCommandInContainer(resultPane.SelectionCommands, null);
			ownerSelectionCommandInContainer.Commands.Clear();
			ownerSelectionCommandInContainer.Commands.CommandAdded -= new CommandEventHandler(this.CommandsOfOwnerSelectionCommandInContainer_CommandAdded);
			ownerSelectionCommandInContainer.Commands.CommandRemoved -= new CommandEventHandler(this.CommandsOfOwnerSelectionCommandInContainer_CommandRemoved);
			base.SelectionCommands.Remove(ownerSelectionCommandInContainer);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0004BD0A File Offset: 0x00049F0A
		private void CommandsOfOwnerSelectionCommandInContainer_CommandAdded(object sender, CommandEventArgs e)
		{
			e.Command.Executing += this.CommandInOwnerSelectionCommandInContainer_Executing;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0004BD23 File Offset: 0x00049F23
		private void CommandsOfOwnerSelectionCommandInContainer_CommandRemoved(object sender, CommandEventArgs e)
		{
			e.Command.Executing -= this.CommandInOwnerSelectionCommandInContainer_Executing;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0004BD3C File Offset: 0x00049F3C
		private void CommandInOwnerSelectionCommandInContainer_Executing(object sender, CancelEventArgs e)
		{
			this.GetOwnerSelectionCommandInContainer(sender as Command).Invoke();
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0004BD50 File Offset: 0x00049F50
		private void SelectionCommandsOfResultPane_CommandAdded(object sender, CommandEventArgs e)
		{
			Command ownerSelectionCommandInContainer = this.GetOwnerSelectionCommandInContainer(sender as CommandCollection);
			ownerSelectionCommandInContainer.Commands.Insert((sender as CommandCollection).IndexOf(e.Command), e.Command);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0004BD8C File Offset: 0x00049F8C
		private void SelectionCommandsOfResultPane_CommandRemoved(object sender, CommandEventArgs e)
		{
			Command ownerSelectionCommandInContainer = this.GetOwnerSelectionCommandInContainer(sender as CommandCollection);
			ownerSelectionCommandInContainer.Commands.Remove(e.Command);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0004BDB8 File Offset: 0x00049FB8
		private Command GetOwnerSelectionCommandInContainer(Command selectionCommandOfResultPane)
		{
			foreach (Command command in this.ownerSelectionCommandsInContainer.Values)
			{
				if (command.Commands.Contains(selectionCommandOfResultPane))
				{
					return command;
				}
			}
			return null;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0004BE20 File Offset: 0x0004A020
		private Command GetOwnerSelectionCommandInContainer(CommandCollection selectionCommandsOfResultPane)
		{
			if (!this.ownerSelectionCommandsInContainer.ContainsKey(selectionCommandsOfResultPane))
			{
				return null;
			}
			return this.ownerSelectionCommandsInContainer[selectionCommandsOfResultPane];
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0004BE3E File Offset: 0x0004A03E
		private void SetOwnerSelectionCommandInContainer(CommandCollection selectionCommandsOfResultPane, Command ownerSelectionCommandInContainer)
		{
			if (ownerSelectionCommandInContainer == null)
			{
				this.ownerSelectionCommandsInContainer.Remove(selectionCommandsOfResultPane);
				return;
			}
			this.ownerSelectionCommandsInContainer[selectionCommandsOfResultPane] = ownerSelectionCommandInContainer;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0004BE5E File Offset: 0x0004A05E
		private bool HasImpactOnRefreshableDataSourceOfContainer(AbstractResultPane resultPane)
		{
			return this.ResultPanes.Contains(resultPane) && resultPane == this.SelectedResultPane;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0004BE79 File Offset: 0x0004A079
		private bool TryToBindToRefreshableDataSourceOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnRefreshableDataSourceOfContainer(resultPane))
			{
				this.RefreshableDataSource = this.SelectedResultPane.RefreshableDataSource;
				this.SelectedResultPane.RefreshableDataSourceChanged += this.SelectedResultPane_RefreshableDataSourceChanged;
				return true;
			}
			return false;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0004BEAF File Offset: 0x0004A0AF
		private bool TryToUnbindFromRefreshableDataSourceOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnRefreshableDataSourceOfContainer(resultPane))
			{
				this.RefreshableDataSource = null;
				this.SelectedResultPane.RefreshableDataSourceChanged -= this.SelectedResultPane_RefreshableDataSourceChanged;
				return true;
			}
			return false;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0004BEDB File Offset: 0x0004A0DB
		private void SelectedResultPane_RefreshableDataSourceChanged(object sender, EventArgs e)
		{
			this.RefreshableDataSource = this.SelectedResultPane.RefreshableDataSource;
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0004BEF0 File Offset: 0x0004A0F0
		public override string SelectionHelpTopic
		{
			get
			{
				string result = null;
				for (AbstractResultPane dependedResultPane = this.SelectedResultPane; dependedResultPane != null; dependedResultPane = dependedResultPane.DependedResultPane)
				{
					if (this.ResultPanes.Contains(dependedResultPane) && dependedResultPane.Enabled && this.IsActiveToContainer(dependedResultPane) && dependedResultPane.HasSelection && !string.IsNullOrEmpty(dependedResultPane.SelectionHelpTopic))
					{
						result = dependedResultPane.SelectionHelpTopic;
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0004BF50 File Offset: 0x0004A150
		protected override void OnSetActive(EventArgs e)
		{
			try
			{
				this.isInSetActive = true;
				foreach (AbstractResultPane abstractResultPane in this.ResultPanes)
				{
					if (this.IsActiveToContainer(abstractResultPane))
					{
						abstractResultPane.OnSetActive();
					}
				}
			}
			finally
			{
				this.isInSetActive = false;
			}
			base.OnSetActive(e);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0004BFCC File Offset: 0x0004A1CC
		protected override void OnKillingActive(EventArgs e)
		{
			try
			{
				this.isInKillingActive = true;
				foreach (AbstractResultPane abstractResultPane in this.ResultPanes)
				{
					if (abstractResultPane.IsActive)
					{
						abstractResultPane.OnKillActive();
					}
				}
			}
			finally
			{
				this.isInKillingActive = false;
			}
			base.OnKillingActive(e);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0004C044 File Offset: 0x0004A244
		private bool HasImpactOnSelectionOfContainer(AbstractResultPane resultPane)
		{
			return this.ResultPanes.Contains(resultPane) && resultPane.Enabled && this.IsActiveToContainer(resultPane) && this.SelectedResultPane != null && (resultPane == this.SelectedResultPane || ResultPane.IsDependedResultPane(resultPane, this.SelectedResultPane));
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0004C094 File Offset: 0x0004A294
		private bool TryToBindToSelectionOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnSelectionOfContainer(resultPane))
			{
				List<AbstractResultPane> list = new List<AbstractResultPane>();
				list.Add(resultPane);
				if (resultPane == this.SelectedResultPane)
				{
					list = this.GetAllResultPanesHavingImpactOnSelectionOfContainer();
				}
				foreach (AbstractResultPane abstractResultPane in list)
				{
					abstractResultPane.SelectionChanged += this.ResultPaneHavingImpactOnSelectionOfContainer_SelectionChanged;
				}
				this.ResultPaneHavingImpactOnSelectionOfContainer_SelectionChanged(resultPane, EventArgs.Empty);
				return true;
			}
			return false;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0004C124 File Offset: 0x0004A324
		private bool TryToUnbindFromSelectionOfContainer(AbstractResultPane resultPane)
		{
			if (this.HasImpactOnSelectionOfContainer(resultPane))
			{
				List<AbstractResultPane> list = new List<AbstractResultPane>();
				list.Add(resultPane);
				if (resultPane == this.SelectedResultPane)
				{
					list = this.GetAllResultPanesHavingImpactOnSelectionOfContainer();
				}
				foreach (AbstractResultPane abstractResultPane in list)
				{
					abstractResultPane.SelectionChanged -= this.ResultPaneHavingImpactOnSelectionOfContainer_SelectionChanged;
				}
				if (resultPane == this.SelectedResultPane)
				{
					base.UpdateSelection(null, " ", null);
				}
				else
				{
					this.ResultPaneHavingImpactOnSelectionOfContainer_SelectionChanged(this.SelectedResultPane, EventArgs.Empty);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0004C1D4 File Offset: 0x0004A3D4
		private List<AbstractResultPane> GetAllResultPanesHavingImpactOnSelectionOfContainer()
		{
			List<AbstractResultPane> enabledDependedResultPanesInContainer = this.GetEnabledDependedResultPanesInContainer(this.SelectedResultPane, new Predicate<AbstractResultPane>(this.IsActiveToContainer));
			enabledDependedResultPanesInContainer.Add(this.SelectedResultPane);
			return enabledDependedResultPanesInContainer;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0004C207 File Offset: 0x0004A407
		private void ResultPaneHavingImpactOnSelectionOfContainer_SelectionChanged(object sender, EventArgs e)
		{
			this.UpdateSelection();
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0004C210 File Offset: 0x0004A410
		private void UpdateSelection()
		{
			AbstractResultPane dependedResultPane = this.SelectedResultPane;
			while (dependedResultPane != null && (!this.ResultPanes.Contains(dependedResultPane) || !dependedResultPane.Enabled || !this.IsActiveToContainer(dependedResultPane) || !dependedResultPane.HasSelection))
			{
				dependedResultPane = dependedResultPane.DependedResultPane;
			}
			if (dependedResultPane != null)
			{
				string text = " ";
				if ((dependedResultPane is ContainerResultPane && dependedResultPane.DependedResultPane == null) || (dependedResultPane is ResultPane && dependedResultPane.DependedResultPane == null && (dependedResultPane as ResultPane).DependentResultPanes.Count == 0))
				{
					text = dependedResultPane.SelectionDataObject.GetText();
					if (string.IsNullOrEmpty(text))
					{
						DataListViewResultPane dataListViewResultPane = dependedResultPane as DataListViewResultPane;
						if (dataListViewResultPane != null)
						{
							text = Strings.SelectionNameDoesNotExist(dataListViewResultPane.ListControl.Columns[dataListViewResultPane.ListControl.SelectionNameProperty].Text);
						}
					}
				}
				base.UpdateSelection(dependedResultPane.SelectedObjects, text, null);
				return;
			}
			base.UpdateSelection(null, " ", null);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0004C31C File Offset: 0x0004A51C
		public void SelectResultPaneByName(string resultPaneName)
		{
			Predicate<AbstractResultPane> selectionCondition = (AbstractResultPane resultPane) => string.Compare(resultPane.Name, resultPaneName, StringComparison.InvariantCultureIgnoreCase) == 0;
			this.SelectResultPane(selectionCondition);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0004C34C File Offset: 0x0004A54C
		public bool SelectResultPane(Predicate<AbstractResultPane> selectionCondition)
		{
			foreach (AbstractResultPane abstractResultPane in this.ResultPanes)
			{
				if (selectionCondition(abstractResultPane))
				{
					this.SelectedResultPane = abstractResultPane;
					return true;
				}
				ContainerResultPane containerResultPane = abstractResultPane as ContainerResultPane;
				if (containerResultPane != null && containerResultPane.SelectResultPane(selectionCondition))
				{
					this.SelectedResultPane = containerResultPane;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0004C3C8 File Offset: 0x0004A5C8
		object IServiceProvider.GetService(Type serviceType)
		{
			if (this.Site != null)
			{
				return this.Site.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0004C3E3 File Offset: 0x0004A5E3
		private List<AbstractResultPane> GetEnabledDependedResultPanesInContainer(AbstractResultPane resultPane)
		{
			return this.GetEnabledDependedResultPanesInContainer(resultPane, (AbstractResultPane param0) => true);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0004C40F File Offset: 0x0004A60F
		private List<AbstractResultPane> GetEnabledDependedResultPanesInContainer(AbstractResultPane resultPane, Predicate<AbstractResultPane> resultCondition)
		{
			return this.GetEnabledDependedResultPanesInContainer(resultPane, resultCondition, (AbstractResultPane dependedResultPane) => dependedResultPane == null);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0004C438 File Offset: 0x0004A638
		private List<AbstractResultPane> GetEnabledDependedResultPanesInContainer(AbstractResultPane resultPane, Predicate<AbstractResultPane> resultCondition, Predicate<AbstractResultPane> stopCondition)
		{
			List<AbstractResultPane> list = new List<AbstractResultPane>();
			ResultPane dependedResultPane = resultPane.DependedResultPane;
			while (dependedResultPane != null && !stopCondition(dependedResultPane))
			{
				if (this.ResultPanes.Contains(dependedResultPane) && dependedResultPane.Enabled && resultCondition(dependedResultPane))
				{
					list.Insert(0, dependedResultPane);
				}
				dependedResultPane = dependedResultPane.DependedResultPane;
			}
			return list;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0004C492 File Offset: 0x0004A692
		private AbstractResultPane GetTopEnabledResultPaneInContainer(AbstractResultPane resultPane)
		{
			return this.GetTopEnabledResultPaneInContainer(resultPane, (AbstractResultPane param0) => true);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0004C4BE File Offset: 0x0004A6BE
		private AbstractResultPane GetTopEnabledResultPaneInContainer(AbstractResultPane resultPane, Predicate<AbstractResultPane> resultCondition)
		{
			return this.GetTopEnabledResultPaneInContainer(resultPane, resultCondition, (AbstractResultPane dependedResultPane) => dependedResultPane == null);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0004C4E8 File Offset: 0x0004A6E8
		private AbstractResultPane GetTopEnabledResultPaneInContainer(AbstractResultPane resultPane, Predicate<AbstractResultPane> resultCondition, Predicate<AbstractResultPane> stopCondition)
		{
			AbstractResultPane result = resultPane;
			ResultPane dependedResultPane = resultPane.DependedResultPane;
			while (dependedResultPane != null && !stopCondition(dependedResultPane))
			{
				if (this.ResultPanes.Contains(dependedResultPane) && dependedResultPane.Enabled && resultCondition(dependedResultPane))
				{
					result = dependedResultPane;
				}
				dependedResultPane = dependedResultPane.DependedResultPane;
			}
			return result;
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0004C538 File Offset: 0x0004A738
		private List<AbstractResultPane> GetEnabledDependentResultPanesInContainer(ResultPane resultPane)
		{
			return this.GetEnabledDependentResultPanesInContainer(resultPane, (AbstractResultPane param0) => true);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0004C560 File Offset: 0x0004A760
		private List<AbstractResultPane> GetEnabledDependentResultPanesInContainer(ResultPane resultPane, Predicate<AbstractResultPane> resultCondition)
		{
			List<AbstractResultPane> list = new List<AbstractResultPane>();
			List<AbstractResultPane> list2 = new List<AbstractResultPane>();
			list2.Add(resultPane);
			while (list2.Count > 0)
			{
				AbstractResultPane abstractResultPane = list2[0];
				list2.RemoveAt(0);
				if (abstractResultPane is ResultPane)
				{
					foreach (AbstractResultPane abstractResultPane2 in (abstractResultPane as ResultPane).DependentResultPanes)
					{
						if (this.ResultPanes.Contains(abstractResultPane2) && abstractResultPane2.Enabled && resultCondition(abstractResultPane2))
						{
							list.Add(abstractResultPane2);
						}
						list2.Add(abstractResultPane2);
					}
				}
			}
			return list;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0004C617 File Offset: 0x0004A817
		private List<AbstractResultPane> GetDirectEnabledDependentResultPanesInContainer(ResultPane resultPane)
		{
			return this.GetDirectEnabledDependentResultPanesInContainer(resultPane, (AbstractResultPane param0) => true);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0004C640 File Offset: 0x0004A840
		private List<AbstractResultPane> GetDirectEnabledDependentResultPanesInContainer(ResultPane resultPane, Predicate<AbstractResultPane> resultCondition)
		{
			List<AbstractResultPane> list = new List<AbstractResultPane>();
			foreach (AbstractResultPane abstractResultPane in resultPane.DependentResultPanes)
			{
				if (this.ResultPanes.Contains(abstractResultPane) && abstractResultPane.Enabled && resultCondition(abstractResultPane))
				{
					list.Add(abstractResultPane);
				}
			}
			return list;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0004C6B4 File Offset: 0x0004A8B4
		private static void BindCommandsOfSelectedResultPaneToCommandsOfContainer(CommandCollection commandsOfSelectedResultPane, CommandCollection commandsOfContainer, CommandEventHandler commandAddedHandler, CommandEventHandler commandRemovedHandler)
		{
			commandsOfContainer.InsertRange(0, commandsOfSelectedResultPane);
			commandsOfSelectedResultPane.CommandAdded += commandAddedHandler;
			commandsOfSelectedResultPane.CommandRemoved += commandRemovedHandler;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0004C6CC File Offset: 0x0004A8CC
		private static void UnbindCommandsOfSelectedResultPaneFromCommandsOfContainer(CommandCollection commandsOfSelectedResultPane, CommandCollection commandsOfContainer, CommandEventHandler commandAddedHandler, CommandEventHandler commandRemovedHandler)
		{
			commandsOfSelectedResultPane.CommandAdded -= commandAddedHandler;
			commandsOfSelectedResultPane.CommandRemoved -= commandRemovedHandler;
			if (commandsOfSelectedResultPane.Count > 0)
			{
				int num = commandsOfContainer.IndexOf(commandsOfSelectedResultPane[0]);
				if (num >= 0)
				{
					commandsOfContainer.RemoveRange(num, commandsOfSelectedResultPane.Count);
				}
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0004C710 File Offset: 0x0004A910
		private static void CommandsOfSelectedResultPane_CommandAdded(CommandCollection commandsOfSelectedResultPane, CommandCollection commandsOfContainer, CommandEventArgs e)
		{
			if (commandsOfSelectedResultPane.Count > 1)
			{
				int num = commandsOfContainer.IndexOf(commandsOfSelectedResultPane[0]);
				int num2 = commandsOfSelectedResultPane.IndexOf(e.Command);
				commandsOfContainer.Insert(num + num2, e.Command);
				return;
			}
			commandsOfContainer.Insert(0, e.Command);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0004C760 File Offset: 0x0004A960
		private AbstractResultPane FindResultPaneWithResultPaneCommandsInContainer(CommandCollection resultPaneCommands)
		{
			foreach (AbstractResultPane abstractResultPane in this.ResultPanes)
			{
				if (abstractResultPane.ResultPaneCommands == resultPaneCommands)
				{
					return abstractResultPane;
				}
			}
			return null;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0004C7B8 File Offset: 0x0004A9B8
		private static void BindResultPaneCommandsToCommands(CommandCollection targetCommands, IList<AbstractResultPane> sourceResultPanes, AbstractResultPane resultPane, CommandEventHandler commandAddedEventHandler, CommandEventHandler commandRemovedEventHandler)
		{
			targetCommands.InsertRange(ContainerResultPane.FindInsertionPostionForResultPaneCommands(targetCommands, sourceResultPanes, resultPane), resultPane.ResultPaneCommands);
			resultPane.ResultPaneCommands.CommandAdded += commandAddedEventHandler;
			resultPane.ResultPaneCommands.CommandRemoved += commandRemovedEventHandler;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0004C7E8 File Offset: 0x0004A9E8
		private static void UnbindResultPaneCommandsFromCommands(CommandCollection targetCommands, IList<AbstractResultPane> sourceResultPanes, AbstractResultPane resultPane, CommandEventHandler commandAddedEventHandler, CommandEventHandler commandRemovedEventHandler)
		{
			resultPane.ResultPaneCommands.CommandAdded -= commandAddedEventHandler;
			resultPane.ResultPaneCommands.CommandRemoved -= commandRemovedEventHandler;
			foreach (object obj in resultPane.ResultPaneCommands)
			{
				Command command = (Command)obj;
				targetCommands.Remove(command);
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0004C85C File Offset: 0x0004AA5C
		private static void AddResultPaneCommandToCommands(CommandCollection targetCommands, IList<AbstractResultPane> sourceResultPanes, AbstractResultPane resultPane, Command resultPaneCommand)
		{
			int num = ContainerResultPane.FindInsertionPostionForResultPaneCommands(targetCommands, sourceResultPanes, resultPane) + resultPane.ResultPaneCommands.IndexOf(resultPaneCommand);
			targetCommands.Insert(num, resultPaneCommand);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0004C887 File Offset: 0x0004AA87
		private static void RemoveResultPaneCommandFromCommands(CommandCollection targetCommands, IList<AbstractResultPane> sourceResultPanes, AbstractResultPane resultPane, Command resultPaneCommand)
		{
			targetCommands.Remove(resultPaneCommand);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0004C890 File Offset: 0x0004AA90
		private static int FindInsertionPostionForResultPaneCommands(CommandCollection targetCommands, IList<AbstractResultPane> sourceResultPanes, AbstractResultPane resultPane)
		{
			int result = 0;
			int num = sourceResultPanes.IndexOf(resultPane);
			for (int i = num - 1; i >= 0; i--)
			{
				AbstractResultPane abstractResultPane = sourceResultPanes[i];
				if (abstractResultPane.ResultPaneCommands.Count > 0 && targetCommands.Contains(abstractResultPane.ResultPaneCommands[0]))
				{
					result = targetCommands.IndexOf(abstractResultPane.ResultPaneCommands[0]) + abstractResultPane.ResultPaneCommands.Count;
					break;
				}
			}
			return result;
		}

		// Token: 0x040006FE RID: 1790
		private ServicedContainer components;

		// Token: 0x040006FF RID: 1791
		private ChangeNotifyingCollection<AbstractResultPane> resultPanes = new ChangeNotifyingCollection<AbstractResultPane>();

		// Token: 0x04000700 RID: 1792
		private AbstractResultPane selectedResultPane;

		// Token: 0x04000701 RID: 1793
		private static readonly object EventSelectedResultPaneChanged = new object();

		// Token: 0x04000702 RID: 1794
		private ChangeNotifyingCollection<AbstractResultPane> resultPanesActiveToContainer = new ChangeNotifyingCollection<AbstractResultPane>();

		// Token: 0x04000703 RID: 1795
		private Dictionary<CommandCollection, Command> ownerSelectionCommandsInContainer = new Dictionary<CommandCollection, Command>();

		// Token: 0x04000704 RID: 1796
		private bool isInSetActive;

		// Token: 0x04000705 RID: 1797
		private bool isInKillingActive;
	}
}
