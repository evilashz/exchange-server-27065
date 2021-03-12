using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000119 RID: 281
	public partial class CommandLoggingDialog : ExchangeForm
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x00025B44 File Offset: 0x00023D44
		public static Command GetCommandLoggingCommand()
		{
			if (CommandLoggingDialog.viewCommandLoggingCommand == null)
			{
				CommandLoggingDialog.viewCommandLoggingCommand = new Command();
				CommandLoggingDialog.viewCommandLoggingCommand.Name = "viewCommandLoggingCommand";
				CommandLoggingDialog.viewCommandLoggingCommand.Text = Strings.ViewCommandLOgging;
				CommandLoggingDialog.viewCommandLoggingCommand.Execute += delegate(object param0, EventArgs param1)
				{
					CommandLoggingDialog.ShowCommandLoggingDialog();
				};
			}
			return CommandLoggingDialog.viewCommandLoggingCommand;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00025BB4 File Offset: 0x00023DB4
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			if (!e.Cancel)
			{
				lock (CommandLoggingDialog.mutex)
				{
					CommandLoggingDialog.instance = null;
				}
				this.privateSettings.SaveDataListViewSettings(this.resultListView);
				this.privateSettings.Save();
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00025C4C File Offset: 0x00023E4C
		public CommandLoggingDialog()
		{
			this.InitializeComponent();
			this.fileMenuItem.Text = Strings.ObjectPickerFile;
			this.closeToolStripMenuItem.Text = Strings.ObjectPickerClose;
			this.copyCommandsToolStripMenuItem.Text = Strings.CopyCommands;
			this.actionMenuItem.Text = Strings.CommandLoggingAction;
			this.helpCommandLoggingToolStripMenuItem.Text = Strings.ShowHelpCommand;
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			this.ContextMenuStrip = contextMenuStrip;
			this.actionMenuItem.DropDown = contextMenuStrip;
			this.clearLogToolStripMenuItem.Text = Strings.ClearLogText;
			this.exportListCommand.Text = Strings.ExportListDefaultCommandText;
			this.exportListCommand.Name = "exportListCommand";
			base.Icon = Icons.CommandLogging;
			this.startLoggingDate.Text = (string.IsNullOrEmpty(CommandLoggingDialog.StartDateTime) ? Strings.CommandLoggingStopped : Strings.StartLoggingDate(CommandLoggingDialog.StartDateTime));
			Command command = new Command();
			command.Text = Strings.ModifyMaximumRecordToLog;
			command.Name = "modifyMaximumRecordToLogCommand";
			command.Execute += this.commandLoggingDialog_modifyMaximumRecordToLog;
			this.resultListView.AvailableColumns.Add(CommandLoggingSession.startExecutionTime, Strings.StartExecutionTime, true, string.Empty, null, "G", null);
			this.resultListView.AvailableColumns.Add(CommandLoggingSession.endExecutionTime, Strings.EndExecutionTime, true, string.Empty, null, "G", null);
			this.resultListView.AvailableColumns.Add(CommandLoggingSession.executionStatus, Strings.ExecutionStatus, true, "");
			this.resultListView.AvailableColumns.Add(CommandLoggingSession.command, Strings.Command, true, "");
			this.resultListView.SelectionNameProperty = CommandLoggingSession.command;
			this.resultListView.DataSource = CommandLoggingSession.GetInstance().LoggingData;
			this.addRemoveColumnsToolStripMenuItem.Command = this.resultListView.ShowColumnPickerCommand;
			this.addRemoveColumnsToolStripMenuItem.ToolTipText = "";
			this.exportListToolStripMenuItem.Command = this.exportListCommand;
			this.exportListToolStripMenuItem.ToolTipText = "";
			this.modifyMaximumRecordToLogToolStripMenuItem.Command = command;
			this.modifyMaximumRecordToLogToolStripMenuItem.ToolTipText = "";
			this.resultListView.SelectionChanged += this.resultListView_SelectionChanged;
			if (CommandLoggingDialog.settingsProvider == null)
			{
				CommandLoggingDialog.settingsProvider = new ExchangeSettingsProvider();
				CommandLoggingDialog.settingsProvider.Initialize(null, null);
			}
			this.privateSettings.UpdateProviders(CommandLoggingDialog.settingsProvider);
			this.privateSettings.Reload();
			if (CommandLoggingDialog.GlobalSettings != null && !string.IsNullOrEmpty(CommandLoggingDialog.GlobalSettings.InstanceDisplayName))
			{
				this.Text = Strings.CommandLoggingDialogTitle(CommandLoggingDialog.GlobalSettings.InstanceDisplayName);
			}
			else
			{
				this.Text = Strings.CommandLoggingDialogTitle("");
			}
			this.PrepareMenuStripItems();
			contextMenuStrip.Items.AddRange(new ToolStripItem[]
			{
				this.switchCommandLoggingToolStripMenuItem,
				this.modifyMaximumRecordToLogToolStripMenuItem,
				this.clearLogToolStripMenuItem,
				new ToolStripSeparator(),
				this.addRemoveColumnsToolStripMenuItem,
				new ToolStripSeparator(),
				this.exportListToolStripMenuItem,
				this.separator,
				this.copyCommandsToolStripMenuItem,
				new ToolStripSeparator(),
				this.helpCommandLoggingToolStripMenuItem
			});
			this.PrepareActionMenuStrip();
			base.ResizeEnd += delegate(object param0, EventArgs param1)
			{
				this.SaveCommandLoggingSettings();
			};
			this.splitContainer.SplitterMoved += delegate(object param0, SplitterEventArgs param1)
			{
				this.SaveCommandLoggingSettings();
			};
			base.Load += delegate(object param0, EventArgs param1)
			{
				this.LoadCommandLoggingSettings();
			};
			this.helpCommandLoggingToolStripMenuItem.Click += delegate(object param0, EventArgs param1)
			{
				this.OnHelpRequested(new HelpEventArgs(Point.Empty));
			};
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00026218 File Offset: 0x00024418
		private void PrepareMenuStripItems()
		{
			this.exportListCommand.Execute += delegate(object param0, EventArgs param1)
			{
				WinformsHelper.ShowExportDialog(this, this.resultListView, base.ShellUI);
			};
			this.closeToolStripMenuItem.Click += delegate(object param0, EventArgs param1)
			{
				base.Close();
			};
			this.switchCommandLoggingToolStripMenuItem.Click += delegate(object param0, EventArgs param1)
			{
				if (CommandLoggingSession.GetInstance().CommandLoggingEnabled)
				{
					CommandLoggingSession.GetInstance().CommandLoggingEnabled = false;
					if (CommandLoggingDialog.GlobalSettings != null)
					{
						CommandLoggingDialog.GlobalSettings.IsCommandLoggingEnabled = false;
					}
					CommandLoggingDialog.StartDateTime = string.Empty;
					this.startLoggingDate.Text = Strings.CommandLoggingStopped;
					this.switchCommandLoggingToolStripMenuItem.Text = Strings.StartCommandLogging;
					return;
				}
				CommandLoggingSession.GetInstance().CommandLoggingEnabled = true;
				if (CommandLoggingDialog.GlobalSettings != null)
				{
					CommandLoggingDialog.GlobalSettings.IsCommandLoggingEnabled = true;
				}
				CommandLoggingDialog.StartDateTime = ((DateTime)ExDateTime.Now).ToString();
				this.startLoggingDate.Text = Strings.StartLoggingDate(CommandLoggingDialog.StartDateTime);
				this.switchCommandLoggingToolStripMenuItem.Text = Strings.StopCommandLogging;
			};
			this.clearLogToolStripMenuItem.Click += delegate(object param0, EventArgs param1)
			{
				if (base.ShellUI.ShowMessage(Strings.ConfirmClearText, UIService.DefaultCaption, MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					CommandLoggingSession.GetInstance().Clear();
				}
			};
			this.copyCommandsToolStripMenuItem.Click += delegate(object param0, EventArgs param1)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.resultListView.SelectedObjects)
				{
					DataRowView row = (DataRowView)obj;
					stringBuilder.AppendLine(this.RetrieveValueFromRow(row, CommandLoggingSession.command));
				}
				Clipboard.SetText(stringBuilder.ToString());
			};
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00026298 File Offset: 0x00024498
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			if (!hevent.Handled)
			{
				ExchangeHelpService.ShowHelpFromHelpTopicId(HelpId.CommandLoggingDialog.ToString());
				hevent.Handled = true;
			}
			base.OnHelpRequested(hevent);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002630C File Offset: 0x0002450C
		private void commandLoggingDialog_modifyMaximumRecordToLog(object sender, EventArgs e)
		{
			using (PromptDialog promptDialog = new PromptDialog(true))
			{
				ExchangeSettings exchangeSettings = new ExchangeSettings(new Component());
				exchangeSettings.MaximumRecordCount = CommandLoggingSession.GetInstance().MaximumRecordCount;
				promptDialog.Buttons = MessageBoxButtons.OKCancel;
				promptDialog.Title = Strings.MaximumNumberToLogTitle;
				promptDialog.Message = Strings.MaximumRecordToLogIntroduction;
				promptDialog.ContentLabel = Strings.MaximumRecordToLogText;
				promptDialog.DataSource = exchangeSettings;
				promptDialog.ValueMember = "MaximumRecordCount";
				promptDialog.Parse += delegate(object obj, ConvertEventArgs args)
				{
					int num;
					if (!int.TryParse((string)args.Value, out num) || !CommandLoggingSession.IsValidMaximumRecordCount(num))
					{
						throw new LocalizedException(Strings.InvalidMaximumRecordNumber(CommandLoggingSession.MaximumRecordCountLimit));
					}
					args.Value = num;
				};
				if (base.ShellUI.ShowDialog(promptDialog) == DialogResult.OK)
				{
					CommandLoggingSession.GetInstance().MaximumRecordCount = exchangeSettings.MaximumRecordCount;
					if (CommandLoggingDialog.GlobalSettings != null)
					{
						CommandLoggingDialog.GlobalSettings.MaximumRecordCount = exchangeSettings.MaximumRecordCount;
					}
				}
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000263FC File Offset: 0x000245FC
		private string RetrieveValueFromRow(DataRowView row, string columnName)
		{
			try
			{
				return (string)row[columnName];
			}
			catch (RowNotInTableException)
			{
			}
			return string.Empty;
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00026434 File Offset: 0x00024634
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0002643B File Offset: 0x0002463B
		internal static string StartDateTime
		{
			get
			{
				return CommandLoggingDialog.startDateTime;
			}
			set
			{
				CommandLoggingDialog.startDateTime = value;
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00026444 File Offset: 0x00024644
		private void resultListView_SelectionChanged(object sender, EventArgs e)
		{
			this.selectedCountLabel.Text = Strings.CommandsSelected(this.resultListView.SelectedIndices.Count);
			StringBuilder stringBuilder = new StringBuilder().AppendLine(Strings.CommandTitle);
			DataRowView dataRowView = this.resultListView.SelectedObject as DataRowView;
			if (dataRowView != null && this.resultListView.SelectedIndices.Count == 1)
			{
				stringBuilder.AppendLine(this.RetrieveValueFromRow(dataRowView, CommandLoggingSession.command));
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(Strings.OutputMessageTitle);
				string value = this.RetrieveValueFromRow(dataRowView, CommandLoggingSession.warning);
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.AppendLine(value);
				}
				string value2 = this.RetrieveValueFromRow(dataRowView, CommandLoggingSession.error);
				if (!string.IsNullOrEmpty(value2))
				{
					stringBuilder.AppendLine(value2);
				}
			}
			else
			{
				stringBuilder.AppendLine().AppendLine(Strings.OutputMessageTitle);
			}
			this.outputTextBox.Text = stringBuilder.ToString();
			this.PrepareActionMenuStrip();
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00026546 File Offset: 0x00024746
		private void PrepareActionMenuStrip()
		{
			this.copyCommandsToolStripMenuItem.Visible = (this.resultListView.SelectedIndices.Count > 0);
			this.separator.Visible = (this.resultListView.SelectedIndices.Count > 0);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00026584 File Offset: 0x00024784
		public static void ShowCommandLoggingDialog()
		{
			lock (CommandLoggingDialog.mutex)
			{
				if (CommandLoggingDialog.commandLoggingThread != null && CommandLoggingDialog.commandLoggingThread.IsAlive)
				{
					if (CommandLoggingDialog.instance != null)
					{
						CommandLoggingDialog.instance.ActivateForm();
					}
				}
				else
				{
					CommandLoggingDialog.commandLoggingThread = new Thread(new ThreadStart(CommandLoggingDialog.ShowModelessInternal));
					CommandLoggingDialog.commandLoggingThread.SetApartmentState(ApartmentState.STA);
					CommandLoggingDialog.commandLoggingThread.Start();
				}
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00026610 File Offset: 0x00024810
		public static void CloseCommandLoggingDialg()
		{
			if (CommandLoggingDialog.instance != null)
			{
				CommandLoggingDialog.instance.CloseForm();
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00026623 File Offset: 0x00024823
		private void CloseForm()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.CloseForm));
				return;
			}
			base.Close();
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00026648 File Offset: 0x00024848
		internal static void LogStart(Guid guid, DateTime startTime, string commandText)
		{
			lock (CommandLoggingDialog.mutex)
			{
				if (CommandLoggingDialog.instance != null)
				{
					CommandLoggingDialog.instance.EnsureThreadSafeToLogStartAndDisplay(guid, startTime, commandText);
				}
				else
				{
					CommandLoggingSession.GetInstance().LogStart(guid, startTime, commandText);
				}
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000266A4 File Offset: 0x000248A4
		private void EnsureThreadSafeToLogStartAndDisplay(Guid guid, DateTime startTime, string commandText)
		{
			if (base.InvokeRequired)
			{
				try
				{
					base.Invoke(new CommandLoggingDialog.LogStartDelegate(this.EnsureThreadSafeToLogStartAndDisplay), new object[]
					{
						guid,
						startTime,
						commandText
					});
					return;
				}
				catch (InvalidOperationException)
				{
					return;
				}
			}
			CommandLoggingSession.GetInstance().LogStart(guid, startTime, commandText);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002670C File Offset: 0x0002490C
		internal static void LogEnd(Guid guid, DateTime endTime)
		{
			lock (CommandLoggingDialog.mutex)
			{
				if (CommandLoggingDialog.instance != null)
				{
					CommandLoggingDialog.instance.EnsureThreadSafeToLogEndAndDisplay(guid, endTime);
				}
				else
				{
					CommandLoggingSession.GetInstance().LogEnd(guid, endTime);
				}
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00026768 File Offset: 0x00024968
		private void EnsureThreadSafeToLogEndAndDisplay(Guid guid, DateTime endTime)
		{
			if (base.InvokeRequired)
			{
				try
				{
					base.Invoke(new CommandLoggingDialog.LogEndDelegate(this.EnsureThreadSafeToLogEndAndDisplay), new object[]
					{
						guid,
						endTime
					});
					return;
				}
				catch (InvalidOperationException)
				{
					return;
				}
			}
			CommandLoggingSession.GetInstance().LogEnd(guid, endTime);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000267CC File Offset: 0x000249CC
		internal static void LogWarning(Guid guid, string warning)
		{
			lock (CommandLoggingDialog.mutex)
			{
				if (CommandLoggingDialog.instance != null)
				{
					CommandLoggingDialog.instance.EnsureThreadSafeToLogWarningAndDisplay(guid, warning);
				}
				else
				{
					CommandLoggingSession.GetInstance().LogWarning(guid, warning);
				}
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00026828 File Offset: 0x00024A28
		private void EnsureThreadSafeToLogWarningAndDisplay(Guid guid, string warning)
		{
			if (base.InvokeRequired)
			{
				try
				{
					base.Invoke(new CommandLoggingDialog.LogWarningDelegate(this.EnsureThreadSafeToLogWarningAndDisplay), new object[]
					{
						guid,
						warning
					});
					return;
				}
				catch (InvalidOperationException)
				{
					return;
				}
			}
			CommandLoggingSession.GetInstance().LogWarning(guid, warning);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00026888 File Offset: 0x00024A88
		internal static void LogError(Guid guid, string error)
		{
			lock (CommandLoggingDialog.mutex)
			{
				if (CommandLoggingDialog.instance != null)
				{
					CommandLoggingDialog.instance.EnsureThreadSafeToLogErrorAndDisplay(guid, error);
				}
				else
				{
					CommandLoggingSession.GetInstance().LogError(guid, error);
				}
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000268E4 File Offset: 0x00024AE4
		private void EnsureThreadSafeToLogErrorAndDisplay(Guid guid, string error)
		{
			if (base.InvokeRequired)
			{
				try
				{
					base.Invoke(new CommandLoggingDialog.LogErrorDelegate(this.EnsureThreadSafeToLogErrorAndDisplay), new object[]
					{
						guid,
						error
					});
					return;
				}
				catch (InvalidOperationException)
				{
					return;
				}
			}
			CommandLoggingSession.GetInstance().LogError(guid, error);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00026944 File Offset: 0x00024B44
		private static void ShowModelessInternal()
		{
			lock (CommandLoggingDialog.mutex)
			{
				if (CommandLoggingDialog.instance == null)
				{
					CommandLoggingDialog.instance = new CommandLoggingDialog();
				}
			}
			CommandLoggingDialog.instance.ActivateForm();
			Application.Run(CommandLoggingDialog.instance);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000269A4 File Offset: 0x00024BA4
		private void ActivateForm()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.ActivateForm));
				return;
			}
			if (base.WindowState == FormWindowState.Minimized)
			{
				base.WindowState = FormWindowState.Normal;
			}
			base.Activate();
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000269D8 File Offset: 0x00024BD8
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x000269DF File Offset: 0x00024BDF
		public static ExchangeSettings GlobalSettings
		{
			get
			{
				return CommandLoggingDialog.globalSettings;
			}
			set
			{
				CommandLoggingDialog.globalSettings = value;
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000269E8 File Offset: 0x00024BE8
		private void LoadCommandLoggingSettings()
		{
			try
			{
				base.SuspendLayout();
				this.splitContainer.SuspendLayout();
				this.switchCommandLoggingToolStripMenuItem.Text = (CommandLoggingSession.GetInstance().CommandLoggingEnabled ? Strings.StopCommandLogging : Strings.StartCommandLogging);
				base.Size = ((CommandLoggingDialog.GlobalSettings != null) ? CommandLoggingDialog.GlobalSettings.CommandLoggingDialogSize : CommandLoggingDialog.DefaultDialogSize);
				base.Location = ((CommandLoggingDialog.GlobalSettings != null) ? CommandLoggingDialog.GlobalSettings.CommandLoggingDialogLocation : CommandLoggingDialog.DefaultDialogLocation);
				this.SplitterDistanceScale = ((CommandLoggingDialog.GlobalSettings != null) ? CommandLoggingDialog.GlobalSettings.CommandLoggingDialogSplitterDistanceScale : CommandLoggingDialog.DefaultSplitterDistanceScale);
				this.privateSettings.LoadDataListViewSettings(this.resultListView);
			}
			finally
			{
				this.splitContainer.ResumeLayout(true);
				base.ResumeLayout(true);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00026AC0 File Offset: 0x00024CC0
		private void SaveCommandLoggingSettings()
		{
			if (base.WindowState == FormWindowState.Maximized)
			{
				return;
			}
			if (CommandLoggingDialog.GlobalSettings != null)
			{
				CommandLoggingDialog.GlobalSettings.CommandLoggingDialogSize = base.Size;
				CommandLoggingDialog.GlobalSettings.CommandLoggingDialogLocation = base.Location;
				CommandLoggingDialog.GlobalSettings.CommandLoggingDialogSplitterDistanceScale = this.SplitterDistanceScale;
				return;
			}
			CommandLoggingDialog.DefaultDialogSize = base.Size;
			CommandLoggingDialog.DefaultDialogLocation = base.Location;
			CommandLoggingDialog.DefaultSplitterDistanceScale = this.SplitterDistanceScale;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00026B30 File Offset: 0x00024D30
		private float SplitterContainerSize
		{
			get
			{
				return (float)((this.splitContainer.Orientation == Orientation.Horizontal) ? (this.splitContainer.Panel1.Height + this.splitContainer.Panel2.Height) : (this.splitContainer.Panel1.Width + this.splitContainer.Panel2.Width));
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00026B8F File Offset: 0x00024D8F
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x00026BA4 File Offset: 0x00024DA4
		private float SplitterDistanceScale
		{
			get
			{
				return (float)this.splitContainer.SplitterDistance / this.SplitterContainerSize;
			}
			set
			{
				if (value != this.SplitterDistanceScale)
				{
					this.splitContainer.SplitterDistance = (int)(value * this.SplitterContainerSize);
				}
			}
		}

		// Token: 0x0400046D RID: 1133
		private static Thread commandLoggingThread;

		// Token: 0x0400046E RID: 1134
		private static object mutex = new object();

		// Token: 0x0400046F RID: 1135
		private static ExchangeSettingsProvider settingsProvider;

		// Token: 0x04000470 RID: 1136
		internal static CommandLoggingDialog instance;

		// Token: 0x04000471 RID: 1137
		private Command exportListCommand = new Command();

		// Token: 0x04000472 RID: 1138
		private static Command viewCommandLoggingCommand = null;

		// Token: 0x04000473 RID: 1139
		private ToolStripSeparator separator = new ToolStripSeparator();

		// Token: 0x04000474 RID: 1140
		private DataListViewSettings privateSettings = new DataListViewSettings(new Component());

		// Token: 0x04000475 RID: 1141
		private static string startDateTime = string.Empty;

		// Token: 0x04000476 RID: 1142
		private static ExchangeSettings globalSettings;

		// Token: 0x04000477 RID: 1143
		internal static Size DefaultDialogSize = new Size(500, 400);

		// Token: 0x04000478 RID: 1144
		internal static Point DefaultDialogLocation = WinformsHelper.GetCentralLocation(CommandLoggingDialog.DefaultDialogSize);

		// Token: 0x04000479 RID: 1145
		internal static float DefaultSplitterDistanceScale = 0.7f;

		// Token: 0x0200011A RID: 282
		// (Invoke) Token: 0x06000AE3 RID: 2787
		private delegate void LogStartDelegate(Guid guid, DateTime datetime, string command);

		// Token: 0x0200011B RID: 283
		// (Invoke) Token: 0x06000AE7 RID: 2791
		private delegate void LogEndDelegate(Guid guid, DateTime datetime);

		// Token: 0x0200011C RID: 284
		// (Invoke) Token: 0x06000AEB RID: 2795
		private delegate void LogErrorDelegate(Guid guid, string error);

		// Token: 0x0200011D RID: 285
		// (Invoke) Token: 0x06000AEF RID: 2799
		private delegate void LogWarningDelegate(Guid guid, string warning);
	}
}
