using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001D4 RID: 468
	internal class SingleStatementPromptConfig : StatementPromptConfig
	{
		// Token: 0x06000D9A RID: 3482 RVA: 0x0003C591 File Offset: 0x0003A791
		internal SingleStatementPromptConfig(List<PromptConfigBase> parameterPrompts, string name, string conditionString, ActivityManagerConfig managerConfig) : base(parameterPrompts, name, "statement", conditionString, managerConfig)
		{
			this.BuildAllCulturePromptConfigs(managerConfig);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003C5AC File Offset: 0x0003A7AC
		internal override void AddPrompts(ArrayList promptList, ActivityManager manager, CultureInfo culture)
		{
			ArrayList arrayList = null;
			try
			{
				arrayList = this.allCulturePromptConfigs[culture];
			}
			catch (KeyNotFoundException innerException)
			{
				throw new FsmConfigurationException(Strings.MissingResourcePrompt(base.PromptName, culture.LCID), innerException);
			}
			foreach (object obj in arrayList)
			{
				PromptConfigBase promptConfigBase = (PromptConfigBase)obj;
				promptConfigBase.AddPrompts(promptList, manager, culture);
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0003C640 File Offset: 0x0003A840
		internal override void Validate()
		{
			ICollection<CultureInfo> collection;
			if (base.PromptName.StartsWith("vui", StringComparison.Ordinal))
			{
				collection = GlobCfg.VuiCultures;
			}
			else
			{
				collection = UmCultures.GetSupportedPromptCultures();
			}
			foreach (CultureInfo key in collection)
			{
				foreach (object obj in this.allCulturePromptConfigs[key])
				{
					PromptConfigBase promptConfigBase = (PromptConfigBase)obj;
					promptConfigBase.Validate();
				}
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003C6F8 File Offset: 0x0003A8F8
		private void BuildAllCulturePromptConfigs(ActivityManagerConfig managerConfig)
		{
			ICollection<CultureInfo> collection;
			if (base.PromptName.StartsWith("vui", StringComparison.Ordinal))
			{
				collection = GlobCfg.VuiCultures;
			}
			else
			{
				collection = UmCultures.GetSupportedPromptCultures();
			}
			this.allCulturePromptConfigs = new Dictionary<CultureInfo, ArrayList>(collection.Count);
			foreach (CultureInfo cultureInfo in collection)
			{
				List<PromptConfigBase> c = PromptUtils.CreateStatementPromptConfig(base.PromptName, base.ConditionString, base.ParameterPrompts, cultureInfo, managerConfig);
				this.allCulturePromptConfigs[cultureInfo] = new ArrayList(c);
			}
		}

		// Token: 0x04000A9C RID: 2716
		private Dictionary<CultureInfo, ArrayList> allCulturePromptConfigs;
	}
}
