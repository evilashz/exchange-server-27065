using System;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000062 RID: 98
	internal static class TransportFacades
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00011244 File Offset: 0x0000F444
		public static Dns Dns
		{
			get
			{
				return TransportFacades.enhancedDns;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0001124B File Offset: 0x0000F44B
		public static ICategorizerComponentFacade CategorizerComponent
		{
			get
			{
				return TransportFacades.categorizerComponent;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00011254 File Offset: 0x0000F454
		public static bool IsVoicemail(EmailMessage message)
		{
			if (message != null && message.MapiMessageClass != null)
			{
				string mapiMessageClass = message.MapiMessageClass;
				return mapiMessageClass.Equals("IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA", StringComparison.OrdinalIgnoreCase) || mapiMessageClass.Equals("IPM.Note.rpmsg.Microsoft.Voicemail.UM", StringComparison.OrdinalIgnoreCase) || mapiMessageClass.Equals("IPM.Note.Microsoft.Voicemail.UM", StringComparison.OrdinalIgnoreCase) || mapiMessageClass.Equals("IPM.Note.Microsoft.Voicemail.UM.CA", StringComparison.OrdinalIgnoreCase) || mapiMessageClass.Equals("IPM.Note.Microsoft.Conversation.Voice", StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000112BB File Offset: 0x0000F4BB
		public static IShadowRedundancyComponent ShadowRedundancyComponent
		{
			get
			{
				return TransportFacades.shadowRedundancyComponent;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000112C2 File Offset: 0x0000F4C2
		public static bool IsStopping
		{
			get
			{
				return TransportFacades.isStopping;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000347 RID: 839 RVA: 0x000112C9 File Offset: 0x0000F4C9
		// (remove) Token: 0x06000348 RID: 840 RVA: 0x000112E0 File Offset: 0x0000F4E0
		public static event EventHandler ConfigChanged
		{
			add
			{
				TransportFacades.configChangedDelegate = (EventHandler)Delegate.Combine(TransportFacades.configChangedDelegate, value);
			}
			remove
			{
				TransportFacades.configChangedDelegate = (EventHandler)Delegate.Remove(TransportFacades.configChangedDelegate, value);
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000112F8 File Offset: 0x0000F4F8
		public static ITransportMailItemFacade NewMailItem(ITransportMailItemFacade originalMailItem)
		{
			ITransportMailItemFacade transportMailItemFacade = TransportFacades.newMailItemDelegate(originalMailItem);
			IShadowRedundancyManagerFacade shadowRedundancyManager = TransportFacades.shadowRedundancyComponent.ShadowRedundancyManager;
			shadowRedundancyManager.LinkSideEffectMailItemIfNeeded(originalMailItem, transportMailItemFacade);
			return transportMailItemFacade;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00011325 File Offset: 0x0000F525
		public static void EnsureSecurityAttributes(ITransportMailItemFacade mailItem)
		{
			TransportFacades.ensureSecurityAttributesDelegate(mailItem);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00011332 File Offset: 0x0000F532
		internal static IHistoryFacade ReadHistoryFrom(ITransportMailItemFacade mailItem)
		{
			return TransportFacades.readHistoryFromMailItemByAgentDelegate(mailItem);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001133F File Offset: 0x0000F53F
		internal static IHistoryFacade ReadHistoryFrom(IMailRecipientFacade recipient)
		{
			return TransportFacades.readHistoryFromRecipientByAgentDelegate(recipient);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001134C File Offset: 0x0000F54C
		public static void TrackReceiveByAgent(ITransportMailItemFacade mailItem, string sourceContext, string connectorId, long? relatedMailItemId)
		{
			TransportFacades.trackReceiveByAgentDelegate(mailItem, sourceContext, connectorId, relatedMailItemId);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001135C File Offset: 0x0000F55C
		public static void TrackRecipientAddByAgent(ITransportMailItemFacade mailItem, string recipEmail, RecipientP2Type recipientType, string agentName)
		{
			TransportFacades.trackRecipientAddByAgentDelegate(mailItem, recipEmail, recipientType, agentName);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001136C File Offset: 0x0000F56C
		public static SmtpResponse CreateAndSubmitApprovalInitiationForTransportRules(ITransportMailItemFacade transportMailItemFacade, string originalSenderAddress, string approverAddresses, string transportRuleName)
		{
			return TransportFacades.createAndSubmitApprovalInitiationForTransportRulesDelegate(transportMailItemFacade, originalSenderAddress, approverAddresses, transportRuleName);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001137C File Offset: 0x0000F57C
		internal static void Initialize(Dns enhancedDns, ICategorizerComponentFacade categorizerComponent, IShadowRedundancyComponent shadowRedundancyComponent, EventHandler configChangedDelegate, NewMailItemDelegate newMailItemDelegate, EnsureSecurityAttributesDelegate ensureSecurityAttributesDelegate, TrackReceiveByAgentDelegate trackReceiveByAgentDelegate, TrackRecipientAddByAgentDelegate trackRecipientAddByAgentDelegate, ReadHistoryFromMailItemByAgentDelegate readHistoryFromMailItemByAgentDelegate, ReadHistoryFromRecipientByAgentDelegate readHistoryFromRecipientByAgentDelegate, CreateAndSubmitApprovalInitiationForTransportRulesDelegate createAndSubmitApprovalInitiationForTransportRulesDelegate)
		{
			TransportFacades.enhancedDns = enhancedDns;
			TransportFacades.categorizerComponent = categorizerComponent;
			TransportFacades.shadowRedundancyComponent = shadowRedundancyComponent;
			TransportFacades.configChangedDelegate = configChangedDelegate;
			TransportFacades.newMailItemDelegate = newMailItemDelegate;
			TransportFacades.ensureSecurityAttributesDelegate = ensureSecurityAttributesDelegate;
			TransportFacades.trackReceiveByAgentDelegate = trackReceiveByAgentDelegate;
			TransportFacades.trackRecipientAddByAgentDelegate = trackRecipientAddByAgentDelegate;
			TransportFacades.readHistoryFromMailItemByAgentDelegate = readHistoryFromMailItemByAgentDelegate;
			TransportFacades.readHistoryFromRecipientByAgentDelegate = readHistoryFromRecipientByAgentDelegate;
			TransportFacades.createAndSubmitApprovalInitiationForTransportRulesDelegate = createAndSubmitApprovalInitiationForTransportRulesDelegate;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000113D2 File Offset: 0x0000F5D2
		internal static void Stop()
		{
			TransportFacades.isStopping = true;
		}

		// Token: 0x040003D2 RID: 978
		private static Dns enhancedDns;

		// Token: 0x040003D3 RID: 979
		private static ICategorizerComponentFacade categorizerComponent;

		// Token: 0x040003D4 RID: 980
		private static IShadowRedundancyComponent shadowRedundancyComponent;

		// Token: 0x040003D5 RID: 981
		private static EventHandler configChangedDelegate;

		// Token: 0x040003D6 RID: 982
		private static NewMailItemDelegate newMailItemDelegate;

		// Token: 0x040003D7 RID: 983
		private static EnsureSecurityAttributesDelegate ensureSecurityAttributesDelegate;

		// Token: 0x040003D8 RID: 984
		private static TrackReceiveByAgentDelegate trackReceiveByAgentDelegate;

		// Token: 0x040003D9 RID: 985
		private static TrackRecipientAddByAgentDelegate trackRecipientAddByAgentDelegate;

		// Token: 0x040003DA RID: 986
		private static ReadHistoryFromMailItemByAgentDelegate readHistoryFromMailItemByAgentDelegate;

		// Token: 0x040003DB RID: 987
		private static ReadHistoryFromRecipientByAgentDelegate readHistoryFromRecipientByAgentDelegate;

		// Token: 0x040003DC RID: 988
		private static CreateAndSubmitApprovalInitiationForTransportRulesDelegate createAndSubmitApprovalInitiationForTransportRulesDelegate;

		// Token: 0x040003DD RID: 989
		private static bool isStopping;
	}
}
