using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000FE RID: 254
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class ContextMoveStatusValue
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001FE9C File Offset: 0x0001E09C
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0001FEA4 File Offset: 0x0001E0A4
		[XmlAttribute]
		public ContextMoveStage Stage
		{
			get
			{
				return this.stageField;
			}
			set
			{
				this.stageField = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0001FEAD File Offset: 0x0001E0AD
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0001FEB5 File Offset: 0x0001E0B5
		[XmlAttribute(DataType = "nonNegativeInteger")]
		public string OtherEpoch
		{
			get
			{
				return this.otherEpochField;
			}
			set
			{
				this.otherEpochField = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0001FEBE File Offset: 0x0001E0BE
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0001FEC6 File Offset: 0x0001E0C6
		[XmlAttribute(DataType = "positiveInteger")]
		public string OtherPartitionId
		{
			get
			{
				return this.otherPartitionIdField;
			}
			set
			{
				this.otherPartitionIdField = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001FECF File Offset: 0x0001E0CF
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x0001FED7 File Offset: 0x0001E0D7
		[XmlAttribute]
		public DateTime StartTimestamp
		{
			get
			{
				return this.startTimestampField;
			}
			set
			{
				this.startTimestampField = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		[XmlAttribute]
		public DateTime EndTimestamp
		{
			get
			{
				return this.endTimestampField;
			}
			set
			{
				this.endTimestampField = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001FEF1 File Offset: 0x0001E0F1
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0001FEF9 File Offset: 0x0001E0F9
		[XmlIgnore]
		public bool EndTimestampSpecified
		{
			get
			{
				return this.endTimestampFieldSpecified;
			}
			set
			{
				this.endTimestampFieldSpecified = value;
			}
		}

		// Token: 0x040003F2 RID: 1010
		private ContextMoveStage stageField;

		// Token: 0x040003F3 RID: 1011
		private string otherEpochField;

		// Token: 0x040003F4 RID: 1012
		private string otherPartitionIdField;

		// Token: 0x040003F5 RID: 1013
		private DateTime startTimestampField;

		// Token: 0x040003F6 RID: 1014
		private DateTime endTimestampField;

		// Token: 0x040003F7 RID: 1015
		private bool endTimestampFieldSpecified;
	}
}
