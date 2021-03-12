using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000C5 RID: 197
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueAuthorizedParty
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001F2A9 File Offset: 0x0001D4A9
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0001F2B1 File Offset: 0x0001D4B1
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

		// Token: 0x04000345 RID: 837
		private AuthorizedPartyValue authorizedPartyField;
	}
}
