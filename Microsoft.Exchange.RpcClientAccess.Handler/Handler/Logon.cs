using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000086 RID: 134
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class Logon : PropertyServerObject
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x000266E8 File Offset: 0x000248E8
		protected Logon(StoreSession session, ClientSecurityContext delegatedClientSecurityContext, ConnectionHandler connectionHandler, NotificationHandler notificationHandler, OpenFlags openFlags, byte logonId, ProtocolLogLogonType protocolLogLogonType) : this(session, delegatedClientSecurityContext, connectionHandler, notificationHandler, openFlags, logonId, protocolLogLogonType, ClientSideProperties.LogonInstance, PropertyConverter.Logon)
		{
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00026710 File Offset: 0x00024910
		protected Logon(StoreSession session, ClientSecurityContext delegatedClientSecurityContext, ConnectionHandler connectionHandler, NotificationHandler notificationHandler, OpenFlags openFlags, byte logonId, ProtocolLogLogonType protocolLogLogonType, ClientSideProperties clientSideProperties, PropertyConverter converter) : base(clientSideProperties, converter)
		{
			this.connectionHandler = connectionHandler;
			this.logonId = logonId;
			this.notificationHandler = notificationHandler;
			this.protocolLogLogonType = protocolLogLogonType;
			this.delegatedClientSecurityContext = delegatedClientSecurityContext;
			this.propertyDefinitionFactory = new CoreObjectPropertyDefinitionFactory(session, session.Mailbox.CoreObject.PropertyBag);
			this.storageObjectProperties = new CoreObjectProperties(session.Mailbox.CoreObject.PropertyBag);
			this.supportProgressForSetReadFlags = true;
			if (connectionHandler.Connection.ClientInformation.Version >= MapiVersion.Outlook14 && (openFlags & OpenFlags.SupportProgress) != OpenFlags.SupportProgress)
			{
				this.supportProgressForSetReadFlags = false;
			}
			this.connectionDropSubscription = Subscription.CreateMailboxSubscription(session, new NotificationHandler(this.BackEndConnectionDropNotificationHandler), NotificationType.ConnectionDropped);
			this.fastTransferActivityThrottle = PerServerActivityThrottle<RopFastTransferSourceGetBuffer>.GetPerServerActivityThrottle(session.ServerFullyQualifiedDomainName);
			if (!String8Encodings.TryGetEncoding(this.Connection.CodePageId, out this.string8Encoding))
			{
				string message = string.Format("Cannot resolve code page: {0}", this.Connection.CultureInfo.TextInfo.ANSICodePage);
				throw new RopExecutionException(message, ErrorCode.UnknownCodepage);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0002683F File Offset: 0x00024A3F
		public override Schema Schema
		{
			get
			{
				return MailboxSchema.Instance;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00026846 File Offset: 0x00024A46
		internal IConnection Connection
		{
			get
			{
				return this.connectionHandler.Connection;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00026853 File Offset: 0x00024A53
		internal byte LogonId
		{
			get
			{
				return this.logonId;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0002685B File Offset: 0x00024A5B
		internal Encoding LogonString8Encoding
		{
			get
			{
				return this.string8Encoding;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00026863 File Offset: 0x00024A63
		internal IResourceTracker ResourceTracker
		{
			get
			{
				if (this.resourceTracker == null)
				{
					this.resourceTracker = this.CreateResourceTracker();
				}
				return this.resourceTracker;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0002687F File Offset: 0x00024A7F
		internal bool HasActiveAsyncOperation
		{
			get
			{
				return this.hasActiveAsyncOperation;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00026887 File Offset: 0x00024A87
		internal bool SupportProgressForSetReadFlags
		{
			get
			{
				return this.supportProgressForSetReadFlags;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0002688F File Offset: 0x00024A8F
		protected ClientSecurityContext DelegatedClientSecurityContext
		{
			get
			{
				return this.delegatedClientSecurityContext;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00026897 File Offset: 0x00024A97
		internal static void ValidateLogonSettings(LogonFlags logonFlags, OpenFlags openFlags, MailboxId? mailboxId)
		{
			if (!Logon.AreSupportedOpenFlags(openFlags))
			{
				throw new RopExecutionException("Unsupported openFlags.", (ErrorCode)2147746050U);
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000268B4 File Offset: 0x00024AB4
		internal static ExchangePrincipal FindExchangePrincipal(IConnection connection, MailboxId mailboxId)
		{
			ExchangePrincipal result;
			try
			{
				TestInterceptor.Intercept(TestInterceptorLocation.Logon_FindExchangePrincipal, new object[0]);
				if (mailboxId.IsLegacyDn)
				{
					result = connection.FindExchangePrincipalByLegacyDN(mailboxId.LegacyDn);
				}
				else
				{
					result = ExchangePrincipal.FromLocalServerMailboxGuid(connection.OrganizationId.ToADSessionSettings(), mailboxId.DatabaseGuid, mailboxId.MailboxGuid);
				}
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new RopExecutionException(string.Format("Couldn't find a mailbox for {0} in the current forest. The client should attempt to re-discover it.", mailboxId), ErrorCode.UnknownUser, innerException);
			}
			return result;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002693C File Offset: 0x00024B3C
		internal static LegacyDN CreatePersonalizedServerRedirectLegacyDN(ExchangePrincipal exchangePrincipal)
		{
			LegacyDN legacyDN;
			if (!LegacyDN.TryParse(exchangePrincipal.MailboxInfo.Location.RpcClientAccessServerLegacyDn, out legacyDN))
			{
				throw new RopExecutionException(string.Format("Invalid RpcClientAccessServerLegacyDistinguishedName property {0}", exchangePrincipal.MailboxInfo.Location.RpcClientAccessServerLegacyDn), ErrorCode.ADPropertyError);
			}
			return ExchangeRpcClientAccess.CreatePersonalizedServerRedirectLegacyDN(legacyDN, exchangePrincipal.MailboxInfo.MailboxGuid, exchangePrincipal.MailboxInfo.PrimarySmtpAddress.Domain);
		}

		// Token: 0x06000555 RID: 1365
		internal abstract StoreId[] GetDefaultFolderIds();

		// Token: 0x06000556 RID: 1366 RVA: 0x000269AB File Offset: 0x00024BAB
		public void AbortSubmit(StoreId folderId, StoreId messageId)
		{
			this.Session.AbortSubmit(this.Session.IdConverter.CreateMessageId(folderId, messageId));
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000269D4 File Offset: 0x00024BD4
		internal NotificationQueue NotificationQueue
		{
			get
			{
				if (this.notificationQueue == null)
				{
					this.notificationQueue = new NotificationQueue(this);
				}
				return this.notificationQueue;
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000269F0 File Offset: 0x00024BF0
		internal void LogLogoff()
		{
			ProtocolLog.LogLogoff(this.protocolLogLogonType, (int)this.LogonId);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00026A04 File Offset: 0x00024C04
		internal IDisposable GetFastTransferActivityLock(RopId rop)
		{
			IDisposable result = null;
			ExDateTime utcNow = ExDateTime.UtcNow;
			uint num;
			if (this.fastTransferRejectionEnd > utcNow)
			{
				num = (uint)(this.fastTransferRejectionEnd - utcNow).TotalMilliseconds;
				throw new ClientBackoffException("This logon is already under a Fast Transfer get buffer backoff, so continue the backoff.", this.LogonId, 0U, new BackoffRopData[]
				{
					new BackoffRopData(rop, num)
				}, Array<byte>.Empty);
			}
			if (this.fastTransferActivityThrottle.TryGetActivityLock(this.fastTransferRejectionCount >= Configuration.ServiceConfiguration.FastTransferBackoffRetryCount || this.Connection.ClientInformation.Mode == ClientMode.ExchangeServer, Configuration.ServiceConfiguration.FastTransferMaxRequests, out result))
			{
				this.fastTransferRejectionCount = 0;
				return result;
			}
			this.fastTransferRejectionCount++;
			num = (uint)(this.fastTransferRejectionCount * Configuration.ServiceConfiguration.FastTransferBackoffInterval);
			this.fastTransferRejectionEnd = utcNow.AddMilliseconds(num);
			throw new ClientBackoffException("Too many active Fast Transfer get buffer requests to same backend.", this.LogonId, 0U, new BackoffRopData[]
			{
				new BackoffRopData(rop, num)
			}, Array<byte>.Empty);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00026B20 File Offset: 0x00024D20
		internal Rights GetEffectiveRights(byte[] addressBookId, StoreId folderId)
		{
			StoreObjectId folderId2 = this.Session.IdConverter.CreateFolderId(folderId);
			return (Rights)this.Session.GetEffectiveRights(addressBookId, folderId2);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00026B51 File Offset: 0x00024D51
		internal IAsyncOperationExecutor AsyncOperationExecutor
		{
			get
			{
				return this.asyncOperationExecutor;
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00026B5C File Offset: 0x00024D5C
		internal IAsyncOperationExecutor CreateAsyncOperationExecutor(SegmentedRopOperation segmentedRopOperation, object progressToken)
		{
			AsyncOperationExecutor asyncOperationExecutor = this.asyncOperationExecutor;
			if (asyncOperationExecutor != null)
			{
				if (!asyncOperationExecutor.IsCompleted)
				{
					string message = string.Format("This Logon has an AsyncOperationExecutor: {0}. It's not clear for creating a new async operation executor", asyncOperationExecutor.ToString());
					throw new RopExecutionException(message, ErrorCode.InvalidOperation);
				}
				this.RemoveAsyncExecutor();
			}
			this.asyncOperationExecutor = new AsyncOperationExecutor(segmentedRopOperation, progressToken, new Action(this.AsyncOperationDoneCallback));
			this.hasActiveAsyncOperation = true;
			return this.asyncOperationExecutor;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00026BC8 File Offset: 0x00024DC8
		internal void RemoveAsyncExecutor()
		{
			AsyncOperationExecutor asyncOperationExecutor = this.asyncOperationExecutor;
			if (asyncOperationExecutor != null)
			{
				asyncOperationExecutor.Dispose();
				this.asyncOperationExecutor = null;
				this.hasActiveAsyncOperation = false;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00026BF4 File Offset: 0x00024DF4
		private static bool AreSupportedOpenFlags(OpenFlags openFlags)
		{
			return (openFlags & OpenFlags.Transport) != OpenFlags.Transport && (openFlags & OpenFlags.InternetAnonymous) != OpenFlags.InternetAnonymous && (openFlags & OpenFlags.CallbackLogon) != OpenFlags.CallbackLogon && (openFlags & OpenFlags.Local) != OpenFlags.Local && (openFlags & OpenFlags.DeliverNormalMessage) != OpenFlags.DeliverNormalMessage && (openFlags & OpenFlags.DeliverQuotaMessage) != OpenFlags.DeliverQuotaMessage && (openFlags & OpenFlags.DeliverSpecialMessage) != OpenFlags.DeliverSpecialMessage;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00026C8D File Offset: 0x00024E8D
		private void BackEndConnectionDropNotificationHandler(Notification notification)
		{
			this.Connection.ExecuteInContext<Notification>(notification, delegate(Notification innerNotification)
			{
				ExTraceGlobals.NotificationHandlerTracer.TraceDebug<IConnection, NotificationType>(Activity.TraceId, "BackEndConnectionDropNotificationHandler. Connection = {0}.", this.Connection, innerNotification.Type);
				this.Connection.MarkAsDeadAndDropAllAsyncCalls();
			});
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00026CA8 File Offset: 0x00024EA8
		protected static T CreateStoreSession<T>(IConnection connection, OpenFlags openFlags, LogonExtendedRequestFlags extendedFlags, ExchangePrincipal exchangeMailboxPrincipal, ClientSecurityContext delegatedClientSecurityContext, string applicationId, Func<ExchangePrincipal, ClientSecurityContext, OpenFlags, IConnection, string, T> createSessionDelegate) where T : StoreSession
		{
			string arg = ((extendedFlags & LogonExtendedRequestFlags.ApplicationId) == LogonExtendedRequestFlags.ApplicationId) ? string.Format("{0};{1}", "Client=MSExchangeRPC", applicationId) : "Client=MSExchangeRPC";
			T result = default(T);
			try
			{
				TestInterceptor.Intercept(TestInterceptorLocation.Logon_CreateStoreSession, new object[0]);
				result = createSessionDelegate(exchangeMailboxPrincipal, delegatedClientSecurityContext, openFlags, connection, arg);
			}
			catch (AccessDeniedException innerException)
			{
				throw new RopExecutionException(string.Format("User '{0}' acting as '{1}' doesn't have permissions to logon to/act as mailbox '{2}'.", connection.AccessingClientSecurityContext.UserSid, connection.ActAsLegacyDN, exchangeMailboxPrincipal.LegacyDn), ErrorCode.LoginPerm, innerException);
			}
			catch (WrongServerException innerException2)
			{
				connection.InvalidateCachedUserInfo();
				throw new ClientBackoffException("Mailbox was moved to a different mailbox server. A client needs to retry.", innerException2);
			}
			catch (ConnectionFailedPermanentException innerException3)
			{
				connection.InvalidateCachedUserInfo();
				throw new RopExecutionException("Mailbox is not available", ErrorCode.MdbOffline, innerException3);
			}
			catch (ConnectionFailedTransientException ex)
			{
				connection.InvalidateCachedUserInfo();
				if (!ExceptionTranslator.IsConnectionToStoreDead(ex))
				{
					throw;
				}
				throw new RopExecutionException("Mailbox is temporarily unavailable", ErrorCode.MdbOffline, ex);
			}
			return result;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00026DB0 File Offset: 0x00024FB0
		private void AsyncOperationDoneCallback()
		{
			this.hasActiveAsyncOperation = false;
			if (this.notificationQueue != null && !this.notificationQueue.IsEmpty)
			{
				this.notificationHandler.InvokeCallback();
			}
		}

		// Token: 0x06000562 RID: 1378
		protected abstract IResourceTracker CreateResourceTracker();

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00026DD9 File Offset: 0x00024FD9
		protected override IPropertyDefinitionFactory PropertyDefinitionFactory
		{
			get
			{
				return this.propertyDefinitionFactory;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00026DE1 File Offset: 0x00024FE1
		protected override IStorageObjectProperties StorageObjectProperties
		{
			get
			{
				return this.storageObjectProperties;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00026DE9 File Offset: 0x00024FE9
		protected ICorePropertyBag PropertyBag
		{
			get
			{
				return this.Session.Mailbox.CoreObject.PropertyBag;
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00026E00 File Offset: 0x00025000
		public override void ClearCacheIfNeededForGetProperties()
		{
			this.PropertyBag.Clear();
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00026E10 File Offset: 0x00025010
		public override PropertyProblem[] SaveAndGetPropertyProblems(NativeStorePropertyDefinition[] propertyDefinitions, PropertyTag[] propertyTags)
		{
			FolderSaveResult folderSaveResult = ((CoreMailboxObject)this.Session.Mailbox.CoreObject).Save();
			return Folder.ConvertFolderSaveResultToProblems(folderSaveResult, propertyDefinitions, propertyTags);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00026E40 File Offset: 0x00025040
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.notificationQueue);
			Util.DisposeIfPresent(this.connectionDropSubscription);
			Util.DisposeIfPresent(this.asyncOperationExecutor);
			Util.DisposeIfPresent(this.delegatedClientSecurityContext);
			base.InternalDispose();
		}

		// Token: 0x04000231 RID: 561
		private const string RpcClientAccessApplicationId = "Client=MSExchangeRPC";

		// Token: 0x04000232 RID: 562
		private const string CompositeApplicationId = "{0};{1}";

		// Token: 0x04000233 RID: 563
		internal const LogonFlags LogonFlagsResultMask = LogonFlags.Private | LogonFlags.Undercover | LogonFlags.Ghosted;

		// Token: 0x04000234 RID: 564
		protected const int DefaultMaxUserMessageSizeInKBytes = 10240;

		// Token: 0x04000235 RID: 565
		private readonly byte logonId;

		// Token: 0x04000236 RID: 566
		private readonly ConnectionHandler connectionHandler;

		// Token: 0x04000237 RID: 567
		private readonly Encoding string8Encoding;

		// Token: 0x04000238 RID: 568
		private readonly ProtocolLogLogonType protocolLogLogonType;

		// Token: 0x04000239 RID: 569
		private readonly PerServerActivityThrottle<RopFastTransferSourceGetBuffer> fastTransferActivityThrottle;

		// Token: 0x0400023A RID: 570
		private readonly bool supportProgressForSetReadFlags;

		// Token: 0x0400023B RID: 571
		private AsyncOperationExecutor asyncOperationExecutor;

		// Token: 0x0400023C RID: 572
		private bool hasActiveAsyncOperation;

		// Token: 0x0400023D RID: 573
		private Subscription connectionDropSubscription;

		// Token: 0x0400023E RID: 574
		private NotificationQueue notificationQueue;

		// Token: 0x0400023F RID: 575
		private NotificationHandler notificationHandler;

		// Token: 0x04000240 RID: 576
		private ClientSecurityContext delegatedClientSecurityContext;

		// Token: 0x04000241 RID: 577
		private int fastTransferRejectionCount;

		// Token: 0x04000242 RID: 578
		private ExDateTime fastTransferRejectionEnd = ExDateTime.UtcNow;

		// Token: 0x04000243 RID: 579
		private IResourceTracker resourceTracker;

		// Token: 0x04000244 RID: 580
		private readonly CoreObjectPropertyDefinitionFactory propertyDefinitionFactory;

		// Token: 0x04000245 RID: 581
		private readonly CoreObjectProperties storageObjectProperties;
	}
}
