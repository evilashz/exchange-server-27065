using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001BE RID: 446
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ArrayOfTrackingPropertiesType
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x000251C8 File Offset: 0x000233C8
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x000251D0 File Offset: 0x000233D0
		[XmlElement("TrackingPropertyType")]
		public TrackingPropertyType[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x04000D57 RID: 3415
		private TrackingPropertyType[] itemsField;
	}
}
