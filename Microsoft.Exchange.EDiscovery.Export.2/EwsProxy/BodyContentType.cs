using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000EA RID: 234
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class BodyContentType
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00020DAF File Offset: 0x0001EFAF
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x00020DB7 File Offset: 0x0001EFB7
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00020DC0 File Offset: 0x0001EFC0
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x00020DC8 File Offset: 0x0001EFC8
		public BodyTypeType BodyType
		{
			get
			{
				return this.bodyTypeField;
			}
			set
			{
				this.bodyTypeField = value;
			}
		}

		// Token: 0x040007E8 RID: 2024
		private string valueField;

		// Token: 0x040007E9 RID: 2025
		private BodyTypeType bodyTypeField;
	}
}
