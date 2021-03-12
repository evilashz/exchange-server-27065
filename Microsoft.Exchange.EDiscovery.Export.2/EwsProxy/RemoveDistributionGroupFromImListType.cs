using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000322 RID: 802
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class RemoveDistributionGroupFromImListType : BaseRequestType
	{
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x00028C30 File Offset: 0x00026E30
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x00028C38 File Offset: 0x00026E38
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

		// Token: 0x0400118F RID: 4495
		private ItemIdType groupIdField;
	}
}
