using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200052D RID: 1325
	[DataContract]
	public class SetOrgConfig : SetObjectProperties
	{
		// Token: 0x1700249D RID: 9373
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x000BD8AA File Offset: 0x000BBAAA
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-OrganizationConfig";
			}
		}

		// Token: 0x1700249E RID: 9374
		// (get) Token: 0x06003EFB RID: 16123 RVA: 0x000BD8B1 File Offset: 0x000BBAB1
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}

		// Token: 0x1700249F RID: 9375
		// (get) Token: 0x06003EFC RID: 16124 RVA: 0x000BD8B8 File Offset: 0x000BBAB8
		// (set) Token: 0x06003EFD RID: 16125 RVA: 0x000BD8CA File Offset: 0x000BBACA
		[DataMember]
		public string GroupNamingPolicy
		{
			internal get
			{
				return (string)base["DistributionGroupNamingPolicy"];
			}
			set
			{
				base["DistributionGroupNamingPolicy"] = value;
			}
		}

		// Token: 0x170024A0 RID: 9376
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x000BD8D8 File Offset: 0x000BBAD8
		// (set) Token: 0x06003EFF RID: 16127 RVA: 0x000BD8EA File Offset: 0x000BBAEA
		[DataMember]
		public IEnumerable<string> DistributionGroupNameBlockedWordsList
		{
			get
			{
				return (IEnumerable<string>)base["DistributionGroupNameBlockedWordsList"];
			}
			set
			{
				base["DistributionGroupNameBlockedWordsList"] = value;
			}
		}
	}
}
