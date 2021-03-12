using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000083 RID: 131
	internal class DnsProbeOperation
	{
		// Token: 0x06000372 RID: 882 RVA: 0x00014D40 File Offset: 0x00012F40
		private DnsProbeOperation(DnsProbeOperation.OperationAttributes attributes, TracingContext traceContext, DnsConfiguration dnsConfig)
		{
			this.traceContext = traceContext;
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			if (dnsConfig == null)
			{
				throw new ArgumentNullException("dnsConfig");
			}
			this.operationAttributes = attributes.GetClone();
			this.dnsConfig = dnsConfig;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00014D7E File Offset: 0x00012F7E
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00014D86 File Offset: 0x00012F86
		public string ErrorMessage { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00014D8F File Offset: 0x00012F8F
		public bool ExitOnFailure
		{
			get
			{
				return this.operationAttributes.ExitOnFailure;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00014D9C File Offset: 0x00012F9C
		public int ProbeRetryAttempts
		{
			get
			{
				return this.operationAttributes.ProbeRetryAttempts;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00014DAC File Offset: 0x00012FAC
		public static List<DnsProbeOperation> GetOperations(XmlElement operationsNode, DnsConfiguration dnsConfig, TracingContext traceContext)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, traceContext, "DnsProbeOperation: Trying to get list of Operations", null, "GetOperations", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 114);
			if (operationsNode == null)
			{
				throw new ArgumentNullException("operationsNode");
			}
			if (operationsNode.ChildNodes.Count == 0)
			{
				throw new ArgumentException("Atleast one operation needs to be specified", "operationsNode.ChildNodes.Count");
			}
			if (dnsConfig == null)
			{
				throw new ArgumentNullException("dnsConfig");
			}
			List<DnsProbeOperation> list = new List<DnsProbeOperation>();
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, traceContext, "DnsProbeOperation: Parsing operation attributes", null, "GetOperations", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 133);
			DnsProbeOperation.OperationAttributes operationAttributes = new DnsProbeOperation.OperationAttributes();
			operationAttributes.Sla = TimeSpan.FromSeconds((double)Utils.GetInteger(Utils.GetMandatoryXmlAttribute<string>(operationsNode, "SlaSeconds"), "DnsOperations.SlaSeconds", 1, 1));
			operationAttributes.RequestTimeout = TimeSpan.FromSeconds((double)Utils.GetInteger(Utils.GetMandatoryXmlAttribute<string>(operationsNode, "TimeoutSeconds"), "DnsOperations.TimeoutSeconds", 1, 1));
			operationAttributes.SocketRetryAttempts = Utils.GetInteger(Utils.GetMandatoryXmlAttribute<string>(operationsNode, "SocketRetryAttempts"), "DnsOperations.SocketRetryAttempts", 0, 0);
			operationAttributes.ProbeRetryAttempts = Utils.GetInteger(Utils.GetOptionalXmlAttribute<string>(operationsNode, "ProbeRetryAttempts", null), "DnsOperations.ProbeRetryAttempts", 0, 0);
			operationAttributes.ExitOnFailure = Utils.GetOptionalXmlAttribute<bool>(operationsNode, "ExitOnFailure", false);
			if (operationAttributes.RequestTimeout < operationAttributes.Sla)
			{
				throw new DnsMonitorException(string.Format("RequestTimeout should be >= Sla, actual RequestTimeout={0}, Sla={1}", operationAttributes.RequestTimeout, operationAttributes.Sla), null);
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, traceContext, "DnsProbeOperation: Parsing operation nodes", null, "GetOperations", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 147);
			foreach (object obj in operationsNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					operationAttributes.DomainSelectionFilter = Utils.GetEnumValue<DomainSelection>(xmlElement.GetAttribute("DomainSelection"), "DnsOperations/Operation.DomainSelection");
					if (operationAttributes.DomainSelectionFilter == DomainSelection.CustomQuery)
					{
						operationAttributes.DomainKey = Utils.CheckNullOrWhiteSpace(Utils.GetMandatoryXmlAttribute<string>(xmlElement, "DomainKey"), "DomainKey").Trim();
						operationAttributes.ZoneName = Utils.CheckNullOrWhiteSpace(Utils.GetMandatoryXmlAttribute<string>(xmlElement, "ZoneName"), "ZoneName").Trim();
					}
					operationAttributes.QueryType = Utils.GetEnumValue<RecordType>(xmlElement.GetAttribute("QueryType"), "DnsOperations/Operation.QueryType");
					operationAttributes.QueryClass = Utils.GetEnumValue<RecordClass>(xmlElement.GetAttribute("QueryClass"), "DnsOperations/Operation.QueryClass");
					operationAttributes.ExpectedResponseCode = Utils.GetEnumValue<QueryResponseCode>(xmlElement.GetAttribute("ExpectedResponse"), "DnsOperations/Operation.ExpectedResponse");
					DnsProbeOperation.AddOperations(list, dnsConfig, operationAttributes, traceContext);
				}
			}
			return list;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00015048 File Offset: 0x00013248
		public override string ToString()
		{
			return string.Format("Op={0}, {1}", this.operationAttributes, this.ErrorMessage);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00015060 File Offset: 0x00013260
		public bool Invoke(CancellationToken cancellationToken)
		{
			IPEndPoint server = new IPEndPoint(this.operationAttributes.ServerIPAddress, 53);
			WTFDiagnostics.TraceInformation<DnsProbeOperation>(ExTraceGlobals.DNSTracer, this.traceContext, "DnsProbeOperation: Processing DNS request for operation, {0}", this, null, "Invoke", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 203);
			string errorMessage;
			if (DnsHelper.ProcessDNSRequest(this.operationAttributes.DomainName, this.operationAttributes.QueryType, this.operationAttributes.QueryClass, server, this.operationAttributes.Sla, this.operationAttributes.RequestTimeout, this.operationAttributes.SocketRetryAttempts, this.operationAttributes.ExpectedResponseCode, this.operationAttributes.DomainName.StartsWith(this.dnsConfig.IpV6Prefix), out errorMessage, cancellationToken))
			{
				WTFDiagnostics.TraceInformation<DnsProbeOperation>(ExTraceGlobals.DNSTracer, this.traceContext, "DnsProbeOperation: Operation Passed", this, null, "Invoke", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 217);
				return true;
			}
			this.ErrorMessage = errorMessage;
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DNSTracer, this.traceContext, "DnsProbeOperation: Operation Failed, message={0}", this.ErrorMessage, null, "Invoke", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 223);
			return false;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00015178 File Offset: 0x00013378
		public bool CanInvoke()
		{
			bool result = false;
			WTFDiagnostics.TraceInformation<DnsProbeOperation>(ExTraceGlobals.DNSTracer, this.traceContext, "DnsProbeOperation: checking if the operation can be invoked, {0}", this, null, "CanInvoke", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 236);
			if (string.IsNullOrWhiteSpace(this.operationAttributes.DomainName))
			{
				if (this.operationAttributes.DomainSelectionFilter == DomainSelection.InvalidDomainWithFallback)
				{
					this.ErrorMessage = "Cannot execute the operation, as we do not have any domain with fallback";
				}
				else
				{
					if (this.operationAttributes.DomainSelectionFilter != DomainSelection.InvalidDomainWithoutFallback)
					{
						throw new DnsMonitorException("Internal error, the domainName for the query was not generated", null);
					}
					this.ErrorMessage = "Cannot execute the operation, as we do not have any domain without fallback";
				}
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DNSTracer, this.traceContext, "DnsProbeOperation: Operation CanInvoke check failed, message={0}", this.ErrorMessage, null, "CanInvoke", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 253);
			}
			else if (this.operationAttributes.DomainSelectionFilter == DomainSelection.CustomQuery && !this.dnsConfig.IsSupportedZone(this.operationAttributes.ZoneName))
			{
				this.ErrorMessage = string.Format("Cannot execute the operation, as the zone '{0}' is not supported", this.operationAttributes.ZoneName);
			}
			else
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DNSTracer, this.traceContext, "DnsProbeOperation: Operation CanInvoke check passed", null, "CanInvoke", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 262);
				result = true;
			}
			return result;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001529C File Offset: 0x0001349C
		private static void AddOperations(List<DnsProbeOperation> operations, DnsConfiguration dnsConfig, DnsProbeOperation.OperationAttributes operationAttributes, TracingContext traceContext)
		{
			List<string> list = null;
			if (operationAttributes.DomainSelectionFilter == DomainSelection.CustomQuery)
			{
				list = new List<string>
				{
					operationAttributes.DomainKey + "." + operationAttributes.ZoneName
				};
			}
			else
			{
				list = dnsConfig.GetDomainsToLookup(operationAttributes.DomainSelectionFilter);
			}
			if (list.Count > 0)
			{
				using (List<IPAddress>.Enumerator enumerator = dnsConfig.DnsServerIps.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IPAddress serverIPAddress = enumerator.Current;
						foreach (string domainName in list)
						{
							operationAttributes.DomainName = domainName;
							operationAttributes.ServerIPAddress = serverIPAddress;
							DnsProbeOperation dnsProbeOperation = new DnsProbeOperation(operationAttributes, traceContext, dnsConfig);
							WTFDiagnostics.TraceInformation<DnsProbeOperation>(ExTraceGlobals.DNSTracer, traceContext, "DnsProbeOperation: Adding operation, {0}", dnsProbeOperation, null, "AddOperations", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 300);
							operations.Add(dnsProbeOperation);
						}
					}
					return;
				}
			}
			operationAttributes.DomainName = null;
			DnsProbeOperation dnsProbeOperation2 = new DnsProbeOperation(operationAttributes, traceContext, dnsConfig);
			WTFDiagnostics.TraceInformation<DnsProbeOperation>(ExTraceGlobals.DNSTracer, traceContext, "DnsProbeOperation: Adding operation, {0}", dnsProbeOperation2, null, "AddOperations", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\DnsProbeOperation.cs", 311);
			operations.Add(dnsProbeOperation2);
		}

		// Token: 0x040001FC RID: 508
		private DnsProbeOperation.OperationAttributes operationAttributes;

		// Token: 0x040001FD RID: 509
		private TracingContext traceContext;

		// Token: 0x040001FE RID: 510
		private DnsConfiguration dnsConfig;

		// Token: 0x02000084 RID: 132
		private class OperationAttributes
		{
			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x0600037C RID: 892 RVA: 0x000153E8 File Offset: 0x000135E8
			// (set) Token: 0x0600037D RID: 893 RVA: 0x000153F0 File Offset: 0x000135F0
			public DomainSelection DomainSelectionFilter { get; set; }

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x0600037E RID: 894 RVA: 0x000153F9 File Offset: 0x000135F9
			// (set) Token: 0x0600037F RID: 895 RVA: 0x00015401 File Offset: 0x00013601
			public string DomainName { get; set; }

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000380 RID: 896 RVA: 0x0001540A File Offset: 0x0001360A
			// (set) Token: 0x06000381 RID: 897 RVA: 0x00015412 File Offset: 0x00013612
			public IPAddress ServerIPAddress { get; set; }

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000382 RID: 898 RVA: 0x0001541B File Offset: 0x0001361B
			// (set) Token: 0x06000383 RID: 899 RVA: 0x00015423 File Offset: 0x00013623
			public RecordType QueryType { get; set; }

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x06000384 RID: 900 RVA: 0x0001542C File Offset: 0x0001362C
			// (set) Token: 0x06000385 RID: 901 RVA: 0x00015434 File Offset: 0x00013634
			public RecordClass QueryClass { get; set; }

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x06000386 RID: 902 RVA: 0x0001543D File Offset: 0x0001363D
			// (set) Token: 0x06000387 RID: 903 RVA: 0x00015445 File Offset: 0x00013645
			public TimeSpan Sla { get; set; }

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x06000388 RID: 904 RVA: 0x0001544E File Offset: 0x0001364E
			// (set) Token: 0x06000389 RID: 905 RVA: 0x00015456 File Offset: 0x00013656
			public TimeSpan RequestTimeout { get; set; }

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x0600038A RID: 906 RVA: 0x0001545F File Offset: 0x0001365F
			// (set) Token: 0x0600038B RID: 907 RVA: 0x00015467 File Offset: 0x00013667
			public int ProbeRetryAttempts { get; set; }

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x0600038C RID: 908 RVA: 0x00015470 File Offset: 0x00013670
			// (set) Token: 0x0600038D RID: 909 RVA: 0x00015478 File Offset: 0x00013678
			public int SocketRetryAttempts { get; set; }

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600038E RID: 910 RVA: 0x00015481 File Offset: 0x00013681
			// (set) Token: 0x0600038F RID: 911 RVA: 0x00015489 File Offset: 0x00013689
			public bool ExitOnFailure { get; set; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000390 RID: 912 RVA: 0x00015492 File Offset: 0x00013692
			// (set) Token: 0x06000391 RID: 913 RVA: 0x0001549A File Offset: 0x0001369A
			public QueryResponseCode ExpectedResponseCode { get; set; }

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000392 RID: 914 RVA: 0x000154A3 File Offset: 0x000136A3
			// (set) Token: 0x06000393 RID: 915 RVA: 0x000154AB File Offset: 0x000136AB
			public string DomainKey { get; set; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000394 RID: 916 RVA: 0x000154B4 File Offset: 0x000136B4
			// (set) Token: 0x06000395 RID: 917 RVA: 0x000154BC File Offset: 0x000136BC
			public string ZoneName { get; set; }

			// Token: 0x06000396 RID: 918 RVA: 0x000154C8 File Offset: 0x000136C8
			public DnsProbeOperation.OperationAttributes GetClone()
			{
				return new DnsProbeOperation.OperationAttributes
				{
					DomainName = this.DomainName,
					DomainSelectionFilter = this.DomainSelectionFilter,
					ExpectedResponseCode = this.ExpectedResponseCode,
					QueryClass = this.QueryClass,
					QueryType = this.QueryType,
					RequestTimeout = this.RequestTimeout,
					ProbeRetryAttempts = this.ProbeRetryAttempts,
					SocketRetryAttempts = this.SocketRetryAttempts,
					ServerIPAddress = this.ServerIPAddress,
					Sla = this.Sla,
					ExitOnFailure = this.ExitOnFailure,
					DomainKey = this.DomainKey,
					ZoneName = this.ZoneName
				};
			}

			// Token: 0x06000397 RID: 919 RVA: 0x00015578 File Offset: 0x00013778
			public override string ToString()
			{
				return string.Format("F:{0}, Q:{1}, T:{2}, C:{3}", new object[]
				{
					this.DomainSelectionFilter,
					this.DomainName,
					this.QueryType,
					this.QueryClass
				});
			}
		}
	}
}
