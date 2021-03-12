using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000334 RID: 820
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetClientExtensionType : BaseRequestType
	{
		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x00028EE9 File Offset: 0x000270E9
		// (set) Token: 0x06001A75 RID: 6773 RVA: 0x00028EF1 File Offset: 0x000270F1
		[XmlArrayItem("Action", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public SetClientExtensionActionType[] Actions
		{
			get
			{
				return this.actionsField;
			}
			set
			{
				this.actionsField = value;
			}
		}

		// Token: 0x040011B5 RID: 4533
		private SetClientExtensionActionType[] actionsField;
	}
}
