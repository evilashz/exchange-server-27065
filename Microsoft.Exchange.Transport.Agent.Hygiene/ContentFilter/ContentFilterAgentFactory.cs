using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Interop;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.ContentFilter;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.ContentFilter
{
	// Token: 0x02000017 RID: 23
	public sealed class ContentFilterAgentFactory : SmtpReceiveAgentFactory, IDisposable
	{
		// Token: 0x06000056 RID: 86 RVA: 0x0000581C File Offset: 0x00003A1C
		public ContentFilterAgentFactory()
		{
			this.dispatcher = new ContentFilterAgentFactory.Dispatcher(this);
			CommonUtils.RegisterConfigurationChangeHandlers("Content Filtering", new ADOperation(this.RegisterConfigurationChangeHandlers), ExTraceGlobals.InitializationTracer, this);
			this.Configure(true);
			this.antispamUpdateModePollingTimer = new Timer(new TimerCallback(this.AntispamUpdateServiceMonitor), null, 600000, -1);
			this.isDataCenterEnvironment = Datacenter.IsMultiTenancyEnabled();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005894 File Offset: 0x00003A94
		~ContentFilterAgentFactory()
		{
			this.Dispose(false);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000058C4 File Offset: 0x00003AC4
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("ContentFilterAgentFactory");
			}
			BypassedRecipients bypassedRecipients = new BypassedRecipients(this.contentFilterConfig.BypassedRecipients, (server != null) ? server.AddressBook : null);
			return new ContentFilterAgent(this, this.contentFilterConfig, bypassedRecipients, this.bypassedSenders);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005914 File Offset: 0x00003B14
		public override void Close()
		{
			this.UnregisterConfigurationChangeHandlers();
			Util.PerformanceCounters.RemoveCounters();
			this.Dispose();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00005927 File Offset: 0x00003B27
		internal bool IsDataCenterEnvironment
		{
			get
			{
				return this.isDataCenterEnvironment;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00005930 File Offset: 0x00003B30
		internal IAsyncResult BeginScanMessage(AsyncCallback callback, ContentFilterAgent.AsyncState state)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (state == null)
			{
				throw new ArgumentNullException("state");
			}
			ContentFilterAgentFactory.ScanRequest scanRequest = new ContentFilterAgentFactory.ScanRequest(this.dispatcher, callback, state);
			this.dispatcher.Enqueue(scanRequest);
			return scanRequest;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005974 File Offset: 0x00003B74
		internal ScanMessageResult EndScanMessage(IAsyncResult asyncResult)
		{
			ContentFilterAgentFactory.ScanRequest scanRequest = asyncResult as ContentFilterAgentFactory.ScanRequest;
			if (scanRequest == null)
			{
				throw new InvalidOperationException("the asyncResult argument must be one previously obtained from a call to BeginScanMessage().");
			}
			if (scanRequest.Exception != null)
			{
				throw scanRequest.Exception;
			}
			return scanRequest.ScanResult;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000059AC File Offset: 0x00003BAC
		private void RegisterConfigurationChangeHandlers()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 237, "RegisterConfigurationChangeHandlers", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\ContentFilter\\Agent\\ContentFilterAgentFactory.cs");
			ADObjectId orgContainerId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
			ADObjectId childId = orgContainerId.GetChildId("Transport Settings");
			ADObjectId childId2 = childId.GetChildId("Message Hygiene");
			TransportFacades.ConfigChanged += this.ConfigUpdate;
			this.configRequestCookie = ADNotificationAdapter.RegisterChangeNotification<ContentFilterConfig>(childId2, new ADNotificationCallback(this.Configure));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005A21 File Offset: 0x00003C21
		private void UnregisterConfigurationChangeHandlers()
		{
			TransportFacades.ConfigChanged -= this.ConfigUpdate;
			if (this.configRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.configRequestCookie);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005A47 File Offset: 0x00003C47
		private void ConfigUpdate(object source, EventArgs args)
		{
			this.Configure(false);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005A50 File Offset: 0x00003C50
		private void Configure(ADNotificationEventArgs args)
		{
			try
			{
				this.Configure(false);
			}
			catch (ExchangeConfigurationException arg)
			{
				string formatString = "SmartScreen could not be re-initialized with new configuration and will keep running with the current configuration. Details: {0}";
				ExTraceGlobals.InitializationTracer.TraceError<ExchangeConfigurationException>((long)this.GetHashCode(), formatString, arg);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005ACC File Offset: 0x00003CCC
		private void Configure(bool onStartup)
		{
			ContentFilterConfig contentFilterConfig;
			ADOperationResult adoperationResult;
			if (ADNotificationAdapter.TryReadConfiguration<ContentFilterConfig>(delegate()
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(OrganizationId.ForestWideOrgId);
				return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 318, "Configure", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\ContentFilter\\Agent\\ContentFilterAgentFactory.cs").FindSingletonConfigurationObject<ContentFilterConfig>();
			}, out contentFilterConfig, out adoperationResult))
			{
				this.contentFilterConfig = contentFilterConfig;
				this.bypassedSenders = new BypassedSenders(contentFilterConfig.BypassedSenders, contentFilterConfig.BypassedSenderDomains);
				this.SetPremiumModeEnabled(onStartup);
				lock (this.wrapperLock)
				{
					this.wrapper = this.InitializeFilter();
					return;
				}
			}
			CommonUtils.FailedToReadConfiguration("Content Filtering", onStartup, adoperationResult.Exception, ExTraceGlobals.InitializationTracer, Util.EventLogger, this);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005B7C File Offset: 0x00003D7C
		private ContentFilterAgentFactory.ContentFilterWrapper RecreateContentFilterWrapper(ContentFilterAgentFactory.ContentFilterWrapper invalidWrapper)
		{
			lock (this.wrapperLock)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("ContentFilterAgentFactory");
				}
				if (this.wrapper == invalidWrapper)
				{
					this.wrapper = this.InitializeFilter();
				}
			}
			return this.wrapper;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005BE4 File Offset: 0x00003DE4
		private ContentFilterAgentFactory.ContentFilterWrapper InitializeFilter()
		{
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			ContentFilterAgentFactory.ContentFilterWrapper result;
			try
			{
				ComProxy comProxy = new ComProxy(Constants.ContentFilterWrapperGuid);
				ComArguments comArguments = new ComArguments();
				bool flag = false;
				ExTraceGlobals.InitializationTracer.TraceDebug((long)this.GetHashCode(), "Initializing the filter");
				comArguments[10] = Encoding.Unicode.GetBytes(directoryName);
				this.SaveCustomWordsToPropertyBag(comArguments);
				comArguments.SetBool(11, this.contentFilterConfig.OutlookEmailPostmarkValidationEnabled);
				comArguments.SetBool(17, this.premiumModeEnabled);
				try
				{
					Util.InitializeFilter(comProxy, comArguments);
				}
				catch (UnauthorizedAccessException arg)
				{
					ExTraceGlobals.InitializationTracer.TraceError<UnauthorizedAccessException>((long)this.GetHashCode(), "Caught UnauthorizedAccessException when initializing filter. Details: {0}. Retrying...", arg);
					flag = true;
				}
				catch (BadImageFormatException arg2)
				{
					ExTraceGlobals.InitializationTracer.TraceError<BadImageFormatException>((long)this.GetHashCode(), "Caught BadImageFormatException when initializing filter. Details: {0}. Retrying...", arg2);
					flag = true;
				}
				if (flag)
				{
					Thread.Sleep(1000);
					Util.InitializeFilter(comProxy, comArguments);
				}
				Util.LogContentFilterInitialized();
				ExTraceGlobals.InitializationTracer.TraceDebug((long)this.GetHashCode(), "Filter was successfully initialized");
				result = new ContentFilterAgentFactory.ContentFilterWrapper(comProxy);
			}
			catch (UnauthorizedAccessException ex)
			{
				ExTraceGlobals.InitializationTracer.TraceDebug<UnauthorizedAccessException>((long)this.GetHashCode(), "UnauthorizedAccessException when initializing filter: {0}", ex);
				Util.LogFailedWithUnauthorizedAccess(directoryName, ex);
				throw new ExchangeConfigurationException(ex.Message, ex);
			}
			catch (BadImageFormatException ex2)
			{
				ExTraceGlobals.InitializationTracer.TraceDebug<BadImageFormatException>((long)this.GetHashCode(), "BadImageFormatException when initializing filter: {0}", ex2);
				Util.LogFailedWithBadImageFormat(directoryName, ex2);
				throw new ExchangeConfigurationException(ex2.Message, ex2);
			}
			catch (FileNotFoundException ex3)
			{
				ExTraceGlobals.InitializationTracer.TraceDebug<FileNotFoundException>((long)this.GetHashCode(), "FileNotFoundException when initializing filter: {0}", ex3);
				Util.LogContentFilterInitFailedFileNotFound(ex3);
				throw new ExchangeConfigurationException(ex3.Message, ex3);
			}
			catch (COMException ex4)
			{
				ExTraceGlobals.InitializationTracer.TraceDebug<COMException>((long)this.GetHashCode(), "COMException when initializing filter: {0}", ex4);
				if (ex4.ErrorCode == -2147024774)
				{
					Util.LogFailedInsufficientBuffer(ex4);
				}
				else if (ex4.ErrorCode == -2147023649)
				{
					Util.LogFailedFSWatcherAlreadyInitialized(ex4);
				}
				else if (ex4.ErrorCode == -1067253755)
				{
					Util.LogExSMimeFailedToInitialize(ex4);
				}
				throw new ExchangeConfigurationException(ex4.Message, ex4);
			}
			catch (Exception ex5)
			{
				ExTraceGlobals.InitializationTracer.TraceDebug<Exception>((long)this.GetHashCode(), "Failed to initialize filter: {0}", ex5);
				Util.LogContentFilterNotInitialized(ex5);
				throw new ExchangeConfigurationException(ex5.Message, ex5);
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005E70 File Offset: 0x00004070
		private void SaveCustomWordsToPropertyBag(ComArguments comArguments)
		{
			ReadOnlyCollection<ContentFilterPhrase> phrases = this.contentFilterConfig.GetPhrases();
			int count = phrases.Count;
			byte[][] array = new byte[count][];
			byte[][] array2 = new byte[count][];
			byte[][] array3 = new byte[count][];
			int num = 0;
			int num2 = 0;
			foreach (ContentFilterPhrase contentFilterPhrase in phrases)
			{
				array[num] = Encoding.Unicode.GetBytes(contentFilterPhrase.Phrase);
				array2[num] = BitConverter.GetBytes((int)contentFilterPhrase.Influence);
				array3[num] = BitConverter.GetBytes(array[num].Length);
				num2 += array[num].Length + array2[num].Length + array3[num].Length;
				num++;
			}
			byte[] array4 = Util.SerializeByteArrays(num2, new byte[][][]
			{
				array3,
				array,
				array2
			});
			comArguments[6] = BitConverter.GetBytes(array4.Length);
			comArguments[7] = array4;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005F7C File Offset: 0x0000417C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005F8C File Offset: 0x0000418C
		private void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				lock (this.wrapperLock)
				{
					if (this.wrapper != null)
					{
						this.wrapper.Dispose();
					}
				}
				if (this.antispamUpdateModePollingTimer != null)
				{
					this.antispamUpdateModePollingTimer.Dispose();
					this.antispamUpdateModePollingTimer = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00006008 File Offset: 0x00004208
		private void AntispamUpdateServiceMonitor(object state)
		{
			bool flag = this.premiumModeEnabled;
			this.SetPremiumModeEnabled(false);
			bool flag2 = flag != this.premiumModeEnabled;
			if (flag2)
			{
				ExTraceGlobals.InitializationTracer.TraceDebug<string>((long)this.GetHashCode(), "Anti-spam Update mode has been changed and SmartScreen is being re-initialized with {0} mode on.", this.premiumModeEnabled ? "Premium" : "Standard");
				Util.LogUpdateModeChangedReinitializingSmartScreen();
				lock (this.wrapperLock)
				{
					try
					{
						this.wrapper = this.InitializeFilter();
					}
					catch (ExchangeConfigurationException arg)
					{
						string formatString = "SmartScreen could not be re-initialized with {0} mode on and will keep running in the previous mode. Details: {1}";
						ExTraceGlobals.InitializationTracer.TraceError<string, ExchangeConfigurationException>((long)this.GetHashCode(), formatString, this.premiumModeEnabled ? "Premium" : "Standard", arg);
					}
				}
			}
			this.antispamUpdateModePollingTimer.Change(600000, -1);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000060EC File Offset: 0x000042EC
		private void SetPremiumModeEnabled(bool onStartup)
		{
			try
			{
				AntispamUpdates antispamUpdates = new AntispamUpdates();
				this.premiumModeEnabled = antispamUpdates.IsPremiumSKUInstalled();
			}
			catch (Exception ex)
			{
				if (onStartup)
				{
					throw new ExchangeConfigurationException(ex.Message, ex);
				}
				ExTraceGlobals.InitializationTracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to refresh Anti-spam Update mode - will continue running with current mode. Exception: {0}", ex);
				Util.LogFailedToReadAntispamUpdateMode(ex);
			}
		}

		// Token: 0x04000097 RID: 151
		private const int AntispamUpdateModeMonitorPeriod = 600000;

		// Token: 0x04000098 RID: 152
		private bool disposed;

		// Token: 0x04000099 RID: 153
		private ContentFilterAgentFactory.ContentFilterWrapper wrapper;

		// Token: 0x0400009A RID: 154
		private object wrapperLock = new object();

		// Token: 0x0400009B RID: 155
		private ContentFilterConfig contentFilterConfig;

		// Token: 0x0400009C RID: 156
		private ContentFilterAgentFactory.Dispatcher dispatcher;

		// Token: 0x0400009D RID: 157
		private ADNotificationRequestCookie configRequestCookie;

		// Token: 0x0400009E RID: 158
		private BypassedSenders bypassedSenders;

		// Token: 0x0400009F RID: 159
		private Timer antispamUpdateModePollingTimer;

		// Token: 0x040000A0 RID: 160
		private bool premiumModeEnabled;

		// Token: 0x040000A1 RID: 161
		private bool isDataCenterEnvironment;

		// Token: 0x02000018 RID: 24
		private sealed class Dispatcher
		{
			// Token: 0x0600006A RID: 106 RVA: 0x00006150 File Offset: 0x00004350
			public Dispatcher(ContentFilterAgentFactory factory)
			{
				this.factory = factory;
				this.pendingRequests = new ContentFilterAgentFactory.Dispatcher.PendingQueue();
				this.activeRequests = new ContentFilterAgentFactory.Dispatcher.ActiveRequests();
				new Thread(new ThreadStart(this.DispatcherProc))
				{
					Name = "ContentFilterMessageDispatcher",
					IsBackground = true
				}.Start();
			}

			// Token: 0x0600006B RID: 107 RVA: 0x000061AA File Offset: 0x000043AA
			public void Enqueue(ContentFilterAgentFactory.ScanRequest scanRequest)
			{
				this.pendingRequests.Enqueue(scanRequest);
			}

			// Token: 0x0600006C RID: 108 RVA: 0x000061B8 File Offset: 0x000043B8
			public void OnRequestFinished(ContentFilterAgentFactory.ScanRequest request)
			{
				this.activeRequests.Remove(request);
			}

			// Token: 0x0600006D RID: 109 RVA: 0x000061C8 File Offset: 0x000043C8
			private void DispatcherProc()
			{
				while (!TransportFacades.IsStopping)
				{
					ContentFilterAgentFactory.ScanRequest nextRequest = this.GetNextRequest();
					if (nextRequest != null)
					{
						this.activeRequests.Add(nextRequest);
						this.Dispatch(nextRequest, this.factory.wrapper);
					}
					else
					{
						Thread.Sleep(100);
					}
				}
				IEnumerable<ContentFilterAgentFactory.ScanRequest> enumerable = this.activeRequests.RemoveAll();
				foreach (ContentFilterAgentFactory.ScanRequest scanRequest in enumerable)
				{
					scanRequest.Abort((ScanMessageResult)4294967295U);
				}
				for (;;)
				{
					ContentFilterAgentFactory.ScanRequest scanRequest2 = this.pendingRequests.Dequeue();
					if (scanRequest2 != null)
					{
						scanRequest2.Abort((ScanMessageResult)4294967295U);
					}
					else
					{
						Thread.Sleep(100);
					}
				}
			}

			// Token: 0x0600006E RID: 110 RVA: 0x0000627C File Offset: 0x0000447C
			private ContentFilterAgentFactory.ScanRequest GetNextRequest()
			{
				ContentFilterAgentFactory.ScanRequest scanRequest = null;
				while (scanRequest == null)
				{
					scanRequest = this.pendingRequests.Dequeue();
					if (scanRequest == null)
					{
						break;
					}
					if ((DateTime.UtcNow - scanRequest.CreationTime).Minutes > 10)
					{
						ExTraceGlobals.ScanMessageTracer.TraceError((long)this.GetHashCode(), "Request timed out while sitting in the pending queue.");
						ThreadPool.QueueUserWorkItem(new WaitCallback(scanRequest.Abort), (ScanMessageResult)4294967294U);
						scanRequest = null;
					}
				}
				return scanRequest;
			}

			// Token: 0x0600006F RID: 111 RVA: 0x000062F0 File Offset: 0x000044F0
			private void Dispatch(ContentFilterAgentFactory.ScanRequest request, ContentFilterAgentFactory.ContentFilterWrapper wrapper)
			{
				ScanMessageResult scanMessageResult = ScanMessageResult.Error;
				try
				{
					try
					{
						bool flag = request.EnterWrapper(wrapper);
						if (flag)
						{
							scanMessageResult = request.Submit(wrapper);
						}
						else
						{
							scanMessageResult = (ScanMessageResult)4294967294U;
							ExTraceGlobals.ScanMessageTracer.TraceError((long)this.GetHashCode(), "Timed out waiting to enter the COM wrapper. It might have crashed.");
							if (wrapper.TimeoutCount == 1)
							{
								if (Interlocked.CompareExchange(ref wrapper.PingPending, 1, 0) == 0)
								{
									scanMessageResult = request.Ping(wrapper);
									ExTraceGlobals.ScanMessageTracer.TraceError((long)this.GetHashCode(), "Ping request submitted.");
									Util.LogWrapperSendingPingRequest(15);
								}
								else
								{
									ExTraceGlobals.ScanMessageTracer.TraceError((long)this.GetHashCode(), "Ping request not being sent because a previous ping has not returned.");
								}
							}
							else if (wrapper.TimeoutCount == 10)
							{
								IEnumerable<ContentFilterAgentFactory.ScanRequest> enumerable = this.activeRequests.RemoveAll();
								foreach (ContentFilterAgentFactory.ScanRequest scanRequest in enumerable)
								{
									scanRequest.Abort((ScanMessageResult)4294967294U);
								}
								this.RecycleWrapper(wrapper);
							}
							else
							{
								Util.LogWrapperNotResponding();
							}
						}
					}
					catch (COMException ex)
					{
						ExTraceGlobals.ScanMessageTracer.TraceError<COMException>((long)this.GetHashCode(), "Content Filter wrapper appears to be down. Details: {0}", ex);
						Util.LogErrorSubmittingMessage(ex);
						IEnumerable<ContentFilterAgentFactory.ScanRequest> enumerable2 = this.activeRequests.RemoveAll();
						foreach (ContentFilterAgentFactory.ScanRequest scanRequest2 in enumerable2)
						{
							scanRequest2.Abort((ScanMessageResult)4294967294U);
						}
						this.factory.RecreateContentFilterWrapper(wrapper);
					}
				}
				catch (Exception ex2)
				{
					ExTraceGlobals.ScanMessageTracer.TraceError<Exception>((long)this.GetHashCode(), "Unhandled exception in ScanRequest.Submit(): {0}.", ex2);
					request.Exception = ex2;
				}
				finally
				{
					if (scanMessageResult != ScanMessageResult.Pending)
					{
						ExTraceGlobals.ScanMessageTracer.TraceDebug((long)this.GetHashCode(), "Message was not accepted by wrapper.");
						request.ExitWrapper();
						ThreadPool.QueueUserWorkItem(new WaitCallback(request.Abort), scanMessageResult);
					}
				}
			}

			// Token: 0x06000070 RID: 112 RVA: 0x0000652C File Offset: 0x0000472C
			private void RecycleWrapper(ContentFilterAgentFactory.ContentFilterWrapper wrapper)
			{
				ExTraceGlobals.ScanMessageTracer.TraceDebug((long)this.GetHashCode(), "Recycling the wrapper process...");
				Util.LogWrapperBeingRecycled();
				if (wrapper.Recycle())
				{
					ExTraceGlobals.ScanMessageTracer.TraceDebug((long)this.GetHashCode(), "Wrapper process has been recycled.");
					Util.LogWrapperSuccessfullyRecycled();
					this.factory.RecreateContentFilterWrapper(wrapper);
					return;
				}
				ExTraceGlobals.ScanMessageTracer.TraceDebug((long)this.GetHashCode(), "Wrapper recycle timed out.");
				Util.LogWrapperRecycleTimedout();
			}

			// Token: 0x040000A3 RID: 163
			internal const int MaximumNumberOfActiveRequests = 15;

			// Token: 0x040000A4 RID: 164
			private const int ScanRequestQueueTimeout = 10;

			// Token: 0x040000A5 RID: 165
			private const int WrapperRecycleTimeoutThreshold = 10;

			// Token: 0x040000A6 RID: 166
			private ContentFilterAgentFactory factory;

			// Token: 0x040000A7 RID: 167
			private ContentFilterAgentFactory.Dispatcher.PendingQueue pendingRequests;

			// Token: 0x040000A8 RID: 168
			private ContentFilterAgentFactory.Dispatcher.ActiveRequests activeRequests;

			// Token: 0x02000019 RID: 25
			private sealed class PendingQueue
			{
				// Token: 0x06000071 RID: 113 RVA: 0x000065A0 File Offset: 0x000047A0
				public void Enqueue(ContentFilterAgentFactory.ScanRequest scanRequest)
				{
					lock (this.list)
					{
						this.list.AddLast(scanRequest);
					}
				}

				// Token: 0x06000072 RID: 114 RVA: 0x000065E8 File Offset: 0x000047E8
				public ContentFilterAgentFactory.ScanRequest Dequeue()
				{
					ContentFilterAgentFactory.ScanRequest result = null;
					lock (this.list)
					{
						if (this.list.Count > 0)
						{
							result = this.list.First.Value;
							this.list.RemoveFirst();
						}
					}
					return result;
				}

				// Token: 0x040000A9 RID: 169
				private LinkedList<ContentFilterAgentFactory.ScanRequest> list = new LinkedList<ContentFilterAgentFactory.ScanRequest>();
			}

			// Token: 0x0200001A RID: 26
			private sealed class ActiveRequests
			{
				// Token: 0x06000074 RID: 116 RVA: 0x00006664 File Offset: 0x00004864
				public void Add(ContentFilterAgentFactory.ScanRequest request)
				{
					lock (this.syncRoot)
					{
						this.dictionary.Add(request, null);
					}
				}

				// Token: 0x06000075 RID: 117 RVA: 0x000066AC File Offset: 0x000048AC
				public void Remove(ContentFilterAgentFactory.ScanRequest request)
				{
					lock (this.syncRoot)
					{
						this.dictionary.Remove(request);
					}
				}

				// Token: 0x06000076 RID: 118 RVA: 0x000066F4 File Offset: 0x000048F4
				public IEnumerable<ContentFilterAgentFactory.ScanRequest> RemoveAll()
				{
					IEnumerable<ContentFilterAgentFactory.ScanRequest> result = null;
					lock (this.syncRoot)
					{
						result = this.dictionary.Keys;
						this.dictionary = new Dictionary<ContentFilterAgentFactory.ScanRequest, object>();
					}
					return result;
				}

				// Token: 0x040000AA RID: 170
				private object syncRoot = new object();

				// Token: 0x040000AB RID: 171
				private Dictionary<ContentFilterAgentFactory.ScanRequest, object> dictionary = new Dictionary<ContentFilterAgentFactory.ScanRequest, object>();
			}
		}

		// Token: 0x0200001B RID: 27
		private sealed class ScanRequest : IAsyncResult
		{
			// Token: 0x06000078 RID: 120 RVA: 0x00006766 File Offset: 0x00004966
			public ScanRequest(ContentFilterAgentFactory.Dispatcher dispatcher, AsyncCallback asyncCallback, ContentFilterAgent.AsyncState asyncState)
			{
				this.dispatcher = dispatcher;
				this.asyncCallback = asyncCallback;
				this.asyncState = asyncState;
				this.scanResult = ScanMessageResult.Error;
				this.creationTime = DateTime.UtcNow;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000079 RID: 121 RVA: 0x00006795 File Offset: 0x00004995
			public ScanMessageResult ScanResult
			{
				get
				{
					return this.scanResult;
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600007A RID: 122 RVA: 0x0000679D File Offset: 0x0000499D
			// (set) Token: 0x0600007B RID: 123 RVA: 0x000067A5 File Offset: 0x000049A5
			public Exception Exception
			{
				get
				{
					return this.exception;
				}
				set
				{
					this.exception = value;
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600007C RID: 124 RVA: 0x000067AE File Offset: 0x000049AE
			public DateTime CreationTime
			{
				get
				{
					return this.creationTime;
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600007D RID: 125 RVA: 0x000067B6 File Offset: 0x000049B6
			public object AsyncState
			{
				get
				{
					return this.asyncState;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600007E RID: 126 RVA: 0x000067BE File Offset: 0x000049BE
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600007F RID: 127 RVA: 0x000067C5 File Offset: 0x000049C5
			public bool CompletedSynchronously
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000080 RID: 128 RVA: 0x000067CC File Offset: 0x000049CC
			public bool IsCompleted
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06000081 RID: 129 RVA: 0x000067D3 File Offset: 0x000049D3
			public void Abort(ScanMessageResult result)
			{
				this.scanResult = result;
				ExTraceGlobals.ScanMessageTracer.TraceDebug((long)this.GetHashCode(), "Scan request has been aborted.");
				this.Finish();
			}

			// Token: 0x06000082 RID: 130 RVA: 0x000067F8 File Offset: 0x000049F8
			public void Abort(object result)
			{
				this.Abort((result is ScanMessageResult) ? ((ScanMessageResult)result) : ScanMessageResult.Error);
			}

			// Token: 0x06000083 RID: 131 RVA: 0x00006814 File Offset: 0x00004A14
			public bool EnterWrapper(ContentFilterAgentFactory.ContentFilterWrapper wrapper)
			{
				bool flag = wrapper.Enter();
				this.wrapper = (flag ? wrapper : null);
				return flag;
			}

			// Token: 0x06000084 RID: 132 RVA: 0x00006838 File Offset: 0x00004A38
			public void ExitWrapper()
			{
				ContentFilterAgentFactory.ContentFilterWrapper contentFilterWrapper = Interlocked.Exchange<ContentFilterAgentFactory.ContentFilterWrapper>(ref this.wrapper, null);
				if (contentFilterWrapper != null)
				{
					if (Interlocked.CompareExchange(ref contentFilterWrapper.PingPending, 0, 1) == 1)
					{
						ExTraceGlobals.ScanMessageTracer.TraceError((long)this.GetHashCode(), "Ping request returned.");
						return;
					}
					contentFilterWrapper.Exit();
				}
			}

			// Token: 0x06000085 RID: 133 RVA: 0x00006882 File Offset: 0x00004A82
			public ScanMessageResult Ping(ContentFilterAgentFactory.ContentFilterWrapper wrapper)
			{
				this.wrapper = wrapper;
				return this.Submit(wrapper);
			}

			// Token: 0x06000086 RID: 134 RVA: 0x00006894 File Offset: 0x00004A94
			public ScanMessageResult Submit(ContentFilterAgentFactory.ContentFilterWrapper wrapper)
			{
				try
				{
					Util.InvokeExLapi(wrapper.ComProxy, new ComProxy.AsyncCompletionCallback(this.ScanCompletedComCallback), this.asyncState.ComArguments, this.asyncState.EndOfDataEventArgs.MailItem, Constants.RequestTypes.ScanMessage);
					return (ScanMessageResult)this.asyncState.ComArguments.GetInt32(2);
				}
				catch (ArgumentException arg)
				{
					ExTraceGlobals.ScanMessageTracer.TraceError<ArgumentException>((long)this.GetHashCode(), "Error when reading result from ExLapi property bag: {0}.", arg);
				}
				catch (InvalidOperationException arg2)
				{
					ExTraceGlobals.ScanMessageTracer.TraceError<InvalidOperationException>((long)this.GetHashCode(), "Failed to open Mimestream. Error: {0}", arg2);
				}
				catch (ExchangeDataException arg3)
				{
					ExTraceGlobals.ScanMessageTracer.TraceError<ExchangeDataException>((long)this.GetHashCode(), "Failed to open Mimestream. Error: {0}", arg3);
				}
				catch (IOException ex)
				{
					int hrforException = Marshal.GetHRForException(ex);
					if (hrforException != -2147024784)
					{
						throw;
					}
					ExTraceGlobals.ScanMessageTracer.TraceError<IOException>((long)this.GetHashCode(), "Error when reading Mimestream. Error: {0}", ex);
				}
				return ScanMessageResult.Error;
			}

			// Token: 0x06000087 RID: 135 RVA: 0x000069AC File Offset: 0x00004BAC
			private void Finish()
			{
				if (Interlocked.Exchange(ref this.finished, 1) == 0)
				{
					this.dispatcher.OnRequestFinished(this);
					if (this.asyncCallback != null)
					{
						ExTraceGlobals.ScanMessageTracer.TraceDebug((long)this.GetHashCode(), "Invoking the agent callback.");
						this.asyncCallback(this);
					}
				}
			}

			// Token: 0x06000088 RID: 136 RVA: 0x000069FD File Offset: 0x00004BFD
			private void ScanCompletedComCallback(ComArguments arguments)
			{
				this.ExitWrapper();
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnScanMessageCompleted));
			}

			// Token: 0x06000089 RID: 137 RVA: 0x00006A18 File Offset: 0x00004C18
			private void OnScanMessageCompleted(object state)
			{
				ExTraceGlobals.ScanMessageTracer.TraceDebug((long)this.GetHashCode(), "Filter has completed scanning message.");
				try
				{
					this.scanResult = (ScanMessageResult)this.asyncState.ComArguments.GetInt32(3);
				}
				catch (ArgumentException arg)
				{
					ExTraceGlobals.ScanMessageTracer.TraceError<ArgumentException>((long)this.GetHashCode(), "Error when reading result from ExLapi property bag: {0}.", arg);
					this.scanResult = ScanMessageResult.Error;
				}
				catch (Exception arg2)
				{
					ExTraceGlobals.ScanMessageTracer.TraceError<Exception>((long)this.GetHashCode(), "Unhandled exception in ScanRequest.OnScanMessageCompleted: {0}.", arg2);
					this.exception = arg2;
					this.scanResult = ScanMessageResult.Error;
				}
				this.Finish();
			}

			// Token: 0x040000AC RID: 172
			private readonly ContentFilterAgentFactory.Dispatcher dispatcher;

			// Token: 0x040000AD RID: 173
			private readonly AsyncCallback asyncCallback;

			// Token: 0x040000AE RID: 174
			private readonly ContentFilterAgent.AsyncState asyncState;

			// Token: 0x040000AF RID: 175
			private readonly DateTime creationTime;

			// Token: 0x040000B0 RID: 176
			private ContentFilterAgentFactory.ContentFilterWrapper wrapper;

			// Token: 0x040000B1 RID: 177
			private ScanMessageResult scanResult;

			// Token: 0x040000B2 RID: 178
			private Exception exception;

			// Token: 0x040000B3 RID: 179
			private int finished;
		}

		// Token: 0x0200001C RID: 28
		private sealed class ContentFilterWrapper : IDisposable
		{
			// Token: 0x0600008A RID: 138 RVA: 0x00006AC0 File Offset: 0x00004CC0
			public ContentFilterWrapper(ComProxy comProxy)
			{
				if (comProxy == null)
				{
					throw new ArgumentNullException("comProxy");
				}
				this.semaphore = new Semaphore(15, 15);
				this.recycledEvent = new AutoResetEvent(false);
				this.comProxy = comProxy;
			}

			// Token: 0x0600008B RID: 139 RVA: 0x00006AF8 File Offset: 0x00004CF8
			~ContentFilterWrapper()
			{
				this.Dispose(false);
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600008C RID: 140 RVA: 0x00006B28 File Offset: 0x00004D28
			public ComProxy ComProxy
			{
				get
				{
					return this.comProxy;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600008D RID: 141 RVA: 0x00006B30 File Offset: 0x00004D30
			public int TimeoutCount
			{
				get
				{
					return this.timeoutCount;
				}
			}

			// Token: 0x0600008E RID: 142 RVA: 0x00006B38 File Offset: 0x00004D38
			public bool Enter()
			{
				bool flag = this.semaphore.WaitOne(30000, false);
				if (flag)
				{
					this.timeoutCount = 0;
				}
				else
				{
					this.timeoutCount++;
				}
				return flag;
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00006B72 File Offset: 0x00004D72
			public void Exit()
			{
				this.semaphore.Release();
			}

			// Token: 0x06000090 RID: 144 RVA: 0x00006B80 File Offset: 0x00004D80
			public bool Recycle()
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ShutdownProc));
				return this.recycledEvent.WaitOne(15000, false);
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00006BA5 File Offset: 0x00004DA5
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00006BB4 File Offset: 0x00004DB4
			private void Dispose(bool disposing)
			{
				if (!this.disposed && disposing)
				{
					this.comProxy.Dispose();
					this.semaphore.Close();
					this.recycledEvent.Close();
					this.disposed = true;
				}
			}

			// Token: 0x06000093 RID: 147 RVA: 0x00006BEC File Offset: 0x00004DEC
			private void ShutdownProc(object state)
			{
				try
				{
					Util.InvokeExLapi(this.comProxy, null, new ComArguments(), null, Constants.RequestTypes.Shutdown);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2147023170)
					{
						this.recycledEvent.Set();
					}
					else
					{
						ExTraceGlobals.ScanMessageTracer.TraceDebug<Exception>((long)this.GetHashCode(), "Error sending shutdown message to the wrapper. Details: {0}", ex);
						Util.LogWrapperRecycleError(ex);
					}
				}
				catch (Exception ex2)
				{
					ExTraceGlobals.ScanMessageTracer.TraceDebug<Exception>((long)this.GetHashCode(), "Error sending shutdown message to the wrapper. Details: {0}", ex2);
					Util.LogWrapperRecycleError(ex2);
				}
			}

			// Token: 0x040000B4 RID: 180
			private const int ScanMessageTimeout = 30000;

			// Token: 0x040000B5 RID: 181
			private const int ShutdownTimeout = 15000;

			// Token: 0x040000B6 RID: 182
			public int PingPending;

			// Token: 0x040000B7 RID: 183
			private readonly ComProxy comProxy;

			// Token: 0x040000B8 RID: 184
			private readonly Semaphore semaphore;

			// Token: 0x040000B9 RID: 185
			private readonly AutoResetEvent recycledEvent;

			// Token: 0x040000BA RID: 186
			private int timeoutCount;

			// Token: 0x040000BB RID: 187
			private bool disposed;
		}
	}
}
