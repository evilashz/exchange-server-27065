using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000934 RID: 2356
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueAuthorizedParty
	{
		// Token: 0x170027B7 RID: 10167
		// (get) Token: 0x06006FB9 RID: 28601 RVA: 0x00176E43 File Offset: 0x00175043
		// (set) Token: 0x06006FBA RID: 28602 RVA: 0x00176E4B File Offset: 0x0017504B
		[XmlElement(Order = 0)]
		public AuthorizedPartyValue AuthorizedParty
		{
			get
			{
				return this.authorizedPartyField;
			}
			set
			{
				this.authorizedPartyField = value;
			}
		}

		// Token: 0x04004889 RID: 18569
		private AuthorizedPartyValue authorizedPartyField;
	}
}
