using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200036E RID: 878
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetServerTimeZonesType : BaseRequestType
	{
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00029BBC File Offset: 0x00027DBC
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00029BC4 File Offset: 0x00027DC4
		[XmlArrayItem("Id", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Ids
		{
			get
			{
				return this.idsField;
			}
			set
			{
				this.idsField = value;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00029BCD File Offset: 0x00027DCD
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x00029BD5 File Offset: 0x00027DD5
		[XmlAttribute]
		public bool ReturnFullTimeZoneData
		{
			get
			{
				return this.returnFullTimeZoneDataField;
			}
			set
			{
				this.returnFullTimeZoneDataField = value;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00029BDE File Offset: 0x00027DDE
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x00029BE6 File Offset: 0x00027DE6
		[XmlIgnore]
		public bool ReturnFullTimeZoneDataSpecified
		{
			get
			{
				return this.returnFullTimeZoneDataFieldSpecified;
			}
			set
			{
				this.returnFullTimeZoneDataFieldSpecified = value;
			}
		}

		// Token: 0x04001296 RID: 4758
		private string[] idsField;

		// Token: 0x04001297 RID: 4759
		private bool returnFullTimeZoneDataField;

		// Token: 0x04001298 RID: 4760
		private bool returnFullTimeZoneDataFieldSpecified;
	}
}
