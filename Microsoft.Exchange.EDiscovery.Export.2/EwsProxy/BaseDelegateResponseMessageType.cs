using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001E8 RID: 488
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlInclude(typeof(GetDelegateResponseMessageType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(UpdateDelegateResponseMessageType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(AddDelegateResponseMessageType))]
	[XmlInclude(typeof(RemoveDelegateResponseMessageType))]
	[Serializable]
	public abstract class BaseDelegateResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x0002588D File Offset: 0x00023A8D
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x00025895 File Offset: 0x00023A95
		[XmlArrayItem(IsNullable = false)]
		public DelegateUserResponseMessageType[] ResponseMessages
		{
			get
			{
				return this.responseMessagesField;
			}
			set
			{
				this.responseMessagesField = value;
			}
		}

		// Token: 0x04000DCE RID: 3534
		private DelegateUserResponseMessageType[] responseMessagesField;
	}
}
