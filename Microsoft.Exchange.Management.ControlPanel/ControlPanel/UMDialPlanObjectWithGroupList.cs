using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000362 RID: 866
	[DataContract]
	public class UMDialPlanObjectWithGroupList : BaseRow
	{
		// Token: 0x06002FD8 RID: 12248 RVA: 0x00091C86 File Offset: 0x0008FE86
		public UMDialPlanObjectWithGroupList(UMDialPlan dialPlan) : base(dialPlan)
		{
			this.ConfiguredInCountryOrRegionGroupNameList = this.GetDialingRuleGroupNameList(dialPlan.ConfiguredInCountryOrRegionGroups);
			this.ConfiguredInternationalGroupNameList = this.GetDialingRuleGroupNameList(dialPlan.ConfiguredInternationalGroups);
		}

		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x00091CB3 File Offset: 0x0008FEB3
		// (set) Token: 0x06002FDA RID: 12250 RVA: 0x00091CBB File Offset: 0x0008FEBB
		public IEnumerable<string> ConfiguredInCountryOrRegionGroupNameList { get; private set; }

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x00091CC4 File Offset: 0x0008FEC4
		// (set) Token: 0x06002FDC RID: 12252 RVA: 0x00091CCC File Offset: 0x0008FECC
		public IEnumerable<string> ConfiguredInternationalGroupNameList { get; private set; }

		// Token: 0x06002FDD RID: 12253 RVA: 0x00091CD8 File Offset: 0x0008FED8
		private IEnumerable<string> GetDialingRuleGroupNameList(MultiValuedProperty<DialGroupEntry> dialingRuleGroups)
		{
			List<string> list = new List<string>(dialingRuleGroups.Count);
			foreach (DialGroupEntry dialGroupEntry in dialingRuleGroups)
			{
				if (!list.Contains(dialGroupEntry.Name))
				{
					list.Add(dialGroupEntry.Name);
				}
			}
			list.Sort(StringComparer.OrdinalIgnoreCase);
			return list;
		}
	}
}
