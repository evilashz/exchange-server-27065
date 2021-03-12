using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A80 RID: 2688
	[Serializable]
	public sealed class MessageCategory : XsoMailboxConfigurationObject
	{
		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x0600625F RID: 25183 RVA: 0x0019FAC4 File Offset: 0x0019DCC4
		public override ObjectId Identity
		{
			get
			{
				return (ObjectId)this[MessageCategorySchema.Identity];
			}
		}

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x06006260 RID: 25184 RVA: 0x0019FAD6 File Offset: 0x0019DCD6
		// (set) Token: 0x06006261 RID: 25185 RVA: 0x0019FAE8 File Offset: 0x0019DCE8
		public string Name
		{
			get
			{
				return (string)this[MessageCategorySchema.Name];
			}
			set
			{
				this[MessageCategorySchema.Name] = value;
			}
		}

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x06006262 RID: 25186 RVA: 0x0019FAF6 File Offset: 0x0019DCF6
		// (set) Token: 0x06006263 RID: 25187 RVA: 0x0019FB08 File Offset: 0x0019DD08
		public int Color
		{
			get
			{
				return (int)this[MessageCategorySchema.Color];
			}
			set
			{
				this[MessageCategorySchema.Color] = value;
			}
		}

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x06006264 RID: 25188 RVA: 0x0019FB1B File Offset: 0x0019DD1B
		// (set) Token: 0x06006265 RID: 25189 RVA: 0x0019FB2D File Offset: 0x0019DD2D
		public Guid Guid
		{
			get
			{
				return (Guid)this[MessageCategorySchema.Guid];
			}
			set
			{
				this[MessageCategorySchema.Guid] = value;
			}
		}

		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x06006266 RID: 25190 RVA: 0x0019FB40 File Offset: 0x0019DD40
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return MessageCategory.schema;
			}
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x0019FB47 File Offset: 0x0019DD47
		public MessageCategory()
		{
			((SimplePropertyBag)this.propertyBag).SetObjectIdentityPropertyDefinition(MessageCategorySchema.Identity);
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x0019FB64 File Offset: 0x0019DD64
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return base.ToString();
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x0019FB94 File Offset: 0x0019DD94
		internal static object IdentityGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[XsoMailboxConfigurationObjectSchema.MailboxOwnerId];
			Guid value = (Guid)propertyBag[MessageCategorySchema.Guid];
			string name = (string)propertyBag[MessageCategorySchema.Name];
			if (adobjectId != null)
			{
				return new MessageCategoryId(adobjectId, name, new Guid?(value));
			}
			return null;
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x0019FBE6 File Offset: 0x0019DDE6
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
		}

		// Token: 0x040037C3 RID: 14275
		private static MessageCategorySchema schema = ObjectSchema.GetInstance<MessageCategorySchema>();
	}
}
