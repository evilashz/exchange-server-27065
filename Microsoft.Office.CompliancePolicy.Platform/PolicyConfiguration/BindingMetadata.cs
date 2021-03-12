using System;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x02000082 RID: 130
	[Serializable]
	public sealed class BindingMetadata
	{
		// Token: 0x0600035C RID: 860 RVA: 0x0000B8E5 File Offset: 0x00009AE5
		public BindingMetadata()
		{
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000B8ED File Offset: 0x00009AED
		public BindingMetadata(string displayName, string name, string immutableIdentity, PolicyBindingTypes type = PolicyBindingTypes.IndividualResource)
		{
			this.DisplayName = displayName;
			this.Name = name;
			this.ImmutableIdentity = immutableIdentity;
			this.Type = type;
			this.SchemaVersion = BindingMetadata.latestVersion;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000B91D File Offset: 0x00009B1D
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000B925 File Offset: 0x00009B25
		public string DisplayName { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000B92E File Offset: 0x00009B2E
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000B936 File Offset: 0x00009B36
		public string Name { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000B93F File Offset: 0x00009B3F
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000B947 File Offset: 0x00009B47
		public string ImmutableIdentity { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000B950 File Offset: 0x00009B50
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000B958 File Offset: 0x00009B58
		public PolicyBindingTypes Type { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000B961 File Offset: 0x00009B61
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000B969 File Offset: 0x00009B69
		public Workload Workload { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000B972 File Offset: 0x00009B72
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000B97A File Offset: 0x00009B7A
		public int SchemaVersion { get; set; }

		// Token: 0x0600036A RID: 874 RVA: 0x0000B983 File Offset: 0x00009B83
		public static BindingMetadata FromStorage(string storageValue)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("storageValue", storageValue);
			return CommonUtility.StringToObject<BindingMetadata>(storageValue);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000B996 File Offset: 0x00009B96
		public static string ToStorage(BindingMetadata binding)
		{
			ArgumentValidator.ThrowIfNull("binding", binding);
			return CommonUtility.ObjectToString(binding);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000B9A9 File Offset: 0x00009BA9
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public override bool Equals(object other)
		{
			bool result = false;
			if (other != null)
			{
				if (this == other)
				{
					result = true;
				}
				else
				{
					BindingMetadata bindingMetadata = other as BindingMetadata;
					if (bindingMetadata != null)
					{
						result = (string.Equals(bindingMetadata.DisplayName, this.DisplayName) && string.Equals(bindingMetadata.Name, this.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(bindingMetadata.ImmutableIdentity, this.ImmutableIdentity, StringComparison.OrdinalIgnoreCase) && bindingMetadata.SchemaVersion == this.SchemaVersion && bindingMetadata.Type == this.Type && bindingMetadata.Workload == this.Workload);
					}
				}
			}
			return result;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000BA42 File Offset: 0x00009C42
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000226 RID: 550
		private static readonly int latestVersion = 2;
	}
}
