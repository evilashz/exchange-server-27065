using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200027B RID: 635
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class SerializableTimeZone
	{
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x00027620 File Offset: 0x00025820
		// (set) Token: 0x06001781 RID: 6017 RVA: 0x00027628 File Offset: 0x00025828
		public int Bias
		{
			get
			{
				return this.biasField;
			}
			set
			{
				this.biasField = value;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x00027631 File Offset: 0x00025831
		// (set) Token: 0x06001783 RID: 6019 RVA: 0x00027639 File Offset: 0x00025839
		public SerializableTimeZoneTime StandardTime
		{
			get
			{
				return this.standardTimeField;
			}
			set
			{
				this.standardTimeField = value;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00027642 File Offset: 0x00025842
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x0002764A File Offset: 0x0002584A
		public SerializableTimeZoneTime DaylightTime
		{
			get
			{
				return this.daylightTimeField;
			}
			set
			{
				this.daylightTimeField = value;
			}
		}

		// Token: 0x04000FDD RID: 4061
		private int biasField;

		// Token: 0x04000FDE RID: 4062
		private SerializableTimeZoneTime standardTimeField;

		// Token: 0x04000FDF RID: 4063
		private SerializableTimeZoneTime daylightTimeField;
	}
}
