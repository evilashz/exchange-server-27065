using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200001D RID: 29
	internal class ApnsChannel : PushNotificationChannel<ApnsNotification>
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00004658 File Offset: 0x00002858
		public ApnsChannel(ApnsChannelSettings settings, ITracer tracer) : base(settings.AppId, tracer)
		{
			ArgumentValidator.ThrowIfNull("settings", settings);
			settings.Validate();
			this.config = settings;
			this.State = ApnsChannelState.Init;
			this.WaitingExit = ExDateTime.UtcNow;
			this.unconfirmedNotifications = new Queue<ApnsNotification>();
			this.Counters = ApnsChannelCounters.GetInstance(base.AppId);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000046B8 File Offset: 0x000028B8
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000046C0 File Offset: 0x000028C0
		private protected ApnsChannelState State { protected get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000046C9 File Offset: 0x000028C9
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000046D1 File Offset: 0x000028D1
		private protected ExDateTime WaitingExit { protected get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000046DA File Offset: 0x000028DA
		protected bool IsDisconnected
		{
			get
			{
				return this.tcpClient == null && this.sslStream == null && this.readTask == null;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000046F7 File Offset: 0x000028F7
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000046FF File Offset: 0x000028FF
		private ApnsChannelCountersInstance Counters { get; set; }

		// Token: 0x06000101 RID: 257 RVA: 0x00004708 File Offset: 0x00002908
		public override void Send(ApnsNotification notification, CancellationToken cancelToken)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("notification", notification);
			if (!notification.IsValid)
			{
				this.OnInvalidNotificationFound(new InvalidNotificationEventArgs(notification, new InvalidPushNotificationException(notification.ValidationErrors[0])));
				return;
			}
			ApnsChannelContext apnsChannelContext = new ApnsChannelContext(notification, cancelToken, base.Tracer, this.config);
			ApnsChannelState apnsChannelState = this.State;
			while (apnsChannelContext.IsActive)
			{
				this.CheckCancellation(apnsChannelContext);
				switch (this.State)
				{
				case ApnsChannelState.Init:
					apnsChannelState = this.ProcessInit(apnsChannelContext);
					break;
				case ApnsChannelState.Connecting:
					apnsChannelState = this.ProcessConnecting(apnsChannelContext);
					break;
				case ApnsChannelState.DelayingConnect:
					apnsChannelState = this.ProcessDelayingConnect(apnsChannelContext);
					break;
				case ApnsChannelState.Authenticating:
					apnsChannelState = this.ProcessAuthenticating(apnsChannelContext);
					break;
				case ApnsChannelState.Reading:
					apnsChannelState = this.ProcessReading(apnsChannelContext);
					break;
				case ApnsChannelState.Sending:
					apnsChannelState = this.ProcessSending(apnsChannelContext);
					break;
				case ApnsChannelState.Waiting:
					apnsChannelState = this.ProcessWaiting(apnsChannelContext);
					break;
				}
				base.Tracer.TraceDebug<ApnsChannelState, ApnsChannelState>((long)this.GetHashCode(), "[Send] Transitioning from {0} to {1}", this.State, apnsChannelState);
				this.State = apnsChannelState;
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004812 File Offset: 0x00002A12
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[InternalDispose] Disposing the channel for '{0}'", base.AppId);
				this.State = this.TransitionToInit();
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004840 File Offset: 0x00002A40
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ApnsChannel>(this);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004848 File Offset: 0x00002A48
		protected virtual ApnsCertProvider CreateCertProvider()
		{
			return new ApnsCertProvider(new ApnsCertStore(new X509Store(StoreName.My, StoreLocation.LocalMachine)), this.config.IgnoreCertificateErrors, base.Tracer, base.AppId);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004872 File Offset: 0x00002A72
		protected virtual void Delay(int delayTime)
		{
			if (delayTime > 0)
			{
				Thread.Sleep(delayTime);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000487E File Offset: 0x00002A7E
		protected virtual ApnsTcpClient CreateTcpClient()
		{
			return new ApnsTcpClient(new TcpClient());
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004892 File Offset: 0x00002A92
		protected virtual ApnsSslStream CreateSslStream()
		{
			return new ApnsSslStream(new SslStream(this.tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(this.certProvider.ValidateCertificate), (object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers) => this.cert));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000048C8 File Offset: 0x00002AC8
		private ApnsChannelState ProcessInit(ApnsChannelContext currentNotification)
		{
			if (this.certProvider == null)
			{
				this.certProvider = this.CreateCertProvider();
			}
			base.Tracer.TraceDebug<string, ApnsChannelContext>((long)this.GetHashCode(), "[ProcessInit] Finding certificate by thumbprint '{0}' for {1}", this.config.CertificateThumbprint, currentNotification);
			this.cert = null;
			ApnsChannelState result;
			try
			{
				this.cert = this.certProvider.LoadCertificate(this.config.CertificateThumbprint, this.config.CertificateThumbprintFallback);
				result = ApnsChannelState.DelayingConnect;
			}
			catch (ApnsCertificateException)
			{
				result = this.TransitionToWaiting();
			}
			return result;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000495C File Offset: 0x00002B5C
		private ApnsChannelState ProcessDelayingConnect(ApnsChannelContext currentNotification)
		{
			int num = currentNotification.CurrentRetryDelay;
			if (num > 0)
			{
				base.Tracer.TraceDebug<int, ApnsChannelContext>((long)this.GetHashCode(), "[ProcessDelayingConnect] Delaying our next connection try {0} milliseconds for {1}", num, currentNotification);
			}
			bool flag = false;
			while (!flag && !currentNotification.IsCancelled)
			{
				if (num > this.config.ConnectStepTimeout)
				{
					this.Delay(this.config.ConnectStepTimeout);
					num -= this.config.ConnectStepTimeout;
				}
				else
				{
					this.Delay(num);
					flag = true;
				}
			}
			return ApnsChannelState.Connecting;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000049D8 File Offset: 0x00002BD8
		private ApnsChannelState ProcessConnecting(ApnsChannelContext currentNotification)
		{
			this.tcpClient = this.CreateTcpClient();
			base.Tracer.TraceDebug<string, int, ApnsChannelContext>((long)this.GetHashCode(), "[ProcessConnecting] Connecting to {0}:{1} for {2}", this.config.Host, this.config.Port, currentNotification);
			bool flag = true;
			AverageTimeCounterBase averageTimeCounterBase = new AverageTimeCounterBase(this.Counters.AverageApnsConnectionTime, this.Counters.AverageApnsConnectionTimeBase, true);
			try
			{
				IAsyncResult asyncResult = this.tcpClient.BeginConnect(this.config.Host, this.config.Port, null, base.AppId);
				while (!currentNotification.IsCancelled)
				{
					if (asyncResult.AsyncWaitHandle.WaitOne(this.config.ConnectStepTimeout))
					{
						this.tcpClient.EndConnect(asyncResult);
						if (this.tcpClient.Connected)
						{
							flag = false;
							currentNotification.ResetConnectRetries();
							averageTimeCounterBase.Stop();
							PushNotificationsMonitoring.PublishSuccessNotification("ApnsChannelConnect", base.AppId);
							return ApnsChannelState.Authenticating;
						}
						this.Counters.ApnsConnectionFailed.Increment();
						base.Tracer.TraceDebug((long)this.GetHashCode(), "[ProcessConnecting] EndConnect didn't fail but the TcpClient was not connected");
						break;
					}
				}
			}
			catch (SocketException ex)
			{
				string text = ex.ToTraceString();
				base.Tracer.TraceError<string>((long)this.GetHashCode(), "[TryEndConnect] Unexpected SocketException: {0}", text);
				if (currentNotification.IsRetryable)
				{
					PushNotificationsCrimsonEvents.PushNotificationRetryableError.Log<string, string, string>(base.AppId, string.Empty, text);
				}
				else
				{
					PushNotificationsCrimsonEvents.ApnsChannelConnectError.Log<string, SocketError, string>(base.AppId, ex.SocketErrorCode, text);
				}
			}
			finally
			{
				if (flag)
				{
					this.Disconnect();
				}
			}
			if (currentNotification.IsRetryable || currentNotification.IsCancelled)
			{
				currentNotification.IncrementConnectRetries();
				return ApnsChannelState.DelayingConnect;
			}
			PushNotificationsMonitoring.PublishFailureNotification("ApnsChannelConnect", base.AppId, "");
			return this.TransitionToWaiting();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004BD0 File Offset: 0x00002DD0
		private ApnsChannelState ProcessAuthenticating(ApnsChannelContext currentNotification)
		{
			this.sslStream = this.CreateSslStream();
			base.Tracer.TraceDebug<string, ApnsChannelContext>((long)this.GetHashCode(), "[ProcessAuthenticating] Authenticating to {0} for {1}", this.config.Host, currentNotification);
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			bool flag = true;
			int num = this.config.ConnectTotalTimeout;
			AverageTimeCounterBase averageTimeCounterBase = new AverageTimeCounterBase(this.Counters.AverageApnsAuthTime, this.Counters.AverageApnsAuthTimeBase, true);
			try
			{
				IAsyncResult asyncResult = this.sslStream.BeginAuthenticateAsClient(this.config.Host, new X509Certificate2Collection(this.cert), SslProtocols.Default, false, null, base.AppId);
				while (!currentNotification.IsCancelled)
				{
					if (asyncResult.AsyncWaitHandle.WaitOne(this.config.ConnectStepTimeout))
					{
						this.sslStream.EndAuthenticateAsClient(asyncResult);
						if (this.sslStream.IsMutuallyAuthenticated && this.sslStream.CanRead && this.sslStream.CanWrite)
						{
							stopwatch.Stop();
							averageTimeCounterBase.Stop();
							if (this.averageTimeApnsChannelOpen == null)
							{
								this.averageTimeApnsChannelOpen = new AverageTimeCounterBase(this.Counters.AverageApnsChannelOpenTime, this.Counters.AverageApnsChannelOpenTimeBase, true);
							}
							flag = false;
							PushNotificationsCrimsonEvents.ApnsSuccessfulAuthentication.Log<string, long>(base.AppId, stopwatch.ElapsedMilliseconds);
							PushNotificationsMonitoring.PublishSuccessNotification("ApnsChannelAuthenticate", base.AppId);
							this.sslStream.WriteTimeout = this.config.WriteTimeout;
							return ApnsChannelState.Reading;
						}
						base.Tracer.TraceDebug<string, string, string>((long)ApnsChannel.ClassTraceId, "[ProcessAuthenticating] Closing stream{0}{1}{2}", this.sslStream.IsMutuallyAuthenticated ? string.Empty : " - Stream not mutually authenticated", this.sslStream.CanRead ? string.Empty : " - Stream not readable", this.sslStream.CanWrite ? string.Empty : " - Stream not writable");
						break;
					}
					else
					{
						num -= this.config.ConnectStepTimeout;
						if (num <= 0)
						{
							this.ScheduleConnectionCleanup(asyncResult);
							throw new AuthenticationException(Strings.ApnsChannelAuthenticationTimeout);
						}
					}
				}
			}
			catch (AuthenticationException aex)
			{
				string text = aex.ToTraceString();
				base.Tracer.TraceError<string>((long)this.GetHashCode(), "[ProcessAuthenticating] AuthenticationException calling EndAuthenticateAsClient: {0}", text);
				if (currentNotification.IsRetryable)
				{
					PushNotificationsCrimsonEvents.PushNotificationRetryableError.Log<string, string, string>(base.AppId, string.Empty, text);
				}
				else
				{
					PushNotificationsCrimsonEvents.ApnsChannelAuthenticateError.Log<string, string, string>(base.AppId, string.Empty, text);
				}
			}
			catch (IOException ioex)
			{
				string text2 = ioex.ToTraceString();
				base.Tracer.TraceError<string>((long)this.GetHashCode(), "[ProcessAuthenticating] IOException calling EndAuthenticateAsClient: {0}", text2);
				if (currentNotification.IsRetryable)
				{
					PushNotificationsCrimsonEvents.PushNotificationRetryableError.Log<string, string, string>(base.AppId, string.Empty, text2);
				}
				else
				{
					PushNotificationsCrimsonEvents.ApnsChannelAuthenticateError.Log<string, string, string>(base.AppId, string.Empty, text2);
				}
			}
			finally
			{
				if (flag)
				{
					stopwatch.Stop();
					this.Disconnect();
				}
			}
			if (currentNotification.IsRetryable || currentNotification.IsCancelled)
			{
				currentNotification.IncrementAuthenticateRetries();
				return this.TransitionToInit();
			}
			PushNotificationsMonitoring.PublishFailureNotification("ApnsChannelAuthenticate", base.AppId, "");
			return this.TransitionToWaiting();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004F48 File Offset: 0x00003148
		private ApnsChannelState ProcessReading(ApnsChannelContext currentNotification)
		{
			if (this.readTask == null)
			{
				this.readTask = Task.Factory.StartNew<ApnsResponse>(() => this.Read());
			}
			else if (this.readTask.IsCompleted)
			{
				this.Counters.ApnsReadTaskEnded.Increment();
				base.Tracer.TraceDebug((long)this.GetHashCode(), "[ProcessReading] Read task finished. Analyze the result and reset.");
				return this.TransitionToInit();
			}
			return ApnsChannelState.Sending;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004FC0 File Offset: 0x000031C0
		private ApnsChannelState ProcessSending(ApnsChannelContext currentNotification)
		{
			ApnsChannelState result;
			try
			{
				AverageTimeCounterBase averageTimeCounterBase = new AverageTimeCounterBase(this.Counters.AverageApnsChannelSendTime, this.Counters.AverageApnsChannelSendTimeBase, true);
				byte[] buffer = currentNotification.Notification.ConvertToApnsBinaryFormat();
				if (currentNotification.Notification.IsMonitoring)
				{
					PushNotificationsMonitoring.PublishSuccessNotification("NotificationProcessed", base.AppId);
				}
				else
				{
					this.sslStream.Write(buffer);
					PushNotificationTracker.ReportSent(currentNotification.Notification, PushNotificationPlatform.None);
					currentNotification.Notification.SentTime = ExDateTime.UtcNow;
					this.unconfirmedNotifications.Enqueue(currentNotification.Notification);
					this.ConfirmCachedNotifications();
					averageTimeCounterBase.Stop();
				}
				currentNotification.Done();
				result = ApnsChannelState.Reading;
			}
			catch (IOException ioex)
			{
				string text = ioex.ToTraceString();
				base.Tracer.TraceError<string, string>((long)this.GetHashCode(), "[ProcessSending] IOException while sending notification '{0}'. {1}", currentNotification.Notification.ToFullString(), text);
				PushNotificationsCrimsonEvents.ApnsChannelSendingError.Log<string, string, string>(base.AppId, currentNotification.Notification.ToFullString(), text);
				currentNotification.Drop(text);
				result = this.TransitionToInit();
			}
			return result;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000050D0 File Offset: 0x000032D0
		private ApnsChannelState ProcessWaiting(ApnsChannelContext currentNotification)
		{
			if (this.WaitingExit > ExDateTime.UtcNow)
			{
				currentNotification.Drop(this.State.ToString());
				return ApnsChannelState.Waiting;
			}
			return ApnsChannelState.Init;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000050FD File Offset: 0x000032FD
		private ApnsChannelState TransitionToInit()
		{
			this.Reset();
			return ApnsChannelState.Init;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005108 File Offset: 0x00003308
		private ApnsChannelState TransitionToWaiting()
		{
			this.Reset();
			this.WaitingExit = ExDateTime.UtcNow.AddSeconds((double)this.config.BackOffTimeInSeconds);
			PushNotificationsCrimsonEvents.ApnsChannelTransitionToWaiting.LogPeriodic<string, ExDateTime>(base.AppId, TimeSpan.FromSeconds((double)this.config.BackOffTimeInSeconds), base.AppId, this.WaitingExit);
			return ApnsChannelState.Waiting;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000516C File Offset: 0x0000336C
		private void CheckCancellation(ApnsChannelContext currentNotification)
		{
			if (currentNotification.IsCancelled)
			{
				switch (this.State)
				{
				case ApnsChannelState.Authenticating:
					this.State = this.TransitionToInit();
					break;
				case ApnsChannelState.Sending:
					this.State = ApnsChannelState.Reading;
					break;
				}
				base.Tracer.TraceDebug<ApnsChannelState>((long)this.GetHashCode(), "[CheckCancellation] Cancellation requested, next state is {0}", this.State);
				throw new OperationCanceledException();
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000051E4 File Offset: 0x000033E4
		private ApnsResponse Read()
		{
			ApnsResponse apnsResponse = null;
			try
			{
				base.Tracer.TraceDebug((long)this.GetHashCode(), "[Read] Beginning read from APNs");
				byte[] array = new byte[6];
				if (6 == this.sslStream.Read(array, 0, 6))
				{
					apnsResponse = ApnsResponse.FromApnsFormat(array);
					base.Tracer.TraceError<ApnsResponse>((long)this.GetHashCode(), "[Read] APNs response: '{0}'", apnsResponse);
				}
				else
				{
					base.Tracer.TraceWarning((long)this.GetHashCode(), "[Read] Unexpected number of bytes read from the SSL stream");
				}
			}
			catch (ObjectDisposedException exception)
			{
				base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[Read] {0}", exception.ToTraceString());
			}
			return apnsResponse;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005290 File Offset: 0x00003490
		private void AnalyzeReadResult(bool ignoreException)
		{
			if (this.readTask.IsCanceled)
			{
				return;
			}
			if (!this.readTask.IsFaulted)
			{
				if (this.readTask.Result != null)
				{
					ApnsNotification apnsNotification = null;
					foreach (ApnsNotification apnsNotification2 in this.unconfirmedNotifications)
					{
						if (apnsNotification2.SequenceNumber == this.readTask.Result.Identifier)
						{
							apnsNotification = apnsNotification2;
							break;
						}
						base.Tracer.TraceDebug<ApnsNotification>((long)this.GetHashCode(), "[AnalyzeReadResult] Notification confirmed '{0}'", apnsNotification2);
					}
					if (apnsNotification != null)
					{
						this.OnInvalidNotificationFound(new InvalidNotificationEventArgs(apnsNotification, new InvalidPushNotificationException(Strings.InvalidReportFromApns(this.readTask.Result.ToString()))));
						return;
					}
				}
			}
			else
			{
				Exception innerException = this.readTask.Exception.InnerException;
				base.Tracer.TraceError<Exception>((long)this.GetHashCode(), "[AnalyzeReadResult] Read task failed with an exception: {0}", innerException);
				if (!ignoreException)
				{
					PushNotificationsCrimsonEvents.ApnsChannelReadError.Log<string, string, Exception>(base.AppId, string.Empty, this.readTask.Exception.InnerException);
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000053BC File Offset: 0x000035BC
		private void ConfirmCachedNotifications()
		{
			ExDateTime t = ExDateTime.UtcNow.Subtract(TimeSpan.FromSeconds(2.0));
			while (this.unconfirmedNotifications.Count > 0 && this.unconfirmedNotifications.Peek().SentTime < t)
			{
				base.Tracer.TraceDebug<ApnsNotification>((long)this.GetHashCode(), "[ConfirmCachedNotifications] Notification confirmed '{0}'", this.unconfirmedNotifications.Dequeue());
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005430 File Offset: 0x00003630
		private void Reset()
		{
			this.Counters.ApnsChannelReset.Increment();
			if (this.averageTimeApnsChannelOpen != null)
			{
				this.averageTimeApnsChannelOpen.Stop();
				this.averageTimeApnsChannelOpen = null;
			}
			base.Tracer.TraceDebug((long)this.GetHashCode(), "[Reset] Resetting channel");
			this.ConfirmCachedNotifications();
			this.Disconnect();
			this.cert = null;
			this.WaitingExit = ExDateTime.UtcNow;
			if (this.unconfirmedNotifications.Count != 0)
			{
				this.unconfirmedNotifications = new Queue<ApnsNotification>();
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000054B8 File Offset: 0x000036B8
		private void Disconnect()
		{
			base.Tracer.TraceDebug((long)this.GetHashCode(), "[Disconnect] Disconnecting channel");
			bool ignoreException = this.readTask == null || !this.readTask.IsCompleted;
			if (this.sslStream != null)
			{
				this.sslStream.Dispose();
			}
			if (this.tcpClient != null)
			{
				this.tcpClient.Dispose();
			}
			if (this.readTask != null)
			{
				if (!this.readTask.IsCompleted)
				{
					base.Tracer.TraceDebug((long)this.GetHashCode(), "[Disconnect] Waiting for the Read task to finish");
					try
					{
						this.readTask.Wait();
					}
					catch (AggregateException)
					{
					}
				}
				this.AnalyzeReadResult(ignoreException);
				this.readTask.Dispose();
			}
			this.readTask = null;
			this.sslStream = null;
			this.tcpClient = null;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000562C File Offset: 0x0000382C
		private void ScheduleConnectionCleanup(IAsyncResult beginAuthenticateResult)
		{
			base.Tracer.TraceDebug((long)this.GetHashCode(), "[ScheduleConnectionCleanup] Creating a task to close this.sslStream when authentication ends");
			ApnsSslStream tempStreamRef = this.sslStream;
			ApnsTcpClient tempClientRef = this.tcpClient;
			string tempAppIdRef = base.AppId;
			this.sslStream = null;
			this.tcpClient = null;
			Task task = new Task(delegate()
			{
				try
				{
					tempStreamRef.EndAuthenticateAsClient(beginAuthenticateResult);
				}
				catch (Exception exception)
				{
					string text = exception.ToTraceString();
					this.Tracer.TraceError<string>((long)ApnsChannel.ClassTraceId, "[ScheduleConnectionCleanup] Unexpected exception: {0}", text);
					PushNotificationsCrimsonEvents.ApnsChannelCleanupUnexpectedError.Log<string, string, string>(tempAppIdRef, string.Empty, text);
				}
				finally
				{
					tempStreamRef.Dispose();
					tempClientRef.Dispose();
				}
			});
			task.Start();
		}

		// Token: 0x04000050 RID: 80
		private static readonly int ClassTraceId = typeof(ApnsChannel).GetHashCode();

		// Token: 0x04000051 RID: 81
		private ApnsChannelSettings config;

		// Token: 0x04000052 RID: 82
		private X509Certificate2 cert;

		// Token: 0x04000053 RID: 83
		private ApnsCertProvider certProvider;

		// Token: 0x04000054 RID: 84
		private ApnsTcpClient tcpClient;

		// Token: 0x04000055 RID: 85
		private ApnsSslStream sslStream;

		// Token: 0x04000056 RID: 86
		private Queue<ApnsNotification> unconfirmedNotifications;

		// Token: 0x04000057 RID: 87
		private Task<ApnsResponse> readTask;

		// Token: 0x04000058 RID: 88
		private AverageTimeCounterBase averageTimeApnsChannelOpen;
	}
}
