using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.SubscriptionCache;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Cache;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SubscriptionCacheServer : SubscriptionCacheRpcServer
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x0001443C File Offset: 0x0001263C
		public override byte[] TestUserCache(int version, byte[] inputBlob)
		{
			byte[] result;
			try
			{
				lock (this.syncObject)
				{
					this.numberOfCurrentWorkThreads++;
					if (this.stopped)
					{
						return RpcHelper.CreateResponsePropertyCollection(2835349507U, 268435457);
					}
					if (this.numberOfCurrentWorkThreads == 1)
					{
						ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)407UL, SubscriptionCacheServer.tracer, (long)this.GetHashCode(), "Resetting stopped event to non-signalled state.", new object[0]);
						this.workThreadsEvent.Reset();
					}
				}
				MdbefPropertyCollection args = MdbefPropertyCollection.Create(inputBlob, 0, inputBlob.Length);
				int num = -1;
				string text;
				string text2;
				if (!RpcHelper.TryGetProperty<string>(args, 2684485663U, out text, out text2) || !RpcHelper.TryGetProperty<int>(args, 2684420099U, out num, out text2) || !EnumValidator.IsValidValue<SubscriptionCacheAction>((SubscriptionCacheAction)num))
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)408UL, SubscriptionCacheServer.tracer, (long)this.GetHashCode(), "ServerVersion mismatch. Not found all required arguments. PrimarySmtpAddress:{0}, CacheActionValue:{1}, ErrorString:{2}", new object[]
					{
						text,
						num,
						text2
					});
					result = RpcHelper.CreateResponsePropertyCollection(2835349507U, 268435458);
				}
				else
				{
					SubscriptionCacheAction subscriptionCacheAction = (SubscriptionCacheAction)num;
					ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)409UL, SubscriptionCacheServer.tracer, (long)this.GetHashCode(), null, new object[]
					{
						text,
						"Taking action {0} on user mailbox {1}.",
						subscriptionCacheAction,
						text
					});
					SubscriptionCacheServer.TestUserCacheOperationInfo testUserCacheOperationInfo = new SubscriptionCacheServer.TestUserCacheOperationInfo(text, subscriptionCacheAction);
					ExchangePrincipal primaryMailboxOwner = testUserCacheOperationInfo.GetPrimaryMailboxOwner();
					this.TestUserCache(primaryMailboxOwner, testUserCacheOperationInfo);
					result = testUserCacheOperationInfo.GetOutputBytes();
				}
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
				result = null;
			}
			finally
			{
				lock (this.syncObject)
				{
					this.numberOfCurrentWorkThreads--;
					if (this.numberOfCurrentWorkThreads == 0 && this.workThreadsEvent != null)
					{
						ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)410UL, SubscriptionCacheServer.tracer, (long)this.GetHashCode(), "Resetting stopped event to signalled state.", new object[0]);
						this.workThreadsEvent.Set();
					}
				}
			}
			return result;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000146D8 File Offset: 0x000128D8
		internal static bool TryStartServer()
		{
			if (SubscriptionCacheServer.cacheServerInstance != null)
			{
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)411UL, SubscriptionCacheServer.tracer, 0L, "Subscription Cache RPC Server already registered and running.", new object[0]);
				return true;
			}
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
			FileSystemAccessRule accessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			FileSecurity fileSecurity = new FileSecurity();
			fileSecurity.SetOwner(securityIdentifier);
			fileSecurity.SetAccessRule(accessRule);
			SubscriptionCacheServer.cacheServerInstance = (SubscriptionCacheServer)RpcServerBase.RegisterServer(typeof(SubscriptionCacheServer), fileSecurity, 1, false, (uint)ContentAggregationConfig.MaxCacheRpcThreads);
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)412UL, ExTraceGlobals.CacheManagerLookupTracer, 0L, "Subscription Cache RPC Server loaded.", new object[0]);
			SubscriptionCacheServer.cacheServerInstance.Start();
			return true;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001478E File Offset: 0x0001298E
		internal static void StopServer()
		{
			if (SubscriptionCacheServer.cacheServerInstance != null)
			{
				SubscriptionCacheServer.cacheServerInstance.Stop();
				SubscriptionCacheServer.cacheServerInstance = null;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000147A8 File Offset: 0x000129A8
		private static SubscriptionCacheObject GetSubscriptionCacheObject(AggregationSubscription actualSubscription, Guid tenantGuid, Guid actualUserMailboxGuid)
		{
			string primaryMailboxUserLegacyDN = actualSubscription.PrimaryMailboxUserLegacyDN;
			if (actualSubscription.LastSuccessfulSyncTime != null)
			{
				DateTime.UtcNow - actualSubscription.LastSuccessfulSyncTime.Value;
			}
			else
			{
				DateTime.UtcNow - actualSubscription.CreationTime;
			}
			return new SubscriptionCacheObject(actualSubscription.SubscriptionGuid, actualSubscription.SubscriptionMessageId, actualSubscription.UserLegacyDN, actualSubscription.SubscriptionType, actualSubscription.AggregationType, actualSubscription.SyncPhase, null, actualSubscription.IncomingServerName, actualUserMailboxGuid, null, tenantGuid, null, null, null, null, false, true, null, SubscriptionCacheObjectState.Missing, null);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001485C File Offset: 0x00012A5C
		private static SubscriptionCacheObject GetSubscriptionCacheObject(SubscriptionInformation cacheSubscription, SubscriptionCacheObjectState state, string reasonForTheState)
		{
			if (cacheSubscription.LastSyncCompletedTime != null)
			{
				new TimeSpan?(ExDateTime.UtcNow - cacheSubscription.LastSyncCompletedTime.Value);
			}
			long? serializedSubscriptionVersion = (cacheSubscription.SerializedSubscription == null) ? null : new long?(cacheSubscription.SerializedSubscription.Version);
			return new SubscriptionCacheObject(cacheSubscription.SubscriptionGuid, cacheSubscription.SubscriptionMessageId, cacheSubscription.UserLegacyDn, cacheSubscription.SubscriptionType, cacheSubscription.AggregationType, cacheSubscription.SyncPhase, cacheSubscription.LastSyncCompletedTime, cacheSubscription.IncomingServerName, cacheSubscription.MailboxGuid, serializedSubscriptionVersion, cacheSubscription.TenantGuid, cacheSubscription.HubServerDispatched, cacheSubscription.LastHubServerDispatched, cacheSubscription.FirstOutstandingDispatchTime, cacheSubscription.LastSuccessfulDispatchTime, cacheSubscription.RecoverySyncEnabled, cacheSubscription.Disabled, cacheSubscription.Diagnostics, state, reasonForTheState);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001492C File Offset: 0x00012B2C
		private static SubscriptionCacheObject ValidateSubscription(SubscriptionInformation cacheSubscription, AggregationSubscription actualSubscription, Guid actualMailboxGuid)
		{
			string text;
			bool flag = cacheSubscription.Validate(actualSubscription, actualMailboxGuid, false, out text);
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)413UL, SubscriptionCacheServer.tracer, actualSubscription.SubscriptionGuid, actualMailboxGuid, "Validated cache entry {0} to find it is valid: {1}. {2}", new object[]
			{
				cacheSubscription,
				!flag,
				text
			});
			return SubscriptionCacheServer.GetSubscriptionCacheObject(cacheSubscription, flag ? SubscriptionCacheObjectState.Invalid : SubscriptionCacheObjectState.Valid, text);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001499C File Offset: 0x00012B9C
		private void Start()
		{
			lock (this.syncObject)
			{
				this.stopped = false;
				if (this.workThreadsEvent == null)
				{
					this.workThreadsEvent = new ManualResetEvent(true);
				}
			}
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)414UL, SubscriptionCacheServer.tracer, (long)this.GetHashCode(), "Started the SubscriptionCacheServer.", new object[0]);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00014A20 File Offset: 0x00012C20
		private void Stop()
		{
			lock (this.syncObject)
			{
				if (this.stopped)
				{
					return;
				}
				this.stopped = true;
			}
			RpcServerBase.StopServer(SubscriptionCacheRpcServer.RpcIntfHandle);
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)415UL, SubscriptionCacheServer.tracer, (long)this.GetHashCode(), "Waiting for existing cache server threads to finish.", new object[0]);
			this.workThreadsEvent.WaitOne();
			lock (this.syncObject)
			{
				this.workThreadsEvent.Close();
				this.workThreadsEvent = null;
			}
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)416UL, SubscriptionCacheServer.tracer, (long)this.GetHashCode(), "Stopped the SubscriptionCacheServer.", new object[0]);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00014B18 File Offset: 0x00012D18
		private void TestUserCache(ExchangePrincipal mailboxOwner, SubscriptionCacheServer.TestUserCacheOperationInfo operationInfo)
		{
			if (mailboxOwner == null)
			{
				return;
			}
			int cacheObjectsCount = operationInfo.CacheObjectsCount;
			IDictionary<Guid, SubscriptionInformation> cacheSubscriptions = null;
			if (operationInfo.CacheAction != SubscriptionCacheAction.Fix && !DataAccessLayer.TryReadSubscriptionsInformation(mailboxOwner.MailboxInfo.GetDatabaseGuid(), mailboxOwner.MailboxInfo.MailboxGuid, out cacheSubscriptions))
			{
				operationInfo.MarkFailed(Strings.FailureToReadCacheData);
			}
			if (operationInfo.Passed)
			{
				switch (operationInfo.CacheAction)
				{
				case SubscriptionCacheAction.None:
					break;
				case SubscriptionCacheAction.Validate:
					this.ValidateUserCache(mailboxOwner, cacheSubscriptions, operationInfo);
					break;
				case SubscriptionCacheAction.Delete:
					this.DeleteUserCache(mailboxOwner, operationInfo);
					break;
				case SubscriptionCacheAction.Fix:
					this.FixUserCache(mailboxOwner, operationInfo);
					break;
				default:
					throw new InvalidOperationException("Invalid Cache Action request encountered: " + operationInfo.CacheAction);
				}
			}
			int cacheObjectsCount2 = operationInfo.CacheObjectsCount;
			if (cacheObjectsCount == cacheObjectsCount2)
			{
				operationInfo.Add(cacheSubscriptions);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00014BE0 File Offset: 0x00012DE0
		private void ValidateUserCache(ExchangePrincipal mailboxOwner, IDictionary<Guid, SubscriptionInformation> cacheSubscriptions, SubscriptionCacheServer.TestUserCacheOperationInfo operationInfo)
		{
			List<AggregationSubscription> list = null;
			try
			{
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, SubscriptionCacheServer.ClientInfoString, true))
				{
					list = SubscriptionManager.GetAllSubscriptions(mailboxSession, AggregationSubscriptionType.All, false);
				}
			}
			catch (LocalizedException ex)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)417UL, SubscriptionCacheServer.tracer, null, new object[]
				{
					mailboxOwner.LegacyDn,
					"Encountered exception during validation: {0}.",
					ex
				});
				operationInfo.MarkFailed(ex.Message);
				return;
			}
			Guid tenantGuid = mailboxOwner.MailboxInfo.OrganizationId.GetTenantGuid();
			if (cacheSubscriptions == null)
			{
				cacheSubscriptions = new Dictionary<Guid, SubscriptionInformation>(0);
			}
			Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>(list.Capacity);
			foreach (AggregationSubscription aggregationSubscription in list)
			{
				SubscriptionInformation cacheSubscription;
				if (!aggregationSubscription.IsValid)
				{
					string text = string.Format(CultureInfo.InvariantCulture, "ValidateUserCache: Subscription {0} is not valid. Status {1}, Detailed Status {2}.", new object[]
					{
						aggregationSubscription.SubscriptionGuid,
						aggregationSubscription.Status,
						aggregationSubscription.DetailedAggregationStatus
					});
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)418UL, SubscriptionCacheServer.tracer, aggregationSubscription.SubscriptionGuid, mailboxOwner.MailboxInfo.MailboxGuid, text, new object[0]);
					dictionary.Add(aggregationSubscription.SubscriptionGuid, text);
				}
				else if (!cacheSubscriptions.TryGetValue(aggregationSubscription.SubscriptionGuid, out cacheSubscription))
				{
					operationInfo.Add(SubscriptionCacheServer.GetSubscriptionCacheObject(aggregationSubscription, tenantGuid, mailboxOwner.MailboxInfo.MailboxGuid));
				}
				else
				{
					cacheSubscriptions.Remove(aggregationSubscription.SubscriptionGuid);
					SubscriptionCacheObject cacheObject = SubscriptionCacheServer.ValidateSubscription(cacheSubscription, aggregationSubscription, mailboxOwner.MailboxInfo.MailboxGuid);
					operationInfo.Add(cacheObject);
				}
			}
			foreach (SubscriptionInformation subscriptionInformation in cacheSubscriptions.Values)
			{
				string empty = string.Empty;
				dictionary.TryGetValue(subscriptionInformation.SubscriptionGuid, out empty);
				operationInfo.Add(SubscriptionCacheServer.GetSubscriptionCacheObject(subscriptionInformation, SubscriptionCacheObjectState.Unexpected, string.IsNullOrEmpty(empty) ? null : empty));
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00014E88 File Offset: 0x00013088
		private void DeleteUserCache(ExchangePrincipal mailboxOwner, SubscriptionCacheServer.TestUserCacheOperationInfo operationInfo)
		{
			if (DataAccessLayer.TryReportMailboxDeleted(mailboxOwner.MailboxInfo.GetDatabaseGuid(), mailboxOwner.MailboxInfo.MailboxGuid))
			{
				operationInfo.ObjectState = ObjectState.Deleted;
				return;
			}
			operationInfo.MarkFailed(Strings.FailedToDeleteCacheData);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00014EC0 File Offset: 0x000130C0
		private void FixUserCache(ExchangePrincipal mailboxOwner, SubscriptionCacheServer.TestUserCacheOperationInfo operationInfo)
		{
			bool flag;
			if (!DataAccessLayer.TryRebuildMailboxOnDatabase(mailboxOwner.MailboxInfo.GetDatabaseGuid(), mailboxOwner.MailboxInfo.MailboxGuid, out flag))
			{
				operationInfo.MarkFailed(Strings.FailureToRebuildCacheData);
				return;
			}
			if (flag)
			{
				operationInfo.ObjectState = ObjectState.Changed;
			}
			IDictionary<Guid, SubscriptionInformation> cacheSubscriptions;
			if (!DataAccessLayer.TryReadSubscriptionsInformation(mailboxOwner.MailboxInfo.GetDatabaseGuid(), mailboxOwner.MailboxInfo.MailboxGuid, out cacheSubscriptions))
			{
				operationInfo.MarkFailed(Strings.FailureToReadCacheData);
				return;
			}
			operationInfo.Add(cacheSubscriptions);
		}

		// Token: 0x040001A4 RID: 420
		private readonly object syncObject = new object();

		// Token: 0x040001A5 RID: 421
		private static readonly Trace tracer = ExTraceGlobals.SubscriptionCacheRpcServerTracer;

		// Token: 0x040001A6 RID: 422
		private static readonly BinaryFormatter formatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);

		// Token: 0x040001A7 RID: 423
		private static readonly string ClientInfoString = "Client=TransportSync;Action=CacheServer";

		// Token: 0x040001A8 RID: 424
		private static SubscriptionCacheServer cacheServerInstance;

		// Token: 0x040001A9 RID: 425
		private int numberOfCurrentWorkThreads;

		// Token: 0x040001AA RID: 426
		private ManualResetEvent workThreadsEvent;

		// Token: 0x040001AB RID: 427
		private bool stopped;

		// Token: 0x0200003A RID: 58
		private class TestUserCacheOperationInfo
		{
			// Token: 0x060002F4 RID: 756 RVA: 0x00014F74 File Offset: 0x00013174
			internal TestUserCacheOperationInfo(string primarySmtpAddress, SubscriptionCacheAction cacheAction)
			{
				SyncUtilities.ThrowIfArgumentNullOrEmpty("primarySmtpAddress", primarySmtpAddress);
				this.primarySmtpAddress = primarySmtpAddress;
				this.cacheAction = cacheAction;
				this.cacheObjects = new List<SubscriptionCacheObject>(DataAccessLayer.AverageSubscriptionsPerUser);
				this.objectState = ObjectState.Unchanged;
				this.passed = true;
				this.diagnosticsInfo = null;
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x060002F5 RID: 757 RVA: 0x00014FC5 File Offset: 0x000131C5
			internal SubscriptionCacheAction CacheAction
			{
				get
				{
					return this.cacheAction;
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x060002F6 RID: 758 RVA: 0x00014FCD File Offset: 0x000131CD
			internal int CacheObjectsCount
			{
				get
				{
					return this.cacheObjects.Count;
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x060002F7 RID: 759 RVA: 0x00014FDA File Offset: 0x000131DA
			// (set) Token: 0x060002F8 RID: 760 RVA: 0x00014FE2 File Offset: 0x000131E2
			internal ObjectState ObjectState
			{
				get
				{
					return this.objectState;
				}
				set
				{
					this.objectState = value;
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x060002F9 RID: 761 RVA: 0x00014FEB File Offset: 0x000131EB
			internal bool Passed
			{
				get
				{
					return this.passed;
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x060002FA RID: 762 RVA: 0x00014FF3 File Offset: 0x000131F3
			internal string Diagnostics
			{
				get
				{
					return this.diagnosticsInfo;
				}
			}

			// Token: 0x060002FB RID: 763 RVA: 0x00014FFC File Offset: 0x000131FC
			internal ExchangePrincipal GetPrimaryMailboxOwner()
			{
				if (this.passed)
				{
					try
					{
						SmtpAddress smtpAddress = new SmtpAddress(this.primarySmtpAddress);
						ADSessionSettings adSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(smtpAddress.Domain);
						return ExchangePrincipal.FromProxyAddress(adSettings, this.primarySmtpAddress);
					}
					catch (LocalizedException ex)
					{
						ContentAggregationConfig.SyncLogSession.LogError((TSLID)419UL, SubscriptionCacheServer.tracer, (long)this.GetHashCode(), "GetPrimaryMailboxOwner::Exception encountered for mailbox {0}: {1}", new object[]
						{
							this.primarySmtpAddress,
							ex
						});
						this.MarkFailed(ex);
					}
				}
				return null;
			}

			// Token: 0x060002FC RID: 764 RVA: 0x00015098 File Offset: 0x00013298
			internal void Add(IDictionary<Guid, SubscriptionInformation> cacheSubscriptions)
			{
				if (cacheSubscriptions == null)
				{
					return;
				}
				foreach (SubscriptionInformation cacheSubscription in cacheSubscriptions.Values)
				{
					this.cacheObjects.Add(SubscriptionCacheServer.GetSubscriptionCacheObject(cacheSubscription, SubscriptionCacheObjectState.Valid, null));
				}
			}

			// Token: 0x060002FD RID: 765 RVA: 0x000150F8 File Offset: 0x000132F8
			internal void Add(SubscriptionCacheObject cacheObject)
			{
				SyncUtilities.ThrowIfArgumentNull("cacheObject", cacheObject);
				this.cacheObjects.Add(cacheObject);
			}

			// Token: 0x060002FE RID: 766 RVA: 0x00015114 File Offset: 0x00013314
			internal byte[] GetOutputBytes()
			{
				MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
				mdbefPropertyCollection.Add(2835349507U, this.passed ? 0 : 268435456);
				if (!this.passed && !string.IsNullOrEmpty(this.diagnosticsInfo))
				{
					mdbefPropertyCollection.Add(2835415071U, this.diagnosticsInfo);
				}
				int capacity = this.cacheObjects.Count * SubscriptionCacheObject.ApproximateCacheObjectSizeInBytes;
				byte[] array;
				using (MemoryStream memoryStream = new MemoryStream(capacity))
				{
					SubscriptionCacheServer.formatter.Serialize(memoryStream, this.cacheObjects);
					memoryStream.Position = 0L;
					array = new byte[memoryStream.Length];
					memoryStream.Read(array, 0, array.Length);
				}
				mdbefPropertyCollection.Add(2835480834U, array);
				mdbefPropertyCollection.Add(2835546115U, (int)this.objectState);
				return mdbefPropertyCollection.GetBytes();
			}

			// Token: 0x060002FF RID: 767 RVA: 0x00015200 File Offset: 0x00013400
			internal void MarkFailed(string diagnostics)
			{
				this.passed = false;
				this.diagnosticsInfo = diagnostics;
			}

			// Token: 0x06000300 RID: 768 RVA: 0x00015210 File Offset: 0x00013410
			private void MarkFailed(Exception exception)
			{
				this.passed = false;
				this.diagnosticsInfo += exception.Message;
			}

			// Token: 0x040001AC RID: 428
			private readonly string primarySmtpAddress;

			// Token: 0x040001AD RID: 429
			private readonly SubscriptionCacheAction cacheAction;

			// Token: 0x040001AE RID: 430
			private List<SubscriptionCacheObject> cacheObjects;

			// Token: 0x040001AF RID: 431
			private ObjectState objectState;

			// Token: 0x040001B0 RID: 432
			private bool passed;

			// Token: 0x040001B1 RID: 433
			private string diagnosticsInfo;
		}
	}
}
