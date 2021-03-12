using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Configuration
{
	// Token: 0x02000006 RID: 6
	internal static class AppConfigSchema
	{
		// Token: 0x0400000A RID: 10
		public const int MaxNameLength = 255;

		// Token: 0x0400000B RID: 11
		public static readonly HygienePropertyDefinition ParamIdProp = new HygienePropertyDefinition("ParamId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400000C RID: 12
		public static readonly HygienePropertyDefinition ParamVersionProp = new HygienePropertyDefinition("ParamVersion", typeof(long), 0L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400000D RID: 13
		public static readonly HygienePropertyDefinition ParamNameProp = new HygienePropertyDefinition("ParamName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400000E RID: 14
		public static readonly HygienePropertyDefinition ParamValueProp = new HygienePropertyDefinition("ParamValue", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400000F RID: 15
		public static readonly HygienePropertyDefinition DescriptionProp = new HygienePropertyDefinition("Description", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000010 RID: 16
		public static readonly HygienePropertyDefinition DescriptionQueryProp = new HygienePropertyDefinition("Descriptions", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x04000011 RID: 17
		public static readonly HygienePropertyDefinition ParamNamesTableProp = new HygienePropertyDefinition("tvp_ParamNames", typeof(DataTable));

		// Token: 0x04000012 RID: 18
		public static readonly HygienePropertyDefinition ItemsTableProp = new HygienePropertyDefinition("tvp_Items", typeof(DataTable));

		// Token: 0x04000013 RID: 19
		public static readonly HygienePropertyDefinition NameVersionsTableProp = new HygienePropertyDefinition("tvp_NameVersions", typeof(DataTable));

		// Token: 0x02000007 RID: 7
		public sealed class AppConfigNameVersions : ConfigurablePropertyBag
		{
			// Token: 0x0600003D RID: 61 RVA: 0x00002742 File Offset: 0x00000942
			public AppConfigNameVersions(DataTable nameVersions)
			{
				this.NameVersions = nameVersions;
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x0600003E RID: 62 RVA: 0x00002751 File Offset: 0x00000951
			public override ObjectId Identity
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x0600003F RID: 63 RVA: 0x00002758 File Offset: 0x00000958
			// (set) Token: 0x06000040 RID: 64 RVA: 0x0000276A File Offset: 0x0000096A
			public DataTable NameVersions
			{
				get
				{
					return (DataTable)this[AppConfigSchema.NameVersionsTableProp];
				}
				set
				{
					this[AppConfigSchema.NameVersionsTableProp] = value;
				}
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00002778 File Offset: 0x00000978
			public override Type GetSchemaType()
			{
				return typeof(AppConfigSchema);
			}
		}

		// Token: 0x02000008 RID: 8
		public sealed class AppConfigItems : ConfigurablePropertyBag
		{
			// Token: 0x06000042 RID: 66 RVA: 0x00002784 File Offset: 0x00000984
			public AppConfigItems(DataTable items)
			{
				this.Items = items;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000043 RID: 67 RVA: 0x00002793 File Offset: 0x00000993
			public override ObjectId Identity
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000044 RID: 68 RVA: 0x0000279A File Offset: 0x0000099A
			// (set) Token: 0x06000045 RID: 69 RVA: 0x000027AC File Offset: 0x000009AC
			public DataTable Items
			{
				get
				{
					return (DataTable)this[AppConfigSchema.ItemsTableProp];
				}
				set
				{
					this[AppConfigSchema.ItemsTableProp] = value;
				}
			}

			// Token: 0x06000046 RID: 70 RVA: 0x000027BA File Offset: 0x000009BA
			public override Type GetSchemaType()
			{
				return typeof(AppConfigSchema);
			}
		}

		// Token: 0x02000009 RID: 9
		public sealed class AppConfigByName : AppConfigParameter
		{
		}

		// Token: 0x0200000A RID: 10
		public sealed class AppConfigByDescription : AppConfigParameter
		{
		}

		// Token: 0x0200000B RID: 11
		public sealed class AppConfigByVersion : AppConfigParameter
		{
		}
	}
}
