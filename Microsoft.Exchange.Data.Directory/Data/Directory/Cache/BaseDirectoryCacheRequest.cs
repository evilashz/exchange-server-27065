using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x0200008A RID: 138
	[DebuggerDisplay("{RequestId}")]
	[DataContract]
	internal abstract class BaseDirectoryCacheRequest : IExtensibleDataObject
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0002592C File Offset: 0x00023B2C
		protected BaseDirectoryCacheRequest()
		{
			this.RequestId = string.Concat(new object[]
			{
				Globals.ProcessId,
				":",
				Globals.ProcessName,
				":",
				Thread.CurrentThread.ManagedThreadId,
				":",
				Guid.NewGuid().ToString()
			});
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000259A7 File Offset: 0x00023BA7
		protected BaseDirectoryCacheRequest(string forestOrPartitionFqdn) : this()
		{
			ArgumentValidator.ThrowIfNullOrEmpty("forestOrPartitionFqdn", forestOrPartitionFqdn);
			this.ForestOrPartitionFqdn = forestOrPartitionFqdn;
			this.OrganizationId = null;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x000259C8 File Offset: 0x00023BC8
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x000259D0 File Offset: 0x00023BD0
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string RequestId { get; private set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x000259D9 File Offset: 0x00023BD9
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x000259E1 File Offset: 0x00023BE1
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		public string ForestOrPartitionFqdn { get; protected set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x000259EA File Offset: 0x00023BEA
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x000259F2 File Offset: 0x00023BF2
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string OrganizationId { get; private set; }

		// Token: 0x060006FD RID: 1789 RVA: 0x000259FB File Offset: 0x00023BFB
		protected void InternalSetOrganizationId(OrganizationId organizationId)
		{
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			if (Microsoft.Exchange.Data.Directory.OrganizationId.ForestWideOrgId.Equals(organizationId))
			{
				return;
			}
			this.OrganizationId = organizationId.ConfigurationUnit.DistinguishedName;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00025A27 File Offset: 0x00023C27
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x00025A2F File Offset: 0x00023C2F
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
