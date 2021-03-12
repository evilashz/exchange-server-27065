using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Components.Filtering;
using Microsoft.Filtering.Results;
using Microsoft.Internal.ManagedWPP;

namespace Microsoft.Filtering
{
	// Token: 0x0200000A RID: 10
	internal class FipsDataStreamFilteringService : IFipsDataStreamFilteringService, IDisposable
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022A7 File Offset: 0x000004A7
		public FipsDataStreamFilteringService() : this(new FilteringService())
		{
			this.teLog.Start();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022BF File Offset: 0x000004BF
		internal FipsDataStreamFilteringService(IFilteringService service)
		{
			this.service = service;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002300 File Offset: 0x00000500
		public IAsyncResult BeginScan(FipsDataStreamFilteringRequest fipsDataStreamFilteringRequest, FilteringRequest filteringRequest, AsyncCallback callback, object state)
		{
			filteringRequest.AddRecoveryOptions(fipsDataStreamFilteringRequest.RecoveryOptions);
			IAsyncResult result;
			try
			{
				result = new FipsDataStreamFilteringAsyncResult((AsyncCallback c) => this.service.BeginScan(filteringRequest, c, state), callback, fipsDataStreamFilteringRequest);
			}
			catch (FilteringException e)
			{
				this.AddExceptionData(e);
				throw;
			}
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002374 File Offset: 0x00000574
		public FilteringResponse EndScan(IAsyncResult ar)
		{
			FipsDataStreamFilteringAsyncResult fipsDataStreamFilteringAsyncResult = (FipsDataStreamFilteringAsyncResult)ar;
			FilteringResponse result;
			try
			{
				FilteringResponse filteringResponse = this.service.EndScan(fipsDataStreamFilteringAsyncResult.InnerAsyncResult);
				if ((filteringResponse.Flags.HasFlag(2) || filteringResponse.Flags.HasFlag(64)) && Tracing.tracer.Level >= 4 && (Tracing.tracer.Flags & 2048) != 0)
				{
					WPP_0aa0dc6458a5fee18236a223d5cb2d97.WPP_isss(6, 10, this.GetHashCode(), TraceProvider.MakeStringArg(fipsDataStreamFilteringAsyncResult.FipsDataStreamFilteringRequest.Id), TraceProvider.MakeStringArg(filteringResponse.Flags), TraceProvider.MakeStringArg(ResultsFormatter.ConsoleFormatter.Format(filteringResponse.Results)));
				}
				this.LogTextExtractionInfo(filteringResponse.Results);
				result = filteringResponse;
			}
			catch (ScanTimeoutException e)
			{
				this.AddExceptionData(e);
				fipsDataStreamFilteringAsyncResult.FipsDataStreamFilteringRequest.RecoveryOptions = RecoveryOptions.Timeout;
				this.LogFailureResults(fipsDataStreamFilteringAsyncResult);
				throw;
			}
			catch (ScannerCrashException e2)
			{
				this.AddExceptionData(e2);
				fipsDataStreamFilteringAsyncResult.FipsDataStreamFilteringRequest.RecoveryOptions = RecoveryOptions.Crash;
				this.LogFailureResults(fipsDataStreamFilteringAsyncResult);
				throw;
			}
			catch (FilteringException e3)
			{
				this.AddExceptionData(e3);
				this.LogFailureResults(fipsDataStreamFilteringAsyncResult);
				throw;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024B8 File Offset: 0x000006B8
		public void Dispose()
		{
			this.teLog.Stop();
			this.service.Dispose();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024D0 File Offset: 0x000006D0
		private void AddExceptionData(FilteringException e)
		{
			FipsDataStreamFilteringService.ExceptionRetryInfo exceptionRetryInfo = null;
			if (FipsDataStreamFilteringService.exceptionRetryInfoMap.TryGetValue(e.GetType(), out exceptionRetryInfo))
			{
				e.Data.Add("RetryCount", exceptionRetryInfo.RetryCount);
				if (exceptionRetryInfo.MinSecondsBetweenRetries > 0 && exceptionRetryInfo.RetryCount != 0)
				{
					e.Data.Add("MinSecondsBetweenRetries", exceptionRetryInfo.MinSecondsBetweenRetries);
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000253C File Offset: 0x0000073C
		private void LogFailureResults(FipsDataStreamFilteringAsyncResult eafar)
		{
			FilteringAsyncResult filteringAsyncResult = (FilteringAsyncResult)eafar.InnerAsyncResult;
			if (filteringAsyncResult != null)
			{
				this.LogTextExtractionInfo(filteringAsyncResult.Response.DiagnosticResults);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002578 File Offset: 0x00000778
		private void LogTextExtractionInfo(FilteringResults filteringResults)
		{
			if (filteringResults == null)
			{
				ExTraceGlobals.FilteringServiceApiTracer.TraceDebug((long)this.GetHashCode(), "Diagnostic Results are null. So text extraction information cannot be logged.");
				return;
			}
			string exMessageId = null;
			foreach (StreamIdentity streamIdentity in filteringResults.Streams)
			{
				if (streamIdentity.Id == 0)
				{
					exMessageId = streamIdentity.Name;
				}
				TextExtractionData teData = default(TextExtractionData);
				if (streamIdentity.Types.Length != 0)
				{
					teData.Types = FormatterExtensions.CommaSeparate(from t in streamIdentity.Types
					select t.ToString());
					streamIdentity.Properties.TryGetInt32("ScanningPipeline::TextExtractionKeys::TextExtractionResult", ref teData.TextExtractionResult);
					streamIdentity.Properties.TryGetInt64("Parsing::ParsingKeys::StreamSize", ref teData.StreamSize);
					teData.StreamId = streamIdentity.Id;
					teData.ParentId = streamIdentity.ParentId;
					streamIdentity.Properties.TryGetString("ScanningPipeline::TextExtractionKeys::TextExtractionMethod", ref teData.ModuleUsed);
					streamIdentity.Properties.TryGetString("ScanningPipeline::TextExtractionKeys::TextExtractionSkippedModules", ref teData.SkippedModules);
					streamIdentity.Properties.TryGetString("ScanningPipeline::TextExtractionKeys::TextExtractionFailedModules", ref teData.FailedModules);
					streamIdentity.Properties.TryGetString("ScanningPipeline::TextExtractionKeys::TextExtractionDisabledModules", ref teData.DisabledModules);
					streamIdentity.Properties.TryGetString("ScanningPipeline::TextExtractionKeys::TextExtractionAdditionalInformation", ref teData.AdditionalInformation);
					this.teLog.Trace(exMessageId, teData);
				}
			}
		}

		// Token: 0x0400000B RID: 11
		private static IDictionary<Type, FipsDataStreamFilteringService.ExceptionRetryInfo> exceptionRetryInfoMap = new Dictionary<Type, FipsDataStreamFilteringService.ExceptionRetryInfo>
		{
			{
				typeof(QueueFullException),
				new FipsDataStreamFilteringService.ExceptionRetryInfo
				{
					RetryCount = -1,
					MinSecondsBetweenRetries = (int)TimeSpan.FromMinutes(5.0).TotalSeconds
				}
			},
			{
				typeof(ConfigurationException),
				new FipsDataStreamFilteringService.ExceptionRetryInfo
				{
					RetryCount = 0,
					MinSecondsBetweenRetries = 0
				}
			},
			{
				typeof(ScanQueueTimeoutException),
				new FipsDataStreamFilteringService.ExceptionRetryInfo
				{
					RetryCount = -1,
					MinSecondsBetweenRetries = (int)TimeSpan.FromMinutes(2.0).TotalSeconds
				}
			},
			{
				typeof(ScanTimeoutException),
				new FipsDataStreamFilteringService.ExceptionRetryInfo
				{
					RetryCount = 3,
					MinSecondsBetweenRetries = (int)TimeSpan.FromHours(1.0).TotalSeconds
				}
			},
			{
				typeof(ScanAbortedException),
				new FipsDataStreamFilteringService.ExceptionRetryInfo
				{
					RetryCount = -1,
					MinSecondsBetweenRetries = 60
				}
			},
			{
				typeof(BiasException),
				new FipsDataStreamFilteringService.ExceptionRetryInfo
				{
					RetryCount = 0,
					MinSecondsBetweenRetries = 0
				}
			},
			{
				typeof(ScannerCrashException),
				new FipsDataStreamFilteringService.ExceptionRetryInfo
				{
					RetryCount = -1,
					MinSecondsBetweenRetries = 0
				}
			},
			{
				typeof(ServiceUnavailableException),
				new FipsDataStreamFilteringService.ExceptionRetryInfo
				{
					RetryCount = -1,
					MinSecondsBetweenRetries = 30
				}
			},
			{
				typeof(FilteringException),
				new FipsDataStreamFilteringService.ExceptionRetryInfo
				{
					RetryCount = -1,
					MinSecondsBetweenRetries = 0
				}
			}
		};

		// Token: 0x0400000C RID: 12
		private readonly IFilteringService service;

		// Token: 0x0400000D RID: 13
		private TextExtractionLog teLog = new TextExtractionLog();

		// Token: 0x0200000B RID: 11
		private class ExceptionRetryInfo
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600001B RID: 27 RVA: 0x000028D9 File Offset: 0x00000AD9
			// (set) Token: 0x0600001C RID: 28 RVA: 0x000028E1 File Offset: 0x00000AE1
			public int RetryCount { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600001D RID: 29 RVA: 0x000028EA File Offset: 0x00000AEA
			// (set) Token: 0x0600001E RID: 30 RVA: 0x000028F2 File Offset: 0x00000AF2
			public int MinSecondsBetweenRetries { get; set; }
		}
	}
}
