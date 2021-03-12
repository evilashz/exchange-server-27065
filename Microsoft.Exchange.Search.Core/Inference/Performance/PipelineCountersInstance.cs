using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Performance
{
	// Token: 0x020000C9 RID: 201
	internal sealed class PipelineCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000622 RID: 1570 RVA: 0x0001325C File Offset: 0x0001145C
		internal PipelineCountersInstance(string instanceName, PipelineCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeInference Pipeline")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NumberOfOutstandingDocuments = new ExPerformanceCounter(base.CategoryName, "Number of Outstanding Documents", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOutstandingDocuments);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Document Incoming Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.NumberOfIncomingDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Incoming Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.NumberOfIncomingDocuments);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Document Reject Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.NumberOfRejectedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Rejected Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.NumberOfRejectedDocuments);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Document Processing Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.NumberOfProcessedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Processed Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.NumberOfProcessedDocuments);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Document Success Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.NumberOfSucceededDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Succeeded Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.NumberOfSucceededDocuments);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Document Failure Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.NumberOfFailedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Failed Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.NumberOfFailedDocuments);
				this.AverageDocumentProcessingTime = new ExPerformanceCounter(base.CategoryName, "Average Document Processing Time In Seconds", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDocumentProcessingTime);
				this.AverageDocumentProcessingTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Document Processing Time Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDocumentProcessingTimeBase);
				this.NumberOfComponentsPoisoned = new ExPerformanceCounter(base.CategoryName, "Number Of Components Poisoned", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfComponentsPoisoned);
				long num = this.NumberOfOutstandingDocuments.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter6 in list)
					{
						exPerformanceCounter6.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001356C File Offset: 0x0001176C
		internal PipelineCountersInstance(string instanceName) : base(instanceName, "MSExchangeInference Pipeline")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NumberOfOutstandingDocuments = new ExPerformanceCounter(base.CategoryName, "Number of Outstanding Documents", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOutstandingDocuments);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Document Incoming Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.NumberOfIncomingDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Incoming Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.NumberOfIncomingDocuments);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Document Reject Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.NumberOfRejectedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Rejected Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.NumberOfRejectedDocuments);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Document Processing Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.NumberOfProcessedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Processed Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.NumberOfProcessedDocuments);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Document Success Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.NumberOfSucceededDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Succeeded Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.NumberOfSucceededDocuments);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Document Failure Rate Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.NumberOfFailedDocuments = new ExPerformanceCounter(base.CategoryName, "Number Of Failed Documents", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.NumberOfFailedDocuments);
				this.AverageDocumentProcessingTime = new ExPerformanceCounter(base.CategoryName, "Average Document Processing Time In Seconds", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDocumentProcessingTime);
				this.AverageDocumentProcessingTimeBase = new ExPerformanceCounter(base.CategoryName, "Average Document Processing Time Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageDocumentProcessingTimeBase);
				this.NumberOfComponentsPoisoned = new ExPerformanceCounter(base.CategoryName, "Number Of Components Poisoned", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfComponentsPoisoned);
				long num = this.NumberOfOutstandingDocuments.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter6 in list)
					{
						exPerformanceCounter6.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001387C File Offset: 0x00011A7C
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

		// Token: 0x040002D2 RID: 722
		public readonly ExPerformanceCounter NumberOfOutstandingDocuments;

		// Token: 0x040002D3 RID: 723
		public readonly ExPerformanceCounter NumberOfIncomingDocuments;

		// Token: 0x040002D4 RID: 724
		public readonly ExPerformanceCounter NumberOfRejectedDocuments;

		// Token: 0x040002D5 RID: 725
		public readonly ExPerformanceCounter NumberOfProcessedDocuments;

		// Token: 0x040002D6 RID: 726
		public readonly ExPerformanceCounter NumberOfSucceededDocuments;

		// Token: 0x040002D7 RID: 727
		public readonly ExPerformanceCounter NumberOfFailedDocuments;

		// Token: 0x040002D8 RID: 728
		public readonly ExPerformanceCounter AverageDocumentProcessingTime;

		// Token: 0x040002D9 RID: 729
		public readonly ExPerformanceCounter AverageDocumentProcessingTimeBase;

		// Token: 0x040002DA RID: 730
		public readonly ExPerformanceCounter NumberOfComponentsPoisoned;
	}
}
