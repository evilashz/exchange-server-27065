using System;
using System.Collections;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConditionCollection : CollectionBase
	{
		// Token: 0x0600033C RID: 828 RVA: 0x0000CCC1 File Offset: 0x0000AEC1
		public int Add(Condition c)
		{
			return base.List.Add(c);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		public int Add(ConditionCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			int result = base.Count;
			foreach (object obj in collection)
			{
				Condition value = (Condition)obj;
				result = base.List.Add(value);
			}
			return result;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000CD40 File Offset: 0x0000AF40
		public bool AddUnique(Condition newCondition, bool removeInconsistencies)
		{
			bool flag = false;
			for (int i = 0; i < base.Count; i++)
			{
				Condition condition = this[i];
				if (newCondition.Match(condition))
				{
					if (newCondition.Role.ExpectedResult != condition.Role.ExpectedResult)
					{
						if (!removeInconsistencies)
						{
							throw new ConditionException(newCondition);
						}
						base.RemoveAt(i);
					}
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.Add(newCondition);
			}
			return !flag;
		}

		// Token: 0x170000BF RID: 191
		public Condition this[int index]
		{
			get
			{
				return (Condition)base.List[index];
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("{");
			for (int i = 0; i < base.List.Count; i++)
			{
				stringBuilder.Append(base.List[i].ToString());
				if (i + 1 < base.List.Count)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
