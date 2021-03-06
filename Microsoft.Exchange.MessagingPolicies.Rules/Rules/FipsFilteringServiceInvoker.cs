using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Filtering;
using Microsoft.Filtering.Configuration;
using Microsoft.Filtering.Results;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200001C RID: 28
	internal class FipsFilteringServiceInvoker : FilteringServiceInvoker
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00004684 File Offset: 0x00002884
		internal static IEnumerable<DiscoveredDataClassification> GetDataClassifications(IEnumerable<Rule> rules, FilteringServiceInvokerRequest filteringServiceInvokerRequest, ITracer tracer, out FilteringResults textExtractionResults)
		{
			Dictionary<string, string> dataClassificationsFromRules = TransportUtils.GetDataClassificationsFromRules(rules);
			IEnumerable<DiscoveredDataClassification> dataClassificationResult = null;
			FilteringResults textExtractionResultsFromCallBack = null;
			if (dataClassificationsFromRules.Any<KeyValuePair<string, string>>())
			{
				tracer.TraceDebug("Executing filtering service");
				AutoResetEvent scanComplete = new AutoResetEvent(false);
				Exception dataClassificationException = null;
				FilteringServiceInvoker.ScanResult dataClassificationScanResult = FilteringServiceInvoker.ScanResult.Failure;
				FilteringServiceInvoker filteringServiceInvoker = FilteringServiceInvokerFactory.Create(tracer);
				FilteringServiceInvoker.BeginScanResult beginScanResult = filteringServiceInvoker.BeginScan(filteringServiceInvokerRequest, tracer, dataClassificationsFromRules, delegate(FilteringServiceInvoker.ScanResult scanResult, IEnumerable<DiscoveredDataClassification> dataClassifications, FilteringResults filteringResults, Exception exception)
				{
					FipsFilteringServiceInvoker.ClassificationsDiscovered(scanComplete, scanResult, exception, dataClassifications, filteringResults, out dataClassificationResult, out dataClassificationScanResult, out dataClassificationException, out textExtractionResultsFromCallBack);
				});
				if (beginScanResult == FilteringServiceInvoker.BeginScanResult.Queued)
				{
					int num = (int)(1.1 * filteringServiceInvokerRequest.ScanTimeout.TotalMilliseconds);
					if (!scanComplete.WaitOne(num))
					{
						string message = string.Format("The attempt to extract text in FIPS timed out after {0}ms", num);
						tracer.TraceDebug(message);
						throw new FilteringServiceTimeoutException(message);
					}
				}
				if (dataClassificationScanResult != FilteringServiceInvoker.ScanResult.Success)
				{
					string message2 = TransportRulesStrings.ErrorInvokingFilteringService((int)beginScanResult, filteringServiceInvoker.ErrorDescription);
					tracer.TraceDebug(message2);
					if (dataClassificationException != null)
					{
						throw new FilteringServiceFailureException(string.Format("FIPS data classification failed with error: '{0}'. See inner exception for details", dataClassificationException.Message), dataClassificationException);
					}
					throw new FilteringServiceFailureException(message2);
				}
				else if (dataClassificationResult != null && dataClassificationResult.Any<DiscoveredDataClassification>())
				{
					IEnumerable<string> source = from dataClassification in dataClassificationResult
					select string.Format("(id = {0}, count = {1}, confidence = {2})", dataClassification.Id, dataClassification.TotalCount, dataClassification.MaxConfidenceLevel);
					tracer.TraceDebug("Discovered classifications in mail {0}", new object[]
					{
						string.Join(",", source.ToArray<string>())
					});
				}
			}
			textExtractionResults = textExtractionResultsFromCallBack;
			return dataClassificationResult ?? new List<DiscoveredDataClassification>();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004840 File Offset: 0x00002A40
		private static void ClassificationsDiscovered(AutoResetEvent scanComplete, FilteringServiceInvoker.ScanResult scanResult, Exception exception, IEnumerable<DiscoveredDataClassification> dataClassifications, FilteringResults filteringResults, out IEnumerable<DiscoveredDataClassification> dataClassificationResult, out FilteringServiceInvoker.ScanResult dataClassificationScanResult, out Exception dataClassificationException, out FilteringResults textExtractionResults)
		{
			dataClassificationScanResult = scanResult;
			dataClassificationException = exception;
			textExtractionResults = filteringResults;
			if (scanResult == FilteringServiceInvoker.ScanResult.Success)
			{
				dataClassificationResult = dataClassifications;
			}
			else
			{
				dataClassificationResult = null;
			}
			scanComplete.Set();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000488C File Offset: 0x00002A8C
		public override FilteringServiceInvoker.BeginScanResult BeginScan(FilteringServiceInvokerRequest filteringServiceInvokerRequest, ITracer tracer, Dictionary<string, string> classificationsToLookFor, FilteringServiceInvoker.ScanCompleteCallback scanCompleteCallback)
		{
			IFipsDataStreamFilteringService filteringService = null;
			FilteringServiceInvoker.BeginScanResult result;
			try
			{
				base.ErrorDescription = string.Empty;
				FilteringServiceFactory.Create(out filteringService);
				ScanConfiguration config = FipsFilteringServiceInvoker.CreateDlpScanConfiguration(filteringServiceInvokerRequest.OrganizationId, filteringServiceInvokerRequest.TextScanLimit, 0, classificationsToLookFor);
				FilteringRequest filteringRequest = FipsFilteringServiceInvoker.CreateFipsRequest(config, filteringServiceInvokerRequest);
				filteringService.BeginScan(filteringServiceInvokerRequest.FipsDataStreamFilteringRequest, filteringRequest, delegate(IAsyncResult asyncResult)
				{
					this.ScanComplete(filteringService, scanCompleteCallback, asyncResult, tracer);
				}, this);
				result = FilteringServiceInvoker.BeginScanResult.Queued;
			}
			catch (Exception e)
			{
				result = this.HandleBeginScanException(filteringService, scanCompleteCallback, e, tracer);
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004958 File Offset: 0x00002B58
		protected FilteringServiceInvoker.BeginScanResult HandleBeginScanException(IFipsDataStreamFilteringService filteringService, FilteringServiceInvoker.ScanCompleteCallback scanCompleteCallback, Exception e, ITracer tracer)
		{
			this.LogBeginScanException(e, tracer);
			filteringService.Dispose();
			scanCompleteCallback(FilteringServiceInvoker.ScanResult.Failure, new List<DiscoveredDataClassification>(), null, e);
			return FilteringServiceInvoker.BeginScanResult.Failure;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004978 File Offset: 0x00002B78
		protected void ScanComplete(IFipsDataStreamFilteringService filteringService, FilteringServiceInvoker.ScanCompleteCallback scanCompleteCallback, IAsyncResult asyncResult, ITracer tracer)
		{
			FilteringResults filteringResults = null;
			Exception exception = null;
			IEnumerable<DiscoveredDataClassification> classifications;
			FilteringServiceInvoker.ScanResult scanResult;
			try
			{
				FilteringResponse response = filteringService.EndScan(asyncResult);
				scanResult = this.ProcessFilteringResponse(response, tracer, out classifications, out filteringResults);
			}
			catch (Exception ex)
			{
				exception = ex;
				this.LogEndScanException(ex, tracer);
				if (ex is ScanQueueTimeoutException)
				{
					scanResult = FilteringServiceInvoker.ScanResult.QueueTimeout;
				}
				else if (ex is ScanTimeoutException)
				{
					scanResult = FilteringServiceInvoker.ScanResult.Timeout;
				}
				else if (ex is ScannerCrashException)
				{
					scanResult = FilteringServiceInvoker.ScanResult.CrashFailure;
				}
				else
				{
					scanResult = FilteringServiceInvoker.ScanResult.Failure;
				}
				classifications = new List<DiscoveredDataClassification>();
			}
			finally
			{
				filteringService.Dispose();
			}
			scanCompleteCallback(scanResult, classifications, filteringResults, exception);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004A1C File Offset: 0x00002C1C
		protected FilteringServiceInvoker.ScanResult ProcessFilteringResponse(FilteringResponse response, ITracer tracer, out IEnumerable<DiscoveredDataClassification> dataClassifications, out FilteringResults filteringResults)
		{
			filteringResults = response.Results;
			RuleAgentResultUtils.ValidateResults(filteringResults);
			FilteringServiceInvoker.ScanResult result;
			if ((response.Flags & 1) != null)
			{
				tracer.TraceDebug("FIPS run complete - results found");
				result = FilteringServiceInvoker.ScanResult.Success;
				if (filteringResults != null)
				{
					dataClassifications = FipsResultParser.ParseDataClassifications(filteringResults, tracer);
					string formatString = "Discovered classifications: {0}";
					object[] array = new object[1];
					array[0] = string.Join(",", from dataClassification in dataClassifications
					select dataClassification.Id);
					tracer.TraceDebug(formatString, array);
				}
				else
				{
					tracer.TraceError("FIPS run complete - classifications found but result XML is empty");
					result = FilteringServiceInvoker.ScanResult.Failure;
					dataClassifications = new List<DiscoveredDataClassification>();
				}
			}
			else
			{
				tracer.TraceDebug("FIPS run complete - no classifications found");
				result = FilteringServiceInvoker.ScanResult.Success;
				dataClassifications = new List<DiscoveredDataClassification>();
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004AD2 File Offset: 0x00002CD2
		protected void LogBeginScanException(Exception e, ITracer tracer)
		{
			this.LogFilteringServiceException(e, tracer, "BeginScan");
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004AE1 File Offset: 0x00002CE1
		protected void LogEndScanException(Exception e, ITracer tracer)
		{
			this.LogFilteringServiceException(e, tracer, "EndScan");
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004AF0 File Offset: 0x00002CF0
		protected void LogFilteringServiceException(Exception e, ITracer tracer, string scanPhase)
		{
			base.ErrorDescription = string.Format("Exception in FIPS scan (stage = '{0}', exception = '{1}')", scanPhase, e.Message);
			tracer.TraceError(base.ErrorDescription);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004BAC File Offset: 0x00002DAC
		internal static string GenerateFipsCategoriesXml(string tenantId, Dictionary<string, string> dataClassifications)
		{
			XDeclaration declaration = new XDeclaration("1.0", "utf-16", "yes");
			object[] array = new object[1];
			object[] array2 = array;
			int num = 0;
			XName name = "ClassificationInformation";
			object[] array3 = new object[2];
			array3[0] = new XElement("Organization", new XAttribute("id", tenantId));
			array3[1] = (from classification in dataClassifications
			group classification by classification.Value).Select(delegate(IGrouping<string, KeyValuePair<string, string>> group)
			{
				XName name2 = "Group";
				object[] array4 = new object[2];
				array4[0] = new XAttribute("id", group.Key);
				array4[1] = from classification in @group
				select new XElement("Classification", new XAttribute("id", classification.Key));
				return new XElement(name2, array4);
			});
			array2[num] = new XElement(name, array3);
			XDocument xdocument = new XDocument(declaration, array);
			return xdocument.Declaration.ToString() + xdocument.ToString();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004C78 File Offset: 0x00002E78
		internal static FilteringRequest CreateFipsRequest(ScanConfiguration config, FilteringServiceInvokerRequest filteringServiceInvokerRequest)
		{
			TimeSpan scanTimeout = filteringServiceInvokerRequest.ScanTimeout;
			FilteringRequest filteringRequest = filteringServiceInvokerRequest.FipsDataStreamFilteringRequest.ToFilteringRequest(true);
			filteringRequest.Timeout = Convert.ToInt32(scanTimeout.TotalSeconds);
			filteringRequest.SetConfiguration(config);
			return filteringRequest;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004CB4 File Offset: 0x00002EB4
		internal static ScanConfiguration CreateTextExtractionConfiguration(int textScanLimit, int textScanUnlimited)
		{
			return new ScanConfiguration
			{
				MalwareSettings = new MalwareSettings
				{
					IsEnabled = new bool?(false)
				},
				ClassificationSettings = new ClassificationSettings
				{
					IsEnabled = new bool?(false)
				},
				GeneralSettings = new GeneralSettings
				{
					ParseOfficeCustomProperties = new bool?(true),
					ReturnExtractedText = new bool?(true),
					MaxScanTextSize = new ThresholdViolationAction
					{
						Threshold = new int?((textScanLimit == textScanUnlimited) ? int.MaxValue : textScanLimit)
					}
				}
			};
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004D48 File Offset: 0x00002F48
		internal static ScanConfiguration CreateDlpScanConfiguration(string tenantId, int textScanLimit, int textScanUnlimited, Dictionary<string, string> classificationsToLookFor)
		{
			ScanConfiguration scanConfiguration = new ScanConfiguration
			{
				ClassificationSettings = new ClassificationSettings
				{
					IsEnabled = new bool?(true)
				},
				MalwareSettings = new MalwareSettings
				{
					IsEnabled = new bool?(false)
				},
				GeneralSettings = new GeneralSettings
				{
					ReturnExtractedText = new bool?(true),
					MaxScanTextSize = new ThresholdViolationAction
					{
						Threshold = new int?((textScanLimit == textScanUnlimited) ? int.MaxValue : textScanLimit)
					}
				}
			};
			ScannerSettings scannerSettings = new ScannerSettings
			{
				Name = FipsFilteringServiceInvoker.dlpEngineName,
				Enabled = true
			};
			scannerSettings.Properties.Add(FipsFilteringServiceInvoker.classificationParameterPropertyName, FipsFilteringServiceInvoker.GenerateFipsCategoriesXml(tenantId, classificationsToLookFor));
			scanConfiguration.ClassificationSettings.Scanners.Add(scannerSettings);
			return scanConfiguration;
		}

		// Token: 0x040000D8 RID: 216
		internal const int TransportRuleAttachmentTextScanUnlimited = 0;

		// Token: 0x040000D9 RID: 217
		private static readonly string dlpEngineName = "MSClassification";

		// Token: 0x040000DA RID: 218
		private static readonly string classificationParameterPropertyName = "ClassificationRules";
	}
}
