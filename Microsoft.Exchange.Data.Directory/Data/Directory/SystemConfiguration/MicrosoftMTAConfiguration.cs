using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005B8 RID: 1464
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public sealed class MicrosoftMTAConfiguration : ADLegacyVersionableObject
	{
		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06004352 RID: 17234 RVA: 0x000FCC1B File Offset: 0x000FAE1B
		internal override ADObjectSchema Schema
		{
			get
			{
				return MicrosoftMTAConfiguration.schema;
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06004353 RID: 17235 RVA: 0x000FCC22 File Offset: 0x000FAE22
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MicrosoftMTAConfiguration.mostDerivedClass;
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x000FCC29 File Offset: 0x000FAE29
		// (set) Token: 0x06004355 RID: 17237 RVA: 0x000FCC3B File Offset: 0x000FAE3B
		[Parameter(Mandatory = true)]
		public string LocalDesig
		{
			get
			{
				return (string)this[MicrosoftMTAConfigurationSchema.LocalDesig];
			}
			set
			{
				this[MicrosoftMTAConfigurationSchema.LocalDesig] = value;
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x000FCC49 File Offset: 0x000FAE49
		// (set) Token: 0x06004357 RID: 17239 RVA: 0x000FCC5B File Offset: 0x000FAE5B
		[Parameter(Mandatory = true)]
		public int TransRetryMins
		{
			get
			{
				return (int)this[MicrosoftMTAConfigurationSchema.TransRetryMins];
			}
			set
			{
				this[MicrosoftMTAConfigurationSchema.TransRetryMins] = value;
			}
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x000FCC6E File Offset: 0x000FAE6E
		// (set) Token: 0x06004359 RID: 17241 RVA: 0x000FCC80 File Offset: 0x000FAE80
		[Parameter(Mandatory = true)]
		public int TransTimeoutMins
		{
			get
			{
				return (int)this[MicrosoftMTAConfigurationSchema.TransTimeoutMins];
			}
			set
			{
				this[MicrosoftMTAConfigurationSchema.TransTimeoutMins] = value;
			}
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x000FCC93 File Offset: 0x000FAE93
		// (set) Token: 0x0600435B RID: 17243 RVA: 0x000FCCA5 File Offset: 0x000FAEA5
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[MicrosoftMTAConfigurationSchema.ExchangeLegacyDN];
			}
			internal set
			{
				this[MicrosoftMTAConfigurationSchema.ExchangeLegacyDN] = value;
			}
		}

		// Token: 0x04002DAB RID: 11691
		public const string MTAObjectRdn = "Microsoft MTA";

		// Token: 0x04002DAC RID: 11692
		private static MicrosoftMTAConfigurationSchema schema = ObjectSchema.GetInstance<MicrosoftMTAConfigurationSchema>();

		// Token: 0x04002DAD RID: 11693
		private static string mostDerivedClass = "mTA";
	}
}
