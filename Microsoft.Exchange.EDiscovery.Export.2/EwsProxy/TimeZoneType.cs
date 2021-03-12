using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000146 RID: 326
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class TimeZoneType
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x000227DD File Offset: 0x000209DD
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x000227E5 File Offset: 0x000209E5
		[XmlElement(DataType = "duration")]
		public string BaseOffset
		{
			get
			{
				return this.baseOffsetField;
			}
			set
			{
				this.baseOffsetField = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x000227EE File Offset: 0x000209EE
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x000227F6 File Offset: 0x000209F6
		public TimeChangeType Standard
		{
			get
			{
				return this.standardField;
			}
			set
			{
				this.standardField = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x000227FF File Offset: 0x000209FF
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x00022807 File Offset: 0x00020A07
		public TimeChangeType Daylight
		{
			get
			{
				return this.daylightField;
			}
			set
			{
				this.daylightField = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00022810 File Offset: 0x00020A10
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x00022818 File Offset: 0x00020A18
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

		// Token: 0x040009D2 RID: 2514
		private string baseOffsetField;

		// Token: 0x040009D3 RID: 2515
		private TimeChangeType standardField;

		// Token: 0x040009D4 RID: 2516
		private TimeChangeType daylightField;

		// Token: 0x040009D5 RID: 2517
		private string timeZoneNameField;
	}
}
