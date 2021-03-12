using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200017F RID: 383
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class EmailUserType
	{
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00023BF0 File Offset: 0x00021DF0
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x00023BF8 File Offset: 0x00021DF8
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

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00023C01 File Offset: 0x00021E01
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x00023C09 File Offset: 0x00021E09
		public string UserId
		{
			get
			{
				return this.userIdField;
			}
			set
			{
				this.userIdField = value;
			}
		}

		// Token: 0x04000B56 RID: 2902
		private string nameField;

		// Token: 0x04000B57 RID: 2903
		private string userIdField;
	}
}
