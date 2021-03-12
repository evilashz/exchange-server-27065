using System;
using System.Collections;
using System.DirectoryServices.Protocols;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200002A RID: 42
	internal abstract class ADGenericReader
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000EF09 File Offset: 0x0000D109
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000EF11 File Offset: 0x0000D111
		protected bool UseNullRoot
		{
			get
			{
				return this.useNullRoot;
			}
			set
			{
				this.useNullRoot = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000EF1A File Offset: 0x0000D11A
		protected bool IsEmptyReader
		{
			get
			{
				return this.isEmptyReader;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000EF22 File Offset: 0x0000D122
		protected virtual ADRawEntry ScopeDeterminingObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000EF25 File Offset: 0x0000D125
		protected internal CustomExceptionHandler CustomExceptionHandler
		{
			set
			{
				this.customExceptionHandler = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000EF2E File Offset: 0x0000D12E
		internal string PreferredServerName
		{
			get
			{
				return this.preferredServerName;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000EF36 File Offset: 0x0000D136
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000EF3E File Offset: 0x0000D13E
		public byte[] Cookie
		{
			get
			{
				return this.cookie;
			}
			set
			{
				this.cookie = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000EF47 File Offset: 0x0000D147
		public int Lcid
		{
			get
			{
				return this.lcid;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000EF4F File Offset: 0x0000D14F
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000EF58 File Offset: 0x0000D158
		public bool IncludeDeletedObjects
		{
			get
			{
				return this.includeDeletedObjects;
			}
			set
			{
				this.includeDeletedObjects = value;
				if (value)
				{
					if (!this.directoryControls.Contains(ADGenericReader.showDeletedControl))
					{
						this.directoryControls.Add(ADGenericReader.showDeletedControl);
						return;
					}
				}
				else if (ADGenericReader.showDeletedControl != null && this.directoryControls.Contains(ADGenericReader.showDeletedControl))
				{
					this.directoryControls.Remove(ADGenericReader.showDeletedControl);
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000EFBC File Offset: 0x0000D1BC
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
		public bool SearchAllNcs
		{
			get
			{
				return this.searchAllNcs;
			}
			set
			{
				this.searchAllNcs = value;
				if (value)
				{
					if (!this.directoryControls.Contains(ADGenericReader.searchOptionsControl))
					{
						this.directoryControls.Add(ADGenericReader.searchOptionsControl);
						return;
					}
				}
				else if (ADGenericReader.searchOptionsControl != null && this.directoryControls.Contains(ADGenericReader.searchOptionsControl))
				{
					this.directoryControls.Remove(ADGenericReader.searchOptionsControl);
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000F028 File Offset: 0x0000D228
		protected IDirectorySession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000F030 File Offset: 0x0000D230
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000F038 File Offset: 0x0000D238
		protected string[] LdapAttributes
		{
			get
			{
				return this.ldapAttributes;
			}
			set
			{
				this.ldapAttributes = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000F041 File Offset: 0x0000D241
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000F049 File Offset: 0x0000D249
		internal string LdapFilter
		{
			get
			{
				return this.ldapFilter;
			}
			set
			{
				this.ldapFilter = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000F052 File Offset: 0x0000D252
		protected ADObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000F05A File Offset: 0x0000D25A
		protected DirectoryControlCollection DirectoryControls
		{
			get
			{
				return this.directoryControls;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000F062 File Offset: 0x0000D262
		protected virtual int SizeLimit
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000F065 File Offset: 0x0000D265
		protected internal ADGenericReader()
		{
			this.isEmptyReader = true;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000F074 File Offset: 0x0000D274
		protected ADGenericReader(IDirectorySession session, ADObjectId rootId, QueryScope scope, SortBy sortBy)
		{
			this.session = session;
			this.rootId = rootId;
			this.scope = scope;
			this.lcid = session.Lcid;
			this.sortBy = sortBy;
			this.directoryControls = new DirectoryControlCollection();
			this.directoryControls.Add(new ExtendedDNControl(ExtendedDNFlag.StandardString));
			if (this.sortBy != null)
			{
				this.AddSortControl();
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		protected SearchResultEntryCollection GetNextResultCollection(Type controlType, out DirectoryControl responseControl)
		{
			SearchRequest searchRequest = new SearchRequest(null, this.ldapFilter, (SearchScope)this.scope, this.ldapAttributes);
			searchRequest.Controls.AddRange(this.directoryControls);
			searchRequest.SizeLimit = this.SizeLimit;
			if (this.session.ServerTimeout != null)
			{
				searchRequest.TimeLimit = this.session.ServerTimeout.Value;
			}
			SearchResponse searchResponse = null;
			responseControl = null;
			RetryManager retryManager = new RetryManager();
			ADObjectId adobjectId = this.rootId;
			bool flag = !this.session.SessionSettings.IncludeSoftDeletedObjects && !this.session.SessionSettings.IncludeInactiveMailbox && this.session.EnforceContainerizedScoping;
			for (;;)
			{
				PooledLdapConnection readConnection = this.session.GetReadConnection(this.preferredServerName, null, ref adobjectId, this.ScopeDeterminingObject);
				Guid serviceProviderRequestId = Guid.Empty;
				try
				{
					try
					{
						if (this.useNullRoot)
						{
							searchRequest.DistinguishedName = null;
						}
						else
						{
							searchRequest.DistinguishedName = adobjectId.ToDNString();
							if (flag && searchRequest.Scope == SearchScope.Subtree)
							{
								ADObjectId domainId = adobjectId.DomainId;
								if (domainId != null)
								{
									ADObjectId childId = domainId.GetChildId("OU", "Microsoft Exchange Hosted Organizations");
									ADObjectId parent = adobjectId.Parent;
									if (childId != null && parent != null && ADObjectId.Equals(childId, parent))
									{
										searchRequest.Scope = SearchScope.OneLevel;
									}
								}
							}
						}
						if (TopologyProvider.IsAdamTopology() && string.IsNullOrEmpty(searchRequest.DistinguishedName))
						{
							searchRequest.Controls.Add(new SearchOptionsControl(SearchOption.PhantomRoot));
						}
						ExTraceGlobals.ADFindTracer.TraceDebug((long)this.GetHashCode(), "ADGenericReader::GetNextResultCollection({0}) using {1} - LDAP search from {2}, scope {3}, filter {4}", new object[]
						{
							controlType.Name,
							readConnection.ADServerInfo.FqdnPlusPort,
							searchRequest.DistinguishedName,
							(int)searchRequest.Scope,
							searchRequest.Filter
						});
						serviceProviderRequestId = Trace.TraceCasStart(CasTraceEventType.ActiveDirectory);
						searchResponse = (SearchResponse)readConnection.SendRequest(searchRequest, LdapOperation.Search, null, this.session.ActivityScope, this.session.CallerInfo);
						this.preferredServerName = readConnection.ServerName;
						this.session.UpdateServerSettings(readConnection);
						break;
					}
					catch (DirectoryException de)
					{
						if (this.customExceptionHandler != null)
						{
							this.customExceptionHandler(de);
						}
						if (readConnection.IsResultCode(de, ResultCode.NoSuchObject))
						{
							ExTraceGlobals.ADFindTracer.TraceWarning<string, object>((long)this.GetHashCode(), "NoSuchObject caught when searching from {0} with filter {1}", searchRequest.DistinguishedName, searchRequest.Filter);
							return null;
						}
						if (readConnection.IsResultCode(de, ResultCode.VirtualListViewError) && this.lcid != LcidMapper.DefaultLcid)
						{
							ExTraceGlobals.ADFindTracer.TraceWarning<int, int>((long)this.GetHashCode(), "VirtualListView error caught when performing a VLV lookup using LCID 0x{0:X}. Falling back to US English 0x{1:X}", this.lcid, LcidMapper.DefaultLcid);
							this.RefreshSortControlWithDefaultLCID(searchRequest);
						}
						else
						{
							retryManager.Tried(readConnection.ServerName);
							this.session.AnalyzeDirectoryError(readConnection, searchRequest, de, retryManager.TotalRetries, retryManager[readConnection.ServerName]);
						}
					}
					continue;
				}
				finally
				{
					bool isSnapshotInProgress = PerformanceContext.Current.IsSnapshotInProgress;
					bool flag2 = ETWTrace.ShouldTraceCasStop(serviceProviderRequestId);
					if (isSnapshotInProgress || flag2)
					{
						string text = string.Format(CultureInfo.InvariantCulture, "scope: {0}, filter: {1}", new object[]
						{
							searchRequest.Scope,
							searchRequest.Filter
						});
						if (isSnapshotInProgress)
						{
							PerformanceContext.Current.AppendToOperations(text);
						}
						if (flag2)
						{
							Trace.TraceCasStop(CasTraceEventType.ActiveDirectory, serviceProviderRequestId, 0, 0, readConnection.ADServerInfo.FqdnPlusPort, searchRequest.DistinguishedName, "ADGenericReader::GetNextResultCollection", text, string.Empty);
						}
					}
					readConnection.ReturnToPool();
				}
				break;
			}
			responseControl = this.FindControlInCollection(searchResponse.Controls, controlType);
			return searchResponse.Entries;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		private DirectoryControl FindControlInCollection(IEnumerable controls, Type controlType)
		{
			foreach (object obj in controls)
			{
				DirectoryControl directoryControl = (DirectoryControl)obj;
				if (directoryControl.GetType().Equals(controlType))
				{
					return directoryControl;
				}
			}
			return null;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000F50C File Offset: 0x0000D70C
		private void AddSortControl()
		{
			ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)this.sortBy.ColumnDefinition;
			SortRequestControl sortRequestControl = new SortRequestControl(adpropertyDefinition.LdapDisplayName, LcidMapper.OidFromLcid(this.lcid), this.sortBy.SortOrder == SortOrder.Descending);
			sortRequestControl.IsCritical = false;
			ExTraceGlobals.ADFindTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "ADGenericReader::AddSortControl - Sort on {0}, {1} using rule {2})", sortRequestControl.SortKeys[0].AttributeName, sortRequestControl.SortKeys[0].ReverseOrder ? "reverse order (descending)" : "regular  order (ascending)", sortRequestControl.SortKeys[0].MatchingRule);
			this.DirectoryControls.Add(sortRequestControl);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		private void RefreshSortControlWithDefaultLCID(DirectoryRequest request)
		{
			DirectoryControl value = this.FindControlInCollection(this.DirectoryControls, typeof(SortRequestControl));
			this.DirectoryControls.Remove(value);
			this.lcid = LcidMapper.DefaultLcid;
			this.AddSortControl();
			request.Controls.Clear();
			request.Controls.AddRange(this.DirectoryControls);
		}

		// Token: 0x040000A7 RID: 167
		private const CasTraceEventType ActiveDirectoryTraceEventType = CasTraceEventType.ActiveDirectory;

		// Token: 0x040000A8 RID: 168
		private const int UnlimitedSizeLimit = 0;

		// Token: 0x040000A9 RID: 169
		private static readonly ShowDeletedControl showDeletedControl = new ShowDeletedControl();

		// Token: 0x040000AA RID: 170
		private static readonly SearchOptionsControl searchOptionsControl = new SearchOptionsControl(SearchOption.PhantomRoot);

		// Token: 0x040000AB RID: 171
		private string preferredServerName;

		// Token: 0x040000AC RID: 172
		private ADObjectId rootId;

		// Token: 0x040000AD RID: 173
		private QueryScope scope;

		// Token: 0x040000AE RID: 174
		private byte[] cookie;

		// Token: 0x040000AF RID: 175
		private IDirectorySession session;

		// Token: 0x040000B0 RID: 176
		private SortBy sortBy;

		// Token: 0x040000B1 RID: 177
		private int lcid;

		// Token: 0x040000B2 RID: 178
		private string[] ldapAttributes;

		// Token: 0x040000B3 RID: 179
		private string ldapFilter;

		// Token: 0x040000B4 RID: 180
		private DirectoryControlCollection directoryControls;

		// Token: 0x040000B5 RID: 181
		private CustomExceptionHandler customExceptionHandler;

		// Token: 0x040000B6 RID: 182
		private bool useNullRoot;

		// Token: 0x040000B7 RID: 183
		private bool includeDeletedObjects;

		// Token: 0x040000B8 RID: 184
		private bool searchAllNcs;

		// Token: 0x040000B9 RID: 185
		private bool isEmptyReader;
	}
}
