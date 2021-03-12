using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000CD RID: 205
	[Serializable]
	public sealed class DeviceTenantRule : DeviceRuleBase
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00024403 File Offset: 0x00022603
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x0002440B File Offset: 0x0002260B
		public MultiValuedProperty<Guid> ExclusionList { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00024414 File Offset: 0x00022614
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x0002441C File Offset: 0x0002261C
		private new MultiValuedProperty<Guid> TargetGroups { get; set; }

		// Token: 0x06000878 RID: 2168 RVA: 0x00024425 File Offset: 0x00022625
		public DeviceTenantRule()
		{
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002442D File Offset: 0x0002262D
		public DeviceTenantRule(RuleStorage ruleStorage) : base(ruleStorage)
		{
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x00024436 File Offset: 0x00022636
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0002443E File Offset: 0x0002263E
		public PolicyResourceScope? ApplyPolicyTo { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00024447 File Offset: 0x00022647
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x0002444F File Offset: 0x0002264F
		public bool? BlockUnsupportedDevices { get; set; }

		// Token: 0x0600087E RID: 2174 RVA: 0x00024458 File Offset: 0x00022658
		protected override IEnumerable<Condition> GetTaskConditions()
		{
			List<Condition> list = new List<Condition>();
			if (this.ExclusionList != null)
			{
				List<string> list2 = new List<string>();
				foreach (Guid guid in this.ExclusionList)
				{
					list2.Add(guid.ToString());
				}
				list.Add(new IsPredicate(Property.CreateProperty("ExclusionList", typeof(Guid).ToString()), list2));
			}
			if (this.ApplyPolicyTo != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceTenantRule.AccessControl_ResourceScope, typeof(string).ToString()), new List<string>
				{
					((int)this.ApplyPolicyTo.Value).ToString()
				}));
			}
			if (this.BlockUnsupportedDevices != null)
			{
				list.Add(new NameValuesPairConfigurationPredicate(Property.CreateProperty(DeviceTenantRule.AccessControl_AllowActionOnUnsupportedPlatform, typeof(string).ToString()), new List<string>
				{
					this.BlockUnsupportedDevices.ToString()
				}));
			}
			return list;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000245A8 File Offset: 0x000227A8
		protected override void SetTaskConditions(IEnumerable<Condition> conditions)
		{
			foreach (Condition condition in conditions)
			{
				if (condition.GetType() == typeof(NameValuesPairConfigurationPredicate) || condition.GetType() == typeof(IsPredicate))
				{
					IsPredicate isPredicate = condition as IsPredicate;
					if (isPredicate != null)
					{
						MultiValuedProperty<Guid> multiValuedProperty = new MultiValuedProperty<Guid>();
						if (isPredicate.Property.Name.Equals("ExclusionList"))
						{
							if (isPredicate.Value.ParsedValue is Guid)
							{
								multiValuedProperty.Add(isPredicate.Value.ParsedValue);
							}
							if (isPredicate.Value.ParsedValue is List<Guid>)
							{
								foreach (string item in ((List<string>)isPredicate.Value.ParsedValue))
								{
									multiValuedProperty.Add(item);
								}
							}
							this.ExclusionList = multiValuedProperty;
						}
					}
					else
					{
						NameValuesPairConfigurationPredicate nameValuesPairConfigurationPredicate = condition as NameValuesPairConfigurationPredicate;
						if (nameValuesPairConfigurationPredicate != null)
						{
							bool value2;
							if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceTenantRule.AccessControl_ResourceScope))
							{
								PolicyResourceScope value;
								if (Enum.TryParse<PolicyResourceScope>(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value))
								{
									this.ApplyPolicyTo = new PolicyResourceScope?(value);
								}
							}
							else if (nameValuesPairConfigurationPredicate.Property.Name.Equals(DeviceTenantRule.AccessControl_AllowActionOnUnsupportedPlatform) && bool.TryParse(nameValuesPairConfigurationPredicate.Value.RawValues.FirstOrDefault<string>(), out value2))
							{
								this.BlockUnsupportedDevices = new bool?(value2);
							}
						}
					}
				}
			}
		}

		// Token: 0x0400039A RID: 922
		public static string AccessControl_ResourceScope = "AccessControl_ResourceScope";

		// Token: 0x0400039B RID: 923
		public static string AccessControl_AllowActionOnUnsupportedPlatform = "AccessControl_AllowActionOnUnsupportedPlatform";
	}
}
