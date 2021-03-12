using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000175 RID: 373
	internal class MenuConfig : ActivityConfig
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x0002FD49 File Offset: 0x0002DF49
		internal MenuConfig(ActivityManagerConfig manager) : base(manager)
		{
			this.promptConfigGroups = new Dictionary<string, ArrayList>();
			base.Prompts = new ArrayList();
			this.promptConfigGroups.Add("Main", base.Prompts);
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0002FD7E File Offset: 0x0002DF7E
		internal int InputTimeout
		{
			get
			{
				return this.inputTimeout;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0002FD86 File Offset: 0x0002DF86
		internal int InitialSilenceTimeout
		{
			get
			{
				return this.initialSilenceTimeout;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0002FD8E File Offset: 0x0002DF8E
		internal int InterDigitTimeout
		{
			get
			{
				return this.interDigitTimeout;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0002FD96 File Offset: 0x0002DF96
		internal byte MinDtmfSize
		{
			get
			{
				return MenuConfig.DtmfSizeToByte(this.minDtmfSize);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0002FDA3 File Offset: 0x0002DFA3
		internal uint MaxNumeric
		{
			get
			{
				return this.maxNumeric;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0002FDAB File Offset: 0x0002DFAB
		internal uint MinNumeric
		{
			get
			{
				return this.minNumeric;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002FDB3 File Offset: 0x0002DFB3
		internal string DtmfInputValue
		{
			get
			{
				return this.dtmfInputValue;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0002FDBB File Offset: 0x0002DFBB
		internal string DtmfStopTones
		{
			get
			{
				return this.dtmfStopTones;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002FDC3 File Offset: 0x0002DFC3
		internal bool Uninterruptible
		{
			get
			{
				return this.uninterruptible;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002FDCB File Offset: 0x0002DFCB
		internal bool StopPromptOnBargeIn
		{
			get
			{
				return this.stopPromptOnBargeIn;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002FDD3 File Offset: 0x0002DFD3
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0002FDE0 File Offset: 0x0002DFE0
		protected internal byte MaxDtmfSize
		{
			get
			{
				return MenuConfig.DtmfSizeToByte(this.maxDtmfSize);
			}
			protected set
			{
				this.maxDtmfSize = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0002FDF4 File Offset: 0x0002DFF4
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x0002FDFC File Offset: 0x0002DFFC
		public bool KeepDtmfOnNoMatch { get; private set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0002FE05 File Offset: 0x0002E005
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0002FE0D File Offset: 0x0002E00D
		protected Dictionary<string, ArrayList> PromptConfigGroups
		{
			get
			{
				return this.promptConfigGroups;
			}
			set
			{
				this.promptConfigGroups = value;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002FE16 File Offset: 0x0002E016
		internal override ActivityBase CreateActivity(ActivityManager manager)
		{
			return new Menu(manager, this);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002FE20 File Offset: 0x0002E020
		internal virtual ArrayList GetPrompts(ActivityManager manager, string promptGroupName, CultureInfo culture, IPromptCounter counter)
		{
			ArrayList promptConfigArray = this.promptConfigGroups[promptGroupName];
			return PromptConfigBase.BuildConditionalPrompts(promptConfigArray, manager, culture, counter);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002FE44 File Offset: 0x0002E044
		protected override void LoadAttributes(XmlNode rootNode)
		{
			base.LoadAttributes(rootNode);
			this.maxDtmfSize = rootNode.Attributes["maxDtmfSize"].Value;
			this.minDtmfSize = rootNode.Attributes["minDtmfSize"].Value;
			this.dtmfInputValue = string.Intern(rootNode.Attributes["dtmfInputValue"].Value);
			this.dtmfStopTones = rootNode.Attributes["dtmfStopTones"].Value;
			this.maxNumeric = uint.Parse(rootNode.Attributes["maxNumericInput"].Value, CultureInfo.InvariantCulture);
			this.minNumeric = uint.Parse(rootNode.Attributes["minNumericInput"].Value, CultureInfo.InvariantCulture);
			this.uninterruptible = bool.Parse(rootNode.Attributes["uninterruptible"].Value);
			this.stopPromptOnBargeIn = bool.Parse(rootNode.Attributes["stopPromptOnBargeIn"].Value);
			this.KeepDtmfOnNoMatch = bool.Parse(rootNode.Attributes["keepDtmfOnNoMatch"].Value);
			if (rootNode.Attributes["interDigitTimeout"] != null)
			{
				this.interDigitTimeout = int.Parse(rootNode.Attributes["interDigitTimeout"].Value, CultureInfo.InvariantCulture);
			}
			else
			{
				this.interDigitTimeout = 1;
			}
			if (rootNode.Attributes["inputTimeout"] != null)
			{
				this.inputTimeout = int.Parse(rootNode.Attributes["inputTimeout"].Value, CultureInfo.InvariantCulture);
			}
			else
			{
				this.inputTimeout = 10;
			}
			if (rootNode.Attributes["initialSilenceTimeout"] != null)
			{
				this.initialSilenceTimeout = int.Parse(rootNode.Attributes["initialSilenceTimeout"].Value, CultureInfo.InvariantCulture);
			}
			else
			{
				this.initialSilenceTimeout = 6;
			}
			if (this.initialSilenceTimeout > this.inputTimeout)
			{
				this.initialSilenceTimeout = this.inputTimeout;
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00030050 File Offset: 0x0002E250
		protected override void LoadChild(string name, XmlNode node)
		{
			if (string.Equals(name, "Prompt", StringComparison.OrdinalIgnoreCase))
			{
				string a = string.Intern(node.Attributes["type"].Value);
				if (string.Equals(a, "group", StringComparison.OrdinalIgnoreCase))
				{
					string value = node.Attributes["name"].Value;
					using (IEnumerator enumerator = this.promptConfigGroups[value].GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							PromptConfigBase promptConfigBase = (PromptConfigBase)obj;
							PromptConfigBase promptConfigBase2 = (PromptConfigBase)promptConfigBase.Clone();
							string scope = ActivityConfig.BuildCondition(base.ConditionScope, node);
							promptConfigBase2.SetConditionString(ActivityConfig.BuildCondition(scope, promptConfigBase.GetConditionString()), base.ManagerConfig);
							base.Prompts.Add(promptConfigBase2);
						}
						return;
					}
				}
				base.Prompts.Add(PromptConfigBase.Create(node, base.ConditionScope, base.ManagerConfig));
				return;
			}
			if (string.Equals(name, "PromptGroup", StringComparison.OrdinalIgnoreCase))
			{
				string value2 = node.Attributes["name"].Value;
				ArrayList prompts = base.Prompts;
				base.Prompts = new ArrayList();
				this.promptConfigGroups.Add(value2, base.Prompts);
				this.LoadChildren(node);
				base.Prompts = prompts;
				return;
			}
			base.LoadChild(name, node);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000301C8 File Offset: 0x0002E3C8
		protected override void LoadComplete()
		{
			if (this.inputTimeout < this.interDigitTimeout)
			{
				throw new FsmConfigurationException(Strings.InputTimeoutLessThanInterdigit(base.ActivityId));
			}
			if (this.MinDtmfSize > this.MaxDtmfSize)
			{
				throw new FsmConfigurationException(Strings.MinDtmfGreaterThanMax(base.ActivityId));
			}
			if (this.minNumeric > this.maxNumeric)
			{
				throw new FsmConfigurationException(Strings.MinNumericGreaterThanMax(base.ActivityId));
			}
			if (this.MinDtmfSize == 0 && !ActivityConfig.TransitionMap.ContainsKey(ActivityConfig.BuildTransitionMapKey(this, "noKey")))
			{
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, "Activity id={0} has minDtmfSize=0 without a NoKey Transition.", new object[]
				{
					base.ActivityId
				});
				throw new FsmConfigurationException(Strings.MinDtmfZeroWithoutNoKey(base.ActivityId));
			}
			if (this.MinDtmfSize != 0 && ActivityConfig.TransitionMap.ContainsKey(ActivityConfig.BuildTransitionMapKey(this, "noKey")))
			{
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, "Activity id={0} has minDtmfSize != 0 with a NoKey Transition.", new object[]
				{
					base.ActivityId
				});
				throw new FsmConfigurationException(Strings.MinDtmfNotZeroWithNoKey(base.ActivityId));
			}
			base.LoadComplete();
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000302F4 File Offset: 0x0002E4F4
		private static byte DtmfSizeToByte(string dtmfSize)
		{
			if (dtmfSize != null)
			{
				if (dtmfSize == "extension")
				{
					return 16;
				}
				if (dtmfSize == "password")
				{
					return 25;
				}
				if (dtmfSize == "name")
				{
					return 76;
				}
			}
			return byte.Parse(dtmfSize, CultureInfo.InvariantCulture);
		}

		// Token: 0x04000994 RID: 2452
		private uint maxNumeric;

		// Token: 0x04000995 RID: 2453
		private uint minNumeric;

		// Token: 0x04000996 RID: 2454
		private string maxDtmfSize;

		// Token: 0x04000997 RID: 2455
		private string minDtmfSize;

		// Token: 0x04000998 RID: 2456
		private string dtmfInputValue;

		// Token: 0x04000999 RID: 2457
		private string dtmfStopTones;

		// Token: 0x0400099A RID: 2458
		private int interDigitTimeout;

		// Token: 0x0400099B RID: 2459
		private int inputTimeout;

		// Token: 0x0400099C RID: 2460
		private int initialSilenceTimeout;

		// Token: 0x0400099D RID: 2461
		private bool uninterruptible;

		// Token: 0x0400099E RID: 2462
		private bool stopPromptOnBargeIn;

		// Token: 0x0400099F RID: 2463
		private Dictionary<string, ArrayList> promptConfigGroups;
	}
}
