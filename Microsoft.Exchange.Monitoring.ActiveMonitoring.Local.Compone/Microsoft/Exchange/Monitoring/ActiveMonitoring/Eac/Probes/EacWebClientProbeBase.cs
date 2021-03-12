using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Exchange.Net.MonitoringWebClient.Ecp;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Eac.Probes
{
	// Token: 0x02000163 RID: 355
	public abstract class EacWebClientProbeBase : WebClientProbeBase
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x0003F863 File Offset: 0x0003DA63
		public EacWebClientProbeBase()
		{
			this.TraceCollector = new TraceCollector();
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0003F876 File Offset: 0x0003DA76
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0003F87E File Offset: 0x0003DA7E
		internal string CancelReason { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0003F887 File Offset: 0x0003DA87
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x0003F88F File Offset: 0x0003DA8F
		internal TraceCollector TraceCollector { get; private set; }

		// Token: 0x06000A15 RID: 2581 RVA: 0x0003F898 File Offset: 0x0003DA98
		internal override IExceptionAnalyzer CreateExceptionAnalyzer()
		{
			Dictionary<string, RequestTarget> dictionary = new Dictionary<string, RequestTarget>();
			dictionary.Add("login.live.com", RequestTarget.LiveIdConsumer);
			dictionary.Add("login.microsoftonline.com", RequestTarget.LiveIdBusiness);
			dictionary.Add("outlook.com", RequestTarget.Ecp);
			dictionary.Add("login.partner.microsoftonline.cn", RequestTarget.LiveIdBusiness);
			dictionary.Add("partner.outlook.cn", RequestTarget.Ecp);
			dictionary.Add("login.live-int.com", RequestTarget.LiveIdConsumer);
			dictionary.Add("login.microsoftonline-int.com", RequestTarget.LiveIdBusiness);
			dictionary.Add("exchangelabs.live-int.com", RequestTarget.Ecp);
			string host = new Uri(base.Definition.Endpoint).Host;
			if (!dictionary.ContainsKey(host))
			{
				dictionary.Add(new Uri(base.Definition.Endpoint).Host, RequestTarget.Ecp);
			}
			return new EcpExceptionAnalyzer(dictionary);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0003F94E File Offset: 0x0003DB4E
		internal override IResponseTracker CreateResponseTracker()
		{
			this.responseTracker = base.CreateResponseTracker();
			return this.responseTracker;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0003F962 File Offset: 0x0003DB62
		internal override void TraceInformation(string message, params object[] parameters)
		{
			this.TraceCollector.TraceInformation(ExTraceGlobals.ECPTracer, base.TraceContext, message, parameters);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0003F97C File Offset: 0x0003DB7C
		internal override void ScenarioSucceeded(Task scenarioTask)
		{
			this.TraceInformation("Scenario succeeded", new object[0]);
			base.Result.ResultType = ResultType.Succeeded;
			this.PopulateResult(null, this.responseTracker.Items);
			bool flag = true;
			if (base.Definition.Attributes != null && base.Definition.Attributes.ContainsKey("LogSuccessProbeResult") && bool.TryParse(base.Definition.Attributes["LogSuccessProbeResult"], out flag) && flag)
			{
				this.FlushTracesIntoLogFile();
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0003FA08 File Offset: 0x0003DC08
		internal override void ScenarioFailed(Task scenarioTask)
		{
			try
			{
				this.TraceInformation("Scenario failed", new object[0]);
				this.TraceInformation(scenarioTask.Exception.ToString(), new object[0]);
				base.Result.ResultType = ResultType.Failed;
				this.PopulateResult(scenarioTask.Exception.InnerException.GetScenarioException(), this.responseTracker.Items);
				this.FlushTracesIntoLogFile();
				scenarioTask.Exception.Flatten().Handle((Exception e) => false);
			}
			catch (Exception ex)
			{
				this.TraceInformation("Exception thrown from EacWebClientProbeBase.ScenarioFailed() method: {0}", new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0003FACC File Offset: 0x0003DCCC
		internal override void ScenarioCancelled(Task scenarioTask)
		{
			this.TraceInformation("Scenario cancelled", new object[0]);
			this.PopulateResult(null, this.responseTracker.Items);
			base.Result.Error = "Skipped";
			base.Result.FailureContext = this.CancelReason;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0003FB20 File Offset: 0x0003DD20
		private void FlushTracesIntoLogFile()
		{
			if (base.Definition.Attributes.ContainsKey("LogFileInstanceName"))
			{
				string text = base.Definition.Attributes["LogFileInstanceName"];
				if (!string.IsNullOrWhiteSpace(text))
				{
					text = string.Format("ECP\\{0}", text);
					MonitoringLogConfiguration logConfiguration = new MonitoringLogConfiguration(text);
					this.TraceCollector.FlushToFile(text, logConfiguration);
				}
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0003FB84 File Offset: 0x0003DD84
		private void PopulateRequestInformation(IEnumerable<ResponseTrackerItem> responseItems, Dictionary<string, string> resultPairs)
		{
			int num = 0;
			foreach (ResponseTrackerItem responseTrackerItem in responseItems)
			{
				if (responseTrackerItem.TargetType == RequestTarget.Ecp.ToString())
				{
					string arg = Regex.Replace(responseTrackerItem.PathAndQuery, "msExchEcpCanary=([^&]+)", "msExchEcpCanary=...");
					resultPairs.Add(string.Format("{0}", num), string.Format("{0}{1},{2}", responseTrackerItem.TargetHost, arg, responseTrackerItem.TotalLatency));
				}
				else
				{
					resultPairs.Add(string.Format("{0}", num), string.Format("{0}{1},{2}", responseTrackerItem.TargetHost, responseTrackerItem.PathAndQuery.Split(new char[]
					{
						'?'
					})[0], responseTrackerItem.TotalLatency));
				}
				num++;
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0003FC9C File Offset: 0x0003DE9C
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
				base.Result.StateAttribute22 = exception.ExceptionHint;
				IEnumerable<ResponseTrackerItem> enumerable = from item in responseItems
				where item.FailingServer != null && item.FailureHttpResponseCode != null
				select item;
				if (enumerable != null && enumerable.Count<ResponseTrackerItem>() > 0)
				{
					ResponseTrackerItem responseTrackerItem = enumerable.First<ResponseTrackerItem>();
					resultPairs.Add("Server", responseTrackerItem.FailingServer);
					resultPairs.Add("HttpStatus", responseTrackerItem.FailureHttpResponseCode.Value.ToString());
					base.Result.FailureContext = responseTrackerItem.FailingServer;
					base.Result.StateAttribute15 = responseTrackerItem.FailingTargetHostname;
					base.Result.StateAttribute21 = responseTrackerItem.FailingTargetIPAddress;
				}
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0003FF5C File Offset: 0x0003E15C
		private void PopulateLatencyInformation(IEnumerable<ResponseTrackerItem> responseItems, Dictionary<string, string> resultPairs)
		{
			var enumerable = from item in responseItems
			group item by item.TargetType into g
			select new
			{
				TargetType = g.Key,
				TotalLatency = g.Sum((ResponseTrackerItem item) => item.TotalLatency.TotalMilliseconds)
			};
			base.Result.SampleValue = (double)this.responseTracker.Items.Sum((ResponseTrackerItem i) => i.TotalLatency.Milliseconds);
			foreach (var <>f__AnonymousType in enumerable)
			{
				RequestTarget target = (RequestTarget)Enum.Parse(typeof(RequestTarget), <>f__AnonymousType.TargetType);
				resultPairs.Add(this.GetTargetAbbreviation(target) + "_LTCY", <>f__AnonymousType.TotalLatency.ToString());
				switch (target)
				{
				case RequestTarget.Ecp:
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

		// Token: 0x06000A1F RID: 2591 RVA: 0x0004018C File Offset: 0x0003E38C
		private void PopulateServerInformation(IEnumerable<ResponseTrackerItem> responseItems, Dictionary<string, string> resultPairs)
		{
			IEnumerable<string> list = from item in responseItems
			where (item.TargetType == RequestTarget.Owa.ToString() || item.TargetType == RequestTarget.Ecp.ToString()) && item.RespondingServer != null
			select item.RespondingServer;
			string stateAttribute = null;
			string stateAttribute2 = null;
			string text = this.AggregateIntoCommaSeparatedList(list, out stateAttribute, out stateAttribute2);
			string text2 = this.AggregateIntoCommaSeparatedList(from item in responseItems
			where item.TargetType == RequestTarget.LiveIdConsumer.ToString() && item.RespondingServer != null
			select item.RespondingServer);
			string text3 = this.AggregateIntoCommaSeparatedList(from item in responseItems
			where item.TargetType == RequestTarget.LiveIdBusiness.ToString() && item.RespondingServer != null
			select item.RespondingServer);
			string text4 = this.AggregateIntoCommaSeparatedList(from item in responseItems
			where item.MailboxServer != null
			select item.MailboxServer);
			string text5 = this.AggregateIntoCommaSeparatedList(from item in responseItems
			where item.DomainController != null
			select item.DomainController);
			resultPairs.Add("CAS", text);
			resultPairs.Add("MBX", text4);
			resultPairs.Add("DC", text5);
			resultPairs.Add("LID_BUS", text3);
			resultPairs.Add("LID_CON", text2);
			base.Result.StateAttribute3 = text;
			base.Result.StateAttribute4 = text2;
			base.Result.StateAttribute5 = text3;
			base.Result.StateAttribute11 = stateAttribute;
			base.Result.StateAttribute12 = stateAttribute2;
			base.Result.StateAttribute13 = text4;
			base.Result.StateAttribute14 = text5;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x000403BC File Offset: 0x0003E5BC
		private void PopulateResult(ScenarioException exception, IEnumerable<ResponseTrackerItem> responseItems)
		{
			base.Result.Version = 1;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("User", base.Definition.Account);
			this.PopulateServerInformation(responseItems, dictionary);
			this.PopulateFailingInformation(exception, responseItems, dictionary);
			this.PopulateRequestInformation(responseItems, dictionary);
			this.PopulateLatencyInformation(responseItems, dictionary);
			base.Result.ExecutionContext = this.TransformToString(dictionary);
			base.Result.StateAttribute23 = base.Definition.Account;
			if (!string.IsNullOrEmpty(base.Definition.SecondaryAccount))
			{
				base.Result.StateAttribute24 = base.Definition.SecondaryAccount.Split(new char[]
				{
					'@'
				})[1];
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00040478 File Offset: 0x0003E678
		private string GetTargetAbbreviation(RequestTarget target)
		{
			switch (target)
			{
			case RequestTarget.Owa:
				return "OWA";
			case RequestTarget.Ecp:
				return "EAC";
			case RequestTarget.LiveIdConsumer:
				return "LID_CON";
			case RequestTarget.LiveIdBusiness:
				return "LID_BUS";
			}
			return "UNK";
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000404CC File Offset: 0x0003E6CC
		private string AggregateIntoCommaSeparatedList(IEnumerable<string> list, out string first, out string last)
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

		// Token: 0x06000A23 RID: 2595 RVA: 0x0004056C File Offset: 0x0003E76C
		private string AggregateIntoCommaSeparatedList(IEnumerable<string> list)
		{
			string text;
			string text2;
			return this.AggregateIntoCommaSeparatedList(list, out text, out text2);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00040584 File Offset: 0x0003E784
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

		// Token: 0x0400072D RID: 1837
		public const string LogFileInstanceParameterName = "LogFileInstanceName";

		// Token: 0x0400072E RID: 1838
		public const string LogSuccessProbeResultParameterName = "LogSuccessProbeResult";

		// Token: 0x0400072F RID: 1839
		private const string EACLogFilePathTemplate = "ECP\\{0}";

		// Token: 0x04000730 RID: 1840
		private const int CurrentResultVersion = 1;

		// Token: 0x04000731 RID: 1841
		private IResponseTracker responseTracker;
	}
}
