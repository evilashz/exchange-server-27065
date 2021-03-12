using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001D8 RID: 472
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[XmlInclude(typeof(RoomType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryEntryType
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x000256C9 File Offset: 0x000238C9
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x000256D1 File Offset: 0x000238D1
		public EmailAddressType Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x04000DA9 RID: 3497
		private EmailAddressType idField;
	}
}
