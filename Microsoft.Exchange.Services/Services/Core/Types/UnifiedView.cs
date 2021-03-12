using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008AE RID: 2222
	internal class UnifiedView : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003F1E RID: 16158 RVA: 0x000DAADC File Offset: 0x000D8CDC
		private UnifiedView(Trace tracer, CallContext callContext, Guid[] mailboxGuids, DistinguishedFolderIdName distinguishedFolderIdName)
		{
			this.tracer = tracer;
			this.disposeTracker = ((IDisposeTrackable)this).GetDisposeTracker();
			this.callContext = callContext;
			if (mailboxGuids == null)
			{
				throw new ArgumentNullException("mailboxGuids", "Parameters mailboxGuids must not be null.");
			}
			if (distinguishedFolderIdName == DistinguishedFolderIdName.none)
			{
				throw new ArgumentNullException("distinguishedFolderIdName", "Parameter distinguishedFolderIdName cannot be 'none' when parameter mailboxGuids is not null.");
			}
			this.mailboxGuids = mailboxGuids;
			this.distinguishedFolderIdName = distinguishedFolderIdName;
			this.defaultFolderType = IdConverter.GetDefaultFolderTypeFromDistinguishedFolderIdNameType(this.distinguishedFolderIdName.ToString());
			this.ValidateMailboxGuids();
			if (this.mailboxGuids.Length > 0)
			{
				this.accessingMailboxGuid = ((this.mailboxGuids.Length > 1) ? this.callContext.AccessingADUser.ExchangeGuid : this.mailboxGuids[0]);
			}
			this.unifiedSessionRequired = (this.MailboxGuids.Length > 1 || this.accessingMailboxGuid == this.callContext.AccessingADUser.ExchangeGuid);
			this.storeObjectIds = new LazyMember<StoreObjectId[]>(new InitializeLazyMember<StoreObjectId[]>(this.GetStoreObjectIds));
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x000DABE5 File Offset: 0x000D8DE5
		// (set) Token: 0x06003F20 RID: 16160 RVA: 0x000DABF3 File Offset: 0x000D8DF3
		internal SearchFolder SearchFolder
		{
			get
			{
				this.CheckDisposed();
				return this.searchFolder;
			}
			private set
			{
				this.searchFolder = value;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x000DABFC File Offset: 0x000D8DFC
		internal Guid[] MailboxGuids
		{
			get
			{
				this.CheckDisposed();
				return this.mailboxGuids;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x000DAC0A File Offset: 0x000D8E0A
		internal DefaultFolderType DefaultFolderType
		{
			get
			{
				this.CheckDisposed();
				return this.defaultFolderType;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06003F23 RID: 16163 RVA: 0x000DAC18 File Offset: 0x000D8E18
		internal DistinguishedFolderIdName DistinguishedFolderIdName
		{
			get
			{
				this.CheckDisposed();
				return this.distinguishedFolderIdName;
			}
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x000DAC2C File Offset: 0x000D8E2C
		internal static string CreateSearchFolderParentFolderName(Guid[] mailboxGuids)
		{
			if (mailboxGuids == null)
			{
				throw new ArgumentNullException("mailboxGuids", "Parameters mailboxGuids must not be null.");
			}
			return string.Join<Guid>(" ", from guid in mailboxGuids
			orderby guid
			select guid).GetHashCode().ToString();
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06003F25 RID: 16165 RVA: 0x000DAC86 File Offset: 0x000D8E86
		internal bool UnifiedViewScopeSpecified
		{
			get
			{
				this.CheckDisposed();
				return this.MailboxGuids.Length > 0;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x000DAC99 File Offset: 0x000D8E99
		internal bool UnifiedSessionRequired
		{
			get
			{
				this.CheckDisposed();
				return this.unifiedSessionRequired;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06003F27 RID: 16167 RVA: 0x000DACA7 File Offset: 0x000D8EA7
		internal StoreObjectId[] StoreObjectIds
		{
			get
			{
				this.CheckDisposed();
				return this.storeObjectIds.Member;
			}
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x000DACBC File Offset: 0x000D8EBC
		internal static UnifiedView Create(Trace tracer, CallContext callContext, Guid[] mailboxGuids, TargetFolderId parentFolder)
		{
			if (mailboxGuids == null)
			{
				return null;
			}
			DistinguishedFolderIdName distinguishedFolderIdName = DistinguishedFolderIdName.none;
			if (parentFolder != null)
			{
				DistinguishedFolderId distinguishedFolderId = parentFolder.BaseFolderId as DistinguishedFolderId;
				if (distinguishedFolderId != null)
				{
					distinguishedFolderIdName = distinguishedFolderId.Id;
				}
			}
			return new UnifiedView(tracer, callContext, mailboxGuids, distinguishedFolderIdName);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x000DACF4 File Offset: 0x000D8EF4
		internal StoreObjectId GetSearchFolderParentFolderId(MailboxSession session)
		{
			this.CheckDisposed();
			StoreObjectId parentFolderId = null;
			using (Folder folder = Folder.Create(session, session.SafeGetDefaultFolderId(DefaultFolderType.Configuration), StoreObjectType.Folder, "UnifiedViews", CreateMode.OpenIfExists))
			{
				folder.Save();
				folder.Load();
				parentFolderId = folder.StoreObjectId;
			}
			StoreObjectId storeObjectId;
			using (Folder folder2 = Folder.Create(session, parentFolderId, StoreObjectType.Folder, UnifiedView.CreateSearchFolderParentFolderName(this.MailboxGuids), CreateMode.OpenIfExists))
			{
				folder2.Save();
				folder2.Load();
				storeObjectId = folder2.StoreObjectId;
			}
			return storeObjectId;
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x000DAD94 File Offset: 0x000D8F94
		internal SearchFolder CreateOrOpenSearchFolder(MailboxSession session)
		{
			this.CheckDisposed();
			SearchFolder result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				SearchFolder searchFolder = SearchFolder.Create(session, this.GetSearchFolderParentFolderId(session), this.DefaultFolderType.ToString(), CreateMode.OpenIfExists);
				disposeGuard.Add<SearchFolder>(searchFolder);
				searchFolder.Save();
				searchFolder.Load();
				SearchFolderCriteria searchFolderCriteria = null;
				try
				{
					searchFolderCriteria = searchFolder.GetSearchCriteria();
				}
				catch (ObjectNotInitializedException arg)
				{
					this.tracer.TraceDebug<ObjectNotInitializedException, DefaultFolderType>(-1L, "UnifiedView::CreateSearchFolder. Expected exception {0} which indicates the search folder criteria does not exist for search folder {1}. Will create new one.", arg, this.DefaultFolderType);
				}
				if (searchFolderCriteria == null)
				{
					QueryFilter searchQuery = UnifiedMailboxHelper.CreateQueryFilter(this.DefaultFolderType);
					searchFolderCriteria = new SearchFolderCriteria(searchQuery, this.StoreObjectIds);
					IAsyncResult asyncResult = searchFolder.BeginApplyContinuousSearch(searchFolderCriteria, null, null);
					if (asyncResult.AsyncWaitHandle.WaitOne(60000))
					{
						searchFolder.EndApplyContinuousSearch(asyncResult);
					}
					else
					{
						this.tracer.TraceDebug(-1L, "UnifiedView::CreateSearchFolder. Timeout expired waiting for the search folder to finish population.");
					}
				}
				disposeGuard.Success();
				result = searchFolder;
			}
			return result;
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x000DAE9C File Offset: 0x000D909C
		internal IdAndSession CreateIdAndSessionUsingSessionCache()
		{
			MailboxSession mailboxSessionByMailboxId = this.callContext.SessionCache.GetMailboxSessionByMailboxId(new MailboxId(this.accessingMailboxGuid), this.unifiedSessionRequired);
			return this.CreateIdAndSession(mailboxSessionByMailboxId);
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x000DAED4 File Offset: 0x000D90D4
		internal IdAndSession CreateIdAndSession(MailboxSession mailboxSession)
		{
			this.CheckDisposed();
			StoreObjectId storeId;
			if (this.MailboxGuids.Length > 1)
			{
				this.SearchFolder = this.CreateOrOpenSearchFolder(mailboxSession);
				storeId = this.SearchFolder.StoreObjectId;
			}
			else
			{
				storeId = this.StoreObjectIds[0];
			}
			return new IdAndSession(storeId, mailboxSession);
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x000DAF20 File Offset: 0x000D9120
		internal static void UpdateConversationResponseShape(ConversationResponseShape shape)
		{
			List<PropertyPath> list = (shape.AdditionalProperties != null) ? shape.AdditionalProperties.ToList<PropertyPath>() : new List<PropertyPath>();
			foreach (PropertyUriEnum uri in UnifiedView.AdditionalProperties)
			{
				PropertyUri item = new PropertyUri(uri);
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			shape.AdditionalProperties = list.ToArray();
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x000DAFA4 File Offset: 0x000D91A4
		private StoreObjectId[] GetStoreObjectIds()
		{
			MailboxSession[] mailboxSessions = (from mailboxGuid in this.MailboxGuids
			select this.callContext.SessionCache.GetMailboxSessionByMailboxId(new MailboxId(mailboxGuid), false)).ToArray<MailboxSession>();
			return UnifiedView.GetDefaultFolderIdsInUnifiedDefaultFolderView(mailboxSessions, this.DefaultFolderType);
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x000DAFF0 File Offset: 0x000D91F0
		private static StoreObjectId[] GetDefaultFolderIdsInUnifiedDefaultFolderView(MailboxSession[] mailboxSessions, DefaultFolderType defaultFolderType)
		{
			List<StoreObjectId> list = new List<StoreObjectId>();
			bool flag = UnifiedMailboxHelper.DefaultSearchFolderTypesSupportedForUnifiedViews.Contains(defaultFolderType);
			for (int i = 0; i < mailboxSessions.Length; i++)
			{
				MailboxSession mailboxSession = mailboxSessions[i];
				StoreObjectId storeObjectId = mailboxSession.SafeGetDefaultFolderId(defaultFolderType);
				if (storeObjectId.ObjectType == StoreObjectType.Folder)
				{
					list.Add(storeObjectId);
				}
				else
				{
					if (storeObjectId.ObjectType != StoreObjectType.SearchFolder || !flag)
					{
						throw new ArgumentException(string.Format("The default folder {0} is not supported for unified view.", defaultFolderType), "defaultFolderType");
					}
					if (mailboxSessions.Length == 1)
					{
						list.Add(storeObjectId);
					}
					else
					{
						list.AddRange((from defaultFolderTypeInSearchScope in UnifiedMailboxHelper.GetSearchScopeForDefaultSearchFolder(defaultFolderType)
						select mailboxSession.GetDefaultFolderId(defaultFolderTypeInSearchScope)).ToArray<StoreObjectId>());
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x000DB0C8 File Offset: 0x000D92C8
		private void ValidateMailboxGuids()
		{
			if (this.mailboxGuids.Distinct<Guid>().Count<Guid>() != this.mailboxGuids.Length)
			{
				throw new ArgumentException("Array of mailbox GUIDs must not contain duplicates.");
			}
			ADUser accessingADUser = this.callContext.AccessingADUser;
			if (accessingADUser == null)
			{
				throw new CannotFindUserException(ResponseCodeType.ErrorCannotFindUser, CoreResources.ErrorUserADObjectNotFound);
			}
			Guid exchangeGuid = accessingADUser.ExchangeGuid;
			foreach (Guid guid in this.mailboxGuids)
			{
				if (guid != exchangeGuid)
				{
					accessingADUser.AggregatedMailboxGuids.Contains(guid);
				}
			}
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x000DB15C File Offset: 0x000D935C
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.searchFolder != null)
				{
					this.searchFolder.Dispose();
					this.searchFolder = null;
				}
				this.disposed = true;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x000DB1B2 File Offset: 0x000D93B2
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UnifiedView>(this);
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x000DB1BA File Offset: 0x000D93BA
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x000DB1CF File Offset: 0x000D93CF
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x0400242F RID: 9263
		internal const int SearchTimeoutInMilliseconds = 60000;

		// Token: 0x04002430 RID: 9264
		internal const string RootFolderNameForAllUnifiedViewSearchFolders = "UnifiedViews";

		// Token: 0x04002431 RID: 9265
		internal const DefaultFolderType RootDeafultFolderTypeForAllUnifiedViewSearchFolders = DefaultFolderType.Configuration;

		// Token: 0x04002432 RID: 9266
		internal static readonly PropertyUriEnum[] AdditionalProperties = new PropertyUriEnum[]
		{
			PropertyUriEnum.ConversationMailboxGuid
		};

		// Token: 0x04002433 RID: 9267
		private readonly Guid[] mailboxGuids;

		// Token: 0x04002434 RID: 9268
		private readonly Guid accessingMailboxGuid;

		// Token: 0x04002435 RID: 9269
		private readonly bool unifiedSessionRequired;

		// Token: 0x04002436 RID: 9270
		private readonly CallContext callContext;

		// Token: 0x04002437 RID: 9271
		private bool disposed;

		// Token: 0x04002438 RID: 9272
		private DisposeTracker disposeTracker;

		// Token: 0x04002439 RID: 9273
		private Trace tracer;

		// Token: 0x0400243A RID: 9274
		private SearchFolder searchFolder;

		// Token: 0x0400243B RID: 9275
		private readonly DefaultFolderType defaultFolderType;

		// Token: 0x0400243C RID: 9276
		private readonly DistinguishedFolderIdName distinguishedFolderIdName;

		// Token: 0x0400243D RID: 9277
		private readonly LazyMember<StoreObjectId[]> storeObjectIds;
	}
}
