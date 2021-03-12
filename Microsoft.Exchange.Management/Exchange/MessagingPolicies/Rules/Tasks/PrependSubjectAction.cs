using System;
using System.Collections.Generic;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B96 RID: 2966
	[Serializable]
	public class PrependSubjectAction : TransportRuleAction, IEquatable<PrependSubjectAction>
	{
		// Token: 0x06006FD2 RID: 28626 RVA: 0x001CA308 File Offset: 0x001C8508
		public override int GetHashCode()
		{
			return this.Prefix.GetHashCode();
		}

		// Token: 0x06006FD3 RID: 28627 RVA: 0x001CA329 File Offset: 0x001C8529
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as PrependSubjectAction)));
		}

		// Token: 0x06006FD4 RID: 28628 RVA: 0x001CA364 File Offset: 0x001C8564
		public bool Equals(PrependSubjectAction other)
		{
			return this.Prefix.Equals(other.Prefix);
		}

		// Token: 0x170022D7 RID: 8919
		// (get) Token: 0x06006FD5 RID: 28629 RVA: 0x001CA385 File Offset: 0x001C8585
		// (set) Token: 0x06006FD6 RID: 28630 RVA: 0x001CA38D File Offset: 0x001C858D
		[LocDisplayName(RulesTasksStrings.IDs.PrefixDisplayName)]
		[ActionParameterName("PrependSubject")]
		[LocDescription(RulesTasksStrings.IDs.PrefixDescription)]
		public SubjectPrefix Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x170022D8 RID: 8920
		// (get) Token: 0x06006FD7 RID: 28631 RVA: 0x001CA398 File Offset: 0x001C8598
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionPrependSubject(this.Prefix.ToString());
			}
		}

		// Token: 0x06006FD8 RID: 28632 RVA: 0x001CA3C4 File Offset: 0x001C85C4
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "PrependSubject")
			{
				return null;
			}
			PrependSubjectAction prependSubjectAction = new PrependSubjectAction();
			try
			{
				prependSubjectAction.Prefix = new SubjectPrefix(TransportRuleAction.GetStringValue(action.Arguments[0]));
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
			return prependSubjectAction;
		}

		// Token: 0x06006FD9 RID: 28633 RVA: 0x001CA424 File Offset: 0x001C8624
		internal override void Reset()
		{
			this.prefix = SubjectPrefix.Empty;
			base.Reset();
		}

		// Token: 0x06006FDA RID: 28634 RVA: 0x001CA438 File Offset: 0x001C8638
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.prefix == SubjectPrefix.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			int index;
			if (!Utils.CheckIsUnicodeStringWellFormed(this.prefix.Value, out index))
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.CommentsHaveInvalidChars((int)this.prefix.Value[index]), base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06006FDB RID: 28635 RVA: 0x001CA4B4 File Offset: 0x001C86B4
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("PrependSubject", new ShortList<Argument>
			{
				new Value(this.Prefix.ToString())
			}, Utils.GetActionName(this));
		}

		// Token: 0x06006FDC RID: 28636 RVA: 0x001CA4FC File Offset: 0x001C86FC
		internal override string GetActionParameters()
		{
			return Utils.QuoteCmdletParameter(this.Prefix.ToString());
		}

		// Token: 0x06006FDD RID: 28637 RVA: 0x001CA524 File Offset: 0x001C8724
		internal override void SuppressPiiData()
		{
			string text = SuppressingPiiProperty.TryRedactValue<string>(RuleSchema.PrependSubject, this.Prefix.ToString());
			if (text != null && text.Length > 32)
			{
				text = text.Substring(0, 32);
			}
			this.Prefix = new SubjectPrefix(text);
		}

		// Token: 0x040039CE RID: 14798
		private SubjectPrefix prefix;
	}
}
