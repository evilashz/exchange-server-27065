using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Search.Platform.Parallax.DataLoad;

namespace Microsoft.Exchange.VariantConfiguration.DataLoad
{
	// Token: 0x0200000B RID: 11
	internal class VariantConfigurationDataLoader : DataLoader
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002B00 File Offset: 0x00000D00
		public VariantConfigurationDataLoader(IDataSourceReader dataSourceReader, IDataTransformation transformation, IEnumerable<string> preloadDataSources) : base(transformation)
		{
			this.preloadDataSources = preloadDataSources;
			this.dataSourcesLock = new object();
			this.loadedDataSources = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.dataSourceReader = dataSourceReader;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B34 File Offset: 0x00000D34
		public void ForceLoad(IEnumerable<string> dataSources)
		{
			lock (this.dataSourcesLock)
			{
				this.Load(dataSources);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B78 File Offset: 0x00000D78
		public void ReloadIfLoaded()
		{
			this.ReloadIfLoaded(this.loadedDataSources);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B88 File Offset: 0x00000D88
		public void ReloadIfLoaded(IEnumerable<string> dataSources)
		{
			if (this.AreAnyLoaded(dataSources))
			{
				lock (this.dataSourcesLock)
				{
					IEnumerable<string> enumerable = dataSources.Intersect(this.loadedDataSources, StringComparer.OrdinalIgnoreCase);
					if (enumerable.Count<string>() > 0)
					{
						this.Load(enumerable);
					}
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public void LoadIfNotLoaded(IEnumerable<string> dataSources)
		{
			if (!this.AreAllLoaded(dataSources))
			{
				lock (this.dataSourcesLock)
				{
					IEnumerable<string> enumerable = dataSources.Except(this.loadedDataSources, StringComparer.OrdinalIgnoreCase);
					if (enumerable.Count<string>() > 0)
					{
						this.Load(enumerable);
					}
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C58 File Offset: 0x00000E58
		protected override void OnInitialized()
		{
			base.OnInitialized();
			if (this.preloadDataSources != null && this.preloadDataSources.Count<string>() > 0)
			{
				this.ForceLoad(this.preloadDataSources);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002CF0 File Offset: 0x00000EF0
		private void Load(IEnumerable<string> dataSources)
		{
			if (base.ExecuteTransaction(delegate(TransactionContext context)
			{
				foreach (string text in dataSources)
				{
					Func<TextReader> contentReader = this.dataSourceReader.GetContentReader(text);
					context.LoadDataSource(text, contentReader);
				}
				return 0;
			}, dataSources))
			{
				HashSet<string> hashSet = new HashSet<string>(this.loadedDataSources, StringComparer.OrdinalIgnoreCase);
				foreach (string item in dataSources)
				{
					hashSet.Add(item);
				}
				this.loadedDataSources = hashSet;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D84 File Offset: 0x00000F84
		private bool AreAnyLoaded(IEnumerable<string> dataSources)
		{
			return dataSources.Intersect(this.loadedDataSources, StringComparer.OrdinalIgnoreCase).Count<string>() > 0;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D9F File Offset: 0x00000F9F
		private bool AreAllLoaded(IEnumerable<string> dataSources)
		{
			return dataSources.Except(this.loadedDataSources, StringComparer.OrdinalIgnoreCase).Count<string>() == 0;
		}

		// Token: 0x04000015 RID: 21
		private readonly object dataSourcesLock;

		// Token: 0x04000016 RID: 22
		private readonly IEnumerable<string> preloadDataSources;

		// Token: 0x04000017 RID: 23
		private readonly IDataSourceReader dataSourceReader;

		// Token: 0x04000018 RID: 24
		private HashSet<string> loadedDataSources;
	}
}
