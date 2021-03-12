using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ManagementGUI.Tasks;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200014C RID: 332
	public class ObjectList : ExchangeUserControl
	{
		// Token: 0x06000D71 RID: 3441 RVA: 0x000325C9 File Offset: 0x000307C9
		public ObjectList()
		{
			this.InitializeComponent();
			base.Name = "ObjectList";
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x000325E4 File Offset: 0x000307E4
		private void InitializeComponent()
		{
			base.SuspendLayout();
			this.listview = new DataListView();
			this.listview.TabIndex = 1;
			this.listview.Dock = DockStyle.Fill;
			this.listview.AllowColumnReorder = true;
			this.listview.FullRowSelect = true;
			this.listview.Name = "listView";
			this.listview.View = View.Details;
			this.listview.ShowFilter = false;
			this.filter = new FilterControl();
			this.filter.Dock = DockStyle.Top;
			this.filter.TabIndex = 0;
			this.filter.Visible = false;
			this.filter.Name = "filter";
			base.Controls.Add(this.listview);
			base.Controls.Add(this.filter);
			base.ResumeLayout();
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x000326C1 File Offset: 0x000308C1
		// (set) Token: 0x06000D74 RID: 3444 RVA: 0x000326CC File Offset: 0x000308CC
		[DefaultValue(null)]
		public object DataSource
		{
			get
			{
				return this.datasource;
			}
			set
			{
				if (this.datasource != value)
				{
					this.datasource = value;
					this.listview.DataSource = this.datasource;
					IAdvancedBindingListView advancedBindingListView = this.datasource as IAdvancedBindingListView;
					if (advancedBindingListView != null && advancedBindingListView.SupportsFiltering)
					{
						this.filter.DataSource = advancedBindingListView;
					}
					IPagedList pagedList = this.datasource as IPagedList;
					if (pagedList != null && pagedList.SupportsPaging)
					{
						this.PagingControl.Visible = true;
						this.PagingControl.DataSource = pagedList;
					}
				}
			}
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000D75 RID: 3445 RVA: 0x0003274C File Offset: 0x0003094C
		// (remove) Token: 0x06000D76 RID: 3446 RVA: 0x0003275A File Offset: 0x0003095A
		public new event EventHandler DoubleClick
		{
			add
			{
				this.listview.DoubleClick += value;
			}
			remove
			{
				this.listview.DoubleClick -= value;
			}
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000D77 RID: 3447 RVA: 0x00032768 File Offset: 0x00030968
		// (remove) Token: 0x06000D78 RID: 3448 RVA: 0x00032776 File Offset: 0x00030976
		public event EventHandler SelectionChanged
		{
			add
			{
				this.listview.SelectionChanged += value;
			}
			remove
			{
				this.listview.SelectionChanged -= value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00032784 File Offset: 0x00030984
		public ListView.SelectedIndexCollection SelectedIndices
		{
			get
			{
				return this.listview.SelectedIndices;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00032791 File Offset: 0x00030991
		public ListView.ListViewItemCollection Items
		{
			get
			{
				return this.listview.Items;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0003279E File Offset: 0x0003099E
		public string FilterExpression
		{
			get
			{
				return this.filter.Expression;
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000D7C RID: 3452 RVA: 0x000327AB File Offset: 0x000309AB
		// (remove) Token: 0x06000D7D RID: 3453 RVA: 0x000327B9 File Offset: 0x000309B9
		public event EventHandler FilterExpressionChanged
		{
			add
			{
				this.filter.ExpressionChanged += value;
			}
			remove
			{
				this.filter.ExpressionChanged -= value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000327C7 File Offset: 0x000309C7
		public DataListView ListView
		{
			get
			{
				return this.listview;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x000327CF File Offset: 0x000309CF
		public FilterControl FilterControl
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x000327D8 File Offset: 0x000309D8
		public PagingControl PagingControl
		{
			get
			{
				if (this.paging == null)
				{
					this.paging = new PagingControl();
					this.paging.Dock = DockStyle.Bottom;
					this.paging.TabIndex = 2;
					this.paging.BackColor = SystemColors.Control;
					this.paging.Visible = false;
					this.paging.Name = "paging";
					base.Controls.Add(this.paging);
				}
				return this.paging;
			}
		}

		// Token: 0x0400055D RID: 1373
		private FilterControl filter;

		// Token: 0x0400055E RID: 1374
		private DataListView listview;

		// Token: 0x0400055F RID: 1375
		private PagingControl paging;

		// Token: 0x04000560 RID: 1376
		private object datasource;
	}
}
