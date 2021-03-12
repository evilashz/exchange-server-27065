using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000185 RID: 389
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class EmailAddressEntityType : EntityType
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00023DB8 File Offset: 0x00021FB8
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x00023DC0 File Offset: 0x00021FC0
		public string EmailAddress
		{
			get
			{
				return this.emailAddressField;
			}
			set
			{
				this.emailAddressField = value;
			}
		}

		// Token: 0x04000B6E RID: 2926
		private string emailAddressField;
	}
}
