using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200017F RID: 383
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class StatelessCollectionCommandsDelete
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0001DA3B File Offset: 0x0001BC3B
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0001DA43 File Offset: 0x0001BC43
		public string ServerId
		{
			get
			{
				return this.serverIdField;
			}
			set
			{
				this.serverIdField = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0001DA4C File Offset: 0x0001BC4C
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x0001DA54 File Offset: 0x0001BC54
		[XmlElement(Namespace = "HMMAIL:")]
		public string SourceFolderId
		{
			get
			{
				return this.sourceFolderIdField;
			}
			set
			{
				this.sourceFolderIdField = value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0001DA5D File Offset: 0x0001BC5D
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x0001DA65 File Offset: 0x0001BC65
		[XmlElement(Namespace = "HMSYNC:")]
		public DeletesAsMoves DeletesAsMoves
		{
			get
			{
				return this.deletesAsMovesField;
			}
			set
			{
				this.deletesAsMovesField = value;
			}
		}

		// Token: 0x04000632 RID: 1586
		private string serverIdField;

		// Token: 0x04000633 RID: 1587
		private string sourceFolderIdField;

		// Token: 0x04000634 RID: 1588
		private DeletesAsMoves deletesAsMovesField;
	}
}
