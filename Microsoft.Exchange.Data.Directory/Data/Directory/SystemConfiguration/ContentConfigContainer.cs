using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003CB RID: 971
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ContentConfigContainer : ADConfigurationObject
	{
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000B64CF File Offset: 0x000B46CF
		// (set) Token: 0x06002C46 RID: 11334 RVA: 0x000B64E1 File Offset: 0x000B46E1
		public MultiValuedProperty<string> MimeTypes
		{
			get
			{
				return (MultiValuedProperty<string>)base[ContentConfigContainerSchema.MimeTypes];
			}
			internal set
			{
				base[ContentConfigContainerSchema.MimeTypes] = value;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000B64EF File Offset: 0x000B46EF
		internal override ADObjectSchema Schema
		{
			get
			{
				return ContentConfigContainer.schema;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06002C48 RID: 11336 RVA: 0x000B64F6 File Offset: 0x000B46F6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchContentConfigContainer";
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x000B64FD File Offset: 0x000B46FD
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001A7E RID: 6782
		public const string DefaultName = "Internet Message Formats";

		// Token: 0x04001A7F RID: 6783
		private const string MostDerivedClass = "msExchContentConfigContainer";

		// Token: 0x04001A80 RID: 6784
		private static readonly ADObjectSchema schema = ObjectSchema.GetInstance<ContentConfigContainerSchema>();
	}
}
