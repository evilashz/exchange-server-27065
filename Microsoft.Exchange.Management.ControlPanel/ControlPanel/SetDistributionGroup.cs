using System;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004F0 RID: 1264
	[DataContract]
	public class SetDistributionGroup : SetDistributionGroupBase<SetGroup, UpdateDistributionGroupMember>
	{
		// Token: 0x1700240C RID: 9228
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x000B833E File Offset: 0x000B653E
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x1700240D RID: 9229
		// (get) Token: 0x06003D32 RID: 15666 RVA: 0x000B8348 File Offset: 0x000B6548
		// (set) Token: 0x06003D33 RID: 15667 RVA: 0x000B837C File Offset: 0x000B657C
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
