using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000231 RID: 561
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MovedCopiedEventType : BaseObjectChangedEventType
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0002648E File Offset: 0x0002468E
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x00026496 File Offset: 0x00024696
		[XmlElement("OldFolderId", typeof(FolderIdType))]
		[XmlElement("OldItemId", typeof(ItemIdType))]
		public object Item1
		{
			get
			{
				return this.item1Field;
			}
			set
			{
				this.item1Field = value;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0002649F File Offset: 0x0002469F
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x000264A7 File Offset: 0x000246A7
		public FolderIdType OldParentFolderId
		{
			get
			{
				return this.oldParentFolderIdField;
			}
			set
			{
				this.oldParentFolderIdField = value;
			}
		}

		// Token: 0x04000EBB RID: 3771
		private object item1Field;

		// Token: 0x04000EBC RID: 3772
		private FolderIdType oldParentFolderIdField;
	}
}
