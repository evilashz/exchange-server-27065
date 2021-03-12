using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AjaxControlToolkit
{
	// Token: 0x0200002A RID: 42
	public class ScriptBuckets : Collection<CombinableScripts>
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00005A8C File Offset: 0x00003C8C
		public CombinableScripts GetScriptByAlias(string alias, bool thrownOnNotFound = false)
		{
			CombinableScripts result = null;
			if (!this.Alias2Script.TryGetValue(alias.ToLowerInvariant(), out result) && thrownOnNotFound)
			{
				throw new InvalidOperationException(string.Format("Script alias '{0}' cannot be found.", alias));
			}
			return result;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public CombinableScripts GetScriptByName(string name)
		{
			CombinableScripts result = null;
			this.Name2Script.TryGetValue(name.ToLowerInvariant(), out result);
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005AEC File Offset: 0x00003CEC
		internal void Initialize()
		{
			Dictionary<string, CombinableScripts> dictionary = this.Alias2Script;
			Dictionary<string, CombinableScripts> dictionary2 = this.Name2Script;
			foreach (CombinableScripts combinableScripts in this)
			{
				if (combinableScripts.Rank == 0)
				{
					this.IncreaseScriptRank(combinableScripts);
				}
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005B4C File Offset: 0x00003D4C
		internal void InitializeRuntimeScript(CombinableScripts script)
		{
			if (this.GetScriptByAlias(script.Alias, false) == null)
			{
				base.Add(script);
				this.alias2Script = null;
				this.name2Script = null;
				this.Initialize();
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005B88 File Offset: 0x00003D88
		private void IncreaseScriptRank(CombinableScripts script)
		{
			script.Rank++;
			if (script.Rank > base.Count)
			{
				throw new InvalidOperationException("Circular reference detected in CombinableScripts.DependsOn.");
			}
			if (script.DependsOn != null && script.DependsOn.Length > 0)
			{
				for (int i = 0; i < script.DependsOn.Length; i++)
				{
					CombinableScripts scriptByAlias = this.GetScriptByAlias(script.DependsOn[i], true);
					if (scriptByAlias.Rank <= script.Rank)
					{
						scriptByAlias.Rank = script.Rank;
						this.IncreaseScriptRank(scriptByAlias);
					}
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005C14 File Offset: 0x00003E14
		private Dictionary<string, CombinableScripts> Name2Script
		{
			get
			{
				if (this.name2Script == null)
				{
					Dictionary<string, CombinableScripts> dictionary = new Dictionary<string, CombinableScripts>(base.Count * 2);
					foreach (CombinableScripts combinableScripts in this)
					{
						foreach (ScriptEntry scriptEntry in combinableScripts.Scripts)
						{
							dictionary.Add(scriptEntry.Name.ToLowerInvariant(), combinableScripts);
						}
					}
					this.name2Script = dictionary;
				}
				return this.name2Script;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00005CC4 File Offset: 0x00003EC4
		private Dictionary<string, CombinableScripts> Alias2Script
		{
			get
			{
				if (this.alias2Script == null)
				{
					Dictionary<string, CombinableScripts> dictionary = new Dictionary<string, CombinableScripts>(base.Count);
					foreach (CombinableScripts combinableScripts in this)
					{
						dictionary.Add(combinableScripts.Alias.ToLowerInvariant(), combinableScripts);
					}
					this.alias2Script = dictionary;
				}
				return this.alias2Script;
			}
		}

		// Token: 0x0400005B RID: 91
		private Dictionary<string, CombinableScripts> name2Script;

		// Token: 0x0400005C RID: 92
		private Dictionary<string, CombinableScripts> alias2Script;
	}
}
