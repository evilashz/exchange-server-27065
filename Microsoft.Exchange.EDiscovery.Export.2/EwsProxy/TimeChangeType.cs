using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000147 RID: 327
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TimeChangeType
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00022829 File Offset: 0x00020A29
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x00022831 File Offset: 0x00020A31
		[XmlElement(DataType = "duration")]
		public string Offset
		{
			get
			{
				return this.offsetField;
			}
			set
			{
				this.offsetField = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0002283A File Offset: 0x00020A3A
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00022842 File Offset: 0x00020A42
		[XmlElement("RelativeYearlyRecurrence", typeof(RelativeYearlyRecurrencePatternType))]
		[XmlElement("AbsoluteDate", typeof(DateTime), DataType = "date")]
		public object Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0002284B File Offset: 0x00020A4B
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x00022853 File Offset: 0x00020A53
		[XmlElement(DataType = "time")]
		public DateTime Time
		{
			get
			{
				return this.timeField;
			}
			set
			{
				this.timeField = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0002285C File Offset: 0x00020A5C
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x00022864 File Offset: 0x00020A64
		[XmlAttribute]
		public string TimeZoneName
		{
			get
			{
				return this.timeZoneNameField;
			}
			set
			{
				this.timeZoneNameField = value;
			}
		}

		// Token: 0x040009D6 RID: 2518
		private string offsetField;

		// Token: 0x040009D7 RID: 2519
		private object itemField;

		// Token: 0x040009D8 RID: 2520
		private DateTime timeField;

		// Token: 0x040009D9 RID: 2521
		private string timeZoneNameField;
	}
}
