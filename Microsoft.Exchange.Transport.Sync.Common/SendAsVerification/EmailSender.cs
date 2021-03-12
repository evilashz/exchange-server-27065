using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport.Sync.Common.ExSmtpClient;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.SendAsVerification
{
	// Token: 0x020000A7 RID: 167
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EmailSender : IEmailSender
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x00016624 File Offset: 0x00014824
		internal EmailSender(PimAggregationSubscription subscription, ADUser subscriptionAdUser, ExchangePrincipal subscriptionExchangePrincipal, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("subscriptinAdUser", subscriptionAdUser);
			SyncUtilities.ThrowIfArgumentNull("subscriptionExchangePrincipal", subscriptionExchangePrincipal);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			if (!subscription.SendAsNeedsVerification)
			{
				throw new ArgumentException("subscription is not SendAs verified.  Type: " + subscription.SubscriptionType.ToString(), "subscription");
			}
			this.subscription = subscription;
			this.subscriptionAdUser = subscriptionAdUser;
			this.subscriptionExchangePrincipal = subscriptionExchangePrincipal;
			this.syncLogSession = syncLogSession;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000166AE File Offset: 0x000148AE
		public bool SendAttempted
		{
			get
			{
				return this.sendAttempted;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000166B6 File Offset: 0x000148B6
		public bool SendSuccessful
		{
			get
			{
				return this.SendAttempted && this.sendSuccessful;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000166C8 File Offset: 0x000148C8
		public string MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x000166D0 File Offset: 0x000148D0
		internal static IEmailSender NullEmailSender
		{
			get
			{
				return EmailSender.nullEmailSender;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000166D8 File Offset: 0x000148D8
		public void SendWith(Guid sharedSecret)
		{
			SyncUtilities.ThrowIfGuidEmpty("sharedSecret", sharedSecret);
			this.sendAttempted = true;
			this.RegisterSmtpServicePrincipalName();
			this.messageId = string.Empty;
			string host;
			if (!this.TryPickHub(out host))
			{
				this.sendSuccessful = false;
				return;
			}
			Exception ex = null;
			using (SmtpClient smtpClient = new SmtpClient(host, 25, new SmtpClientTransportSyncDebugOutput(this.syncLogSession)))
			{
				try
				{
					SendAsVerificationExchangeRecipientLookup sendAsVerificationExchangeRecipientLookup = new SendAsVerificationExchangeRecipientLookup();
					string text = sendAsVerificationExchangeRecipientLookup.ExchangeRecipientFor(this.subscriptionAdUser, this.syncLogSession);
					using (SendAsVerificationEmail sendAsVerificationEmail = new SendAsVerificationEmail(this.subscriptionExchangePrincipal, text, this.subscription, sharedSecret, this.syncLogSession))
					{
						smtpClient.AuthCredentials(CredentialCache.DefaultNetworkCredentials);
						smtpClient.From = text;
						smtpClient.To = new string[]
						{
							sendAsVerificationEmail.SubscriptionAddress
						};
						smtpClient.NDRRequired = false;
						smtpClient.DataStream = sendAsVerificationEmail.MessageData;
						smtpClient.Submit();
						this.syncLogSession.LogVerbose((TSLID)31UL, EmailSender.Tracer, (long)this.GetHashCode(), "Email was sent to: [{0}] from: [{1}] message id: [{2}]", new object[]
						{
							sendAsVerificationEmail.SubscriptionAddress,
							text,
							sendAsVerificationEmail.MessageId
						});
						this.messageId = sendAsVerificationEmail.MessageId;
					}
				}
				catch (FailedToGenerateVerificationEmailException ex2)
				{
					ex = ex2;
				}
				catch (UnexpectedSmtpServerResponseException ex3)
				{
					ex = ex3;
				}
				catch (AlreadyConnectedToSMTPServerException ex4)
				{
					ex = ex4;
				}
				catch (AuthFailureException ex5)
				{
					ex = ex5;
				}
				catch (FailedToConnectToSMTPServerException ex6)
				{
					ex = ex6;
				}
				catch (InvalidSmtpServerResponseException ex7)
				{
					ex = ex7;
				}
				catch (MustBeTlsForAuthException ex8)
				{
					ex = ex8;
				}
				catch (NotConnectedToSMTPServerException ex9)
				{
					ex = ex9;
				}
				catch (AuthApiFailureException ex10)
				{
					ex = ex10;
				}
				catch (SocketException ex11)
				{
					ex = ex11;
				}
				catch (IOException ex12)
				{
					if (ex12.InnerException == null || !(ex12.InnerException is SocketException))
					{
						this.syncLogSession.LogError((TSLID)32UL, EmailSender.Tracer, (long)this.GetHashCode(), "Unexpected IOException: {0}.  Rethrowing", new object[]
						{
							ex12
						});
						throw;
					}
					ex = ex12;
				}
			}
			if (ex != null)
			{
				this.syncLogSession.LogError((TSLID)33UL, EmailSender.Tracer, (long)this.GetHashCode(), "An exception was encountered while attempting to send an email: {0}", new object[]
				{
					ex
				});
				CommonLoggingHelper.EventLogger.LogEvent(TransportSyncCommonEventLogConstants.Tuple_VerificationEmailNotSent, ex.GetType().FullName, new object[]
				{
					this.subscriptionAdUser.LegacyExchangeDN,
					ex.ToString()
				});
			}
			this.sendSuccessful = (ex == null);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00016A84 File Offset: 0x00014C84
		private bool TryPickHub(out string hubFqdn)
		{
			bool result = false;
			hubFqdn = string.Empty;
			using (ServerPickerManager serverPickerManager = new ServerPickerManager("Microsoft Exchange Transport Sync SendAsVerification", ServerRole.HubTransport, EmailSender.Tracer))
			{
				PickerServerList pickerServerList = serverPickerManager.GetPickerServerList();
				try
				{
					PickerServer pickerServer = pickerServerList.PickNextUsingRoundRobin();
					if (pickerServer == null)
					{
						this.syncLogSession.LogError((TSLID)34UL, EmailSender.Tracer, (long)this.GetHashCode(), "No hub server found to send email through.", new object[0]);
						FailedToGenerateVerificationEmailException ex = new FailedToGenerateVerificationEmailException();
						CommonLoggingHelper.EventLogger.LogEvent(TransportSyncCommonEventLogConstants.Tuple_VerificationEmailNotSent, ex.GetType().FullName, new object[]
						{
							this.subscriptionAdUser.LegacyExchangeDN,
							ex
						});
						return false;
					}
					hubFqdn = pickerServer.FQDN;
					result = true;
				}
				finally
				{
					pickerServerList.Release();
				}
			}
			return result;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00016B70 File Offset: 0x00014D70
		private void RegisterSmtpServicePrincipalName()
		{
			int num = ServicePrincipalName.RegisterServiceClass("SmtpSvc");
			if (num != 0)
			{
				this.syncLogSession.LogError((TSLID)35UL, EmailSender.Tracer, (long)this.GetHashCode(), "Unable to register SPN.  Status value was: {0}.", new object[]
				{
					num
				});
			}
		}

		// Token: 0x0400027E RID: 638
		private static readonly IEmailSender nullEmailSender = new EmailSender.NullEmailSenderImplementation();

		// Token: 0x0400027F RID: 639
		private static readonly Trace Tracer = ExTraceGlobals.SendAsTracer;

		// Token: 0x04000280 RID: 640
		private PimAggregationSubscription subscription;

		// Token: 0x04000281 RID: 641
		private ADUser subscriptionAdUser;

		// Token: 0x04000282 RID: 642
		private ExchangePrincipal subscriptionExchangePrincipal;

		// Token: 0x04000283 RID: 643
		private string messageId;

		// Token: 0x04000284 RID: 644
		private bool sendAttempted;

		// Token: 0x04000285 RID: 645
		private bool sendSuccessful;

		// Token: 0x04000286 RID: 646
		private SyncLogSession syncLogSession;

		// Token: 0x020000A8 RID: 168
		private class NullEmailSenderImplementation : IEmailSender
		{
			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000420 RID: 1056 RVA: 0x00016BD6 File Offset: 0x00014DD6
			public bool SendAttempted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000421 RID: 1057 RVA: 0x00016BD9 File Offset: 0x00014DD9
			public bool SendSuccessful
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000422 RID: 1058 RVA: 0x00016BDC File Offset: 0x00014DDC
			public string MessageId
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x00016BE3 File Offset: 0x00014DE3
			public void SendWith(Guid sharedSecret)
			{
			}
		}
	}
}
