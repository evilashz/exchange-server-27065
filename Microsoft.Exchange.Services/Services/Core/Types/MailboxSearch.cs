using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007F9 RID: 2041
	internal sealed class MailboxSearch : IDisposable
	{
		// Token: 0x06003BBF RID: 15295 RVA: 0x000D118E File Offset: 0x000CF38E
		internal MailboxSearch(string mailboxId, bool isArchiveMailbox, OrganizationId orgId)
		{
			this.Initialize(mailboxId, isArchiveMailbox, orgId);
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x000D119F File Offset: 0x000CF39F
		internal SearchMailboxCriteria SearchCriteria
		{
			get
			{
				return this.searchMailboxCriteria;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x000D11A7 File Offset: 0x000CF3A7
		internal SearchMailboxResult SearchResult
		{
			get
			{
				if (this.searchMailboxWorker == null)
				{
					return null;
				}
				return this.searchMailboxWorker.SearchResult;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x000D11BE File Offset: 0x000CF3BE
		internal OrganizationId OrganizationId
		{
			get
			{
				return this.exchangePrincipal.MailboxInfo.OrganizationId;
			}
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000D11D0 File Offset: 0x000CF3D0
		internal void CreateSearchMailboxCriteria(CultureInfo language, MultiValuedProperty<string> senders, MultiValuedProperty<string> recipients, DateTime startDate, DateTime endDate, MultiValuedProperty<KindKeyword> messageTypes, string userQuery, bool searchDumpster, bool includeUnsearchableItems, bool includePersonalArchive)
		{
			if (this.searchMailboxCriteria == null)
			{
				SearchObject searchObject = new SearchObject();
				searchObject.Language = language;
				searchObject.Senders = senders;
				searchObject.Recipients = recipients;
				if (!startDate.Equals(DateTime.MinValue))
				{
					searchObject.StartDate = new ExDateTime?((ExDateTime)startDate.ToLocalTime());
				}
				if (!endDate.Equals(DateTime.MinValue))
				{
					searchObject.EndDate = new ExDateTime?((ExDateTime)endDate.ToLocalTime());
				}
				searchObject.MessageTypes = messageTypes;
				searchObject.SearchQuery = userQuery;
				SearchUser searchUser = new SearchUser(this.exchangePrincipal.ObjectId, this.exchangePrincipal.MailboxInfo.DisplayName, this.exchangePrincipal.MailboxInfo.Location.ServerFqdn);
				this.searchMailboxCriteria = new SearchMailboxCriteria(searchObject.Language, searchObject.AqsQuery, searchObject.SearchQuery, new SearchUser[]
				{
					searchUser
				});
				this.searchMailboxCriteria.SearchDumpster = searchDumpster;
				this.searchMailboxCriteria.IncludeUnsearchableItems = includeUnsearchableItems;
				this.searchMailboxCriteria.IncludePersonalArchive = includePersonalArchive;
				this.searchMailboxCriteria.ResolveQueryFilter(null, null);
				this.searchMailboxCriteria.GenerateSubQueryFilters(null, null);
				this.folderScope = this.searchMailboxCriteria.GetFolderScope(this.mailboxSession);
			}
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000D1314 File Offset: 0x000CF514
		internal void CreateSearchFolderAndPerformInitialEstimation()
		{
			this.CreateSearchMailboxWorkerInstance();
			this.searchFolder = this.searchMailboxWorker.CreateSearchFolder(this.mailboxSession, ref this.dumpsterFolders, ref this.resultFolders);
			this.searchHit = new KeywordHit
			{
				Phrase = this.searchMailboxCriteria.UserQuery
			};
			this.searchMailboxWorker.PerformInitialEstimation(this.mailboxSession, this.resultFolders, ref this.searchHit);
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000D1385 File Offset: 0x000CF585
		internal void PerformSingleKeywordSearch(string keyword)
		{
			this.searchMailboxWorker.PerformSingleKeywordEstimationSearch(this.mailboxSession, this.searchFolder, this.folderScope, this.resultFolders, keyword, ref this.searchHit);
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000D13BC File Offset: 0x000CF5BC
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				if (this.searchMailboxCriteria != null)
				{
					this.searchMailboxCriteria = null;
				}
				if (this.searchMailboxWorker != null)
				{
					this.searchMailboxWorker = null;
				}
				if (this.dumpsterFolders != null)
				{
					this.dumpsterFolders.ForEach(delegate(Folder folder)
					{
						folder.Dispose();
					});
					this.dumpsterFolders.Clear();
					this.dumpsterFolders = null;
				}
				if (this.searchFolder != null)
				{
					StoreId id = this.searchFolder.Id;
					this.searchFolder.Dispose();
					this.mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
					{
						id
					});
					this.searchFolder = null;
				}
				if (this.mailboxSession != null)
				{
					this.mailboxSession.Dispose();
					this.mailboxSession = null;
				}
				if (this.exchangePrincipal != null)
				{
					this.exchangePrincipal = null;
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x000D14A4 File Offset: 0x000CF6A4
		private void Initialize(string id, bool isArchiveMailbox, OrganizationId orgId)
		{
			bool flag = false;
			do
			{
				RemotingOptions remotingOptions = flag ? RemotingOptions.AllowCrossSite : RemotingOptions.LocalConnectionsOnly;
				if (isArchiveMailbox)
				{
					this.exchangePrincipal = ExchangePrincipal.FromMailboxGuid(orgId.ToADSessionSettings(), new Guid(id), remotingOptions, null);
				}
				else
				{
					this.exchangePrincipal = ExchangePrincipal.FromProxyAddress(orgId.ToADSessionSettings(), id, remotingOptions);
				}
				try
				{
					this.mailboxSession = MailboxSession.OpenAsSystemService(this.exchangePrincipal, CultureInfo.InvariantCulture, "Client=EDiscoverySearch;Action=Search;Interactive=False");
					break;
				}
				catch (WrongServerException)
				{
					ExTraceGlobals.SearchTracer.TraceDebug<bool>(0L, "FindMailboxStatisticsByKeywords EWS call from [Hybrid E14->E15(arbitration)->E15(mailbox)] Failed with WrongServerException. retryByAllowingCrossSite={0}", flag);
					if (flag)
					{
						throw;
					}
					flag = true;
				}
			}
			while (flag);
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x000D1538 File Offset: 0x000CF738
		private void CreateSearchMailboxWorkerInstance()
		{
			if (this.searchMailboxWorker == null)
			{
				this.searchMailboxWorker = new SearchMailboxWorker(this.searchMailboxCriteria, 0);
			}
		}

		// Token: 0x040020E5 RID: 8421
		private bool isDisposed;

		// Token: 0x040020E6 RID: 8422
		private SearchMailboxWorker searchMailboxWorker;

		// Token: 0x040020E7 RID: 8423
		private SearchMailboxCriteria searchMailboxCriteria;

		// Token: 0x040020E8 RID: 8424
		private ExchangePrincipal exchangePrincipal;

		// Token: 0x040020E9 RID: 8425
		private MailboxSession mailboxSession;

		// Token: 0x040020EA RID: 8426
		private StoreId[] folderScope;

		// Token: 0x040020EB RID: 8427
		private SearchFolder searchFolder;

		// Token: 0x040020EC RID: 8428
		private Folder[] resultFolders;

		// Token: 0x040020ED RID: 8429
		private List<Folder> dumpsterFolders;

		// Token: 0x040020EE RID: 8430
		private KeywordHit searchHit;
	}
}
