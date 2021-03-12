using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000080 RID: 128
	internal sealed class RouteMessageOutboundRequireTls : TransportAction
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00015202 File Offset: 0x00013402
		public RouteMessageOutboundRequireTls(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001520B File Offset: 0x0001340B
		public override Version MinimumVersion
		{
			get
			{
				return RouteMessageOutboundConnector.ConditionBasedRoutingBaseVersion;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00015212 File Offset: 0x00013412
		public override string Name
		{
			get
			{
				return "RouteMessageOutboundRequireTls";
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00015219 File Offset: 0x00013419
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.RecipientRelated;
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001521C File Offset: 0x0001341C
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext context = (TransportRulesEvaluationContext)baseContext;
			return RouteMessageOutboundRequireTls.RequireOutboundTls(context);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00015258 File Offset: 0x00013458
		internal static ExecutionControl RequireOutboundTls(TransportRulesEvaluationContext context)
		{
			if (context.EventType == EventType.EndOfData || context.EventType == EventType.OnRoutedMessage)
			{
				return ExecutionControl.Execute;
			}
			if (context.OnResolvedSource == null)
			{
				throw new RuleInvalidOperationException("Routing actions can only be called at OnResolvedMessage");
			}
			ExTraceGlobals.TransportRulesEngineTracer.TraceDebug(0L, "Message is forced outbound TLS at OnResolvedMessage");
			RoutingActionUtils.ProcessRecipients(context, delegate(EnvelopeRecipient recipient)
			{
				context.OnResolvedSource.SetTlsAuthLevel(recipient, new RequiredTlsAuthLevel?(RequiredTlsAuthLevel.EncryptionOnly));
			});
			return ExecutionControl.Execute;
		}
	}
}
