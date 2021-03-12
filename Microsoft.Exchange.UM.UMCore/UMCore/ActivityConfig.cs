using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000008 RID: 8
	internal abstract class ActivityConfig
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00003095 File Offset: 0x00001295
		internal ActivityConfig(ActivityManagerConfig configManager)
		{
			this.manager = configManager;
			this.prompts = new ArrayList(16);
			this.conditionalStopPatterns = new StopPatterns();
			this.invariantStopPatterns = new StopPatterns();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000030C7 File Offset: 0x000012C7
		private ActivityConfig()
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000030CF File Offset: 0x000012CF
		internal string ActivityId
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000030D7 File Offset: 0x000012D7
		internal string UniqueId
		{
			get
			{
				return this.uniqueId;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000030DF File Offset: 0x000012DF
		internal ActivityManagerConfig ManagerConfig
		{
			get
			{
				return this.manager;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000030E7 File Offset: 0x000012E7
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000030EE File Offset: 0x000012EE
		protected static Dictionary<string, List<TransitionBase>> TransitionMap
		{
			get
			{
				return ActivityConfig.transitionMap;
			}
			set
			{
				ActivityConfig.transitionMap = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000030F6 File Offset: 0x000012F6
		protected StopPatterns InvariantStopPatterns
		{
			get
			{
				return this.invariantStopPatterns;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000030FE File Offset: 0x000012FE
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003106 File Offset: 0x00001306
		protected ArrayList Prompts
		{
			get
			{
				return this.prompts;
			}
			set
			{
				this.prompts = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000310F File Offset: 0x0000130F
		protected string ConditionScope
		{
			get
			{
				if (this.conditionScope == null)
				{
					return string.Empty;
				}
				return this.conditionScope;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003125 File Offset: 0x00001325
		private ArrayList ConditionStack
		{
			get
			{
				if (this.conditionStack == null)
				{
					this.conditionStack = new ArrayList();
				}
				return this.conditionStack;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003140 File Offset: 0x00001340
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type: {0}, ID: {1}", new object[]
			{
				base.GetType().ToString(),
				this.ActivityId
			});
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000317C File Offset: 0x0000137C
		internal static string BuildCondition(string scope, XmlNode localNode)
		{
			string localCondition = string.Empty;
			if (localNode.Attributes["condition"] != null)
			{
				localCondition = localNode.Attributes["condition"].Value.Trim();
			}
			return ActivityConfig.BuildCondition(scope, localCondition);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031C4 File Offset: 0x000013C4
		internal static string BuildCondition(string scope, string localCondition)
		{
			string text;
			if (scope.Length == 0)
			{
				text = localCondition;
			}
			else if (localCondition.Length == 0)
			{
				text = scope;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(scope);
				stringBuilder.Append(" ");
				stringBuilder.Append(ConditionParser.AndOperator);
				stringBuilder.Append(" ");
				stringBuilder.Append("(");
				stringBuilder.Append(localCondition);
				stringBuilder.Append(")");
				text = stringBuilder.ToString();
			}
			return text.Trim();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000324C File Offset: 0x0000144C
		internal StopPatterns ComputeStopPatterns(ActivityManager m)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Computing stop patterns for Activity {0}.", new object[]
			{
				this
			});
			StopPatterns stopPatterns = null;
			foreach (string text in this.conditionalStopPatterns)
			{
				TransitionBase transition = this.GetTransition(text, m);
				if (transition != null)
				{
					if (stopPatterns == null)
					{
						stopPatterns = new StopPatterns(this.conditionalStopPatterns.Count + this.invariantStopPatterns.Count);
					}
					stopPatterns.Add(text, transition.BargeIn);
				}
			}
			if (stopPatterns == null)
			{
				return this.invariantStopPatterns;
			}
			stopPatterns.Add(this.invariantStopPatterns);
			return stopPatterns;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003308 File Offset: 0x00001508
		internal virtual void Load(XmlNode rootNode)
		{
			this.LoadAttributes(rootNode);
			this.LoadChildren(rootNode);
			this.LoadComplete();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003320 File Offset: 0x00001520
		internal virtual TransitionBase GetTransition(string rawInput, ActivityManager manager)
		{
			PIIMessage data = PIIMessage.Create(PIIType._PII, rawInput);
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "Getting transition for input _PII in Activity {0}.", new object[]
			{
				this
			});
			TransitionBase result = null;
			if (rawInput == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Null input in ActivityConfig.GetTransition... returning null.", new object[0]);
				result = null;
			}
			else if (string.Equals(rawInput, "stopEvent", StringComparison.OrdinalIgnoreCase))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Stop transition found in activityconfig.GetTransition().", new object[0]);
				result = new StopTransition();
			}
			else
			{
				string key = ActivityConfig.BuildTransitionMapKey(this, rawInput);
				List<TransitionBase> list;
				if (ActivityConfig.TransitionMap.TryGetValue(key, out list))
				{
					for (int i = 0; i < list.Count; i++)
					{
						TransitionBase transitionBase = list[i];
						ExpressionParser.Expression condition = transitionBase.Condition;
						object obj;
						if (condition == null || ((obj = condition.Eval(manager)) is bool && (bool)obj))
						{
							result = transitionBase;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600006F RID: 111
		internal abstract ActivityBase CreateActivity(ActivityManager manager);

		// Token: 0x06000070 RID: 112 RVA: 0x0000340A File Offset: 0x0000160A
		protected static string BuildTransitionMapKey(ActivityConfig activity, string eventName)
		{
			return activity.UniqueId + "::" + eventName;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000341D File Offset: 0x0000161D
		protected void PushCondition(string condition)
		{
			this.ConditionStack.Add(condition);
			this.RebuildConditionScope();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003432 File Offset: 0x00001632
		protected void PopCondition()
		{
			this.ConditionStack.RemoveAt(this.ConditionStack.Count - 1);
			this.RebuildConditionScope();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003454 File Offset: 0x00001654
		protected void ParseTransitionNode(XmlNode nodeCurrent, ActivityManagerConfig managerConfig)
		{
			string text = string.Intern(nodeCurrent.Attributes["event"].Value);
			string text2 = string.Intern(nodeCurrent.Attributes["refId"].Value);
			string action = string.Intern(nodeCurrent.Attributes["action"].Value);
			bool heavy = false;
			if (nodeCurrent.Attributes["heavyaction"] != null)
			{
				heavy = bool.Parse(nodeCurrent.Attributes["heavyaction"].Value);
			}
			bool playback = false;
			if (nodeCurrent.Attributes["playbackAction"] != null)
			{
				playback = bool.Parse(nodeCurrent.Attributes["playbackAction"].Value);
			}
			string refInfo = null;
			if (nodeCurrent.Attributes["refInfo"] != null)
			{
				refInfo = string.Intern(nodeCurrent.Attributes["refInfo"].Value);
			}
			FsmAction action2 = this.ParseAction(action);
			if (!ConstantValidator.Instance.ValidateEvent(text))
			{
				throw new FsmConfigurationException(Strings.InvalidEvent(text));
			}
			TransitionBase transitionBase = null;
			try
			{
				string text3 = ActivityConfig.BuildCondition(this.ConditionScope, nodeCurrent);
				ExpressionParser.Expression condition = null;
				if (text3.Length > 0)
				{
					condition = ConditionParser.Instance.Parse(text3, managerConfig);
				}
				transitionBase = TransitionBase.Create(action2, text2, text, heavy, playback, managerConfig, condition, refInfo);
			}
			catch (FsmConfigurationException innerException)
			{
				throw new FsmConfigurationException(Strings.UnknownTransitionId(text2, this.ActivityId), innerException);
			}
			string key = ActivityConfig.BuildTransitionMapKey(this, text);
			List<TransitionBase> list;
			if (!ActivityConfig.TransitionMap.TryGetValue(key, out list))
			{
				list = new List<TransitionBase>();
				ActivityConfig.TransitionMap[key] = list;
				if (transitionBase.Condition != null)
				{
					this.conditionalStopPatterns.Add(text, transitionBase.BargeIn);
				}
				else
				{
					this.invariantStopPatterns.Add(text, transitionBase.BargeIn);
				}
			}
			else
			{
				for (int i = 0; i < list.Count; i++)
				{
					TransitionBase transitionBase2 = list[i];
					if (transitionBase2.Condition == transitionBase.Condition)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Duplicate event transition: {0}.", new object[]
						{
							text
						});
						throw new FsmConfigurationException(Strings.DuplicateTransition(this.ActivityId, text));
					}
				}
			}
			list.Add(transitionBase);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000036A8 File Offset: 0x000018A8
		protected virtual void ParseConditionNode(XmlNode nodeCurrent)
		{
			string condition = string.Intern("(" + nodeCurrent.Attributes["value"].Value + ")");
			this.PushCondition(condition);
			this.LoadChildren(nodeCurrent);
			this.PopCondition();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000036F4 File Offset: 0x000018F4
		protected virtual void LoadChildren(XmlNode rootNode)
		{
			foreach (object obj in rootNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name = string.Intern(xmlNode.Name);
				this.LoadChild(name, xmlNode);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000375C File Offset: 0x0000195C
		protected virtual void LoadAttributes(XmlNode rootNode)
		{
			this.activityId = string.Intern(rootNode.Attributes["id"].Value);
			if (this.ManagerConfig != null)
			{
				this.uniqueId = this.ManagerConfig.UniqueId + this.ActivityId;
				return;
			}
			this.uniqueId = this.ActivityId;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000037BA File Offset: 0x000019BA
		protected virtual void LoadChild(string name, XmlNode node)
		{
			if (string.Equals(name, "Transition", StringComparison.OrdinalIgnoreCase))
			{
				this.ParseTransitionNode(node, this.manager);
				return;
			}
			if (string.Equals(name, "Condition", StringComparison.OrdinalIgnoreCase))
			{
				this.ParseConditionNode(node);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000037ED File Offset: 0x000019ED
		protected virtual void LoadComplete()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000037F0 File Offset: 0x000019F0
		private void RebuildConditionScope()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.conditionStack.Count; i++)
			{
				stringBuilder.Append((string)this.conditionStack[i]);
				stringBuilder.Append(" ");
				if (i < this.conditionStack.Count - 1)
				{
					stringBuilder.Append(ConditionParser.AndOperator);
					stringBuilder.Append(" ");
				}
			}
			this.conditionScope = stringBuilder.ToString();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003874 File Offset: 0x00001A74
		private FsmAction ParseAction(string action)
		{
			if (string.Compare("null", action, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return null;
			}
			ActivityManagerConfig activityManagerConfig = this as ActivityManagerConfig;
			if (activityManagerConfig == null)
			{
				activityManagerConfig = this.ManagerConfig;
			}
			QualifiedName actionName = new QualifiedName(action, activityManagerConfig.ClassName);
			return FsmAction.Create(actionName, activityManagerConfig);
		}

		// Token: 0x04000012 RID: 18
		private static Dictionary<string, List<TransitionBase>> transitionMap = new Dictionary<string, List<TransitionBase>>();

		// Token: 0x04000013 RID: 19
		private string activityId;

		// Token: 0x04000014 RID: 20
		private string uniqueId;

		// Token: 0x04000015 RID: 21
		private ArrayList prompts;

		// Token: 0x04000016 RID: 22
		private ActivityManagerConfig manager;

		// Token: 0x04000017 RID: 23
		private ArrayList conditionStack;

		// Token: 0x04000018 RID: 24
		private string conditionScope;

		// Token: 0x04000019 RID: 25
		private StopPatterns conditionalStopPatterns;

		// Token: 0x0400001A RID: 26
		private StopPatterns invariantStopPatterns;
	}
}
