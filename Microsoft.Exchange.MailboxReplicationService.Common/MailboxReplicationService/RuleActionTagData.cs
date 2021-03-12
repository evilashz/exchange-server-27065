using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200007B RID: 123
	[DataContract]
	internal sealed class RuleActionTagData : RuleActionData
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00009EDE File Offset: 0x000080DE
		public RuleActionTagData()
		{
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00009EE6 File Offset: 0x000080E6
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00009EEE File Offset: 0x000080EE
		[DataMember(EmitDefaultValue = false)]
		public PropValueData Value { get; set; }

		// Token: 0x06000561 RID: 1377 RVA: 0x00009EF7 File Offset: 0x000080F7
		public RuleActionTagData(RuleAction.Tag ruleAction) : base(ruleAction)
		{
			this.Value = DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(ruleAction.Value);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00009F11 File Offset: 0x00008111
		protected override RuleAction GetRuleActionInternal()
		{
			return new RuleAction.Tag(DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(this.Value));
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00009F24 File Offset: 0x00008124
		protected override void EnumPropTagsInternal(CommonUtils.EnumPropTagDelegate del)
		{
			int propTag = this.Value.PropTag;
			del(ref propTag);
			this.Value.PropTag = propTag;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00009F51 File Offset: 0x00008151
		protected override void EnumPropValuesInternal(CommonUtils.EnumPropValueDelegate del)
		{
			del(this.Value);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00009F5F File Offset: 0x0000815F
		protected override string ToStringInternal()
		{
			return string.Format("TAG {0}", this.Value);
		}
	}
}
