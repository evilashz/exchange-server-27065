using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000239 RID: 569
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class ArrayOfTimeZoneDefinitionType
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0002658A File Offset: 0x0002478A
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x00026592 File Offset: 0x00024792
		[XmlElement("TimeZoneDefinition")]
		public TimeZoneDefinitionType[] TimeZoneDefinition
		{
			get
			{
				return this.timeZoneDefinitionField;
			}
			set
			{
				this.timeZoneDefinitionField = value;
			}
		}

		// Token: 0x04000ED3 RID: 3795
		private TimeZoneDefinitionType[] timeZoneDefinitionField;
	}
}
