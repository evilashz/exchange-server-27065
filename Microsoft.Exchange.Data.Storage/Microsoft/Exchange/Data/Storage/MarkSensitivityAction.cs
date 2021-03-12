using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B7F RID: 2943
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MarkSensitivityAction : ActionBase
	{
		// Token: 0x06006A31 RID: 27185 RVA: 0x001C5EFD File Offset: 0x001C40FD
		private MarkSensitivityAction(ActionType actionType, Sensitivity sensitivity, Rule rule) : base(actionType, rule)
		{
			this.sensitivity = sensitivity;
		}

		// Token: 0x06006A32 RID: 27186 RVA: 0x001C5F10 File Offset: 0x001C4110
		public static MarkSensitivityAction Create(Sensitivity sensitivity, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule
			});
			EnumValidator.ThrowIfInvalid<Sensitivity>(sensitivity, "sensitivity");
			return new MarkSensitivityAction(ActionType.MarkSensitivityAction, sensitivity, rule);
		}

		// Token: 0x17001D04 RID: 7428
		// (get) Token: 0x06006A33 RID: 27187 RVA: 0x001C5F42 File Offset: 0x001C4142
		public Sensitivity Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x001C5F4C File Offset: 0x001C414C
		internal override RuleAction BuildRuleAction()
		{
			return new RuleAction.Tag(new PropValue(PropTag.Sensitivity, this.Sensitivity));
		}

		// Token: 0x04003C6B RID: 15467
		private Sensitivity sensitivity;
	}
}
