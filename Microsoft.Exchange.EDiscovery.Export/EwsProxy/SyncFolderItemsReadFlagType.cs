using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001FE RID: 510
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class SyncFolderItemsReadFlagType
	{
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00025C09 File Offset: 0x00023E09
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x00025C11 File Offset: 0x00023E11
		public ItemIdType ItemId
		{
			get
			{
				return this.itemIdField;
			}
			set
			{
				this.itemIdField = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00025C1A File Offset: 0x00023E1A
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x00025C22 File Offset: 0x00023E22
		public bool IsRead
		{
			get
			{
				return this.isReadField;
			}
			set
			{
				this.isReadField = value;
			}
		}

		// Token: 0x04000E0F RID: 3599
		private ItemIdType itemIdField;

		// Token: 0x04000E10 RID: 3600
		private bool isReadField;
	}
}
