using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000029 RID: 41
	public class RuleCollection : IList<Rule>, ICollection<Rule>, IEnumerable<Rule>, IEnumerable
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00003EBC File Offset: 0x000020BC
		public RuleCollection(string name)
		{
			this.name = name;
			this.historyPropertyName = "Microsoft.Exchange." + name + ".RuleCollectionHistory";
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003EEC File Offset: 0x000020EC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003EF4 File Offset: 0x000020F4
		public int Count
		{
			get
			{
				return this.rules.Count;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003F01 File Offset: 0x00002101
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003F04 File Offset: 0x00002104
		public int CountAllNotDisabled
		{
			get
			{
				int num = 0;
				foreach (Rule rule in this.rules)
				{
					if (rule.Enabled != RuleState.Disabled)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003F60 File Offset: 0x00002160
		public string HistoryPropertyName
		{
			get
			{
				return this.historyPropertyName;
			}
		}

		// Token: 0x17000051 RID: 81
		public Rule this[int index]
		{
			get
			{
				return this.rules[index];
			}
			set
			{
				this.CheckNameNotExistsExcept(value.Name, index);
				this.rules[index] = value;
			}
		}

		// Token: 0x17000052 RID: 82
		public Rule this[string ruleName]
		{
			get
			{
				foreach (Rule rule in this.rules)
				{
					if (rule.Name.Equals(ruleName, StringComparison.OrdinalIgnoreCase))
					{
						return rule;
					}
				}
				return null;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003FF8 File Offset: 0x000021F8
		public int IndexOf(Rule rule)
		{
			return this.rules.IndexOf(rule);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004006 File Offset: 0x00002206
		public void RemoveAt(int index)
		{
			this.rules.RemoveAt(index);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004014 File Offset: 0x00002214
		public bool Remove(Rule rule)
		{
			return this.rules.Remove(rule);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004022 File Offset: 0x00002222
		public void Insert(int index, Rule rule)
		{
			this.CheckNameNotExists(rule.Name);
			this.rules.Insert(index, rule);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000403D File Offset: 0x0000223D
		public void Add(Rule rule)
		{
			this.CheckNameNotExists(rule.Name);
			this.rules.Add(rule);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004057 File Offset: 0x00002257
		internal void AddWithoutNameCheck(Rule rule)
		{
			this.rules.Add(rule);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004065 File Offset: 0x00002265
		public void Clear()
		{
			this.rules.Clear();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004072 File Offset: 0x00002272
		public bool Contains(Rule rule)
		{
			return this.rules.Contains(rule);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004080 File Offset: 0x00002280
		public void CopyTo(Rule[] array, int arrayIndex)
		{
			this.rules.CopyTo(array, arrayIndex);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000408F File Offset: 0x0000228F
		public IEnumerator<Rule> GetEnumerator()
		{
			return this.rules.GetEnumerator();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000040A1 File Offset: 0x000022A1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.rules.GetEnumerator();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000040B4 File Offset: 0x000022B4
		public bool Remove(string name)
		{
			foreach (Rule rule in this.rules)
			{
				if (rule.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					return this.rules.Remove(rule);
				}
			}
			return false;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004124 File Offset: 0x00002324
		public int GetEstimatedSize()
		{
			int num = 0;
			foreach (Rule rule in this.rules)
			{
				num += rule.GetEstimatedSize();
			}
			return num;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000417C File Offset: 0x0000237C
		private void CheckNameNotExists(string name)
		{
			foreach (Rule rule in this.rules)
			{
				if (rule.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					throw new RulesValidationException(RulesStrings.RuleNameExists(name));
				}
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000041E4 File Offset: 0x000023E4
		private void CheckNameNotExistsExcept(string name, int index)
		{
			for (int i = 0; i < this.rules.Count; i++)
			{
				if (this.rules[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase) && i != index)
				{
					throw new RulesValidationException(RulesStrings.RuleNameExists(name));
				}
			}
		}

		// Token: 0x04000055 RID: 85
		private string name;

		// Token: 0x04000056 RID: 86
		private ShortList<Rule> rules = new ShortList<Rule>();

		// Token: 0x04000057 RID: 87
		private string historyPropertyName;
	}
}
