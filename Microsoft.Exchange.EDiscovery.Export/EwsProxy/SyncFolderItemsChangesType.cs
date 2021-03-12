using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001FB RID: 507
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SyncFolderItemsChangesType
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00025BAD File Offset: 0x00023DAD
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x00025BB5 File Offset: 0x00023DB5
		[XmlElement("Create", typeof(SyncFolderItemsCreateOrUpdateType))]
		[XmlElement("Delete", typeof(SyncFolderItemsDeleteType))]
		[XmlElement("ReadFlagChange", typeof(SyncFolderItemsReadFlagType))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("Update", typeof(SyncFolderItemsCreateOrUpdateType))]
		public object[] Items
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

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00025BBE File Offset: 0x00023DBE
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x00025BC6 File Offset: 0x00023DC6
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ItemsChoiceType2[] ItemsElementName
		{
			get
			{
				return this.itemsElementNameField;
			}
			set
			{
				this.itemsElementNameField = value;
			}
		}

		// Token: 0x04000E0B RID: 3595
		private object[] itemsField;

		// Token: 0x04000E0C RID: 3596
		private ItemsChoiceType2[] itemsElementNameField;
	}
}
