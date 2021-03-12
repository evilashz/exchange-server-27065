using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200002F RID: 47
	[Table]
	public sealed class ResponderDefinition : WorkDefinition, IPersistence
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000D712 File Offset: 0x0000B912
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000D71A File Offset: 0x0000B91A
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public override int Id { get; internal set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000D723 File Offset: 0x0000B923
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000D72B File Offset: 0x0000B92B
		[Column]
		public override string AssemblyPath { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000D734 File Offset: 0x0000B934
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000D73C File Offset: 0x0000B93C
		[Column]
		public override string TypeName { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000D745 File Offset: 0x0000B945
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000D74D File Offset: 0x0000B94D
		[Column]
		public override string Name { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000D756 File Offset: 0x0000B956
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000D75E File Offset: 0x0000B95E
		[Column]
		public override string WorkItemVersion { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000D767 File Offset: 0x0000B967
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000D76F File Offset: 0x0000B96F
		[Column]
		public override string ServiceName { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000D778 File Offset: 0x0000B978
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000D780 File Offset: 0x0000B980
		[Column]
		public override int DeploymentId { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000D789 File Offset: 0x0000B989
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000D791 File Offset: 0x0000B991
		[Column]
		public override string ExecutionLocation { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000D79A File Offset: 0x0000B99A
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000D7A2 File Offset: 0x0000B9A2
		[Column]
		public override DateTime CreatedTime { get; internal set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000D7AB File Offset: 0x0000B9AB
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000D7B3 File Offset: 0x0000B9B3
		[Column]
		public override bool Enabled { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		[Column]
		public override string TargetPartition { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000D7CD File Offset: 0x0000B9CD
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000D7D5 File Offset: 0x0000B9D5
		[Column]
		public override string TargetGroup { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000D7DE File Offset: 0x0000B9DE
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000D7E6 File Offset: 0x0000B9E6
		[Column]
		public override string TargetResource { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000D7EF File Offset: 0x0000B9EF
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000D7F7 File Offset: 0x0000B9F7
		[Column]
		public override string TargetExtension { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000D800 File Offset: 0x0000BA00
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000D808 File Offset: 0x0000BA08
		[Column]
		public override string TargetVersion { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000D811 File Offset: 0x0000BA11
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000D819 File Offset: 0x0000BA19
		[Column]
		public override int RecurrenceIntervalSeconds { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000D822 File Offset: 0x0000BA22
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000D82A File Offset: 0x0000BA2A
		[Column]
		public override int TimeoutSeconds { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000D833 File Offset: 0x0000BA33
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000D83B File Offset: 0x0000BA3B
		[Column]
		public override DateTime StartTime { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000D844 File Offset: 0x0000BA44
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0000D84C File Offset: 0x0000BA4C
		[Column]
		public override DateTime UpdateTime { get; internal set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000D855 File Offset: 0x0000BA55
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000D85D File Offset: 0x0000BA5D
		[Column]
		public override int MaxRetryAttempts { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000D866 File Offset: 0x0000BA66
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000D86E File Offset: 0x0000BA6E
		[Column]
		public override string ExtensionAttributes { get; internal set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000D877 File Offset: 0x0000BA77
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000D87F File Offset: 0x0000BA7F
		[PropertyInformation("The filter (by default a prefix filter over MonitorResult.AlertName) identifying the alerts to which this responder reacts.", false)]
		[Column]
		public string AlertMask { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000D888 File Offset: 0x0000BA88
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000D890 File Offset: 0x0000BA90
		[Column]
		[PropertyInformation("The amount of time to wait after attempting another response.", false)]
		public int WaitIntervalSeconds { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000D899 File Offset: 0x0000BA99
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000D8A1 File Offset: 0x0000BAA1
		[Column]
		[PropertyInformation("The amount of time between two escalates.", false)]
		public int MinimumSecondsBetweenEscalates { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000D8AA File Offset: 0x0000BAAA
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000D8B2 File Offset: 0x0000BAB2
		[Column]
		[PropertyInformation("The subject text to be included in the escalation mail.", false)]
		public string EscalationSubject { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000D8BB File Offset: 0x0000BABB
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000D8C3 File Offset: 0x0000BAC3
		[Column]
		[PropertyInformation("The message body to be included in the escalation mail.", false)]
		public string EscalationMessage { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000D8CC File Offset: 0x0000BACC
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		[PropertyInformation("The name of the service that the team belongs to.", false)]
		[Column]
		public string EscalationService { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000D8DD File Offset: 0x0000BADD
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000D8E5 File Offset: 0x0000BAE5
		[Column]
		[PropertyInformation("The name of the team to escalate to.", false)]
		public string EscalationTeam { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000D8EE File Offset: 0x0000BAEE
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000D8F6 File Offset: 0x0000BAF6
		[Column]
		public NotificationServiceClass NotificationServiceClass { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000D8FF File Offset: 0x0000BAFF
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000D907 File Offset: 0x0000BB07
		[Column]
		public string DailySchedulePattern { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000D910 File Offset: 0x0000BB10
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000D918 File Offset: 0x0000BB18
		[Column]
		public bool AlwaysEscalateOnMonitorChanges { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000D921 File Offset: 0x0000BB21
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000D929 File Offset: 0x0000BB29
		[PropertyInformation("The CentralAdmin powershell endpoint to use.", false)]
		[Column]
		public string Endpoint { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000D932 File Offset: 0x0000BB32
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000D93A File Offset: 0x0000BB3A
		[Column]
		public override int CreatedById { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000D943 File Offset: 0x0000BB43
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000D94B File Offset: 0x0000BB4B
		[Column]
		[PropertyInformation("The certificate subject or account to use when connection to CentralAdmin.", false)]
		public string Account { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000D954 File Offset: 0x0000BB54
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000D95C File Offset: 0x0000BB5C
		[PropertyInformation("The password to use when connecting to CentralAdmin. This should be set only if the auth method is not cert-based.", false)]
		[Column]
		public string AccountPassword { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000D965 File Offset: 0x0000BB65
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000D96D File Offset: 0x0000BB6D
		[PropertyInformation("The unique alert id associated with this type of alert. This should be a human readable string in the format [Team]/[Component]/[AlertType].", false)]
		[Column]
		public string AlertTypeId { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000D976 File Offset: 0x0000BB76
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000D97E File Offset: 0x0000BB7E
		[Column]
		public ServiceHealthStatus TargetHealthState { get; internal set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000D987 File Offset: 0x0000BB87
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000D98F File Offset: 0x0000BB8F
		[Column]
		public string CorrelatedMonitorsXml
		{
			get
			{
				return this.correlatedMonitorsXml;
			}
			set
			{
				this.correlatedMonitorsXml = value;
				this.correlatedMonitors = this.ConvertXmlToCorrelatedMonitors(this.correlatedMonitorsXml);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000D9AA File Offset: 0x0000BBAA
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000D9B2 File Offset: 0x0000BBB2
		[Column]
		public CorrelatedMonitorAction ActionOnCorrelatedMonitors { get; internal set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000D9BB File Offset: 0x0000BBBB
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000D9C3 File Offset: 0x0000BBC3
		[Column]
		public string ResponderCategory { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		[Column]
		public string ThrottleGroupName { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000D9DD File Offset: 0x0000BBDD
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000D9E5 File Offset: 0x0000BBE5
		[Column]
		public string ThrottlePolicyXml { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000D9EE File Offset: 0x0000BBEE
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000D9F6 File Offset: 0x0000BBF6
		[Column]
		public bool UploadScopeNotification { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000D9FF File Offset: 0x0000BBFF
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000DA07 File Offset: 0x0000BC07
		[Column]
		public bool SuppressEscalation { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000DA10 File Offset: 0x0000BC10
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000DA18 File Offset: 0x0000BC18
		[Column]
		internal override int Version { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000DA21 File Offset: 0x0000BC21
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000DA29 File Offset: 0x0000BC29
		internal CorrelatedMonitorInfo[] CorrelatedMonitors
		{
			get
			{
				return this.correlatedMonitors;
			}
			set
			{
				if (value != null && value.Length > 10)
				{
					throw new InvalidOperationException("Maximum number of CorrelationMonitors can not exceeed 10");
				}
				this.correlatedMonitors = value;
				this.correlatedMonitorsXml = this.ConvertCorrelatedMonitorsToXml(this.correlatedMonitors);
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		public override void FromXml(XmlNode definition)
		{
			base.FromXml(definition);
			this.AlertMask = base.GetMandatoryXmlAttribute<string>(definition, "AlertMask");
			this.WaitIntervalSeconds = base.GetMandatoryXmlAttribute<int>(definition, "WaitIntervalSeconds");
			this.AlertTypeId = base.GetMandatoryXmlAttribute<string>(definition, "AlertTypeId");
			this.Account = base.GetOptionalXmlAttribute<string>(definition, "Account", string.Empty);
			this.AccountPassword = base.GetOptionalXmlAttribute<string>(definition, "AccountPassword", string.Empty);
			this.EscalationSubject = base.GetOptionalXmlAttribute<string>(definition, "EscalationSubject", string.Empty);
			this.EscalationMessage = base.GetOptionalXmlAttribute<string>(definition, "EscalationMessage", string.Empty);
			this.EscalationService = base.GetOptionalXmlAttribute<string>(definition, "EscalationService", string.Empty);
			this.EscalationTeam = base.GetOptionalXmlAttribute<string>(definition, "EscalationTeam", string.Empty);
			this.Endpoint = base.GetOptionalXmlAttribute<string>(definition, "Endpoint", string.Empty);
			this.TargetHealthState = base.GetOptionalXmlEnumAttribute<ServiceHealthStatus>(definition, "TargetHealthState", ServiceHealthStatus.None);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000DB5C File Offset: 0x0000BD5C
		internal string ConvertCorrelatedMonitorsToXml(CorrelatedMonitorInfo[] monitors)
		{
			if (monitors != null && monitors.Length > 0)
			{
				XElement xelement = new XElement("CorrelatedMonitors");
				foreach (CorrelatedMonitorInfo correlatedMonitorInfo in monitors)
				{
					xelement.Add(new XElement("Monitor", new object[]
					{
						new XAttribute("Identity", correlatedMonitorInfo.Identity),
						new XAttribute("ExceptionMatchString", correlatedMonitorInfo.MatchString ?? string.Empty),
						new XAttribute("ModeOfMatch", correlatedMonitorInfo.ModeOfMatch.ToString())
					}));
				}
				return xelement.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		internal CorrelatedMonitorInfo[] ConvertXmlToCorrelatedMonitors(string correlatedMonitorsXml)
		{
			if (!string.IsNullOrEmpty(correlatedMonitorsXml))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(correlatedMonitorsXml);
				return this.GetCorrelatedMonitors(xmlDocument);
			}
			return new CorrelatedMonitorInfo[0];
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000DC5C File Offset: 0x0000BE5C
		internal CorrelatedMonitorInfo[] GetCorrelatedMonitors(XmlNode definition)
		{
			List<CorrelatedMonitorInfo> list = new List<CorrelatedMonitorInfo>(10);
			XmlNode xmlNode = definition.SelectSingleNode("CorrelatedMonitors");
			if (xmlNode != null)
			{
				using (XmlNodeList childNodes = xmlNode.ChildNodes)
				{
					if (childNodes != null)
					{
						foreach (object obj in childNodes)
						{
							XmlNode definition2 = (XmlNode)obj;
							string mandatoryXmlAttribute = base.GetMandatoryXmlAttribute<string>(definition2, "Identity");
							string optionalXmlAttribute = base.GetOptionalXmlAttribute<string>(definition2, "ExceptionMatchString", string.Empty);
							CorrelatedMonitorInfo.MatchMode optionalXmlEnumAttribute = base.GetOptionalXmlEnumAttribute<CorrelatedMonitorInfo.MatchMode>(definition2, "ModeOfMatch", CorrelatedMonitorInfo.MatchMode.Wildcard);
							CorrelatedMonitorInfo correlatedMonitorInfo = new CorrelatedMonitorInfo(mandatoryXmlAttribute, optionalXmlAttribute, optionalXmlEnumAttribute);
							WTFDiagnostics.TraceDebug<string, string, CorrelatedMonitorInfo.MatchMode>(WTFLog.ManagedAvailability, base.TraceContext, "[CorrelatedMonitor] {0} ExceptionMatch:{1} ModeOfMatch:{2}", correlatedMonitorInfo.Identity, correlatedMonitorInfo.MatchString, correlatedMonitorInfo.ModeOfMatch, null, "GetCorrelatedMonitors", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\ResponderWorkDefinition.cs", 543);
							list.Add(correlatedMonitorInfo);
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		internal override WorkItemResult CreateResult()
		{
			return new ResponderResult(this);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000DD84 File Offset: 0x0000BF84
		internal override bool Validate(List<string> errors)
		{
			int count = errors.Count;
			base.Validate(errors);
			if (string.IsNullOrWhiteSpace(this.AlertMask))
			{
				errors.Add("AlertMask cannot be null or empty. ");
			}
			if (string.IsNullOrWhiteSpace(this.AlertTypeId))
			{
				errors.Add("AlertTypeId cannot be null or empty. ");
			}
			if (this.WaitIntervalSeconds < -1)
			{
				errors.Add("WaitIntervalSeconds cannot be less than -1. ");
			}
			return count == errors.Count;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000DDED File Offset: 0x0000BFED
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000DDF5 File Offset: 0x0000BFF5
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; private set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000DDFE File Offset: 0x0000BFFE
		internal static int SchemaVersion
		{
			get
			{
				return ResponderDefinition.schemaVersion;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000DE05 File Offset: 0x0000C005
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000DE0C File Offset: 0x0000C00C
		internal static IEnumerable<WorkDefinitionOverride> GlobalOverrides { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000DE14 File Offset: 0x0000C014
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000DE1B File Offset: 0x0000C01B
		internal static IEnumerable<WorkDefinitionOverride> LocalOverrides { get; set; }

		// Token: 0x060003AF RID: 943 RVA: 0x0000DE23 File Offset: 0x0000C023
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000DE34 File Offset: 0x0000C034
		public void SetProperties(Dictionary<string, string> propertyBag)
		{
			string text;
			if (propertyBag.TryGetValue("Id", out text) && !string.IsNullOrEmpty(text))
			{
				this.Id = int.Parse(text);
			}
			if (propertyBag.TryGetValue("AssemblyPath", out text))
			{
				this.AssemblyPath = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TypeName", out text))
			{
				this.TypeName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Name", out text))
			{
				this.Name = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("WorkItemVersion", out text))
			{
				this.WorkItemVersion = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("ServiceName", out text))
			{
				this.ServiceName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("DeploymentId", out text) && !string.IsNullOrEmpty(text))
			{
				this.DeploymentId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ExecutionLocation", out text))
			{
				this.ExecutionLocation = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("CreatedTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.CreatedTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("Enabled", out text) && !string.IsNullOrEmpty(text))
			{
				this.Enabled = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("TargetPartition", out text))
			{
				this.TargetPartition = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetGroup", out text))
			{
				this.TargetGroup = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetResource", out text))
			{
				this.TargetResource = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetExtension", out text))
			{
				this.TargetExtension = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetVersion", out text))
			{
				this.TargetVersion = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("RecurrenceIntervalSeconds", out text) && !string.IsNullOrEmpty(text))
			{
				this.RecurrenceIntervalSeconds = int.Parse(text);
			}
			if (propertyBag.TryGetValue("TimeoutSeconds", out text) && !string.IsNullOrEmpty(text))
			{
				this.TimeoutSeconds = int.Parse(text);
			}
			if (propertyBag.TryGetValue("StartTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.StartTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("UpdateTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.UpdateTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("MaxRetryAttempts", out text) && !string.IsNullOrEmpty(text))
			{
				this.MaxRetryAttempts = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ExtensionAttributes", out text))
			{
				this.ExtensionAttributes = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("AlertMask", out text))
			{
				this.AlertMask = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("WaitIntervalSeconds", out text) && !string.IsNullOrEmpty(text))
			{
				this.WaitIntervalSeconds = int.Parse(text);
			}
			if (propertyBag.TryGetValue("MinimumSecondsBetweenEscalates", out text) && !string.IsNullOrEmpty(text))
			{
				this.MinimumSecondsBetweenEscalates = int.Parse(text);
			}
			if (propertyBag.TryGetValue("EscalationSubject", out text))
			{
				this.EscalationSubject = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("EscalationMessage", out text))
			{
				this.EscalationMessage = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("EscalationService", out text))
			{
				this.EscalationService = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("EscalationTeam", out text))
			{
				this.EscalationTeam = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("NotificationServiceClass", out text) && !string.IsNullOrEmpty(text))
			{
				this.NotificationServiceClass = (NotificationServiceClass)Enum.Parse(typeof(NotificationServiceClass), text);
			}
			if (propertyBag.TryGetValue("DailySchedulePattern", out text))
			{
				this.DailySchedulePattern = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("AlwaysEscalateOnMonitorChanges", out text) && !string.IsNullOrEmpty(text))
			{
				this.AlwaysEscalateOnMonitorChanges = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("Endpoint", out text))
			{
				this.Endpoint = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("CreatedById", out text) && !string.IsNullOrEmpty(text))
			{
				this.CreatedById = int.Parse(text);
			}
			if (propertyBag.TryGetValue("Account", out text))
			{
				this.Account = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("AlertTypeId", out text))
			{
				this.AlertTypeId = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetHealthState", out text) && !string.IsNullOrEmpty(text))
			{
				this.TargetHealthState = (ServiceHealthStatus)Enum.Parse(typeof(ServiceHealthStatus), text);
			}
			if (propertyBag.TryGetValue("CorrelatedMonitorsXml", out text))
			{
				this.CorrelatedMonitorsXml = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("ActionOnCorrelatedMonitors", out text) && !string.IsNullOrEmpty(text))
			{
				this.ActionOnCorrelatedMonitors = (CorrelatedMonitorAction)Enum.Parse(typeof(CorrelatedMonitorAction), text);
			}
			if (propertyBag.TryGetValue("ResponderCategory", out text))
			{
				this.ResponderCategory = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("ThrottleGroupName", out text))
			{
				this.ThrottleGroupName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("ThrottlePolicyXml", out text))
			{
				this.ThrottlePolicyXml = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("UploadScopeNotification", out text) && !string.IsNullOrEmpty(text))
			{
				this.UploadScopeNotification = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("SuppressEscalation", out text) && !string.IsNullOrEmpty(text))
			{
				this.SuppressEscalation = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("Version", out text) && !string.IsNullOrEmpty(text))
			{
				this.Version = int.Parse(text);
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			DefinitionIdGenerator<ResponderDefinition>.AssignId(this);
			Update<ResponderDefinition>.ApplyUpdates(this);
			if (ResponderDefinition.GlobalOverrides != null)
			{
				foreach (WorkDefinitionOverride definitionOverride in ResponderDefinition.GlobalOverrides)
				{
					definitionOverride.TryApplyTo(this, base.TraceContext);
				}
			}
			if (ResponderDefinition.LocalOverrides != null)
			{
				foreach (WorkDefinitionOverride definitionOverride2 in ResponderDefinition.LocalOverrides)
				{
					definitionOverride2.TryApplyTo(this, base.TraceContext);
				}
			}
			if (preWriteHandler != null)
			{
				preWriteHandler(this);
			}
			NativeMethods.ResponderDefinitionUnmanaged responderDefinitionUnmanaged = this.ToUnmanaged();
			NativeMethods.WriteResponderDefinition(ref responderDefinitionUnmanaged);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000E490 File Offset: 0x0000C690
		internal NativeMethods.ResponderDefinitionUnmanaged ToUnmanaged()
		{
			return new NativeMethods.ResponderDefinitionUnmanaged
			{
				Id = this.Id,
				AssemblyPath = CrimsonHelper.NullCode(this.AssemblyPath),
				TypeName = CrimsonHelper.NullCode(this.TypeName),
				Name = CrimsonHelper.NullCode(this.Name),
				WorkItemVersion = CrimsonHelper.NullCode(this.WorkItemVersion),
				ServiceName = CrimsonHelper.NullCode(this.ServiceName),
				DeploymentId = this.DeploymentId,
				ExecutionLocation = CrimsonHelper.NullCode(this.ExecutionLocation),
				CreatedTime = this.CreatedTime.ToUniversalTime().ToString("o"),
				Enabled = (this.Enabled ? 1 : 0),
				TargetPartition = CrimsonHelper.NullCode(this.TargetPartition),
				TargetGroup = CrimsonHelper.NullCode(this.TargetGroup),
				TargetResource = CrimsonHelper.NullCode(this.TargetResource),
				TargetExtension = CrimsonHelper.NullCode(this.TargetExtension),
				TargetVersion = CrimsonHelper.NullCode(this.TargetVersion),
				RecurrenceIntervalSeconds = this.RecurrenceIntervalSeconds,
				TimeoutSeconds = this.TimeoutSeconds,
				StartTime = this.StartTime.ToUniversalTime().ToString("o"),
				UpdateTime = this.UpdateTime.ToUniversalTime().ToString("o"),
				MaxRetryAttempts = this.MaxRetryAttempts,
				ExtensionAttributes = CrimsonHelper.NullCode(this.ExtensionAttributes),
				AlertMask = CrimsonHelper.NullCode(this.AlertMask),
				WaitIntervalSeconds = this.WaitIntervalSeconds,
				MinimumSecondsBetweenEscalates = this.MinimumSecondsBetweenEscalates,
				EscalationSubject = CrimsonHelper.NullCode(this.EscalationSubject),
				EscalationMessage = CrimsonHelper.NullCode(this.EscalationMessage),
				EscalationService = CrimsonHelper.NullCode(this.EscalationService),
				EscalationTeam = CrimsonHelper.NullCode(this.EscalationTeam),
				NotificationServiceClass = this.NotificationServiceClass,
				DailySchedulePattern = CrimsonHelper.NullCode(this.DailySchedulePattern),
				AlwaysEscalateOnMonitorChanges = (this.AlwaysEscalateOnMonitorChanges ? 1 : 0),
				Endpoint = CrimsonHelper.NullCode(this.Endpoint),
				CreatedById = this.CreatedById,
				Account = CrimsonHelper.NullCode(this.Account),
				AlertTypeId = CrimsonHelper.NullCode(this.AlertTypeId),
				TargetHealthState = this.TargetHealthState,
				CorrelatedMonitorsXml = CrimsonHelper.NullCode(this.CorrelatedMonitorsXml),
				ActionOnCorrelatedMonitors = this.ActionOnCorrelatedMonitors,
				ResponderCategory = CrimsonHelper.NullCode(this.ResponderCategory),
				ThrottleGroupName = CrimsonHelper.NullCode(this.ThrottleGroupName),
				ThrottlePolicyXml = CrimsonHelper.NullCode(this.ThrottlePolicyXml),
				UploadScopeNotification = (this.UploadScopeNotification ? 1 : 0),
				SuppressEscalation = (this.SuppressEscalation ? 1 : 0),
				Version = this.Version
			};
		}

		// Token: 0x040002AD RID: 685
		private CorrelatedMonitorInfo[] correlatedMonitors;

		// Token: 0x040002AE RID: 686
		private string correlatedMonitorsXml;

		// Token: 0x040002AF RID: 687
		private static int schemaVersion = 65536;
	}
}
