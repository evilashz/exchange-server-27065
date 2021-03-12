using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200014B RID: 331
	internal class CertificateValidationResultCache : DisposeTrackableBase
	{
		// Token: 0x06000EC6 RID: 3782 RVA: 0x000396F4 File Offset: 0x000378F4
		public CertificateValidationResultCache(ProcessTransportRole transportRole, string name, TransportAppConfig.SecureMailConfig config, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			ArgumentValidator.ThrowIfNull("config", config);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.transientFailureExpiryInterval = config.CertificateValidationCacheTransientFailureExpiryInterval;
			this.cache = new Cache<string, CertificateValidationResultCache.ValidationResultItem>((long)config.CertificateValidationCacheMaxSize.ToBytes(), config.CertificateValidationCacheExpiryInterval, TimeSpan.Zero, new DefaultCacheTracer<string>(tracer, name), new CertificateValidationResultCache.ValidationResultPerformanceCounters(transportRole, name));
			tracer.TraceDebug((long)this.GetHashCode(), "Created Certificate Validation Result Cache '{0}' with the following configuration: MaxSize={1}, ExpiryInterval={2}, TransientFailureExpiryInterval={3}", new object[]
			{
				name,
				config.CertificateValidationCacheMaxSize,
				config.CertificateValidationCacheExpiryInterval,
				config.CertificateValidationCacheTransientFailureExpiryInterval
			});
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x000397B4 File Offset: 0x000379B4
		public bool TryAdd(X509Certificate2 certificate, ChainValidityStatus result)
		{
			string thumbprint = certificate.Thumbprint;
			if (string.IsNullOrEmpty(thumbprint))
			{
				return false;
			}
			DateTime expiryTime = this.CalculateItemExpiryTime(certificate, result);
			return this.cache.TryAdd(thumbprint, new CertificateValidationResultCache.ValidationResultItem(result, expiryTime));
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x000397F0 File Offset: 0x000379F0
		public bool TryGetValue(X509Certificate2 certificate, out ChainValidityStatus result)
		{
			result = ChainValidityStatus.EmptyCertificate;
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			string thumbprint = certificate.Thumbprint;
			CertificateValidationResultCache.ValidationResultItem validationResultItem;
			if (!string.IsNullOrEmpty(thumbprint) && this.cache.TryGetValue(thumbprint, out validationResultItem))
			{
				result = validationResultItem.Result;
				return true;
			}
			return false;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00039838 File Offset: 0x00037A38
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.cache.Dispose();
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00039848 File Offset: 0x00037A48
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CertificateValidationResultCache>(this);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00039850 File Offset: 0x00037A50
		protected virtual DateTime GetCertificateExpiryTime(X509Certificate2 certificate)
		{
			return certificate.NotAfter;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00039858 File Offset: 0x00037A58
		private DateTime CalculateItemExpiryTime(X509Certificate2 certificate, ChainValidityStatus result)
		{
			if (result <= (ChainValidityStatus)2148081683U)
			{
				switch (result)
				{
				case ChainValidityStatus.Valid:
				case ChainValidityStatus.ValidSelfSigned:
					return this.GetCertificateExpiryTime(certificate);
				default:
					switch (result)
					{
					case (ChainValidityStatus)2148081682U:
					case (ChainValidityStatus)2148081683U:
						break;
					default:
						goto IL_59;
					}
					break;
				}
			}
			else if (result != (ChainValidityStatus)2148204810U && result != (ChainValidityStatus)2148204814U)
			{
				goto IL_59;
			}
			return DateTime.UtcNow + this.transientFailureExpiryInterval;
			IL_59:
			return DateTime.MaxValue;
		}

		// Token: 0x04000727 RID: 1831
		private readonly Cache<string, CertificateValidationResultCache.ValidationResultItem> cache;

		// Token: 0x04000728 RID: 1832
		private readonly TimeSpan transientFailureExpiryInterval;

		// Token: 0x0200014C RID: 332
		private class ValidationResultItem : CachableItem
		{
			// Token: 0x06000ECD RID: 3789 RVA: 0x000398C3 File Offset: 0x00037AC3
			public ValidationResultItem(ChainValidityStatus result, DateTime expiryTime)
			{
				this.Result = result;
				this.expiryTime = expiryTime;
			}

			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x06000ECE RID: 3790 RVA: 0x000398D9 File Offset: 0x00037AD9
			public override long ItemSize
			{
				get
				{
					return 12L;
				}
			}

			// Token: 0x06000ECF RID: 3791 RVA: 0x000398DE File Offset: 0x00037ADE
			public override bool IsExpired(DateTime currentTime)
			{
				return currentTime > this.expiryTime;
			}

			// Token: 0x04000729 RID: 1833
			public readonly ChainValidityStatus Result;

			// Token: 0x0400072A RID: 1834
			private readonly DateTime expiryTime;
		}

		// Token: 0x0200014D RID: 333
		private class ValidationResultPerformanceCounters : DefaultCachePerformanceCounters
		{
			// Token: 0x06000ED0 RID: 3792 RVA: 0x000398EC File Offset: 0x00037AEC
			public ValidationResultPerformanceCounters(ProcessTransportRole transportRole, string instanceName)
			{
				if (instanceName == null)
				{
					throw new ArgumentNullException("instanceName");
				}
				CertificateValidationResultCachePerfCountersInstance certificateValidationResultCachePerfCountersInstance;
				try
				{
					CertificateValidationResultCachePerfCounters.SetCategoryName(CertificateValidationResultCache.ValidationResultPerformanceCounters.perfCounterCategoryMap[transportRole]);
					certificateValidationResultCachePerfCountersInstance = CertificateValidationResultCachePerfCounters.GetInstance(instanceName);
				}
				catch (InvalidOperationException ex)
				{
					ExTraceGlobals.GeneralTracer.TraceError<string, InvalidOperationException>(0L, "Failed to initialize performance counters for component '{0}': {1}", instanceName, ex);
					ExEventLog exEventLog = new ExEventLog(ExTraceGlobals.GeneralTracer.Category, TransportEventLog.GetEventSource());
					exEventLog.LogEvent(TransportEventLogConstants.Tuple_PerfCountersLoadFailure, null, new object[]
					{
						"Certificate Validation Result Cache",
						instanceName,
						ex.ToString()
					});
					certificateValidationResultCachePerfCountersInstance = null;
				}
				if (certificateValidationResultCachePerfCountersInstance != null)
				{
					base.InitializeCounters(certificateValidationResultCachePerfCountersInstance.Requests, certificateValidationResultCachePerfCountersInstance.HitRatio, certificateValidationResultCachePerfCountersInstance.HitRatio_Base, certificateValidationResultCachePerfCountersInstance.CacheSize);
				}
			}

			// Token: 0x0400072B RID: 1835
			private static readonly IDictionary<ProcessTransportRole, string> perfCounterCategoryMap = new Dictionary<ProcessTransportRole, string>
			{
				{
					ProcessTransportRole.Edge,
					"MSExchangeTransport Certificate Validation Cache"
				},
				{
					ProcessTransportRole.Hub,
					"MSExchangeTransport Certificate Validation Cache"
				},
				{
					ProcessTransportRole.FrontEnd,
					"MSExchangeFrontEndTransport Certificate Validation Cache"
				},
				{
					ProcessTransportRole.MailboxDelivery,
					"MSExchange Delivery Certificate Validation Cache"
				},
				{
					ProcessTransportRole.MailboxSubmission,
					"MSExchange Submission Certificate Validation Cache"
				}
			};
		}
	}
}
