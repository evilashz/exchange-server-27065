using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000718 RID: 1816
	[Serializable]
	public class IdentityDetails
	{
		// Token: 0x060055B0 RID: 21936 RVA: 0x0013596D File Offset: 0x00133B6D
		internal IdentityDetails(ADObjectId identity, string name, string displayName, string externalDirectoryObjectId)
		{
			this.Identity = identity;
			this.Name = name;
			this.DisplayName = displayName;
			this.ExternalDirectoryObjectId = externalDirectoryObjectId;
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x00135994 File Offset: 0x00133B94
		internal IdentityDetails(ADRawEntry dataObject)
		{
			this.Identity = dataObject.Id;
			this.Name = (string)dataObject[ADObjectSchema.Name];
			this.DisplayName = (string)dataObject[ADRecipientSchema.DisplayName];
			this.ExternalDirectoryObjectId = (string)dataObject[ADRecipientSchema.ExternalDirectoryObjectId];
		}

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x060055B2 RID: 21938 RVA: 0x001359F5 File Offset: 0x00133BF5
		// (set) Token: 0x060055B3 RID: 21939 RVA: 0x001359FD File Offset: 0x00133BFD
		public ADObjectId Identity { get; private set; }

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x060055B4 RID: 21940 RVA: 0x00135A06 File Offset: 0x00133C06
		// (set) Token: 0x060055B5 RID: 21941 RVA: 0x00135A0E File Offset: 0x00133C0E
		public string Name { get; private set; }

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x060055B6 RID: 21942 RVA: 0x00135A17 File Offset: 0x00133C17
		// (set) Token: 0x060055B7 RID: 21943 RVA: 0x00135A1F File Offset: 0x00133C1F
		public string DisplayName { get; private set; }

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x060055B8 RID: 21944 RVA: 0x00135A28 File Offset: 0x00133C28
		// (set) Token: 0x060055B9 RID: 21945 RVA: 0x00135A30 File Offset: 0x00133C30
		public string ExternalDirectoryObjectId { get; private set; }

		// Token: 0x060055BA RID: 21946 RVA: 0x00135A39 File Offset: 0x00133C39
		public override string ToString()
		{
			return this.Identity.ToString();
		}

		// Token: 0x040039E1 RID: 14817
		internal static readonly ADPropertyDefinition[] Properties = new ADPropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADObjectSchema.Name,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.ExternalDirectoryObjectId
		};
	}
}
