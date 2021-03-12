using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002CE RID: 718
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EmailAddress
	{
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x00027D9C File Offset: 0x00025F9C
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x00027DA4 File Offset: 0x00025FA4
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x00027DAD File Offset: 0x00025FAD
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x00027DB5 File Offset: 0x00025FB5
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

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x00027DBE File Offset: 0x00025FBE
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x00027DC6 File Offset: 0x00025FC6
		public string RoutingType
		{
			get
			{
				return this.routingTypeField;
			}
			set
			{
				this.routingTypeField = value;
			}
		}

		// Token: 0x04001084 RID: 4228
		private string nameField;

		// Token: 0x04001085 RID: 4229
		private string addressField;

		// Token: 0x04001086 RID: 4230
		private string routingTypeField;
	}
}
