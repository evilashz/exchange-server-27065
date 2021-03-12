using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000030 RID: 48
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RemoteConduit : IRemoteConduit
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00009F84 File Offset: 0x00008184
		protected RemoteConduit()
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteConduit ctor start");
			ServicePointManager.DefaultConnectionLimit = BrokerConfiguration.ConnectionLimit.Value;
			this.PushSemaphore = new SemaphoreSlim(BrokerConfiguration.MaxConcurrentPushes.Value, BrokerConfiguration.MaxConcurrentPushes.Value);
			this.AcceptSemaphore = new SemaphoreSlim(BrokerConfiguration.MaxConcurrentAccepts.Value, BrokerConfiguration.MaxConcurrentAccepts.Value);
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteConduit ctor end");
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000A010 File Offset: 0x00008210
		public static RemoteConduit Singleton
		{
			get
			{
				return RemoteConduit.singleton;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000A017 File Offset: 0x00008217
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000A021 File Offset: 0x00008221
		internal bool KeepRunning
		{
			get
			{
				return this.keepRunning;
			}
			set
			{
				this.keepRunning = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000A02C File Offset: 0x0000822C
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000A034 File Offset: 0x00008234
		internal HttpListener HttpListener { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000A03D File Offset: 0x0000823D
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000A045 File Offset: 0x00008245
		internal HttpClient HttpClient { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000A04E File Offset: 0x0000824E
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000A056 File Offset: 0x00008256
		internal SemaphoreSlim AcceptSemaphore { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000A05F File Offset: 0x0000825F
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000A067 File Offset: 0x00008267
		internal SemaphoreSlim PushSemaphore { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000A070 File Offset: 0x00008270
		private IGenerator Generator
		{
			get
			{
				IGenerator result;
				if ((result = this.generator) == null)
				{
					result = (this.generator = this.GetGenerator());
				}
				return result;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000A096 File Offset: 0x00008296
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000A09E File Offset: 0x0000829E
		private Thread AcceptThread { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A0A7 File Offset: 0x000082A7
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000A0AF File Offset: 0x000082AF
		private Thread PushThread { get; set; }

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A0B8 File Offset: 0x000082B8
		public static string FindBackEndServer(Guid mailboxGuid, OrganizationId organizationId)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<Guid>(0L, "FindBackEndServer start, looking up = {0}", mailboxGuid);
			ExchangePrincipal exchangePrincipal = RemoteConduit.GetExchangePrincipal(mailboxGuid, organizationId);
			BackEndServer backEndServer = BackEndLocator.GetBackEndServer(exchangePrincipal.MailboxInfo);
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<string>(0L, "FindBackEndServer end, found server = {0}", backEndServer.Fqdn);
			return backEndServer.Fqdn;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A108 File Offset: 0x00008308
		public void Start()
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "Start start");
			this.HttpListener = this.CreateHttpListener();
			this.HttpListener.Start();
			this.KeepRunning = true;
			this.AcceptThread = new Thread(new ThreadStart(this.AcceptRequests));
			this.AcceptThread.Name = "Accept Request Thread";
			this.AcceptThread.IsBackground = true;
			this.AcceptThread.Start();
			this.PushThread = new Thread(new ThreadStart(this.PushNotifications));
			this.PushThread.Name = "Push Notifications Thread";
			this.PushThread.IsBackground = true;
			this.PushThread.Start();
			RemoteMultiQueue.Singleton.StartTimers();
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "Start end");
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A1E8 File Offset: 0x000083E8
		public void Stop()
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "Stop start");
			this.KeepRunning = false;
			RemoteMultiQueue.Singleton.StopTimers();
			this.HttpListener.Stop();
			this.HttpListener.Close();
			Thread.Sleep(TimeSpan.FromSeconds(1.0));
			this.HttpListener = null;
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "Stop end");
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A264 File Offset: 0x00008464
		public void Subscribe(HttpListenerContext context, OrganizationId resolvedOrganizationId)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "Subscribe start");
			SubscriptionMessage subscriptionMessageFromStream = this.GetSubscriptionMessageFromStream(context.Request.InputStream);
			RemoteMessengerFactory.FixupSender(subscriptionMessageFromStream.Subscription, resolvedOrganizationId);
			RemoteCommandStatus remoteCommandStatus = this.GetMessageStatus(subscriptionMessageFromStream);
			if (!this.MailboxIsHostedLocally(subscriptionMessageFromStream.Subscription.Sender.MailboxGuid))
			{
				ExTraceGlobals.RemoteConduitTracer.TraceWarning<Guid>((long)this.GetHashCode(), "Subscribe: Mailbox {0} is not hosted locally", subscriptionMessageFromStream.Subscription.Sender.MailboxGuid);
				remoteCommandStatus = RemoteCommandStatus.MailboxIsNotHostedLocally;
			}
			if (remoteCommandStatus == RemoteCommandStatus.Success)
			{
				this.Generator.Subscribe(subscriptionMessageFromStream.Subscription);
			}
			context.Response.StatusCode = (int)remoteCommandStatus;
			context.Response.Close();
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "Subscribe end");
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A338 File Offset: 0x00008538
		public void Unsubscribe(HttpListenerContext context, OrganizationId resolvedOrganizationId)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "Unsubscribe start");
			SubscriptionMessage subscriptionMessageFromStream = this.GetSubscriptionMessageFromStream(context.Request.InputStream);
			RemoteMessengerFactory.FixupSender(subscriptionMessageFromStream.Subscription, resolvedOrganizationId);
			RemoteCommandStatus remoteCommandStatus = this.GetMessageStatus(subscriptionMessageFromStream);
			if (!this.MailboxIsHostedLocally(subscriptionMessageFromStream.Subscription.Sender.MailboxGuid))
			{
				ExTraceGlobals.RemoteConduitTracer.TraceWarning<Guid>((long)this.GetHashCode(), "Unsubscribe: Mailbox {0} is not hosted locally", subscriptionMessageFromStream.Subscription.Sender.MailboxGuid);
				remoteCommandStatus = RemoteCommandStatus.MailboxIsNotHostedLocally;
			}
			if (remoteCommandStatus == RemoteCommandStatus.Success)
			{
				this.Generator.Unsubscribe(subscriptionMessageFromStream.Subscription);
			}
			context.Response.StatusCode = (int)remoteCommandStatus;
			context.Response.Close();
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "Unsubscribe end");
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000A40C File Offset: 0x0000860C
		public void DeliverNotificationBatch(HttpListenerContext context)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "DeliverNotificationBatch start");
			NotificationMessage notificationMessageFromStream = this.GetNotificationMessageFromStream(context.Request.InputStream);
			RemoteCommandStatus messageStatus = this.GetMessageStatus(notificationMessageFromStream);
			if (messageStatus == RemoteCommandStatus.Success)
			{
				foreach (BrokerNotification brokerNotification in notificationMessageFromStream.Notifications)
				{
					LocalMultiQueue.Singleton.Put(brokerNotification.ConsumerId, brokerNotification);
				}
			}
			context.Response.StatusCode = (int)messageStatus;
			context.Response.Close();
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "DeliverNotificationBatch end");
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000A4C8 File Offset: 0x000086C8
		public void Ping(HttpListenerContext context)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "Ping start");
			string s = string.Format("The RemoteConduit on '{0}' is alive at '{1}'.", Environment.MachineName, ExDateTime.Now);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			context.Response.OutputStream.Write(bytes, 0, bytes.Length);
			context.Response.StatusCode = 200;
			context.Response.Close();
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "Ping end");
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A6B0 File Offset: 0x000088B0
		public async Task<RemoteCommandStatus> ForwardSubscribeAsync(BrokerSubscription brokerSubscription)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "ForwardSubscribeAsync start");
			RemoteMessengerFactory.FixupReceiver(brokerSubscription, this.Generator);
			RemoteMessenger remoteMessenger = RemoteMessengerFactory.CreateForSubscribeRequest(brokerSubscription);
			brokerSubscription.TrimForSubscribeRequest();
			RemoteCommandStatus status = await remoteMessenger.SendMessageAsync(brokerSubscription);
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "ForwardSubscribeAsync end");
			return status;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A840 File Offset: 0x00008A40
		public async Task<RemoteCommandStatus> ForwardUnsubscribeAsync(BrokerSubscription brokerSubscription)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "ForwardUnsubscribeAsync start");
			RemoteMessenger remoteMessenger = RemoteMessengerFactory.CreateForUnsubscribeRequest(brokerSubscription);
			brokerSubscription.TrimForUnsubscribeRequest();
			RemoteCommandStatus status = await remoteMessenger.SendMessageAsync(brokerSubscription);
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "ForwardUnsubscribeAsync end");
			return status;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A8E7 File Offset: 0x00008AE7
		internal void AcceptRequests()
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "AcceptRequests start");
			BrokerDiagnostics.SendWatsonReportOnUnhandledException(delegate
			{
				while (this.KeepRunning)
				{
					this.AcceptSemaphore.Wait();
					Task.Run(delegate()
					{
						BrokerDiagnostics.SendWatsonReportOnUnhandledException(delegate
						{
							this.AcceptAndProcessRequest();
						});
					});
				}
			});
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "AcceptRequests end");
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000AB90 File Offset: 0x00008D90
		internal async void AcceptAndProcessRequest()
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "AcceptAndProcessRequest start");
			try
			{
				HttpListenerContext context = await this.HttpListener.GetContextAsync();
				ExTraceGlobals.RemoteConduitTracer.TraceDebug<Uri, IPrincipal>((long)this.GetHashCode(), "AcceptAndProcessRequest: Received HTTP request {0}, User {1}", context.Request.Url, context.User);
				OrganizationId resolvedOrganizationId = null;
				if (!this.TryResolveOrganizationId(context.Request, out resolvedOrganizationId))
				{
					context.Response.StatusCode = 401;
					context.Response.Close();
				}
				else
				{
					this.ProcessRequest(context, resolvedOrganizationId);
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.RemoteConduitTracer.TraceDebug<string>((long)this.GetHashCode(), "AcceptAndProcessRequest: Exception caught: {0}", ex.ToString());
				if (this.HttpListener != null && this.HttpListener.IsListening)
				{
					throw;
				}
			}
			finally
			{
				this.AcceptSemaphore.Release();
			}
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "AcceptAndProcessRequest end");
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000ABCC File Offset: 0x00008DCC
		internal void ProcessRequest(HttpListenerContext context, OrganizationId resolvedOrganizationId)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "ProcessRequest start");
			string[] array = context.Request.Url.AbsolutePath.Split(new char[]
			{
				'/'
			});
			string text = array[array.Length - 1];
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessRequest: methodName = {0}", text);
			string a;
			if ((a = text) != null)
			{
				if (!(a == "Subscribe"))
				{
					if (!(a == "Unsubscribe"))
					{
						if (!(a == "DeliverNotificationBatch"))
						{
							if (!(a == "Ping"))
							{
								goto IL_B9;
							}
							this.Ping(context);
						}
						else
						{
							this.DeliverNotificationBatch(context);
						}
					}
					else
					{
						this.Unsubscribe(context, resolvedOrganizationId);
					}
				}
				else
				{
					this.Subscribe(context, resolvedOrganizationId);
				}
				ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "ProcessRequest end");
				return;
			}
			IL_B9:
			throw new NotSupportedException();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		internal SubscriptionMessage GetSubscriptionMessageFromStream(Stream inputStream)
		{
			string text = new StreamReader(inputStream).ReadToEnd();
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<string>((long)this.GetHashCode(), "GetSubscriptionMessageFromStream, JSON = '{0}'", text);
			return SubscriptionMessage.FromJson(text);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		internal NotificationMessage GetNotificationMessageFromStream(Stream inputStream)
		{
			string text = new StreamReader(inputStream).ReadToEnd();
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<string>((long)this.GetHashCode(), "GetNotificationMessageFromStream, JSON = '{0}'", text);
			return NotificationMessage.FromJson(text);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000AD1E File Offset: 0x00008F1E
		internal RemoteCommandStatus GetMessageStatus(InterbrokerMessage msg)
		{
			if (msg.Version.IsTooNew(BrokerConfiguration.MaximumProtocolVersion))
			{
				return RemoteCommandStatus.BrokerProtocolIsTooNew;
			}
			if (msg.Version.IsTooOld(BrokerConfiguration.MinimumProtocolVersion))
			{
				return RemoteCommandStatus.BrokerProtocolIsTooOld;
			}
			return RemoteCommandStatus.Success;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000ADD7 File Offset: 0x00008FD7
		internal void PushNotifications()
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "PushNotifications start");
			BrokerDiagnostics.SendWatsonReportOnUnhandledException(delegate
			{
				while (this.KeepRunning)
				{
					ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteConduit.PushNotifications: wait for PushSemaphore");
					this.PushSemaphore.Wait();
					ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteConduit.PushNotifications: PushSemaphore acquired");
					Task.Run(delegate()
					{
						BrokerDiagnostics.SendWatsonReportOnUnhandledException(delegate
						{
							this.PushBatchAndWaitForResponse();
						});
					});
				}
			});
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "PushNotifications end");
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000B118 File Offset: 0x00009318
		internal async void PushBatchAndWaitForResponse()
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "PushBatchAndWaitForResponse start");
			try
			{
				RemoteQueue remoteQueue = await RemoteMultiQueue.Singleton.GetNextQueueAsync();
				NotificationBatch batch = remoteQueue.Get(BrokerConfiguration.PreferredBatchSize.Value);
				RemoteCommandStatus status = await this.PushNotificationBatchAsync(batch);
				RemoteCommandStatus remoteCommandStatus = status;
				if (remoteCommandStatus != RemoteCommandStatus.Success)
				{
					switch (remoteCommandStatus)
					{
					case RemoteCommandStatus.UnsupportedCommand:
						throw new NotSupportedException("An unsupported command was sent - internal error.");
					case RemoteCommandStatus.BrokerProtocolIsTooOld:
						goto IL_1F7;
					case RemoteCommandStatus.BrokerProtocolIsTooNew:
						throw new NotSupportedException("Newer protocols are not supported.");
					}
					ExTraceGlobals.RemoteConduitTracer.TraceError<RemoteCommandStatus>((long)this.GetHashCode(), "PushBatchAndWaitForResponse: Unexpected status: {0}, going to retry.", status);
					RemoteMultiQueue.Singleton.BatchSendFailed(remoteQueue);
				}
				else
				{
					RemoteMultiQueue.Singleton.BatchSendSucceeded(remoteQueue);
				}
				IL_1F7:;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.RemoteConduitTracer.TraceDebug<string>((long)this.GetHashCode(), "PushBatchAndWaitForResponse: Exception caught: {0}", ex.ToString());
				throw;
			}
			finally
			{
				ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteConduit.PushBatchAndWaitForResponse: about to release PushSemaphore");
				this.PushSemaphore.Release();
			}
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "PushBatchAndWaitForResponse end");
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B2A4 File Offset: 0x000094A4
		internal async Task<RemoteCommandStatus> PushNotificationBatchAsync(NotificationBatch batch)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<int, Uri>((long)this.GetHashCode(), "PushNotificationBatchAsync start, sending {0} notifications to {1}", batch.Notifications.Count, batch.RemoteMessenger.Url);
			RemoteCommandStatus status = await batch.RemoteMessenger.SendMessageAsync(batch);
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<RemoteCommandStatus>((long)this.GetHashCode(), "PushNotificationBatchAsync end, status = {0}", status);
			return status;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B2F2 File Offset: 0x000094F2
		protected virtual IGenerator GetGenerator()
		{
			return Microsoft.Exchange.Notifications.Broker.Generator.Singleton;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B2F9 File Offset: 0x000094F9
		protected virtual bool MailboxIsHostedLocally(Guid mailboxGuid)
		{
			return this.Generator.MailboxIsHostedLocally(mailboxGuid);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B308 File Offset: 0x00009508
		private static ExchangePrincipal GetExchangePrincipal(Guid mailboxGuid, OrganizationId organizationId)
		{
			ADSessionSettings adSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
			return ExchangePrincipal.FromMailboxGuid(adSettings, mailboxGuid, null);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B324 File Offset: 0x00009524
		private HttpListener CreateHttpListener()
		{
			string text = string.Format("https://*:444/{0}/", BrokerConfiguration.VdirName.Value);
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<string>((long)this.GetHashCode(), "Start, prefix = {0}", text);
			return new HttpListener
			{
				Prefixes = 
				{
					text
				},
				IgnoreWriteExceptions = true,
				AuthenticationSchemes = (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled ? AuthenticationSchemes.Anonymous : AuthenticationSchemes.Negotiate)
			};
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B3A0 File Offset: 0x000095A0
		private bool TryResolveOrganizationId(HttpListenerRequest httpListenerRequest, out OrganizationId resolveOrganizationId)
		{
			resolveOrganizationId = null;
			if (string.Equals("1", httpListenerRequest.Headers[WellKnownHeader.XIsFromCafe]))
			{
				try
				{
					string text = httpListenerRequest.Headers["Authorization"];
					text = text.Substring(7).Trim();
					Uri targetUri = new Uri(httpListenerRequest.Headers[WellKnownHeader.MsExchProxyUri]);
					string text2;
					OAuthTokenHandler oauthTokenHandler = OAuthTokenHandler.CreateTokenHandler(text, targetUri, out text2);
					OAuthIdentity oauthIdentity = oauthTokenHandler.GetOAuthIdentity();
					resolveOrganizationId = oauthIdentity.OrganizationId;
				}
				catch (Exception arg)
				{
					ExTraceGlobals.RemoteConduitTracer.TraceError<Exception>((long)this.GetHashCode(), "Resolving the oauth token hits {0}", arg);
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x040000D4 RID: 212
		internal const string SubscribeMethodName = "Subscribe";

		// Token: 0x040000D5 RID: 213
		internal const string UnsubscribeMethodName = "Unsubscribe";

		// Token: 0x040000D6 RID: 214
		internal const string DeliverNotificationBatchMethodName = "DeliverNotificationBatch";

		// Token: 0x040000D7 RID: 215
		internal const string PingMethodName = "Ping";

		// Token: 0x040000D8 RID: 216
		private static readonly RemoteConduit singleton = new RemoteConduit();

		// Token: 0x040000D9 RID: 217
		private IGenerator generator;

		// Token: 0x040000DA RID: 218
		private volatile bool keepRunning;
	}
}
