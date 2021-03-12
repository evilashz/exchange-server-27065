using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200008D RID: 141
	public abstract class NotificationItem
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x0001D5E8 File Offset: 0x0001B7E8
		public NotificationItem(string serviceName, string component, string tag, string message, ResultSeverityLevel severity)
		{
			this.ServiceName = serviceName;
			this.ResultName = NotificationItem.GenerateResultName(serviceName, component, tag);
			this.Message = message;
			this.CustomProperties = new Dictionary<string, string>();
			this.Severity = severity;
			this.TimeStamp = DateTime.UtcNow;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001D636 File Offset: 0x0001B836
		public NotificationItem(string serviceName, string component, string tag, string message, string stateAttribute1, ResultSeverityLevel severity) : this(serviceName, component, tag, message, severity)
		{
			this.StateAttribute1 = stateAttribute1;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001D64D File Offset: 0x0001B84D
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001D655 File Offset: 0x0001B855
		public string ServiceName { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001D65E File Offset: 0x0001B85E
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001D666 File Offset: 0x0001B866
		public string ResultName { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001D66F File Offset: 0x0001B86F
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001D677 File Offset: 0x0001B877
		public Dictionary<string, string> CustomProperties { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001D680 File Offset: 0x0001B880
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001D688 File Offset: 0x0001B888
		public ResultSeverityLevel Severity { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001D691 File Offset: 0x0001B891
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001D699 File Offset: 0x0001B899
		public DateTime TimeStamp { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001D6A2 File Offset: 0x0001B8A2
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0001D6AA File Offset: 0x0001B8AA
		public double SampleValue { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001D6B3 File Offset: 0x0001B8B3
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001D6BB File Offset: 0x0001B8BB
		public string Message { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		public string StateAttribute1 { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001D6D5 File Offset: 0x0001B8D5
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001D6DD File Offset: 0x0001B8DD
		public string StateAttribute2 { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001D6E6 File Offset: 0x0001B8E6
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001D6EE File Offset: 0x0001B8EE
		public string StateAttribute3 { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001D6F7 File Offset: 0x0001B8F7
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001D6FF File Offset: 0x0001B8FF
		public string StateAttribute4 { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001D708 File Offset: 0x0001B908
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001D710 File Offset: 0x0001B910
		public string StateAttribute5 { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001D719 File Offset: 0x0001B919
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001D721 File Offset: 0x0001B921
		public string Exception { get; set; }

		// Token: 0x06000734 RID: 1844 RVA: 0x0001D72C File Offset: 0x0001B92C
		public static string GenerateResultName(string serviceName, string component, string tag = null)
		{
			if (string.IsNullOrEmpty(serviceName))
			{
				throw new ArgumentException("String argument cannot be null or empty", "serviceName");
			}
			if (string.IsNullOrEmpty(component))
			{
				throw new ArgumentException("String argument cannot be null or empty", "component");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}/{1}", serviceName, component);
			if (!string.IsNullOrEmpty(tag))
			{
				stringBuilder.AppendFormat("/{0}", tag);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001D798 File Offset: 0x0001B998
		public void AddCustomProperty(string name, object value)
		{
			this.CustomProperties.Add(name, value.ToString());
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001D7AC File Offset: 0x0001B9AC
		public void Publish(bool throwOnError = false)
		{
			string extensionXml = CrimsonHelper.ConvertDictionaryToXml(this.CustomProperties);
			ProbeResult probeResult = new ProbeResult();
			probeResult.ResultName = this.ResultName;
			probeResult.SampleValue = this.SampleValue;
			probeResult.ServiceName = this.ServiceName;
			probeResult.IsNotified = true;
			probeResult.ExecutionStartTime = (probeResult.ExecutionEndTime = this.TimeStamp);
			probeResult.Error = this.Message;
			probeResult.ExtensionXml = extensionXml;
			probeResult.StateAttribute1 = this.StateAttribute1;
			probeResult.StateAttribute2 = this.StateAttribute2;
			probeResult.StateAttribute3 = this.StateAttribute3;
			probeResult.StateAttribute4 = this.StateAttribute4;
			probeResult.StateAttribute5 = this.StateAttribute5;
			probeResult.Exception = this.Exception;
			probeResult.WorkItemId = DefinitionIdGenerator<ProbeDefinition>.GetIdForNotification(this.ResultName);
			switch (this.Severity)
			{
			case ResultSeverityLevel.Informational:
			case ResultSeverityLevel.Verbose:
				probeResult.ResultType = ResultType.Succeeded;
				break;
			default:
				probeResult.ResultType = ResultType.Failed;
				break;
			}
			try
			{
				probeResult.Write(null);
			}
			catch (Exception arg)
			{
				WTFDiagnostics.TraceDebug<Exception>(WTFLog.DataAccess, TracingContext.Default, "Notification publishing failed with exception {0}", arg, null, "Publish", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\NotificationItem.cs", 254);
				if (throwOnError)
				{
					throw;
				}
			}
		}
	}
}
