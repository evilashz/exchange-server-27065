using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.MessagingPolicies.Journaling;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200040E RID: 1038
	[DataContract]
	public class JournalRuleRow : RuleRow
	{
		// Token: 0x060034F0 RID: 13552 RVA: 0x000A5140 File Offset: 0x000A3340
		public JournalRuleRow(JournalRuleObject rule) : base(rule)
		{
			this.JournalEmailAddress = rule.JournalEmailAddress.ToString();
			this.Scope = rule.Scope.ToString();
			this.Global = this.Scope.Equals("Global");
			this.Internal = this.Scope.Equals("Internal");
			this.External = this.Scope.Equals("External");
			this.RecipientString = ((rule.Recipient != null) ? rule.Recipient.ToString() : string.Empty);
			base.Supported = true;
		}

		// Token: 0x170020C7 RID: 8391
		// (get) Token: 0x060034F1 RID: 13553 RVA: 0x000A51FE File Offset: 0x000A33FE
		// (set) Token: 0x060034F2 RID: 13554 RVA: 0x000A5206 File Offset: 0x000A3406
		[DataMember]
		public string JournalEmailAddress { get; private set; }

		// Token: 0x170020C8 RID: 8392
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x000A520F File Offset: 0x000A340F
		// (set) Token: 0x060034F4 RID: 13556 RVA: 0x000A5217 File Offset: 0x000A3417
		[DataMember]
		public string Scope { get; private set; }

		// Token: 0x170020C9 RID: 8393
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x000A5220 File Offset: 0x000A3420
		// (set) Token: 0x060034F6 RID: 13558 RVA: 0x000A5228 File Offset: 0x000A3428
		[DataMember(EmitDefaultValue = false)]
		public bool Global { get; private set; }

		// Token: 0x170020CA RID: 8394
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000A5231 File Offset: 0x000A3431
		// (set) Token: 0x060034F8 RID: 13560 RVA: 0x000A5239 File Offset: 0x000A3439
		[DataMember(EmitDefaultValue = false)]
		public bool Internal { get; private set; }

		// Token: 0x170020CB RID: 8395
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000A5242 File Offset: 0x000A3442
		// (set) Token: 0x060034FA RID: 13562 RVA: 0x000A524A File Offset: 0x000A344A
		[DataMember(EmitDefaultValue = false)]
		public bool External { get; private set; }

		// Token: 0x170020CC RID: 8396
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x000A5253 File Offset: 0x000A3453
		// (set) Token: 0x060034FC RID: 13564 RVA: 0x000A525B File Offset: 0x000A345B
		[DataMember]
		public string RecipientString { get; private set; }
	}
}
