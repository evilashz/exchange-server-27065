using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001FA RID: 506
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SyncFolderItemsResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00025B61 File Offset: 0x00023D61
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x00025B69 File Offset: 0x00023D69
		public string SyncState
		{
			get
			{
				return this.syncStateField;
			}
			set
			{
				this.syncStateField = value;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00025B72 File Offset: 0x00023D72
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x00025B7A File Offset: 0x00023D7A
		public bool IncludesLastItemInRange
		{
			get
			{
				return this.includesLastItemInRangeField;
			}
			set
			{
				this.includesLastItemInRangeField = value;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00025B83 File Offset: 0x00023D83
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x00025B8B File Offset: 0x00023D8B
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified
		{
			get
			{
				return this.includesLastItemInRangeFieldSpecified;
			}
			set
			{
				this.includesLastItemInRangeFieldSpecified = value;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00025B94 File Offset: 0x00023D94
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x00025B9C File Offset: 0x00023D9C
		public SyncFolderItemsChangesType Changes
		{
			get
			{
				return this.changesField;
			}
			set
			{
				this.changesField = value;
			}
		}

		// Token: 0x04000E07 RID: 3591
		private string syncStateField;

		// Token: 0x04000E08 RID: 3592
		private bool includesLastItemInRangeField;

		// Token: 0x04000E09 RID: 3593
		private bool includesLastItemInRangeFieldSpecified;

		// Token: 0x04000E0A RID: 3594
		private SyncFolderItemsChangesType changesField;
	}
}
