using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000935 RID: 2357
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class AuthorizedPartyValue
	{
		// Token: 0x170027B8 RID: 10168
		// (get) Token: 0x06006FBC RID: 28604 RVA: 0x00176E5C File Offset: 0x0017505C
		// (set) Token: 0x06006FBD RID: 28605 RVA: 0x00176E64 File Offset: 0x00175064
		[XmlAttribute]
		public string ForeignContextId
		{
			get
			{
				return this.foreignContextIdField;
			}
			set
			{
				this.foreignContextIdField = value;
			}
		}

		// Token: 0x170027B9 RID: 10169
		// (get) Token: 0x06006FBE RID: 28606 RVA: 0x00176E6D File Offset: 0x0017506D
		// (set) Token: 0x06006FBF RID: 28607 RVA: 0x00176E75 File Offset: 0x00175075
		[XmlAttribute]
		public string ForeignPrincipalId
		{
			get
			{
				return this.foreignPrincipalIdField;
			}
			set
			{
				this.foreignPrincipalIdField = value;
			}
		}

		// Token: 0x0400488A RID: 18570
		private string foreignContextIdField;

		// Token: 0x0400488B RID: 18571
		private string foreignPrincipalIdField;
	}
}
