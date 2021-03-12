using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Exchange.Net.MonitoringWebClient.Rws;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rws.Probes
{
	// Token: 0x0200045D RID: 1117
	public class RwsProbe : WebClientProbeBase
	{
		// Token: 0x06001C41 RID: 7233 RVA: 0x000A4C2C File Offset: 0x000A2E2C
		internal override IExceptionAnalyzer CreateExceptionAnalyzer()
		{
			Dictionary<string, RequestTarget> dictionary = new Dictionary<string, RequestTarget>();
			Uri uri = new Uri(base.Definition.Endpoint);
			dictionary.Add(uri.Host, RequestTarget.Rws);
			return new RwsExceptionAnalyzer(dictionary);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000A4C65 File Offset: 0x000A2E65
		internal override IResponseTracker CreateResponseTracker()
		{
			this.responseTracker = base.CreateResponseTracker();
			return this.responseTracker;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x000A4C79 File Offset: 0x000A2E79
		internal override void TraceInformation(string message, params object[] parameters)
		{
			this.traceCollector.TraceInformation(ExTraceGlobals.RWSTracer, base.TraceContext, message, parameters);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000A4C93 File Offset: 0x000A2E93
		internal override void ScenarioSucceeded(Task scenarioTask)
		{
			this.TraceInformation("RWS endpoint call scenario succeeded", new object[0]);
			base.Result.ResultType = ResultType.Succeeded;
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000A4CB8 File Offset: 0x000A2EB8
		internal override void ScenarioFailed(Task scenarioTask)
		{
			try
			{
				Exception innerException = scenarioTask.Exception.InnerException;
				ScenarioException scenarioException = innerException.GetScenarioException();
				this.TraceInformation("RWS endpoint call scenario failed: {0}{1}{2}", new object[]
				{
					innerException,
					Environment.NewLine,
					scenarioException
				});
				base.Result.ResultType = ResultType.Failed;
				this.PopulateResult(scenarioException, this.responseTracker.Items);
				scenarioTask.Exception.Flatten().Handle((Exception e) => false);
			}
			catch (Exception ex)
			{
				this.TraceInformation("Exception thrown on TestScenarioFailed function: {0}", new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000A4D78 File Offset: 0x000A2F78
		internal override void ScenarioCancelled(Task scenarioTask)
		{
			throw new NotSupportedException("RWSProbe doesn't support cancelling a scenario");
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000A4D84 File Offset: 0x000A2F84
		internal override Task ExecuteScenario(IHttpSession session)
		{
			Uri uri = new Uri(base.Definition.Endpoint);
			RwsAuthenticationInfo authenticationInfo;
			if (uri.Port == 444)
			{
				this.TraceInformation("Creating common access token for brick authentication.", new object[0]);
				CommonAccessToken commonAccessToken = CommonAccessTokenHelper.CreateLiveIdBasic(base.Definition.Account);
				this.TraceInformation("Token: type - LiveIdBasic MemberName - {0}", new object[]
				{
					base.Definition.Account
				});
				authenticationInfo = new RwsAuthenticationInfo(commonAccessToken);
				this.TraceInformation("Created common access token \"{0}\" for brick authentication.", new object[]
				{
					commonAccessToken.ToString()
				});
			}
			else
			{
				authenticationInfo = new RwsAuthenticationInfo(base.Definition.Account, base.Definition.Account.Split(new char[]
				{
					'@'
				})[1], base.Definition.AccountPassword.ConvertToSecureString());
			}
			ITestFactory testFactory = new TestFactory();
			ITestStep testStep = testFactory.CreateRwsCallScenario(uri, authenticationInfo, testFactory);
			testStep.MaxRunTime = new TimeSpan?(RwsProbe.ScenarioTimeout);
			return testStep.CreateTask(session);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000A4EA8 File Offset: 0x000A30A8
		private void PopulateFailingInformation(ScenarioException exception, IEnumerable<ResponseTrackerItem> responseItems, Dictionary<string, string> resultPairs)
		{
			if (exception != null)
			{
				resultPairs.Add("Component", exception.FailingComponent.ToString());
				resultPairs.Add("Reason", exception.FailureReason.ToString());
				resultPairs.Add("Source", exception.FailureSource.ToString());
				base.Result.FailureCategory = (int)exception.FailingComponent;
				base.Result.StateAttribute1 = exception.FailingComponent.ToString();
				base.Result.StateAttribute2 = exception.FailureReason.ToString();
				IEnumerable<ResponseTrackerItem> enumerable = from item in responseItems
				where item.FailingServer != null && item.FailureHttpResponseCode != null
				select item;
				if (enumerable != null && enumerable.Count<ResponseTrackerItem>() > 0)
				{
					ResponseTrackerItem responseTrackerItem = enumerable.First<ResponseTrackerItem>();
					resultPairs.Add("Server", responseTrackerItem.FailingServer);
					resultPairs.Add("HttpStatus", responseTrackerItem.FailureHttpResponseCode.Value.ToString());
					base.Result.FailureContext = responseTrackerItem.FailingServer;
				}
			}
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000A4FCC File Offset: 0x000A31CC
		private void PopulateRequestInformation(IEnumerable<ResponseTrackerItem> responseItems, Dictionary<string, string> resultPairs)
		{
			int num = 0;
			foreach (ResponseTrackerItem responseTrackerItem in responseItems)
			{
				if (responseTrackerItem.TargetType == RequestTarget.Rws.ToString())
				{
					resultPairs.Add(string.Format("{0}", num), string.Format("{0}{1}", responseTrackerItem.TargetHost, responseTrackerItem.PathAndQuery));
				}
				else
				{
					resultPairs.Add(string.Format("{0}", num), string.Format("{0}{1}", responseTrackerItem.TargetHost, responseTrackerItem.PathAndQuery.Split(new char[]
					{
						'?'
					})[0]));
				}
				num++;
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000A50A0 File Offset: 0x000A32A0
		private void PopulateResult(ScenarioException exception, IEnumerable<ResponseTrackerItem> responseItems)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			this.PopulateFailingInformation(exception, responseItems, dictionary);
			this.PopulateRequestInformation(responseItems, dictionary);
			this.PopulateLatencyInformation(exception, responseItems, dictionary);
			this.PopulateServerInformation(responseItems, dictionary);
			base.Result.ExecutionContext = this.TransformToString(dictionary);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000A50E8 File Offset: 0x000A32E8
		private string GetTargetAbbreviation(RequestTarget target)
		{
			switch (target)
			{
			case RequestTarget.Rws:
				return "RWS";
			case RequestTarget.LiveIdConsumer:
				return "LID_CON";
			case RequestTarget.LiveIdBusiness:
				return "LID_BUS";
			default:
				return "UNK";
			}
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000A5178 File Offset: 0x000A3378
		private void PopulateLatencyInformation(ScenarioException exception, IEnumerable<ResponseTrackerItem> responseItems, Dictionary<string, string> resultPairs)
		{
			var enumerable = from item in responseItems
			group item by item.TargetType into g
			select new
			{
				TargetType = g.Key,
				TotalLatency = g.Sum((ResponseTrackerItem item) => item.TotalLatency.TotalMilliseconds)
			};
			base.Result.SampleValue = this.responseTracker.Items.Sum((ResponseTrackerItem i) => i.TotalLatency.TotalMilliseconds);
			if (exception != null && string.Compare(exception.FailureReason.ToString().Trim(), "RequestTimeout", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				base.Result.SampleValue = WebClientProbeBase.RequestTimeout.TotalMilliseconds;
			}
			foreach (var <>f__AnonymousType in enumerable)
			{
				RequestTarget target = (RequestTarget)Enum.Parse(typeof(RequestTarget), <>f__AnonymousType.TargetType);
				resultPairs.Add(this.GetTargetAbbreviation(target) + "_LTCY", <>f__AnonymousType.TotalLatency.ToString());
				switch (target)
				{
				case RequestTarget.Rws:
					base.Result.StateAttribute18 = <>f__AnonymousType.TotalLatency;
					break;
				case RequestTarget.LiveIdConsumer:
					base.Result.StateAttribute16 = <>f__AnonymousType.TotalLatency;
					break;
				case RequestTarget.LiveIdBusiness:
					base.Result.StateAttribute17 = <>f__AnonymousType.TotalLatency;
					break;
				}
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000A53A4 File Offset: 0x000A35A4
		private void PopulateServerInformation(IEnumerable<ResponseTrackerItem> responseItems, Dictionary<string, string> resultPairs)
		{
			IEnumerable<string> list = from item in responseItems
			where item.TargetType == RequestTarget.Rws.ToString() && item.RespondingServer != null
			select item.RespondingServer;
			string stateAttribute = null;
			string stateAttribute2 = null;
			string text = this.AggregateIntoCommaSeparatedUniqueList(list, out stateAttribute, out stateAttribute2);
			string text2 = this.AggregateIntoCommaSeparatedUniqueList(from item in responseItems
			where item.TargetType == RequestTarget.LiveIdConsumer.ToString() && item.RespondingServer != null
			select item.RespondingServer);
			string text3 = this.AggregateIntoCommaSeparatedUniqueList(from item in responseItems
			where item.TargetType == RequestTarget.LiveIdBusiness.ToString() && item.RespondingServer != null
			select item.RespondingServer);
			resultPairs.Add("RWS", text);
			resultPairs.Add("LID_BUS", text3);
			resultPairs.Add("LID_CON", text2);
			base.Result.StateAttribute3 = text;
			base.Result.StateAttribute4 = text2;
			base.Result.StateAttribute5 = text3;
			base.Result.StateAttribute11 = stateAttribute;
			base.Result.StateAttribute12 = stateAttribute2;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000A5508 File Offset: 0x000A3708
		private string AggregateIntoCommaSeparatedUniqueList(IEnumerable<string> list, out string first, out string last)
		{
			string text;
			last = (text = null);
			first = text;
			if (list == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string text2 in list)
			{
				if (!hashSet.Contains(text2))
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(text2);
					last = text2;
					if (first == null)
					{
						first = text2;
					}
					hashSet.Add(text2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000A55A8 File Offset: 0x000A37A8
		private string AggregateIntoCommaSeparatedUniqueList(IEnumerable<string> list)
		{
			string text;
			string text2;
			return this.AggregateIntoCommaSeparatedUniqueList(list, out text, out text2);
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x000A55C0 File Offset: 0x000A37C0
		private string TransformToString(Dictionary<string, string> keyPairs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (string text in keyPairs.Keys)
			{
				if (!string.IsNullOrEmpty(keyPairs[text]))
				{
					if (num > 0)
					{
						stringBuilder.Append(";");
					}
					stringBuilder.AppendFormat("{0}={1}", text, keyPairs[text]);
					num++;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001382 RID: 4994
		public const string UserPuidParameterName = "UserPuid";

		// Token: 0x04001383 RID: 4995
		public const string UserSidParameterName = "UserSid";

		// Token: 0x04001384 RID: 4996
		public const string PartitionIdParameterName = "PartitionId";

		// Token: 0x04001385 RID: 4997
		private const int BrickPort = 444;

		// Token: 0x04001386 RID: 4998
		private static readonly TimeSpan ScenarioTimeout = TimeSpan.FromSeconds(100.0);

		// Token: 0x04001387 RID: 4999
		private TraceCollector traceCollector = new TraceCollector();

		// Token: 0x04001388 RID: 5000
		private IResponseTracker responseTracker;
	}
}
