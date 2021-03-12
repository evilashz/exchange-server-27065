using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000C7 RID: 199
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class XmlValueThrottleLimit
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0001F2F4 File Offset: 0x0001D4F4
		public ThrottleLimitValue ThrottleLimit
		{
			get
			{
				return this.throttleLimitField;
			}
			set
			{
				this.throttleLimitField = value;
			}
		}

		// Token: 0x04000348 RID: 840
		private ThrottleLimitValue throttleLimitField;
	}
}
