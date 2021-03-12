using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.Exchange.Sqm;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000E0 RID: 224
	[Designer(typeof(ScrollableControlDesigner))]
	public class ResultPane : AbstractResultPane
	{
		// Token: 0x06000885 RID: 2181 RVA: 0x0001B46C File Offset: 0x0001966C
		public ResultPane() : this(null, null)
		{
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001B476 File Offset: 0x00019676
		public ResultPane(IResultsLoaderConfiguration config) : this((config != null) ? config.BuildResultsLoaderProfile() : null, null)
		{
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001B48B File Offset: 0x0001968B
		public ResultPane(DataTableLoader loader) : this((loader != null) ? loader.ResultsLoaderProfile : null, loader)
		{
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001B4A0 File Offset: 0x000196A0
		public ResultPane(ObjectPickerProfileLoader profileLoader, string profileName) : this(profileLoader.GetProfile(profileName))
		{
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001B4AF File Offset: 0x000196AF
		public ResultPane(ResultsLoaderProfile profile) : this(profile, null)
		{
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001B500 File Offset: 0x00019700
		protected ResultPane(ResultsLoaderProfile profile, DataTableLoader loader)
		{
			base.SuspendLayout();
			this.warningCaption = new AutoHeightLabel();
			this.warningCaption.BackColor = SystemColors.Info;
			this.warningCaption.ForeColor = SystemColors.InfoText;
			this.warningCaption.Image = IconLibrary.ToSmallBitmap(Icons.Warning);
			this.warningCaption.Dock = DockStyle.Top;
			this.warningCaption.Name = "warningCaption";
			this.warningCaption.ImageAlign = ContentAlignment.MiddleLeft;
			this.warningCaption.TextAlign = ContentAlignment.MiddleLeft;
			this.warningCaption.Padding = new Padding(20, 2, 2, 2);
			this.warningCaption.Visible = false;
			base.Controls.Add(this.warningCaption);
			base.Name = "ResultPane";
			base.ResumeLayout(false);
			Command command = Command.CreateSeparator();
			command.Visible = false;
			this.dependentResultPanesCommandsSeparator = Command.CreateSeparator();
			this.dependentResultPanesCommandsSeparator.Visible = false;
			this.deleteSelectionCommandsSeparator = Command.CreateSeparator();
			this.deleteSelectionCommandsSeparator.Visible = false;
			this.showSelectionPropertiesCommandsSeparator = Command.CreateSeparator();
			this.showSelectionPropertiesCommandsSeparator.Visible = false;
			Command command2 = Command.CreateSeparator();
			command2.Visible = false;
			this.helpCommandSeparator = Command.CreateSeparator();
			this.helpCommandSeparator.Visible = false;
			this.warningCaption.MouseEnter += delegate(object param0, EventArgs param1)
			{
				this.warningCaption.ForeColor = SystemColors.HighlightText;
				this.warningCaption.BackColor = SystemColors.Highlight;
			};
			this.warningCaption.MouseLeave += delegate(object param0, EventArgs param1)
			{
				this.warningCaption.ForeColor = SystemColors.InfoText;
				this.warningCaption.BackColor = SystemColors.Info;
			};
			this.warningCaption.Click += this.warningCaption_Click;
			this.DependentResultPanes.ListChanging += this.DependentResultPanes_ListChanging;
			this.DependentResultPanes.ListChanged += this.DependentResultPanes_ListChanged;
			this.CustomSelectionCommands.CommandAdded += new CommandEventHandler(this.CustomSelectionCommands_CommandAdded);
			this.CustomSelectionCommands.CommandRemoved += new CommandEventHandler(this.CustomSelectionCommands_CommandRemoved);
			this.DependentResultPaneCommands.CommandAdded += new CommandEventHandler(this.DependentResultPaneCommands_CommandAdded);
			this.DependentResultPaneCommands.CommandRemoved += new CommandEventHandler(this.DependentResultPaneCommands_CommandRemoved);
			this.DeleteSelectionCommands.CommandAdded += new CommandEventHandler(this.DeleteSelectionCommands_CommandAdded);
			this.DeleteSelectionCommands.CommandRemoved += new CommandEventHandler(this.DeleteSelectionCommands_CommandRemoved);
			this.ShowSelectionPropertiesCommands.CommandAdded += new CommandEventHandler(this.ShowSelectionPropertiesCommands_CommandAdded);
			this.ShowSelectionPropertiesCommands.CommandRemoved += new CommandEventHandler(this.ShowSelectionPropertiesCommands_CommandRemoved);
			base.SelectionCommands.AddRange(new Command[]
			{
				this.dependentResultPanesCommandsSeparator,
				this.deleteSelectionCommandsSeparator,
				this.showSelectionPropertiesCommandsSeparator
			});
			base.ResultPaneCommands.CommandAdded += new CommandEventHandler(this.ResultPaneCommands_CommandAdded);
			base.ResultPaneCommands.CommandRemoved += new CommandEventHandler(this.ResultPaneCommands_CommandRemoved);
			base.ExportListCommands.CommandAdded += new CommandEventHandler(this.ExportListCommands_CommandAdded);
			base.ExportListCommands.CommandRemoved += new CommandEventHandler(this.ExportListCommands_CommandRemoved);
			this.viewCommand = new Command();
			this.viewCommand.Text = Strings.ViewCommands;
			this.viewCommand.Visible = false;
			this.viewCommand.Name = "resultPaneViewCommand";
			this.viewCommand.Icon = Icons.View;
			base.ViewModeCommands.CommandAdded += new CommandEventHandler(this.ViewModeCommands_CommandAdded);
			base.ViewModeCommands.CommandRemoved += new CommandEventHandler(this.ViewModeCommands_CommandRemoved);
			this.WhitespaceCommands.AddRange(new Command[]
			{
				command,
				this.viewCommand,
				command2,
				base.RefreshCommand
			});
			this.ResultsLoaderProfile = profile;
			this.RefreshableDataSource = loader;
			this.SetupCommandsProfile();
			this.SyncCommands(this.CommandsProfile.ResultPaneCommands, base.ResultPaneCommands);
			this.SyncCommands(this.CommandsProfile.CustomSelectionCommands, this.CustomSelectionCommands);
			this.SyncCommands(this.CommandsProfile.DeleteSelectionCommands, this.DeleteSelectionCommands);
			this.SubscribedRefreshCategories.Add(ResultPane.ConfigurationDomainControllerRefreshCategory);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001B98C File Offset: 0x00019B8C
		private void SyncCommands(List<ResultsCommandProfile> profiles, CommandCollection commands)
		{
			foreach (ResultsCommandProfile resultsCommandProfile in profiles)
			{
				if (resultsCommandProfile.HasPermission())
				{
					resultsCommandProfile.ResultPane = this;
					commands.Add(resultsCommandProfile.Command);
					if (!resultsCommandProfile.IsSeparator)
					{
						base.Components.Add(resultsCommandProfile);
					}
				}
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0001BA04 File Offset: 0x00019C04
		public override string SelectionHelpTopic
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001BA0B File Offset: 0x00019C0B
		public override bool HasPermission()
		{
			return this.ResultsLoaderProfile == null || this.ResultsLoaderProfile.HasPermission();
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001BA24 File Offset: 0x00019C24
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				base.ResultPaneCommands.CommandAdded -= new CommandEventHandler(this.ResultPaneCommands_CommandAdded);
				base.ResultPaneCommands.CommandRemoved -= new CommandEventHandler(this.ResultPaneCommands_CommandRemoved);
				base.ExportListCommands.CommandAdded -= new CommandEventHandler(this.ExportListCommands_CommandAdded);
				base.ExportListCommands.CommandRemoved -= new CommandEventHandler(this.ExportListCommands_CommandRemoved);
				base.ViewModeCommands.CommandAdded -= new CommandEventHandler(this.ViewModeCommands_CommandAdded);
				base.ViewModeCommands.CommandRemoved -= new CommandEventHandler(this.ViewModeCommands_CommandRemoved);
				this.WhitespaceCommands.Clear();
				this.viewCommand.Commands.Clear();
				this.CustomSelectionCommands.Clear();
				this.DependentResultPaneCommands.Clear();
				this.DeleteSelectionCommands.Clear();
				this.ShowSelectionPropertiesCommands.Clear();
				this.CustomSelectionCommands.CommandAdded -= new CommandEventHandler(this.CustomSelectionCommands_CommandAdded);
				this.CustomSelectionCommands.CommandRemoved -= new CommandEventHandler(this.CustomSelectionCommands_CommandRemoved);
				this.DependentResultPaneCommands.CommandAdded -= new CommandEventHandler(this.DependentResultPaneCommands_CommandAdded);
				this.DependentResultPaneCommands.CommandRemoved -= new CommandEventHandler(this.DependentResultPaneCommands_CommandRemoved);
				this.DeleteSelectionCommands.CommandAdded -= new CommandEventHandler(this.DeleteSelectionCommands_CommandAdded);
				this.DeleteSelectionCommands.CommandRemoved -= new CommandEventHandler(this.DeleteSelectionCommands_CommandRemoved);
				this.ShowSelectionPropertiesCommands.CommandAdded -= new CommandEventHandler(this.ShowSelectionPropertiesCommands_CommandAdded);
				this.ShowSelectionPropertiesCommands.CommandRemoved -= new CommandEventHandler(this.ShowSelectionPropertiesCommands_CommandRemoved);
				this.DependentResultPanes.ListChanging -= this.DependentResultPanes_ListChanging;
				this.DependentResultPanes.ListChanged -= this.DependentResultPanes_ListChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001BBF5 File Offset: 0x00019DF5
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			base.Controls.SetChildIndex(this.warningCaption, 1);
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0001BC10 File Offset: 0x00019E10
		private bool IsSingleMoreResultsWarning
		{
			get
			{
				return this.workUnitsForWarnings.Count == 1 && this.workUnitsForWarnings[0].Warnings.Count == 1 && 0 == string.Compare(this.workUnitsForWarnings[0].Warnings[0], ResultPane.MoreItemsWarning);
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001BC6A File Offset: 0x00019E6A
		private void warningCaption_Click(object sender, EventArgs e)
		{
			this.HideWarningCaption();
			if (!this.IsSingleMoreResultsWarning)
			{
				UIService.ShowError("", Strings.Warnings, this.workUnitsForWarnings, base.ShellUI);
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001BC9B File Offset: 0x00019E9B
		protected void HideWarningCaption()
		{
			this.warningCaption.Visible = false;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001BCA9 File Offset: 0x00019EA9
		public ChangeNotifyingCollection<AbstractResultPane> DependentResultPanes
		{
			get
			{
				return this.dependentResultPanes;
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001BCB4 File Offset: 0x00019EB4
		private void DependentResultPanes_ListChanging(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				this.RemovingDependentResultPaneAt(e.NewIndex);
				return;
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				for (int i = this.DependentResultPanes.Count - 1; i >= 0; i--)
				{
					this.RemovingDependentResultPaneAt(i);
				}
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001BCFE File Offset: 0x00019EFE
		private void DependentResultPanes_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				this.InsertedDependentResultPaneAt(e.NewIndex);
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001BD18 File Offset: 0x00019F18
		private void InsertedDependentResultPaneAt(int index)
		{
			AbstractResultPane abstractResultPane = this.DependentResultPanes[index];
			if (abstractResultPane == null)
			{
				throw new InvalidOperationException("Cannot add null to ResultPane.DependentResultPanes");
			}
			if (abstractResultPane.DependedResultPane != null)
			{
				throw new InvalidOperationException("the result pane has been added to DependentResultPanes of another result pane.");
			}
			abstractResultPane.DependedResultPane = this;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001BD5C File Offset: 0x00019F5C
		private void RemovingDependentResultPaneAt(int index)
		{
			AbstractResultPane abstractResultPane = this.DependentResultPanes[index];
			abstractResultPane.DependedResultPane = null;
			abstractResultPane.Enabled = true;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001BD84 File Offset: 0x00019F84
		public bool IsDependedResultPane(AbstractResultPane resultPane)
		{
			bool result = false;
			if (resultPane != null)
			{
				for (ResultPane dependedResultPane = resultPane.DependedResultPane; dependedResultPane != null; dependedResultPane = dependedResultPane.DependedResultPane)
				{
					if (dependedResultPane == this)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001BDB2 File Offset: 0x00019FB2
		public static bool IsDependedResultPane(AbstractResultPane firstResultPane, AbstractResultPane secondResultPane)
		{
			return firstResultPane is ResultPane && (firstResultPane as ResultPane).IsDependedResultPane(secondResultPane);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001BDCA File Offset: 0x00019FCA
		protected override void OnSharedSettingsChanging()
		{
			if (base.SharedSettings != null)
			{
				base.SharedSettings.RefreshResultPane -= new CustomDataRefreshEventHandler(this.SharedSettings_RefreshResultPane);
			}
			base.OnSharedSettingsChanging();
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001BDF1 File Offset: 0x00019FF1
		protected override void OnSharedSettingsChanged()
		{
			if (base.SharedSettings != null)
			{
				base.SharedSettings.RefreshResultPane += new CustomDataRefreshEventHandler(this.SharedSettings_RefreshResultPane);
			}
			base.OnSharedSettingsChanged();
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001BE18 File Offset: 0x0001A018
		public CommandCollection CustomSelectionCommands
		{
			get
			{
				return this.customSelectionCommands;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001BE20 File Offset: 0x0001A020
		private void CustomSelectionCommands_CommandAdded(object sender, CommandEventArgs e)
		{
			base.SelectionCommands.Insert(this.CustomSelectionCommands.IndexOf(e.Command), e.Command);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001BE44 File Offset: 0x0001A044
		private void CustomSelectionCommands_CommandRemoved(object sender, CommandEventArgs e)
		{
			base.SelectionCommands.Remove(e.Command);
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001BE57 File Offset: 0x0001A057
		internal CommandCollection DependentResultPaneCommands
		{
			get
			{
				return this.dependentResultPaneCommands;
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001BE5F File Offset: 0x0001A05F
		private void DependentResultPaneCommands_CommandAdded(object sender, CommandEventArgs e)
		{
			base.SelectionCommands.Insert(base.SelectionCommands.IndexOf(this.dependentResultPanesCommandsSeparator) + 1 + this.DependentResultPaneCommands.IndexOf(e.Command), e.Command);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001BE97 File Offset: 0x0001A097
		private void DependentResultPaneCommands_CommandRemoved(object sender, CommandEventArgs e)
		{
			base.SelectionCommands.Remove(e.Command);
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001BEAA File Offset: 0x0001A0AA
		public CommandCollection DeleteSelectionCommands
		{
			get
			{
				return this.deleteSelectionCommands;
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001BEB2 File Offset: 0x0001A0B2
		private void DeleteSelectionCommands_CommandAdded(object sender, CommandEventArgs e)
		{
			base.SelectionCommands.Insert(base.SelectionCommands.IndexOf(this.deleteSelectionCommandsSeparator) + 1 + this.DeleteSelectionCommands.IndexOf(e.Command), e.Command);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001BEEA File Offset: 0x0001A0EA
		private void DeleteSelectionCommands_CommandRemoved(object sender, CommandEventArgs e)
		{
			base.SelectionCommands.Remove(e.Command);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001BF00 File Offset: 0x0001A100
		public void InvokeCurrentDeleteSelectionCommand()
		{
			foreach (object obj in this.DeleteSelectionCommands)
			{
				Command command = (Command)obj;
				if (!command.IsSeparator && command.Visible)
				{
					command.Invoke();
					return;
				}
			}
			foreach (object obj2 in this.CustomSelectionCommands)
			{
				Command command2 = (Command)obj2;
				ResultsCommandProfile profile = this.CommandsProfile.GetProfile(command2);
				if (!command2.IsSeparator && command2.Visible && profile != null && profile.Setting != null && profile.Setting.IsRemoveCommand)
				{
					command2.Invoke();
					break;
				}
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0001BFF8 File Offset: 0x0001A1F8
		public CommandCollection ShowSelectionPropertiesCommands
		{
			get
			{
				return this.showSelectionPropertiesCommands;
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001C000 File Offset: 0x0001A200
		private void ShowSelectionPropertiesCommands_CommandAdded(object sender, CommandEventArgs e)
		{
			base.SelectionCommands.Insert(base.SelectionCommands.IndexOf(this.showSelectionPropertiesCommandsSeparator) + 1 + this.ShowSelectionPropertiesCommands.IndexOf(e.Command), e.Command);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001C038 File Offset: 0x0001A238
		private void ShowSelectionPropertiesCommands_CommandRemoved(object sender, CommandEventArgs e)
		{
			base.SelectionCommands.Remove(e.Command);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001C04C File Offset: 0x0001A24C
		public void InvokeCurrentShowSelectionPropertiesCommand()
		{
			foreach (object obj in this.ShowSelectionPropertiesCommands)
			{
				Command command = (Command)obj;
				if (!command.IsSeparator && command.Visible)
				{
					command.Invoke();
					return;
				}
			}
			foreach (object obj2 in this.CustomSelectionCommands)
			{
				Command command2 = (Command)obj2;
				ResultsCommandProfile profile = this.CommandsProfile.GetProfile(command2);
				if (!command2.IsSeparator && command2.Visible && profile != null && profile.Setting != null && profile.Setting.IsPropertiesCommand)
				{
					command2.Invoke();
					break;
				}
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001C144 File Offset: 0x0001A344
		public virtual IRefreshable GetSelectionRefreshObjects()
		{
			return null;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0001C147 File Offset: 0x0001A347
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0001C14F File Offset: 0x0001A34F
		public ResultsLoaderProfile ResultsLoaderProfile
		{
			get
			{
				return this.resultsLoaderProfile;
			}
			private set
			{
				if (this.ResultsLoaderProfile != value)
				{
					this.resultsLoaderProfile = value;
					if (this.ResultsLoaderProfile != null)
					{
						this.Text = this.ResultsLoaderProfile.DisplayName;
					}
				}
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001C17A File Offset: 0x0001A37A
		public ResultCommandsProfile CommandsProfile
		{
			get
			{
				if (this.ResultsLoaderProfile == null)
				{
					return this.commandsProfile;
				}
				return this.ResultsLoaderProfile.CommandsProfile;
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001C196 File Offset: 0x0001A396
		protected virtual void SetupCommandsProfile()
		{
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001C198 File Offset: 0x0001A398
		public DataTableLoader DataTableLoader
		{
			get
			{
				return this.RefreshableDataSource as DataTableLoader;
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001C1A5 File Offset: 0x0001A3A5
		protected override void OnRefreshableDataSourceChanging(EventArgs e)
		{
			if (this.DataTableLoader != null)
			{
				this.DataTableLoader.RefreshCompleted -= this.RefreshableDataSource_RefreshCompleted;
			}
			base.OnRefreshableDataSourceChanging(e);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001C1CD File Offset: 0x0001A3CD
		protected override void OnRefreshableDataSourceChanged(EventArgs e)
		{
			if (this.DataTableLoader != null)
			{
				this.DataTableLoader.RefreshCompleted += this.RefreshableDataSource_RefreshCompleted;
			}
			base.OnRefreshableDataSourceChanged(e);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
		private void RefreshableDataSource_RefreshCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.workUnitsForWarnings = ((DataTableLoader)sender).WorkUnits.FindByErrorOrWarning();
			if (this.warningCaption.Visible = (this.workUnitsForWarnings.Count > 0))
			{
				this.warningCaption.Text = (this.IsSingleMoreResultsWarning ? this.workUnitsForWarnings[0].Warnings[0] : Strings.WarningNotification);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0001C26F File Offset: 0x0001A46F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CommandCollection WhitespaceCommands
		{
			get
			{
				return this.whitespaceCommands;
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001C278 File Offset: 0x0001A478
		private void ViewModeCommands_CommandAdded(object sender, CommandEventArgs e)
		{
			this.viewCommand.Commands.Insert(base.ViewModeCommands.IndexOf(e.Command), e.Command);
			e.Command.VisibleChanged += this.ViewModeCommand_VisibleChanged;
			this.ViewModeCommand_VisibleChanged(e.Command, EventArgs.Empty);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		private void ViewModeCommands_CommandRemoved(object sender, CommandEventArgs e)
		{
			this.viewCommand.Commands.Remove(e.Command);
			e.Command.VisibleChanged -= this.ViewModeCommand_VisibleChanged;
			this.ViewModeCommand_VisibleChanged(e.Command, EventArgs.Empty);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001C314 File Offset: 0x0001A514
		private void ViewModeCommand_VisibleChanged(object sender, EventArgs e)
		{
			this.viewCommand.Visible = base.ViewModeCommands.HasVisibleCommandsUpToIndex(base.ViewModeCommands.Count);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001C337 File Offset: 0x0001A537
		private void ResultPaneCommands_CommandAdded(object sender, CommandEventArgs e)
		{
			this.WhitespaceCommands.Insert(base.ResultPaneCommands.IndexOf(e.Command), e.Command);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001C35B File Offset: 0x0001A55B
		private void ResultPaneCommands_CommandRemoved(object sender, CommandEventArgs e)
		{
			this.WhitespaceCommands.Remove(e.Command);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001C36E File Offset: 0x0001A56E
		private void ExportListCommands_CommandAdded(object sender, CommandEventArgs e)
		{
			this.WhitespaceCommands.Insert(base.ResultPaneCommands.Count + base.ExportListCommands.IndexOf(e.Command), e.Command);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001C39E File Offset: 0x0001A59E
		private void ExportListCommands_CommandRemoved(object sender, CommandEventArgs e)
		{
			this.WhitespaceCommands.Remove(e.Command);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001C3B4 File Offset: 0x0001A5B4
		protected override void UpdateContextMenuCommands()
		{
			base.UpdateContextMenuCommands();
			if (base.HasSelection)
			{
				base.ContextMenuCommands.AddRange(base.SelectionCommands);
			}
			else
			{
				base.ContextMenuCommands.AddRange(this.WhitespaceCommands);
			}
			base.ContextMenuCommands.Add(this.helpCommandSeparator);
			base.ContextMenuCommands.Add(base.HelpCommand);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001C418 File Offset: 0x0001A618
		protected override void OnSetActive(EventArgs e)
		{
			if (base.IsActivatedFirstly)
			{
				this.SyncCommands(this.CommandsProfile.ShowSelectionPropertiesCommands, this.ShowSelectionPropertiesCommands);
			}
			if (this.RefreshableDataSource == null && this.ResultsLoaderProfile != null)
			{
				this.RefreshableDataSource = new DataTableLoader(this.ResultsLoaderProfile);
			}
			bool flag = this.RefreshableDataSource != null && this.RefreshableDataSource.Refreshed;
			if (!flag || this.refreshWhenActivated)
			{
				this.DoFullRefreshWhenActivated();
				this.refreshWhenActivated = false;
			}
			else if (this.partialRefreshRequests.Count > 0)
			{
				this.ProcessPartialRefreshRequests();
			}
			base.OnSetActive(e);
			if (ManagementGuiSqmSession.Instance.Enabled)
			{
				ManagementGuiSqmSession.Instance.AddToStreamDataPoint(SqmDataID.DATAID_EMC_GUI_ACTION, new object[]
				{
					1U,
					base.Name
				});
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001C4E6 File Offset: 0x0001A6E6
		public ArrayList SubscribedRefreshCategories
		{
			get
			{
				return this.subscribedRefreshCategories;
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001C4F0 File Offset: 0x0001A6F0
		private void SharedSettings_RefreshResultPane(object sender, CustomDataRefreshEventArgs e)
		{
			if (!this.refreshWhenActivated)
			{
				bool flag = false;
				foreach (object obj in e.RefreshArguments)
				{
					if (obj is PartialRefreshRequest)
					{
						PartialRefreshRequest partialRefreshRequest = (PartialRefreshRequest)obj;
						if (!this.partialRefreshRequests.Contains(partialRefreshRequest) && this.SubscribedRefreshCategories.Contains(partialRefreshRequest.RefreshCategory))
						{
							this.partialRefreshRequests.Add(partialRefreshRequest);
						}
					}
					else if (this.SubscribedRefreshCategories.Contains(obj))
					{
						flag = true;
						break;
					}
				}
				if (flag || this.partialRefreshRequests.Count > 5)
				{
					this.SetRefreshWhenActivated();
					return;
				}
				if (this.partialRefreshRequests.Count > 0)
				{
					this.ProcessPartialRefreshRequests();
				}
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001C5CC File Offset: 0x0001A7CC
		public void SetRefreshWhenActivated()
		{
			if (base.IsActive)
			{
				this.DoFullRefreshWhenActivated();
			}
			else if (this.RefreshableDataSource != null && this.RefreshableDataSource.Refreshed)
			{
				this.refreshWhenActivated = true;
			}
			this.partialRefreshRequests.Clear();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001C605 File Offset: 0x0001A805
		protected virtual void DoFullRefreshWhenActivated()
		{
			this.DelayInvokeRefreshCommand();
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001C624 File Offset: 0x0001A824
		private void DelayInvokeRefreshCommand()
		{
			if (base.IsHandleCreated)
			{
				if (!this.refreshCommandInvokeScheduled)
				{
					this.refreshCommandInvokeScheduled = true;
					base.BeginInvoke(new MethodInvoker(delegate()
					{
						this.refreshCommandInvokeScheduled = false;
						base.RefreshCommand.Invoke();
					}));
					return;
				}
			}
			else
			{
				base.RefreshCommand.Invoke();
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001C670 File Offset: 0x0001A870
		private void ProcessPartialRefreshRequests()
		{
			if (this.partialRefreshRequests.Count > 0 && base.IsActive)
			{
				foreach (PartialRefreshRequest partialRefreshRequest in this.partialRefreshRequests)
				{
					partialRefreshRequest.DoRefresh(this.RefreshableDataSource, base.CreateProgress(base.RefreshCommand.Text));
				}
				this.partialRefreshRequests.Clear();
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001C6FC File Offset: 0x0001A8FC
		public IRefreshable CreateRefreshableObject(params object[] refreshArguments)
		{
			if (refreshArguments == null || refreshArguments.Length == 0)
			{
				throw new ArgumentNullException("refreshArguments");
			}
			return new ResultPane.RefreshableObject(this, new CustomDataRefreshEventArgs(refreshArguments));
		}

		// Token: 0x040003CC RID: 972
		internal const int PartialRefreshRequestThreshold = 5;

		// Token: 0x040003CD RID: 973
		public static readonly object ConfigurationDomainControllerRefreshCategory = new object();

		// Token: 0x040003CE RID: 974
		private AutoHeightLabel warningCaption;

		// Token: 0x040003CF RID: 975
		private IList<WorkUnit> workUnitsForWarnings;

		// Token: 0x040003D0 RID: 976
		private static string MoreItemsWarning = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Configuration.Common.LocStrings.Strings", typeof(WorkUnit).Assembly).GetString("WarningMoreResultsAvailable");

		// Token: 0x040003D1 RID: 977
		private ChangeNotifyingCollection<AbstractResultPane> dependentResultPanes = new ChangeNotifyingCollection<AbstractResultPane>();

		// Token: 0x040003D2 RID: 978
		private CommandCollection customSelectionCommands = new CommandCollection();

		// Token: 0x040003D3 RID: 979
		private Command dependentResultPanesCommandsSeparator;

		// Token: 0x040003D4 RID: 980
		private CommandCollection dependentResultPaneCommands = new CommandCollection();

		// Token: 0x040003D5 RID: 981
		private Command deleteSelectionCommandsSeparator;

		// Token: 0x040003D6 RID: 982
		private CommandCollection deleteSelectionCommands = new CommandCollection();

		// Token: 0x040003D7 RID: 983
		private Command showSelectionPropertiesCommandsSeparator;

		// Token: 0x040003D8 RID: 984
		private CommandCollection showSelectionPropertiesCommands = new CommandCollection();

		// Token: 0x040003D9 RID: 985
		private ResultsLoaderProfile resultsLoaderProfile;

		// Token: 0x040003DA RID: 986
		private ResultCommandsProfile commandsProfile = new ResultCommandsProfile();

		// Token: 0x040003DB RID: 987
		private CommandCollection whitespaceCommands = new CommandCollection();

		// Token: 0x040003DC RID: 988
		private Command viewCommand;

		// Token: 0x040003DD RID: 989
		private Command helpCommandSeparator;

		// Token: 0x040003DE RID: 990
		private readonly ArrayList subscribedRefreshCategories = new ArrayList();

		// Token: 0x040003DF RID: 991
		private List<PartialRefreshRequest> partialRefreshRequests = new List<PartialRefreshRequest>();

		// Token: 0x040003E0 RID: 992
		private bool refreshWhenActivated;

		// Token: 0x040003E1 RID: 993
		private bool refreshCommandInvokeScheduled;

		// Token: 0x020000E1 RID: 225
		private class RefreshableObject : IRefreshable
		{
			// Token: 0x060008C8 RID: 2248 RVA: 0x0001C751 File Offset: 0x0001A951
			public RefreshableObject(ResultPane ownerResultPane, CustomDataRefreshEventArgs refreshEventArgs)
			{
				this.ownerResultPane = ownerResultPane;
				this.refreshEventArgs = refreshEventArgs;
			}

			// Token: 0x060008C9 RID: 2249 RVA: 0x0001C767 File Offset: 0x0001A967
			void IRefreshable.Refresh(IProgress progress)
			{
				if (this.ownerResultPane != null && this.ownerResultPane.SharedSettings != null)
				{
					this.ownerResultPane.SharedSettings.RaiseRefreshResultPane(this.refreshEventArgs);
				}
				if (progress != null)
				{
					progress.ReportProgress(100, 100, "");
				}
			}

			// Token: 0x040003E2 RID: 994
			private readonly CustomDataRefreshEventArgs refreshEventArgs;

			// Token: 0x040003E3 RID: 995
			private readonly ResultPane ownerResultPane;
		}
	}
}
