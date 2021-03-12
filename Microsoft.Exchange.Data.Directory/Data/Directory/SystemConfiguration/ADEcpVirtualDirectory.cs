using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000365 RID: 869
	[Serializable]
	public sealed class ADEcpVirtualDirectory : ExchangeWebAppVirtualDirectory
	{
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x000A88AE File Offset: 0x000A6AAE
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADEcpVirtualDirectory.schema;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x000A88B5 File Offset: 0x000A6AB5
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADEcpVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x000A88BC File Offset: 0x000A6ABC
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x000A88C3 File Offset: 0x000A6AC3
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x000A88D6 File Offset: 0x000A6AD6
		// (set) Token: 0x0600282E RID: 10286 RVA: 0x000A88E8 File Offset: 0x000A6AE8
		[Parameter]
		public bool AdminEnabled
		{
			get
			{
				return (bool)this[ADEcpVirtualDirectorySchema.AdminEnabled];
			}
			set
			{
				this[ADEcpVirtualDirectorySchema.AdminEnabled] = value;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x000A88FB File Offset: 0x000A6AFB
		// (set) Token: 0x06002830 RID: 10288 RVA: 0x000A890D File Offset: 0x000A6B0D
		[Parameter]
		public bool OwaOptionsEnabled
		{
			get
			{
				return (bool)this[ADEcpVirtualDirectorySchema.OwaOptionsEnabled];
			}
			set
			{
				this[ADEcpVirtualDirectorySchema.OwaOptionsEnabled] = value;
			}
		}

		// Token: 0x04001864 RID: 6244
		private static readonly ADEcpVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADEcpVirtualDirectorySchema>();

		// Token: 0x04001865 RID: 6245
		public static readonly string MostDerivedClass = "msExchEcpVirtualDirectory";
	}
}
