using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000311 RID: 785
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class SaveUMPinType : BaseRequestType
	{
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x000289B1 File Offset: 0x00026BB1
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x000289B9 File Offset: 0x00026BB9
		public PinInfoType PinInfo
		{
			get
			{
				return this.pinInfoField;
			}
			set
			{
				this.pinInfoField = value;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x000289C2 File Offset: 0x00026BC2
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x000289CA File Offset: 0x00026BCA
		public string UserUMMailboxPolicyGuid
		{
			get
			{
				return this.userUMMailboxPolicyGuidField;
			}
			set
			{
				this.userUMMailboxPolicyGuidField = value;
			}
		}

		// Token: 0x0400115F RID: 4447
		private PinInfoType pinInfoField;

		// Token: 0x04001160 RID: 4448
		private string userUMMailboxPolicyGuidField;
	}
}
