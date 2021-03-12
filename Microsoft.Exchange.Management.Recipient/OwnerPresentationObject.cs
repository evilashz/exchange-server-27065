using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	public class OwnerPresentationObject : IConfigurable
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002D752 File Offset: 0x0002B952
		public OwnerPresentationObject(ObjectId identity, string ownerValue)
		{
			this.id = identity;
			this.owner = ownerValue;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0002D768 File Offset: 0x0002B968
		public ObjectId Identity
		{
			get
			{
				ObjectId objectId = this.id;
				if (objectId is ADObjectId && SuppressingPiiContext.NeedPiiSuppression)
				{
					objectId = (ObjectId)SuppressingPiiProperty.TryRedact(ADObjectSchema.Id, objectId);
				}
				return objectId;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0002D7A0 File Offset: 0x0002B9A0
		public string Owner
		{
			get
			{
				string text = this.owner;
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					text = SuppressingPiiData.Redact(text);
				}
				return text;
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002D7C3 File Offset: 0x0002B9C3
		ValidationError[] IConfigurable.Validate()
		{
			return ValidationError.None;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002D7CA File Offset: 0x0002B9CA
		public bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002D7CD File Offset: 0x0002B9CD
		public ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
		void IConfigurable.ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002D7D7 File Offset: 0x0002B9D7
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000245 RID: 581
		private ObjectId id;

		// Token: 0x04000246 RID: 582
		private readonly string owner;
	}
}
