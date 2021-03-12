using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.MessagingPolicies.Journaling;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200040F RID: 1039
	[KnownType(typeof(JournalRule))]
	[DataContract]
	public class JournalRule : JournalRuleRow
	{
		// Token: 0x060034FD RID: 13565 RVA: 0x000A5264 File Offset: 0x000A3464
		public JournalRule(JournalRuleObject rule) : base(rule)
		{
			this.Rule = rule;
		}

		// Token: 0x170020CD RID: 8397
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x000A5274 File Offset: 0x000A3474
		// (set) Token: 0x060034FF RID: 13567 RVA: 0x000A527C File Offset: 0x000A347C
		public JournalRuleObject Rule { get; private set; }

		// Token: 0x170020CE RID: 8398
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x000A5288 File Offset: 0x000A3488
		// (set) Token: 0x06003501 RID: 13569 RVA: 0x000A52C7 File Offset: 0x000A34C7
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] Recipient
		{
			get
			{
				if (this.Rule.Recipient != null)
				{
					return new PeopleIdentity[]
					{
						this.Rule.Recipient.ToPeopleIdentity()
					};
				}
				return null;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
