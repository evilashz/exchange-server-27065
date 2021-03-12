using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AppendOnlyActivityLog : IActivityLog
	{
		// Token: 0x06000310 RID: 784 RVA: 0x00017856 File Offset: 0x00015A56
		public AppendOnlyActivityLog(MailboxSession mailboxSession)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00017870 File Offset: 0x00015A70
		public bool IsGroup()
		{
			return this.mailboxSession.MailboxOwner.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00017890 File Offset: 0x00015A90
		public void Append(IEnumerable<Activity> activities)
		{
			Util.ThrowOnNullArgument(activities, "activities");
			MapiFastTransferStream mapiFastTransferStream = null;
			StoreSession storeSession = this.mailboxSession;
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
				mapiFastTransferStream = (MapiFastTransferStream)this.mailboxSession.Mailbox.MapiStore.OpenProperty(PropTag.FastTransfer, InterfaceIds.IFastTransferStream, 0, OpenPropertyFlags.None);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToWriteActivityLog, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Append", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToWriteActivityLog, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Append", new object[0]),
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
			using (mapiFastTransferStream)
			{
				StoreSession storeSession2 = this.mailboxSession;
				bool flag2 = false;
				try
				{
					if (storeSession2 != null)
					{
						storeSession2.BeginMapiCall();
						storeSession2.BeginServerHealthCall();
						flag2 = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					mapiFastTransferStream.Configure(AppendOnlyActivityLog.FxConfigurationProps);
				}
				catch (MapiPermanentException ex3)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToWriteActivityLog, ex3, storeSession2, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Append", new object[0]),
						ex3
					});
				}
				catch (MapiRetryableException ex4)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToWriteActivityLog, ex4, storeSession2, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Append", new object[0]),
						ex4
					});
				}
				finally
				{
					try
					{
						if (storeSession2 != null)
						{
							storeSession2.EndMapiCall();
							if (flag2)
							{
								storeSession2.EndServerHealthCall();
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
				using (IFastTransferProcessor<FastTransferDownloadContext> fastTransferProcessor = new FastTransferMessageIterator(new ActivityMessageIterator(activities), FastTransferCopyMessagesFlag.None, true))
				{
					using (FastTransferDownloadContext fastTransferDownloadContext = FastTransferDownloadContext.CreateForDownload(FastTransferSendOption.Unicode, 1U, CTSGlobals.AsciiEncoding, NullResourceTracker.Instance, IncludeAllPropertyFilterFactory.Instance, false))
					{
						fastTransferDownloadContext.PushInitial(fastTransferProcessor);
						byte[] array = new byte[30720];
						ArraySegment<byte> buffer = new ArraySegment<byte>(array);
						while (fastTransferDownloadContext.State != FastTransferState.Done)
						{
							if (fastTransferDownloadContext.State == FastTransferState.Error)
							{
								throw new InvalidOperationException("FastTransferDownloadContext failed during activity log upload.");
							}
							int nextBuffer = fastTransferDownloadContext.GetNextBuffer(buffer);
							if (nextBuffer > 0)
							{
								StoreSession storeSession3 = this.mailboxSession;
								bool flag3 = false;
								try
								{
									if (storeSession3 != null)
									{
										storeSession3.BeginMapiCall();
										storeSession3.BeginServerHealthCall();
										flag3 = true;
									}
									if (StorageGlobals.MapiTestHookBeforeCall != null)
									{
										StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
									}
									mapiFastTransferStream.Upload(new ArraySegment<byte>(array, 0, nextBuffer));
								}
								catch (MapiPermanentException ex5)
								{
									throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToWriteActivityLog, ex5, storeSession3, this, "{0}. MapiException = {1}.", new object[]
									{
										string.Format("Append", new object[0]),
										ex5
									});
								}
								catch (MapiRetryableException ex6)
								{
									throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToWriteActivityLog, ex6, storeSession3, this, "{0}. MapiException = {1}.", new object[]
									{
										string.Format("Append", new object[0]),
										ex6
									});
								}
								finally
								{
									try
									{
										if (storeSession3 != null)
										{
											storeSession3.EndMapiCall();
											if (flag3)
											{
												storeSession3.EndServerHealthCall();
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
						StoreSession storeSession4 = this.mailboxSession;
						bool flag4 = false;
						try
						{
							if (storeSession4 != null)
							{
								storeSession4.BeginMapiCall();
								storeSession4.BeginServerHealthCall();
								flag4 = true;
							}
							if (StorageGlobals.MapiTestHookBeforeCall != null)
							{
								StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
							}
							mapiFastTransferStream.Flush();
						}
						catch (MapiPermanentException ex7)
						{
							throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToWriteActivityLog, ex7, storeSession4, this, "{0}. MapiException = {1}.", new object[]
							{
								string.Format("Append", new object[0]),
								ex7
							});
						}
						catch (MapiRetryableException ex8)
						{
							throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToWriteActivityLog, ex8, storeSession4, this, "{0}. MapiException = {1}.", new object[]
							{
								string.Format("Append", new object[0]),
								ex8
							});
						}
						finally
						{
							try
							{
								if (storeSession4 != null)
								{
									storeSession4.EndMapiCall();
									if (flag4)
									{
										storeSession4.EndServerHealthCall();
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
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00018710 File Offset: 0x00016910
		public IEnumerable<Activity> Query()
		{
			MapiFastTransferStream fxStream = null;
			StoreSession storeSession = this.mailboxSession;
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
				fxStream = (MapiFastTransferStream)this.mailboxSession.Mailbox.MapiStore.OpenProperty(PropTag.FastTransfer, InterfaceIds.IFastTransferStream, 1, OpenPropertyFlags.None);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToReadActivityLog, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Query", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToReadActivityLog, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Query", new object[0]),
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
			using (fxStream)
			{
				StoreSession storeSession2 = this.mailboxSession;
				bool flag2 = false;
				try
				{
					if (storeSession2 != null)
					{
						storeSession2.BeginMapiCall();
						storeSession2.BeginServerHealthCall();
						flag2 = true;
					}
					if (StorageGlobals.MapiTestHookBeforeCall != null)
					{
						StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
					}
					fxStream.Configure(AppendOnlyActivityLog.FxConfigurationProps);
				}
				catch (MapiPermanentException ex3)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToReadActivityLog, ex3, storeSession2, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Query", new object[0]),
						ex3
					});
				}
				catch (MapiRetryableException ex4)
				{
					throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToReadActivityLog, ex4, storeSession2, this, "{0}. MapiException = {1}.", new object[]
					{
						string.Format("Query", new object[0]),
						ex4
					});
				}
				finally
				{
					try
					{
						if (storeSession2 != null)
						{
							storeSession2.EndMapiCall();
							if (flag2)
							{
								storeSession2.EndServerHealthCall();
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
				Queue<Activity> activityBuffer = new Queue<Activity>();
				bool resetActivityFound = false;
				Action<Activity> desearializationDelegate = delegate(Activity activity)
				{
					if (AppendOnlyActivityLog.IsResetActivity(activity))
					{
						resetActivityFound = true;
					}
					if (!resetActivityFound)
					{
						activityBuffer.Enqueue(activity);
					}
				};
				using (IFastTransferProcessor<FastTransferUploadContext> processor = new FastTransferMessageIterator(new ActivityMessageIteratorClient(desearializationDelegate), true))
				{
					using (FastTransferUploadContext context = new FastTransferUploadContext(CTSGlobals.AsciiEncoding, NullResourceTracker.Instance, IncludeAllPropertyFilterFactory.Instance, false))
					{
						context.PushInitial(processor);
						byte[] buffer = null;
						while (!resetActivityFound)
						{
							StoreSession storeSession3 = this.mailboxSession;
							bool flag3 = false;
							try
							{
								if (storeSession3 != null)
								{
									storeSession3.BeginMapiCall();
									storeSession3.BeginServerHealthCall();
									flag3 = true;
								}
								if (StorageGlobals.MapiTestHookBeforeCall != null)
								{
									StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
								}
								buffer = fxStream.Download();
							}
							catch (MapiPermanentException ex5)
							{
								throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToReadActivityLog, ex5, storeSession3, this, "{0}. MapiException = {1}.", new object[]
								{
									string.Format("Query", new object[0]),
									ex5
								});
							}
							catch (MapiRetryableException ex6)
							{
								throw StorageGlobals.TranslateMapiException(ServerStrings.FailedToReadActivityLog, ex6, storeSession3, this, "{0}. MapiException = {1}.", new object[]
								{
									string.Format("Query", new object[0]),
									ex6
								});
							}
							finally
							{
								try
								{
									if (storeSession3 != null)
									{
										storeSession3.EndMapiCall();
										if (flag3)
										{
											storeSession3.EndServerHealthCall();
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
							if (buffer == null)
							{
								break;
							}
							context.PutNextBuffer(new ArraySegment<byte>(buffer));
							if (context.State == FastTransferState.Error)
							{
								new CorruptDataException(ServerStrings.FailedToReadActivityLog);
							}
							while (activityBuffer.Count > 0)
							{
								yield return activityBuffer.Dequeue();
							}
						}
						context.Flush();
						if (context.State == FastTransferState.Error)
						{
							new CorruptDataException(ServerStrings.FailedToReadActivityLog);
						}
						while (activityBuffer.Count > 0)
						{
							yield return activityBuffer.Dequeue();
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00018730 File Offset: 0x00016930
		public void Reset()
		{
			Activity[] activities = new Activity[]
			{
				AppendOnlyActivityLog.CreateResetActivity()
			};
			this.Append(activities);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00018755 File Offset: 0x00016955
		internal static bool IsResetActivity(Activity activity)
		{
			return activity.Id == ActivityId.Min && activity.ClientId == ClientId.Exchange && activity.ClientSessionId == AppendOnlyActivityLog.ResetActivityClientSessionId;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00018784 File Offset: 0x00016984
		internal static Activity CreateResetActivity()
		{
			MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
			memoryPropertyBag.SetProperty(ActivitySchema.ActivityId, ActivityId.Min);
			memoryPropertyBag.SetProperty(ActivitySchema.ClientId, ClientId.Exchange.ToInt());
			memoryPropertyBag.SetProperty(ActivitySchema.SessionId, AppendOnlyActivityLog.ResetActivityClientSessionId);
			memoryPropertyBag.SetProperty(ActivitySchema.TimeStamp, ExDateTime.UtcNow);
			return new Activity(memoryPropertyBag);
		}

		// Token: 0x040000DF RID: 223
		private const int UploadBufferSize = 30720;

		// Token: 0x040000E0 RID: 224
		private static readonly PropValue[] FxConfigurationProps = new PropValue[]
		{
			new PropValue(PropTag.InstanceGuid, MapiFastTransferStream.WellKnownIds.InferenceLog)
		};

		// Token: 0x040000E1 RID: 225
		private static readonly Guid ResetActivityClientSessionId = Guid.Parse("21279651-118a-488e-9248-ac62188f0db0");

		// Token: 0x040000E2 RID: 226
		private readonly MailboxSession mailboxSession;
	}
}
