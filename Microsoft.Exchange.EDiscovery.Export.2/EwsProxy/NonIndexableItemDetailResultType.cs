using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200018E RID: 398
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class NonIndexableItemDetailResultType
	{
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x00024090 File Offset: 0x00022290
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x00024098 File Offset: 0x00022298
		[XmlArrayItem("NonIndexableItemDetail", IsNullable = false)]
		public NonIndexableItemDetailType[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x000240A1 File Offset: 0x000222A1
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x000240A9 File Offset: 0x000222A9
		[XmlArrayItem("FailedMailbox", IsNullable = false)]
		public FailedSearchMailboxType[] FailedMailboxes
		{
			get
			{
				return this.failedMailboxesField;
			}
			set
			{
				this.failedMailboxesField = value;
			}
		}

		// Token: 0x04000BD1 RID: 3025
		private NonIndexableItemDetailType[] itemsField;

		// Token: 0x04000BD2 RID: 3026
		private FailedSearchMailboxType[] failedMailboxesField;
	}
}
