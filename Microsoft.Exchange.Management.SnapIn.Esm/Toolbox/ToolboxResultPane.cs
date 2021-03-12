using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SnapIn.Esm.Toolbox
{
	// Token: 0x0200000D RID: 13
	public class ToolboxResultPane : CaptionedResultPane
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003958 File Offset: 0x00001B58
		public ToolboxResultPane()
		{
			base.Name = "ToolboxResultPane";
			base.SuspendLayout();
			base.CaptionText = Strings.ToolboxResultPaneCaptionText;
			base.Icon = Icons.Toolbox;
			base.ListControl = new DataListView();
			base.ListControl.VirtualMode = false;
			base.ListControl.Dock = DockStyle.Fill;
			base.ListControl.Name = "toolboxList";
			base.ListControl.AutoArrange = false;
			base.Controls.Add(base.ListControl);
			base.Controls.SetChildIndex(base.ListControl, 0);
			base.ListControl.IconLibrary = Tool.ToolIcons;
			base.ListControl.MultiSelect = false;
			base.ListControl.ImagePropertyName = "IconKey";
			base.ListControl.View = View.Tile;
			base.ListControl.AutoGenerateColumns = false;
			int width = (base.ListControl.ClientRectangle.Width < 300) ? 100 : (base.ListControl.ClientRectangle.Width / 3);
			base.ListControl.AvailableColumns.Add("Name", Strings.NameColumn, width, true);
			base.ListControl.AvailableColumns.Add("Description", Strings.ToolDescriptionColumn, width, true);
			base.ListControl.AvailableColumns.Add("Version", Strings.VersionColumn, width, true);
			base.ListControl.IdentityProperty = "Name";
			base.ListControl.SortProperty = "Name";
			base.ViewModeCommands.Remove(base.ListControl.ShowColumnPickerCommand);
			base.ExportListCommands.Clear();
			base.ListControl.UpdateItem += delegate(object sender, ItemCheckedEventArgs e)
			{
				Tool tool = base.ListControl.GetRowFromItem(e.Item) as Tool;
				e.Item.Group = base.ListControl.Groups[tool.GroupName.ToUpper()];
				e.Item.UseItemStyleForSubItems = false;
				for (int i = 1; i < e.Item.SubItems.Count; i++)
				{
					e.Item.SubItems[i].ForeColor = SystemColors.GrayText;
				}
			};
			base.ListControl.Layout += delegate(object param0, LayoutEventArgs param1)
			{
				base.ListControl.TileSize = new Size(Math.Max(1, base.ListControl.ClientSize.Width - SystemInformation.VerticalScrollBarWidth), (Control.DefaultFont.Height + 3) * 3);
			};
			Command command = new Command();
			command.Name = "OpenTool";
			command.Icon = Icons.OpenTool;
			command.Text = new LocalizedString(Strings.OpenTool);
			command.Execute += delegate(object param0, EventArgs param1)
			{
				Tool tool = (Tool)base.SelectedObject;
				if (tool != null)
				{
					if (tool.Type == "DynamicURL")
					{
						this.OpenURLTool(tool);
						return;
					}
					if (tool.Type == "StaticURL")
					{
						this.OpenURL(tool.CommandParameters);
						return;
					}
					this.OpenExecutableTool(tool);
				}
			};
			base.CustomSelectionCommands.Add(command);
			base.ListControl.ShowSelectionPropertiesCommand = command;
			Command command2 = new Command();
			command2.Name = "OpenToolDownloadWebsite";
			command2.Icon = Icons.OpenToolDownloadWebsite;
			command2.Execute += delegate(object param0, EventArgs param1)
			{
				this.OpenURL("http://go.microsoft.com/fwlink/?LinkId=186692");
			};
			command2.Text = new LocalizedString(Strings.FindTools);
			base.ResultPaneCommands.Add(command2);
			base.ListControl.CreatingItemsForRows += delegate(object param0, EventArgs param1)
			{
				base.ListControl.Groups.Clear();
				Comparison<ListViewGroup> comparison = new Comparison<ListViewGroup>(this.ListViewGroupComparer);
				ListViewGroup[] array = new ListViewGroup[Tool.AvailableGroups.Count];
				for (int i = 0; i < Tool.AvailableGroups.Count; i++)
				{
					array[i] = new ListViewGroup(Tool.AvailableGroups[i].ToUpper(), Tool.AvailableGroups[i]);
				}
				Array.Sort<ListViewGroup>(array, comparison);
				base.ListControl.Groups.AddRange(array);
			};
			base.ResumeLayout();
			base.SubscribedRefreshCategories.Remove(ResultPane.ConfigurationDomainControllerRefreshCategory);
			CommandLoggingDialog.GetCommandLoggingCommand().Visible = false;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003C68 File Offset: 0x00001E68
		private void OpenExecutableTool(Tool tool)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = tool.Command.Trim();
			processStartInfo.Arguments = tool.CommandFile.Trim() + " " + tool.CommandParameters.Trim();
			processStartInfo.WorkingDirectory = tool.WorkingFolder.Trim();
			try
			{
				Process.Start(processStartInfo);
			}
			catch (Win32Exception)
			{
				base.ShowError(Strings.CommandMissing(tool.Name, tool.Command));
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003CFC File Offset: 0x00001EFC
		private void OpenURLTool(Tool tool)
		{
			this.OpenURL(tool.Command);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003D0C File Offset: 0x00001F0C
		private void OpenURL(string url)
		{
			try
			{
				WinformsHelper.OpenUrl(new Uri(url));
			}
			catch (UrlHandlerNotFoundException ex)
			{
				base.ShowError(ex.Message);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003D48 File Offset: 0x00001F48
		private int ListViewGroupComparer(ListViewGroup group1, ListViewGroup group2)
		{
			return string.Compare(group1.Header, group2.Header, true);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003D5C File Offset: 0x00001F5C
		public override string SelectionHelpTopic
		{
			get
			{
				return SelectionHelpTopics.Tool;
			}
		}
	}
}
