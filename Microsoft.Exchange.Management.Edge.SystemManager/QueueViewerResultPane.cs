using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x0200000F RID: 15
	public class QueueViewerResultPane : TabbedResultPane
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00005808 File Offset: 0x00003A08
		public QueueViewerResultPane()
		{
			this.InitializeComponent();
			base.CaptionText = Strings.QueueViewerNodeName(Strings.QueueViewerNotConnected);
			base.Icon = Icons.QueueViewerTool;
			this.commandQueuesView = new Command();
			this.commandQueuesView.Description = LocalizedString.Empty;
			this.commandQueuesView.Name = "commandQueuesView";
			this.commandQueuesView.Text = Strings.QueueViewerQueuesText;
			this.commandQueuesView.Execute += this.commandQueuesView_Execute;
			this.commandAllMessagesView = new Command();
			this.commandAllMessagesView.Description = LocalizedString.Empty;
			this.commandAllMessagesView.Name = "commandAllMessagesView";
			this.commandAllMessagesView.Text = Strings.QueueViewerMessagesText;
			this.commandAllMessagesView.Execute += this.commandAllMessagesView_Execute;
			this.commandMessagesView = new Command();
			this.commandMessagesView.Description = LocalizedString.Empty;
			this.commandMessagesView.Name = "commandMessagesView";
			this.commandMessagesView.Text = Strings.QueueViewerMessagesText;
			this.commandMessagesView.Execute += this.commandMessagesView_Execute;
			this.commandMessagesView.Visible = false;
			this.commandOptions = new Command();
			this.commandOptions.Description = LocalizedString.Empty;
			this.commandOptions.Name = "commandMessagesView";
			this.commandOptions.Text = Strings.QueueViewerOptionsText;
			this.commandOptions.Execute += this.commandOptions_Execute;
			base.ViewModeCommands.Add(this.commandQueuesView);
			base.ViewModeCommands.Add(this.commandAllMessagesView);
			base.ViewModeCommands.Add(this.commandMessagesView);
			base.ViewModeCommands.Add(this.commandOptions);
			this.commandConnectToServer = new Command();
			this.commandConnectToServer.Description = LocalizedString.Empty;
			this.commandConnectToServer.Name = "ConnectToServer";
			this.commandConnectToServer.Icon = Icons.ConnectToServer;
			this.commandConnectToServer.Text = Strings.ConnectToServerCommand;
			this.commandConnectToServer.Execute += this.commandConnectToServer_Execute;
			this.commandConnectToServer.Visible = false;
			base.ResultPaneCommands.Add(this.commandConnectToServer);
			base.ResultPaneTabs.Add(this.queueListResultPane);
			base.ResultPaneTabs.Add(this.allMessagesListResultPane);
			this.messageListResultPane.ViewModeCommands.Remove(this.messageListResultPane.SaveDefaultFilterCommand);
			this.Dock = DockStyle.Fill;
			this.timer = new Timer();
			this.timer.Tick += delegate(object param0, EventArgs param1)
			{
				if (this.AutoRefreshEnabled)
				{
					QueueViewerResultPaneBase queueViewerResultPaneBase = base.SelectedResultPane as QueueViewerResultPaneBase;
					queueViewerResultPaneBase.SetRefreshWhenActivated();
				}
			};
			this.timer.Interval = this.RefreshInterval * 1000;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00005B38 File Offset: 0x00003D38
		protected override void OnSetActive(EventArgs e)
		{
			base.OnSetActive(e);
			if (!this.alreadySetActive)
			{
				this.alreadySetActive = true;
				bool visible = true;
				RoleCollection installedRoles = RoleManager.GetInstalledRoles();
				foreach (Role role in installedRoles)
				{
					if ((role.ServerRole == ServerRole.Edge || role.ServerRole == ServerRole.HubTransport || role.ServerRole == ServerRole.Mailbox) && string.IsNullOrEmpty(this.ServerName))
					{
						this.ServerName = NativeHelpers.GetLocalComputerFqdn(false);
					}
					if (role.ServerRole == ServerRole.Edge)
					{
						visible = false;
					}
				}
				this.commandConnectToServer.Visible = visible;
				if (string.IsNullOrEmpty(this.ServerName))
				{
					this.commandConnectToServer.Invoke();
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00005C08 File Offset: 0x00003E08
		protected override ExchangeSettings CreatePrivateSettings(IComponent owner)
		{
			return new QueueViewerSettings(this);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00005C10 File Offset: 0x00003E10
		private void InitializeComponent()
		{
			this.queueListResultPane = new QueueViewerQueuesResultPane();
			this.allMessagesListResultPane = new QueueViewerMessagesResultPane();
			this.messageListResultPane = new QueueViewerMessagesResultPane();
			this.queueListResultPane.Name = "QueuesListResultPane";
			this.queueListResultPane.Text = Strings.QueueViewerQueues;
			this.allMessagesListResultPane.Name = "AllMessagesListResultPane";
			this.allMessagesListResultPane.Text = Strings.QueueViewerMessages;
			this.messageListResultPane.Name = "MessageListResultPane";
			this.messageListResultPane.Text = Strings.QueueViewerMessages;
			this.DoubleBuffered = true;
			base.Name = "QueueViewerResultPane";
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00005CC0 File Offset: 0x00003EC0
		private void commandConnectToServer_Execute(object sender, EventArgs e)
		{
			ConnectToServerParams connectToServerParams = new ConnectToServerParams(false, string.Empty);
			DataContext context = new DataContext(new ExchangeDataHandler
			{
				DataSource = connectToServerParams
			});
			ConnectToServerControl connectToServerControl = new ConnectToServerControl();
			connectToServerControl.Text = Strings.ConnectToServer;
			connectToServerControl.ConnectServerLabelText = Strings.SelectTransportServerLabelText;
			connectToServerControl.SetDefaultServerCheckBoxText = Strings.SetDefaultServerCheckBoxText;
			connectToServerControl.AutoSize = true;
			connectToServerControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			connectToServerControl.ServerRoleToPicker = (ServerRole.Mailbox | ServerRole.HubTransport);
			connectToServerControl.Context = context;
			using (PropertyPageDialog propertyPageDialog = new PropertyPageDialog(connectToServerControl))
			{
				propertyPageDialog.OkButtonText = Strings.ConnectButtonText;
				propertyPageDialog.LinkIsDirtyToOkEnabled = true;
				propertyPageDialog.HelpTopic = HelpId.ConnectToServerControl.ToString();
				if (this.QueueSettings.UseDefaultServer)
				{
					connectToServerParams.ServerName = this.QueueSettings.DefaultServerName;
					connectToServerControl.IsDirty = true;
				}
				if (this.ShowDialog(propertyPageDialog) == DialogResult.OK)
				{
					this.ServerName = connectToServerParams.ServerName;
					if (connectToServerParams.SetAsDefaultServer)
					{
						this.QueueSettings.UseDefaultServer = connectToServerParams.SetAsDefaultServer;
						this.QueueSettings.DefaultServerName = connectToServerParams.ServerName;
					}
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00005DF8 File Offset: 0x00003FF8
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00005E00 File Offset: 0x00004000
		internal string ServerName
		{
			get
			{
				return this.serverName;
			}
			set
			{
				if (value != this.serverName)
				{
					if (base.ResultPaneTabs.Contains(this.messageListResultPane))
					{
						base.ResultPaneTabs.Remove(this.messageListResultPane);
						base.ViewModeCommands.Remove(this.commandMessagesView);
					}
					this.serverName = value;
					base.CaptionText = Strings.QueueViewerNodeName(this.serverName);
					this.QueueListResultPane.ServerName = this.serverName;
					this.AllMessagesListResultPane.ServerName = this.serverName;
					this.MessageListResultPane.ServerName = this.serverName;
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00005EA4 File Offset: 0x000040A4
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00005EE8 File Offset: 0x000040E8
		internal QueueViewerResultPane.ViewModes CurrentView
		{
			get
			{
				QueueViewerResultPane.ViewModes result = QueueViewerResultPane.ViewModes.QueueView;
				if (base.SelectedResultPane == this.QueueListResultPane)
				{
					result = QueueViewerResultPane.ViewModes.QueueView;
				}
				else if (base.SelectedResultPane == this.AllMessagesListResultPane)
				{
					result = QueueViewerResultPane.ViewModes.AllMessageView;
				}
				else if (base.SelectedResultPane == this.MessageListResultPane)
				{
					result = QueueViewerResultPane.ViewModes.MessageView;
				}
				return result;
			}
			set
			{
				switch (value)
				{
				case QueueViewerResultPane.ViewModes.QueueView:
					base.SelectedResultPane = this.QueueListResultPane;
					return;
				case QueueViewerResultPane.ViewModes.AllMessageView:
					base.SelectedResultPane = this.AllMessagesListResultPane;
					return;
				case QueueViewerResultPane.ViewModes.MessageView:
					base.SelectedResultPane = this.MessageListResultPane;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00005F30 File Offset: 0x00004130
		internal QueueViewerQueuesResultPane QueueListResultPane
		{
			get
			{
				return this.queueListResultPane;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00005F38 File Offset: 0x00004138
		internal QueueViewerMessagesResultPane MessageListResultPane
		{
			get
			{
				return this.messageListResultPane;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00005F40 File Offset: 0x00004140
		internal QueueViewerMessagesResultPane AllMessagesListResultPane
		{
			get
			{
				return this.allMessagesListResultPane;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00005F48 File Offset: 0x00004148
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00005F50 File Offset: 0x00004150
		internal int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				if (value != this.pageSize)
				{
					this.pageSize = value;
					this.AllMessagesListResultPane.PageSize = this.pageSize;
					this.MessageListResultPane.PageSize = this.pageSize;
					this.QueueListResultPane.PageSize = this.pageSize;
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00005FA0 File Offset: 0x000041A0
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00005FA8 File Offset: 0x000041A8
		internal bool AutoRefreshEnabled
		{
			get
			{
				return this.autoRefreshEnabled;
			}
			set
			{
				this.autoRefreshEnabled = value;
				this.timer.Enabled = this.autoRefreshEnabled;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00005FC2 File Offset: 0x000041C2
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00005FCC File Offset: 0x000041CC
		internal int RefreshInterval
		{
			get
			{
				return this.refreshInterval;
			}
			set
			{
				this.refreshInterval = value;
				this.timer.Interval = (int)EnhancedTimeSpan.FromSeconds((double)this.refreshInterval).TotalMilliseconds;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00006000 File Offset: 0x00004200
		internal QueueViewerSettings QueueSettings
		{
			get
			{
				return (QueueViewerSettings)base.PrivateSettings;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000600D File Offset: 0x0000420D
		internal Command CommandQueuesView
		{
			get
			{
				return this.commandQueuesView;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00006015 File Offset: 0x00004215
		internal Command CommandAllMessagesView
		{
			get
			{
				return this.commandAllMessagesView;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000601D File Offset: 0x0000421D
		internal Command CommandMessagesView
		{
			get
			{
				return this.commandMessagesView;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00006025 File Offset: 0x00004225
		private void commandQueuesView_Execute(object sender, EventArgs e)
		{
			this.CurrentView = QueueViewerResultPane.ViewModes.QueueView;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000602E File Offset: 0x0000422E
		private void commandMessagesView_Execute(object sender, EventArgs e)
		{
			this.CurrentView = QueueViewerResultPane.ViewModes.MessageView;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00006037 File Offset: 0x00004237
		private void commandAllMessagesView_Execute(object sender, EventArgs e)
		{
			this.CurrentView = QueueViewerResultPane.ViewModes.AllMessageView;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00006040 File Offset: 0x00004240
		internal Command CommandConnectToServer
		{
			get
			{
				return this.commandConnectToServer;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00006048 File Offset: 0x00004248
		internal Command CommandOptions
		{
			get
			{
				return this.commandOptions;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00006050 File Offset: 0x00004250
		private void commandOptions_Execute(object sender, EventArgs e)
		{
			QueueViewerOptions queueViewerOptions = new QueueViewerOptions(this.AutoRefreshEnabled, EnhancedTimeSpan.FromSeconds((double)this.RefreshInterval), (uint)this.PageSize);
			DataContext context = new DataContext(new ExchangeDataHandler
			{
				DataSource = queueViewerOptions
			});
			using (PropertyPageDialog propertyPageDialog = new PropertyPageDialog(new QueueViewerPropertyPage
			{
				Context = context
			}))
			{
				propertyPageDialog.LinkIsDirtyToOkEnabled = true;
				if (this.ShowDialog(propertyPageDialog) == DialogResult.OK)
				{
					this.AutoRefreshEnabled = queueViewerOptions.AutoRefreshEnabled;
					this.RefreshInterval = (int)queueViewerOptions.RefreshInterval.TotalSeconds;
					this.PageSize = (int)queueViewerOptions.PageSize;
				}
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00006104 File Offset: 0x00004304
		internal void SetDatasourcesOnView()
		{
			this.QueueListResultPane.Datasource = new QueueViewerDataSource("queue");
			this.MessageListResultPane.Datasource = new QueueViewerDataSource("message");
			this.AllMessagesListResultPane.Datasource = new QueueViewerDataSource("message");
			base.Components.Add(this.QueueListResultPane.Datasource);
			base.Components.Add(this.MessageListResultPane.Datasource);
			base.Components.Add(this.AllMessagesListResultPane.Datasource);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00006194 File Offset: 0x00004394
		public override void LoadComponentSettings()
		{
			base.LoadComponentSettings();
			this.PageSize = this.QueueSettings.PageSize;
			this.AutoRefreshEnabled = this.QueueSettings.AutoRefreshEnabled;
			this.RefreshInterval = (int)this.QueueSettings.RefreshInterval.TotalSeconds;
			if (this.QueueSettings.UseDefaultServer)
			{
				this.ServerName = this.QueueSettings.DefaultServerName;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00006204 File Offset: 0x00004404
		public override void ResetComponentSettings()
		{
			base.ResetComponentSettings();
			this.PageSize = this.QueueSettings.PageSize;
			this.AutoRefreshEnabled = this.QueueSettings.AutoRefreshEnabled;
			this.RefreshInterval = (int)this.QueueSettings.RefreshInterval.TotalSeconds;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00006254 File Offset: 0x00004454
		public override void SaveComponentSettings()
		{
			this.QueueSettings.PageSize = this.PageSize;
			this.QueueSettings.AutoRefreshEnabled = this.AutoRefreshEnabled;
			this.QueueSettings.RefreshInterval = EnhancedTimeSpan.FromSeconds((double)this.RefreshInterval);
			base.SaveComponentSettings();
		}

		// Token: 0x04000024 RID: 36
		private QueueViewerQueuesResultPane queueListResultPane;

		// Token: 0x04000025 RID: 37
		private QueueViewerMessagesResultPane allMessagesListResultPane;

		// Token: 0x04000026 RID: 38
		private QueueViewerMessagesResultPane messageListResultPane;

		// Token: 0x04000027 RID: 39
		private Command commandQueuesView;

		// Token: 0x04000028 RID: 40
		private Command commandAllMessagesView;

		// Token: 0x04000029 RID: 41
		private Command commandMessagesView;

		// Token: 0x0400002A RID: 42
		private Command commandOptions;

		// Token: 0x0400002B RID: 43
		private Command commandConnectToServer;

		// Token: 0x0400002C RID: 44
		private Timer timer;

		// Token: 0x0400002D RID: 45
		private bool alreadySetActive;

		// Token: 0x0400002E RID: 46
		private string serverName = string.Empty;

		// Token: 0x0400002F RID: 47
		private int pageSize = 1000;

		// Token: 0x04000030 RID: 48
		private bool autoRefreshEnabled = true;

		// Token: 0x04000031 RID: 49
		private int refreshInterval = 30;

		// Token: 0x02000010 RID: 16
		internal enum ViewModes
		{
			// Token: 0x04000033 RID: 51
			QueueView,
			// Token: 0x04000034 RID: 52
			AllMessageView,
			// Token: 0x04000035 RID: 53
			MessageView
		}
	}
}
