using System;
using System.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200007F RID: 127
	internal class RouteMessageOutboundConnector : TransportAction
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x00015031 File Offset: 0x00013231
		public RouteMessageOutboundConnector(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001503A File Offset: 0x0001323A
		public override Version MinimumVersion
		{
			get
			{
				return RouteMessageOutboundConnector.ConditionBasedRoutingBaseVersion;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00015041 File Offset: 0x00013241
		public override string Name
		{
			get
			{
				return "RouteMessageOutboundConnector";
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00015048 File Offset: 0x00013248
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001504B File Offset: 0x0001324B
		public override Type[] ArgumentsType
		{
			get
			{
				return RouteMessageOutboundConnector.ArgumentTypes;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00015054 File Offset: 0x00013254
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext context = (TransportRulesEvaluationContext)baseContext;
			string connectorName = (string)base.Arguments[0].GetValue(context);
			return RouteMessageOutboundConnector.RouteByConnectorName(context, connectorName);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00015088 File Offset: 0x00013288
		internal static ExecutionControl RouteByConnectorName(TransportRulesEvaluationContext context, string connectorName)
		{
			if (context.EventType == EventType.EndOfData || context.EventType == EventType.OnRoutedMessage)
			{
				return ExecutionControl.Execute;
			}
			if (context.OnResolvedSource == null)
			{
				throw new RuleInvalidOperationException("Routing actions can only be called at OnResolvedMessage");
			}
			ExTraceGlobals.TransportRulesEngineTracer.TraceDebug(0L, "Message is conditional routed through specified connector at OnResolvedMessage");
			TenantOutboundConnector enabledOutboundConnector;
			try
			{
				enabledOutboundConnector = ConnectorConfigurationSession.GetEnabledOutboundConnector(TransportUtils.GetTransportMailItem(context.MailItem).OrganizationId, connectorName);
			}
			catch (OutboundConnectorNotFoundException)
			{
				context.ResetRulesCache();
				throw;
			}
			Header header = Header.Create("X-MS-Exchange-Organization-OutboundConnector");
			header.Value = string.Format("{0};{1}", enabledOutboundConnector.Guid, HttpUtility.UrlEncode(connectorName));
			context.MailItem.Message.MimeDocument.RootPart.Headers.RemoveAll("X-MS-Exchange-Organization-OutboundConnector");
			context.MailItem.Message.MimeDocument.RootPart.Headers.AppendChild(header);
			Header header2 = Header.Create("X-MS-Exchange-Forest-OutboundConnector");
			header2.Value = header.Value;
			context.MailItem.Message.MimeDocument.RootPart.Headers.RemoveAll("X-MS-Exchange-Forest-OutboundConnector");
			context.MailItem.Message.MimeDocument.RootPart.Headers.AppendChild(header2);
			return ExecutionControl.Execute;
		}

		// Token: 0x04000261 RID: 609
		internal static readonly Version ConditionBasedRoutingBaseVersion = new Version("15.00.0002.00");

		// Token: 0x04000262 RID: 610
		private static readonly Type[] ArgumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}
