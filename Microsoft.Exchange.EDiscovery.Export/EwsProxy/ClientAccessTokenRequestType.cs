using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002D6 RID: 726
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ClientAccessTokenRequestType
	{
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x00027F20 File Offset: 0x00026120
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x00027F28 File Offset: 0x00026128
		public string Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x00027F31 File Offset: 0x00026131
		// (set) Token: 0x06001897 RID: 6295 RVA: 0x00027F39 File Offset: 0x00026139
		public ClientAccessTokenTypeType TokenType
		{
			get
			{
				return this.tokenTypeField;
			}
			set
			{
				this.tokenTypeField = value;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x00027F42 File Offset: 0x00026142
		// (set) Token: 0x06001899 RID: 6297 RVA: 0x00027F4A File Offset: 0x0002614A
		public string Scope
		{
			get
			{
				return this.scopeField;
			}
			set
			{
				this.scopeField = value;
			}
		}

		// Token: 0x040010A6 RID: 4262
		private string idField;

		// Token: 0x040010A7 RID: 4263
		private ClientAccessTokenTypeType tokenTypeField;

		// Token: 0x040010A8 RID: 4264
		private string scopeField;
	}
}
