using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BC4 RID: 3012
	[Serializable]
	public class AttachmentSizeOverPredicate : SizeOverPredicate, IEquatable<AttachmentSizeOverPredicate>
	{
		// Token: 0x06007176 RID: 29046 RVA: 0x001CED52 File Offset: 0x001CCF52
		public AttachmentSizeOverPredicate() : base("Message.MaxAttachmentSize")
		{
		}

		// Token: 0x06007177 RID: 29047 RVA: 0x001CED60 File Offset: 0x001CCF60
		public override int GetHashCode()
		{
			return this.Size.GetHashCode();
		}

		// Token: 0x06007178 RID: 29048 RVA: 0x001CED81 File Offset: 0x001CCF81
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AttachmentSizeOverPredicate)));
		}

		// Token: 0x06007179 RID: 29049 RVA: 0x001CEDBC File Offset: 0x001CCFBC
		public bool Equals(AttachmentSizeOverPredicate other)
		{
			return this.Size.Equals(other.Size);
		}

		// Token: 0x17002324 RID: 8996
		// (get) Token: 0x0600717A RID: 29050 RVA: 0x001CEDDD File Offset: 0x001CCFDD
		// (set) Token: 0x0600717B RID: 29051 RVA: 0x001CEDE5 File Offset: 0x001CCFE5
		[LocDisplayName(RulesTasksStrings.IDs.AttachmentSizeDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.AttachmentSizeDescription)]
		[ConditionParameterName("AttachmentSizeOver")]
		[ExceptionParameterName("ExceptIfAttachmentSizeOver")]
		public override ByteQuantifiedSize Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		// Token: 0x17002325 RID: 8997
		// (get) Token: 0x0600717C RID: 29052 RVA: 0x001CEDF0 File Offset: 0x001CCFF0
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionAttachmentSizeOver(this.Size.ToString());
			}
		}

		// Token: 0x0600717D RID: 29053 RVA: 0x001CEE1B File Offset: 0x001CD01B
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			return SizeOverPredicate.CreateFromInternalCondition<AttachmentSizeOverPredicate>(condition, "Message.MaxAttachmentSize");
		}

		// Token: 0x04003A3B RID: 14907
		private const string InternalPropertyName = "Message.MaxAttachmentSize";
	}
}
