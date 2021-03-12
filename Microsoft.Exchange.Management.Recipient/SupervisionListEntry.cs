using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public sealed class SupervisionListEntry : ConfigurableObject
	{
		// Token: 0x06001361 RID: 4961 RVA: 0x00047BCC File Offset: 0x00045DCC
		public SupervisionListEntry(string entryName, string tag, SupervisionRecipientType recipientType) : base(new SupervisionListEntryPropertyBag())
		{
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			this.EntryName = entryName;
			this.Tag = tag;
			this.RecipientType = recipientType;
			this.Identity = new SupervisionListEntryId(this);
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x00047C21 File Offset: 0x00045E21
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return SupervisionListEntry.schema;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x00047C28 File Offset: 0x00045E28
		private new bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x00047C2B File Offset: 0x00045E2B
		// (set) Token: 0x06001365 RID: 4965 RVA: 0x00047C42 File Offset: 0x00045E42
		public new ObjectId Identity
		{
			get
			{
				return (ObjectId)this.propertyBag[SupervisionListEntrySchema.Identity];
			}
			internal set
			{
				this.propertyBag[SupervisionListEntrySchema.Identity] = value;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x00047C55 File Offset: 0x00045E55
		// (set) Token: 0x06001367 RID: 4967 RVA: 0x00047C6C File Offset: 0x00045E6C
		public string EntryName
		{
			get
			{
				return (string)this.propertyBag[SupervisionListEntrySchema.EntryName];
			}
			internal set
			{
				this.propertyBag[SupervisionListEntrySchema.EntryName] = value;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x00047C7F File Offset: 0x00045E7F
		// (set) Token: 0x06001369 RID: 4969 RVA: 0x00047C96 File Offset: 0x00045E96
		public string Tag
		{
			get
			{
				return (string)this.propertyBag[SupervisionListEntrySchema.Tag];
			}
			internal set
			{
				this.propertyBag[SupervisionListEntrySchema.Tag] = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00047CA9 File Offset: 0x00045EA9
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x00047CC0 File Offset: 0x00045EC0
		public SupervisionRecipientType RecipientType
		{
			get
			{
				return (SupervisionRecipientType)this.propertyBag[SupervisionListEntrySchema.RecipientType];
			}
			internal set
			{
				this.propertyBag[SupervisionListEntrySchema.RecipientType] = value;
			}
		}

		// Token: 0x040003D0 RID: 976
		private static SupervisionListEntrySchema schema = ObjectSchema.GetInstance<SupervisionListEntrySchema>();
	}
}
