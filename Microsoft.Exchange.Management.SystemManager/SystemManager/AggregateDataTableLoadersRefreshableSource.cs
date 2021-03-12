using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class AggregateDataTableLoadersRefreshableSource : Component, IRefreshableNotification, IRefreshable, ISupportFastRefresh
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x000057FB File Offset: 0x000039FB
		public AggregateDataTableLoadersRefreshableSource(params DataTableLoader[] dataTableLoaders)
		{
			this.DataTableLoaders.AddRange(dataTableLoaders);
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005825 File Offset: 0x00003A25
		public List<DataTableLoader> DataTableLoaders
		{
			get
			{
				return this.dataTableLoaders;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000582D File Offset: 0x00003A2D
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00005835 File Offset: 0x00003A35
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
						this.refreshed = true;
					}
					this.refreshing = value;
					this.OnRefreshingChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000585C File Offset: 0x00003A5C
		public bool Refreshed
		{
			get
			{
				if (!this.refreshed)
				{
					bool flag = true;
					foreach (DataTableLoader dataTableLoader in this.DataTableLoaders)
					{
						flag = (flag && dataTableLoader.Refreshed);
						if (!flag)
						{
							break;
						}
					}
					this.refreshed = flag;
				}
				return this.refreshed;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060000FC RID: 252 RVA: 0x000058D0 File Offset: 0x00003AD0
		// (remove) Token: 0x060000FD RID: 253 RVA: 0x00005908 File Offset: 0x00003B08
		public event EventHandler RefreshingChanged;

		// Token: 0x060000FE RID: 254 RVA: 0x00005940 File Offset: 0x00003B40
		protected virtual void OnRefreshingChanged(EventArgs e)
		{
			EventHandler refreshingChanged = this.RefreshingChanged;
			if (refreshingChanged != null)
			{
				refreshingChanged(this, e);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000595F File Offset: 0x00003B5F
		void ISupportFastRefresh.Refresh(IProgress progress, object identity)
		{
			this.RefreshCore(progress, identity);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005969 File Offset: 0x00003B69
		void ISupportFastRefresh.Refresh(IProgress progress, object[] identities, RefreshRequestPriority priority)
		{
			if (identities.Length == 1)
			{
				this.RefreshCore(progress, identities[0]);
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005981 File Offset: 0x00003B81
		public void Refresh(IProgress progress)
		{
			this.RefreshCore(progress, null);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000598C File Offset: 0x00003B8C
		private void RefreshCore(IProgress progress, object identity)
		{
			MultiRefreshableSource multiRefreshableSource = this.CreateRefresher(identity);
			this.refreshers.Add(multiRefreshableSource);
			this.Refreshing = true;
			multiRefreshableSource.RefreshingChanged += this.Refresher_RefreshingChanged;
			multiRefreshableSource.Refresh(progress);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000059D0 File Offset: 0x00003BD0
		private void Refresher_RefreshingChanged(object sender, EventArgs e)
		{
			MultiRefreshableSource multiRefreshableSource = sender as MultiRefreshableSource;
			if (!multiRefreshableSource.Refreshing)
			{
				multiRefreshableSource.RefreshingChanged -= this.Refresher_RefreshingChanged;
				this.refreshers.Remove(multiRefreshableSource);
				if (this.refreshers.Count == 0)
				{
					this.Refreshing = false;
				}
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005A20 File Offset: 0x00003C20
		private MultiRefreshableSource CreateRefresher(object identity)
		{
			MultiRefreshableSource multiRefreshableSource = new MultiRefreshableSource();
			if (identity == null)
			{
				multiRefreshableSource.RefreshableSources.AddRange(this.DataTableLoaders.ToArray());
			}
			else
			{
				foreach (DataTableLoader dataTableLoader in this.DataTableLoaders)
				{
					multiRefreshableSource.RefreshableSources.Add(new SingleRowRefreshObject(identity, dataTableLoader));
				}
			}
			return multiRefreshableSource;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005AA0 File Offset: 0x00003CA0
		void ISupportFastRefresh.Remove(object identity)
		{
			foreach (DataTableLoader dataTableLoader in this.DataTableLoaders)
			{
				dataTableLoader.Remove(identity);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005AF4 File Offset: 0x00003CF4
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00005AFC File Offset: 0x00003CFC
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				if (base.Site != value)
				{
					base.Site = value;
					if (this.dataTableLoaders != null)
					{
						foreach (DataTableLoader dataTableLoader in this.dataTableLoaders)
						{
							dataTableLoader.Site = value;
						}
					}
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005B68 File Offset: 0x00003D68
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.dataTableLoaders != null)
			{
				this.dataTableLoaders.Clear();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000049 RID: 73
		private List<MultiRefreshableSource> refreshers = new List<MultiRefreshableSource>();

		// Token: 0x0400004A RID: 74
		private List<DataTableLoader> dataTableLoaders = new List<DataTableLoader>();

		// Token: 0x0400004B RID: 75
		private bool refreshing;

		// Token: 0x0400004C RID: 76
		private bool refreshed;
	}
}
