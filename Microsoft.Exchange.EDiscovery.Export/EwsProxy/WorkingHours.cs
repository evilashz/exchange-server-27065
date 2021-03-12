using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200027A RID: 634
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class WorkingHours
	{
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x000275F6 File Offset: 0x000257F6
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x000275FE File Offset: 0x000257FE
		public SerializableTimeZone TimeZone
		{
			get
			{
				return this.timeZoneField;
			}
			set
			{
				this.timeZoneField = value;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x00027607 File Offset: 0x00025807
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x0002760F File Offset: 0x0002580F
		[XmlArrayItem(IsNullable = false)]
		public WorkingPeriod[] WorkingPeriodArray
		{
			get
			{
				return this.workingPeriodArrayField;
			}
			set
			{
				this.workingPeriodArrayField = value;
			}
		}

		// Token: 0x04000FDB RID: 4059
		private SerializableTimeZone timeZoneField;

		// Token: 0x04000FDC RID: 4060
		private WorkingPeriod[] workingPeriodArrayField;
	}
}
