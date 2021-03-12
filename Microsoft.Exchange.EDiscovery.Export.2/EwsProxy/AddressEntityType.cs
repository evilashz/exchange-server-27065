using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200017C RID: 380
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class AddressEntityType : EntityType
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00023BAD File Offset: 0x00021DAD
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x00023BB5 File Offset: 0x00021DB5
		public string Address
		{
			get
			{
				return this.addressField;
			}
			set
			{
				this.addressField = value;
			}
		}

		// Token: 0x04000B4E RID: 2894
		private string addressField;
	}
}
