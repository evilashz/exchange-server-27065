using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001E9 RID: 489
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DelegateUserResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x000258A6 File Offset: 0x00023AA6
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x000258AE File Offset: 0x00023AAE
		public DelegateUserType DelegateUser
		{
			get
			{
				return this.delegateUserField;
			}
			set
			{
				this.delegateUserField = value;
			}
		}

		// Token: 0x04000DCF RID: 3535
		private DelegateUserType delegateUserField;
	}
}
