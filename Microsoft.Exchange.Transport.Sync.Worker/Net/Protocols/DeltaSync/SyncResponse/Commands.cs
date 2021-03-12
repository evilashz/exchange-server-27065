using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001BC RID: 444
	[XmlType(TypeName = "Commands", Namespace = "AirSync:")]
	[Serializable]
	public class Commands
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0001EAEB File Offset: 0x0001CCEB
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x0001EB06 File Offset: 0x0001CD06
		[XmlIgnore]
		public DeleteCollection DeleteCollection
		{
			get
			{
				if (this.internalDeleteCollection == null)
				{
					this.internalDeleteCollection = new DeleteCollection();
				}
				return this.internalDeleteCollection;
			}
			set
			{
				this.internalDeleteCollection = value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0001EB0F File Offset: 0x0001CD0F
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x0001EB2A File Offset: 0x0001CD2A
		[XmlIgnore]
		public ChangeCollection ChangeCollection
		{
			get
			{
				if (this.internalChangeCollection == null)
				{
					this.internalChangeCollection = new ChangeCollection();
				}
				return this.internalChangeCollection;
			}
			set
			{
				this.internalChangeCollection = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0001EB33 File Offset: 0x0001CD33
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x0001EB4E File Offset: 0x0001CD4E
		[XmlIgnore]
		public AddCollection AddCollection
		{
			get
			{
				if (this.internalAddCollection == null)
				{
					this.internalAddCollection = new AddCollection();
				}
				return this.internalAddCollection;
			}
			set
			{
				this.internalAddCollection = value;
			}
		}

		// Token: 0x040006F7 RID: 1783
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Delete), ElementName = "Delete", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		public DeleteCollection internalDeleteCollection;

		// Token: 0x040006F8 RID: 1784
		[XmlElement(Type = typeof(Change), ElementName = "Change", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ChangeCollection internalChangeCollection;

		// Token: 0x040006F9 RID: 1785
		[XmlElement(Type = typeof(Add), ElementName = "Add", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AddCollection internalAddCollection;
	}
}
