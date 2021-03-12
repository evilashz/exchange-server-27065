using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000E8 RID: 232
	[DataContract]
	public abstract class UpdateDistributionGroupMemberBase : WebServiceParameters
	{
		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x0005B6CF File Offset: 0x000598CF
		public override string AssociatedCmdlet
		{
			get
			{
				return "Update-DistributionGroupMember";
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0005B6D6 File Offset: 0x000598D6
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x0005B6E8 File Offset: 0x000598E8
		[DataMember]
		public Identity[] Members
		{
			get
			{
				return (Identity[])base[DistributionGroupSchema.Members];
			}
			set
			{
				base[DistributionGroupSchema.Members] = value.ToIdParameters();
			}
		}
	}
}
