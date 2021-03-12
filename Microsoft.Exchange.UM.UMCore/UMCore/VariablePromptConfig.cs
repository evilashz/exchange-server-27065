using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000237 RID: 567
	internal class VariablePromptConfig<TPrompt, TPromptVariable> : PromptConfigBase where TPrompt : VariablePrompt<TPromptVariable>, new()
	{
		// Token: 0x0600109C RID: 4252 RVA: 0x0004A9EC File Offset: 0x00048BEC
		internal VariablePromptConfig(string name, string type, string conditionString, ActivityManagerConfig managerConfig) : base(name, type, conditionString, managerConfig)
		{
			QualifiedName variableName = new QualifiedName(name, managerConfig);
			this.fsmVariable = FsmVariable<TPromptVariable>.Create(variableName, managerConfig);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0004AA1C File Offset: 0x00048C1C
		internal override void AddPrompts(ArrayList promptList, ActivityManager manager, CultureInfo culture)
		{
			TPrompt tprompt = Activator.CreateInstance<TPrompt>();
			if (manager != null)
			{
				PromptSetting config = new PromptSetting(this);
				tprompt.Initialize(config, culture, this.fsmVariable.GetValue(manager));
				tprompt.SetProsodyRate(manager.ProsodyRate);
				tprompt.TTSLanguage = manager.MessagePlayerContext.Language;
			}
			else
			{
				PromptSetting config2 = new PromptSetting(this);
				tprompt.Initialize(config2, culture);
			}
			promptList.Add(tprompt);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0004AAA6 File Offset: 0x00048CA6
		internal override void Validate()
		{
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0004AAA8 File Offset: 0x00048CA8
		internal override string GetSuffixVariable(ActivityManager manager)
		{
			TPromptVariable value = this.fsmVariable.GetValue(manager);
			return value.ToString();
		}

		// Token: 0x04000B98 RID: 2968
		private FsmVariable<TPromptVariable> fsmVariable;
	}
}
