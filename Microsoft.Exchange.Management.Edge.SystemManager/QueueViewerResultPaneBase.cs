using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x02000005 RID: 5
	public abstract class QueueViewerResultPaneBase : DataListViewResultPane
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002C48 File Offset: 0x00000E48
		public QueueViewerResultPaneBase()
		{
			this.icons = new IconLibrary();
			this.icons.Icons.Add("Filter", Icons.CreateFilter);
			this.objectList = new ObjectList();
			this.objectList.FilterControl.SupportsOrOperator = false;
			this.objectList.DataSource = null;
			this.objectList.Dock = DockStyle.Fill;
			this.objectList.ListView.IdentityProperty = "Identity";
			base.ListControl = this.objectList.ListView;
			base.FilterControl = this.objectList.FilterControl;
			this.ObjectList.FilterExpressionChanged += this.objectList_FilterExpressionChanged;
			base.Controls.Add(this.objectList);
			this.Dock = DockStyle.Fill;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002D30 File Offset: 0x00000F30
		private void objectList_FilterExpressionChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.ObjectList.FilterControl.Expression))
			{
				base.Icon = this.icons.GetIcon("Filter", -1);
				return;
			}
			base.Icon = null;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002D68 File Offset: 0x00000F68
		internal ObjectList ObjectList
		{
			get
			{
				return this.objectList;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002D70 File Offset: 0x00000F70
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002D78 File Offset: 0x00000F78
		[DefaultValue(null)]
		internal QueueViewerDataSource Datasource
		{
			get
			{
				return this.datasource;
			}
			set
			{
				if (this.Datasource != value)
				{
					this.datasource = value;
					if (this.Datasource != null)
					{
						if (this.Datasource.GetCommandParameters.Contains("server"))
						{
							this.Datasource.GetCommandParameters.Remove("server");
						}
						if (!string.IsNullOrEmpty(this.ServerName))
						{
							this.Datasource.GetCommandParameters.AddWithValue("server", this.ServerName);
						}
						this.RefreshableDataSource = this.Datasource;
						this.SetUpDatasourceColumns();
						this.Datasource.BeginInit();
						this.Datasource.PageSize = this.pageSize;
						this.ObjectList.DataSource = this.Datasource;
						this.ObjectList.FilterControl.PersistedExpression = base.PrivateSettings.FilterExpression;
						this.Datasource.EndInit();
						base.SetRefreshWhenActivated();
					}
					this.OnDatasourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002E70 File Offset: 0x00001070
		protected virtual void OnDatasourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[QueueViewerResultPaneBase.EventDatasourceChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000014 RID: 20 RVA: 0x00002E9E File Offset: 0x0000109E
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x00002EB1 File Offset: 0x000010B1
		public event EventHandler DatasourceChanged
		{
			add
			{
				base.Events.AddHandler(QueueViewerResultPaneBase.EventDatasourceChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(QueueViewerResultPaneBase.EventDatasourceChanged, value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002EC4 File Offset: 0x000010C4
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002ECC File Offset: 0x000010CC
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
					if (this.Datasource != null)
					{
						this.Datasource.PageSize = this.pageSize;
						this.RefreshDatasource();
					}
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002EFD File Offset: 0x000010FD
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002F08 File Offset: 0x00001108
		internal virtual string ServerName
		{
			get
			{
				return this.serverName;
			}
			set
			{
				if (value != this.serverName)
				{
					this.serverName = value;
					if (this.Datasource != null)
					{
						if (this.Datasource.GetCommandParameters.Contains("server"))
						{
							this.Datasource.GetCommandParameters.Remove("server");
						}
						if (!string.IsNullOrEmpty(this.ServerName))
						{
							this.Datasource.GetCommandParameters.AddWithValue("server", this.serverName);
						}
						this.RefreshDatasource();
					}
				}
			}
		}

		// Token: 0x0600001A RID: 26
		protected abstract void SetUpDatasourceColumns();

		// Token: 0x0600001B RID: 27 RVA: 0x00002F8D File Offset: 0x0000118D
		private void RefreshDatasource()
		{
			if (this.Datasource != null && base.IsHandleCreated && !base.DesignMode)
			{
				base.SetRefreshWhenActivated();
			}
		}

		// Token: 0x0400000A RID: 10
		private const string FilterIconName = "Filter";

		// Token: 0x0400000B RID: 11
		private ObjectList objectList;

		// Token: 0x0400000C RID: 12
		private QueueViewerDataSource datasource;

		// Token: 0x0400000D RID: 13
		private IconLibrary icons;

		// Token: 0x0400000E RID: 14
		private static readonly object EventDatasourceChanged = new object();

		// Token: 0x0400000F RID: 15
		private int pageSize = 1000;

		// Token: 0x04000010 RID: 16
		private string serverName = string.Empty;
	}
}
