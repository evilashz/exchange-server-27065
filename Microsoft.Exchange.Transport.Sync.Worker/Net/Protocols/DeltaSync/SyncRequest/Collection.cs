using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001AB RID: 427
	[XmlType(TypeName = "Collection", Namespace = "AirSync:")]
	[Serializable]
	public class Collection
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0001E545 File Offset: 0x0001C745
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x0001E54D File Offset: 0x0001C74D
		[XmlIgnore]
		public string Class
		{
			get
			{
				return this.internalClass;
			}
			set
			{
				this.internalClass = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0001E556 File Offset: 0x0001C756
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x0001E55E File Offset: 0x0001C75E
		[XmlIgnore]
		public string SyncKey
		{
			get
			{
				return this.internalSyncKey;
			}
			set
			{
				this.internalSyncKey = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0001E567 File Offset: 0x0001C767
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x0001E582 File Offset: 0x0001C782
		[XmlIgnore]
		public GetChanges GetChanges
		{
			get
			{
				if (this.internalGetChanges == null)
				{
					this.internalGetChanges = new GetChanges();
				}
				return this.internalGetChanges;
			}
			set
			{
				this.internalGetChanges = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0001E58B File Offset: 0x0001C78B
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x0001E593 File Offset: 0x0001C793
		[XmlIgnore]
		public int WindowSize
		{
			get
			{
				return this.internalWindowSize;
			}
			set
			{
				this.internalWindowSize = value;
				this.internalWindowSizeSpecified = true;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0001E5A3 File Offset: 0x0001C7A3
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0001E5AB File Offset: 0x0001C7AB
		[XmlIgnore]
		public string CollectionId
		{
			get
			{
				return this.internalCollectionId;
			}
			set
			{
				this.internalCollectionId = value;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0001E5CF File Offset: 0x0001C7CF
		[XmlIgnore]
		public Commands Commands
		{
			get
			{
				if (this.internalCommands == null)
				{
					this.internalCommands = new Commands();
				}
				return this.internalCommands;
			}
			set
			{
				this.internalCommands = value;
			}
		}

		// Token: 0x040006D9 RID: 1753
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Class", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		public string internalClass;

		// Token: 0x040006DA RID: 1754
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "SyncKey", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		public string internalSyncKey;

		// Token: 0x040006DB RID: 1755
		[XmlElement(Type = typeof(GetChanges), ElementName = "GetChanges", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public GetChanges internalGetChanges;

		// Token: 0x040006DC RID: 1756
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "WindowSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "AirSync:")]
		public int internalWindowSize;

		// Token: 0x040006DD RID: 1757
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalWindowSizeSpecified;

		// Token: 0x040006DE RID: 1758
		[XmlElement(ElementName = "CollectionId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalCollectionId;

		// Token: 0x040006DF RID: 1759
		[XmlElement(Type = typeof(Commands), ElementName = "Commands", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Commands internalCommands;
	}
}
