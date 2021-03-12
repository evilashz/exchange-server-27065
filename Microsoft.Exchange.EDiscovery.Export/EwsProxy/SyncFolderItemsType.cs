using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000366 RID: 870
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SyncFolderItemsType : BaseRequestType
	{
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x00029A52 File Offset: 0x00027C52
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x00029A5A File Offset: 0x00027C5A
		public ItemResponseShapeType ItemShape
		{
			get
			{
				return this.itemShapeField;
			}
			set
			{
				this.itemShapeField = value;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x00029A63 File Offset: 0x00027C63
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x00029A6B File Offset: 0x00027C6B
		public TargetFolderIdType SyncFolderId
		{
			get
			{
				return this.syncFolderIdField;
			}
			set
			{
				this.syncFolderIdField = value;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00029A74 File Offset: 0x00027C74
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x00029A7C File Offset: 0x00027C7C
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

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00029A85 File Offset: 0x00027C85
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x00029A8D File Offset: 0x00027C8D
		[XmlArrayItem("ItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ItemIdType[] Ignore
		{
			get
			{
				return this.ignoreField;
			}
			set
			{
				this.ignoreField = value;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00029A96 File Offset: 0x00027C96
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x00029A9E File Offset: 0x00027C9E
		public int MaxChangesReturned
		{
			get
			{
				return this.maxChangesReturnedField;
			}
			set
			{
				this.maxChangesReturnedField = value;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00029AA7 File Offset: 0x00027CA7
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x00029AAF File Offset: 0x00027CAF
		public SyncFolderItemsScopeType SyncScope
		{
			get
			{
				return this.syncScopeField;
			}
			set
			{
				this.syncScopeField = value;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00029AB8 File Offset: 0x00027CB8
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x00029AC0 File Offset: 0x00027CC0
		[XmlIgnore]
		public bool SyncScopeSpecified
		{
			get
			{
				return this.syncScopeFieldSpecified;
			}
			set
			{
				this.syncScopeFieldSpecified = value;
			}
		}

		// Token: 0x04001281 RID: 4737
		private ItemResponseShapeType itemShapeField;

		// Token: 0x04001282 RID: 4738
		private TargetFolderIdType syncFolderIdField;

		// Token: 0x04001283 RID: 4739
		private string syncStateField;

		// Token: 0x04001284 RID: 4740
		private ItemIdType[] ignoreField;

		// Token: 0x04001285 RID: 4741
		private int maxChangesReturnedField;

		// Token: 0x04001286 RID: 4742
		private SyncFolderItemsScopeType syncScopeField;

		// Token: 0x04001287 RID: 4743
		private bool syncScopeFieldSpecified;
	}
}
