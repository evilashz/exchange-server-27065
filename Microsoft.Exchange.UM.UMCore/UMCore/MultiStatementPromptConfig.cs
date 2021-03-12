using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200018D RID: 397
	internal class MultiStatementPromptConfig : StatementPromptConfig
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x00032C07 File Offset: 0x00030E07
		internal MultiStatementPromptConfig(List<PromptConfigBase> parameterPrompts, string name, string conditionString, ActivityManagerConfig managerConfig) : base(parameterPrompts, name, "statement", conditionString, managerConfig)
		{
			this.BuildInnerStatements(managerConfig);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00032C24 File Offset: 0x00030E24
		internal override void AddPrompts(ArrayList promptList, ActivityManager manager, CultureInfo culture)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.PromptName);
			foreach (PromptConfigBase promptConfigBase in base.ParameterPrompts)
			{
				if (promptConfigBase.Suffix != null && promptConfigBase.Suffix.Length != 0)
				{
					if (!promptConfigBase.Suffix.Equals("singularPlural", StringComparison.OrdinalIgnoreCase))
					{
						throw new FsmConfigurationException(Strings.MissingSuffixRule(promptConfigBase.Suffix));
					}
					string suffixVariable = promptConfigBase.GetSuffixVariable(manager);
					if (Regex.Match(suffixVariable, PromptConfigBase.PromptResourceManager.GetString("Zero", culture)).Success)
					{
						stringBuilder.Append("_Zero");
					}
					else if (Regex.Match(suffixVariable, PromptConfigBase.PromptResourceManager.GetString("Singular", culture)).Success)
					{
						stringBuilder.Append("_Singular");
					}
					else if (Regex.Match(suffixVariable, PromptConfigBase.PromptResourceManager.GetString("Plural", culture)).Success)
					{
						stringBuilder.Append("_Plural");
					}
					else
					{
						stringBuilder.Append("_Plural2");
					}
				}
			}
			SingleStatementPromptConfig singleStatementPromptConfig = this.singleStatements[stringBuilder.ToString()];
			singleStatementPromptConfig.AddPrompts(promptList, manager, culture);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00032D80 File Offset: 0x00030F80
		internal override void Validate()
		{
			foreach (SingleStatementPromptConfig singleStatementPromptConfig in this.singleStatements.Values)
			{
				singleStatementPromptConfig.Validate();
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00032DD8 File Offset: 0x00030FD8
		internal void BuildInnerStatements(ActivityManagerConfig managerConfig)
		{
			int num = 1;
			List<List<string>> list = this.BuildAllSuffixChunks(out num);
			this.singleStatements = new Dictionary<string, SingleStatementPromptConfig>(num);
			int[] array = new int[list.Count];
			array[0] = 1;
			for (int i = 1; i < array.Length; i++)
			{
				array[i] = array[i - 1] * list[i - 1].Count;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int j = 0; j < num; j++)
			{
				stringBuilder.Length = 0;
				stringBuilder.Append(base.PromptName);
				for (int k = 0; k < list.Count; k++)
				{
					int count = list[k].Count;
					int index = j / array[k] % count;
					stringBuilder.Append(list[k][index]);
				}
				string text = stringBuilder.ToString();
				SingleStatementPromptConfig value = new SingleStatementPromptConfig(base.ParameterPrompts, text, base.ConditionString, managerConfig);
				this.singleStatements.Add(text, value);
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00032EDC File Offset: 0x000310DC
		private List<List<string>> BuildAllSuffixChunks(out int numPermutations)
		{
			List<List<string>> list = new List<List<string>>();
			numPermutations = 1;
			foreach (PromptConfigBase promptConfigBase in base.ParameterPrompts)
			{
				if (promptConfigBase.Suffix != null && promptConfigBase.Suffix.Length != 0)
				{
					if (!promptConfigBase.Suffix.Equals("singularPlural", StringComparison.OrdinalIgnoreCase))
					{
						throw new FsmConfigurationException(Strings.MissingSuffixRule(promptConfigBase.Suffix));
					}
					List<string> list2 = new List<string>();
					list2.Add("_Zero");
					list2.Add("_Singular");
					list2.Add("_Plural");
					list2.Add("_Plural2");
					list.Add(list2);
					numPermutations *= list2.Count;
				}
			}
			return list;
		}

		// Token: 0x040009E0 RID: 2528
		private Dictionary<string, SingleStatementPromptConfig> singleStatements;
	}
}
