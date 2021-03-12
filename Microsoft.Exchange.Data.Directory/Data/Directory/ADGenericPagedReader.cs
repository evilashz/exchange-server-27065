using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200002B RID: 43
	internal abstract class ADGenericPagedReader<TResult> : ADGenericReader, IEnumerable<TResult>, IEnumerable, IPageInformation where TResult : IConfigurable, new()
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000F624 File Offset: 0x0000D824
		protected override ADRawEntry ScopeDeterminingObject
		{
			get
			{
				return this.dummyInstance;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000F62C File Offset: 0x0000D82C
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000F634 File Offset: 0x0000D834
		protected bool? RetrievedAllData
		{
			get
			{
				return this.retrievedAllData;
			}
			set
			{
				this.retrievedAllData = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000F63D File Offset: 0x0000D83D
		public int PagesReturned
		{
			get
			{
				return this.pagesReturned;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000F645 File Offset: 0x0000D845
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000F64D File Offset: 0x0000D84D
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				this.pageSize = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000F656 File Offset: 0x0000D856
		public int LastRetrievedCount
		{
			get
			{
				return this.lastRetrievedCount;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000F660 File Offset: 0x0000D860
		public bool? MorePagesAvailable
		{
			get
			{
				bool? flag = this.retrievedAllData;
				if (flag == null)
				{
					return null;
				}
				return new bool?(!flag.GetValueOrDefault());
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000F696 File Offset: 0x0000D896
		public bool SkipNonUniqueResults
		{
			get
			{
				return this.skipNonUniqueResults;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000F69E File Offset: 0x0000D89E
		protected internal ADGenericPagedReader()
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		protected internal ADGenericPagedReader(IDirectorySession session, ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties, bool skipCheckVirtualIndex) : base(session, rootId, scope, sortBy)
		{
			if (!typeof(ADRawEntry).IsAssignableFrom(typeof(TResult)))
			{
				throw new InvalidOperationException(DirectoryStrings.ErrorMustBeADRawEntry);
			}
			if (pageSize < 0 || pageSize > 10000)
			{
				throw new ArgumentOutOfRangeException("pageSize", pageSize, string.Format("pageSize should be between 1 and {0} or 0 to use the default page size: {1}", 10000, ADGenericPagedReader<TResult>.DefaultPageSize));
			}
			this.dummyInstance = (ADRawEntry)((object)((default(TResult) == null) ? Activator.CreateInstance<TResult>() : default(TResult)));
			QueryFilter filter2 = filter;
			ConfigScopes configScopes;
			ADScope readScope = session.GetReadScope(rootId, this.dummyInstance, false, out configScopes);
			ADObject adobject;
			string[] ldapAttributes;
			base.Session.GetSchemaAndApplyFilter(this.dummyInstance, readScope, out adobject, out ldapAttributes, ref filter, ref properties);
			ADDataSession addataSession = base.Session as ADDataSession;
			if (addataSession != null)
			{
				addataSession.UpdateFilterforInactiveMailboxSearch(this.dummyInstance, ref filter);
			}
			base.LdapAttributes = ldapAttributes;
			this.pageSize = ((pageSize == 0) ? ADGenericPagedReader<TResult>.DefaultPageSize : pageSize);
			this.retrievedAllData = null;
			this.properties = properties;
			session.CheckFilterForUnsafeIdentity(filter2);
			base.LdapFilter = LdapFilterBuilder.LdapFilterFromQueryFilter(filter, skipCheckVirtualIndex, base.Session.SessionSettings.PartitionSoftLinkMode, base.Session.SessionSettings.IsTenantScoped);
			this.skipNonUniqueResults = (session is IConfigurationSession);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000F820 File Offset: 0x0000DA20
		public TResult[] ReadAllPages()
		{
			if (this.retrievedAllData != null && this.retrievedAllData.Value)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionPagedReaderIsSingleUse);
			}
			if (this.pagesReturned > 0)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionPagedReaderReadAllAfterEnumerating);
			}
			List<TResult> list = new List<TResult>();
			foreach (TResult item in this)
			{
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000FA94 File Offset: 0x0000DC94
		public IEnumerator<TResult> GetEnumerator()
		{
			if (base.IsEmptyReader)
			{
				this.RetrievedAllData = new bool?(true);
			}
			else
			{
				if (this.retrievedAllData != null && this.retrievedAllData.Value)
				{
					throw new InvalidOperationException(DirectoryStrings.ExceptionPagedReaderIsSingleUse);
				}
				while (this.retrievedAllData == null || !this.retrievedAllData.Value)
				{
					TResult[] results = this.GetNextPage();
					this.lastRetrievedCount = results.Length;
					foreach (TResult result in results)
					{
						yield return result;
					}
				}
			}
			yield break;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002CE RID: 718
		protected abstract SearchResultEntryCollection GetNextResultCollection();

		// Token: 0x060002CF RID: 719 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		protected virtual TResult[] GetNextPage()
		{
			SearchResultEntryCollection nextResultCollection = this.GetNextResultCollection();
			if (nextResultCollection == null)
			{
				return (TResult[])new TResult[0];
			}
			TResult[] array = base.Session.ObjectsFromEntries<TResult>(nextResultCollection, base.PreferredServerName, this.properties, this.dummyInstance);
			TResult[] result = this.skipNonUniqueResults ? this.GetUniqueResults(array) : array;
			this.pagesReturned++;
			return result;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000FB20 File Offset: 0x0000DD20
		private TResult[] GetUniqueResults(TResult[] results)
		{
			TResult[] result;
			if (this.pagesReturned == 0)
			{
				if (this.retrievedAllData == null || !this.retrievedAllData.Value)
				{
					this.idHashSet = new HashSet<Guid>(this.pageSize);
					foreach (TResult tresult in results)
					{
						ADRawEntry adrawEntry = (ADRawEntry)((object)tresult);
						this.idHashSet.Add(adrawEntry.Id.ObjectGuid);
					}
				}
				result = results;
			}
			else
			{
				List<TResult> list = new List<TResult>(results.Length);
				foreach (TResult tresult2 in results)
				{
					ADRawEntry adrawEntry2 = (ADRawEntry)((object)tresult2);
					if (this.idHashSet.TryAdd(adrawEntry2.Id.ObjectGuid))
					{
						list.Add(tresult2);
					}
					else
					{
						ExTraceGlobals.ADReadTracer.TraceError<int, string>((long)this.GetHashCode(), "Warning: Removing non-unique object from result set on {0}th page: {1}", this.pagesReturned + 1, adrawEntry2.Id.DistinguishedName);
					}
				}
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x040000BA RID: 186
		public const int MaximumPageSize = 10000;

		// Token: 0x040000BB RID: 187
		public static readonly int DefaultPageSize = 1000;

		// Token: 0x040000BC RID: 188
		private IEnumerable<PropertyDefinition> properties;

		// Token: 0x040000BD RID: 189
		private int pageSize;

		// Token: 0x040000BE RID: 190
		private bool? retrievedAllData;

		// Token: 0x040000BF RID: 191
		private ADRawEntry dummyInstance;

		// Token: 0x040000C0 RID: 192
		private int lastRetrievedCount;

		// Token: 0x040000C1 RID: 193
		private int pagesReturned;

		// Token: 0x040000C2 RID: 194
		private HashSet<Guid> idHashSet;

		// Token: 0x040000C3 RID: 195
		private bool skipNonUniqueResults;
	}
}
