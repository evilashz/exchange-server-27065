using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000323 RID: 803
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RemoveContactFromImListType : BaseRequestType
	{
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x00028C49 File Offset: 0x00026E49
		// (set) Token: 0x06001A25 RID: 6693 RVA: 0x00028C51 File Offset: 0x00026E51
		public ItemIdType ContactId
		{
			get
			{
				return this.contactIdField;
			}
			set
			{
				this.contactIdField = value;
			}
		}

		// Token: 0x04001190 RID: 4496
		private ItemIdType contactIdField;
	}
}
