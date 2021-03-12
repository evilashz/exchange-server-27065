using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.WinForms;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000DF RID: 223
	public abstract class AbstractResultPane : ExchangeUserControl, IResultPaneControl, IPersistComponentSettings, IHasPermission
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x0001A474 File Offset: 0x00018674
		protected AbstractResultPane()
		{
			base.Name = "AbstractResultPane";
			base.SuspendLayout();
			base.SetStyle(ControlStyles.ContainerControl, true);
			base.ResumeLayout(false);
			SystemEvents.UserPreferenceChanged += this.SystemEvents_UserPreferenceChanged;
			this.UpdateFontSetting();
			this.SharedSettingsBindingSource = new BindingSource(base.Components);
			this.RegisterCommandsEvent(this.ResultPaneCommands);
			this.RegisterCommandsEvent(this.ExportListCommands);
			this.RegisterCommandsEvent(this.ViewModeCommands);
			this.RegisterCommandsEvent(this.SelectionCommands);
			this.RefreshCommand.Text = Strings.RefreshCommand;
			this.RefreshCommand.Icon = Icons.Refresh;
			this.RefreshCommand.Name = "Refresh";
			this.RefreshCommand.Execute += this.RefreshCommand_Execute;
			this.RefreshCommand.EnabledChanged += delegate(object param0, EventArgs param1)
			{
				this.RefreshCommand.Visible = this.RefreshCommand.Enabled;
			};
			this.RefreshCommand.Enabled = false;
			this.HelpCommand.Name = "Help";
			this.HelpCommand.Icon = Icons.Help;
			this.HelpCommand.Text = Strings.ShowHelpCommand;
			this.HelpCommand.Execute += delegate(object param0, EventArgs param1)
			{
				this.OnHelpRequested(new HelpEventArgs(Point.Empty));
			};
			this.contextMenuLocal = new CommandContextMenu(this.contextMenuCommands);
			this.ContextMenu = this.contextMenuLocal;
			this.ContextMenu.Popup += this.ContextMenu_Popup;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001A674 File Offset: 0x00018874
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this.RefreshableDataSource is DataTableLoader || this.RefreshableDataSource is AggregateDataTableLoadersRefreshableSource)
				{
					base.Components.Remove(this.RefreshableDataSource as IComponent);
				}
				this.ContextMenuCommands.Clear();
				this.ContextMenu = null;
				if (this.contextMenuLocal != null)
				{
					this.contextMenuLocal.Dispose();
					this.contextMenuLocal = null;
				}
				this.RefreshableDataSource = null;
				this.SelectionCommands.Clear();
				this.UnregisterCommandsEvent(this.SelectionCommands);
				this.ViewModeCommands.Clear();
				this.UnregisterCommandsEvent(this.ViewModeCommands);
				this.ExportListCommands.Clear();
				this.UnregisterCommandsEvent(this.ExportListCommands);
				this.ResultPaneCommands.Clear();
				this.UnregisterCommandsEvent(this.ResultPaneCommands);
				this.SharedSettings = null;
				SystemEvents.UserPreferenceChanged -= this.SystemEvents_UserPreferenceChanged;
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001A765 File Offset: 0x00018965
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.SettingsKey = base.Name;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001A77C File Offset: 0x0001897C
		protected override void OnParentChanged(EventArgs e)
		{
			if (base.Parent != null && base.Parent.GetType().Name == "FormViewContainerControl")
			{
				base.SetBounds(0, 0, base.Parent.Width, base.Parent.Height);
			}
			base.OnParentChanged(e);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001A7D2 File Offset: 0x000189D2
		private void RegisterCommandsEvent(CommandCollection commands)
		{
			commands.CommandAdded += new CommandEventHandler(this.Commands_CommandAdded);
			commands.CommandRemoved += new CommandEventHandler(this.Commands_CommandRemoved);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001A7F8 File Offset: 0x000189F8
		private void UnregisterCommandsEvent(CommandCollection commands)
		{
			commands.CommandAdded -= new CommandEventHandler(this.Commands_CommandAdded);
			commands.CommandRemoved -= new CommandEventHandler(this.Commands_CommandRemoved);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001A81E File Offset: 0x00018A1E
		private void Commands_CommandAdded(object sender, CommandEventArgs e)
		{
			e.Command.VisibleChanged += this.Command_VisibleChanged;
			this.ScanAllSeparators(sender as CommandCollection);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001A844 File Offset: 0x00018A44
		private void Command_VisibleChanged(object sender, EventArgs e)
		{
			Command command = sender as Command;
			if (!command.IsSeparator)
			{
				this.ScanAllSeparators(this.GetCommandCollection(command));
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001A86D File Offset: 0x00018A6D
		private void Commands_CommandRemoved(object sender, CommandEventArgs e)
		{
			e.Command.VisibleChanged -= this.Command_VisibleChanged;
			this.ScanAllSeparators(sender as CommandCollection);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001A894 File Offset: 0x00018A94
		private CommandCollection GetCommandCollection(Command command)
		{
			CommandCollection result = null;
			if (this.ResultPaneCommands.Contains(command))
			{
				result = this.ResultPaneCommands;
			}
			else if (this.ViewModeCommands.Contains(command))
			{
				result = this.ViewModeCommands;
			}
			else if (this.ExportListCommands.Contains(command))
			{
				result = this.ExportListCommands;
			}
			else if (this.SelectionCommands.Contains(command))
			{
				result = this.SelectionCommands;
			}
			return result;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001A8FE File Offset: 0x00018AFE
		public virtual bool HasPermission()
		{
			return true;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001A901 File Offset: 0x00018B01
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x0001A909 File Offset: 0x00018B09
		public ContainerResultPane ContainerResultPane
		{
			get
			{
				return this.containerResultPane;
			}
			internal set
			{
				this.containerResultPane = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001A912 File Offset: 0x00018B12
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x0001A91A File Offset: 0x00018B1A
		public ResultPane DependedResultPane
		{
			get
			{
				return this.dependedResultPane;
			}
			internal set
			{
				if (this.DependedResultPane != value)
				{
					this.dependedResultPane = value;
				}
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001A92C File Offset: 0x00018B2C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(400, 400);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001A93D File Offset: 0x00018B3D
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0001A945 File Offset: 0x00018B45
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001A94E File Offset: 0x00018B4E
		private bool ShouldSerializeFont()
		{
			return this.Font == FontHelper.GetDefaultFont();
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001A95D File Offset: 0x00018B5D
		private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Window)
			{
				this.UpdateFontSetting();
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001A96F File Offset: 0x00018B6F
		private void UpdateFontSetting()
		{
			base.SuspendLayout();
			this.Font = FontHelper.GetDefaultFont();
			base.ResumeLayout(false);
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001A989 File Offset: 0x00018B89
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0001A991 File Offset: 0x00018B91
		[Category("Result Pane")]
		[DefaultValue(null)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (this.Icon != value)
				{
					this.icon = value;
					this.OnIconChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001A9B0 File Offset: 0x00018BB0
		protected virtual void OnIconChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventIconChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000822 RID: 2082 RVA: 0x0001A9DE File Offset: 0x00018BDE
		// (remove) Token: 0x06000823 RID: 2083 RVA: 0x0001A9F1 File Offset: 0x00018BF1
		public event EventHandler IconChanged
		{
			add
			{
				base.Events.AddHandler(AbstractResultPane.EventIconChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(AbstractResultPane.EventIconChanged, value);
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001AA04 File Offset: 0x00018C04
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			this.needSuppressHelp = (Keys.F1 != keyData && (Keys.F1 & keyData) == Keys.F1 && ((Keys)458864 & keyData) == keyData);
			return Keys.F1 == keyData || base.ProcessCmdKey(ref msg, keyData);
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001AA38 File Offset: 0x00018C38
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0001AA40 File Offset: 0x00018C40
		public BindingSource SharedSettingsBindingSource
		{
			get
			{
				return this.sharedSettingsBindingSource;
			}
			private set
			{
				this.sharedSettingsBindingSource = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001AA49 File Offset: 0x00018C49
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x0001AA51 File Offset: 0x00018C51
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ExchangeSettings SharedSettings
		{
			get
			{
				return this.sharedSettings;
			}
			set
			{
				if (value != this.SharedSettings)
				{
					this.OnSharedSettingsChanging();
					this.sharedSettings = value;
					this.SharedSettingsBindingSource.DataSource = this.sharedSettings;
					this.OnSharedSettingsChanged();
				}
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001AA80 File Offset: 0x00018C80
		protected virtual void OnSharedSettingsChanging()
		{
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001AA82 File Offset: 0x00018C82
		protected virtual void OnSharedSettingsChanged()
		{
			if (this.SharedSettings != null && !this.dataBoundToSettings)
			{
				this.OnBindToSharedSettings();
				this.dataBoundToSettings = true;
			}
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001AAA1 File Offset: 0x00018CA1
		protected virtual void OnBindToSharedSettings()
		{
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001AAA3 File Offset: 0x00018CA3
		public ExchangeSettings PrivateSettings
		{
			get
			{
				if (this.privateSettings == null)
				{
					this.privateSettings = this.CreatePrivateSettings(this);
				}
				return this.privateSettings;
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001AAC0 File Offset: 0x00018CC0
		protected virtual ExchangeSettings CreatePrivateSettings(IComponent owner)
		{
			return new ExchangeSettings(owner);
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001AAC8 File Offset: 0x00018CC8
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x0001AACB File Offset: 0x00018CCB
		[DefaultValue(true)]
		public virtual bool SaveSettings
		{
			get
			{
				return true;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001AAD2 File Offset: 0x00018CD2
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x0001AADF File Offset: 0x00018CDF
		[DefaultValue("")]
		public string SettingsKey
		{
			get
			{
				return this.PrivateSettings.SettingsKey;
			}
			set
			{
				this.PrivateSettings.SettingsKey = value;
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001AAF0 File Offset: 0x00018CF0
		public virtual void LoadComponentSettings()
		{
			ISettingsProviderService provSvc = this.Site.GetService(typeof(ISettingsProviderService)) as ISettingsProviderService;
			this.PrivateSettings.UpdateProviders(provSvc);
			this.PrivateSettings.Reload();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001AB2F File Offset: 0x00018D2F
		public virtual void ResetComponentSettings()
		{
			this.PrivateSettings.Reset();
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001AB3C File Offset: 0x00018D3C
		public virtual void SaveComponentSettings()
		{
			ISettingsProviderService provSvc = this.Site.GetService(typeof(ISettingsProviderService)) as ISettingsProviderService;
			this.PrivateSettings.UpdateProviders(provSvc);
			this.PrivateSettings.Save();
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001AB7B File Offset: 0x00018D7B
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x0001AB83 File Offset: 0x00018D83
		[Category("Result Pane")]
		[DefaultValue("")]
		public string Status
		{
			get
			{
				return this.status;
			}
			set
			{
				if (string.Compare(this.Status, value) != 0)
				{
					if (value == null)
					{
						this.status = "";
					}
					else
					{
						this.status = value;
					}
					this.OnStatusChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001ABB8 File Offset: 0x00018DB8
		protected virtual void OnStatusChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventStatusChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000838 RID: 2104 RVA: 0x0001ABE6 File Offset: 0x00018DE6
		// (remove) Token: 0x06000839 RID: 2105 RVA: 0x0001ABF9 File Offset: 0x00018DF9
		public event EventHandler StatusChanged
		{
			add
			{
				base.Events.AddHandler(AbstractResultPane.EventStatusChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(AbstractResultPane.EventStatusChanged, value);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001AC0C File Offset: 0x00018E0C
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0001AC14 File Offset: 0x00018E14
		[DefaultValue(false)]
		public bool IsModified
		{
			get
			{
				return this.isModified;
			}
			set
			{
				if (this.IsModified != value)
				{
					this.isModified = value;
					this.OnIsModifiedChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001AC34 File Offset: 0x00018E34
		protected virtual void OnIsModifiedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventIsModifiedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600083D RID: 2109 RVA: 0x0001AC62 File Offset: 0x00018E62
		// (remove) Token: 0x0600083E RID: 2110 RVA: 0x0001AC75 File Offset: 0x00018E75
		public event EventHandler IsModifiedChanged
		{
			add
			{
				SynchronizedDelegate.Combine(base.Events, AbstractResultPane.EventIsModifiedChanged, value);
			}
			remove
			{
				SynchronizedDelegate.Remove(base.Events, AbstractResultPane.EventIsModifiedChanged, value);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0001AC88 File Offset: 0x00018E88
		public CommandCollection ResultPaneCommands
		{
			get
			{
				return this.resultPaneCommands;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0001AC90 File Offset: 0x00018E90
		public CommandCollection ExportListCommands
		{
			get
			{
				return this.exportListCommands;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0001AC98 File Offset: 0x00018E98
		public CommandCollection ViewModeCommands
		{
			get
			{
				return this.viewModeCommands;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CommandCollection SelectionCommands
		{
			get
			{
				return this.selectionCommands;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0001ACA8 File Offset: 0x00018EA8
		public Command RefreshCommand
		{
			get
			{
				return this.refreshCommand;
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001ACB0 File Offset: 0x00018EB0
		private void RefreshCommand_Execute(object sender, EventArgs e)
		{
			if (this.RefreshableDataSource != null)
			{
				this.RefreshableDataSource.Refresh(base.CreateProgress(this.RefreshCommand.Text));
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001ACD6 File Offset: 0x00018ED6
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x0001ACE0 File Offset: 0x00018EE0
		[DefaultValue(null)]
		public virtual IRefreshableNotification RefreshableDataSource
		{
			get
			{
				return this.refreshableDataSource;
			}
			set
			{
				if (this.RefreshableDataSource != value)
				{
					this.OnRefreshableDataSourceChanging(EventArgs.Empty);
					if (this.RefreshableDataSource != null)
					{
						this.RefreshableDataSource.RefreshingChanged -= this.RefreshableDataSource_RefreshingChanged;
					}
					this.refreshableDataSource = value;
					if (this.RefreshableDataSource != null)
					{
						this.RefreshableDataSource.RefreshingChanged += this.RefreshableDataSource_RefreshingChanged;
						this.RefreshableDataSource_RefreshingChanged(this.RefreshableDataSource, EventArgs.Empty);
						if (this.RefreshableDataSource is DataTableLoader || this.RefreshableDataSource is AggregateDataTableLoadersRefreshableSource)
						{
							base.Components.Add(this.RefreshableDataSource as IComponent);
						}
					}
					else
					{
						this.RefreshCommand.Enabled = false;
					}
					this.OnRefreshableDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		protected virtual void OnRefreshableDataSourceChanging(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventRefreshableDataSourceChanging];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000848 RID: 2120 RVA: 0x0001ADD2 File Offset: 0x00018FD2
		// (remove) Token: 0x06000849 RID: 2121 RVA: 0x0001ADE5 File Offset: 0x00018FE5
		public event EventHandler RefreshableDataSourceChanging
		{
			add
			{
				base.Events.AddHandler(AbstractResultPane.EventRefreshableDataSourceChanging, value);
			}
			remove
			{
				base.Events.RemoveHandler(AbstractResultPane.EventRefreshableDataSourceChanging, value);
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001ADF8 File Offset: 0x00018FF8
		protected virtual void OnRefreshableDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventRefreshableDataSourceChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600084B RID: 2123 RVA: 0x0001AE26 File Offset: 0x00019026
		// (remove) Token: 0x0600084C RID: 2124 RVA: 0x0001AE39 File Offset: 0x00019039
		public event EventHandler RefreshableDataSourceChanged
		{
			add
			{
				base.Events.AddHandler(AbstractResultPane.EventRefreshableDataSourceChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(AbstractResultPane.EventRefreshableDataSourceChanged, value);
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001AE4C File Offset: 0x0001904C
		private void RefreshableDataSource_RefreshingChanged(object sender, EventArgs e)
		{
			this.RefreshCommand.Enabled = !this.RefreshableDataSource.Refreshing;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600084E RID: 2126
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract string SelectionHelpTopic { get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0001AE67 File Offset: 0x00019067
		public Command HelpCommand
		{
			get
			{
				return this.helpCommand;
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001AE70 File Offset: 0x00019070
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			try
			{
				if (this.needSuppressHelp)
				{
					hevent.Handled = true;
				}
				else if (!hevent.Handled && !string.IsNullOrEmpty(this.SelectionHelpTopic))
				{
					hevent.Handled = true;
					ExchangeHelpService.ShowHelpFromHelpTopicId(this, this.SelectionHelpTopic);
				}
				base.OnHelpRequested(hevent);
			}
			finally
			{
				this.needSuppressHelp = false;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001AED8 File Offset: 0x000190D8
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x0001AEE0 File Offset: 0x000190E0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0001AEE9 File Offset: 0x000190E9
		public CommandCollection ContextMenuCommands
		{
			get
			{
				return this.contextMenuCommands;
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001AEF1 File Offset: 0x000190F1
		private void ContextMenu_Popup(object sender, EventArgs e)
		{
			this.UpdateContextMenu();
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001AEF9 File Offset: 0x000190F9
		internal void UpdateContextMenu()
		{
			this.UpdateContextMenuCommands();
			this.ScanAllSeparators(this.ContextMenuCommands);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001AF0D File Offset: 0x0001910D
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void UpdateContextMenuCommands()
		{
			this.ContextMenuCommands.Clear();
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0001AF1A File Offset: 0x0001911A
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0001AF22 File Offset: 0x00019122
		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
			private set
			{
				this.isActive = value;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001AF2B File Offset: 0x0001912B
		protected virtual void DelayInitialize()
		{
			this.delayInitialized = true;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001AF34 File Offset: 0x00019134
		public void OnSetActive()
		{
			if (!this.delayInitialized)
			{
				this.DelayInitialize();
			}
			this.numberOfActivation++;
			this.OnSettingActive(EventArgs.Empty);
			this.IsActive = true;
			this.PublishSelection();
			this.OnSetActive(EventArgs.Empty);
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0001AF80 File Offset: 0x00019180
		public bool IsActivatedFirstly
		{
			get
			{
				return this.numberOfActivation == 1;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001AF8C File Offset: 0x0001918C
		protected virtual void OnSettingActive(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventSettingActive];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x0600085D RID: 2141 RVA: 0x0001AFBA File Offset: 0x000191BA
		// (remove) Token: 0x0600085E RID: 2142 RVA: 0x0001AFCD File Offset: 0x000191CD
		public event EventHandler SettingActive
		{
			add
			{
				base.Events.AddHandler(AbstractResultPane.EventSettingActive, value);
			}
			remove
			{
				base.Events.RemoveHandler(AbstractResultPane.EventSettingActive, value);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001AFE0 File Offset: 0x000191E0
		protected virtual void OnSetActive(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventSetActive];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000860 RID: 2144 RVA: 0x0001B00E File Offset: 0x0001920E
		// (remove) Token: 0x06000861 RID: 2145 RVA: 0x0001B021 File Offset: 0x00019221
		public event EventHandler SetActive
		{
			add
			{
				base.Events.AddHandler(AbstractResultPane.EventSetActive, value);
			}
			remove
			{
				base.Events.RemoveHandler(AbstractResultPane.EventSetActive, value);
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001B034 File Offset: 0x00019234
		public void OnKillActive()
		{
			this.OnKillingActive(EventArgs.Empty);
			this.IsActive = false;
			this.OnKillActive(EventArgs.Empty);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001B054 File Offset: 0x00019254
		protected virtual void OnKillingActive(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventKillingActive];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000864 RID: 2148 RVA: 0x0001B082 File Offset: 0x00019282
		// (remove) Token: 0x06000865 RID: 2149 RVA: 0x0001B095 File Offset: 0x00019295
		public event EventHandler KillingActive
		{
			add
			{
				base.Events.AddHandler(AbstractResultPane.EventKillingActive, value);
			}
			remove
			{
				base.Events.RemoveHandler(AbstractResultPane.EventKillingActive, value);
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001B0A8 File Offset: 0x000192A8
		protected virtual void OnKillActive(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventKillActive];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000867 RID: 2151 RVA: 0x0001B0D6 File Offset: 0x000192D6
		// (remove) Token: 0x06000868 RID: 2152 RVA: 0x0001B0E9 File Offset: 0x000192E9
		public event EventHandler KillActive
		{
			add
			{
				base.Events.AddHandler(AbstractResultPane.EventKillActive, value);
			}
			remove
			{
				base.Events.RemoveHandler(AbstractResultPane.EventKillActive, value);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001B0FC File Offset: 0x000192FC
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x0001B104 File Offset: 0x00019304
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(true)]
		public new bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.Enabled != value)
				{
					this.OnEnabledChanging(EventArgs.Empty);
					this.enabled = value;
					if (this.Enabled != base.Enabled)
					{
						base.Enabled = this.Enabled;
					}
				}
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001B13C File Offset: 0x0001933C
		protected virtual void OnEnabledChanging(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventEnabledChanging];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600086C RID: 2156 RVA: 0x0001B16A File Offset: 0x0001936A
		// (remove) Token: 0x0600086D RID: 2157 RVA: 0x0001B17D File Offset: 0x0001937D
		public event EventHandler EnabledChanging
		{
			add
			{
				base.Events.AddHandler(AbstractResultPane.EventEnabledChanging, value);
			}
			remove
			{
				base.Events.RemoveHandler(AbstractResultPane.EventEnabledChanging, value);
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001B190 File Offset: 0x00019390
		protected override void OnEnabledChanged(EventArgs e)
		{
			if (this.Enabled != base.Enabled)
			{
				this.Enabled = base.Enabled;
			}
			base.OnEnabledChanged(e);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001B1B3 File Offset: 0x000193B3
		protected void UpdateSelection(ICollection selectedObjects, string displayName, DataObject dataObject)
		{
			this.UpdateSelection(selectedObjects, displayName, dataObject, string.Empty);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001B1C4 File Offset: 0x000193C4
		protected virtual void UpdateSelection(ICollection selectedObjects, string displayName, DataObject dataObject, string description)
		{
			this.selectionDataObject = ((dataObject == null) ? new DataObject() : dataObject);
			this.SelectedObjects = selectedObjects;
			this.selectionDataObject.SetText(displayName);
			this.SelectionDataObject.SetData("SelectedObjects", this.SelectedObjects);
			this.selectionDataObject.SetData("Description", description);
			if (this.IsActive)
			{
				this.PublishSelection();
			}
			this.OnSelectionChanged(EventArgs.Empty);
			this.UpdateContextMenu();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001B23C File Offset: 0x0001943C
		private void PublishSelection()
		{
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SetSelectedComponents(this.SelectedObjects);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0001B26E File Offset: 0x0001946E
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0001B276 File Offset: 0x00019476
		public ICollection SelectedObjects
		{
			get
			{
				return this.selectedObjects;
			}
			private set
			{
				if (value == null)
				{
					value = AbstractResultPane.emptySelectedObjects;
				}
				this.selectedObjects = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001B289 File Offset: 0x00019489
		public DataObject SelectionDataObject
		{
			get
			{
				return this.selectionDataObject;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001B291 File Offset: 0x00019491
		public virtual string SelectedName
		{
			get
			{
				return this.SelectionDataObject.GetText();
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0001B29E File Offset: 0x0001949E
		public virtual string SelectedObjectDetailsType
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0001B2A5 File Offset: 0x000194A5
		public bool HasSelection
		{
			get
			{
				return this.SelectedObjects.Count > 0;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0001B2B5 File Offset: 0x000194B5
		public bool HasSingleSelection
		{
			get
			{
				return this.SelectedObjects.Count == 1;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001B2C5 File Offset: 0x000194C5
		public bool HasMultiSelection
		{
			get
			{
				return this.SelectedObjects.Count > 1;
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001B2D8 File Offset: 0x000194D8
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[AbstractResultPane.EventSelectionChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600087B RID: 2171 RVA: 0x0001B306 File Offset: 0x00019506
		// (remove) Token: 0x0600087C RID: 2172 RVA: 0x0001B319 File Offset: 0x00019519
		public event EventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(AbstractResultPane.EventSelectionChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(AbstractResultPane.EventSelectionChanged, value);
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001B32C File Offset: 0x0001952C
		private void ScanAllSeparators(CommandCollection commands)
		{
			bool flag = false;
			Command command = null;
			foreach (object obj in commands)
			{
				Command command2 = (Command)obj;
				if (flag)
				{
					if (command2.IsSeparator)
					{
						command2.Visible = true;
						command = command2;
						flag = false;
					}
				}
				else if (command2.IsSeparator)
				{
					command2.Visible = false;
				}
				else if (command2.Visible)
				{
					flag = true;
				}
			}
			if (!flag && command != null)
			{
				command.Visible = false;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0001B3C0 File Offset: 0x000195C0
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0001B3D1 File Offset: 0x000195D1
		public override RightToLeft RightToLeft
		{
			get
			{
				if (!LayoutHelper.CultureInfoIsRightToLeft)
				{
					return base.RightToLeft;
				}
				return RightToLeft.Yes;
			}
			set
			{
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001B45A File Offset: 0x0001965A
		void IResultPaneControl.add_HelpRequested(HelpEventHandler A_1)
		{
			base.HelpRequested += A_1;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001B463 File Offset: 0x00019663
		void IResultPaneControl.remove_HelpRequested(HelpEventHandler A_1)
		{
			base.HelpRequested -= A_1;
		}

		// Token: 0x040003A5 RID: 933
		private const Icon DefaultIcon = null;

		// Token: 0x040003A6 RID: 934
		private const string DefaultStatus = "";

		// Token: 0x040003A7 RID: 935
		private ContextMenu contextMenuLocal;

		// Token: 0x040003A8 RID: 936
		private ContainerResultPane containerResultPane;

		// Token: 0x040003A9 RID: 937
		private ResultPane dependedResultPane;

		// Token: 0x040003AA RID: 938
		private Icon icon;

		// Token: 0x040003AB RID: 939
		private static readonly object EventIconChanged = new object();

		// Token: 0x040003AC RID: 940
		private bool needSuppressHelp;

		// Token: 0x040003AD RID: 941
		[AccessedThroughProperty("SharedSettingsBindingSource")]
		private BindingSource sharedSettingsBindingSource;

		// Token: 0x040003AE RID: 942
		private bool dataBoundToSettings;

		// Token: 0x040003AF RID: 943
		private ExchangeSettings sharedSettings;

		// Token: 0x040003B0 RID: 944
		private ExchangeSettings privateSettings;

		// Token: 0x040003B1 RID: 945
		private string status = "";

		// Token: 0x040003B2 RID: 946
		private static readonly object EventStatusChanged = new object();

		// Token: 0x040003B3 RID: 947
		private bool isModified;

		// Token: 0x040003B4 RID: 948
		private static readonly object EventIsModifiedChanged = new object();

		// Token: 0x040003B5 RID: 949
		private CommandCollection resultPaneCommands = new CommandCollection();

		// Token: 0x040003B6 RID: 950
		private CommandCollection exportListCommands = new CommandCollection();

		// Token: 0x040003B7 RID: 951
		private CommandCollection viewModeCommands = new CommandCollection();

		// Token: 0x040003B8 RID: 952
		private CommandCollection selectionCommands = new CommandCollection();

		// Token: 0x040003B9 RID: 953
		private Command refreshCommand = new Command();

		// Token: 0x040003BA RID: 954
		private IRefreshableNotification refreshableDataSource;

		// Token: 0x040003BB RID: 955
		private static readonly object EventRefreshableDataSourceChanging = new object();

		// Token: 0x040003BC RID: 956
		private static readonly object EventRefreshableDataSourceChanged = new object();

		// Token: 0x040003BD RID: 957
		private Command helpCommand = new Command();

		// Token: 0x040003BE RID: 958
		private CommandCollection contextMenuCommands = new CommandCollection();

		// Token: 0x040003BF RID: 959
		private bool isActive;

		// Token: 0x040003C0 RID: 960
		private bool delayInitialized;

		// Token: 0x040003C1 RID: 961
		private int numberOfActivation;

		// Token: 0x040003C2 RID: 962
		private static readonly object EventSettingActive = new object();

		// Token: 0x040003C3 RID: 963
		private static readonly object EventSetActive = new object();

		// Token: 0x040003C4 RID: 964
		private static readonly object EventKillingActive = new object();

		// Token: 0x040003C5 RID: 965
		private static readonly object EventKillActive = new object();

		// Token: 0x040003C6 RID: 966
		private bool enabled = true;

		// Token: 0x040003C7 RID: 967
		private static readonly object EventEnabledChanging = new object();

		// Token: 0x040003C8 RID: 968
		private ICollection selectedObjects = AbstractResultPane.emptySelectedObjects;

		// Token: 0x040003C9 RID: 969
		private static ICollection emptySelectedObjects = new object[0];

		// Token: 0x040003CA RID: 970
		private DataObject selectionDataObject = new DataObject();

		// Token: 0x040003CB RID: 971
		private static readonly object EventSelectionChanged = new object();
	}
}
