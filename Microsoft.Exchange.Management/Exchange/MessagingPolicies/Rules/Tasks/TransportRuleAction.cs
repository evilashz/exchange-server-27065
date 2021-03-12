using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B7B RID: 2939
	[Serializable]
	public abstract class TransportRuleAction : RulePhrase
	{
		// Token: 0x06006EDC RID: 28380 RVA: 0x001C7507 File Offset: 0x001C5707
		internal static TypeMapping[] GetAvailableActionMappings()
		{
			if (!Utils.IsEdgeRoleInstalled())
			{
				return TransportRuleAction.BridgeheadMappings;
			}
			return TransportRuleAction.EdgeMappings;
		}

		// Token: 0x06006EDD RID: 28381 RVA: 0x001C751C File Offset: 0x001C571C
		internal static TransportRuleAction CreateAction(TypeMapping[] mappings, string name, IConfigDataProvider session)
		{
			foreach (TypeMapping typeMapping in mappings)
			{
				if (name.Equals(typeMapping.Name, StringComparison.OrdinalIgnoreCase))
				{
					return TransportRuleAction.InternalCreateAction(typeMapping.Type, mappings, session);
				}
			}
			return null;
		}

		// Token: 0x06006EDE RID: 28382 RVA: 0x001C7560 File Offset: 0x001C5760
		internal static TransportRuleAction[] CreateAllAvailableActions(TypeMapping[] mappings, IConfigDataProvider session)
		{
			TransportRuleAction[] array = new TransportRuleAction[mappings.Length];
			for (int i = 0; i < mappings.Length; i++)
			{
				array[i] = TransportRuleAction.InternalCreateAction(mappings[i].Type, mappings, session);
			}
			return array;
		}

		// Token: 0x06006EDF RID: 28383 RVA: 0x001C7598 File Offset: 0x001C5798
		internal static bool TryCreateFromInternalAction(TypeMapping[] mappings, ShortList<Action> actions, ref int index, out TransportRuleAction taskAction, IConfigDataProvider session)
		{
			foreach (TypeMapping typeMapping in mappings)
			{
				MethodInfo method = typeMapping.Type.GetMethod("CreateFromInternalActionWithSession", BindingFlags.Static | BindingFlags.NonPublic);
				MethodInfo method2 = typeMapping.Type.GetMethod("CreateFromInternalAction", BindingFlags.Static | BindingFlags.NonPublic);
				MethodInfo method3 = typeMapping.Type.GetMethod("CreateFromInternalActions", BindingFlags.Static | BindingFlags.NonPublic);
				if (method != null)
				{
					TransportRuleAction transportRuleAction = (TransportRuleAction)method.Invoke(null, new object[]
					{
						actions[index],
						session
					});
					if (transportRuleAction != null)
					{
						index++;
						taskAction = transportRuleAction;
						taskAction.Initialize(mappings);
						return true;
					}
				}
				else if (method2 != null)
				{
					TransportRuleAction transportRuleAction = (TransportRuleAction)method2.Invoke(null, new object[]
					{
						actions[index]
					});
					if (transportRuleAction != null)
					{
						index++;
						taskAction = transportRuleAction;
						taskAction.Initialize(mappings);
						return true;
					}
				}
				else if (method3 != null)
				{
					object[] array = new object[]
					{
						actions,
						index
					};
					TransportRuleAction transportRuleAction = (TransportRuleAction)method3.Invoke(null, array);
					index = (int)array[1];
					if (transportRuleAction != null)
					{
						taskAction = transportRuleAction;
						taskAction.Initialize(mappings);
						return true;
					}
				}
			}
			taskAction = null;
			return false;
		}

		// Token: 0x06006EE0 RID: 28384 RVA: 0x001C76F4 File Offset: 0x001C58F4
		internal static string GetStringValue(Argument argument)
		{
			Value value = argument as Value;
			if (value != null)
			{
				return (string)value.ParsedValue;
			}
			throw new InvalidOperationException("Only value argument should be used here.");
		}

		// Token: 0x06006EE1 RID: 28385 RVA: 0x001C7724 File Offset: 0x001C5924
		internal void Initialize(TypeMapping[] mappings)
		{
			Type type = base.GetType();
			for (int i = 0; i < mappings.Length; i++)
			{
				TypeMapping typeMapping = mappings[i];
				if (type == typeMapping.Type)
				{
					base.SetReadOnlyProperties(typeMapping.Name, i, typeMapping.LinkedDisplayText);
					return;
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006EE2 RID: 28386 RVA: 0x001C7778 File Offset: 0x001C5978
		internal virtual Action[] ToInternalActions()
		{
			return new Action[]
			{
				this.ToInternalAction()
			};
		}

		// Token: 0x06006EE3 RID: 28387 RVA: 0x001C7796 File Offset: 0x001C5996
		internal virtual Action ToInternalAction()
		{
			return null;
		}

		// Token: 0x06006EE4 RID: 28388 RVA: 0x001C7799 File Offset: 0x001C5999
		internal virtual string ToCmdletParameter()
		{
			return string.Format("-{0} {1}", this.GetParameterName(), this.GetActionParameters()).Trim();
		}

		// Token: 0x06006EE5 RID: 28389 RVA: 0x001C77B6 File Offset: 0x001C59B6
		internal virtual string GetActionParameters()
		{
			return string.Empty;
		}

		// Token: 0x06006EE6 RID: 28390 RVA: 0x001C77C0 File Offset: 0x001C59C0
		private static TransportRuleAction InternalCreateAction(Type actionType, TypeMapping[] mappings, IConfigDataProvider session)
		{
			ConstructorInfo constructor = actionType.GetConstructor(new Type[]
			{
				typeof(IConfigDataProvider)
			});
			TransportRuleAction transportRuleAction;
			if (constructor != null)
			{
				transportRuleAction = (TransportRuleAction)constructor.Invoke(new object[]
				{
					session
				});
			}
			else
			{
				transportRuleAction = (TransportRuleAction)Activator.CreateInstance(actionType);
			}
			transportRuleAction.Initialize(mappings);
			return transportRuleAction;
		}

		// Token: 0x06006EE7 RID: 28391 RVA: 0x001C7820 File Offset: 0x001C5A20
		protected string GetParameterName()
		{
			IEnumerable<ActionParameterName> source = (IEnumerable<ActionParameterName>)base.GetType().GetCustomAttributes(typeof(ActionParameterName), true);
			if (source.Any<ActionParameterName>())
			{
				return source.First<ActionParameterName>().Name;
			}
			PropertyInfo[] properties = base.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				IEnumerable<ActionParameterName> source2 = (IEnumerable<ActionParameterName>)propertyInfo.GetCustomAttributes(typeof(ActionParameterName), true);
				if (source2.Any<ActionParameterName>())
				{
					return source2.First<ActionParameterName>().Name;
				}
			}
			return string.Empty;
		}

		// Token: 0x06006EE8 RID: 28392 RVA: 0x001C78BB File Offset: 0x001C5ABB
		internal virtual void SuppressPiiData()
		{
		}

		// Token: 0x0400399D RID: 14749
		internal static TypeMapping[] BridgeheadMappings = new TypeMapping[]
		{
			new TypeMapping("RouteMessageOutboundConnector", typeof(RouteMessageOutboundConnectorAction), RulesTasksStrings.LinkedActionRouteMessageOutboundConnector),
			new TypeMapping("RouteMessageOutboundRequireTls", typeof(RouteMessageOutboundRequireTlsAction), RulesTasksStrings.LinkedActionRouteMessageOutboundRequireTls),
			new TypeMapping("PrependSubject", typeof(PrependSubjectAction), RulesTasksStrings.LinkedActionPrependSubject),
			new TypeMapping("SetAuditSeverity", typeof(SetAuditSeverityAction), RulesTasksStrings.LinkedActionSetAuditSeverity),
			new TypeMapping("ApplyClassification", typeof(ApplyClassificationAction), RulesTasksStrings.LinkedActionApplyClassification),
			new TypeMapping("ApplyHtmlDisclaimer", typeof(ApplyHtmlDisclaimerAction), RulesTasksStrings.LinkedActionApplyHtmlDisclaimer),
			new TypeMapping("RightsProtectMessage", typeof(RightsProtectMessageAction), RulesTasksStrings.LinkedActionRightsProtectMessage),
			new TypeMapping("SetSCL", typeof(SetSclAction), RulesTasksStrings.LinkedActionSetScl),
			new TypeMapping("SetHeader", typeof(SetHeaderAction), RulesTasksStrings.LinkedActionSetHeader),
			new TypeMapping("RemoveHeader", typeof(RemoveHeaderAction), RulesTasksStrings.LinkedActionRemoveHeader),
			new TypeMapping("ApplyOME", typeof(ApplyOMEAction), RulesTasksStrings.LinkedActionApplyOME),
			new TypeMapping("RemoveOME", typeof(RemoveOMEAction), RulesTasksStrings.LinkedActionRemoveOME),
			new TypeMapping("GenerateNotification", typeof(GenerateNotificationAction), RulesTasksStrings.LinkedActionGenerateNotificationAction),
			new TypeMapping("AddToRecipient", typeof(AddToRecipientAction), RulesTasksStrings.LinkedActionAddToRecipient),
			new TypeMapping("CopyTo", typeof(CopyToAction), RulesTasksStrings.LinkedActionCopyTo),
			new TypeMapping("BlindCopyTo", typeof(BlindCopyToAction), RulesTasksStrings.LinkedActionBlindCopyTo),
			new TypeMapping("AddManagerAsRecipientType", typeof(AddManagerAsRecipientTypeAction), RulesTasksStrings.LinkedActionAddManagerAsRecipientType),
			new TypeMapping("NotifySender", typeof(NotifySenderAction), RulesTasksStrings.LinkedActionNotifySenderAction),
			new TypeMapping("ModerateMessageByUser", typeof(ModerateMessageByUserAction), RulesTasksStrings.LinkedActionModerateMessageByUser),
			new TypeMapping("ModerateMessageByManager", typeof(ModerateMessageByManagerAction), RulesTasksStrings.LinkedActionModerateMessageByManager),
			new TypeMapping("RedirectMessage", typeof(RedirectMessageAction), RulesTasksStrings.LinkedActionRedirectMessage),
			new TypeMapping("Quarantine", typeof(QuarantineAction), RulesTasksStrings.LinkedActionQuarantine),
			new TypeMapping("RejectMessage", typeof(RejectMessageAction), RulesTasksStrings.LinkedActionRejectMessage),
			new TypeMapping("DeleteMessage", typeof(DeleteMessageAction), RulesTasksStrings.LinkedActionDeleteMessage),
			new TypeMapping("StopRuleProcessing", typeof(StopRuleProcessingAction), RulesTasksStrings.LinkedActionStopRuleProcessing),
			new TypeMapping("GenerateIncidentReport", typeof(GenerateIncidentReportAction), RulesTasksStrings.LinkedActionGenerateIncidentReportAction)
		};

		// Token: 0x0400399E RID: 14750
		internal static TypeMapping[] EdgeMappings = new TypeMapping[]
		{
			new TypeMapping("LogEvent", typeof(LogEventAction), RulesTasksStrings.LinkedActionLogEvent),
			new TypeMapping("PrependSubject", typeof(PrependSubjectAction), RulesTasksStrings.LinkedActionPrependSubject),
			new TypeMapping("SetSCL", typeof(SetSclAction), RulesTasksStrings.LinkedActionSetScl),
			new TypeMapping("SetHeader", typeof(SetHeaderAction), RulesTasksStrings.LinkedActionSetHeader),
			new TypeMapping("RemoveHeader", typeof(RemoveHeaderAction), RulesTasksStrings.LinkedActionRemoveHeader),
			new TypeMapping("AddToRecipient", typeof(AddToRecipientAction), RulesTasksStrings.LinkedActionAddToRecipient),
			new TypeMapping("CopyTo", typeof(CopyToAction), RulesTasksStrings.LinkedActionCopyTo),
			new TypeMapping("BlindCopyTo", typeof(BlindCopyToAction), RulesTasksStrings.LinkedActionBlindCopyTo),
			new TypeMapping("Disconnect", typeof(DisconnectAction), RulesTasksStrings.LinkedActionDisconnect),
			new TypeMapping("RedirectMessage", typeof(RedirectMessageAction), RulesTasksStrings.LinkedActionRedirectMessage),
			new TypeMapping("Quarantine", typeof(QuarantineAction), RulesTasksStrings.LinkedActionQuarantine),
			new TypeMapping("SmtpRejectMessage", typeof(SmtpRejectMessageAction), RulesTasksStrings.LinkedActionSmtpRejectMessage),
			new TypeMapping("DeleteMessage", typeof(DeleteMessageAction), RulesTasksStrings.LinkedActionDeleteMessage),
			new TypeMapping("StopRuleProcessing", typeof(StopRuleProcessingAction), RulesTasksStrings.LinkedActionStopRuleProcessing)
		};
	}
}
