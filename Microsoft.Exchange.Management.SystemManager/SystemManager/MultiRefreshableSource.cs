using System;
using System.Collections.Generic;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class MultiRefreshableSource : IRefreshableNotification, IRefreshable
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000A369 File Offset: 0x00008569
		public MultiRefreshableSource()
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A37C File Offset: 0x0000857C
		public MultiRefreshableSource(params IRefreshable[] refreshableSources)
		{
			if (refreshableSources == null)
			{
				throw new ArgumentNullException("refreshableSources");
			}
			for (int i = 0; i < refreshableSources.Length; i++)
			{
				if (refreshableSources[i] == null)
				{
					throw new ArgumentNullException();
				}
			}
			this.refreshableSources.AddRange(refreshableSources);
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000A3D1 File Offset: 0x000085D1
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000A3D9 File Offset: 0x000085D9
		public bool Refreshing
		{
			get
			{
				return this.refreshing;
			}
			private set
			{
				if (this.Refreshing != value)
				{
					if (value)
					{
						this.Refreshed = true;
					}
					this.refreshing = value;
					this.OnRefreshingChanged(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000A401 File Offset: 0x00008601
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000A409 File Offset: 0x00008609
		public bool Refreshed
		{
			get
			{
				return this.refreshed;
			}
			private set
			{
				this.refreshed = value;
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060002B7 RID: 695 RVA: 0x0000A414 File Offset: 0x00008614
		// (remove) Token: 0x060002B8 RID: 696 RVA: 0x0000A44C File Offset: 0x0000864C
		public event EventHandler RefreshingChanged;

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000A481 File Offset: 0x00008681
		public List<IRefreshable> RefreshableSources
		{
			get
			{
				return this.refreshableSources;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000A48C File Offset: 0x0000868C
		public void Refresh(IProgress progress)
		{
			if (this.refreshableSources.Count > 0)
			{
				if (this.lastRefresher != null)
				{
					this.lastRefresher.RefreshingChanged -= this.lastRefresher_RefreshingChanged;
					if (this.lastRefresher.IsRefreshing)
					{
						this.lastRefresher.Cancel();
					}
				}
				this.lastRefresher = new MultiRefreshableSource.AggregateRefresh(this.refreshableSources, progress);
				this.lastRefresher.RefreshingChanged += this.lastRefresher_RefreshingChanged;
				this.lastRefresher.StartRefresh();
				return;
			}
			progress.ReportProgress(100, 100, string.Empty);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000A522 File Offset: 0x00008722
		private void lastRefresher_RefreshingChanged(object sender, EventArgs e)
		{
			this.Refreshing = ((MultiRefreshableSource.AggregateRefresh)sender).IsRefreshing;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000A535 File Offset: 0x00008735
		private void OnRefreshingChanged(object sender, EventArgs e)
		{
			if (this.RefreshingChanged != null)
			{
				this.RefreshingChanged(this, e);
			}
		}

		// Token: 0x040000B7 RID: 183
		private List<IRefreshable> refreshableSources = new List<IRefreshable>();

		// Token: 0x040000B8 RID: 184
		private MultiRefreshableSource.AggregateRefresh lastRefresher;

		// Token: 0x040000B9 RID: 185
		private bool refreshing;

		// Token: 0x040000BA RID: 186
		private bool refreshed;

		// Token: 0x02000045 RID: 69
		[Serializable]
		private class AggregateRefresh : IProgress
		{
			// Token: 0x060002BD RID: 701 RVA: 0x0000A54C File Offset: 0x0000874C
			public AggregateRefresh(List<IRefreshable> dataSources, IProgress progress)
			{
				this.refreshableSources.AddRange(dataSources);
				this.progress = progress;
			}

			// Token: 0x14000017 RID: 23
			// (add) Token: 0x060002BE RID: 702 RVA: 0x0000A574 File Offset: 0x00008774
			// (remove) Token: 0x060002BF RID: 703 RVA: 0x0000A5AC File Offset: 0x000087AC
			public event EventHandler RefreshingChanged;

			// Token: 0x060002C0 RID: 704 RVA: 0x0000A5E4 File Offset: 0x000087E4
			public void StartRefresh()
			{
				this.IsRefreshing = true;
				IRefreshable refreshable = this.refreshableSources[this.currentDataSourceIndex];
				if (refreshable is IRefreshableNotification)
				{
					(refreshable as IRefreshableNotification).RefreshingChanged += this.RefreshableNotification_RefreshingChanged;
					refreshable.Refresh(this);
					return;
				}
				refreshable.Refresh(NullProgress.Value);
				this.ReportProgress(100, 100, "");
				this.RefreshNextDataSource();
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x0000A651 File Offset: 0x00008851
			private void OnRefreshingChanged()
			{
				if (this.RefreshingChanged != null)
				{
					this.RefreshingChanged(this, EventArgs.Empty);
				}
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x0000A66C File Offset: 0x0000886C
			private void RefreshNextDataSource()
			{
				if (this.IsRefreshing)
				{
					this.currentDataSourceIndex++;
					if (this.currentDataSourceIndex < this.refreshableSources.Count)
					{
						this.StartRefresh();
						return;
					}
					this.IsRefreshing = false;
				}
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x0000A6A8 File Offset: 0x000088A8
			public void Cancel()
			{
				if (this.IsRefreshing && this.currentDataSourceIndex < this.refreshableSources.Count)
				{
					IRefreshable refreshable = this.refreshableSources[this.currentDataSourceIndex];
					if (refreshable is RefreshableComponent)
					{
						(refreshable as RefreshableComponent).CancelRefresh();
					}
					if (refreshable is IRefreshableNotification)
					{
						(refreshable as IRefreshableNotification).RefreshingChanged -= this.RefreshableNotification_RefreshingChanged;
					}
					this.cancelled = true;
					this.IsRefreshing = false;
				}
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x0000A724 File Offset: 0x00008924
			private void RefreshableNotification_RefreshingChanged(object sender, EventArgs e)
			{
				IRefreshableNotification refreshableNotification = sender as IRefreshableNotification;
				if (!refreshableNotification.Refreshing)
				{
					refreshableNotification.RefreshingChanged -= this.RefreshableNotification_RefreshingChanged;
					this.RefreshNextDataSource();
				}
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000A758 File Offset: 0x00008958
			// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000A760 File Offset: 0x00008960
			public bool IsRefreshing
			{
				get
				{
					return this.isRefreshing;
				}
				private set
				{
					if (this.IsRefreshing != value)
					{
						this.isRefreshing = value;
						this.OnRefreshingChanged();
					}
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000A778 File Offset: 0x00008978
			public bool Canceled
			{
				get
				{
					return this.cancelled;
				}
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x0000A780 File Offset: 0x00008980
			public void ReportProgress(int workProcessed, int totalWork, string statusText)
			{
				if (this.IsRefreshing && this.progress != null)
				{
					if (this.currentDataSourceIndex == this.refreshableSources.Count - 1 && workProcessed == totalWork)
					{
						this.progress.ReportProgress(100, 100, statusText);
						return;
					}
					if (totalWork > 0 && this.refreshableSources.Count > 0)
					{
						int num = this.currentDataSourceIndex * 100 / this.refreshableSources.Count + workProcessed * 100 / totalWork / this.refreshableSources.Count;
						this.progress.ReportProgress(num, 100, statusText);
					}
				}
			}

			// Token: 0x040000BC RID: 188
			private bool cancelled;

			// Token: 0x040000BD RID: 189
			private bool isRefreshing;

			// Token: 0x040000BE RID: 190
			private IProgress progress;

			// Token: 0x040000BF RID: 191
			private List<IRefreshable> refreshableSources = new List<IRefreshable>();

			// Token: 0x040000C0 RID: 192
			private int currentDataSourceIndex;
		}
	}
}
