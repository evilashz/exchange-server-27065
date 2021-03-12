using System;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000236 RID: 566
	[DataContract]
	public class SetMyDistributionGroup : SetDistributionGroupBase<SetMyGroup, UpdateMyDistributionGroupMember>
	{
		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x060027D2 RID: 10194 RVA: 0x0007D23F File Offset: 0x0007B43F
		public override string RbacScope
		{
			get
			{
				return "@W:MyDistributionGroups";
			}
		}

		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x0007D248 File Offset: 0x0007B448
		// (set) Token: 0x060027D4 RID: 10196 RVA: 0x0007D27C File Offset: 0x0007B47C
		public bool IgnoreNamingPolicy
		{
			get
			{
				return base.ParameterIsSpecified("IgnoreNamingPolicy") && ((SwitchParameter)base["IgnoreNamingPolicy"]).ToBool();
			}
			set
			{
				base["IgnoreNamingPolicy"] = new SwitchParameter(value);
			}
		}
	}
}
