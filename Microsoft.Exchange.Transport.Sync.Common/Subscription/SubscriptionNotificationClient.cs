using System;
using System.Globalization;
using System.Security;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.SubscriptionNotification;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Notification;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000CB RID: 203
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SubscriptionNotificationClient : ISyncNowNotificationClient
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x0001D691 File Offset: 0x0001B891
		public SubscriptionNotificationClient() : this(CommonLoggingHelper.SyncLogSession, ExTraceGlobals.SubscriptionNotificationClientTracer)
		{
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001D6A4 File Offset: 0x0001B8A4
		protected SubscriptionNotificationClient(SyncLogSession syncLogSession, Trace tracer)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNull("tracer", tracer);
			this.syncLogSession = syncLogSession;
			this.tracer = tracer;
			SubscriptionNotificationRpcMethodCode[] array = (SubscriptionNotificationRpcMethodCode[])Enum.GetValues(typeof(SubscriptionNotificationRpcMethodCode));
			this.methodCodesPermitted = new HashSet<SubscriptionNotificationRpcMethodCode>(array.Length);
			foreach (SubscriptionNotificationRpcMethodCode item in array)
			{
				this.methodCodesPermitted.Add(item);
			}
			HashSet<SubscriptionNotificationRpcMethodCode> hashSet = this.ReadFromRegistryListOfBlockedMethodCodes();
			foreach (SubscriptionNotificationRpcMethodCode item2 in hashSet)
			{
				this.methodCodesPermitted.Remove(item2);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001D778 File Offset: 0x0001B978
		public static SubscriptionNotificationClient DefaultInstance
		{
			get
			{
				if (SubscriptionNotificationClient.defaultInstance == null)
				{
					lock (SubscriptionNotificationClient.defaultInstanceSyncLock)
					{
						if (SubscriptionNotificationClient.defaultInstance == null)
						{
							SubscriptionNotificationClient.defaultInstance = new SubscriptionNotificationClient();
						}
					}
				}
				return SubscriptionNotificationClient.defaultInstance;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		internal bool NotifySubscriptionAdded(ISyncWorkerData subscription, string mailboxServer)
		{
			return this.TryNotifyMailboxServerOfSubscriptionOperation(subscription, mailboxServer, SubscriptionNotificationRpcMethodCode.SubscriptionAdd);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001D7DB File Offset: 0x0001B9DB
		internal bool NotifySubscriptionUpdated(ISyncWorkerData subscription, string mailboxServer)
		{
			return this.TryNotifyMailboxServerOfSubscriptionOperation(subscription, mailboxServer, SubscriptionNotificationRpcMethodCode.SubscriptionUpdated);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001D7E6 File Offset: 0x0001B9E6
		internal bool NotifySubscriptionDeleted(ISyncWorkerData subscription, string mailboxServer)
		{
			return this.TryNotifyMailboxServerOfSubscriptionOperation(subscription, mailboxServer, SubscriptionNotificationRpcMethodCode.SubscriptionDelete);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001D7F1 File Offset: 0x0001B9F1
		internal bool NotifySubscriptionSyncNowNeeded(ISyncWorkerData subscription, string mailboxServer)
		{
			return this.TryNotifyMailboxServerOfSubscriptionOperation(subscription, mailboxServer, SubscriptionNotificationRpcMethodCode.SubscriptionSyncNowNeeded);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001D7FC File Offset: 0x0001B9FC
		void ISyncNowNotificationClient.NotifyOWALogonTriggeredSyncNowNeeded(Guid mailboxGuid, Guid mdbGuid, string mailboxServer)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("mdbGuid", mdbGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxServer", mailboxServer);
			this.TryNotifyMailboxServer(mailboxGuid, mdbGuid, mailboxServer, SubscriptionNotificationRpcMethodCode.OWALogonTriggeredSyncNow);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001D82A File Offset: 0x0001BA2A
		void ISyncNowNotificationClient.NotifyOWARefreshButtonTriggeredSyncNowNeeded(Guid mailboxGuid, Guid mdbGuid, string mailboxServer)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("mdbGuid", mdbGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxServer", mailboxServer);
			this.TryNotifyMailboxServer(mailboxGuid, mdbGuid, mailboxServer, SubscriptionNotificationRpcMethodCode.OWARefreshButtonTriggeredSyncNow);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001D859 File Offset: 0x0001BA59
		void ISyncNowNotificationClient.NotifyOWAActivityTriggeredSyncNowNeeded(Guid mailboxGuid, Guid mdbGuid, string mailboxServer)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("mdbGuid", mdbGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxServer", mailboxServer);
			this.TryNotifyMailboxServer(mailboxGuid, mdbGuid, mailboxServer, SubscriptionNotificationRpcMethodCode.OWAActivityTriggeredSyncNow);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001D887 File Offset: 0x0001BA87
		internal bool NotifySubscriptionUpdatedAndSyncNowNeeded(ISyncWorkerData subscription, string mailboxServer)
		{
			return this.TryNotifyMailboxServerOfSubscriptionOperation(subscription, mailboxServer, SubscriptionNotificationRpcMethodCode.SubscriptionUpdatedAndSyncNowNeeded);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001D894 File Offset: 0x0001BA94
		protected virtual HashSet<SubscriptionNotificationRpcMethodCode> ReadFromRegistryListOfBlockedMethodCodes()
		{
			HashSet<SubscriptionNotificationRpcMethodCode> hashSet = new HashSet<SubscriptionNotificationRpcMethodCode>();
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SubscriptionNotificationClient.baseRegistryLocation, false))
				{
					if (registryKey == null)
					{
						this.syncLogSession.LogDebugging((TSLID)1338UL, "Registry {0} not found.", new object[]
						{
							SubscriptionNotificationClient.baseRegistryLocation
						});
					}
					else
					{
						string[] subKeyNames = registryKey.GetSubKeyNames();
						string[] array = subKeyNames;
						int i = 0;
						while (i < array.Length)
						{
							string text = array[i];
							SubscriptionNotificationRpcMethodCode item;
							try
							{
								item = (SubscriptionNotificationRpcMethodCode)Enum.Parse(typeof(SubscriptionNotificationRpcMethodCode), text, true);
							}
							catch (ArgumentException ex)
							{
								this.syncLogSession.LogError((TSLID)1468UL, "Ignoring subKey {0}\\{1}: {2}", new object[]
								{
									SubscriptionNotificationClient.baseRegistryLocation,
									text,
									ex
								});
								goto IL_164;
							}
							goto IL_BF;
							IL_164:
							i++;
							continue;
							IL_BF:
							using (RegistryKey registryKey2 = registryKey.OpenSubKey(text, false))
							{
								object value = registryKey2.GetValue(SubscriptionNotificationClient.disabledKeyName);
								bool flag;
								if (value != null && value is string && bool.TryParse((string)value, out flag))
								{
									this.syncLogSession.LogVerbose((TSLID)1469UL, "Found {0}\\{1}:{2}", new object[]
									{
										SubscriptionNotificationClient.baseRegistryLocation,
										SubscriptionNotificationClient.disabledKeyName,
										flag
									});
									if (flag)
									{
										hashSet.Remove(item);
										hashSet.Add(item);
									}
									else
									{
										hashSet.Remove(item);
									}
								}
							}
							goto IL_164;
						}
					}
				}
			}
			catch (SecurityException ex2)
			{
				this.syncLogSession.LogError((TSLID)1470UL, "Failed to read registry {0} due to {1}", new object[]
				{
					SubscriptionNotificationClient.baseRegistryLocation,
					ex2
				});
			}
			catch (UnauthorizedAccessException ex3)
			{
				this.syncLogSession.LogError((TSLID)1471UL, "Failed to read registry {0} due to {1}", new object[]
				{
					SubscriptionNotificationClient.baseRegistryLocation,
					ex3
				});
			}
			return hashSet;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001DB14 File Offset: 0x0001BD14
		protected virtual byte[] MakeRpcCallToServer(string mailboxServer, Guid identity, SubscriptionNotificationRpcMethodCode method, byte[] inputArgs)
		{
			byte[] array = null;
			for (int i = 0; i < 2; i++)
			{
				try
				{
					using (SubscriptionNotificationRpcClient subscriptionNotificationRpcClient = new SubscriptionNotificationRpcClient(mailboxServer))
					{
						this.syncLogSession.LogVerbose((TSLID)108UL, this.tracer, "Notify identity {0} operation {1} to server {2}.", new object[]
						{
							identity,
							method,
							mailboxServer
						});
						array = subscriptionNotificationRpcClient.InvokeSubscriptionNotificationEndPoint(0, inputArgs);
						if (array == null)
						{
							this.syncLogSession.LogError((TSLID)109UL, this.tracer, "Notification to server {0} returned null outputArgs.", new object[]
							{
								mailboxServer
							});
						}
						break;
					}
				}
				catch (RpcException ex)
				{
					this.syncLogSession.LogError((TSLID)187UL, this.tracer, "Notification to server {0} failed. Error Code:{1} with RpcException:{2}.", new object[]
					{
						mailboxServer,
						ex.ErrorCode,
						ex.Message
					});
				}
			}
			return array;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		private static byte[] GetSubscriptionNotificationInputBytes(ISyncWorkerData subscription, SubscriptionNotificationRpcMethodCode methodCode)
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			mdbefPropertyCollection[2684420099U] = (int)methodCode;
			mdbefPropertyCollection[2684551240U] = subscription.SubscriptionGuid;
			mdbefPropertyCollection[2684616735U] = subscription.UserExchangeMailboxSmtpAddress;
			return mdbefPropertyCollection.GetBytes();
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001DC80 File Offset: 0x0001BE80
		private static byte[] GetMailboxNotificationInputBytes(Guid mailboxGuid, Guid mdbGuid, SubscriptionNotificationRpcMethodCode methodCode)
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			mdbefPropertyCollection[2684420099U] = (int)methodCode;
			mdbefPropertyCollection[2684682312U] = mailboxGuid;
			mdbefPropertyCollection[2684747848U] = mdbGuid;
			return mdbefPropertyCollection.GetBytes();
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001DCCC File Offset: 0x0001BECC
		private bool TryNotifyMailboxServerOfSubscriptionOperation(ISyncWorkerData subscription, string mailboxServer, SubscriptionNotificationRpcMethodCode method)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxServer", mailboxServer);
			if (!this.methodCodesPermitted.Contains(method))
			{
				this.syncLogSession.LogVerbose((TSLID)1472UL, this.tracer, "Notify subscription operation {0} for subscription: {1} on mailbox server {2} has been mapped to SyncNow as per configuration.", new object[]
				{
					method,
					subscription.SubscriptionGuid,
					mailboxServer
				});
				method = SubscriptionNotificationRpcMethodCode.SubscriptionSyncNowNeeded;
			}
			this.syncLogSession.LogDebugging((TSLID)107UL, this.tracer, "Notify subscription operation {0} for subscription: '{1}' on mailbox server '{2}'.", new object[]
			{
				method,
				subscription.SubscriptionGuid,
				mailboxServer
			});
			byte[] subscriptionNotificationInputBytes = SubscriptionNotificationClient.GetSubscriptionNotificationInputBytes(subscription, method);
			return this.MakeRpcCallAndProcessResult(mailboxServer, subscription.SubscriptionGuid, method, subscriptionNotificationInputBytes);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001DD9C File Offset: 0x0001BF9C
		private bool MakeRpcCallAndProcessResult(string mailboxServer, Guid identity, SubscriptionNotificationRpcMethodCode method, byte[] inputArgs)
		{
			byte[] array = this.MakeRpcCallToServer(mailboxServer, identity, method, inputArgs);
			if (array == null)
			{
				return false;
			}
			MdbefPropertyCollection mdbefPropertyCollection = MdbefPropertyCollection.Create(array, 0, array.Length);
			object obj;
			if (mdbefPropertyCollection.TryGetValue(2835349507U, out obj) && obj is int)
			{
				SubscriptionNotificationResult subscriptionNotificationResult = (SubscriptionNotificationResult)((int)obj);
				bool flag = subscriptionNotificationResult == SubscriptionNotificationResult.Success;
				if (flag)
				{
					this.syncLogSession.LogDebugging((TSLID)199UL, this.tracer, "Mailbox server '{0}' returned succesfully for notification of identity '{1}', operation: {2}, result: {3}.", new object[]
					{
						mailboxServer,
						identity,
						method,
						subscriptionNotificationResult
					});
				}
				else
				{
					this.syncLogSession.LogError((TSLID)200UL, this.tracer, "Mailbox server '{0}' returned failure for notification of identity '{1}', operation {2}, result: {3}.", new object[]
					{
						mailboxServer,
						identity,
						method,
						subscriptionNotificationResult
					});
				}
				return flag;
			}
			this.syncLogSession.LogError((TSLID)201UL, this.tracer, "Mailbox server '{0}' did not return a result code on notify operation {1} for identity '{2}'.", new object[]
			{
				mailboxServer,
				method,
				identity
			});
			return false;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001DEDC File Offset: 0x0001C0DC
		private bool TryNotifyMailboxServer(Guid mailboxGuid, Guid mdbGuid, string mailboxServer, SubscriptionNotificationRpcMethodCode method)
		{
			if (!this.methodCodesPermitted.Contains(method))
			{
				this.syncLogSession.LogVerbose((TSLID)88UL, this.tracer, "Operation {0} not permitted for mailbox: {1} on mailbox server {2} has been mapped to SyncNow as per configuration.", new object[]
				{
					method,
					mailboxGuid,
					mailboxServer
				});
				return false;
			}
			this.syncLogSession.LogDebugging((TSLID)89UL, this.tracer, "Notify operation {0} for mailbox: '{1}' on mailbox server '{2}'.", new object[]
			{
				method,
				mailboxGuid,
				mailboxServer
			});
			byte[] mailboxNotificationInputBytes = SubscriptionNotificationClient.GetMailboxNotificationInputBytes(mailboxGuid, mdbGuid, method);
			return this.MakeRpcCallAndProcessResult(mailboxServer, mailboxGuid, method, mailboxNotificationInputBytes);
		}

		// Token: 0x04000339 RID: 825
		private static readonly string baseRegistryLocation = string.Format(CultureInfo.InvariantCulture, "SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\Transport\\Sync\\Notifications\\", new object[]
		{
			"v15"
		});

		// Token: 0x0400033A RID: 826
		private static readonly string disabledKeyName = "Disabled";

		// Token: 0x0400033B RID: 827
		private static readonly object defaultInstanceSyncLock = new object();

		// Token: 0x0400033C RID: 828
		private readonly SyncLogSession syncLogSession;

		// Token: 0x0400033D RID: 829
		private readonly Trace tracer;

		// Token: 0x0400033E RID: 830
		private readonly HashSet<SubscriptionNotificationRpcMethodCode> methodCodesPermitted;

		// Token: 0x0400033F RID: 831
		private static SubscriptionNotificationClient defaultInstance;
	}
}
