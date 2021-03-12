using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000028 RID: 40
	public class AdvancedBindingSource : BindingSource, IAdvancedBindingListView, IBindingListView, IBindingList, IList, ICollection, IEnumerable
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00007EFA File Offset: 0x000060FA
		public AdvancedBindingSource()
		{
			base.DataSourceChanged += this.AdvancedBindingSource_DataSourceChanged;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007F14 File Offset: 0x00006114
		private void AdvancedBindingSource_DataSourceChanged(object sender, EventArgs e)
		{
			IAdvancedBindingListView advancedBindingListView = this.GetAdvancedBindingListView();
			if (advancedBindingListView != this.currentList)
			{
				if (this.currentList != null)
				{
					this.currentList.FilteringChanged -= this.CurrentList_FilteringChanged;
				}
				this.currentList = advancedBindingListView;
				if (this.currentList != null)
				{
					this.currentList.FilteringChanged += this.CurrentList_FilteringChanged;
				}
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007F78 File Offset: 0x00006178
		private IAdvancedBindingListView GetAdvancedBindingListView()
		{
			if (base.List == null)
			{
				return null;
			}
			IAdvancedBindingListView advancedBindingListView = base.List as IAdvancedBindingListView;
			if (advancedBindingListView == null)
			{
				throw new NotSupportedException("IAdvancedBindingListView are required");
			}
			return advancedBindingListView;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007FAA File Offset: 0x000061AA
		private void CurrentList_FilteringChanged(object sender, EventArgs e)
		{
			this.OnFilteringChanged(EventArgs.Empty);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007FB7 File Offset: 0x000061B7
		public virtual bool IsSortSupported(string propertyName)
		{
			return this.currentList != null && this.currentList.IsSortSupported(propertyName);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007FCF File Offset: 0x000061CF
		public virtual bool IsFilterSupported(string propertyName)
		{
			return this.currentList != null && this.currentList.IsFilterSupported(propertyName);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007FE7 File Offset: 0x000061E7
		public virtual void ApplyFilter(QueryFilter filter)
		{
			if (this.currentList == null)
			{
				return;
			}
			this.currentList.ApplyFilter(filter);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007FFE File Offset: 0x000061FE
		public virtual QueryFilter QueryFilter
		{
			get
			{
				if (this.currentList != null)
				{
					return this.currentList.QueryFilter;
				}
				return null;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008015 File Offset: 0x00006215
		public virtual bool Filtering
		{
			get
			{
				return this.currentList != null && this.currentList.Filtering;
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060001EE RID: 494 RVA: 0x0000802C File Offset: 0x0000622C
		// (remove) Token: 0x060001EF RID: 495 RVA: 0x0000803F File Offset: 0x0000623F
		public event EventHandler FilteringChanged
		{
			add
			{
				base.Events.AddHandler(AdvancedBindingSource.EventFilteringChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(AdvancedBindingSource.EventFilteringChanged, value);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008052 File Offset: 0x00006252
		protected virtual void OnFilteringChanged(EventArgs e)
		{
			if (base.Events[AdvancedBindingSource.EventFilteringChanged] != null)
			{
				((EventHandler)base.Events[AdvancedBindingSource.EventFilteringChanged])(this, e);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008082 File Offset: 0x00006282
		public virtual bool SupportCancelFiltering
		{
			get
			{
				return this.currentList != null && this.currentList.SupportCancelFiltering;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008099 File Offset: 0x00006299
		public virtual void CancelFiltering()
		{
			if (this.currentList == null)
			{
				return;
			}
			this.currentList.CancelFiltering();
		}

		// Token: 0x0400007C RID: 124
		private IAdvancedBindingListView currentList;

		// Token: 0x0400007D RID: 125
		private static object EventFilteringChanged = new object();
	}
}
