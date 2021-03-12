using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000E6 RID: 230
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueSearchForUsers
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001FABB File Offset: 0x0001DCBB
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001FAC3 File Offset: 0x0001DCC3
		public SearchForUsersValue SearchForUsers
		{
			get
			{
				return this.searchForUsersField;
			}
			set
			{
				this.searchForUsersField = value;
			}
		}

		// Token: 0x040003B1 RID: 945
		private SearchForUsersValue searchForUsersField;
	}
}
