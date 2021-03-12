using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000096 RID: 150
	public sealed class ReferenceAttachmentSchema : AttachmentSchema
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000073EB File Offset: 0x000055EB
		public TypedPropertyDefinition<string> PathNameProperty
		{
			get
			{
				return ReferenceAttachmentSchema.StaticPathNameProperty;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060003DB RID: 987 RVA: 0x000073F2 File Offset: 0x000055F2
		public TypedPropertyDefinition<string> ProviderEndpointUrlProperty
		{
			get
			{
				return ReferenceAttachmentSchema.StaticProviderEndpointUrlProperty;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000073F9 File Offset: 0x000055F9
		public TypedPropertyDefinition<string> ProviderTypeProperty
		{
			get
			{
				return ReferenceAttachmentSchema.StaticProviderTypeProperty;
			}
		}

		// Token: 0x040001E5 RID: 485
		private static readonly TypedPropertyDefinition<string> StaticPathNameProperty = new TypedPropertyDefinition<string>("ReferenceAttachment.PathName", null, true);

		// Token: 0x040001E6 RID: 486
		private static readonly TypedPropertyDefinition<string> StaticProviderEndpointUrlProperty = new TypedPropertyDefinition<string>("ReferenceAttachment.ProviderEndpointUrl", null, true);

		// Token: 0x040001E7 RID: 487
		private static readonly TypedPropertyDefinition<string> StaticProviderTypeProperty = new TypedPropertyDefinition<string>("ReferenceAttachment.ProviderType", null, true);
	}
}
