using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Mserve;
using Microsoft.Exchange.Net.Mserve.ProvisionRequest;
using Microsoft.Exchange.Net.Mserve.ProvisionResponse;
using Microsoft.Exchange.Net.Mserve.SettingsRequest;
using Microsoft.Exchange.Net.Mserve.SettingsResponse;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Win32;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x0200088D RID: 2189
	internal class MserveWebService : IMserveService
	{
		// Token: 0x06002E85 RID: 11909 RVA: 0x00066820 File Offset: 0x00064A20
		public MserveWebService(string provisionUrl, string settingsUrl, string remoteCertSubject, string clientToken, bool batchMode)
		{
			if (string.IsNullOrEmpty(provisionUrl))
			{
				throw new ArgumentNullException("provisionUrl");
			}
			if (string.IsNullOrEmpty(settingsUrl))
			{
				throw new ArgumentNullException("settingsUrl");
			}
			if (string.IsNullOrEmpty(clientToken))
			{
				throw new ArgumentNullException("clientToken");
			}
			this.isMicrosoftHostedOnly = Datacenter.IsMicrosoftHostedOnly(true);
			this.remoteCertSubject = remoteCertSubject;
			this.batchMode = batchMode;
			string query = string.Format(CultureInfo.InvariantCulture, "Dspk={0}", new object[]
			{
				clientToken
			});
			this.provisionUriWithoutQuery = new Uri(provisionUrl);
			this.provisionUri = new UriBuilder(this.provisionUriWithoutQuery)
			{
				Query = query
			}.Uri;
			this.settingUri = new UriBuilder(new Uri(settingsUrl))
			{
				Query = query
			}.Uri;
			this.perfCounters = new MservePerfCounters();
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06002E86 RID: 11910 RVA: 0x0006691B File Offset: 0x00064B1B
		// (set) Token: 0x06002E87 RID: 11911 RVA: 0x00066923 File Offset: 0x00064B23
		public string LastResponseDiagnosticInfo { get; private set; }

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x0006692C File Offset: 0x00064B2C
		// (set) Token: 0x06002E89 RID: 11913 RVA: 0x00066934 File Offset: 0x00064B34
		public string LastResponseTransactionId { get; private set; }

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x0006693D File Offset: 0x00064B3D
		// (set) Token: 0x06002E8B RID: 11915 RVA: 0x00066945 File Offset: 0x00064B45
		public string LastIpUsed { get; private set; }

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06002E8C RID: 11916 RVA: 0x0006694E File Offset: 0x00064B4E
		// (set) Token: 0x06002E8D RID: 11917 RVA: 0x00066956 File Offset: 0x00064B56
		public bool TrackDuplicatedAddEntries
		{
			get
			{
				return this.trackDuplicatedAddEntries;
			}
			set
			{
				this.trackDuplicatedAddEntries = value;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x00066960 File Offset: 0x00064B60
		// (set) Token: 0x06002E8F RID: 11919 RVA: 0x00066B08 File Offset: 0x00064D08
		internal static MserveCacheServiceMode CurrentMserveCacheServiceMode
		{
			get
			{
				int tickCount = Environment.TickCount;
				if (MserveWebService.whenRegKeyLastChecked == 0 || TickDiffer.Elapsed(MserveWebService.whenRegKeyLastChecked, tickCount) > MserveWebService.MserveCacheServiceRegistryCheckInterval)
				{
					lock (MserveWebService.modeLockObject)
					{
						MserveCacheServiceMode mserveCacheServiceMode = MserveWebService.currentMserveCacheServiceMode;
						try
						{
							using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs"))
							{
								if (registryKey != null)
								{
									int num = (int)registryKey.GetValue("MserveCacheServiceEnabled", 0);
									if (Enum.IsDefined(typeof(MserveCacheServiceMode), num))
									{
										MserveWebService.currentMserveCacheServiceMode = (MserveCacheServiceMode)num;
										if (MserveWebService.currentMserveCacheServiceMode != mserveCacheServiceMode)
										{
											MserveCacheServiceProvider.EventLog.LogEvent(CommonEventLogConstants.Tuple_MserveCacheServiceModeChanged, "MserveCacheServiceModeChanged", new object[]
											{
												mserveCacheServiceMode.ToString(),
												MserveWebService.currentMserveCacheServiceMode.ToString()
											});
										}
									}
									else
									{
										MserveWebService.currentMserveCacheServiceMode = MserveCacheServiceMode.NotEnabled;
									}
								}
								else
								{
									MserveWebService.currentMserveCacheServiceMode = MserveCacheServiceMode.NotEnabled;
								}
							}
						}
						catch (Exception ex)
						{
							ExTraceGlobals.MserveCacheServiceTracer.TraceError<string>(0L, "Exception occured while reading MserveCacheServiceMode from registry. Exception : {0}", ex.ToString());
							MserveWebService.currentMserveCacheServiceMode = MserveCacheServiceMode.NotEnabled;
						}
						if (mserveCacheServiceMode != MserveWebService.currentMserveCacheServiceMode)
						{
							ExTraceGlobals.MserveCacheServiceTracer.TraceDebug<MserveCacheServiceMode, MserveCacheServiceMode>(0L, "The MserveCacheServiceMode has changed from {0} to {1}", mserveCacheServiceMode, MserveWebService.currentMserveCacheServiceMode);
						}
						MserveWebService.whenRegKeyLastChecked = Environment.TickCount;
					}
				}
				return MserveWebService.currentMserveCacheServiceMode;
			}
			set
			{
				lock (MserveWebService.modeLockObject)
				{
					MserveWebService.currentMserveCacheServiceMode = value;
				}
			}
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x00066B48 File Offset: 0x00064D48
		public static bool IsTransientException(MserveException exception)
		{
			return exception.InnerException != null && (exception.InnerException is WebException || exception.InnerException is IOException || exception.InnerException is TransientException);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x00066B7E File Offset: 0x00064D7E
		public void Initialize()
		{
			this.Initialize(0);
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x00066B88 File Offset: 0x00064D88
		public void Initialize(int initialChunkSize)
		{
			lock (MserveWebService.mutex)
			{
				if (!MserveWebService.initialized)
				{
					CertificateValidationManager.RegisterCallback("MserveWebService", new RemoteCertificateValidationCallback(MserveWebService.ValidateCertificate));
					MserveWebService.staticRemoteCertSubject = this.remoteCertSubject;
				}
				if (initialChunkSize == 0)
				{
					if (MserveWebService.chunkSize == 0)
					{
						MserveWebService.chunkSize = this.FetchChunkSize();
					}
				}
				else
				{
					MserveWebService.chunkSize = initialChunkSize;
				}
				MserveWebService.initialized = true;
			}
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x00066C0C File Offset: 0x00064E0C
		public void SetConnectionLeaseTimeout(int timeout)
		{
			Uri[] array = new Uri[]
			{
				this.provisionUri,
				this.settingUri
			};
			foreach (Uri address in array)
			{
				try
				{
					ServicePoint servicePoint = ServicePointManager.FindServicePoint(address);
					servicePoint.ConnectionLeaseTimeout = timeout;
				}
				catch (InvalidOperationException)
				{
				}
			}
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x00066C74 File Offset: 0x00064E74
		public ICancelableAsyncResult BeginReadPartnerId(string address, CancelableAsyncCallback callback, object state)
		{
			int num = -1;
			if (string.IsNullOrEmpty(address))
			{
				throw new ArgumentNullException("address");
			}
			Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision provision = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision();
			provision.Delete = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType[1];
			provision.Add = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType[1];
			provision.Read = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType[1];
			Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType accountType = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType();
			accountType.Name = address;
			provision.Read[0] = accountType;
			ExTraceGlobals.DeltaSyncAPITracer.TraceDebug<string>((long)this.GetHashCode(), "Read entry {0}", address);
			if (this.UseOfflineMserveCacheServiceFirst() || this.UseOfflineMserveCacheService())
			{
				Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provision2 = new Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision();
				provision2.Responses = new ProvisionResponses();
				CommandStatusCode commandStatusCode = this.ProcessReadRequest(provision, provision2);
				if (commandStatusCode == CommandStatusCode.Success)
				{
					provision2.Status = 1;
					this.ProcessOneOffReadResponse(provision2, out num);
					ExTraceGlobals.MserveCacheServiceTracer.TraceDebug<int>((long)this.GetHashCode(), "Processing one-off read request in BeginReadPartnerId() for Mserve Cache. PartnerId = {0}", num);
				}
			}
			HttpSessionConfig httpSessionConfig = this.InitializeHttpSessionConfig(true);
			httpSessionConfig.RequestStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(httpSessionConfig.RequestStream, Encoding.UTF8);
			MserveWebService.provRequestSerializer.Serialize(streamWriter, provision);
			httpSessionConfig.RequestStream.Position = 0L;
			this.LogProvisionRequest(this.provisionUriWithoutQuery.ToString());
			HttpClient httpClient = new HttpClient();
			OutstandingAsyncReadConfig outstandingAsyncReadConfig = new OutstandingAsyncReadConfig(httpClient, httpSessionConfig, streamWriter, callback, num, state);
			new CancelableHttpAsyncResult(callback, state, httpClient);
			ICancelableAsyncResult internalResult = httpClient.BeginDownload(this.provisionUri, httpSessionConfig, MserveWebService.asyncApiCallback, outstandingAsyncReadConfig);
			return new CancelableMservAsyncResult(internalResult, outstandingAsyncReadConfig, state);
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x00066DDC File Offset: 0x00064FDC
		public int EndReadPartnerId(ICancelableAsyncResult asyncResult)
		{
			CancelableMservAsyncResult cancelableMservAsyncResult = (CancelableMservAsyncResult)asyncResult;
			int result = -1;
			Exception ex = null;
			string text = string.Empty;
			Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision provision = null;
			int result2;
			try
			{
				this.perfCounters.UpdateTotalRequestPerfCounters(1);
				if (this.UseOfflineMserveCacheServiceFirst() && cancelableMservAsyncResult.ReadConfig.CachePartnerId != -1)
				{
					result2 = cancelableMservAsyncResult.ReadConfig.CachePartnerId;
				}
				else
				{
					if (this.UseRealMserveWebService())
					{
						this.perfCounters.UpdateRequestPerfCountersForMserveWebService(1, 0, 0);
						DownloadResult downloadResult = cancelableMservAsyncResult.ReadConfig.Client.EndDownload(cancelableMservAsyncResult.InternalResult);
						if (downloadResult.IsSucceeded)
						{
							Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provResponse = null;
							StreamReader streamReader = new StreamReader(downloadResult.ResponseStream, Encoding.UTF8);
							try
							{
								provResponse = (Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision)MserveWebService.provResponseSerializer.Deserialize(streamReader);
							}
							catch (InvalidOperationException ex2)
							{
								ex = ((ex2.InnerException != null) ? ex2.InnerException : ex2);
								text = "Failure during deserialization";
							}
							finally
							{
								if (streamReader != null)
								{
									streamReader.Close();
								}
								if (downloadResult.ResponseStream != null)
								{
									downloadResult.ResponseStream.Close();
									downloadResult.ResponseStream = null;
								}
							}
							if (ex == null)
							{
								this.ProcessOneOffReadResponse(provResponse, out result);
								return result;
							}
						}
						else
						{
							ex = downloadResult.Exception;
							text = "Exception during Mserve lookup";
						}
					}
					if (ex != null)
					{
						this.perfCounters.UpdateFailurePerfCountersForMserveWebService(1);
					}
					if (this.UseOfflineMserveCacheService() && cancelableMservAsyncResult.ReadConfig.CachePartnerId != -1)
					{
						result2 = cancelableMservAsyncResult.ReadConfig.CachePartnerId;
					}
					else
					{
						if (provision != null && provision.Read != null && provision.Read.Length > 0)
						{
							this.CleanupQueueAndResultSet(provision.Read, 1);
						}
						this.perfCounters.UpdateTotalFailuresPerfCounters(1);
						if (!string.IsNullOrEmpty(text))
						{
							throw new MserveException(text, ex);
						}
						result2 = -1;
					}
				}
			}
			finally
			{
				cancelableMservAsyncResult.ReadConfig.XmlStreamWriter.Dispose();
				cancelableMservAsyncResult.ReadConfig.Client.Dispose();
				cancelableMservAsyncResult.ReadConfig.SessionConfig.RequestStream.Dispose();
			}
			return result2;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x00067008 File Offset: 0x00065208
		public List<RecipientSyncOperation> Synchronize(List<RecipientSyncOperation> opList)
		{
			foreach (RecipientSyncOperation op in opList)
			{
				this.QueueOperation(op);
			}
			List<RecipientSyncOperation> result = new List<RecipientSyncOperation>(this.completedResultSet);
			this.completedResultSet.Clear();
			return result;
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x00067070 File Offset: 0x00065270
		public List<RecipientSyncOperation> Synchronize(RecipientSyncOperation op)
		{
			this.QueueOperation(op);
			List<RecipientSyncOperation> result = new List<RecipientSyncOperation>(this.completedResultSet);
			this.completedResultSet.Clear();
			return result;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x0006709C File Offset: 0x0006529C
		public List<RecipientSyncOperation> Synchronize()
		{
			return this.Synchronize(true, null);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000670BC File Offset: 0x000652BC
		public List<RecipientSyncOperation> Synchronize(bool keepAlive, int? timeout)
		{
			this.Flush(keepAlive, timeout);
			List<RecipientSyncOperation> list = new List<RecipientSyncOperation>(this.resultSet);
			list.AddRange(this.completedResultSet);
			this.Reset();
			return list;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000670F0 File Offset: 0x000652F0
		public void Reset()
		{
			this.pendingQueue.Clear();
			this.resultSet.Clear();
			this.completedResultSet.Clear();
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x00067114 File Offset: 0x00065314
		private static void AsyncApiCallback(ICancelableAsyncResult result)
		{
			OutstandingAsyncReadConfig outstandingAsyncReadConfig = (OutstandingAsyncReadConfig)result.AsyncState;
			CancelableMservAsyncResult asyncResult = new CancelableMservAsyncResult(result, outstandingAsyncReadConfig, outstandingAsyncReadConfig.ClientState);
			outstandingAsyncReadConfig.ClientCallback(asyncResult);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x00067147 File Offset: 0x00065347
		private static bool IsRetryableDeltaSyncError(int status)
		{
			return status >= 5000;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x00067154 File Offset: 0x00065354
		private HttpSessionConfig InitializeHttpSessionConfig(bool keepAlive)
		{
			HttpSessionConfig httpSessionConfig = new HttpSessionConfig(60000);
			httpSessionConfig.Method = "POST";
			httpSessionConfig.UserAgent = "ExchangeHostedServices/1.0";
			httpSessionConfig.ContentType = "text/xml";
			httpSessionConfig.Headers = new WebHeaderCollection();
			httpSessionConfig.Headers[CertificateValidationManager.ComponentIdHeaderName] = "MserveWebService";
			httpSessionConfig.KeepAlive = keepAlive;
			return httpSessionConfig;
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000671B8 File Offset: 0x000653B8
		private void Flush(bool keepAlive, int? timeout)
		{
			Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision provision = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision();
			ExTraceGlobals.DeltaSyncAPITracer.TraceDebug<int, int>((long)this.GetHashCode(), "About to flush. The pending queue count is {0} and chunk size is {1}", this.pendingQueue.Count, MserveWebService.chunkSize);
			while (this.pendingQueue.Count > 0)
			{
				provision.Delete = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType[MserveWebService.chunkSize];
				provision.Add = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType[MserveWebService.chunkSize];
				provision.Read = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType[MserveWebService.chunkSize];
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				foreach (KeyValuePair<string, RecipientPendingOperation> keyValuePair in this.pendingQueue)
				{
					Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType accountType = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType();
					accountType.Name = keyValuePair.Key;
					switch (keyValuePair.Value.Type)
					{
					case OperationType.Add:
						accountType.PartnerID = keyValuePair.Value.RecipientSyncOperation.PartnerId.ToString(CultureInfo.InvariantCulture);
						provision.Add[num2++] = accountType;
						this.LogProvisionAccountDetail("Add", accountType);
						break;
					case OperationType.Delete:
						accountType.PartnerID = keyValuePair.Value.RecipientSyncOperation.PartnerId.ToString(CultureInfo.InvariantCulture);
						provision.Delete[num++] = accountType;
						this.LogProvisionAccountDetail("Delete", accountType);
						break;
					case OperationType.Read:
						provision.Read[num3++] = accountType;
						this.LogProvisionAccountDetail("Read", accountType);
						break;
					default:
						throw new MserveException(string.Concat(new object[]
						{
							"Synchronization failed when preparing request for ",
							accountType.Name,
							" because of unknown type ",
							keyValuePair.Value.Type
						}), null);
					}
					num4++;
					ExTraceGlobals.DeltaSyncAPITracer.TraceDebug<OperationType, string, string>((long)this.GetHashCode(), "{0} entry {1} with partner Id {2}", keyValuePair.Value.Type, accountType.Name, accountType.PartnerID);
					if (num4 == MserveWebService.chunkSize)
					{
						break;
					}
				}
				int num5 = this.pendingQueue.Count - num4;
				Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provResponse = this.SendProvisionRequest(provision, keepAlive, timeout);
				if (!this.ProcessProvisionResponse(provResponse))
				{
					ExTraceGlobals.DeltaSyncAPITracer.TraceDebug<int, int, int>((long)this.GetHashCode(), "Retrying request.  Attempted to send {0}, pending queue count is {1}, chunk size is {2}", num4, this.pendingQueue.Count, MserveWebService.chunkSize);
				}
				else if (this.pendingQueue.Count != num5)
				{
					throw new MserveException(string.Format("Synchronization failed when flushing because {0} items were left in the pending queue, {1} items expected", this.pendingQueue.Count, num5), null);
				}
			}
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x00067484 File Offset: 0x00065684
		private void QueueOperation(RecipientSyncOperation op)
		{
			this.resultSet.Add(op);
			foreach (string text in op.RemovedEntries)
			{
				if (this.pendingQueue.ContainsKey(text))
				{
					if (!this.pendingQueue[text].IsDelete)
					{
						ExTraceGlobals.DeltaSyncAPITracer.TraceError<string>((long)this.GetHashCode(), "Synchronization failed when inserting remove entry {0} when it is already in the add or read entries.", text);
						throw new MserveException("Synchronization failed when inserting remove entry " + text + " when it is already in the add or read entries", null);
					}
					ExTraceGlobals.DeltaSyncAPITracer.TraceWarning<string>((long)this.GetHashCode(), "Duplicate delete entry for {0} detected. Ignore it", text);
				}
				else
				{
					RecipientPendingOperation value = new RecipientPendingOperation(op, OperationType.Delete);
					this.pendingQueue.Add(text, value);
					if (this.pendingQueue.Count == MserveWebService.chunkSize)
					{
						this.Flush(true, null);
					}
				}
			}
			foreach (string text2 in op.AddedEntries)
			{
				if (this.pendingQueue.ContainsKey(text2))
				{
					if (!this.pendingQueue[text2].IsAdd)
					{
						ExTraceGlobals.DeltaSyncAPITracer.TraceError<string>((long)this.GetHashCode(), "Synchronization failed when inserting add entry {0} when it is already in the remove or read entries.", text2);
						throw new MserveException("Synchronization failed when inserting add entry " + text2 + " when it is already in the remove or read entries", null);
					}
					ExTraceGlobals.DeltaSyncAPITracer.TraceError<string>((long)this.GetHashCode(), "Duplicate add entry for {0} detected. Ignore it", text2);
				}
				else
				{
					RecipientPendingOperation value2 = new RecipientPendingOperation(op, OperationType.Add);
					this.pendingQueue.Add(text2, value2);
					if (this.pendingQueue.Count == MserveWebService.chunkSize)
					{
						this.Flush(true, null);
					}
				}
			}
			foreach (string text3 in op.ReadEntries)
			{
				if (this.pendingQueue.ContainsKey(text3))
				{
					if (!this.pendingQueue[text3].IsRead)
					{
						ExTraceGlobals.DeltaSyncAPITracer.TraceError<string>((long)this.GetHashCode(), "Synchronization failed when inserting read entry {0} when it is already in the remove or add entries.", text3);
						throw new MserveException("Synchronization failed when inserting read entry " + text3 + " when it is already in the remove or add entries", null);
					}
					ExTraceGlobals.DeltaSyncAPITracer.TraceError<string>((long)this.GetHashCode(), "Duplicate read entry for {0} detected. Ignore it", text3);
				}
				else
				{
					RecipientPendingOperation value3 = new RecipientPendingOperation(op, OperationType.Read);
					this.pendingQueue.Add(text3, value3);
					if (this.pendingQueue.Count == MserveWebService.chunkSize)
					{
						this.Flush(true, null);
					}
				}
			}
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x00067780 File Offset: 0x00065980
		private Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision SendProvisionRequest(Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision provRequest, bool keepAlive, int? timeout)
		{
			Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provision = null;
			Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provision2 = null;
			Exception ex = null;
			string text = string.Empty;
			bool flag = false;
			int num = provRequest.Add.Count((Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType s) => s != null);
			int num2 = provRequest.Delete.Count((Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType s) => s != null);
			int num3 = provRequest.Read.Count((Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType s) => s != null);
			this.perfCounters.UpdateTotalRequestPerfCounters(num3 + num + num2);
			if (num3 > 0 && this.UseOfflineMserveCacheServiceFirst())
			{
				this.ProcessAndRemoveReadRequest(provRequest, out provision2, true, ref flag);
			}
			num3 = provRequest.Read.Count((Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType s) => s != null);
			int num4 = num3 + num2 + num;
			if (num4 > 0 && this.UseRealMserveWebService())
			{
				this.perfCounters.UpdateRequestPerfCountersForMserveWebService(num3, num, num2);
				HttpSessionConfig httpSessionConfig = this.InitializeHttpSessionConfig(keepAlive);
				if (timeout != null)
				{
					httpSessionConfig.Timeout = timeout.Value;
				}
				this.LastResponseDiagnosticInfo = null;
				this.LastResponseTransactionId = null;
				this.LastIpUsed = this.ResolveToIpAddress(this.provisionUri.Host, false);
				using (httpSessionConfig.RequestStream = new MemoryStream())
				{
					MserveWebService.provRequestSerializer.Serialize(new StreamWriter(httpSessionConfig.RequestStream, Encoding.UTF8), provRequest);
					httpSessionConfig.RequestStream.Position = 0L;
					this.LogProvisionRequest(this.provisionUriWithoutQuery.ToString());
					using (HttpClient httpClient = new HttpClient())
					{
						ICancelableAsyncResult asyncResult = httpClient.BeginDownload(this.provisionUri, httpSessionConfig, null, null);
						DownloadResult downloadResult = httpClient.EndDownload(asyncResult);
						if (downloadResult.ResponseHeaders != null)
						{
							this.LastResponseDiagnosticInfo = downloadResult.ResponseHeaders["X-WindowsLive-Hydra"];
							this.LastResponseTransactionId = downloadResult.ResponseHeaders["X-TransactionID"];
						}
						else
						{
							this.LastIpUsed = this.ResolveToIpAddress(this.provisionUri.Host, true);
						}
						if (downloadResult.IsSucceeded)
						{
							try
							{
								provision = (Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision)MserveWebService.provResponseSerializer.Deserialize(new StreamReader(downloadResult.ResponseStream, Encoding.UTF8));
							}
							catch (InvalidOperationException ex2)
							{
								ex = ((ex2.InnerException != null) ? ex2.InnerException : ex2);
								text = "Failure during deserialization";
							}
							finally
							{
								if (downloadResult.ResponseStream != null)
								{
									downloadResult.ResponseStream.Close();
									downloadResult.ResponseStream = null;
								}
							}
							if (ex == null)
							{
								if (flag)
								{
									int num5 = provision2.Responses.Read.Count((Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType s) => s != null);
									if (num5 > 0)
									{
										provision.Responses.Read = new Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType[num5];
										for (int i = 0; i < num5; i++)
										{
											provision.Responses.Read[i] = new Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType();
											provision.Responses.Read[i] = provision2.Responses.Read[i];
										}
									}
								}
								return provision;
							}
						}
						else
						{
							ex = downloadResult.Exception;
							text = "Exception during Mserve lookup";
						}
					}
				}
			}
			if (ex != null)
			{
				this.perfCounters.UpdateFailurePerfCountersForMserveWebService(num4);
			}
			if (num4 > 0 && this.UseOfflineMserveCacheService())
			{
				provision = new Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision();
				provision.Responses = new ProvisionResponses();
				CommandStatusCode commandStatusCode = this.ProcessReadRequest(provRequest, provision);
				if (ex != null && commandStatusCode != CommandStatusCode.Success)
				{
					text = string.Format("{0}. Also failed in Mserve Cache lookup. Cache failure type: {1}.", text, commandStatusCode.ToString());
					throw new MserveException(text, ex);
				}
				provision.Status = 1;
				ExTraceGlobals.MserveCacheServiceTracer.TraceDebug<int>((long)this.GetHashCode(), "Successfully processed provisioning request using MserveCacheService. ReadRequest count {0}", (provision.Responses.Read != null) ? provision.Responses.Read.Length : 0);
				return provision;
			}
			else
			{
				if (provision2 != null && num == 0 && num2 == 0)
				{
					return provision2;
				}
				this.CleanupQueueAndResultSet(provRequest.Read, num3);
				this.CleanupQueueAndResultSet(provRequest.Add, num);
				this.CleanupQueueAndResultSet(provRequest.Delete, num2);
				this.perfCounters.UpdateTotalFailuresPerfCounters(num4);
				throw new MserveException(text, ex);
			}
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x00067C3C File Offset: 0x00065E3C
		private CommandStatusCode ProcessAddRequest(Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision provRequest, Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provResponse)
		{
			int num = provRequest.Add.Count((Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType s) => s != null);
			if (num > 0)
			{
				this.CleanupQueueAndResultSet(provRequest.Add, num);
				throw new InvalidMserveRequestException();
			}
			return CommandStatusCode.Success;
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x00067C94 File Offset: 0x00065E94
		private CommandStatusCode ProcessDeleteRequest(Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision provRequest, Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provResponse)
		{
			int num = provRequest.Delete.Count((Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType s) => s != null);
			if (num > 0)
			{
				this.CleanupQueueAndResultSet(provRequest.Delete, num);
				throw new InvalidMserveRequestException();
			}
			return CommandStatusCode.Success;
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x00067CEC File Offset: 0x00065EEC
		private CommandStatusCode ProcessReadRequest(Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision provRequest, Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provResponse)
		{
			CommandStatusCode result = CommandStatusCode.Success;
			int num = provRequest.Read.Count((Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType s) => s != null);
			if (num > 0)
			{
				provResponse.Responses.Read = new Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType[num];
				for (int i = 0; i < num; i++)
				{
					Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType accountType = provRequest.Read[i];
					if (accountType != null)
					{
						provResponse.Responses.Read[i] = new Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType();
						provResponse.Responses.Read[i].Name = accountType.Name;
						this.perfCounters.UpdateRequestPerfCountersForMserveCacheService(1, 0, 0);
						result = this.ProcessCacheLookup(ref provResponse.Responses.Read[i], true);
					}
				}
			}
			return result;
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x00067DA4 File Offset: 0x00065FA4
		private CommandStatusCode ProcessCacheLookup(ref Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType readResponse, bool updateFailurePerfCounters)
		{
			CommandStatusCode result = CommandStatusCode.Success;
			try
			{
				MserveCacheServiceProvider instance = MserveCacheServiceProvider.GetInstance();
				string text = instance.ReadMserveData(readResponse.Name);
				if (text.Equals(-1.ToString()))
				{
					result = CommandStatusCode.UserDoesNotExist;
					if (updateFailurePerfCounters)
					{
						this.UpdateCacheFailurePerfCounters();
					}
				}
				ExTraceGlobals.MserveCacheServiceTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Successfully read Mserve entry from cache for request {0}, partnerId/minorPartnerId = {1}", readResponse.Name, text);
				readResponse.PartnerID = text;
				readResponse.Status = 1;
			}
			catch (Exception ex)
			{
				if (updateFailurePerfCounters)
				{
					this.UpdateCacheFailurePerfCounters();
				}
				ExTraceGlobals.MserveCacheServiceTracer.TraceError<string, string>((long)this.GetHashCode(), "Got exception while reading Mserve entry in cache for request {0}. Exception: {1}", readResponse.Name, ex.ToString());
				readResponse.PartnerID = -1.ToString();
				readResponse.Status = 5101;
				result = CommandStatusCode.MserveCacheServiceChannelFaulted;
			}
			return result;
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x00067E7C File Offset: 0x0006607C
		private void UpdateCacheFailurePerfCounters()
		{
			this.perfCounters.UpdateFailurePerfCountersForMservCacheService(1);
			if (MserveWebService.CurrentMserveCacheServiceMode != MserveCacheServiceMode.EnabledForReadOnly)
			{
				this.perfCounters.UpdateTotalFailuresPerfCounters(1);
			}
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x00067EA0 File Offset: 0x000660A0
		private void CleanupQueueAndResultSet(Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType[] requests, int count)
		{
			for (int i = 0; i < count; i++)
			{
				Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType accountType = requests[i];
				if (this.pendingQueue.ContainsKey(accountType.Name))
				{
					RecipientSyncOperation recipientSyncOperation = this.pendingQueue[accountType.Name].RecipientSyncOperation;
					this.pendingQueue.Remove(accountType.Name);
					if (this.resultSet.Contains(recipientSyncOperation))
					{
						this.resultSet.Remove(recipientSyncOperation);
					}
				}
			}
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x00067F20 File Offset: 0x00066120
		private bool ProcessAndRemoveReadRequest(Microsoft.Exchange.Net.Mserve.ProvisionRequest.Provision provRequest, out Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provResponseForReadOnly, bool removeReadRequests, ref bool mergeReadResponseNeeded)
		{
			provResponseForReadOnly = new Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision();
			provResponseForReadOnly.Responses = new ProvisionResponses();
			if (this.ProcessReadRequest(provRequest, provResponseForReadOnly) != CommandStatusCode.Success)
			{
				ExTraceGlobals.MserveCacheServiceTracer.TraceDebug((long)this.GetHashCode(), "CurrentMserveCacheServiceMode is EnabledForReadOnly, but some entries could not be found in Cache. Will try to read again from Mserve Web service");
				provResponseForReadOnly = null;
				return false;
			}
			ExTraceGlobals.MserveCacheServiceTracer.TraceDebug((long)this.GetHashCode(), "Mserve Read requests are processed successfully from Cache");
			if (removeReadRequests)
			{
				ExTraceGlobals.MserveCacheServiceTracer.TraceDebug((long)this.GetHashCode(), "Removing read requests from ProvisionRequest");
				int num = provRequest.Read.Count((Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType s) => s != null);
				if (num > 0)
				{
					for (int i = 0; i < num; i++)
					{
						provRequest.Read[i] = null;
					}
					mergeReadResponseNeeded = true;
					ExTraceGlobals.MserveCacheServiceTracer.TraceDebug<int>((long)this.GetHashCode(), "Removed {0} read requests from ProvisionRequest. Need to merge read responses with write responses coming from Mserve", num);
				}
			}
			provResponseForReadOnly.Status = 1;
			return true;
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x00067FFC File Offset: 0x000661FC
		private string ResolveToIpAddress(string hostName, bool flushCache)
		{
			DnsResult dnsResult = null;
			DnsQuery query = new DnsQuery(DnsRecordType.A, hostName);
			if (flushCache)
			{
				MserveWebService.dnsCache.FlushCache();
			}
			dnsResult = MserveWebService.dnsCache.Find(query);
			if (dnsResult == null)
			{
				try
				{
					IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
					IPAddress[] addressList = hostEntry.AddressList;
					if (addressList.Length > 0)
					{
						dnsResult = new DnsResult(DnsStatus.Success, addressList[0], TimeSpan.FromMinutes(1.0));
						MserveWebService.dnsCache.Add(query, dnsResult);
					}
				}
				catch (SocketException)
				{
				}
			}
			if (dnsResult != null)
			{
				return dnsResult.Server.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x00068090 File Offset: 0x00066290
		private bool UseRealMserveWebService()
		{
			return MserveWebService.CurrentMserveCacheServiceMode == MserveCacheServiceMode.NotEnabled || MserveWebService.CurrentMserveCacheServiceMode == MserveCacheServiceMode.EnabledWithFallback || MserveWebService.CurrentMserveCacheServiceMode == MserveCacheServiceMode.EnabledForReadOnly;
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000680AC File Offset: 0x000662AC
		private bool UseOfflineMserveCacheService()
		{
			return this.isMicrosoftHostedOnly && (MserveWebService.CurrentMserveCacheServiceMode == MserveCacheServiceMode.EnabledWithFallback || MserveWebService.CurrentMserveCacheServiceMode == MserveCacheServiceMode.AlwaysEnabled);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000680C9 File Offset: 0x000662C9
		private bool UseOfflineMserveCacheServiceFirst()
		{
			return this.isMicrosoftHostedOnly && MserveWebService.CurrentMserveCacheServiceMode == MserveCacheServiceMode.EnabledForReadOnly;
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000680E0 File Offset: 0x000662E0
		private void AckAccountResponse(string name, OperationType type, string partnerId, int status)
		{
			RecipientSyncOperation recipientSyncOperation = this.pendingQueue[name].RecipientSyncOperation;
			recipientSyncOperation.CompletedSyncCount++;
			this.pendingQueue.Remove(name);
			if (status == 1)
			{
				if (type == OperationType.Read)
				{
					int partnerId2;
					if (int.TryParse(partnerId, NumberStyles.Number, CultureInfo.InvariantCulture, out partnerId2))
					{
						recipientSyncOperation.PartnerId = partnerId2;
					}
					else
					{
						recipientSyncOperation.PartnerId = -1;
					}
				}
				if (this.batchMode && type != OperationType.Read)
				{
					recipientSyncOperation.PendingSyncStateCommitEntries[type].Add(name);
				}
			}
			else
			{
				if (!this.batchMode)
				{
					this.resultSet.Remove(recipientSyncOperation);
					throw new MserveException(string.Format("Provision Request for account {0} failed with status {1}, PartnerId {2}", name, status, partnerId), null);
				}
				if (this.trackDuplicatedAddEntries && status == 3215)
				{
					recipientSyncOperation.DuplicatedAddEntries.Add(name);
				}
				else if (MserveWebService.IsRetryableDeltaSyncError(status))
				{
					recipientSyncOperation.RetryableEntries[type].Add(new FailedAddress(name, status, true));
				}
				else
				{
					recipientSyncOperation.NonRetryableEntries[type].Add(new FailedAddress(name, status, false));
					if (type != OperationType.Read)
					{
						recipientSyncOperation.PendingSyncStateCommitEntries[type].Add(name);
					}
				}
			}
			if (recipientSyncOperation.Synchronized)
			{
				this.completedResultSet.Add(recipientSyncOperation);
				this.resultSet.Remove(recipientSyncOperation);
			}
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x00068234 File Offset: 0x00066434
		private void ProcessAccountType(Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType[] accountTypes, OperationType type)
		{
			int num = 0;
			switch (type)
			{
			case OperationType.Add:
				num = (this.trackDuplicatedAddEntries ? 0 : 3215);
				break;
			case OperationType.Delete:
				num = 3201;
				break;
			case OperationType.Read:
				num = 3201;
				break;
			}
			if (accountTypes != null)
			{
				foreach (Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType accountType in accountTypes)
				{
					this.LogResponseAccountDetail(type, accountType);
					this.AckAccountResponse(accountType.Name, type, accountType.PartnerID, (accountType.Status == num) ? 1 : accountType.Status);
				}
			}
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000682C4 File Offset: 0x000664C4
		private bool ProcessProvisionResponse(Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provResponse)
		{
			ExTraceGlobals.DeltaSyncAPITracer.TraceDebug<int>((long)this.GetHashCode(), "Response status is {0}", provResponse.Status);
			this.LogProvisionResponse(provResponse);
			if (provResponse.Status == 4301)
			{
				MserveWebService.chunkSize = this.FetchChunkSize();
				return false;
			}
			if (provResponse.Status != 1)
			{
				this.Reset();
				ExTraceGlobals.DeltaSyncAPITracer.TraceError<int>((long)this.GetHashCode(), "Provision request failed at top level with status {0}", provResponse.Status);
				throw new MserveException(string.Format("Provision request failed at the top level with error: {0} {1}", provResponse.Status, (provResponse.Fault != null) ? provResponse.Fault.Detail : string.Empty), null);
			}
			this.ProcessAccountType(provResponse.Responses.Add, OperationType.Add);
			this.ProcessAccountType(provResponse.Responses.Delete, OperationType.Delete);
			this.ProcessAccountType(provResponse.Responses.Read, OperationType.Read);
			return true;
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000683A8 File Offset: 0x000665A8
		private int ProcessOneOffReadAccountType(Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType[] accountTypes)
		{
			if (accountTypes == null || accountTypes.Length != 1)
			{
				throw new MserveException(string.Format("Unexpected number of responses. Expected 1, got {0}", (accountTypes != null) ? accountTypes.Length.ToString() : "null"));
			}
			this.LogResponseAccountDetail(OperationType.Read, accountTypes[0]);
			int result = -1;
			if (accountTypes[0].Status == 3201 || accountTypes[0].Status == 1)
			{
				if (!int.TryParse(accountTypes[0].PartnerID, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
				{
					result = -1;
				}
				return result;
			}
			throw new MserveException(string.Format("Provision Request for account {0} failed with status {1}, PartnerId {2}", accountTypes[0].Name, accountTypes[0].Status, accountTypes[0].PartnerID));
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x00068454 File Offset: 0x00066654
		private bool ProcessOneOffReadResponse(Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision provResponse, out int partnerId)
		{
			ExTraceGlobals.DeltaSyncAPITracer.TraceDebug<int>((long)this.GetHashCode(), "Response status is {0}", provResponse.Status);
			if (provResponse.Status != 1)
			{
				string arg = string.Empty;
				if (provResponse.Fault != null)
				{
					arg = string.Format("Code: {0}, String: {1}, Details: {2}", provResponse.Fault.Faultcode, provResponse.Fault.Faultstring, provResponse.Fault.Detail);
				}
				ExTraceGlobals.DeltaSyncAPITracer.TraceError<int, string>((long)this.GetHashCode(), "Provision request failed at top level with error {0} {1}", provResponse.Status, arg);
				throw new MserveException(string.Format("Provision request failed at the top level with error: {0} {1}", provResponse.Status, arg));
			}
			partnerId = this.ProcessOneOffReadAccountType(provResponse.Responses.Read);
			return true;
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x00068510 File Offset: 0x00066710
		private int FetchChunkSize()
		{
			ExTraceGlobals.DeltaSyncAPITracer.TraceDebug((long)this.GetHashCode(), "FetchChunkSize");
			Exception ex = null;
			string message = string.Empty;
			if (this.UseRealMserveWebService())
			{
				Microsoft.Exchange.Net.Mserve.SettingsRequest.Settings settings = new Microsoft.Exchange.Net.Mserve.SettingsRequest.Settings();
				settings.ServiceSettings = new ServiceSettingsType();
				settings.ServiceSettings.Properties = new ServiceSettingsTypeProperties();
				settings.ServiceSettings.Properties.Get = new object();
				Microsoft.Exchange.Net.Mserve.SettingsResponse.Settings settings2 = null;
				HttpSessionConfig httpSessionConfig = this.InitializeHttpSessionConfig(true);
				using (httpSessionConfig.RequestStream = new MemoryStream())
				{
					MserveWebService.settingsRequestSerializer.Serialize(new StreamWriter(httpSessionConfig.RequestStream, Encoding.UTF8), settings);
					httpSessionConfig.RequestStream.Position = 0L;
					using (HttpClient httpClient = new HttpClient())
					{
						ICancelableAsyncResult asyncResult = httpClient.BeginDownload(this.settingUri, httpSessionConfig, null, null);
						DownloadResult downloadResult = httpClient.EndDownload(asyncResult);
						if (downloadResult.IsSucceeded)
						{
							try
							{
								settings2 = (Microsoft.Exchange.Net.Mserve.SettingsResponse.Settings)MserveWebService.settingsResponseSerializer.Deserialize(new StreamReader(downloadResult.ResponseStream, Encoding.UTF8));
							}
							catch (InvalidOperationException ex2)
							{
								ex = ((ex2.InnerException != null) ? ex2.InnerException : ex2);
								message = "Failure during deserialization";
							}
							finally
							{
								if (downloadResult.ResponseStream != null)
								{
									downloadResult.ResponseStream.Close();
									downloadResult.ResponseStream = null;
								}
							}
							if (ex == null)
							{
								if (settings2.Status == 1)
								{
									return settings2.ServiceSettings.Properties.Get.MaxNumberOfProvisionCommands;
								}
								ex = new MserveException(string.Format("Reading Config Error: {0} {1}", settings2.Status, (settings2.Fault != null) ? settings2.Fault.Detail : string.Empty), null);
								message = ex.Message;
							}
						}
						else
						{
							ex = downloadResult.Exception;
							message = "Exception during Mserve lookup";
						}
					}
				}
			}
			if (this.UseOfflineMserveCacheService() || MserveWebService.CurrentMserveCacheServiceMode == MserveCacheServiceMode.EnabledForReadOnly)
			{
				MserveCacheServiceProvider instance = MserveCacheServiceProvider.GetInstance();
				int result = 0;
				try
				{
					result = instance.GetChunkSize();
				}
				catch (Exception ex3)
				{
					ExTraceGlobals.MserveCacheServiceTracer.TraceError<string, int>(0L, "Got exception while reading chunk size from Mserve. Exception: {0}. Returning default chunk size {1}", ex3.ToString(), MserveWebService.chunkSize);
					return MserveWebService.chunkSize;
				}
				return result;
			}
			throw new MserveException(message, ex);
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000687C0 File Offset: 0x000669C0
		protected virtual void LogProvisionAccountDetail(string type, Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType account)
		{
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000687C2 File Offset: 0x000669C2
		protected virtual void LogResponseAccountDetail(OperationType type, Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType account)
		{
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000687C4 File Offset: 0x000669C4
		protected virtual void LogProvisionRequest(string url)
		{
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000687C6 File Offset: 0x000669C6
		protected virtual void LogProvisionResponse(Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision response)
		{
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000687C8 File Offset: 0x000669C8
		private static bool ValidateCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslError)
		{
			if (sslError == SslPolicyErrors.None)
			{
				return true;
			}
			if (sslError == SslPolicyErrors.RemoteCertificateNameMismatch)
			{
				if (MserveWebService.staticRemoteCertSubject == null)
				{
					return true;
				}
				if (string.Compare(MserveWebService.staticRemoteCertSubject, cert.Subject, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400288D RID: 10381
		public const int InvalidPartnerId = -1;

		// Token: 0x0400288E RID: 10382
		internal const string DomainEntryAddressFormat = "E5CB63F56E8B4b69A1F70C192276D6AD@{0}";

		// Token: 0x0400288F RID: 10383
		internal const string DomainEntryAddressFormatForOrgGuid = "43BA6209CC0F4542958F65F8BF1CDED6@{0}.exchangereserved";

		// Token: 0x04002890 RID: 10384
		internal const string DomainEntryAddressFormatStartNewOrganization = "21668DE042684883B19BCB376E3BE474@{0}";

		// Token: 0x04002891 RID: 10385
		internal const string DomainEntryAddressFormatMinorPartnerId = "7f66cd009b304aeda37ffdeea1733ff6@{0}";

		// Token: 0x04002892 RID: 10386
		internal const string DomainEntryAddressFormatMinorPartnerIdForOrgGuid = "3da19c7b44a74bd3896daaf008594b6c@{0}.exchangereserved";

		// Token: 0x04002893 RID: 10387
		internal const string DomainEntryAddressFormatTenantNegoConfig = "ade5142cfe3d4ff19fed54a7f6087a98@{0}";

		// Token: 0x04002894 RID: 10388
		internal const string DomainEntryAddressFormatTenantOAuthClientProfileConfig = "0f01471e875a455a80c59def2a36ee3f@{0}";

		// Token: 0x04002895 RID: 10389
		private const string userAgent = "ExchangeHostedServices/1.0";

		// Token: 0x04002896 RID: 10390
		private const string contentType = "text/xml";

		// Token: 0x04002897 RID: 10391
		private const string certificateValidationComponentName = "MserveWebService";

		// Token: 0x04002898 RID: 10392
		private const string partnerTokenAuthQueryStringFormat = "Dspk={0}";

		// Token: 0x04002899 RID: 10393
		private const string mServDiagnosticHeader = "X-WindowsLive-Hydra";

		// Token: 0x0400289A RID: 10394
		private const string mservTransactionHeader = "X-TransactionID";

		// Token: 0x0400289B RID: 10395
		internal const string ExchangeLabsRegkeyPath = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x0400289C RID: 10396
		internal const string MserveCacheServiceModeRegkeyValue = "MserveCacheServiceEnabled";

		// Token: 0x0400289D RID: 10397
		private const MserveCacheServiceMode defaultMserveCacheServiceMode = MserveCacheServiceMode.NotEnabled;

		// Token: 0x0400289E RID: 10398
		private static readonly CancelableAsyncCallback asyncApiCallback = new CancelableAsyncCallback(MserveWebService.AsyncApiCallback);

		// Token: 0x0400289F RID: 10399
		private static readonly DnsCache dnsCache = new DnsCache(10);

		// Token: 0x040028A0 RID: 10400
		private bool trackDuplicatedAddEntries;

		// Token: 0x040028A1 RID: 10401
		private static int chunkSize = 100;

		// Token: 0x040028A2 RID: 10402
		private readonly Uri provisionUri;

		// Token: 0x040028A3 RID: 10403
		private readonly Uri provisionUriWithoutQuery;

		// Token: 0x040028A4 RID: 10404
		private readonly Uri settingUri;

		// Token: 0x040028A5 RID: 10405
		private string remoteCertSubject;

		// Token: 0x040028A6 RID: 10406
		private bool isMicrosoftHostedOnly;

		// Token: 0x040028A7 RID: 10407
		private static int whenRegKeyLastChecked = 0;

		// Token: 0x040028A8 RID: 10408
		private static object modeLockObject = new object();

		// Token: 0x040028A9 RID: 10409
		private static MserveCacheServiceMode currentMserveCacheServiceMode = MserveCacheServiceMode.NotEnabled;

		// Token: 0x040028AA RID: 10410
		private static TimeSpan MserveCacheServiceRegistryCheckInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x040028AB RID: 10411
		private MservePerfCounters perfCounters;

		// Token: 0x040028AC RID: 10412
		private Dictionary<string, RecipientPendingOperation> pendingQueue = new Dictionary<string, RecipientPendingOperation>();

		// Token: 0x040028AD RID: 10413
		private List<RecipientSyncOperation> resultSet = new List<RecipientSyncOperation>();

		// Token: 0x040028AE RID: 10414
		private List<RecipientSyncOperation> completedResultSet = new List<RecipientSyncOperation>();

		// Token: 0x040028AF RID: 10415
		private bool batchMode;

		// Token: 0x040028B0 RID: 10416
		private static bool initialized;

		// Token: 0x040028B1 RID: 10417
		private static readonly object mutex = new object();

		// Token: 0x040028B2 RID: 10418
		private static string staticRemoteCertSubject;

		// Token: 0x040028B3 RID: 10419
		private static Microsoft.Exchange.Net.Mserve.ProvisionRequest.ProvisionSerializer provRequestSerializer = new Microsoft.Exchange.Net.Mserve.ProvisionRequest.ProvisionSerializer();

		// Token: 0x040028B4 RID: 10420
		private static Microsoft.Exchange.Net.Mserve.ProvisionResponse.ProvisionSerializer provResponseSerializer = new Microsoft.Exchange.Net.Mserve.ProvisionResponse.ProvisionSerializer();

		// Token: 0x040028B5 RID: 10421
		private static Microsoft.Exchange.Net.Mserve.SettingsRequest.SettingsSerializer settingsRequestSerializer = new Microsoft.Exchange.Net.Mserve.SettingsRequest.SettingsSerializer();

		// Token: 0x040028B6 RID: 10422
		private static Microsoft.Exchange.Net.Mserve.SettingsResponse.SettingsSerializer settingsResponseSerializer = new Microsoft.Exchange.Net.Mserve.SettingsResponse.SettingsSerializer();
	}
}
