using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200002F RID: 47
	internal class ApnsFeedbackStream
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x000075C2 File Offset: 0x000057C2
		public ApnsFeedbackStream(string appId, ApnsChannelSettings settings) : this(appId, settings, ExTraceGlobals.ApnsPublisherTracer)
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000075D1 File Offset: 0x000057D1
		public ApnsFeedbackStream(string appId, ApnsChannelSettings settings, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("appId", appId);
			ArgumentValidator.ThrowIfNull("settings", settings);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.appId = appId;
			this.config = settings;
			this.tracer = tracer;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007884 File Offset: 0x00005A84
		public IEnumerable<ApnsFeedbackResponse> ReadFeedbackResponses()
		{
			this.tracer.TraceDebug((long)this.GetHashCode(), "[ReadFeedbackResponses] Requesting feedback from APNs");
			PushNotificationsCrimsonEvents.ApnsFeedbackChannelConsuming.Log<string>(this.appId);
			ApnsCertProvider certProvider = this.CreateCertProvider();
			X509Certificate2 cert = certProvider.LoadCertificate(this.config.CertificateThumbprint, this.config.CertificateThumbprintFallback);
			ApnsFeedbackStream.ApnsFeedbackClient feedbackClient = new ApnsFeedbackStream.ApnsFeedbackClient();
			feedbackClient.TcpClient = this.CreateTcpClient();
			this.Connect(feedbackClient);
			feedbackClient.SslStream = this.CreateSslStream(feedbackClient.TcpClient, certProvider, cert);
			this.Authenticate(feedbackClient, cert);
			for (;;)
			{
				ApnsFeedbackResponse response = this.Read(feedbackClient);
				if (response == null)
				{
					break;
				}
				yield return response;
			}
			yield break;
			yield break;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000078A1 File Offset: 0x00005AA1
		protected virtual ApnsCertProvider CreateCertProvider()
		{
			return new ApnsCertProvider(new ApnsCertStore(new X509Store(StoreName.My, StoreLocation.LocalMachine)), this.config.IgnoreCertificateErrors, this.tracer, this.appId);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000078CB File Offset: 0x00005ACB
		protected virtual ApnsTcpClient CreateTcpClient()
		{
			return new ApnsTcpClient(new TcpClient());
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000078E8 File Offset: 0x00005AE8
		protected virtual ApnsSslStream CreateSslStream(ApnsTcpClient tcpClient, ApnsCertProvider certProvider, X509Certificate2 cert)
		{
			return new ApnsSslStream(new SslStream(tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(certProvider.ValidateCertificate), (object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers) => cert));
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000792C File Offset: 0x00005B2C
		private void Connect(ApnsFeedbackStream.ApnsFeedbackClient feedbackClient)
		{
			try
			{
				feedbackClient.TcpClient.Connect(this.config.FeedbackHost, this.config.FeedbackPort);
			}
			catch (SocketException exception)
			{
				throw this.HandleException("Connect", exception);
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000797C File Offset: 0x00005B7C
		private void Authenticate(ApnsFeedbackStream.ApnsFeedbackClient feedbackClient, X509Certificate2 cert)
		{
			try
			{
				IAsyncResult asyncResult = feedbackClient.SslStream.BeginAuthenticateAsClient(this.config.FeedbackHost, new X509Certificate2Collection
				{
					cert
				}, SslProtocols.Default, false, null, null);
				if (!asyncResult.AsyncWaitHandle.WaitOne(this.config.ConnectTotalTimeout))
				{
					feedbackClient.AuthenticateAsyncResult = asyncResult;
					throw this.HandleException("Authenticate", new AuthenticationException(Strings.ApnsChannelAuthenticationTimeout));
				}
				feedbackClient.SslStream.EndAuthenticateAsClient(asyncResult);
			}
			catch (AuthenticationException exception)
			{
				throw this.HandleException("Authenticate", exception);
			}
			catch (IOException exception2)
			{
				throw this.HandleException("Authenticate", exception2);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007A3C File Offset: 0x00005C3C
		private ApnsFeedbackResponse Read(ApnsFeedbackStream.ApnsFeedbackClient feedbackClient)
		{
			byte[] array = new byte[38];
			int num = 0;
			int num2 = 38;
			bool flag = false;
			ApnsFeedbackResponse result;
			try
			{
				while (!flag)
				{
					int num3 = feedbackClient.SslStream.Read(array, num, num2);
					if (num3 == 0 && num == 0)
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "[Read] No more feedback available from APNs at this moment");
						PushNotificationsCrimsonEvents.ApnsFeedbackChannelDone.Log<string>(this.appId);
						return null;
					}
					if (num3 == 0)
					{
						throw this.HandleException("Read", new ApnsFeedbackException(Strings.ApnsFeedbackResponseInvalidLength(num)));
					}
					num += num3;
					num2 -= num3;
					flag = (num2 == 0);
				}
				ApnsFeedbackResponse apnsFeedbackResponse = ApnsFeedbackResponse.FromApnsFormat(array);
				this.tracer.TraceDebug<ApnsFeedbackResponse>((long)this.GetHashCode(), "[Read] Feedback response from APNs: {0}", apnsFeedbackResponse);
				PushNotificationsCrimsonEvents.ApnsFeedbackChannelResponse.Log<string, string, ApnsFeedbackResponse>(this.appId, string.Empty, apnsFeedbackResponse);
				result = apnsFeedbackResponse;
			}
			catch (IOException exception)
			{
				throw this.HandleException("Read", exception);
			}
			return result;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007B2C File Offset: 0x00005D2C
		private Exception HandleException(string header, Exception exception)
		{
			this.tracer.TraceError<string, string>((long)this.GetHashCode(), "[{0}] Exception: {1}", header, exception.ToTraceString());
			PushNotificationsCrimsonEvents.ApnsFeedbackChannelError.Log<string, string, string>(this.appId, string.Empty, exception.ToTraceString());
			if (exception is ApnsFeedbackException)
			{
				return exception;
			}
			return new ApnsFeedbackException(Strings.ApnsFeedbackError(this.appId, exception.GetType().ToString(), exception.Message), exception);
		}

		// Token: 0x040000B6 RID: 182
		private readonly string appId;

		// Token: 0x040000B7 RID: 183
		private ApnsChannelSettings config;

		// Token: 0x040000B8 RID: 184
		private ITracer tracer;

		// Token: 0x02000030 RID: 48
		private class ApnsFeedbackClient : IDisposable
		{
			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007B9E File Offset: 0x00005D9E
			// (set) Token: 0x060001E3 RID: 483 RVA: 0x00007BA6 File Offset: 0x00005DA6
			public string AppId { get; set; }

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007BAF File Offset: 0x00005DAF
			// (set) Token: 0x060001E5 RID: 485 RVA: 0x00007BB7 File Offset: 0x00005DB7
			public ApnsTcpClient TcpClient { get; set; }

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060001E6 RID: 486 RVA: 0x00007BC0 File Offset: 0x00005DC0
			// (set) Token: 0x060001E7 RID: 487 RVA: 0x00007BC8 File Offset: 0x00005DC8
			public ApnsSslStream SslStream { get; set; }

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060001E8 RID: 488 RVA: 0x00007BD1 File Offset: 0x00005DD1
			// (set) Token: 0x060001E9 RID: 489 RVA: 0x00007BD9 File Offset: 0x00005DD9
			public IAsyncResult AuthenticateAsyncResult { get; set; }

			// Token: 0x060001EA RID: 490 RVA: 0x00007BE2 File Offset: 0x00005DE2
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x060001EB RID: 491 RVA: 0x00007C50 File Offset: 0x00005E50
			protected virtual void Dispose(bool disposing)
			{
				if (!this.disposed)
				{
					if (disposing)
					{
						if (this.AuthenticateAsyncResult != null)
						{
							Task task = new Task(delegate()
							{
								try
								{
									this.SslStream.EndAuthenticateAsClient(this.AuthenticateAsyncResult);
								}
								catch (Exception exception)
								{
									PushNotificationsCrimsonEvents.ApnsChannelCleanupUnexpectedError.Log<string, string, string>(this.AppId, string.Empty, exception.ToTraceString());
								}
								finally
								{
									this.Disconnect();
								}
							});
							task.Start();
						}
						else
						{
							this.Disconnect();
						}
					}
					this.disposed = true;
				}
			}

			// Token: 0x060001EC RID: 492 RVA: 0x00007C9E File Offset: 0x00005E9E
			private void Disconnect()
			{
				if (this.SslStream != null)
				{
					this.SslStream.Dispose();
				}
				if (this.TcpClient != null)
				{
					this.TcpClient.Dispose();
				}
			}

			// Token: 0x040000B9 RID: 185
			private bool disposed;
		}
	}
}
