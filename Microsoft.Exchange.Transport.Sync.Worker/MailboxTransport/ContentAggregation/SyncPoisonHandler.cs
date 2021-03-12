using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Worker;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SyncPoisonHandler
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000B08B File Offset: 0x0000928B
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000B092 File Offset: 0x00009292
		internal static bool PoisonDetectionEnabled
		{
			get
			{
				return SyncPoisonHandler.poisonDetectionEnabled;
			}
			set
			{
				SyncPoisonHandler.poisonDetectionEnabled = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000B09A File Offset: 0x0000929A
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000B0A1 File Offset: 0x000092A1
		internal static int PoisonItemThreshold
		{
			get
			{
				return SyncPoisonHandler.poisonItemThreshold;
			}
			set
			{
				SyncPoisonHandler.poisonItemThreshold = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000B0A9 File Offset: 0x000092A9
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000B0B0 File Offset: 0x000092B0
		internal static int PoisonSubscriptionThreshold
		{
			get
			{
				return SyncPoisonHandler.poisonSubscriptionThreshold;
			}
			set
			{
				SyncPoisonHandler.poisonSubscriptionThreshold = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000B0B8 File Offset: 0x000092B8
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000B0BF File Offset: 0x000092BF
		internal static int MaxPoisonousItemsPerSubscriptionThreshold
		{
			get
			{
				return SyncPoisonHandler.maxPoisonousItemsPerSubscriptionThreshold;
			}
			set
			{
				SyncPoisonHandler.maxPoisonousItemsPerSubscriptionThreshold = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000B0C7 File Offset: 0x000092C7
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000B0CE File Offset: 0x000092CE
		internal static TimeSpan PoisonContextExpiry
		{
			get
			{
				return SyncPoisonHandler.poisonContextExpiry;
			}
			set
			{
				SyncPoisonHandler.poisonContextExpiry = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000B0D6 File Offset: 0x000092D6
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000B0DD File Offset: 0x000092DD
		internal static bool TransportSyncEnabled
		{
			get
			{
				return SyncPoisonHandler.transportSyncEnabled;
			}
			set
			{
				SyncPoisonHandler.transportSyncEnabled = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000B0E5 File Offset: 0x000092E5
		internal static bool PoisonDetectionOperational
		{
			get
			{
				return SyncPoisonHandler.TransportSyncEnabled && SyncPoisonHandler.PoisonDetectionEnabled;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B0F8 File Offset: 0x000092F8
		public static void ClearPoisonContext(Guid subscriptionId, SyncPoisonStatus syncPoisonStatus, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionId", subscriptionId);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			syncLogSession.LogDebugging((TSLID)542UL, SyncPoisonHandler.tracer, "Clear Poison Context for Subscription: {0}, with status: {1}", new object[]
			{
				subscriptionId,
				syncPoisonStatus
			});
			if (syncPoisonStatus == SyncPoisonStatus.CleanSubscription)
			{
				return;
			}
			string text = subscriptionId.ToString();
			Exception exception = null;
			try
			{
				lock (SyncPoisonHandler.baseRegistryKeySyncRoot)
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\SyncPoisonContext\\", true))
					{
						if (registryKey != null)
						{
							try
							{
								registryKey.DeleteSubKeyTree(text);
							}
							catch (ArgumentException ex)
							{
								syncLogSession.LogError((TSLID)543UL, SyncPoisonHandler.tracer, "Delete registry entry for subscription {0} failed with error: {1}.", new object[]
								{
									text,
									ex
								});
							}
						}
					}
				}
			}
			catch (SecurityException ex2)
			{
				exception = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				exception = ex3;
			}
			SyncPoisonHandler.HandleRegistryAccessError(exception, syncLogSession);
			lock (SyncPoisonHandler.suspectedSubscriptionListSyncRoot)
			{
				SyncPoisonHandler.suspectedSubscriptionList.Remove(text);
			}
			if (syncPoisonStatus == SyncPoisonStatus.PoisonousItems)
			{
				lock (SyncPoisonHandler.poisonItemListSyncRoot)
				{
					SyncPoisonHandler.poisonItemList.Remove(text);
				}
			}
			lock (SyncPoisonHandler.crashCallstackListSyncRoot)
			{
				SyncPoisonHandler.crashCallstackList.Remove(text);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000B2E8 File Offset: 0x000094E8
		public static bool IsPoisonItem(SyncPoisonContext syncPoisonContext, SyncPoisonStatus syncPoisonStatus, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("syncPoisonContext", syncPoisonContext);
			SyncUtilities.ThrowIfArgumentNull("syncPoisonContext.Item", syncPoisonContext.Item);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			syncLogSession.LogDebugging((TSLID)544UL, SyncPoisonHandler.tracer, "Checking IsPoisonItem for Item: {0}, with status: {1}", new object[]
			{
				syncPoisonContext,
				syncPoisonStatus
			});
			if (!SyncPoisonHandler.PoisonDetectionOperational)
			{
				syncLogSession.LogDebugging((TSLID)545UL, SyncPoisonHandler.tracer, "Poison Detection is not operational, return clean status for the item: {0}", new object[]
				{
					syncPoisonContext
				});
				return false;
			}
			if (syncPoisonStatus != SyncPoisonStatus.PoisonousItems)
			{
				syncLogSession.LogDebugging((TSLID)546UL, SyncPoisonHandler.tracer, "Taking item {0} as clean, since sync poison status ({1}) is not {2}", new object[]
				{
					syncPoisonContext,
					syncPoisonStatus,
					SyncPoisonStatus.PoisonousItems
				});
				return false;
			}
			List<string> list = null;
			lock (SyncPoisonHandler.poisonItemListSyncRoot)
			{
				if (!SyncPoisonHandler.poisonItemList.TryGetValue(syncPoisonContext.SubscriptionId.ToString(), out list))
				{
					syncLogSession.LogDebugging((TSLID)547UL, SyncPoisonHandler.tracer, "No entry found for the item {0} in the poisonous item list", new object[]
					{
						syncPoisonContext
					});
					return false;
				}
			}
			return list.Contains(syncPoisonContext.Item.Key);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000B458 File Offset: 0x00009658
		internal static void SetSyncPoisonContextOnCurrentThread(object syncPoisonContext)
		{
			SyncPoisonHandler.syncPoisonContext = (syncPoisonContext as SyncPoisonContext);
			if (syncPoisonContext != null && SyncPoisonHandler.syncPoisonContext == null)
			{
				throw new ArgumentException("Invalid argument type, should be SyncPoisonContext", "syncPoisonContext");
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000B47F File Offset: 0x0000967F
		internal static void ClearSyncPoisonContextFromCurrentThread()
		{
			SyncPoisonHandler.syncPoisonContext = null;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B488 File Offset: 0x00009688
		internal static void SavePoisonContext(Exception e, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("e", e);
			if (syncLogSession != null)
			{
				syncLogSession.LogDebugging((TSLID)548UL, SyncPoisonHandler.tracer, "Saving Poison Context.", new object[0]);
			}
			if (!SyncPoisonHandler.PoisonDetectionOperational)
			{
				if (syncLogSession != null)
				{
					syncLogSession.LogDebugging((TSLID)549UL, SyncPoisonHandler.tracer, "Poison Detection is not operational, no need to Save Poison Context.", new object[0]);
				}
				return;
			}
			if (SyncPoisonHandler.syncPoisonContext == null)
			{
				if (syncLogSession != null)
				{
					syncLogSession.LogDebugging((TSLID)550UL, SyncPoisonHandler.tracer, "No context information found on the crashing thread", new object[0]);
				}
				return;
			}
			AggregationComponent.EventLogger.LogEvent(TransportSyncWorkerEventLogConstants.Tuple_SubscriptionCausedCrash, null, new object[]
			{
				SyncPoisonHandler.syncPoisonContext.SubscriptionId,
				e
			});
			Exception exception = null;
			try
			{
				string subkey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\SyncPoisonContext\\" + SyncPoisonHandler.syncPoisonContext.SubscriptionId;
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3202755901U);
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(subkey))
				{
					if (SyncPoisonHandler.syncPoisonContext.HasSubscriptionContextOnly)
					{
						SyncPoisonHandler.IncrementCrashCountRegistryValue(registryKey);
					}
					else
					{
						string subkey2 = Guid.NewGuid().ToString();
						using (RegistryKey registryKey2 = registryKey.CreateSubKey(subkey2))
						{
							registryKey2.SetValue("SyncPoisonItem", SyncPoisonHandler.syncPoisonContext.Item.Key);
						}
					}
					registryKey.SetValue("Timestamp", ExDateTime.UtcNow.ToString());
					registryKey.SetValue("CrashCallStack", e.ToString());
				}
			}
			catch (SecurityException ex)
			{
				exception = ex;
			}
			catch (UnauthorizedAccessException ex2)
			{
				exception = ex2;
			}
			SyncPoisonHandler.HandleRegistryAccessError(exception, syncLogSession);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B668 File Offset: 0x00009868
		internal static void Load(SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			syncLogSession.LogDebugging((TSLID)551UL, SyncPoisonHandler.tracer, "Loading Poison Context from Registry ....", new object[0]);
			if (!SyncPoisonHandler.PoisonDetectionEnabled)
			{
				syncLogSession.LogDebugging((TSLID)552UL, SyncPoisonHandler.tracer, "Poison Detection is disabled, no need to Load Poison Context", new object[0]);
				return;
			}
			Exception exception = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\SyncPoisonContext\\", true))
				{
					if (registryKey == null)
					{
						syncLogSession.LogVerbose((TSLID)553UL, SyncPoisonHandler.tracer, "No registry entry found while Loading poison context.", new object[0]);
						return;
					}
					foreach (string text in registryKey.GetSubKeyNames())
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(text, true))
						{
							if (registryKey2 == null)
							{
								syncLogSession.LogError((TSLID)554UL, SyncPoisonHandler.tracer, "Registry entry for Subscription key ({0}) no longer exists, continue with the next one.", new object[]
								{
									text
								});
							}
							else
							{
								object value = registryKey2.GetValue("Timestamp");
								ExDateTime minValue = ExDateTime.MinValue;
								if (value is string && ExDateTime.TryParse((string)value, out minValue))
								{
									if (minValue.Add(SyncPoisonHandler.PoisonContextExpiry) >= ExDateTime.UtcNow)
									{
										string value2 = registryKey2.GetValue("CrashCallStack") as string;
										if (!string.IsNullOrEmpty(value2))
										{
											SyncPoisonHandler.crashCallstackList.Add(text, value2);
										}
										object value3 = registryKey2.GetValue("CrashCount");
										if (value3 is int && (int)value3 >= SyncPoisonHandler.PoisonSubscriptionThreshold)
										{
											SyncPoisonHandler.suspectedSubscriptionList.Add(text, true);
											goto IL_35D;
										}
										SyncPoisonHandler.suspectedSubscriptionList.Add(text, false);
										List<string> list = new List<string>(1);
										Dictionary<string, int> dictionary = new Dictionary<string, int>(1);
										foreach (string text2 in registryKey2.GetSubKeyNames())
										{
											using (RegistryKey registryKey3 = registryKey2.OpenSubKey(text2))
											{
												if (registryKey3 == null)
												{
													syncLogSession.LogError((TSLID)556UL, SyncPoisonHandler.tracer, "Registry entry for item key ({0}) no longer exists, continue will the next one.", new object[]
													{
														text2
													});
												}
												else
												{
													string text3 = registryKey3.GetValue("SyncPoisonItem") as string;
													if (string.IsNullOrEmpty(text3))
													{
														syncLogSession.LogError((TSLID)557UL, SyncPoisonHandler.tracer, "Invalid Value found for SyncPoisonItem: {0}, deleting item Key: {1}", new object[]
														{
															text3,
															text2
														});
														registryKey2.DeleteSubKey(text2, false);
													}
													else
													{
														int num = 0;
														if (dictionary.TryGetValue(text3, out num))
														{
															num++;
														}
														else
														{
															num = 1;
														}
														dictionary[text3] = num;
														if (num >= SyncPoisonHandler.PoisonItemThreshold)
														{
															list.Add(text3);
														}
													}
												}
											}
										}
										if (list.Count >= SyncPoisonHandler.MaxPoisonousItemsPerSubscriptionThreshold)
										{
											syncLogSession.LogError((TSLID)558UL, SyncPoisonHandler.tracer, "Subscription {0} has more than {1} Poisonous items. Making the whole subscription poisonous.", new object[]
											{
												text,
												SyncPoisonHandler.MaxPoisonousItemsPerSubscriptionThreshold
											});
											SyncPoisonHandler.suspectedSubscriptionList[text] = true;
											goto IL_35D;
										}
										if (list.Count > 0)
										{
											SyncPoisonHandler.poisonItemList.Add(text, list);
										}
										goto IL_35D;
									}
								}
								try
								{
									registryKey.DeleteSubKeyTree(text);
								}
								catch (ArgumentException ex)
								{
									syncLogSession.LogError((TSLID)555UL, SyncPoisonHandler.tracer, "Delete registry entry for subscription key {0} failed with error: {1}, continue will the next one.", new object[]
									{
										text,
										ex
									});
								}
							}
						}
						IL_35D:;
					}
				}
			}
			catch (SecurityException ex2)
			{
				exception = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				exception = ex3;
			}
			SyncPoisonHandler.HandleRegistryAccessError(exception, syncLogSession);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000BA9C File Offset: 0x00009C9C
		internal static SyncPoisonStatus GetPoisonStatus(Guid subscriptionId, SyncLogSession syncLogSession, out string poisonCallstack)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionId", subscriptionId);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			poisonCallstack = null;
			syncLogSession.LogDebugging((TSLID)559UL, SyncPoisonHandler.tracer, "Get Poison Status for Subscription: {0}", new object[]
			{
				subscriptionId
			});
			if (!SyncPoisonHandler.PoisonDetectionOperational)
			{
				syncLogSession.LogDebugging((TSLID)560UL, SyncPoisonHandler.tracer, "Poison Detection is not operational, just return Clean Status.", new object[0]);
				return SyncPoisonStatus.CleanSubscription;
			}
			if (SyncPoisonHandler.suspectedSubscriptionList.Count == 0)
			{
				syncLogSession.LogDebugging((TSLID)561UL, SyncPoisonHandler.tracer, "No suspected subscriptions found, just return Clean Status.", new object[0]);
				return SyncPoisonStatus.CleanSubscription;
			}
			string key = subscriptionId.ToString();
			bool flag = false;
			lock (SyncPoisonHandler.suspectedSubscriptionListSyncRoot)
			{
				if (!SyncPoisonHandler.suspectedSubscriptionList.TryGetValue(key, out flag))
				{
					syncLogSession.LogDebugging((TSLID)562UL, SyncPoisonHandler.tracer, "Subscription {0} is not in suspected subscription list, return Clean Status.", new object[]
					{
						subscriptionId
					});
					return SyncPoisonStatus.CleanSubscription;
				}
			}
			if (flag)
			{
				syncLogSession.LogError((TSLID)563UL, SyncPoisonHandler.tracer, "Poisonous Subscription found: {0}", new object[]
				{
					subscriptionId
				});
				lock (SyncPoisonHandler.crashCallstackListSyncRoot)
				{
					if (!SyncPoisonHandler.crashCallstackList.TryGetValue(key, out poisonCallstack))
					{
						syncLogSession.LogDebugging((TSLID)564UL, SyncPoisonHandler.tracer, "Subscription {0} does not have a callstack information, assign to null.", new object[]
						{
							subscriptionId
						});
						poisonCallstack = null;
					}
				}
				return SyncPoisonStatus.PoisonousSubscription;
			}
			lock (SyncPoisonHandler.poisonItemListSyncRoot)
			{
				if (SyncPoisonHandler.poisonItemList.ContainsKey(key))
				{
					syncLogSession.LogError((TSLID)565UL, SyncPoisonHandler.tracer, "Subscription {0} has Poisonous items.", new object[]
					{
						subscriptionId
					});
					return SyncPoisonStatus.PoisonousItems;
				}
			}
			syncLogSession.LogVerbose((TSLID)566UL, SyncPoisonHandler.tracer, "Suspected Subscription {0} found.", new object[]
			{
				subscriptionId
			});
			return SyncPoisonStatus.SuspectedSubscription;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BD10 File Offset: 0x00009F10
		private static void IncrementCrashCountRegistryValue(RegistryKey registryKey)
		{
			int num = 0;
			object value = registryKey.GetValue("CrashCount");
			if (value is int)
			{
				num = (int)value;
			}
			registryKey.SetValue("CrashCount", num + 1);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000BD50 File Offset: 0x00009F50
		private static void HandleRegistryAccessError(Exception exception, SyncLogSession syncLogSession)
		{
			if (exception != null)
			{
				if (syncLogSession != null)
				{
					syncLogSession.LogError((TSLID)567UL, SyncPoisonHandler.tracer, "Registry Access failed with error: {0}", new object[]
					{
						exception
					});
				}
				AggregationComponent.EventLogger.LogEvent(TransportSyncWorkerEventLogConstants.Tuple_RegistryAccessDenied, null, new object[]
				{
					exception
				});
				EventNotificationHelper.PublishTransportEventNotificationItem(TransportSyncNotificationEvent.RegistryAccessDenied.ToString(), exception);
			}
		}

		// Token: 0x04000145 RID: 325
		private const string BaseSyncPoisonLocation = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\SyncPoisonContext\\";

		// Token: 0x04000146 RID: 326
		private const string CrashCountRegistryName = "CrashCount";

		// Token: 0x04000147 RID: 327
		private const string CrashCallstack = "CrashCallStack";

		// Token: 0x04000148 RID: 328
		private const string TimestampRegistryName = "Timestamp";

		// Token: 0x04000149 RID: 329
		private const string SyncPoisonItemRegistryName = "SyncPoisonItem";

		// Token: 0x0400014A RID: 330
		private const int ExpectedPoisonEntries = 0;

		// Token: 0x0400014B RID: 331
		private const int ExpectedPoisonItemsPerSubscription = 1;

		// Token: 0x0400014C RID: 332
		private static readonly Trace tracer = ExTraceGlobals.SyncPoisonHandlerTracer;

		// Token: 0x0400014D RID: 333
		[ThreadStatic]
		private static SyncPoisonContext syncPoisonContext;

		// Token: 0x0400014E RID: 334
		private static Dictionary<string, bool> suspectedSubscriptionList = new Dictionary<string, bool>(0);

		// Token: 0x0400014F RID: 335
		private static Dictionary<string, List<string>> poisonItemList = new Dictionary<string, List<string>>(0);

		// Token: 0x04000150 RID: 336
		private static Dictionary<string, string> crashCallstackList = new Dictionary<string, string>(0);

		// Token: 0x04000151 RID: 337
		private static object baseRegistryKeySyncRoot = new object();

		// Token: 0x04000152 RID: 338
		private static object suspectedSubscriptionListSyncRoot = new object();

		// Token: 0x04000153 RID: 339
		private static object poisonItemListSyncRoot = new object();

		// Token: 0x04000154 RID: 340
		private static object crashCallstackListSyncRoot = new object();

		// Token: 0x04000155 RID: 341
		private static bool poisonDetectionEnabled = false;

		// Token: 0x04000156 RID: 342
		private static bool transportSyncEnabled = false;

		// Token: 0x04000157 RID: 343
		private static int poisonItemThreshold = 2;

		// Token: 0x04000158 RID: 344
		private static int poisonSubscriptionThreshold = 2;

		// Token: 0x04000159 RID: 345
		private static int maxPoisonousItemsPerSubscriptionThreshold = 3;

		// Token: 0x0400015A RID: 346
		private static TimeSpan poisonContextExpiry = TimeSpan.FromDays(2.0);
	}
}
