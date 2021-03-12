using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000EA RID: 234
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class PropagationTaskValue
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001FB8E File Offset: 0x0001DD8E
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001FB96 File Offset: 0x0001DD96
		public XmlElement Parameter
		{
			get
			{
				return this.parameterField;
			}
			set
			{
				this.parameterField = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001FB9F File Offset: 0x0001DD9F
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001FBA7 File Offset: 0x0001DDA7
		[XmlAttribute]
		public string TaskId
		{
			get
			{
				return this.taskIdField;
			}
			set
			{
				this.taskIdField = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001FBB0 File Offset: 0x0001DDB0
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0001FBB8 File Offset: 0x0001DDB8
		[XmlAttribute]
		public string TaskType
		{
			get
			{
				return this.taskTypeField;
			}
			set
			{
				this.taskTypeField = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001FBC1 File Offset: 0x0001DDC1
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0001FBC9 File Offset: 0x0001DDC9
		[XmlAttribute]
		public int Priority
		{
			get
			{
				return this.priorityField;
			}
			set
			{
				this.priorityField = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001FBD2 File Offset: 0x0001DDD2
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0001FBDA File Offset: 0x0001DDDA
		[XmlAttribute]
		public DateTime CreationTime
		{
			get
			{
				return this.creationTimeField;
			}
			set
			{
				this.creationTimeField = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001FBE3 File Offset: 0x0001DDE3
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0001FBEB File Offset: 0x0001DDEB
		[XmlAttribute]
		public PropagationTaskStatus Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001FBF4 File Offset: 0x0001DDF4
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0001FBFC File Offset: 0x0001DDFC
		[XmlAttribute]
		public int RetryCount
		{
			get
			{
				return this.retryCountField;
			}
			set
			{
				this.retryCountField = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001FC05 File Offset: 0x0001DE05
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0001FC0D File Offset: 0x0001DE0D
		[XmlAttribute]
		public DateTime EarliestStartTime
		{
			get
			{
				return this.earliestStartTimeField;
			}
			set
			{
				this.earliestStartTimeField = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001FC16 File Offset: 0x0001DE16
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0001FC1E File Offset: 0x0001DE1E
		[XmlAttribute(DataType = "duration")]
		public string AccumulatedExecutionTime
		{
			get
			{
				return this.accumulatedExecutionTimeField;
			}
			set
			{
				this.accumulatedExecutionTimeField = value;
			}
		}

		// Token: 0x040003C0 RID: 960
		private XmlElement parameterField;

		// Token: 0x040003C1 RID: 961
		private string taskIdField;

		// Token: 0x040003C2 RID: 962
		private string taskTypeField;

		// Token: 0x040003C3 RID: 963
		private int priorityField;

		// Token: 0x040003C4 RID: 964
		private DateTime creationTimeField;

		// Token: 0x040003C5 RID: 965
		private PropagationTaskStatus statusField;

		// Token: 0x040003C6 RID: 966
		private int retryCountField;

		// Token: 0x040003C7 RID: 967
		private DateTime earliestStartTimeField;

		// Token: 0x040003C8 RID: 968
		private string accumulatedExecutionTimeField;
	}
}
