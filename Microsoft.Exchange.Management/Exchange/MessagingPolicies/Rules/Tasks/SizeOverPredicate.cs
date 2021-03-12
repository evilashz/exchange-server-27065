using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BC3 RID: 3011
	[Serializable]
	public abstract class SizeOverPredicate : TransportRulePredicate
	{
		// Token: 0x17002323 RID: 8995
		// (get) Token: 0x0600716E RID: 29038 RVA: 0x001CEBE8 File Offset: 0x001CCDE8
		// (set) Token: 0x0600716F RID: 29039 RVA: 0x001CEBF0 File Offset: 0x001CCDF0
		public virtual ByteQuantifiedSize Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x001CEBF9 File Offset: 0x001CCDF9
		protected SizeOverPredicate(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x06007171 RID: 29041 RVA: 0x001CEC08 File Offset: 0x001CCE08
		internal static TransportRulePredicate CreateFromInternalCondition<T>(Condition condition, string propertyName) where T : SizeOverPredicate, new()
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals("greaterThanOrEqual") || !predicateCondition.Property.Name.Equals(propertyName) || predicateCondition.Value.RawValues.Count != 1)
			{
				return null;
			}
			ulong num;
			if (!ulong.TryParse(predicateCondition.Value.RawValues[0], out num))
			{
				return null;
			}
			T t = Activator.CreateInstance<T>();
			if (num == 0UL)
			{
				t.Size = ByteQuantifiedSize.FromKB(0UL);
			}
			else
			{
				t.Size = ByteQuantifiedSize.FromBytes(num);
			}
			return t;
		}

		// Token: 0x06007172 RID: 29042 RVA: 0x001CECB7 File Offset: 0x001CCEB7
		internal override void Reset()
		{
			this.size = ByteQuantifiedSize.FromKB(0UL);
			base.Reset();
		}

		// Token: 0x06007173 RID: 29043 RVA: 0x001CECCC File Offset: 0x001CCECC
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
		}

		// Token: 0x06007174 RID: 29044 RVA: 0x001CECD8 File Offset: 0x001CCED8
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			shortList.Add(this.Size.ToBytes().ToString());
			return TransportRuleParser.Instance.CreatePredicate("greaterThanOrEqual", TransportRuleParser.Instance.CreateProperty(this.propertyName), shortList);
		}

		// Token: 0x06007175 RID: 29045 RVA: 0x001CED2C File Offset: 0x001CCF2C
		internal override string GetPredicateParameters()
		{
			return Utils.QuoteCmdletParameter(this.Size.ToString());
		}

		// Token: 0x04003A39 RID: 14905
		private readonly string propertyName;

		// Token: 0x04003A3A RID: 14906
		private ByteQuantifiedSize size;
	}
}
