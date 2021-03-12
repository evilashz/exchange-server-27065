using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001E0 RID: 480
	internal class SpeechMenuConfig : MenuConfig
	{
		// Token: 0x06000E20 RID: 3616 RVA: 0x0003F5D4 File Offset: 0x0003D7D4
		internal SpeechMenuConfig(ActivityManagerConfig manager) : base(manager)
		{
			this.parseState = SpeechMenuConfig.ParseState.Root;
			this.grammars = new List<UMGrammarConfig>();
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0003F5EF File Offset: 0x0003D7EF
		internal float Confidence
		{
			get
			{
				return this.confidence;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0003F5F7 File Offset: 0x0003D7F7
		internal int BabbleSeconds
		{
			get
			{
				return this.babbleSeconds;
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003F5FF File Offset: 0x0003D7FF
		internal override ActivityBase CreateActivity(ActivityManager manager)
		{
			return new SpeechMenu(manager, this);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003F608 File Offset: 0x0003D808
		internal ArrayList GetSpeechPrompts(ActivityManager manager, SpeechMenuState state, CultureInfo culture, IPromptCounter counter)
		{
			ArrayList arrayList = null;
			switch (state)
			{
			case SpeechMenuState.Main:
				arrayList = this.GetPrompts(manager, "Main", culture, counter);
				break;
			case SpeechMenuState.Help:
				arrayList = this.GetPrompts(manager, "Help", culture, counter);
				break;
			case SpeechMenuState.Mumble1:
				arrayList = this.GetPrompts(manager, "Mumble1", culture, counter);
				break;
			case SpeechMenuState.Mumble2:
				arrayList = this.GetPrompts(manager, "Mumble2", culture, counter);
				break;
			case SpeechMenuState.Silence1:
				arrayList = this.GetPrompts(manager, "Silence1", culture, counter);
				break;
			case SpeechMenuState.Silence2:
				arrayList = this.GetPrompts(manager, "Silence2", culture, counter);
				break;
			case SpeechMenuState.SpeechError:
				arrayList = this.GetPrompts(manager, "SpeechError", culture, counter);
				break;
			case SpeechMenuState.InvalidCommand:
				arrayList = this.GetPrompts(manager, "InvalidCommand", culture, counter);
				if (arrayList.Count == 0)
				{
					GlobCfg.DefaultPrompts.InvalidCommand.AddPrompts(arrayList, manager, culture);
					arrayList.AddRange(this.GetPrompts(manager, "Main", culture, counter));
				}
				break;
			}
			return arrayList;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003F70C File Offset: 0x0003D90C
		internal virtual List<UMGrammar> GetGrammars(ActivityManager manager, CultureInfo culture)
		{
			List<UMGrammar> list = new List<UMGrammar>(this.grammars.Count);
			ExpressionParser.Expression expression = null;
			bool flag = true;
			foreach (UMGrammarConfig umgrammarConfig in this.grammars)
			{
				if (umgrammarConfig.Condition != expression)
				{
					expression = umgrammarConfig.Condition;
					object obj = (expression == null) ? true : expression.Eval(manager);
					flag = (obj != null && (bool)obj);
				}
				if (flag)
				{
					list.Add(umgrammarConfig.GetGrammar(manager, culture));
				}
			}
			return list;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0003F7B4 File Offset: 0x0003D9B4
		protected override void LoadChild(string name, XmlNode node)
		{
			if (string.Equals(name, "Transition", StringComparison.OrdinalIgnoreCase))
			{
				if (this.parseState != SpeechMenuConfig.ParseState.Transitions)
				{
					throw new FsmConfigurationException(Strings.InvalidParseState(base.ActivityId, name, this.parseState.ToString()));
				}
				base.LoadChild(name, node);
				return;
			}
			else if (string.Equals(name, "Prompt", StringComparison.OrdinalIgnoreCase))
			{
				if (this.parseState != SpeechMenuConfig.ParseState.Prompts)
				{
					throw new FsmConfigurationException(Strings.InvalidParseState(base.ActivityId, name, this.parseState.ToString()));
				}
				base.LoadChild(name, node);
				return;
			}
			else if (string.Equals(name, "Grammar", StringComparison.OrdinalIgnoreCase))
			{
				if (this.parseState != SpeechMenuConfig.ParseState.Grammars)
				{
					throw new FsmConfigurationException(Strings.InvalidParseState(base.ActivityId, name, this.parseState.ToString()));
				}
				this.grammars.Add(UMGrammarConfig.Create(node, base.ConditionScope, base.ManagerConfig));
				return;
			}
			else if (string.Equals(name, "Grammars", StringComparison.OrdinalIgnoreCase))
			{
				if (this.parseState != SpeechMenuConfig.ParseState.Root)
				{
					throw new FsmConfigurationException(Strings.InvalidParseState(base.ActivityId, name, this.parseState.ToString()));
				}
				this.parseState = SpeechMenuConfig.ParseState.Grammars;
				this.LoadChildren(node);
				this.parseState = SpeechMenuConfig.ParseState.Root;
				return;
			}
			else if (string.Equals(name, "Transitions", StringComparison.OrdinalIgnoreCase))
			{
				if (this.parseState != SpeechMenuConfig.ParseState.Root)
				{
					throw new FsmConfigurationException(Strings.InvalidParseState(base.ActivityId, name, this.parseState.ToString()));
				}
				this.parseState = SpeechMenuConfig.ParseState.Transitions;
				this.LoadChildren(node);
				this.parseState = SpeechMenuConfig.ParseState.Root;
				return;
			}
			else
			{
				if (!string.Equals(name, "Main", StringComparison.OrdinalIgnoreCase) && !string.Equals(name, "Help", StringComparison.OrdinalIgnoreCase) && !string.Equals(name, "Mumble1", StringComparison.OrdinalIgnoreCase) && !string.Equals(name, "Mumble2", StringComparison.OrdinalIgnoreCase) && !string.Equals(name, "Silence1", StringComparison.OrdinalIgnoreCase) && !string.Equals(name, "Silence2", StringComparison.OrdinalIgnoreCase) && !string.Equals(name, "SpeechError", StringComparison.OrdinalIgnoreCase) && !string.Equals(name, "InvalidCommand", StringComparison.OrdinalIgnoreCase) && !string.Equals(name, "PromptGroup", StringComparison.OrdinalIgnoreCase))
				{
					base.LoadChild(name, node);
					return;
				}
				if (this.parseState != SpeechMenuConfig.ParseState.Root)
				{
					throw new FsmConfigurationException(Strings.InvalidParseState(base.ActivityId, "Transition", this.parseState.ToString()));
				}
				string key = string.Equals(name, "PromptGroup", StringComparison.OrdinalIgnoreCase) ? node.Attributes["name"].Value : name;
				this.parseState = SpeechMenuConfig.ParseState.Prompts;
				base.Prompts = new ArrayList();
				base.PromptConfigGroups[key] = base.Prompts;
				this.LoadChildren(node);
				this.parseState = SpeechMenuConfig.ParseState.Root;
				return;
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003FA68 File Offset: 0x0003DC68
		protected override void LoadAttributes(XmlNode rootNode)
		{
			base.LoadAttributes(rootNode);
			XmlAttribute xmlAttribute = rootNode.Attributes["babbleSeconds"];
			XmlAttribute xmlAttribute2 = rootNode.Attributes["endSilenceSeconds"];
			XmlAttribute xmlAttribute3 = rootNode.Attributes["confidence"];
			this.babbleSeconds = ((xmlAttribute == null) ? 10 : int.Parse(xmlAttribute.Value, CultureInfo.InvariantCulture));
			this.confidence = ((xmlAttribute3 == null) ? 0.25f : float.Parse(xmlAttribute3.Value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003FAEC File Offset: 0x0003DCEC
		protected override void LoadComplete()
		{
			this.AddDefaultPrompts();
			this.ValidateTransitions();
			this.AddDefaultGrammars();
			byte maxDtmfSize = Math.Max(base.MaxDtmfSize, 1);
			base.MaxDtmfSize = maxDtmfSize;
			base.InvariantStopPatterns.Add("anyKey", true);
			base.LoadComplete();
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003FB38 File Offset: 0x0003DD38
		private void AddDefaultPrompts()
		{
			if (!base.PromptConfigGroups.ContainsKey("Main"))
			{
				throw new FsmConfigurationException(Strings.MissingMainPrompts(base.ActivityId));
			}
			if (!base.PromptConfigGroups.ContainsKey("Mumble1"))
			{
				base.PromptConfigGroups["Mumble1"] = new ArrayList();
				base.PromptConfigGroups["Mumble1"].Add(GlobCfg.DefaultPrompts.Mumble1);
				base.PromptConfigGroups["Mumble1"].AddRange(base.PromptConfigGroups["Main"]);
			}
			if (!base.PromptConfigGroups.ContainsKey("Mumble2"))
			{
				base.PromptConfigGroups["Mumble2"] = new ArrayList();
				base.PromptConfigGroups["Mumble2"].Add(GlobCfg.DefaultPrompts.Mumble2);
				base.PromptConfigGroups["Mumble2"].AddRange(base.PromptConfigGroups["Main"]);
			}
			if (!base.PromptConfigGroups.ContainsKey("Silence1"))
			{
				base.PromptConfigGroups["Silence1"] = new ArrayList();
				base.PromptConfigGroups["Silence1"].Add(GlobCfg.DefaultPrompts.Silence1);
				base.PromptConfigGroups["Silence1"].AddRange(base.PromptConfigGroups["Main"]);
			}
			if (!base.PromptConfigGroups.ContainsKey("Silence2"))
			{
				base.PromptConfigGroups["Silence2"] = new ArrayList();
				base.PromptConfigGroups["Silence2"].Add(GlobCfg.DefaultPrompts.Silence2);
				base.PromptConfigGroups["Silence2"].AddRange(base.PromptConfigGroups["Main"]);
			}
			if (!base.PromptConfigGroups.ContainsKey("SpeechError"))
			{
				base.PromptConfigGroups["SpeechError"] = new ArrayList();
				base.PromptConfigGroups["SpeechError"].Add(GlobCfg.DefaultPrompts.SpeechError);
			}
			if (!base.PromptConfigGroups.ContainsKey("InvalidCommand"))
			{
				base.PromptConfigGroups["InvalidCommand"] = new ArrayList();
				base.PromptConfigGroups["InvalidCommand"].Add(GlobCfg.DefaultPrompts.InvalidCommand);
				base.PromptConfigGroups["InvalidCommand"].AddRange(base.PromptConfigGroups["Main"]);
			}
			if (!base.PromptConfigGroups.ContainsKey("Help"))
			{
				base.PromptConfigGroups["Help"] = new ArrayList();
				base.PromptConfigGroups["Help"].AddRange(base.PromptConfigGroups["Main"]);
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003FE1C File Offset: 0x0003E01C
		private void ValidateTransitions()
		{
			if (!ActivityConfig.TransitionMap.ContainsKey(ActivityConfig.BuildTransitionMapKey(this, "dtmfFallback")))
			{
				throw new FsmConfigurationException(Strings.MissingRequiredTransition(base.ActivityId, "dtmfFallback"));
			}
			if (!ActivityConfig.TransitionMap.ContainsKey(ActivityConfig.BuildTransitionMapKey(this, "dtmfFallback")))
			{
				throw new FsmConfigurationException(Strings.MissingRequiredTransition(base.ActivityId, "recoMainMenu"));
			}
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003FE90 File Offset: 0x0003E090
		private void AddDefaultGrammars()
		{
			this.grammars.Add(GlobCfg.DefaultGrammars.Goodbye);
			this.grammars.Add(GlobCfg.DefaultGrammars.Help);
			this.grammars.Add(GlobCfg.DefaultGrammars.Repeat);
			this.grammars.Add(GlobCfg.DefaultGrammars.MainMenuShortcut);
		}

		// Token: 0x04000AC9 RID: 2761
		private int babbleSeconds;

		// Token: 0x04000ACA RID: 2762
		private float confidence;

		// Token: 0x04000ACB RID: 2763
		private List<UMGrammarConfig> grammars;

		// Token: 0x04000ACC RID: 2764
		private SpeechMenuConfig.ParseState parseState;

		// Token: 0x020001E1 RID: 481
		private enum ParseState
		{
			// Token: 0x04000ACE RID: 2766
			Root,
			// Token: 0x04000ACF RID: 2767
			Prompts,
			// Token: 0x04000AD0 RID: 2768
			Grammars,
			// Token: 0x04000AD1 RID: 2769
			Transitions
		}
	}
}
