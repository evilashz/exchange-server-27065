using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001DD RID: 477
	internal class RequestIndexEntryProvider : IConfigDataProvider
	{
		// Token: 0x060013D2 RID: 5074 RVA: 0x0002CF04 File Offset: 0x0002B104
		public RequestIndexEntryProvider(IRecipientSession recipSession, IConfigurationSession configSession)
		{
			this.ownsSessions = false;
			this.recipSession = recipSession;
			this.configSession = configSession;
			this.domainController = recipSession.Source;
			this.orgId = OrganizationId.ForestWideOrgId;
			this.configSession.SessionSettings.IncludeSoftDeletedObjectLinks = true;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0002CF54 File Offset: 0x0002B154
		public RequestIndexEntryProvider()
		{
			this.ownsSessions = true;
			this.recipSession = null;
			this.configSession = null;
			this.domainController = null;
			this.orgId = OrganizationId.ForestWideOrgId;
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0002CF83 File Offset: 0x0002B183
		public string Source
		{
			get
			{
				return this.RecipientSession.Source;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0002CF90 File Offset: 0x0002B190
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x0002CFA6 File Offset: 0x0002B1A6
		internal IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipSession == null)
				{
					this.CreateSessions();
				}
				return this.recipSession;
			}
			set
			{
				this.recipSession = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0002CFAF File Offset: 0x0002B1AF
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0002CFC5 File Offset: 0x0002B1C5
		internal IConfigurationSession ConfigSession
		{
			get
			{
				if (this.configSession == null)
				{
					this.CreateSessions();
				}
				return this.configSession;
			}
			set
			{
				this.configSession = value;
				this.configSession.SessionSettings.IncludeSoftDeletedObjectLinks = true;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0002CFDF File Offset: 0x0002B1DF
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x0002CFE7 File Offset: 0x0002B1E7
		internal string DomainController
		{
			get
			{
				return this.domainController;
			}
			set
			{
				this.domainController = value;
				this.configSession = null;
				this.recipSession = null;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0002CFFE File Offset: 0x0002B1FE
		internal OrganizationId OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0002D006 File Offset: 0x0002B206
		internal bool OwnsSessions
		{
			get
			{
				return this.ownsSessions;
			}
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0002D010 File Offset: 0x0002B210
		public static void GetMoveGuids(ADUser adUser, out Guid mailboxGuid, out Guid mdbGuid)
		{
			mdbGuid = Guid.Empty;
			if (adUser != null)
			{
				mailboxGuid = adUser.ExchangeGuid;
				if (adUser.MailboxMoveStatus != RequestStatus.None && adUser.MailboxMoveFlags != RequestFlags.None)
				{
					if ((adUser.MailboxMoveFlags & RequestFlags.Pull) != RequestFlags.None)
					{
						if (adUser.MailboxMoveTargetMDB != null)
						{
							mdbGuid = adUser.MailboxMoveTargetMDB.ObjectGuid;
							return;
						}
						if (adUser.MailboxMoveTargetArchiveMDB != null)
						{
							mdbGuid = adUser.MailboxMoveTargetArchiveMDB.ObjectGuid;
						}
						return;
					}
					else if ((adUser.MailboxMoveFlags & RequestFlags.Push) != RequestFlags.None)
					{
						if (adUser.MailboxMoveSourceMDB != null)
						{
							mdbGuid = adUser.MailboxMoveSourceMDB.ObjectGuid;
							return;
						}
						if (adUser.MailboxMoveSourceArchiveMDB != null)
						{
							mdbGuid = adUser.MailboxMoveSourceArchiveMDB.ObjectGuid;
						}
						return;
					}
				}
			}
			else
			{
				mailboxGuid = Guid.Empty;
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0002D128 File Offset: 0x0002B328
		public static void CreateAndPopulateRequestIndexEntries(RequestJobBase requestJobBase, RequestIndexId requestIndexId)
		{
			if (requestJobBase == null)
			{
				throw new ArgumentNullException("requestJobBase");
			}
			if (requestJobBase.IndexEntries == null)
			{
				requestJobBase.IndexEntries = new List<IRequestIndexEntry>();
			}
			if (requestJobBase.IndexIds == null)
			{
				requestJobBase.IndexIds = new List<RequestIndexId>();
			}
			RequestIndexEntryProvider.Handle(requestIndexId.RequestIndexEntryType, delegate(IRequestIndexEntryHandler handler)
			{
				IRequestIndexEntry item = handler.CreateRequestIndexEntryFromRequestJob(requestJobBase, requestIndexId);
				requestJobBase.IndexIds.Add(requestIndexId);
				requestJobBase.IndexEntries.Add(item);
			});
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0002D204 File Offset: 0x0002B404
		public static void CreateAndPopulateRequestIndexEntries(RequestJobBase requestJobBase, IConfigurationSession session)
		{
			if (requestJobBase == null)
			{
				throw new ArgumentNullException("requestJobBase");
			}
			if (requestJobBase.IndexEntries == null)
			{
				requestJobBase.IndexEntries = new List<IRequestIndexEntry>();
			}
			if (requestJobBase.IndexIds == null)
			{
				requestJobBase.IndexIds = new List<RequestIndexId>();
			}
			RequestIndexId indexId = new RequestIndexId(RequestIndexLocation.AD);
			RequestIndexEntryProvider.Handle(indexId.RequestIndexEntryType, delegate(IRequestIndexEntryHandler handler)
			{
				IRequestIndexEntry item = handler.CreateRequestIndexEntryFromRequestJob(requestJobBase, session);
				requestJobBase.IndexIds.Add(indexId);
				requestJobBase.IndexEntries.Add(item);
			});
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0002D2B8 File Offset: 0x0002B4B8
		public void Delete(IConfigurable instance)
		{
			IRequestIndexEntry requestIndexEntry = RequestIndexEntryProvider.ValidateInstance(instance);
			RequestIndexEntryProvider.Handle(requestIndexEntry.GetType(), delegate(IRequestIndexEntryHandler handler)
			{
				handler.Delete(this, requestIndexEntry);
			});
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0002D328 File Offset: 0x0002B528
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			Type requestIndexEntryType = RequestIndexEntryProvider.ValidateQueryFilter<T>(filter);
			IRequestIndexEntry[] array = RequestIndexEntryProvider.Handle<IRequestIndexEntry[]>(requestIndexEntryType, (IRequestIndexEntryHandler handler) => handler.Find(this, filter, rootId, deepSearch, sortBy));
			if (array == null)
			{
				return Array<IConfigurable>.Empty;
			}
			Type typeFromHandle = typeof(T);
			if (array.GetType().GetElementType() == typeFromHandle)
			{
				return array.Cast<IConfigurable>().ToArray<IConfigurable>();
			}
			if (typeFromHandle.IsSubclassOf(typeof(RequestBase)))
			{
				return array.Select(new Func<IRequestIndexEntry, T>(RequestIndexEntryProvider.CreateRequest<T>)).Cast<IConfigurable>().ToArray<IConfigurable>();
			}
			MrsTracer.Common.Warning("IndexId not supported by this provider.", new object[0]);
			return Array<IConfigurable>.Empty;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0002D430 File Offset: 0x0002B630
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			Type requestIndexEntryType = RequestIndexEntryProvider.ValidateQueryFilter<T>(filter);
			IEnumerable<IRequestIndexEntry> enumerable = RequestIndexEntryProvider.Handle<IEnumerable<IRequestIndexEntry>>(requestIndexEntryType, (IRequestIndexEntryHandler handler) => handler.FindPaged(this, filter, rootId, deepSearch, sortBy, pageSize));
			if (enumerable == null)
			{
				return Array<T>.Empty;
			}
			if (typeof(T).IsSubclassOf(typeof(RequestBase)))
			{
				return enumerable.Select(new Func<IRequestIndexEntry, T>(RequestIndexEntryProvider.CreateRequest<T>));
			}
			if (enumerable is IEnumerable<T>)
			{
				return (IEnumerable<T>)enumerable;
			}
			MrsTracer.Common.Warning("IndexId not supported by this provider.", new object[0]);
			return Array<T>.Empty;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0002D4F0 File Offset: 0x0002B6F0
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			RequestIndexEntryObjectId requestIndexEntryObjectId = identity as RequestIndexEntryObjectId;
			if (requestIndexEntryObjectId == null)
			{
				throw new ArgumentException("This provider only supports RequestIndexEntryObjectIds", "identity");
			}
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsSubclassOf(typeof(RequestBase)) && !typeof(IRequestIndexEntry).IsAssignableFrom(typeFromHandle))
			{
				throw new ArgumentException("This provider only supports reading types of RequestBase or IRequestIndexEntry");
			}
			IRequestIndexEntry requestIndexEntry = this.Read(requestIndexEntryObjectId);
			if (requestIndexEntry == null || requestIndexEntry is T)
			{
				return requestIndexEntry;
			}
			if (!typeFromHandle.IsSubclassOf(typeof(RequestBase)))
			{
				return null;
			}
			return RequestIndexEntryProvider.CreateRequest<T>(requestIndexEntry);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0002D5B0 File Offset: 0x0002B7B0
		public void Save(IConfigurable instance)
		{
			IRequestIndexEntry requestIndexEntry = RequestIndexEntryProvider.ValidateInstance(instance);
			RequestIndexEntryProvider.Handle(requestIndexEntry.GetType(), delegate(IRequestIndexEntryHandler handler)
			{
				handler.Save(this, requestIndexEntry);
			});
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0002D610 File Offset: 0x0002B810
		public IRequestIndexEntry Read(RequestIndexEntryObjectId objectId)
		{
			if (objectId.IndexId.RequestIndexEntryType == null)
			{
				return null;
			}
			return RequestIndexEntryProvider.Handle<IRequestIndexEntry>(objectId.IndexId.RequestIndexEntryType, (IRequestIndexEntryHandler handler) => handler.Read(this, objectId));
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0002D66C File Offset: 0x0002B86C
		internal static T CreateRequest<T>(IRequestIndexEntry requestIndexEntry) where T : IConfigurable, new()
		{
			if (requestIndexEntry == null)
			{
				throw new ArgumentNullException("requestIndexEntry");
			}
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			RequestBase requestBase = t as RequestBase;
			if (requestBase == null)
			{
				throw new ArgumentException(string.Format("Type [{0}] not supported.  Must specify a derivative of RequestBase.", typeof(T).Name));
			}
			requestBase.Initialize(requestIndexEntry);
			return t;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0002D6E0 File Offset: 0x0002B8E0
		internal T Read<T>(Func<IRecipientSession, T> function) where T : class
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			int num = 0;
			T result;
			try
			{
				IL_10:
				result = function(this.RecipientSession);
			}
			catch (LocalizedException ex)
			{
				if (ex is ADTransientException || ex is DomainControllerFromWrongDomainException)
				{
					if (num >= 2)
					{
						MrsTracer.Common.Error("Another error connecting to Domain Controller: '{0}' of type '{1}'. Failing.", new object[]
						{
							CommonUtils.FullExceptionMessage(ex),
							ex.GetType().ToString()
						});
						throw new UnableToReadADTransientException(ex);
					}
					string text = string.Empty;
					this.domainController = null;
					if (this.recipSession != null)
					{
						this.recipSession.DomainController = null;
						text = this.recipSession.Source;
					}
					MrsTracer.Common.Warning("Error connecting to Domain Controller ('{0}'): {1}, trying another.", new object[]
					{
						text ?? string.Empty,
						CommonUtils.FullExceptionMessage(ex)
					});
					num++;
					goto IL_10;
				}
				else
				{
					if (ex is ADOperationException)
					{
						MrsTracer.Common.Error("ADOperationException while talking to Domain Controller '{0}': '{1}'", new object[]
						{
							this.RecipientSession.Source,
							CommonUtils.FullExceptionMessage(ex)
						});
						throw new UnableToReadADPermanentException(ex);
					}
					throw;
				}
			}
			return result;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0002D8A8 File Offset: 0x0002BAA8
		internal ADUser ReadADUser(ADObjectId userId, Guid exchangeGuid)
		{
			if (userId == null)
			{
				return null;
			}
			ADRecipient adrecipient = this.Read<ADRecipient>(delegate(IRecipientSession session)
			{
				if (CommonUtils.IsMultiTenantEnabled() && exchangeGuid != Guid.Empty && !userId.GetPartitionId().Equals(this.RecipientSession.SessionSettings.PartitionId))
				{
					return session.FindByExchangeGuidIncludingArchive(exchangeGuid);
				}
				return session.Read(userId);
			});
			if (adrecipient == null)
			{
				MrsTracer.Common.Warning("No ADRecipient found with Identity '{0}' in organizaton '{1}'.", new object[]
				{
					userId.ToString(),
					this.orgId.ToString()
				});
				return null;
			}
			ADUser aduser = adrecipient as ADUser;
			if (aduser == null)
			{
				MrsTracer.Common.Warning("'{0}' is not a user.", new object[]
				{
					userId.ToString()
				});
				return null;
			}
			return aduser;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0002D95C File Offset: 0x0002BB5C
		internal void UpdateADData(ADUser user, TransactionalRequestJob requestJob, bool save)
		{
			if (save)
			{
				user.MailboxMoveStatus = RequestJobBase.GetVersionAppropriateStatus(requestJob.Status, user.ExchangeVersion);
				user.MailboxMoveFlags = RequestJobBase.GetVersionAppropriateFlags(requestJob.Flags, user.ExchangeVersion);
				user.MailboxMoveSourceMDB = requestJob.SourceDatabase;
				user.MailboxMoveTargetMDB = requestJob.TargetDatabase;
				user.MailboxMoveSourceArchiveMDB = requestJob.SourceArchiveDatabase;
				user.MailboxMoveTargetArchiveMDB = requestJob.TargetArchiveDatabase;
				user.MailboxMoveRemoteHostName = requestJob.RemoteHostName;
				user.MailboxMoveBatchName = requestJob.BatchName;
				return;
			}
			user.MailboxMoveStatus = RequestStatus.None;
			user.MailboxMoveFlags = RequestFlags.None;
			user.MailboxMoveSourceMDB = null;
			user.MailboxMoveTargetMDB = null;
			user.MailboxMoveSourceArchiveMDB = null;
			user.MailboxMoveTargetArchiveMDB = null;
			user.MailboxMoveRemoteHostName = null;
			user.MailboxMoveBatchName = null;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0002DA1C File Offset: 0x0002BC1C
		internal IDisposable RescopeTo(string domainController, OrganizationId orgId)
		{
			IDisposable result = new RequestIndexEntryProvider.ScopeRestorer(this);
			if (this.OwnsSessions)
			{
				if (!StringComparer.OrdinalIgnoreCase.Equals(this.domainController, domainController) || !this.orgId.Equals(orgId))
				{
					this.recipSession = null;
					this.configSession = null;
				}
			}
			else
			{
				if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.recipSession, orgId))
				{
					this.recipSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.recipSession, orgId, true);
				}
				if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.configSession, orgId))
				{
					this.configSession = (IConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(this.configSession, orgId, true);
				}
			}
			this.domainController = domainController;
			this.orgId = (orgId ?? OrganizationId.ForestWideOrgId);
			return result;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0002DAD0 File Offset: 0x0002BCD0
		private static void Handle(Type requestIndexEntryType, Action<IRequestIndexEntryHandler> action)
		{
			if (requestIndexEntryType == null)
			{
				throw new ArgumentNullException("requestIndexEntryType");
			}
			IRequestIndexEntryHandler obj;
			if (RequestIndexEntryProvider.requestIndexEntryHandlers.TryGetValue(requestIndexEntryType, out obj))
			{
				action(obj);
			}
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0002DB24 File Offset: 0x0002BD24
		private static T Handle<T>(Type requestIndexEntryType, Func<IRequestIndexEntryHandler, T> function)
		{
			T returnValue = default(T);
			RequestIndexEntryProvider.Handle(requestIndexEntryType, delegate(IRequestIndexEntryHandler handler)
			{
				returnValue = function(handler);
			});
			return returnValue;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0002DB64 File Offset: 0x0002BD64
		private static IRequestIndexEntry ValidateInstance(IConfigurable instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			IRequestIndexEntry requestIndexEntry = instance as IRequestIndexEntry;
			if (requestIndexEntry == null)
			{
				throw new ArgumentException(string.Format("This provider only supports IRequestIndexEntrys but was provided {0}.", instance.GetType().Name), "instance");
			}
			return requestIndexEntry;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0002DBAC File Offset: 0x0002BDAC
		private static Type ValidateQueryFilter<T>(QueryFilter filter) where T : IConfigurable
		{
			RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = filter as RequestIndexEntryQueryFilter;
			if (filter != null && requestIndexEntryQueryFilter == null)
			{
				throw new ArgumentException("This provider only supports RequestIndexEntryQueryFilters", "filter");
			}
			Type result = typeof(T);
			if (requestIndexEntryQueryFilter != null)
			{
				result = requestIndexEntryQueryFilter.IndexId.RequestIndexEntryType;
			}
			return result;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0002DBF4 File Offset: 0x0002BDF4
		private void CreateSessions()
		{
			MrsTracer.Common.Debug("Creating AD sessions for '{0}'", new object[]
			{
				this.OrgId
			});
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.orgId);
			adsessionSettings.IncludeInactiveMailbox = true;
			adsessionSettings.IncludeSoftDeletedObjects = true;
			this.recipSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.domainController, false, ConsistencyMode.PartiallyConsistent, adsessionSettings, 854, "CreateSessions", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Common\\MRSRequestProviders\\IndexProvider\\RequestIndexEntryProvider.cs");
			this.recipSession.EnforceDefaultScope = false;
			this.configSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.domainController, false, ConsistencyMode.PartiallyConsistent, adsessionSettings, 862, "CreateSessions", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Common\\MRSRequestProviders\\IndexProvider\\RequestIndexEntryProvider.cs");
			this.configSession.SessionSettings.IncludeSoftDeletedObjectLinks = true;
		}

		// Token: 0x04000A2B RID: 2603
		private static readonly Dictionary<Type, IRequestIndexEntryHandler> requestIndexEntryHandlers = new Dictionary<Type, IRequestIndexEntryHandler>
		{
			{
				typeof(MRSRequestWrapper),
				new ADHandler()
			},
			{
				typeof(AggregatedAccountConfigurationWrapper),
				new UserMailboxHandler<AggregatedAccountConfigurationWrapper>()
			},
			{
				typeof(MRSRequestMailboxEntry),
				new MailboxRequestIndexEntryHandler()
			},
			{
				typeof(AggregatedAccountListConfigurationWrapper),
				new UserMailboxHandler<AggregatedAccountListConfigurationWrapper>()
			}
		};

		// Token: 0x04000A2C RID: 2604
		private readonly bool ownsSessions;

		// Token: 0x04000A2D RID: 2605
		private IRecipientSession recipSession;

		// Token: 0x04000A2E RID: 2606
		private IConfigurationSession configSession;

		// Token: 0x04000A2F RID: 2607
		private string domainController;

		// Token: 0x04000A30 RID: 2608
		private OrganizationId orgId;

		// Token: 0x020001DE RID: 478
		private class ScopeRestorer : DisposeTrackableBase
		{
			// Token: 0x060013F1 RID: 5105 RVA: 0x0002DD15 File Offset: 0x0002BF15
			public ScopeRestorer(RequestIndexEntryProvider owner)
			{
				this.ownerProvider = owner;
				this.savedRecipSession = owner.recipSession;
				this.savedConfigSession = owner.configSession;
				this.savedDomainController = owner.domainController;
				this.savedOrgId = owner.orgId;
			}

			// Token: 0x060013F2 RID: 5106 RVA: 0x0002DD54 File Offset: 0x0002BF54
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					this.ownerProvider.recipSession = this.savedRecipSession;
					this.ownerProvider.configSession = this.savedConfigSession;
					this.ownerProvider.orgId = this.savedOrgId;
					this.ownerProvider.domainController = this.savedDomainController;
				}
			}

			// Token: 0x060013F3 RID: 5107 RVA: 0x0002DDA8 File Offset: 0x0002BFA8
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<RequestIndexEntryProvider.ScopeRestorer>(this);
			}

			// Token: 0x04000A31 RID: 2609
			private readonly IRecipientSession savedRecipSession;

			// Token: 0x04000A32 RID: 2610
			private readonly IConfigurationSession savedConfigSession;

			// Token: 0x04000A33 RID: 2611
			private readonly string savedDomainController;

			// Token: 0x04000A34 RID: 2612
			private readonly OrganizationId savedOrgId;

			// Token: 0x04000A35 RID: 2613
			private readonly RequestIndexEntryProvider ownerProvider;
		}
	}
}
