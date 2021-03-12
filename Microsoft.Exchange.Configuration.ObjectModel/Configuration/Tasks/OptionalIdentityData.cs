using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000131 RID: 305
	public class OptionalIdentityData : ICloneable
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x000234DD File Offset: 0x000216DD
		public OptionalIdentityData Clone()
		{
			return (OptionalIdentityData)base.MemberwiseClone();
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x000234EA File Offset: 0x000216EA
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x000234F2 File Offset: 0x000216F2
		public QueryFilter AdditionalFilter { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x000234FB File Offset: 0x000216FB
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00023503 File Offset: 0x00021703
		public ADObjectId ConfigurationContainerRdn
		{
			get
			{
				return this.configurationContainerRdn;
			}
			set
			{
				this.configurationContainerRdn = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0002350C File Offset: 0x0002170C
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x00023514 File Offset: 0x00021714
		public ADObjectId RootOrgDomainContainerId
		{
			get
			{
				return this.rootOrgDomainContainerId;
			}
			set
			{
				this.rootOrgDomainContainerId = value;
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002351D File Offset: 0x0002171D
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0400028F RID: 655
		private ADObjectId configurationContainerRdn;

		// Token: 0x04000290 RID: 656
		private ADObjectId rootOrgDomainContainerId;
	}
}
