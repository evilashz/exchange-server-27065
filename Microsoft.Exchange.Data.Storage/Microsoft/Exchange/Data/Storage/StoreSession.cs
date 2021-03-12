using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Compliance.Logging;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Data.Storage.MailboxRules;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.Win32;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;
using Microsoft.Mapi.Security;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class StoreSession : IStoreSession, IDisposeTrackable, IDisposable
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003B79 File Offset: 0x00001D79
		internal static int CurrentServerMajorVersion
		{
			get
			{
				return StoreSession.currentServerMajorVersion;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003B80 File Offset: 0x00001D80
		internal UnifiedGroupMemberType UnifiedGroupMemberType
		{
			get
			{
				UnifiedGroupMemberType result;
				using (this.CheckDisposed("UnifiedGroupMemberType::get"))
				{
					result = this.unifiedGroupMemberType;
				}
				return result;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public virtual IActivitySession ActivitySession
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003BC8 File Offset: 0x00001DC8
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003C0C File Offset: 0x00001E0C
		public IContentIndexingSession ContentIndexingSession
		{
			get
			{
				IContentIndexingSession result;
				using (this.CheckDisposed("ContentIndexingSession::get"))
				{
					result = this.contentIndexingSession;
				}
				return result;
			}
			set
			{
				using (this.CheckDisposed("ContentIndexingSession::set"))
				{
					this.contentIndexingSession = value;
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003C4C File Offset: 0x00001E4C
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003C53 File Offset: 0x00001E53
		public static bool UseRPCContextPoolResiliency
		{
			get
			{
				return StoreSession.useRPCContextPoolResiliency;
			}
			set
			{
				StoreSession.useRPCContextPoolResiliency = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003C5B File Offset: 0x00001E5B
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003C5E File Offset: 0x00001E5E
		public static bool UseRPCContextPool
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003C60 File Offset: 0x00001E60
		protected StoreSession()
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003C91 File Offset: 0x00001E91
		protected StoreSession(CultureInfo cultureInfo, string clientInfoString) : this(cultureInfo, clientInfoString, null)
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003C9C File Offset: 0x00001E9C
		protected StoreSession(CultureInfo cultureInfo, string clientInfoString, IBudget budget)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				StorageGlobals.TraceConstructIDisposable(this);
				this.subscriptionsManager = new SubscriptionsManager();
				this.budget = budget;
				this.disposeTracker = this.GetDisposeTracker();
				this.sessionCapabilities = SessionCapabilities.PrimarySessionCapabilities;
				if (cultureInfo == null)
				{
					throw new ArgumentNullException("cultureInfo");
				}
				if (clientInfoString == null)
				{
					throw new ArgumentNullException("clientInfoString");
				}
				if (clientInfoString.Length == 0)
				{
					throw new ArgumentException("clientInfoString has zero length", "clientInfoString");
				}
				this.idConverter = new IdConverter(this);
				this.clientInfoString = clientInfoString;
				this.SessionCultureInfo = cultureInfo;
				this.schema = MailboxSchema.Instance;
				this.clientIPAddress = IPAddress.IPv6Loopback;
				this.serverIPAddress = IPAddress.IPv6Loopback;
				this.InternalInitializeGccProtocolSession();
				this.InitializeADSessionFactory();
				disposeGuard.Success();
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003DB0 File Offset: 0x00001FB0
		public static bool IsPublicFolderMailbox(int type)
		{
			return type == 1 || type == 2;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003DCC File Offset: 0x00001FCC
		public static bool IsArchiveMailbox(int type)
		{
			return (type & 32) == 32;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public static bool IsGroupMailbox(int type)
		{
			return type == 4;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public static bool IsUserMailbox(int type)
		{
			return type == 1;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003E0C File Offset: 0x0000200C
		public static bool IsTeamSiteMailbox(int type)
		{
			return type == 3;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003E20 File Offset: 0x00002020
		public static bool IsSharedMailbox(int type)
		{
			return type == 2;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E33 File Offset: 0x00002033
		protected static CultureInfo GetCultureWithoutInvariant(CultureInfo culture)
		{
			if (culture == CultureInfo.InvariantCulture)
			{
				return null;
			}
			return culture;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003E40 File Offset: 0x00002040
		public virtual bool IsRemote
		{
			get
			{
				bool result;
				using (this.CheckDisposed("IsRemote::get"))
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006D RID: 109
		public abstract IExchangePrincipal MailboxOwner { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003E7C File Offset: 0x0000207C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003EC4 File Offset: 0x000020C4
		public bool IsMoveUser
		{
			get
			{
				bool isMoveUser;
				using (this.CheckDisposed("IsMoveUser::get"))
				{
					isMoveUser = this.operationContext.IsMoveUser;
				}
				return isMoveUser;
			}
			protected set
			{
				using (this.CheckDisposed("IsMoveUser::set"))
				{
					this.operationContext.IsMoveUser = value;
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003F0C File Offset: 0x0000210C
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003F54 File Offset: 0x00002154
		public bool IsMoveSource
		{
			get
			{
				bool isMoveSource;
				using (this.CheckDisposed("IsMoveSource::get"))
				{
					isMoveSource = this.operationContext.IsMoveSource;
				}
				return isMoveSource;
			}
			set
			{
				using (this.CheckDisposed("IsMoveSource::set"))
				{
					this.operationContext.IsMoveSource = value;
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003F9C File Offset: 0x0000219C
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003FE4 File Offset: 0x000021E4
		public bool IsXForestMove
		{
			get
			{
				bool isXForestMove;
				using (this.CheckDisposed("IsXForestMove::get"))
				{
					isXForestMove = this.operationContext.IsXForestMove;
				}
				return isXForestMove;
			}
			set
			{
				using (this.CheckDisposed("IsXForestMove::set"))
				{
					this.operationContext.IsXForestMove = value;
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000402C File Offset: 0x0000222C
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00004074 File Offset: 0x00002274
		public bool IsOlcMoveDestination
		{
			get
			{
				bool isOlcMoveDestination;
				using (this.CheckDisposed("IsOlcMoveDestination::get"))
				{
					isOlcMoveDestination = this.operationContext.IsOlcMoveDestination;
				}
				return isOlcMoveDestination;
			}
			set
			{
				using (this.CheckDisposed("IsOlcMoveDestination::set"))
				{
					this.operationContext.IsOlcMoveDestination = value;
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000040BC File Offset: 0x000022BC
		public ExchangeOperationContext OperationContext
		{
			get
			{
				ExchangeOperationContext result;
				using (this.CheckDisposed("OperationContext::get"))
				{
					result = this.operationContext;
				}
				return result;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004100 File Offset: 0x00002300
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00004144 File Offset: 0x00002344
		public bool BlockFolderCreation
		{
			get
			{
				bool result;
				using (this.CheckDisposed("BlockFolderCreation::get"))
				{
					result = this.blockFolderCreation;
				}
				return result;
			}
			set
			{
				using (this.CheckDisposed("BlockFolderCreation::set"))
				{
					this.blockFolderCreation = value;
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004184 File Offset: 0x00002384
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000041D4 File Offset: 0x000023D4
		public MailboxMoveStage MailboxMoveStage
		{
			get
			{
				MailboxMoveStage result;
				using (this.CheckDisposed("MailboxMoveStage::get"))
				{
					if (!this.IsMoveUser)
					{
						result = MailboxMoveStage.None;
					}
					else
					{
						result = this.mailboxMoveStage;
					}
				}
				return result;
			}
			set
			{
				using (this.CheckDisposed("MailboxMoveStage::set"))
				{
					this.mailboxMoveStage = value;
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004214 File Offset: 0x00002414
		public virtual bool IsPublicFolderSession
		{
			get
			{
				bool result;
				using (this.CheckDisposed("IsPublicFolderSession.get"))
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004250 File Offset: 0x00002450
		public byte[] PersistableTenantPartitionHint
		{
			get
			{
				return this.Mailbox.TryGetProperty(MailboxSchema.PersistableTenantPartitionHint) as byte[];
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004267 File Offset: 0x00002467
		protected ObjectAccessGuard CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(this.ToString());
			}
			return ObjectAccessGuard.Create(this.sessionThreadTracker, methodName);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004290 File Offset: 0x00002490
		protected virtual ObjectAccessGuard CheckObjectState(string methodName)
		{
			return this.CheckDisposed(methodName);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004299 File Offset: 0x00002499
		protected ObjectAccessGuard CreateSessionGuard(string methodName)
		{
			return ObjectAccessGuard.Create(this.sessionThreadTracker, methodName);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000042A8 File Offset: 0x000024A8
		internal IDictionary<StoreObjectId, bool> IsContactFolder
		{
			get
			{
				IDictionary<StoreObjectId, bool> result;
				using (this.CreateSessionGuard("IsContactFolder::get"))
				{
					if (this.isContactFolder == null)
					{
						this.isContactFolder = new Dictionary<StoreObjectId, bool>();
					}
					result = this.isContactFolder;
				}
				return result;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000042FC File Offset: 0x000024FC
		internal void CheckCapabilities(bool test, string message)
		{
			using (this.CreateSessionGuard("CheckCapabilities"))
			{
				if (!test)
				{
					throw new InvalidOperationException("Session does not have capability " + message);
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000434C File Offset: 0x0000254C
		public virtual void Dispose()
		{
			using (this.CreateSessionGuard("Dispose"))
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004394 File Offset: 0x00002594
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				if (disposing)
				{
					this.InternalEndGccProtocolSession();
				}
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000043C2 File Offset: 0x000025C2
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.InternalDisposeGccProtocolSession();
				Util.DisposeIfPresent(this.subscriptionsManager);
				this.StopDeadSessionChecking();
				this.SetMailboxStoreObject(null);
				Util.DisposeIfPresent(this.disposeTracker);
			}
		}

		// Token: 0x06000085 RID: 133
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x06000086 RID: 134 RVA: 0x000043F4 File Offset: 0x000025F4
		public void SuppressDisposeTracker()
		{
			using (this.CreateSessionGuard("SuppressDisposeTracker"))
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Suppress();
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004440 File Offset: 0x00002640
		public AggregateOperationResult Delete(DeleteItemFlags deleteFlags, params StoreId[] ids)
		{
			return this.Delete(deleteFlags, false, ids);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004494 File Offset: 0x00002694
		public AggregateOperationResult Delete(DeleteItemFlags deleteFlags, bool returnNewItemIds, params StoreId[] ids)
		{
			AggregateOperationResult result;
			using (this.CheckObjectState("Delete"))
			{
				this.CheckDeleteItemFlags(deleteFlags);
				ExTraceGlobals.SessionTracer.Information((long)this.GetHashCode(), "StoreSession::DeleteItems.");
				List<OccurrenceStoreObjectId> list = new List<OccurrenceStoreObjectId>();
				List<StoreId> list2 = new List<StoreId>();
				Folder.GroupOccurrencesAndObjectIds(ids, list2, list);
				List<GroupOperationResult> list3 = new List<GroupOperationResult>();
				Dictionary<StoreObjectId, List<StoreId>> dictionary = new Dictionary<StoreObjectId, List<StoreId>>();
				this.GroupNonOccurrenceByFolder(list2, dictionary, list3);
				this.ExecuteOperationOnObjects(dictionary, list3, (Folder sourceFolder, StoreId[] sourceObjectIds) => sourceFolder.DeleteObjects(deleteFlags, returnNewItemIds, sourceObjectIds));
				using (List<OccurrenceStoreObjectId>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						OccurrenceStoreObjectId occurrenceId = enumerator.Current;
						Folder.ExecuteGroupOperationAndAggregateResults(list3, new StoreObjectId[]
						{
							occurrenceId
						}, () => this.DeleteCalendarOccurrence(deleteFlags, occurrenceId));
					}
				}
				result = Folder.CreateAggregateOperationResult(list3);
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000045E4 File Offset: 0x000027E4
		public AggregateOperationResult Move(StoreId destinationFolderId, params StoreId[] ids)
		{
			AggregateOperationResult result;
			using (this.CreateSessionGuard("Move"))
			{
				result = this.Move(this, destinationFolderId, ids);
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004628 File Offset: 0x00002828
		public AggregateOperationResult Move(StoreSession destinationSession, StoreId destinationFolderId, params StoreId[] ids)
		{
			AggregateOperationResult result;
			using (this.CreateSessionGuard("Move"))
			{
				result = this.Move(destinationSession, destinationFolderId, null, ids);
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004678 File Offset: 0x00002878
		public AggregateOperationResult Move(StoreSession destinationSession, StoreId destinationFolderId, DeleteItemFlags? deleteFlags, params StoreId[] ids)
		{
			AggregateOperationResult result;
			using (this.CreateSessionGuard("Move"))
			{
				result = this.Move(destinationSession, destinationFolderId, false, deleteFlags, ids);
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000046C0 File Offset: 0x000028C0
		public AggregateOperationResult Move(StoreSession destinationSession, StoreId destinationFolderId, bool returnNewIds, params StoreId[] ids)
		{
			AggregateOperationResult result;
			using (this.CreateSessionGuard("Move"))
			{
				result = this.Move(destinationSession, destinationFolderId, returnNewIds, null, ids);
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000473C File Offset: 0x0000293C
		public AggregateOperationResult Move(StoreSession destinationSession, StoreId destinationFolderId, bool returnNewIds, DeleteItemFlags? deleteFlags, params StoreId[] ids)
		{
			AggregateOperationResult result;
			using (this.CheckObjectState("Move"))
			{
				ExTraceGlobals.SessionTracer.Information<int>((long)this.GetHashCode(), "StoreSession::Move. HashCode = {0}", this.GetHashCode());
				List<GroupOperationResult> list = new List<GroupOperationResult>();
				Dictionary<StoreObjectId, List<StoreId>> dictionary = new Dictionary<StoreObjectId, List<StoreId>>();
				this.GroupNonOccurrenceByFolder(ids, dictionary, list);
				this.ExecuteOperationOnObjects(dictionary, list, (Folder sourceFolder, StoreId[] sourceObjectIds) => sourceFolder.MoveObjects(destinationSession, destinationFolderId, returnNewIds, deleteFlags, sourceObjectIds));
				result = Folder.CreateAggregateOperationResult(list);
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000047F0 File Offset: 0x000029F0
		public AggregateOperationResult Copy(StoreId destinationFolderId, params StoreId[] ids)
		{
			AggregateOperationResult result;
			using (this.CreateSessionGuard("Copy"))
			{
				result = this.Copy(this, destinationFolderId, ids);
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004834 File Offset: 0x00002A34
		public AggregateOperationResult Copy(StoreSession destinationSession, StoreId destinationFolderId, params StoreId[] ids)
		{
			AggregateOperationResult result;
			using (this.CreateSessionGuard("Copy"))
			{
				result = this.Copy(destinationSession, destinationFolderId, false, ids);
			}
			return result;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000048A0 File Offset: 0x00002AA0
		public AggregateOperationResult Copy(StoreSession destinationSession, StoreId destinationFolderId, bool returnNewIds, params StoreId[] ids)
		{
			AggregateOperationResult result;
			using (this.CheckObjectState("Copy"))
			{
				ExTraceGlobals.SessionTracer.Information<int>((long)this.GetHashCode(), "StoreSession::Copy. HashCode = {0}", this.GetHashCode());
				List<GroupOperationResult> list = new List<GroupOperationResult>();
				Dictionary<StoreObjectId, List<StoreId>> dictionary = new Dictionary<StoreObjectId, List<StoreId>>();
				this.GroupNonOccurrenceByFolder(ids, dictionary, list);
				this.ExecuteOperationOnObjects(dictionary, list, (Folder sourceFolder, StoreId[] sourceObjectIds) => sourceFolder.CopyObjects(destinationSession, destinationFolderId, returnNewIds, sourceObjectIds));
				result = Folder.CreateAggregateOperationResult(list);
			}
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000494C File Offset: 0x00002B4C
		public void DoneWithMessage(Item item)
		{
			using (this.CheckObjectState("DoneWithMessage"))
			{
				this.CheckCapabilities(this.Capabilities.CanSend, "CanSend");
				if (item == null)
				{
					ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), "StoreSession::DoneWithMessage. DoneWithMessage cannot be called on a Null item.");
					throw new ArgumentNullException("item");
				}
				MapiMessage mapiMessage = item.MapiMessage;
				if (mapiMessage == null)
				{
					ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), "StoreSession::DoneWithMessage. DoneWithMessage cannot be called on a Null MapiMessage.");
					throw new ArgumentException("item");
				}
				MailboxSession mailboxSession = this as MailboxSession;
				StoreObjectId storeObjectId = item.StoreObjectId;
				StoreObjectId sourceFolderId = null;
				StoreObjectId destinationFolderId = null;
				List<StoreObjectId> list = null;
				FolderChangeOperation operation = FolderChangeOperation.Move;
				using (CallbackContext callbackContext = new CallbackContext(this))
				{
					if (mailboxSession != null)
					{
						if (item.GetValueOrDefault<bool>(InternalSchema.DeleteAfterSubmit))
						{
							if (item.GetValueOrDefault<byte[]>(InternalSchema.TargetEntryId) == null)
							{
								operation = FolderChangeOperation.DoneWithMessageDelete;
							}
							else
							{
								operation = FolderChangeOperation.Copy;
							}
							destinationFolderId = null;
						}
						else
						{
							operation = FolderChangeOperation.Move;
							byte[] valueOrDefault = item.GetValueOrDefault<byte[]>(InternalSchema.SentMailEntryId);
							if (valueOrDefault != null)
							{
								destinationFolderId = StoreObjectId.FromProviderSpecificId(valueOrDefault, StoreObjectType.Folder);
							}
							else
							{
								destinationFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.SentItems);
							}
						}
						sourceFolderId = item.ParentId;
						list = new List<StoreObjectId>(1);
						if (storeObjectId != null)
						{
							list.Add(storeObjectId);
						}
						this.OnBeforeFolderChange(operation, FolderChangeOperationFlags.IncludeAll, this, this, sourceFolderId, destinationFolderId, list, callbackContext);
					}
					LocalizedException ex = null;
					try
					{
						bool flag = false;
						try
						{
							if (this != null)
							{
								this.BeginMapiCall();
								this.BeginServerHealthCall();
								flag = true;
							}
							if (StorageGlobals.MapiTestHookBeforeCall != null)
							{
								StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
							}
							mapiMessage.DoneWithMessage();
						}
						catch (MapiPermanentException ex2)
						{
							throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotFinishSubmit, ex2, this, this, "{0}. MapiException = {1}.", new object[]
							{
								string.Format("StoreSession::DoneWithMessage.", new object[0]),
								ex2
							});
						}
						catch (MapiRetryableException ex3)
						{
							throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotFinishSubmit, ex3, this, this, "{0}. MapiException = {1}.", new object[]
							{
								string.Format("StoreSession::DoneWithMessage.", new object[0]),
								ex3
							});
						}
						finally
						{
							try
							{
								if (this != null)
								{
									this.EndMapiCall();
									if (flag)
									{
										this.EndServerHealthCall();
									}
								}
							}
							finally
							{
								if (StorageGlobals.MapiTestHookAfterCall != null)
								{
									StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
								}
							}
						}
					}
					catch (StorageTransientException ex4)
					{
						ex = ex4;
						throw;
					}
					catch (StoragePermanentException ex5)
					{
						ex = ex5;
						throw;
					}
					finally
					{
						if (mailboxSession != null)
						{
							GroupOperationResult result = new GroupOperationResult((ex == null) ? OperationResult.Succeeded : OperationResult.Failed, list.ToArray(), ex);
							this.OnAfterFolderChange(operation, FolderChangeOperationFlags.IncludeAll, this, this, sourceFolderId, destinationFolderId, list, result, callbackContext);
						}
					}
				}
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004C94 File Offset: 0x00002E94
		public GroupOperationResult DeleteAllObjects(DeleteItemFlags flags, StoreId folderId)
		{
			GroupOperationResult result;
			using (this.CheckObjectState("DeleteAllObjects"))
			{
				this.CheckDeleteItemFlags(flags);
				ExTraceGlobals.SessionTracer.Information<int>((long)this.GetHashCode(), "StoreSession::DeleteAllObjects. HashCode = {0}", this.GetHashCode());
				this.CheckDeleteItemFlags(flags);
				if (folderId == null)
				{
					ExTraceGlobals.SessionTracer.TraceError<string>((long)this.GetHashCode(), "StoreSession::DeleteAllObjects. The folder Id cannot be null. Argument = {0}.", "folderId");
					throw new ArgumentNullException(ServerStrings.ExNullParameter("folderId", 2));
				}
				using (Folder folder = Folder.Bind(this, folderId))
				{
					result = folder.DeleteAllObjects(flags);
				}
			}
			return result;
		}

		// Token: 0x06000093 RID: 147
		public abstract void Connect();

		// Token: 0x06000094 RID: 148
		public abstract void Disconnect();

		// Token: 0x06000095 RID: 149 RVA: 0x00004D54 File Offset: 0x00002F54
		public void MarkAsRead(bool suppressReadReceipts, params StoreId[] itemIds)
		{
			this.MarkAsRead(suppressReadReceipts, itemIds, false, false);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004D60 File Offset: 0x00002F60
		public void MarkAsRead(bool suppressReadReceipts, bool suppressNotReadReceipts, params StoreId[] itemIds)
		{
			this.MarkAsRead(suppressReadReceipts, itemIds, false, suppressNotReadReceipts);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004D6C File Offset: 0x00002F6C
		public void MarkAsRead(bool suppressReadReceipts, StoreId[] itemIds, bool throwIfWarning, bool suppressNotReadReceipts = false)
		{
			using (this.CheckObjectState("MarkAsRead"))
			{
				if (itemIds == null)
				{
					ExTraceGlobals.SessionTracer.TraceError<string>((long)this.GetHashCode(), "StoreSession::MarkAsRead. The parameter cannot be null. Parameter = {0}.", "itemIds");
					throw new ArgumentNullException(ServerStrings.ExNullParameter("itemIds", 1));
				}
				ExTraceGlobals.SessionTracer.Information<int>((long)this.GetHashCode(), "StoreSession::MarkAsRead. itemIds.Length = {0}.", itemIds.Length);
				if (itemIds.Length != 0)
				{
					this.InternalMarkReadFlagsByGroup(suppressReadReceipts, true, itemIds, throwIfWarning, suppressNotReadReceipts);
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004E04 File Offset: 0x00003004
		public void MarkAsUnread(bool suppressReadReceipts, params StoreId[] ids)
		{
			this.MarkAsUnread(suppressReadReceipts, ids, false);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004E10 File Offset: 0x00003010
		public void MarkAsUnread(bool suppressReadReceipts, StoreId[] ids, bool throwIfWarning)
		{
			using (this.CheckObjectState("MarkAsUnread"))
			{
				if (ids == null)
				{
					ExTraceGlobals.SessionTracer.TraceError<string>((long)this.GetHashCode(), "StoreSession::MarkAsUnRead. The parameter cannot be null. Parameter = {0}.", "itemIds");
					throw new ArgumentNullException(ServerStrings.ExNullParameter("itemIds", 1));
				}
				ExTraceGlobals.SessionTracer.Information<int>((long)this.GetHashCode(), "StoreSession::MarkAsUnRead. itemIds.Length = {0}.", ids.Length);
				if (ids.Length != 0)
				{
					this.InternalMarkReadFlagsByGroup(suppressReadReceipts, false, ids, throwIfWarning, false);
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004EA8 File Offset: 0x000030A8
		public Mailbox Mailbox
		{
			get
			{
				Mailbox mailbox;
				using (this.CheckObjectState("Mailbox::get"))
				{
					if (this.mailboxStoreObject == null)
					{
						throw new ConnectionFailedPermanentException(new LocalizedString(ServerStrings.ExStoreSessionDisconnected + ", mailboxStoreObject = null"));
					}
					mailbox = this.mailboxStoreObject.Mailbox;
				}
				return mailbox;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004F18 File Offset: 0x00003118
		IXSOMailbox IStoreSession.Mailbox
		{
			get
			{
				return this.Mailbox;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009C RID: 156
		// (set) Token: 0x0600009D RID: 157
		public abstract ExTimeZone ExTimeZone { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009E RID: 158
		public abstract string GccResourceIdentifier { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004F20 File Offset: 0x00003120
		public virtual ContactFolders ContactFolders
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004F24 File Offset: 0x00003124
		public bool IsE15Session
		{
			get
			{
				bool flag = false;
				bool isE15Store;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					isE15Store = this.Mailbox.MapiStore.IsE15Store;
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExErrorInDetectE15Store, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession::IsE15Session.", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExErrorInDetectE15Store, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession::IsE15Session.", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
				return isE15Store;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005048 File Offset: 0x00003248
		internal CultureInfo InternalCulture
		{
			get
			{
				CultureInfo sessionCultureInfo;
				using (this.CheckDisposed("InternalCulture::get"))
				{
					sessionCultureInfo = this.SessionCultureInfo;
				}
				return sessionCultureInfo;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000508C File Offset: 0x0000328C
		public CultureInfo Culture
		{
			get
			{
				CultureInfo internalCulture;
				using (this.CheckDisposed("Culture::get"))
				{
					this.CheckCapabilities(this.Capabilities.CanHaveCulture, "CanHaveCulture");
					internalCulture = this.InternalCulture;
				}
				return internalCulture;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000050E4 File Offset: 0x000032E4
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00005128 File Offset: 0x00003328
		public IBudget AccountingObject
		{
			get
			{
				IBudget result;
				using (this.CheckDisposed("AccountingObject::get"))
				{
					result = this.budget;
				}
				return result;
			}
			set
			{
				using (this.CheckDisposed("AccountingObject::set"))
				{
					this.budget = value;
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005168 File Offset: 0x00003368
		public virtual CultureInfo PreferedCulture
		{
			get
			{
				CultureInfo internalPreferedCulture;
				using (this.CheckDisposed("PreferedCulture::get"))
				{
					this.CheckCapabilities(this.Capabilities.CanHaveCulture, "CanHaveCulture");
					internalPreferedCulture = this.InternalPreferedCulture;
				}
				return internalPreferedCulture;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000051C0 File Offset: 0x000033C0
		internal virtual CultureInfo InternalPreferedCulture
		{
			get
			{
				CultureInfo sessionCultureInfo;
				using (this.CheckDisposed("InternalPreferedCulture::get"))
				{
					sessionCultureInfo = this.SessionCultureInfo;
				}
				return sessionCultureInfo;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005204 File Offset: 0x00003404
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.GetType().FullName);
			stringBuilder.AppendLine();
			if (this.isDisposed)
			{
				stringBuilder.AppendLine("disposed");
			}
			else if (this.Mailbox != null)
			{
				using (this.CreateSessionGuard("ToString"))
				{
					string text = "";
					try
					{
						text = (this.Mailbox.TryGetProperty(MailboxSchema.UserName) as string);
					}
					catch (NotInBagPropertyErrorException)
					{
						text = null;
					}
					if (text != null)
					{
						stringBuilder.AppendFormat("User: {0}", text);
						stringBuilder.AppendLine();
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000A8 RID: 168
		public abstract StoreObjectId GetDefaultFolderId(DefaultFolderType defaultFolderType);

		// Token: 0x060000A9 RID: 169
		public abstract bool TryFixDefaultFolderId(DefaultFolderType defaultFolderType, out StoreObjectId id);

		// Token: 0x060000AA RID: 170 RVA: 0x000052C0 File Offset: 0x000034C0
		internal StoreObjectId SafeGetDefaultFolderId(DefaultFolderType defaultFolderType)
		{
			StoreObjectId result;
			using (this.CreateSessionGuard("SafeGetDefaultFolderId"))
			{
				EnumValidator.ThrowIfInvalid<DefaultFolderType>(defaultFolderType, "defaultFolderType");
				StoreObjectId defaultFolderId = this.GetDefaultFolderId(defaultFolderType);
				if (defaultFolderId == null)
				{
					throw new ObjectNotFoundException(ServerStrings.ExDefaultFolderNotFound(defaultFolderType));
				}
				result = defaultFolderId;
			}
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005324 File Offset: 0x00003524
		public void SetClientIPEndpoints(IPAddress clientIPAddress, IPAddress serverIPAddress)
		{
			using (this.CreateSessionGuard("SetClientIPEndpoints"))
			{
				this.clientIPAddress = clientIPAddress;
				this.serverIPAddress = serverIPAddress;
				this.InternalLogIpEndpoints(clientIPAddress, serverIPAddress);
				if (ExTraceGlobals.SessionTracer.IsTraceEnabled(TraceType.PfdTrace))
				{
					ExTraceGlobals.SessionTracer.TracePfd((long)this.GetHashCode(), "StoreSession::SetClientInfo. ClientIP={0}; ServerIP={1}; ClientMachine={2}; ClientProcess={3}; ClientVersion={4}", new object[]
					{
						this.clientIPAddress,
						this.serverIPAddress,
						this.clientMachineName,
						this.clientProcessName,
						this.clientVersion
					});
				}
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000053D0 File Offset: 0x000035D0
		public bool IsInBackoffState
		{
			get
			{
				bool result;
				using (this.CheckObjectState("IsInBackoffState::get"))
				{
					result = this.Mailbox.MapiStore.BackoffNow();
				}
				return result;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000541C File Offset: 0x0000361C
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00005460 File Offset: 0x00003660
		public StoreSession.IItemBinder ItemBinder
		{
			get
			{
				StoreSession.IItemBinder result;
				using (this.CheckDisposed("ItemBinder::get"))
				{
					result = this.itemBinder;
				}
				return result;
			}
			set
			{
				using (this.CheckDisposed("ItemBinder::set"))
				{
					this.itemBinder = value;
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000054A0 File Offset: 0x000036A0
		public IdConverter IdConverter
		{
			get
			{
				IdConverter result;
				using (this.CheckObjectState("IdConverter::get"))
				{
					result = this.idConverter;
				}
				return result;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000054E4 File Offset: 0x000036E4
		public ICollection<string> SupportedRoutingTypes
		{
			get
			{
				ICollection<string> result;
				using (this.CheckDisposed("SupportedRoutingTypes::get"))
				{
					if (this.supportedRoutingTypes == null)
					{
						this.supportedRoutingTypes = this.InternalGetSupportedRoutingTypes();
					}
					result = this.supportedRoutingTypes;
				}
				return result;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000553C File Offset: 0x0000373C
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00005580 File Offset: 0x00003780
		public SessionCapabilities Capabilities
		{
			get
			{
				SessionCapabilities result;
				using (this.CheckDisposed("Capabilities::get"))
				{
					result = this.sessionCapabilities;
				}
				return result;
			}
			internal set
			{
				using (this.CreateSessionGuard("Capabilities::set"))
				{
					this.sessionCapabilities = value;
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000055C0 File Offset: 0x000037C0
		public SpoolerManager SpoolerManager
		{
			get
			{
				SpoolerManager result;
				using (this.CheckObjectState("SpoolerManager::get"))
				{
					if (this.spoolerManager == null)
					{
						this.spoolerManager = new SpoolerManager(this);
					}
					result = this.spoolerManager;
				}
				return result;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005618 File Offset: 0x00003818
		public virtual bool CheckSubmissionQuota(int recipientCount)
		{
			bool result;
			using (this.CheckDisposed("CheckSubmissionQuota"))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005654 File Offset: 0x00003854
		public string UserLegacyDN
		{
			get
			{
				string result;
				using (this.CheckDisposed("UserLegacyDN::get"))
				{
					result = this.userLegacyDn;
				}
				return result;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005698 File Offset: 0x00003898
		public void AbortSubmit(StoreObjectId submittedId)
		{
			using (this.CheckDisposed("AbortSubmit"))
			{
				MapiStore mapiStore = this.Mailbox.MapiStore;
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					mapiStore.AbortSubmit(submittedId.ProviderLevelItemId);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSubmitMessage, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.AbortSubmit()", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSubmitMessage, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.AbortSubmit()", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000057EC File Offset: 0x000039EC
		public byte[] ReadPerUserInformation(byte[] longTermId, bool wantIfChanged, uint dataOffset, ushort maxDataSize, out bool hasFinished)
		{
			byte[] result;
			using (this.CheckObjectState("ReadPerUserInformation"))
			{
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					this.Mailbox.MapiStore.ReadPerUserInformation(longTermId, wantIfChanged, (int)dataOffset, (int)maxDataSize, out hasFinished, out result);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotReadPerUserInformation, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.ReadPerUserInformation", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotReadPerUserInformation, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.ReadPerUserInformation", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005944 File Offset: 0x00003B44
		public void WritePerUserInformation(byte[] longTermId, bool hasFinished, uint dataOffset, byte[] data, Guid? replicaGuid)
		{
			using (this.CheckObjectState("WritePerUserInformation"))
			{
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					this.Mailbox.MapiStore.WritePerUserInformation(longTermId, hasFinished, (int)dataOffset, data, (replicaGuid != null) ? replicaGuid.Value : Guid.Empty);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotWritePerUserInformation, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.WritePerUserInformation", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotWritePerUserInformation, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.WritePerUserInformation", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005AA8 File Offset: 0x00003CA8
		public uint GetEffectiveRights(byte[] addressBookId, StoreObjectId folderId)
		{
			uint effectiveRights;
			using (this.CheckObjectState("GetEffectiveRights"))
			{
				if (!AddressBookEntryId.IsAddressBookEntryId(addressBookId))
				{
					throw new ArgumentException("addressBookId");
				}
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					effectiveRights = this.Mailbox.MapiStore.GetEffectiveRights(addressBookId, folderId.ProviderLevelItemId);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetEffectiveRights, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.GetEffectiveRights", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetEffectiveRights, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.GetEffectiveRights", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return effectiveRights;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005C10 File Offset: 0x00003E10
		public void CheckForNotifications()
		{
			using (this.CheckObjectState("CheckForNotifications"))
			{
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					this.Mailbox.MapiStore.CheckForNotifications();
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCheckForNotifications, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.CheckForNotifications", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCheckForNotifications, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.CheckForNotifications", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005D58 File Offset: 0x00003F58
		public void ExecuteWithInternalAccessElevation(Action actionDelegate)
		{
			using (this.CheckObjectState("ExecuteWithInternalAccessElevation"))
			{
				StoreSession storeSession = null;
				bool flag = false;
				try
				{
					if (storeSession != null)
					{
						storeSession.BeginMapiCall();
						storeSession.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					this.Mailbox.MapiStore.ExecuteWithInternalAccess(actionDelegate);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotExecuteWithInternalAccess, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.ExecuteWithInternalAccessElevation", new object[0]),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotExecuteWithInternalAccess, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession.ExecuteWithInternalAccessElevation", new object[0]),
						ex2
					});
				}
				finally
				{
					try
					{
						if (storeSession != null)
						{
							storeSession.EndMapiCall();
							if (flag)
							{
								storeSession.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
		}

		// Token: 0x060000BC RID: 188
		public abstract ADSessionSettings GetADSessionSettings();

		// Token: 0x060000BD RID: 189 RVA: 0x00005EA4 File Offset: 0x000040A4
		protected void StartDeadSessionChecking()
		{
			this.isDead = false;
			bool flag = false;
			try
			{
				if (this != null)
				{
					this.BeginMapiCall();
					this.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.tickleNotificationHandle = this.mailboxStoreObject.Mailbox.MapiStore.Advise(null, AdviseFlags.Extended, new MapiNotificationHandler(this.OnTickle), NotificationCallbackMode.Async, (MapiNotificationClientFlags)0);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCannotStartDeadSessionChecking, ex, this, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("StoreSession::StartDeadSessionChecking.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCannotStartDeadSessionChecking, ex2, this, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("StoreSession::StartDeadSessionChecking.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (this != null)
					{
						this.EndMapiCall();
						if (flag)
						{
							this.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			bool flag2 = false;
			try
			{
				if (this != null)
				{
					this.BeginMapiCall();
					this.BeginServerHealthCall();
					flag2 = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.droppedNotificationHandle = this.mailboxStoreObject.Mailbox.MapiStore.Advise(null, AdviseFlags.ConnectionDropped, new MapiNotificationHandler(this.OnDroppedNotify), NotificationCallbackMode.Async, (MapiNotificationClientFlags)0);
			}
			catch (MapiPermanentException ex3)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCannotStartDeadSessionChecking, ex3, this, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("StoreSession::StartDeadSessionChecking.", new object[0]),
					ex3
				});
			}
			catch (MapiRetryableException ex4)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExCannotStartDeadSessionChecking, ex4, this, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("StoreSession::StartDeadSessionChecking.", new object[0]),
					ex4
				});
			}
			finally
			{
				try
				{
					if (this != null)
					{
						this.EndMapiCall();
						if (flag2)
						{
							this.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006128 File Offset: 0x00004328
		protected bool StopDeadSessionChecking()
		{
			bool result = false;
			try
			{
				if (!this.isDead && this.mailboxStoreObject != null && this.mailboxStoreObject.Mailbox != null && this.mailboxStoreObject.Mailbox.MapiStore != null && !this.mailboxStoreObject.Mailbox.MapiStore.IsDead)
				{
					if (this.droppedNotificationHandle != null)
					{
						this.mailboxStoreObject.Mailbox.MapiStore.Unadvise(this.droppedNotificationHandle);
						result = true;
					}
					if (this.tickleNotificationHandle != null)
					{
						this.mailboxStoreObject.Mailbox.MapiStore.Unadvise(this.tickleNotificationHandle);
						result = true;
					}
				}
				this.droppedNotificationHandle = null;
				this.tickleNotificationHandle = null;
			}
			catch (MapiPermanentException arg)
			{
				ExTraceGlobals.SessionTracer.Information<MapiPermanentException>((long)this.GetHashCode(), "StoreSession::StopDeadSessionChecking, MapiPermanentException {0} calling Unadvise, ignored.", arg);
			}
			catch (MapiRetryableException arg2)
			{
				ExTraceGlobals.SessionTracer.Information<MapiRetryableException>((long)this.GetHashCode(), "StoreSession::StopDeadSessionChecking, mapiRetryableException {0} calling Unadvise, ignored.", arg2);
			}
			return result;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BF RID: 191
		public abstract Guid MdbGuid { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C0 RID: 192
		public abstract Guid MailboxGuid { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C1 RID: 193
		public abstract string ServerFullyQualifiedDomainName { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C2 RID: 194
		public abstract string DisplayAddress { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C3 RID: 195
		public abstract OrganizationId OrganizationId { get; }

		// Token: 0x060000C4 RID: 196 RVA: 0x00006230 File Offset: 0x00004430
		protected void ExecuteOperationOnObjects(Dictionary<StoreObjectId, List<StoreId>> groupByFolder, List<GroupOperationResult> groupOperationResultList, StoreSession.ActOnObjectsDelegate actOnObjectsDelegate)
		{
			foreach (KeyValuePair<StoreObjectId, List<StoreId>> keyValuePair in groupByFolder)
			{
				try
				{
					using (Folder folder = Folder.Bind(this, keyValuePair.Key))
					{
						AggregateOperationResult aggregateOperationResult = actOnObjectsDelegate(folder, keyValuePair.Value.ToArray());
						groupOperationResultList.AddRange(aggregateOperationResult.GroupOperationResults);
					}
				}
				catch (ObjectNotFoundException storageException)
				{
					ExTraceGlobals.SessionTracer.TraceError<StoreObjectId>((long)this.GetHashCode(), "StoreSession::CopyItemEx, folderNotFound = {0}", keyValuePair.Key);
					StoreObjectId[] objectIds = Folder.StoreIdsToStoreObjectIds(keyValuePair.Value.ToArray());
					GroupOperationResult item = new GroupOperationResult(OperationResult.Failed, objectIds, storageException);
					groupOperationResultList.Add(item);
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006318 File Offset: 0x00004518
		protected void GroupNonOccurrenceByFolder(IList<StoreId> objectIds, Dictionary<StoreObjectId, List<StoreId>> groupedObjects, List<GroupOperationResult> errors)
		{
			for (int i = 0; i < objectIds.Count; i++)
			{
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId(objectIds[i]);
				this.GroupNonOccurrenceByFolder(storeObjectId, groupedObjects, errors);
			}
		}

		// Token: 0x060000C6 RID: 198
		protected abstract MapiStore ForceOpen(MapiStore linkedStore);

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000634C File Offset: 0x0000454C
		internal bool IsDisposed
		{
			get
			{
				bool result;
				using (this.CreateSessionGuard("IsDisposed"))
				{
					result = this.isDisposed;
				}
				return result;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00006390 File Offset: 0x00004590
		internal Schema Schema
		{
			get
			{
				Schema result;
				using (this.CheckObjectState("Schema::get"))
				{
					result = this.schema;
				}
				return result;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000063D4 File Offset: 0x000045D4
		public string ClientInfoString
		{
			get
			{
				string result;
				using (this.CreateSessionGuard("ClientInfoString::get"))
				{
					result = this.clientInfoString;
				}
				return result;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00006418 File Offset: 0x00004618
		public IPAddress ClientIPAddress
		{
			get
			{
				IPAddress result;
				using (this.CreateSessionGuard("ClientIPAddress::get"))
				{
					result = this.clientIPAddress;
				}
				return result;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000645C File Offset: 0x0000465C
		public IPAddress ServerIPAddress
		{
			get
			{
				IPAddress result;
				using (this.CreateSessionGuard("ServerIPAddress::get"))
				{
					result = this.serverIPAddress;
				}
				return result;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000064A0 File Offset: 0x000046A0
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000064E4 File Offset: 0x000046E4
		public long ClientVersion
		{
			get
			{
				long result;
				using (this.CreateSessionGuard("ClientVersion::get"))
				{
					result = this.clientVersion;
				}
				return result;
			}
			set
			{
				using (this.CreateSessionGuard("ClientVersion::set"))
				{
					this.clientVersion = value;
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00006524 File Offset: 0x00004724
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00006568 File Offset: 0x00004768
		public string ClientProcessName
		{
			get
			{
				string result;
				using (this.CreateSessionGuard("ClientProcessName::get"))
				{
					result = this.clientProcessName;
				}
				return result;
			}
			set
			{
				using (this.CreateSessionGuard("ClientProcessName::set"))
				{
					this.clientProcessName = value;
				}
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000065A8 File Offset: 0x000047A8
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000065EC File Offset: 0x000047EC
		public string ClientMachineName
		{
			get
			{
				string result;
				using (this.CreateSessionGuard("ClientMachineName::get"))
				{
					result = this.clientMachineName;
				}
				return result;
			}
			set
			{
				using (this.CreateSessionGuard("ClientMachineName::set"))
				{
					this.clientMachineName = value;
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000662C File Offset: 0x0000482C
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00006670 File Offset: 0x00004870
		public LogonType LogonType
		{
			get
			{
				LogonType result;
				using (this.CheckDisposed("LogonType::get"))
				{
					result = this.logonType;
				}
				return result;
			}
			internal set
			{
				using (this.CheckDisposed("LogonType::set"))
				{
					this.logonType = value;
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000066B0 File Offset: 0x000048B0
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000066F4 File Offset: 0x000048F4
		public string MappingSignature
		{
			get
			{
				string result;
				using (this.CheckDisposed("MappingSignature::get"))
				{
					result = this.mappingSignature;
				}
				return result;
			}
			internal set
			{
				using (this.CheckDisposed("MappingSignature::set"))
				{
					this.mappingSignature = value;
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00006734 File Offset: 0x00004934
		internal NamedPropMap NamedPropertyResolutionCache
		{
			get
			{
				NamedPropMap result;
				using (this.CheckDisposed("NamedPropertyResolutionCache::get"))
				{
					if (this.propResolutionCache == null || !this.propResolutionCache.IsAlive)
					{
						NamedPropMap namedPropMap = NamedPropMapCache.Default.GetMapping(this.mappingSignature);
						this.propResolutionCache = new WeakReference(namedPropMap);
						result = namedPropMap;
					}
					else
					{
						NamedPropMap namedPropMap = this.propResolutionCache.Target as NamedPropMap;
						if (namedPropMap == null)
						{
							this.propResolutionCache = null;
							result = this.NamedPropertyResolutionCache;
						}
						else
						{
							result = namedPropMap;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000067CC File Offset: 0x000049CC
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00006810 File Offset: 0x00004A10
		public bool IsConnected
		{
			get
			{
				bool result;
				using (this.CheckDisposed("IsConnected::get"))
				{
					result = this.isConnected;
				}
				return result;
			}
			protected set
			{
				this.isConnected = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000681C File Offset: 0x00004A1C
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00006860 File Offset: 0x00004A60
		public bool IsDead
		{
			get
			{
				bool isConnectionDead;
				using (this.CheckDisposed("IsDead::get"))
				{
					isConnectionDead = this.IsConnectionDead;
				}
				return isConnectionDead;
			}
			protected set
			{
				using (this.CheckDisposed("IsDead::set"))
				{
					this.isDead = value;
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000068A0 File Offset: 0x00004AA0
		internal void PlayDead()
		{
			using (this.CreateSessionGuard("PlayDead"))
			{
				this.IsDead = true;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000068E0 File Offset: 0x00004AE0
		public object Identity
		{
			get
			{
				object result;
				using (this.CreateSessionGuard("Identity::get"))
				{
					result = this.identity;
				}
				return result;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00006924 File Offset: 0x00004B24
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00006968 File Offset: 0x00004B68
		internal OpenStoreFlag StoreFlag
		{
			get
			{
				OpenStoreFlag result;
				using (this.CreateSessionGuard("StoreFlag::get"))
				{
					result = this.storeFlag;
				}
				return result;
			}
			set
			{
				using (this.CreateSessionGuard("StoreFlag::set"))
				{
					this.storeFlag = value;
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000069A8 File Offset: 0x00004BA8
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x000069EC File Offset: 0x00004BEC
		public int PreferredInternetCodePageForShiftJis
		{
			get
			{
				int result;
				using (this.CreateSessionGuard("PreferredInternetCodePageForShiftJis::get"))
				{
					result = this.preferredInternetCodePageForShiftJis;
				}
				return result;
			}
			protected set
			{
				using (this.CreateSessionGuard("PreferredInternetCodePageForShiftJis::set"))
				{
					this.preferredInternetCodePageForShiftJis = value;
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00006A2C File Offset: 0x00004C2C
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00006A70 File Offset: 0x00004C70
		public int RequiredCoverage
		{
			get
			{
				int result;
				using (this.CreateSessionGuard("RequiredCoverage::get"))
				{
					result = this.requiredCoverage;
				}
				return result;
			}
			protected set
			{
				using (this.CreateSessionGuard("RequiredCoverage::set"))
				{
					this.requiredCoverage = value;
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00006AB0 File Offset: 0x00004CB0
		internal GenericIdentity AuxiliaryIdentity
		{
			get
			{
				GenericIdentity result;
				using (this.CreateSessionGuard("AuxiliaryIdentity::get"))
				{
					result = this.auxiliaryIdentity;
				}
				return result;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006AF4 File Offset: 0x00004CF4
		internal virtual void CheckDeleteItemFlags(DeleteItemFlags flags)
		{
			using (this.CreateSessionGuard("CheckDeleteItemFlags"))
			{
				EnumValidator.ThrowIfInvalid<DeleteItemFlags>(flags, "flags");
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006B38 File Offset: 0x00004D38
		internal virtual void CheckSystemFolderAccess(StoreObjectId id)
		{
			using (this.CreateSessionGuard("CheckSystemFolderAccess"))
			{
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006B74 File Offset: 0x00004D74
		internal GroupOperationResult DeleteCalendarOccurrence(DeleteItemFlags flags, OccurrenceStoreObjectId itemId)
		{
			GroupOperationResult result;
			using (this.CreateSessionGuard("DeleteCalendarOccurrence"))
			{
				ExTraceGlobals.SessionTracer.Information<DeleteItemFlags, OccurrenceStoreObjectId>((long)this.GetHashCode(), "StoreSession::DeleteCalendarOccurrence. flags={0}; itemId = {1}", flags, itemId);
				using (CalendarItem calendarItem = CalendarItem.Bind(this, itemId.GetMasterStoreObjectId()))
				{
					calendarItem.OpenAsReadWrite();
					try
					{
						calendarItem.DeleteOccurrence(itemId, flags);
					}
					catch (LastOccurrenceDeletionException)
					{
						ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "StoreSession::DeleteCalendarOccurrence, Delete the master because the recurrence is Empty.");
						using (Folder folder = Folder.Bind(this, calendarItem.ParentId))
						{
							AggregateOperationResult aggregateOperationResult = folder.DeleteObjects(flags, new StoreId[]
							{
								calendarItem.Id.ObjectId
							});
							if (aggregateOperationResult.GroupOperationResults.Length > 0)
							{
								return aggregateOperationResult.GroupOperationResults[0];
							}
							return new GroupOperationResult(OperationResult.Succeeded, new StoreObjectId[]
							{
								itemId
							}, null);
						}
					}
					catch (RecurrenceException storageException)
					{
						return new GroupOperationResult(OperationResult.Failed, new StoreObjectId[]
						{
							itemId
						}, storageException);
					}
					ConflictResolutionResult conflictResolutionResult = calendarItem.Save(SaveMode.ResolveConflicts);
					if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
					{
						ExTraceGlobals.StorageTracer.TraceError<OccurrenceStoreObjectId, SaveResult>((long)this.GetHashCode(), "StoreSession::DeleteCalendarOccurrence, Cannot save the master. ItemId = {0}, conflictResolutionResult = {1}", itemId, conflictResolutionResult.SaveStatus);
						result = new GroupOperationResult(OperationResult.Failed, new StoreObjectId[]
						{
							itemId
						}, new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(null), conflictResolutionResult));
					}
					else
					{
						result = new GroupOperationResult(OperationResult.Succeeded, new StoreObjectId[]
						{
							itemId
						}, null);
					}
				}
			}
			return result;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006D74 File Offset: 0x00004F74
		internal MapiProp GetMapiProp(StoreObjectId id)
		{
			MapiProp mapiProp;
			using (this.CreateSessionGuard("GetMapiProp"))
			{
				mapiProp = this.GetMapiProp(id, OpenEntryFlags.BestAccess | OpenEntryFlags.DeferredErrors);
			}
			return mapiProp;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006DB8 File Offset: 0x00004FB8
		internal virtual MapiProp GetMapiProp(StoreObjectId id, OpenEntryFlags flags)
		{
			MapiProp result;
			using (this.CheckObjectState("GetMapiProp"))
			{
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					result = (MapiProp)this.Mailbox.MapiStore.OpenEntry(id.ProviderLevelItemId, flags);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExInvalidItemId, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession::GetMapiProp. id = {0}.", id),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.ExInvalidItemId, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession::GetMapiProp. id = {0}.", id),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006F08 File Offset: 0x00005108
		protected void SetMailboxStoreObject(MailboxStoreObject mailboxStoreObject)
		{
			using (this.CreateSessionGuard("SetMailboxStoreObject"))
			{
				if (this.mailboxStoreObject != mailboxStoreObject)
				{
					if (this.mailboxStoreObject != null)
					{
						if (this.droppedNotificationHandle != null)
						{
							return;
						}
						ExTraceGlobals.SessionTracer.TraceDebug<MailboxStoreObject, MailboxStoreObject>((long)this.GetHashCode(), "StoreSession SetMailboxStoreObject. We are disposing existing mailboxObject and set the new one. Existing = {0}, New = {1}.", this.mailboxStoreObject, mailboxStoreObject);
						this.mailboxStoreObject.Dispose();
						this.mailboxStoreObject = null;
					}
					this.mailboxStoreObject = mailboxStoreObject;
				}
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006F94 File Offset: 0x00005194
		public StoreObjectId GetParentFolderId(StoreObjectId objectId)
		{
			StoreObjectId result;
			using (this.CreateSessionGuard("GetParentFolderId"))
			{
				byte[] entryId = null;
				bool flag = false;
				try
				{
					if (this != null)
					{
						this.BeginMapiCall();
						this.BeginServerHealthCall();
						flag = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					entryId = this.Mailbox.MapiStore.GetParentEntryId(objectId.ProviderLevelItemId);
				}
				catch (MapiPermanentException ex)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetParentId, ex, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession::GetParentFolderId. Object id = {0}.", objectId),
						ex
					});
				}
				catch (MapiRetryableException ex2)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetParentId, ex2, this, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("StoreSession::GetParentFolderId. Object id = {0}.", objectId),
						ex2
					});
				}
				finally
				{
					try
					{
						if (this != null)
						{
							this.EndMapiCall();
							if (flag)
							{
								this.EndServerHealthCall();
							}
						}
					}
					finally
					{
						if (StorageGlobals.MapiTestHookAfterCall != null)
						{
							StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
						}
					}
				}
				result = StoreObjectId.FromProviderSpecificId(entryId);
			}
			return result;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000070E8 File Offset: 0x000052E8
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000070EF File Offset: 0x000052EF
		internal static bool TestRequestExchangeRpcServer
		{
			get
			{
				return StoreSession.testRequestExchangeRpcServer;
			}
			set
			{
				StoreSession.testRequestExchangeRpcServer = value;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000070F8 File Offset: 0x000052F8
		internal void TestSetLogCallback(ILogChanges callback)
		{
			using (this.CreateSessionGuard("TestSetLogCallback"))
			{
				this.testLogCallback = callback;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007138 File Offset: 0x00005338
		internal virtual bool OnBeforeItemChange(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, CallbackContext callbackContext)
		{
			bool result;
			using (this.CreateSessionGuard("OnBeforeItemChange"))
			{
				if (this.contentIndexingSession != null)
				{
					this.contentIndexingSession.OnBeforeItemChange(operation, item);
				}
				if (this.testLogCallback != null)
				{
					result = this.testLogCallback.OnBeforeItemChange(operation, session, itemId, item);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000071A8 File Offset: 0x000053A8
		internal virtual void OnAfterItemChange(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, ConflictResolutionResult result, CallbackContext callbackContext)
		{
			using (this.CreateSessionGuard("OnAfterItemChange"))
			{
				if (this.testLogCallback != null)
				{
					this.testLogCallback.OnAfterItemChange(operation, session, itemId, item, result);
				}
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000071FC File Offset: 0x000053FC
		internal virtual bool OnBeforeItemSave(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, CallbackContext callbackContext)
		{
			bool result;
			using (this.CreateSessionGuard("OnBeforeItemSave"))
			{
				if (this.testLogCallback != null)
				{
					result = this.testLogCallback.OnBeforeItemSave(operation, session, itemId, item);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007254 File Offset: 0x00005454
		internal virtual void OnAfterItemSave(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, ConflictResolutionResult result, CallbackContext callbackContext)
		{
			using (this.CreateSessionGuard("OnAfterItemSave"))
			{
				if (this.testLogCallback != null)
				{
					this.testLogCallback.OnAfterItemSave(operation, session, itemId, item, result);
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000072A8 File Offset: 0x000054A8
		internal virtual bool OnBeforeFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, CallbackContext callbackContext)
		{
			bool result;
			using (this.CreateSessionGuard("OnBeforeFolderChange"))
			{
				if (this.testLogCallback != null)
				{
					result = this.testLogCallback.OnBeforeFolderChange(operation, flags, sourceSession, destinationSession, sourceFolderId, destinationFolderId, itemIds);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007308 File Offset: 0x00005508
		internal virtual void OnAfterFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, GroupOperationResult result, CallbackContext callbackContext)
		{
			using (this.CreateSessionGuard("OnAfterFolderChange"))
			{
				if (this.testLogCallback != null)
				{
					this.testLogCallback.OnAfterFolderChange(operation, flags, sourceSession, destinationSession, sourceFolderId, destinationFolderId, itemIds, result);
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007364 File Offset: 0x00005564
		internal virtual void OnBeforeFolderBind(StoreObjectId folderId, CallbackContext callbackContext)
		{
			using (this.CreateSessionGuard("OnBeforeFolderBind"))
			{
				if (this.testLogCallback != null)
				{
					this.testLogCallback.OnBeforeFolderBind(this, folderId);
				}
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000073B4 File Offset: 0x000055B4
		internal virtual void OnAfterFolderBind(StoreObjectId folderId, CoreFolder folder, bool success, CallbackContext callbackContext)
		{
			using (this.CreateSessionGuard("OnAfterFolderBind"))
			{
				if (this.testLogCallback != null)
				{
					this.testLogCallback.OnAfterFolderBind(this, folderId, folder, success);
				}
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007404 File Offset: 0x00005604
		internal virtual GroupOperationResult GetCallbackResults()
		{
			GroupOperationResult result;
			using (this.CreateSessionGuard("GetCallbackResults"))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00007440 File Offset: 0x00005640
		internal SubscriptionsManager SubscriptionsManager
		{
			get
			{
				SubscriptionsManager result;
				using (this.CreateSessionGuard("SubscriptionsManager::get"))
				{
					result = this.subscriptionsManager;
				}
				return result;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007484 File Offset: 0x00005684
		internal virtual void ValidateOperation(FolderChangeOperation folderOperation, StoreObjectId folderId)
		{
			using (this.CheckObjectState("ValidateOperation"))
			{
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000074C0 File Offset: 0x000056C0
		internal virtual bool IsValidOperation(ICoreObject coreObject, PropertyDefinition property, out PropertyErrorCode? error)
		{
			bool result;
			using (this.CheckDisposed("IsValidOperation"))
			{
				error = null;
				result = true;
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007504 File Offset: 0x00005704
		private void InternalMarkReadFlagsByGroup(bool suppressReadReceipts, bool isMarkAsRead, StoreId[] itemIds, bool throwIfWarning, bool suppressNotReadReceipts = false)
		{
			Dictionary<StoreObjectId, List<StoreId>> dictionary = new Dictionary<StoreObjectId, List<StoreId>>();
			List<GroupOperationResult> errors = new List<GroupOperationResult>();
			List<StoreId> list = new List<StoreId>();
			List<OccurrenceStoreObjectId> sourceOccurrenceIdList = new List<OccurrenceStoreObjectId>();
			Folder.GroupOccurrencesAndObjectIds(itemIds, list, sourceOccurrenceIdList);
			this.GroupNonOccurrenceByFolder(list, dictionary, errors);
			foreach (KeyValuePair<StoreObjectId, List<StoreId>> keyValuePair in dictionary)
			{
				using (Folder folder = Folder.Bind(this, keyValuePair.Key))
				{
					folder.InternalSetReadFlags(suppressReadReceipts, isMarkAsRead, keyValuePair.Value.ToArray(), throwIfWarning, suppressNotReadReceipts);
				}
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000075B8 File Offset: 0x000057B8
		private void GroupNonOccurrenceByFolder(StoreId itemId, Dictionary<StoreObjectId, List<StoreId>> groupedObjects, List<GroupOperationResult> errors)
		{
			if (itemId == null)
			{
				ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), "StoreSession::GroupItemsByFolderHelper. The in itemId cannot be null.");
				throw new ArgumentException(ServerStrings.ExNullItemIdParameter(0));
			}
			ExTraceGlobals.SessionTracer.Information<StoreId>((long)this.GetHashCode(), "Folder::GroupItemsByFolder - ItemId:{0}", itemId);
			StoreObjectId storeObjectId = null;
			StoreObjectId storeObjectId2 = StoreId.GetStoreObjectId(itemId);
			if (storeObjectId2 is OccurrenceStoreObjectId)
			{
				throw new ArgumentException(ServerStrings.ExCannotMoveOrCopyOccurrenceItem(itemId));
			}
			try
			{
				storeObjectId = this.GetParentFolderId(storeObjectId2);
			}
			catch (ObjectNotFoundException ex)
			{
				ExTraceGlobals.StorageTracer.TraceError<StoreObjectId, ObjectNotFoundException>((long)this.GetHashCode(), "StoreSession::GroupNonOccurrenceByFolder, Cannot find the object's parent folder. id = {0}, Exception = {1}", storeObjectId2, ex);
				GroupOperationResult item = new GroupOperationResult(OperationResult.Failed, new StoreObjectId[]
				{
					storeObjectId2
				}, ex);
				errors.Add(item);
				return;
			}
			if (!groupedObjects.ContainsKey(StoreId.GetStoreObjectId(storeObjectId)))
			{
				groupedObjects[storeObjectId] = new List<StoreId>();
			}
			groupedObjects[storeObjectId].Add(itemId);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000076A4 File Offset: 0x000058A4
		private void OnDroppedNotify(MapiNotification notification)
		{
			MapiConnectionDroppedNotification mapiConnectionDroppedNotification = notification as MapiConnectionDroppedNotification;
			if (mapiConnectionDroppedNotification != null)
			{
				ExTraceGlobals.SessionTracer.Information<string, string, int>((long)this.GetHashCode(), "StoreSession::OnDroppedNotify. serverDn = {0}, userDn = {1}, tickDeath {2}.", mapiConnectionDroppedNotification.ServerDN, mapiConnectionDroppedNotification.UserDN, mapiConnectionDroppedNotification.TickDeath);
				this.isDead = true;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000076EA File Offset: 0x000058EA
		private void OnTickle(MapiNotification notification)
		{
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000076EC File Offset: 0x000058EC
		private bool IsConnectionDead
		{
			get
			{
				if (this.isDead)
				{
					return true;
				}
				if (this.mailboxStoreObject != null && this.mailboxStoreObject.Mailbox != null && this.mailboxStoreObject.Mailbox.MapiStore != null)
				{
					try
					{
						if (this.mailboxStoreObject.Mailbox.MapiStore.IsDead)
						{
							this.isDead = true;
						}
					}
					catch (MapiPermanentException arg)
					{
						ExTraceGlobals.SessionTracer.Information<MapiPermanentException>((long)this.GetHashCode(), "StoreSession::IsConnectionDead, MapiPermanentException calling IsDead, ignored. {0}", arg);
					}
					catch (MapiRetryableException arg2)
					{
						ExTraceGlobals.SessionTracer.Information<MapiRetryableException>((long)this.GetHashCode(), "StoreSession::IsConnectionDead, MapiRetryableException calling IsDead, ignored. {0}", arg2);
					}
					return this.isDead;
				}
				return true;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000077A4 File Offset: 0x000059A4
		public static CultureInfo CreateMapiCultureInfo(int stringLCID, int sortLCID, int codePage)
		{
			return MapiCultureInfo.CreateCultureInfo(stringLCID, sortLCID, codePage);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000077AE File Offset: 0x000059AE
		public static void AbandonNotificationsDuringShutdown(bool abandon)
		{
			MapiNotification.AbandonNotificationsDuringShutdown(abandon);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000077B8 File Offset: 0x000059B8
		private void AccumulatePerRPCStatistics(PerRPCPerformanceStatistics newStats)
		{
			uint validVersion = this.cumulativeRPCStats.validVersion;
			this.cumulativeRPCStats.timeInServer = this.cumulativeRPCStats.timeInServer + newStats.timeInServer;
			this.cumulativeRPCStats.timeInCPU = this.cumulativeRPCStats.timeInCPU + newStats.timeInCPU;
			this.cumulativeRPCStats.pagesRead = this.cumulativeRPCStats.pagesRead + newStats.pagesRead;
			this.cumulativeRPCStats.pagesPreread = this.cumulativeRPCStats.pagesPreread + newStats.pagesPreread;
			this.cumulativeRPCStats.logRecords = this.cumulativeRPCStats.logRecords + newStats.logRecords;
			this.cumulativeRPCStats.logBytes = this.cumulativeRPCStats.logBytes + newStats.logBytes;
			this.cumulativeRPCStats.ldapReads = this.cumulativeRPCStats.ldapReads + newStats.ldapReads;
			this.cumulativeRPCStats.ldapSearches = this.cumulativeRPCStats.ldapSearches + newStats.ldapSearches;
			this.cumulativeRPCStats.avgDbLatency = newStats.avgDbLatency;
			this.cumulativeRPCStats.avgServerLatency = newStats.avgServerLatency;
			this.cumulativeRPCStats.totalDbOperations = newStats.totalDbOperations;
			this.cumulativeRPCStats.currentThreads = newStats.currentThreads;
			this.cumulativeRPCStats.currentDbThreads = newStats.currentDbThreads;
			this.cumulativeRPCStats.currentSCTThreads = newStats.currentSCTThreads;
			this.cumulativeRPCStats.currentSCTSessions = newStats.currentSCTSessions;
			this.cumulativeRPCStats.dataProtectionHealth = newStats.dataProtectionHealth;
			this.cumulativeRPCStats.dataAvailabilityHealth = newStats.dataAvailabilityHealth;
			this.cumulativeRPCStats.currentCpuUsage = newStats.currentCpuUsage;
			this.cumulativeRPCStats.validVersion = newStats.validVersion;
			this.cumulativeRPCStats.processID = newStats.processID;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000797C File Offset: 0x00005B7C
		public CumulativeRPCPerformanceStatistics GetStoreCumulativeRPCStats()
		{
			CumulativeRPCPerformanceStatistics result;
			using (this.CreateSessionGuard("GetStoreCumulativeRPCStats"))
			{
				result = this.cumulativeRPCStats;
			}
			return result;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000079C0 File Offset: 0x00005BC0
		public static CumulativeRPCPerformanceStatistics SubtractRPCPerformanceStatistics(CumulativeRPCPerformanceStatistics later, CumulativeRPCPerformanceStatistics earlier)
		{
			CumulativeRPCPerformanceStatistics result;
			result.timeInServer = later.timeInServer - earlier.timeInServer;
			result.timeInCPU = later.timeInCPU - earlier.timeInCPU;
			result.pagesRead = later.pagesRead - earlier.pagesRead;
			result.pagesPreread = later.pagesPreread - earlier.pagesPreread;
			result.logRecords = later.logRecords - earlier.logRecords;
			result.logBytes = later.logBytes - earlier.logBytes;
			result.ldapReads = later.ldapReads - earlier.ldapReads;
			result.ldapSearches = later.ldapSearches - earlier.ldapSearches;
			result.totalDbOperations = later.totalDbOperations - earlier.totalDbOperations;
			result.avgDbLatency = later.avgDbLatency;
			result.avgServerLatency = later.avgServerLatency;
			result.currentThreads = later.currentThreads;
			result.currentDbThreads = later.currentDbThreads;
			result.currentSCTThreads = later.currentSCTThreads;
			result.currentSCTSessions = later.currentSCTSessions;
			result.dataProtectionHealth = later.dataProtectionHealth;
			result.dataAvailabilityHealth = later.dataAvailabilityHealth;
			result.currentCpuUsage = later.currentCpuUsage;
			result.validVersion = later.validVersion;
			result.processID = later.processID;
			return result;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00007B36 File Offset: 0x00005D36
		protected static IDatabaseLocationProvider DatabaseLocationProvider
		{
			get
			{
				if (StoreSession.databaseLocationProvider == null)
				{
					StoreSession.databaseLocationProvider = new DatabaseLocationProvider();
				}
				return StoreSession.databaseLocationProvider;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00007B4E File Offset: 0x00005D4E
		protected RemoteMailboxProperties RemoteMailboxProperties
		{
			get
			{
				if (this.remoteMailboxProperties == null)
				{
					this.remoteMailboxProperties = new RemoteMailboxProperties(this, StoreSession.directoryAccessor);
				}
				return this.remoteMailboxProperties;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007B6F File Offset: 0x00005D6F
		public static void SetDatabaseLocationProviderForTest(IDatabaseLocationProvider databaseLocationProvider)
		{
			StoreSession.databaseLocationProvider = databaseLocationProvider;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007B78 File Offset: 0x00005D78
		public static ClientIdentityInfo FromAuthZContext(ADSessionSettings adSettings, AuthzContextHandle authenticatedUserHandle)
		{
			List<string> list;
			StoreSession.GetSidFromClientContext(authenticatedUserHandle, AuthzContextInformation.UserSid, out list);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(list[0]);
			StoreSession.GetSidFromClientContext(authenticatedUserHandle, AuthzContextInformation.GroupSids, out list);
			int? num = null;
			try
			{
				num = StoreSession.directoryAccessor.GetPrimaryGroupId(adSettings.CreateRecipientSession(null), securityIdentifier);
			}
			catch (ObjectNotFoundException arg)
			{
				ExTraceGlobals.StorageTracer.TraceError<ObjectNotFoundException>(0L, "FromAuthZContext. More than one recipients with same sid was found. Exception = {0}.", arg);
			}
			SecurityIdentifier _sidGroup;
			if (num == null)
			{
				_sidGroup = StoreSession.PickAnyGroup(list);
			}
			else
			{
				_sidGroup = StoreSession.PickOneApproxGroup(securityIdentifier, list, num.Value);
			}
			return new ClientIdentityInfo(authenticatedUserHandle.DangerousGetHandle(), securityIdentifier, _sidGroup);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00007C1C File Offset: 0x00005E1C
		private static SecurityIdentifier PickOneApproxGroup(SecurityIdentifier sidUser, List<string> groupSidStrings, int primaryGroupId)
		{
			if (groupSidStrings == null || groupSidStrings.Count == 0)
			{
				return null;
			}
			string arg = sidUser.Value.Substring(0, sidUser.Value.LastIndexOf('-'));
			int index = 0;
			for (int i = 0; i < groupSidStrings.Count; i++)
			{
				if (string.Compare(arg + "-" + primaryGroupId, groupSidStrings[i]) == 0)
				{
					return new SecurityIdentifier(groupSidStrings[i]);
				}
				if (groupSidStrings[i].EndsWith("-" + primaryGroupId))
				{
					index = i;
				}
			}
			return new SecurityIdentifier(groupSidStrings[index]);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00007CBB File Offset: 0x00005EBB
		private static SecurityIdentifier PickAnyGroup(List<string> sidStrings)
		{
			if (sidStrings == null || sidStrings.Count == 0)
			{
				return null;
			}
			return new SecurityIdentifier(sidStrings[0]);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007CD8 File Offset: 0x00005ED8
		private static void GetSidFromClientContext(AuthzContextHandle contextHandle, AuthzContextInformation contextClass, out List<string> sidStrings)
		{
			sidStrings = null;
			switch (contextClass)
			{
			case AuthzContextInformation.UserSid:
			{
				NativeMethods.SecurityIdentifierAndAttributes securityIdentifierAndAttributes = NativeMethods.AuthzGetInformationFromContextTokenUser(contextHandle);
				sidStrings = new List<string>(1);
				sidStrings.Add(securityIdentifierAndAttributes.sid.ToString());
				return;
			}
			case AuthzContextInformation.GroupSids:
			{
				NativeMethods.SecurityIdentifierAndAttributes[] array = NativeMethods.AuthzGetInformationFromContextTokenGroup(contextHandle);
				if (array != null)
				{
					sidStrings = new List<string>(array.Length);
					foreach (NativeMethods.SecurityIdentifierAndAttributes securityIdentifierAndAttributes2 in array)
					{
						sidStrings.Add(securityIdentifierAndAttributes2.sid.ToString());
					}
					return;
				}
				return;
			}
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007DE0 File Offset: 0x00005FE0
		private void InitializeADSessionFactory()
		{
			this.adRecipientSessionFactory = ((bool isReadonly, ConsistencyMode consistencyMode) => DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, this.InternalPreferedCulture.LCID, isReadonly, consistencyMode, null, this.GetADSessionSettings(), 3509, "InitializeADSessionFactory", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\obj\\amd64\\StoreSession.cs"));
			this.adConfigurationSessionFactory = ((bool isReadonly, ConsistencyMode consistencyMode) => DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, isReadonly, consistencyMode, null, this.GetADSessionSettings(), 3519, "InitializeADSessionFactory", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\obj\\amd64\\StoreSession.cs"));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007E08 File Offset: 0x00006008
		public void SetADRecipientSessionFactory(Func<bool, ConsistencyMode, IRecipientSession> adRecipientSessionFactory)
		{
			using (this.CheckDisposed("SetADRecipientSessionFactory"))
			{
				ArgumentValidator.ThrowIfNull("adRecipientSessionFactory", adRecipientSessionFactory);
				this.adRecipientSessionFactory = adRecipientSessionFactory;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007E54 File Offset: 0x00006054
		public void SetADConfigurationSessionFactory(Func<bool, ConsistencyMode, IConfigurationSession> adConfigurationSessionFactory)
		{
			using (this.CheckDisposed("SetADConfigurationSessionFactory"))
			{
				ArgumentValidator.ThrowIfNull("adConfigurationSessionFactory", adConfigurationSessionFactory);
				this.adConfigurationSessionFactory = adConfigurationSessionFactory;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007EA0 File Offset: 0x000060A0
		public void Deliver(Item item, ProxyAddress recipientProxyAddress, RecipientItemType recipientType)
		{
			using (this.CheckObjectState("Deliver"))
			{
				this.CheckCapabilities(this.Capabilities.CanDeliver, "CanDeliver");
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				MapiMessage mapiMessage = item.MapiMessage;
				if (mapiMessage == null)
				{
					ExTraceGlobals.SessionTracer.TraceError((long)this.GetHashCode(), "StoreSession::Deliver. The item.MapiMessage is Null.");
					throw new ArgumentException("item");
				}
				if (recipientType != RecipientItemType.Unknown && recipientType != RecipientItemType.To && recipientType != RecipientItemType.Cc && recipientType != RecipientItemType.Bcc)
				{
					ExTraceGlobals.SessionTracer.TraceError<RecipientItemType>((long)this.GetHashCode(), "StoreSession::Deliver. Unknown recipient type. recipientType = {0}", recipientType);
					throw new EnumOutOfRangeException("recipientType");
				}
				using (CallbackContext callbackContext = new CallbackContext(this))
				{
					using (MailboxEvaluationResult mailboxEvaluationResult = this.EvaluateFolderRules(item.CoreItem, recipientProxyAddress))
					{
						callbackContext.ItemOperationAuditInfo = new ItemAuditInfo((item.Id == null) ? null : item.Id.ObjectId, null, null, item.PropertyBag.TryGetProperty(CoreItemSchema.Subject) as string, item.PropertyBag.TryGetProperty(ItemSchema.From) as Participant);
						item.SaveFlags |= (PropertyBagSaveFlags.IgnoreMapiComputedErrors | PropertyBagSaveFlags.IgnoreUnresolvedHeaders);
						item.SaveInternal(SaveMode.NoConflictResolution, false, callbackContext, CoreItemOperation.Save);
						byte[] entryId = null;
						IDictionary<string, string> deliveryActivityInfo = null;
						if (this.ActivitySession != null)
						{
							deliveryActivityInfo = ActivityLogHelper.ExtractDeliveryDetails(this, item);
						}
						try
						{
							if (this.ExecuteFolderRulesOnBefore(mailboxEvaluationResult) != FolderRuleEvaluationStatus.Continue)
							{
								return;
							}
							ExTraceGlobals.SessionTracer.TraceDebug((long)this.GetHashCode(), "StoreSession::Deliver. Invoke MapiMessage.Deliver.");
							bool flag = false;
							try
							{
								if (this != null)
								{
									this.BeginMapiCall();
									this.BeginServerHealthCall();
									flag = true;
								}
								if (StorageGlobals.MapiTestHookBeforeCall != null)
								{
									StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
								}
								mapiMessage.Deliver((RecipientType)recipientType);
								if (this.ActivitySession != null)
								{
									PropValue prop = mapiMessage.GetProp(PropTag.EntryId);
									if (!prop.IsError())
									{
										entryId = prop.GetBytes();
									}
								}
							}
							catch (MapiPermanentException ex)
							{
								throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotDeliverItem, ex, this, this, "{0}. MapiException = {1}.", new object[]
								{
									string.Format("StoreSession::Deliver.", new object[0]),
									ex
								});
							}
							catch (MapiRetryableException ex2)
							{
								throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotDeliverItem, ex2, this, this, "{0}. MapiException = {1}.", new object[]
								{
									string.Format("StoreSession::Deliver.", new object[0]),
									ex2
								});
							}
							finally
							{
								try
								{
									if (this != null)
									{
										this.EndMapiCall();
										if (flag)
										{
											this.EndServerHealthCall();
										}
									}
								}
								finally
								{
									if (StorageGlobals.MapiTestHookAfterCall != null)
									{
										StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
									}
								}
							}
						}
						finally
						{
							this.ExecuteFolderRulesOnAfter(mailboxEvaluationResult);
						}
						item.CoreItem.PropertyBag.Clear();
						if (this.ActivitySession != null)
						{
							this.ActivitySession.CaptureDelivery(StoreObjectId.FromProviderSpecificIdOrNull(entryId), deliveryActivityInfo);
						}
						this.OnAfterItemChange(ItemChangeOperation.Create, this, null, item.CoreItem as CoreItem, ConflictResolutionResult.Success, callbackContext);
					}
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00008238 File Offset: 0x00006438
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000828C File Offset: 0x0000648C
		public bool MessagesWereDownloaded
		{
			get
			{
				bool result;
				using (this.CreateSessionGuard("MessagesWereDownloaded::get"))
				{
					result = (this.gccProtocolLoggerSession != null && this.gccProtocolLoggerSession.MessagesWereDownloaded);
				}
				return result;
			}
			set
			{
				using (this.CreateSessionGuard("MessagesWereDownloaded::set"))
				{
					if (this.gccProtocolLoggerSession != null)
					{
						this.gccProtocolLoggerSession.MessagesWereDownloaded = true;
						this.gccProtocolLoggerSession.TagCurrentIntervalAsLogworthy();
					}
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000082E4 File Offset: 0x000064E4
		internal ClientSessionInfo RemoteClientSessionInfo
		{
			get
			{
				ClientSessionInfo result;
				using (this.CreateSessionGuard("RemoteClientSessionInfo::get"))
				{
					result = this.remoteClientSessionInfo;
				}
				return result;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00008328 File Offset: 0x00006528
		public void SetRemoteClientSessionInfo(byte[] infoBlob)
		{
			using (this.CheckDisposed("SetRemoteClientSessionInfo"))
			{
				this.remoteClientSessionInfo = ClientSessionInfo.FromBytes(infoBlob);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008370 File Offset: 0x00006570
		internal static int GetConfigFromRegistry(string registryKeyName, string registryValueName, int defaultValue, Predicate<int> validator)
		{
			int result = defaultValue;
			Exception ex = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryKeyName, RegistryKeyPermissionCheck.ReadSubTree))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue(registryValueName, defaultValue);
						if (value is int && (validator == null || validator((int)value)))
						{
							result = (int)value;
						}
						else
						{
							ExTraceGlobals.SessionTracer.TraceDebug<string, string, int>(0L, "Invalid value provided for registry entry - {0}\\{1}. Default Value of {2} will be used.", registryKeyName, registryValueName, defaultValue);
						}
					}
				}
			}
			catch (UnauthorizedAccessException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			finally
			{
				if (ex != null)
				{
					result = defaultValue;
					ExTraceGlobals.SessionTracer.TraceError(0L, "Failed to read registry entry - {0}\\{1}. Default Value of {2} will be used. Exception - {3}", new object[]
					{
						registryKeyName,
						registryValueName,
						defaultValue,
						ex
					});
				}
			}
			return result;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000084A4 File Offset: 0x000066A4
		protected static byte[] GetTenantHint(IExchangePrincipal principal)
		{
			byte[] tenantHint = null;
			if (principal.MailboxInfo.OrganizationId != null && principal.MailboxInfo.OrganizationId != OrganizationId.ForestWideOrgId)
			{
				DirectoryHelper.DoAdCallAndTranslateExceptions(delegate
				{
					tenantHint = TenantPartitionHint.Serialize(TenantPartitionHint.FromOrganizationId(principal.MailboxInfo.OrganizationId));
				}, "GetTenantHint");
			}
			return tenantHint;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000851C File Offset: 0x0000671C
		public IRecipientSession GetADRecipientSession(bool isReadOnly, ConsistencyMode consistencyMode)
		{
			IRecipientSession result;
			using (this.CheckDisposed("GetADRecipientSession"))
			{
				result = this.adRecipientSessionFactory(isReadOnly, consistencyMode);
			}
			return result;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00008564 File Offset: 0x00006764
		public IConfigurationSession GetADConfigurationSession(bool isReadOnly, ConsistencyMode consistencyMode)
		{
			IConfigurationSession result;
			using (this.CheckDisposed("GetADConfigurationSession"))
			{
				result = this.adConfigurationSessionFactory(isReadOnly, consistencyMode);
			}
			return result;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000085AC File Offset: 0x000067AC
		internal string WlmOperationInstance
		{
			get
			{
				string result;
				using (this.CreateSessionGuard("WlmOperationInstance::get"))
				{
					if (this.wlmOperationInstance == null)
					{
						string str = "NA";
						MailboxSession mailboxSession = this as MailboxSession;
						IExchangePrincipal exchangePrincipal = (mailboxSession != null) ? mailboxSession.MailboxOwner : null;
						string str2;
						if (exchangePrincipal != null && exchangePrincipal.MailboxInfo.IsRemote)
						{
							Uri remoteEndpoint = this.RemoteMailboxProperties.GetRemoteEndpoint(this.MailboxOwner.MailboxInfo.RemoteIdentity);
							if (remoteEndpoint.HostNameType != UriHostNameType.IPv4 && remoteEndpoint.HostNameType != UriHostNameType.IPv6)
							{
								str2 = MachineName.GetNodeNameFromFqdn(remoteEndpoint.Host);
							}
							else
							{
								str2 = remoteEndpoint.Host;
							}
							if (!exchangePrincipal.MailboxInfo.MailboxDatabase.IsNullOrEmpty())
							{
								str = exchangePrincipal.MailboxInfo.GetDatabaseGuid().ToString("D");
							}
						}
						else
						{
							str2 = MachineName.GetNodeNameFromFqdn(this.ServerFullyQualifiedDomainName);
							if (this.MdbGuid != Guid.Empty)
							{
								str = this.MdbGuid.ToString("D");
							}
						}
						this.wlmOperationInstance = str2 + "." + str;
					}
					result = this.wlmOperationInstance;
				}
				return result;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008794 File Offset: 0x00006994
		public void PreFinalSyncDataProcessing(int? sourceMailboxVersion)
		{
			using (this.CheckObjectState("PreFinalSyncDataProcessing"))
			{
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						if (sourceMailboxVersion == null)
						{
							ExTraceGlobals.SessionTracer.TraceDebug<Guid>((long)this.GetHashCode(), "StoreSession::PreFinalSyncDataProcessing. Skipping mailbox as version number is null. GUID: {0}", this.MailboxGuid);
							return;
						}
						ServerVersion serverVersion = new ServerVersion(sourceMailboxVersion.Value);
						if (serverVersion.Major <= Server.Exchange2009MajorVersion)
						{
							AutomaticLink.PerformContactLinkingAfterMigration(this);
							return;
						}
						ExTraceGlobals.SessionTracer.TraceDebug<Guid, ServerVersion>((long)this.GetHashCode(), "StoreSession::PreFinalSyncDataProcessing. Skipping mailbox due to version number. GUID: {0}. Version: {1}", this.MailboxGuid, serverVersion);
					});
				}
				catch (GrayException ex)
				{
					ExTraceGlobals.SessionTracer.TraceError<Guid, GrayException>((long)this.GetHashCode(), "StoreSession::PreFinalSyncDataProcessing. Failed while processing mailbox {1}. Error: {2}", this.MailboxGuid, ex);
					ExWatson.SendReport(ex);
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008830 File Offset: 0x00006A30
		protected string[] InternalGetSupportedRoutingTypes()
		{
			string[] routingTypes = RoutingTypeBuilder.Instance.GetRoutingTypes();
			for (int i = 0; i < routingTypes.Length; i++)
			{
				routingTypes[i] = string.Intern(Participant.NormalizeRoutingType(routingTypes[i]));
			}
			return routingTypes;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008868 File Offset: 0x00006A68
		protected IRPCLatencyProvider GetMdbHealthMonitor()
		{
			IRPCLatencyProvider result;
			using (this.CheckDisposed("HealthMonitor::get"))
			{
				result = (this.mdbHealthMonitor ?? this.GetHealthMonitor<IRPCLatencyProvider>(new MdbResourceHealthMonitorKey(this.MdbGuid), ref this.mdbHealthMonitor));
			}
			return result;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000088C4 File Offset: 0x00006AC4
		protected IDatabaseReplicationProvider GetReplicationHealthMonitor()
		{
			IDatabaseReplicationProvider result;
			using (this.CheckDisposed("ReplicationHealthMonitor::get"))
			{
				result = (this.replicationHealthMonitor ?? this.GetHealthMonitor<IDatabaseReplicationProvider>(new MdbReplicationResourceHealthMonitorKey(this.MdbGuid), ref this.replicationHealthMonitor));
			}
			return result;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008920 File Offset: 0x00006B20
		protected IDatabaseAvailabilityProvider GetAvailabilityHealthMonitor()
		{
			IDatabaseAvailabilityProvider result;
			using (this.CheckDisposed("AvailabilityHealthMonitor::get"))
			{
				result = (this.availabilityHealthMonitor ?? this.GetHealthMonitor<IDatabaseAvailabilityProvider>(new MdbAvailabilityResourceHealthMonitorKey(this.MdbGuid), ref this.availabilityHealthMonitor));
			}
			return result;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000897C File Offset: 0x00006B7C
		internal virtual MailboxEvaluationResult EvaluateFolderRules(ICoreItem item, ProxyAddress recipientProxyAddress)
		{
			MailboxEvaluationResult result;
			using (this.CheckObjectState("EvaluateFolderRules"))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000089B8 File Offset: 0x00006BB8
		internal virtual void ExecuteFolderRulesOnAfter(MailboxEvaluationResult evaluationResult)
		{
			using (this.CheckObjectState("ExecuteFolderRulesOnAfter"))
			{
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000089F4 File Offset: 0x00006BF4
		internal virtual FolderRuleEvaluationStatus ExecuteFolderRulesOnBefore(MailboxEvaluationResult evaluationResult)
		{
			FolderRuleEvaluationStatus result;
			using (this.CheckObjectState("ExecuteFolderRulesOnBefore"))
			{
				result = FolderRuleEvaluationStatus.Continue;
			}
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008A30 File Offset: 0x00006C30
		private T GetHealthMonitor<T>(ResourceKey key, ref T member) where T : IResourceLoadMonitor
		{
			if (this.IsRemote)
			{
				throw new InvalidOperationException();
			}
			try
			{
				member = (T)((object)ResourceHealthMonitorManager.Singleton.Get(key));
			}
			catch (DataSourceTransientException ex)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "StoreSession.GetHealthMonitor encountered transient directory exception: {0}.", new object[]
				{
					ex
				});
			}
			catch (DataSourceOperationException ex2)
			{
				throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "StoreSession.GetHealthMonitor encountered operation directory exception: {0}.", new object[]
				{
					ex2
				});
			}
			if (member == null)
			{
				throw new InvalidOperationException("ResourceHealthMonitorManager.Get should never return a null monitor.  ResourceKey: " + key);
			}
			return member;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008AE4 File Offset: 0x00006CE4
		private void AddOperations(PerRPCPerformanceStatistics stats, RpcStatistics rpcStats)
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (stats.timeInServer.TotalMilliseconds > 0.0)
			{
				ActivityContext.AddOperation(currentActivityScope, ActivityOperationType.StoreCall, this.WlmOperationInstance, (float)stats.timeInServer.TotalMilliseconds, 1);
			}
			ActivityContext.AddOperation(currentActivityScope, ActivityOperationType.MailboxCall, this.WlmOperationInstance, (float)this.stopwatch.ElapsedMilliseconds, (int)rpcStats.rpcCount);
			ActivityContext.AddOperation(currentActivityScope, ActivityOperationType.MapiLatency, this.WlmOperationInstance, (float)this.stopwatch.ElapsedMilliseconds, 1);
			ActivityContext.AddOperation(currentActivityScope, ActivityOperationType.MapiCount, this.WlmOperationInstance, 0f, 1);
			if (rpcStats.rpcCount > 0U)
			{
				ActivityContext.AddOperation(currentActivityScope, ActivityOperationType.RpcCount, this.WlmOperationInstance, 0f, (int)rpcStats.rpcCount);
				ActivityContext.AddOperation(currentActivityScope, ActivityOperationType.RpcLatency, this.WlmOperationInstance, (float)this.stopwatch.ElapsedMilliseconds, 1);
			}
			ActivityContext.AddOperation(currentActivityScope, ActivityOperationType.Rop, this.WlmOperationInstance, 0f, (int)stats.totalDbOperations);
			if (stats.timeInCPU > TimeSpan.Zero)
			{
				ActivityContext.AddOperation(currentActivityScope, ActivityOperationType.StoreCpu, this.WlmOperationInstance, (float)stats.timeInCPU.TotalMilliseconds, 1);
			}
			if (stats.logBytes > 0U)
			{
				ActivityContext.AddOperation(currentActivityScope, ActivityOperationType.MailboxLogBytes, this.WlmOperationInstance, stats.logBytes, 1);
			}
			if (rpcStats.messagesCreated > 0U)
			{
				ActivityContext.AddOperation(currentActivityScope, ActivityOperationType.MailboxMessagesCreated, this.WlmOperationInstance, rpcStats.messagesCreated, 1);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008C44 File Offset: 0x00006E44
		internal void BeginMapiCall()
		{
			using (this.CreateSessionGuard("BeginMapiCall"))
			{
				if (this.mailboxStoreObject != null && !this.IsRemote)
				{
					this.mailboxStoreObject.MapiStore.ClearStorePerRPCStats();
					this.mailboxStoreObject.MapiStore.ClearRpcStatistics();
					IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
					if (currentActivityScope != null)
					{
						this.mailboxStoreObject.MapiStore.SetCurrentActivityInfo(currentActivityScope.ActivityId, currentActivityScope.Component, currentActivityScope.Protocol, currentActivityScope.Action);
					}
				}
			}
			this.stopwatch.Restart();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008CEC File Offset: 0x00006EEC
		internal void EndMapiCall()
		{
			this.stopwatch.Stop();
			using (this.CreateSessionGuard("EndMapiCall"))
			{
				if (this.mailboxStoreObject != null && !this.IsRemote)
				{
					PerRPCPerformanceStatistics storePerRPCStats = this.mailboxStoreObject.MapiStore.GetStorePerRPCStats();
					RpcStatistics rpcStatistics = this.mailboxStoreObject.MapiStore.GetRpcStatistics();
					this.AddOperations(storePerRPCStats, rpcStatistics);
					if (storePerRPCStats.validVersion > 0U)
					{
						this.AccumulatePerRPCStatistics(storePerRPCStats);
						CumulativeRPCPerformanceStatistics storeCumulativeRPCStats = this.GetStoreCumulativeRPCStats();
						try
						{
							this.GetMdbHealthMonitor().Update((int)storeCumulativeRPCStats.avgDbLatency, (storePerRPCStats.validVersion >= 2U) ? storeCumulativeRPCStats.totalDbOperations : 100U);
							if (storePerRPCStats.validVersion >= 4U)
							{
								this.GetReplicationHealthMonitor().Update(storeCumulativeRPCStats.dataProtectionHealth);
								this.GetAvailabilityHealthMonitor().Update(storeCumulativeRPCStats.dataAvailabilityHealth);
							}
						}
						catch (DataSourceTransientException ex)
						{
							throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "StoreSession.EndMapiCall encountered transient directory exception: {0}.", new object[]
							{
								ex
							});
						}
						catch (DataSourceOperationException ex2)
						{
							throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "StoreSession.EndMapiCall encountered operation directory exception: {0}.", new object[]
							{
								ex2
							});
						}
					}
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008E44 File Offset: 0x00007044
		internal DatabaseLocationInfo GetDatabaseLocationInfoOnOperationFailure()
		{
			bool flag = false;
			DatabaseLocationInfo databaseLocationInfo = null;
			if (!this.MailboxOwner.MailboxInfo.MailboxDatabase.IsNullOrEmpty())
			{
				try
				{
					databaseLocationInfo = StoreSession.DatabaseLocationProvider.GetLocationInfo(this.MailboxOwner.MailboxInfo.MailboxDatabase.ObjectGuid, false, this.MailboxOwner.IsCrossSiteAccessAllowed);
				}
				catch (DatabaseLocationUnavailableException)
				{
					flag = true;
				}
				if (databaseLocationInfo != null)
				{
					flag |= (this.MailboxOwner.MailboxInfo.Location.ServerLegacyDn == null || !string.Equals(this.MailboxOwner.MailboxInfo.Location.ServerLegacyDn, databaseLocationInfo.ServerLegacyDN, StringComparison.OrdinalIgnoreCase));
					if (databaseLocationInfo.RequestResult == DatabaseLocationInfoResult.Success && !flag)
					{
						databaseLocationInfo = StoreSession.DatabaseLocationProvider.GetLocationInfo(this.MailboxOwner.MailboxInfo.GetDatabaseGuid(), true, this.MailboxOwner.IsCrossSiteAccessAllowed);
					}
				}
			}
			return databaseLocationInfo;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008F2C File Offset: 0x0000712C
		internal virtual void BeginServerHealthCall()
		{
			using (this.CreateSessionGuard("BeginServerHealthCall"))
			{
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008F68 File Offset: 0x00007168
		internal virtual void EndServerHealthCall()
		{
			using (this.CreateSessionGuard("EndServerHealthCall"))
			{
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008FA4 File Offset: 0x000071A4
		private void InternalInitializeGccProtocolSession()
		{
			if (string.IsNullOrEmpty(MapiStore.GetLocalServerFqdn()))
			{
				MapiStore.SetLocalServerFqdn(LocalServer.GetServer().Fqdn);
			}
			this.gccProtocolLoggerSession = new GccProtocolActivityLoggerSession(this);
			this.gccProtocolLoggerSession.StartSession();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008FD8 File Offset: 0x000071D8
		private void InternalEndGccProtocolSession()
		{
			if (this.gccProtocolLoggerSession != null)
			{
				this.gccProtocolLoggerSession.EndSession();
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008FED File Offset: 0x000071ED
		private void InternalDisposeGccProtocolSession()
		{
			Util.DisposeIfPresent(this.gccProtocolLoggerSession);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008FFC File Offset: 0x000071FC
		private void InternalLogIpEndpoints(IPAddress clientIPAddress, IPAddress serverIPAddress)
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.DataStorage.LogIpEndpoints.Enabled && this.gccProtocolLoggerSession != null)
			{
				string gccResourceIdentifier = "Unknown";
				try
				{
					gccResourceIdentifier = this.GccResourceIdentifier;
				}
				catch (InvalidOperationException)
				{
				}
				this.gccProtocolLoggerSession.SetStoreSessionInfo(gccResourceIdentifier, clientIPAddress, serverIPAddress);
			}
		}

		// Token: 0x04000005 RID: 5
		private readonly Schema schema;

		// Token: 0x04000006 RID: 6
		private readonly SubscriptionsManager subscriptionsManager;

		// Token: 0x04000007 RID: 7
		private readonly IdConverter idConverter;

		// Token: 0x04000008 RID: 8
		private static readonly int currentServerMajorVersion = 15;

		// Token: 0x04000009 RID: 9
		private readonly Stopwatch stopwatch = new Stopwatch();

		// Token: 0x0400000A RID: 10
		protected readonly CultureInfo SessionCultureInfo;

		// Token: 0x0400000B RID: 11
		protected string clientInfoString;

		// Token: 0x0400000C RID: 12
		private static bool useRPCContextPoolResiliency = false;

		// Token: 0x0400000D RID: 13
		[ThreadStatic]
		private static bool testRequestExchangeRpcServer;

		// Token: 0x0400000E RID: 14
		private IContentIndexingSession contentIndexingSession;

		// Token: 0x0400000F RID: 15
		protected bool isConnected;

		// Token: 0x04000010 RID: 16
		protected LogonType logonType;

		// Token: 0x04000011 RID: 17
		protected object identity;

		// Token: 0x04000012 RID: 18
		protected ConnectFlag connectFlag;

		// Token: 0x04000013 RID: 19
		protected string userLegacyDn;

		// Token: 0x04000014 RID: 20
		protected GenericIdentity auxiliaryIdentity;

		// Token: 0x04000015 RID: 21
		protected UnifiedGroupMemberType unifiedGroupMemberType;

		// Token: 0x04000016 RID: 22
		private string[] supportedRoutingTypes;

		// Token: 0x04000017 RID: 23
		private long clientVersion;

		// Token: 0x04000018 RID: 24
		private string clientProcessName;

		// Token: 0x04000019 RID: 25
		private string clientMachineName;

		// Token: 0x0400001A RID: 26
		private readonly ObjectThreadTracker sessionThreadTracker = new ObjectThreadTracker();

		// Token: 0x0400001B RID: 27
		private ExchangeOperationContext operationContext = new ExchangeOperationContext();

		// Token: 0x0400001C RID: 28
		private bool blockFolderCreation;

		// Token: 0x0400001D RID: 29
		private MailboxMoveStage mailboxMoveStage;

		// Token: 0x0400001E RID: 30
		private bool isDisposed;

		// Token: 0x0400001F RID: 31
		private OpenStoreFlag storeFlag;

		// Token: 0x04000020 RID: 32
		private bool isDead;

		// Token: 0x04000021 RID: 33
		private int preferredInternetCodePageForShiftJis;

		// Token: 0x04000022 RID: 34
		private int requiredCoverage = 100;

		// Token: 0x04000023 RID: 35
		private StoreSession.IItemBinder itemBinder;

		// Token: 0x04000024 RID: 36
		private string mappingSignature;

		// Token: 0x04000025 RID: 37
		private WeakReference propResolutionCache;

		// Token: 0x04000026 RID: 38
		private IBudget budget;

		// Token: 0x04000027 RID: 39
		private MailboxStoreObject mailboxStoreObject;

		// Token: 0x04000028 RID: 40
		private DisposeTracker disposeTracker;

		// Token: 0x04000029 RID: 41
		private ILogChanges testLogCallback;

		// Token: 0x0400002A RID: 42
		private SessionCapabilities sessionCapabilities;

		// Token: 0x0400002B RID: 43
		private SpoolerManager spoolerManager;

		// Token: 0x0400002C RID: 44
		private IPAddress clientIPAddress;

		// Token: 0x0400002D RID: 45
		private IPAddress serverIPAddress;

		// Token: 0x0400002E RID: 46
		private MapiNotificationHandle tickleNotificationHandle;

		// Token: 0x0400002F RID: 47
		private MapiNotificationHandle droppedNotificationHandle;

		// Token: 0x04000030 RID: 48
		private IDictionary<StoreObjectId, bool> isContactFolder;

		// Token: 0x04000031 RID: 49
		private CumulativeRPCPerformanceStatistics cumulativeRPCStats;

		// Token: 0x04000032 RID: 50
		private static IDatabaseLocationProvider databaseLocationProvider;

		// Token: 0x04000033 RID: 51
		private IRPCLatencyProvider mdbHealthMonitor;

		// Token: 0x04000034 RID: 52
		private IDatabaseReplicationProvider replicationHealthMonitor;

		// Token: 0x04000035 RID: 53
		private IDatabaseAvailabilityProvider availabilityHealthMonitor;

		// Token: 0x04000036 RID: 54
		private string wlmOperationInstance;

		// Token: 0x04000037 RID: 55
		private GccProtocolActivityLoggerSession gccProtocolLoggerSession;

		// Token: 0x04000038 RID: 56
		private ClientSessionInfo remoteClientSessionInfo;

		// Token: 0x04000039 RID: 57
		private Func<bool, ConsistencyMode, IRecipientSession> adRecipientSessionFactory;

		// Token: 0x0400003A RID: 58
		private Func<bool, ConsistencyMode, IConfigurationSession> adConfigurationSessionFactory;

		// Token: 0x0400003B RID: 59
		private RemoteMailboxProperties remoteMailboxProperties;

		// Token: 0x0400003C RID: 60
		protected static readonly IDirectoryAccessor directoryAccessor = new DirectoryAccessor();

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x0600012F RID: 303
		protected delegate AggregateOperationResult ActOnObjectsDelegate(Folder parentFolder, StoreId[] sourceObjectIds);

		// Token: 0x0200000A RID: 10
		public interface IItemBinder
		{
			// Token: 0x06000132 RID: 306
			Item BindItem(StoreObjectId itemId, bool isPublic, StoreObjectId folderId);
		}
	}
}
