using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000312 RID: 786
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class ValidateUMPinType : BaseRequestType
	{
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x000289DB File Offset: 0x00026BDB
		// (set) Token: 0x060019DB RID: 6619 RVA: 0x000289E3 File Offset: 0x00026BE3
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

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x000289EC File Offset: 0x00026BEC
		// (set) Token: 0x060019DD RID: 6621 RVA: 0x000289F4 File Offset: 0x00026BF4
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

		// Token: 0x04001161 RID: 4449
		private PinInfoType pinInfoField;

		// Token: 0x04001162 RID: 4450
		private string userUMMailboxPolicyGuidField;
	}
}
