using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	public sealed class UMCallAnsweringRule : ConfigurableObject
	{
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x00043282 File Offset: 0x00041482
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return UMCallAnsweringRule.schema;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00043289 File Offset: 0x00041489
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00043290 File Offset: 0x00041490
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x000432A2 File Offset: 0x000414A2
		[Parameter]
		public MultiValuedProperty<CallerIdItem> CallerIds
		{
			get
			{
				return (MultiValuedProperty<CallerIdItem>)this[UMCallAnsweringRuleSchema.CallerIds];
			}
			set
			{
				this[UMCallAnsweringRuleSchema.CallerIds] = value;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x000432B0 File Offset: 0x000414B0
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x000432C2 File Offset: 0x000414C2
		[Parameter]
		public bool CallersCanInterruptGreeting
		{
			get
			{
				return (bool)this[UMCallAnsweringRuleSchema.CallersCanInterruptGreeting];
			}
			set
			{
				this[UMCallAnsweringRuleSchema.CallersCanInterruptGreeting] = value;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x000432D5 File Offset: 0x000414D5
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x000432E7 File Offset: 0x000414E7
		[Parameter]
		public bool CheckAutomaticReplies
		{
			get
			{
				return (bool)this[UMCallAnsweringRuleSchema.CheckAutomaticReplies];
			}
			set
			{
				this[UMCallAnsweringRuleSchema.CheckAutomaticReplies] = value;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x000432FA File Offset: 0x000414FA
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x0004330C File Offset: 0x0004150C
		public bool Enabled
		{
			get
			{
				return (bool)this[UMCallAnsweringRuleSchema.Enabled];
			}
			internal set
			{
				this[UMCallAnsweringRuleSchema.Enabled] = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0004331F File Offset: 0x0004151F
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00043331 File Offset: 0x00041531
		[Parameter]
		public MultiValuedProperty<string> ExtensionsDialed
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMCallAnsweringRuleSchema.ExtensionsDialed];
			}
			set
			{
				this[UMCallAnsweringRuleSchema.ExtensionsDialed] = value;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0004333F File Offset: 0x0004153F
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x00043347 File Offset: 0x00041547
		public bool InError { get; internal set; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00043350 File Offset: 0x00041550
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x00043362 File Offset: 0x00041562
		[Parameter]
		public MultiValuedProperty<KeyMapping> KeyMappings
		{
			get
			{
				return (MultiValuedProperty<KeyMapping>)this[UMCallAnsweringRuleSchema.KeyMappings];
			}
			set
			{
				this[UMCallAnsweringRuleSchema.KeyMappings] = value;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00043370 File Offset: 0x00041570
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x00043382 File Offset: 0x00041582
		[Parameter]
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return (string)this[UMCallAnsweringRuleSchema.Name];
			}
			set
			{
				this[UMCallAnsweringRuleSchema.Name] = value;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00043390 File Offset: 0x00041590
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x000433A2 File Offset: 0x000415A2
		[Parameter]
		public int Priority
		{
			get
			{
				return (int)this[UMCallAnsweringRuleSchema.Priority];
			}
			set
			{
				this[UMCallAnsweringRuleSchema.Priority] = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x000433B5 File Offset: 0x000415B5
		// (set) Token: 0x0600129F RID: 4767 RVA: 0x000433BD File Offset: 0x000415BD
		public RuleDescription Description { get; internal set; }

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x000433C6 File Offset: 0x000415C6
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x000433D8 File Offset: 0x000415D8
		[Parameter]
		public int ScheduleStatus
		{
			get
			{
				return (int)this[UMCallAnsweringRuleSchema.ScheduleStatus];
			}
			set
			{
				this[UMCallAnsweringRuleSchema.ScheduleStatus] = value;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x000433EB File Offset: 0x000415EB
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x000433FD File Offset: 0x000415FD
		[Parameter]
		public TimeOfDay TimeOfDay
		{
			get
			{
				return (TimeOfDay)this[UMCallAnsweringRuleSchema.TimeOfDay];
			}
			set
			{
				this[UMCallAnsweringRuleSchema.TimeOfDay] = value;
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0004340B File Offset: 0x0004160B
		public UMCallAnsweringRule() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00043418 File Offset: 0x00041618
		public UMCallAnsweringRule(UMCallAnsweringRuleId identity) : base(new SimpleProviderPropertyBag())
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.propertyBag.SetField(SimpleProviderObjectSchema.Identity, identity);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0004344B File Offset: 0x0004164B
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return base.ToString();
		}

		// Token: 0x0400039B RID: 923
		private static UMCallAnsweringRuleSchema schema = ObjectSchema.GetInstance<UMCallAnsweringRuleSchema>();
	}
}
