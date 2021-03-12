using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000469 RID: 1129
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public sealed class ExtendedRight : ADNonExchangeObject
	{
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x000CB891 File Offset: 0x000C9A91
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExtendedRight.schema;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000CB898 File Offset: 0x000C9A98
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExtendedRight.mostDerivedClass;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x000CB89F File Offset: 0x000C9A9F
		// (set) Token: 0x06003249 RID: 12873 RVA: 0x000CB8B1 File Offset: 0x000C9AB1
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)this[ExtendedRightSchema.DisplayName];
			}
			set
			{
				this[ExtendedRightSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x000CB8BF File Offset: 0x000C9ABF
		public Guid RightsGuid
		{
			get
			{
				return (Guid)this[ExtendedRightSchema.RightsGuid];
			}
		}

		// Token: 0x0400228E RID: 8846
		private static ExtendedRightSchema schema = ObjectSchema.GetInstance<ExtendedRightSchema>();

		// Token: 0x0400228F RID: 8847
		private static string mostDerivedClass = "controlAccessRight";
	}
}
