using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001B5 RID: 437
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class RulePredicateDateRangeType
	{
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00024DC8 File Offset: 0x00022FC8
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x00024DD0 File Offset: 0x00022FD0
		public DateTime StartDateTime
		{
			get
			{
				return this.startDateTimeField;
			}
			set
			{
				this.startDateTimeField = value;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00024DD9 File Offset: 0x00022FD9
		// (set) Token: 0x060012BA RID: 4794 RVA: 0x00024DE1 File Offset: 0x00022FE1
		[XmlIgnore]
		public bool StartDateTimeSpecified
		{
			get
			{
				return this.startDateTimeFieldSpecified;
			}
			set
			{
				this.startDateTimeFieldSpecified = value;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x00024DEA File Offset: 0x00022FEA
		// (set) Token: 0x060012BC RID: 4796 RVA: 0x00024DF2 File Offset: 0x00022FF2
		public DateTime EndDateTime
		{
			get
			{
				return this.endDateTimeField;
			}
			set
			{
				this.endDateTimeField = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x00024DFB File Offset: 0x00022FFB
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x00024E03 File Offset: 0x00023003
		[XmlIgnore]
		public bool EndDateTimeSpecified
		{
			get
			{
				return this.endDateTimeFieldSpecified;
			}
			set
			{
				this.endDateTimeFieldSpecified = value;
			}
		}

		// Token: 0x04000D1F RID: 3359
		private DateTime startDateTimeField;

		// Token: 0x04000D20 RID: 3360
		private bool startDateTimeFieldSpecified;

		// Token: 0x04000D21 RID: 3361
		private DateTime endDateTimeField;

		// Token: 0x04000D22 RID: 3362
		private bool endDateTimeFieldSpecified;
	}
}
