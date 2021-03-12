using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200053F RID: 1343
	internal sealed class SmtpAvailabilityPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06003E53 RID: 15955 RVA: 0x0010796C File Offset: 0x00105B6C
		internal SmtpAvailabilityPerfCountersInstance(string instanceName, SmtpAvailabilityPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, SmtpAvailabilityPerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalConnections = new ExPerformanceCounter(base.CategoryName, "Total Connections", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalConnections);
				this.AvailabilityPercentage = new ExPerformanceCounter(base.CategoryName, "% Availability", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AvailabilityPercentage);
				this.ActivityPercentage = new ExPerformanceCounter(base.CategoryName, "% Activity", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityPercentage);
				this.FailuresDueToMaxInboundConnectionLimit = new ExPerformanceCounter(base.CategoryName, "% Failures Due To MaxInboundConnectionLimit", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToMaxInboundConnectionLimit);
				this.FailuresDueToWLIDDown = new ExPerformanceCounter(base.CategoryName, "% Failures Due To WLID Down", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToWLIDDown);
				this.FailuresDueToADDown = new ExPerformanceCounter(base.CategoryName, "% Failures Due To Active Directory Down", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToADDown);
				this.FailuresDueToBackPressure = new ExPerformanceCounter(base.CategoryName, "% Failures Due To Back Pressure", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToBackPressure);
				this.FailuresDueToIOExceptions = new ExPerformanceCounter(base.CategoryName, "% Failures Due To IO Exceptions", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToIOExceptions);
				this.FailuresDueToTLSErrors = new ExPerformanceCounter(base.CategoryName, "% Failures Due To TLS Errors", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToTLSErrors);
				this.FailuresDueToMaxLocalLoopCount = new ExPerformanceCounter(base.CategoryName, "Failures Due to Maximum Local Loop Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToMaxLocalLoopCount);
				this.LoopingMessagesLastHour = new ExPerformanceCounter(base.CategoryName, "Looping Messages Last Hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LoopingMessagesLastHour);
				long num = this.TotalConnections.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x00107BF0 File Offset: 0x00105DF0
		internal SmtpAvailabilityPerfCountersInstance(string instanceName) : base(instanceName, SmtpAvailabilityPerfCounters.CategoryName)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.TotalConnections = new ExPerformanceCounter(base.CategoryName, "Total Connections", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalConnections);
				this.AvailabilityPercentage = new ExPerformanceCounter(base.CategoryName, "% Availability", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AvailabilityPercentage);
				this.ActivityPercentage = new ExPerformanceCounter(base.CategoryName, "% Activity", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ActivityPercentage);
				this.FailuresDueToMaxInboundConnectionLimit = new ExPerformanceCounter(base.CategoryName, "% Failures Due To MaxInboundConnectionLimit", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToMaxInboundConnectionLimit);
				this.FailuresDueToWLIDDown = new ExPerformanceCounter(base.CategoryName, "% Failures Due To WLID Down", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToWLIDDown);
				this.FailuresDueToADDown = new ExPerformanceCounter(base.CategoryName, "% Failures Due To Active Directory Down", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToADDown);
				this.FailuresDueToBackPressure = new ExPerformanceCounter(base.CategoryName, "% Failures Due To Back Pressure", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToBackPressure);
				this.FailuresDueToIOExceptions = new ExPerformanceCounter(base.CategoryName, "% Failures Due To IO Exceptions", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToIOExceptions);
				this.FailuresDueToTLSErrors = new ExPerformanceCounter(base.CategoryName, "% Failures Due To TLS Errors", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToTLSErrors);
				this.FailuresDueToMaxLocalLoopCount = new ExPerformanceCounter(base.CategoryName, "Failures Due to Maximum Local Loop Count", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailuresDueToMaxLocalLoopCount);
				this.LoopingMessagesLastHour = new ExPerformanceCounter(base.CategoryName, "Looping Messages Last Hour", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LoopingMessagesLastHour);
				long num = this.TotalConnections.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00107E74 File Offset: 0x00106074
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x0400225A RID: 8794
		public readonly ExPerformanceCounter TotalConnections;

		// Token: 0x0400225B RID: 8795
		public readonly ExPerformanceCounter AvailabilityPercentage;

		// Token: 0x0400225C RID: 8796
		public readonly ExPerformanceCounter ActivityPercentage;

		// Token: 0x0400225D RID: 8797
		public readonly ExPerformanceCounter FailuresDueToMaxInboundConnectionLimit;

		// Token: 0x0400225E RID: 8798
		public readonly ExPerformanceCounter FailuresDueToWLIDDown;

		// Token: 0x0400225F RID: 8799
		public readonly ExPerformanceCounter FailuresDueToADDown;

		// Token: 0x04002260 RID: 8800
		public readonly ExPerformanceCounter FailuresDueToBackPressure;

		// Token: 0x04002261 RID: 8801
		public readonly ExPerformanceCounter FailuresDueToIOExceptions;

		// Token: 0x04002262 RID: 8802
		public readonly ExPerformanceCounter FailuresDueToTLSErrors;

		// Token: 0x04002263 RID: 8803
		public readonly ExPerformanceCounter FailuresDueToMaxLocalLoopCount;

		// Token: 0x04002264 RID: 8804
		public readonly ExPerformanceCounter LoopingMessagesLastHour;
	}
}
