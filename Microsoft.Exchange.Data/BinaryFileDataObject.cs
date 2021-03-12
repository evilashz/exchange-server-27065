using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	public class BinaryFileDataObject : ConfigurableObject
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000200DE File Offset: 0x0001E2DE
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x000200F5 File Offset: 0x0001E2F5
		public byte[] FileData
		{
			get
			{
				return (byte[])this.propertyBag[BinaryFileDataObjectSchema.FileData];
			}
			set
			{
				this.propertyBag[BinaryFileDataObjectSchema.FileData] = value;
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00020108 File Offset: 0x0001E308
		public BinaryFileDataObject() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00020115 File Offset: 0x0001E315
		internal void SetIdentity(ObjectId id)
		{
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = id;
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00020129 File Offset: 0x0001E329
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return BinaryFileDataObject.s_schema;
			}
		}

		// Token: 0x04000631 RID: 1585
		private static ObjectSchema s_schema = ObjectSchema.GetInstance<BinaryFileDataObjectSchema>();
	}
}
