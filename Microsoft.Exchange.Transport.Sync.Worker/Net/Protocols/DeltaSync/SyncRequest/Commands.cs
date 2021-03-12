using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001AD RID: 429
	[XmlType(TypeName = "Commands", Namespace = "AirSync:")]
	[Serializable]
	public class Commands
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0001E603 File Offset: 0x0001C803
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

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0001E60C File Offset: 0x0001C80C
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0001E627 File Offset: 0x0001C827
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

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0001E630 File Offset: 0x0001C830
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0001E64B File Offset: 0x0001C84B
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

		// Token: 0x040006E0 RID: 1760
		[XmlElement(Type = typeof(Change), ElementName = "Change", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ChangeCollection internalChangeCollection;

		// Token: 0x040006E1 RID: 1761
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Delete), ElementName = "Delete", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		public DeleteCollection internalDeleteCollection;

		// Token: 0x040006E2 RID: 1762
		[XmlElement(Type = typeof(Add), ElementName = "Add", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AddCollection internalAddCollection;
	}
}
