using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x02000054 RID: 84
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Directory")]
	public class Relation : SchemaObject
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00010175 File Offset: 0x0000E375
		internal Relation(Guid relationTypeId, Guid targetObjectId) : base(relationTypeId)
		{
			this.InitializeRelation(relationTypeId, targetObjectId);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00010186 File Offset: 0x0000E386
		private Relation()
		{
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0001018E File Offset: 0x0000E38E
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00010196 File Offset: 0x0000E396
		[DataMember]
		public SchemaDictionary Properties
		{
			get
			{
				return this.properties;
			}
			private set
			{
				if (this.properties != null)
				{
					this.properties.InternalStorage = value.InternalStorage;
					return;
				}
				this.properties = value;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000101B9 File Offset: 0x0000E3B9
		private void InitializeRelation(Guid relationTypeId, Guid targetObjectId)
		{
			base.TypeId = relationTypeId;
			this.targetObjectId = targetObjectId;
			this.Properties = new SchemaDictionary<Relation>(this, "RelationProperties", null, null, null, null);
		}

		// Token: 0x04000175 RID: 373
		private SchemaDictionary properties;

		// Token: 0x04000176 RID: 374
		[DataMember]
		private Guid targetObjectId;
	}
}
