using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000304 RID: 772
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ContactsViewType : BasePagingType
	{
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x000287F4 File Offset: 0x000269F4
		// (set) Token: 0x060019A1 RID: 6561 RVA: 0x000287FC File Offset: 0x000269FC
		[XmlAttribute]
		public string InitialName
		{
			get
			{
				return this.initialNameField;
			}
			set
			{
				this.initialNameField = value;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00028805 File Offset: 0x00026A05
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x0002880D File Offset: 0x00026A0D
		[XmlAttribute]
		public string FinalName
		{
			get
			{
				return this.finalNameField;
			}
			set
			{
				this.finalNameField = value;
			}
		}

		// Token: 0x04001143 RID: 4419
		private string initialNameField;

		// Token: 0x04001144 RID: 4420
		private string finalNameField;
	}
}
