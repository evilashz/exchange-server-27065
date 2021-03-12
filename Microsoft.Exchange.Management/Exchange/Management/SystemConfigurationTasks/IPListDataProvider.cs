using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.IPFilter;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A62 RID: 2658
	internal class IPListDataProvider : IConfigDataProvider
	{
		// Token: 0x06005F1A RID: 24346 RVA: 0x0018EC0E File Offset: 0x0018CE0E
		public IPListDataProvider(string machineName)
		{
			this.connection = new IPFilterRpcClient(machineName);
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x0018EC24 File Offset: 0x0018CE24
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			int index = ((IPListEntryIdentity)identity).Index;
			IPFilterRange[] items = this.GetItems(index, this.GetFilterMask<T>(), IPvxAddress.None, 1);
			if (items != null && items.Length > 0 && items[0].Identity == index)
			{
				return IPListEntry.NewIPListEntry<T>(items[0]);
			}
			return null;
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x0018EC74 File Offset: 0x0018CE74
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			return new IPListDataProvider.PagedReader<T>(this, filter, pageSize);
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x0018EC80 File Offset: 0x0018CE80
		public IConfigurable[] Find<T>(QueryFilter queryFilter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			LinkedList<IConfigurable> linkedList = new LinkedList<IConfigurable>();
			IPListDataProvider.PagedReader<T> pagedReader = new IPListDataProvider.PagedReader<T>(this, queryFilter);
			foreach (T t in pagedReader)
			{
				IConfigurable value = t;
				linkedList.AddLast(value);
				if (linkedList.Count >= 1000)
				{
					break;
				}
			}
			IConfigurable[] array = new IConfigurable[linkedList.Count];
			linkedList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x0018ED3C File Offset: 0x0018CF3C
		public void Save(IConfigurable instance)
		{
			IPListEntry entry = (IPListEntry)instance;
			int num = (int)RpcClientHelper.Invoke(() => this.connection.Add(entry.ListType == IPListEntryType.Block, entry.ToIPFilterRange()));
			if (num == -1)
			{
				throw new DataSourceOperationException(Strings.IPListEntryExists(entry.IPRange.ToString()));
			}
			entry.Identity = new IPListEntryIdentity(num);
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x0018EDE8 File Offset: 0x0018CFE8
		public void Delete(IConfigurable instance)
		{
			IPListEntry entry = (IPListEntry)instance;
			RpcClientHelper.Invoke(delegate
			{
				this.connection.Remove(((IPListEntryIdentity)entry.Identity).Index, entry.ListType == IPListEntryType.Block);
				return null;
			});
		}

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x06005F20 RID: 24352 RVA: 0x0018EE20 File Offset: 0x0018D020
		public string Source
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x0018EE24 File Offset: 0x0018D024
		private int GetFilterMask<T>() where T : IConfigurable, new()
		{
			IPListEntry iplistEntry = (IPListEntry)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
			switch (iplistEntry.ListType)
			{
			case IPListEntryType.Allow:
				return 3856;
			case IPListEntryType.Block:
				return 3872;
			default:
				throw new ArgumentException("Invalid IPListEntryType.  This should never happen.");
			}
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x0018EEE0 File Offset: 0x0018D0E0
		private IPFilterRange[] GetItems(int startIndex, int typeFilter, IPvxAddress address, int count)
		{
			return (IPFilterRange[])RpcClientHelper.Invoke(() => this.connection.GetItems(startIndex, typeFilter, (ulong)(address >> 64), (ulong)address, count));
		}

		// Token: 0x04003509 RID: 13577
		private IPFilterRpcClient connection;

		// Token: 0x02000A63 RID: 2659
		private struct EntryTypeMask
		{
			// Token: 0x0400350A RID: 13578
			public const int Allow = 3856;

			// Token: 0x0400350B RID: 13579
			public const int Block = 3872;
		}

		// Token: 0x02000A64 RID: 2660
		private sealed class PagedReader<T> : IEnumerable<!0>, IEnumerable where T : IConfigurable, new()
		{
			// Token: 0x06005F23 RID: 24355 RVA: 0x0018EF30 File Offset: 0x0018D130
			public PagedReader(IPListDataProvider provider, QueryFilter filter, int pageSize)
			{
				if (provider == null)
				{
					throw new ArgumentNullException("provider");
				}
				if (pageSize < 0)
				{
					throw new ArgumentOutOfRangeException("pageSize", "page size must be greater than or equal to zero.");
				}
				this.provider = provider;
				this.filter = (filter as IPListQueryFilter);
				this.pageSize = ((pageSize == 0) ? 1000 : pageSize);
			}

			// Token: 0x06005F24 RID: 24356 RVA: 0x0018EF89 File Offset: 0x0018D189
			public PagedReader(IPListDataProvider provider, QueryFilter filter) : this(provider, filter, 1000)
			{
			}

			// Token: 0x06005F25 RID: 24357 RVA: 0x0018F0E4 File Offset: 0x0018D2E4
			public IEnumerator<T> GetEnumerator()
			{
				IEnumerator<IPFilterRange[]> pages = this.GetPages();
				while (pages.MoveNext())
				{
					foreach (IPFilterRange range in pages.Current)
					{
						yield return IPListEntry.NewIPListEntry<T>(range);
					}
				}
				yield break;
			}

			// Token: 0x06005F26 RID: 24358 RVA: 0x0018F240 File Offset: 0x0018D440
			private IEnumerator<IPFilterRange[]> GetPages()
			{
				bool finished = false;
				int startIndex = 0;
				IPvxAddress address = (this.filter != null) ? this.filter.Address : IPvxAddress.None;
				int listTypeMask = this.provider.GetFilterMask<T>();
				while (!finished)
				{
					IPFilterRange[] matches = this.provider.GetItems(startIndex, listTypeMask, address, this.pageSize);
					if (matches == null || matches.Length <= 0)
					{
						finished = true;
					}
					else
					{
						yield return matches;
						startIndex = matches[matches.Length - 1].Identity + 1;
					}
				}
				yield break;
			}

			// Token: 0x06005F27 RID: 24359 RVA: 0x0018F25C File Offset: 0x0018D45C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400350C RID: 13580
			private const int DefaultPageSize = 1000;

			// Token: 0x0400350D RID: 13581
			private readonly IPListDataProvider provider;

			// Token: 0x0400350E RID: 13582
			private readonly IPListQueryFilter filter;

			// Token: 0x0400350F RID: 13583
			private readonly int pageSize;
		}
	}
}
