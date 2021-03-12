using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002CD RID: 717
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class FreeBusyViewOptionsType
	{
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x00027D3F File Offset: 0x00025F3F
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x00027D47 File Offset: 0x00025F47
		public Duration TimeWindow
		{
			get
			{
				return this.timeWindowField;
			}
			set
			{
				this.timeWindowField = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x00027D50 File Offset: 0x00025F50
		// (set) Token: 0x0600185E RID: 6238 RVA: 0x00027D58 File Offset: 0x00025F58
		public int MergedFreeBusyIntervalInMinutes
		{
			get
			{
				return this.mergedFreeBusyIntervalInMinutesField;
			}
			set
			{
				this.mergedFreeBusyIntervalInMinutesField = value;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x00027D61 File Offset: 0x00025F61
		// (set) Token: 0x06001860 RID: 6240 RVA: 0x00027D69 File Offset: 0x00025F69
		[XmlIgnore]
		public bool MergedFreeBusyIntervalInMinutesSpecified
		{
			get
			{
				return this.mergedFreeBusyIntervalInMinutesFieldSpecified;
			}
			set
			{
				this.mergedFreeBusyIntervalInMinutesFieldSpecified = value;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x00027D72 File Offset: 0x00025F72
		// (set) Token: 0x06001862 RID: 6242 RVA: 0x00027D7A File Offset: 0x00025F7A
		public FreeBusyViewType RequestedView
		{
			get
			{
				return this.requestedViewField;
			}
			set
			{
				this.requestedViewField = value;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x00027D83 File Offset: 0x00025F83
		// (set) Token: 0x06001864 RID: 6244 RVA: 0x00027D8B File Offset: 0x00025F8B
		[XmlIgnore]
		public bool RequestedViewSpecified
		{
			get
			{
				return this.requestedViewFieldSpecified;
			}
			set
			{
				this.requestedViewFieldSpecified = value;
			}
		}

		// Token: 0x0400107F RID: 4223
		private Duration timeWindowField;

		// Token: 0x04001080 RID: 4224
		private int mergedFreeBusyIntervalInMinutesField;

		// Token: 0x04001081 RID: 4225
		private bool mergedFreeBusyIntervalInMinutesFieldSpecified;

		// Token: 0x04001082 RID: 4226
		private FreeBusyViewType requestedViewField;

		// Token: 0x04001083 RID: 4227
		private bool requestedViewFieldSpecified;
	}
}
