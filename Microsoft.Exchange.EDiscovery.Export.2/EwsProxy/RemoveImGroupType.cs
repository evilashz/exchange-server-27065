using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000321 RID: 801
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class RemoveImGroupType : BaseRequestType
	{
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00028C17 File Offset: 0x00026E17
		// (set) Token: 0x06001A1F RID: 6687 RVA: 0x00028C1F File Offset: 0x00026E1F
		public ItemIdType GroupId
		{
			get
			{
				return this.groupIdField;
			}
			set
			{
				this.groupIdField = value;
			}
		}

		// Token: 0x0400118E RID: 4494
		private ItemIdType groupIdField;
	}
}
