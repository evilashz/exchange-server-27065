using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000045 RID: 69
	internal class TextMessageDeliveryContext
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00009F79 File Offset: 0x00008179
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00009F95 File Offset: 0x00008195
		public bool PartnerDelivery
		{
			get
			{
				if (this.AgentWrapper != null)
				{
					return this.AgentWrapper.PartnerDelivery;
				}
				return this.partnerDelivery;
			}
			internal set
			{
				if (this.AgentWrapper != null)
				{
					this.AgentWrapper.PartnerDelivery = value;
				}
				this.partnerDelivery = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00009FB2 File Offset: 0x000081B2
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00009FBA File Offset: 0x000081BA
		public TransportAgentWrapper AgentWrapper { get; internal set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00009FC3 File Offset: 0x000081C3
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00009FCB File Offset: 0x000081CB
		public QueueDataAvailableEventHandler<TextMessageDeliveryContext> CleanerEventHandler { get; internal set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00009FD4 File Offset: 0x000081D4
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00009FDC File Offset: 0x000081DC
		public Uri EcpLinkUrl { get; internal set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00009FE8 File Offset: 0x000081E8
		public string EcpTextMessagingSlab
		{
			get
			{
				if (this.ecpTextMessagingSlab == null)
				{
					this.ecpTextMessagingSlab = ((null == this.EcpLinkUrl) ? string.Empty : AntiXssEncoder.HtmlEncode(this.EcpLinkUrl.ToString() + "/sms/textmessaging.slab", false));
				}
				return this.ecpTextMessagingSlab;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000A03C File Offset: 0x0000823C
		public string EcpEditNotificatonWizard
		{
			get
			{
				if (this.ecpEditNotificatonWizard == null)
				{
					this.ecpEditNotificatonWizard = ((null == this.EcpLinkUrl) ? string.Empty : AntiXssEncoder.HtmlEncode(this.EcpLinkUrl.ToString() + "/sms/EditNotification.aspx", false));
				}
				return this.ecpEditNotificatonWizard;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000A090 File Offset: 0x00008290
		public string EcpInboxRuleSlab
		{
			get
			{
				if (this.ecpInboxRuleSlab == null)
				{
					this.ecpInboxRuleSlab = ((null == this.EcpLinkUrl) ? string.Empty : AntiXssEncoder.HtmlEncode(this.EcpLinkUrl.ToString() + "/RulesEditor/InboxRules.slab", false));
				}
				return this.ecpInboxRuleSlab;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000A0E1 File Offset: 0x000082E1
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000A0E9 File Offset: 0x000082E9
		public string MapiMessageClass { get; internal set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000A0F2 File Offset: 0x000082F2
		public bool IsUndercurrentMessage
		{
			get
			{
				return ObjectClass.IsOfClass(this.MapiMessageClass, "IPM.Note.Mobile.SMS.Undercurrent");
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000A104 File Offset: 0x00008304
		public bool IsAlertMessage
		{
			get
			{
				return ObjectClass.IsOfClass(this.MapiMessageClass, "IPM.Note.Mobile.SMS.Alert");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000A116 File Offset: 0x00008316
		public bool IsRuleAlertMessage
		{
			get
			{
				return ObjectClass.IsOfClass(this.MapiMessageClass, "IPM.Note.Mobile.SMS.Alert", false);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000A129 File Offset: 0x00008329
		public bool IsVoicemailMessage
		{
			get
			{
				return ObjectClass.IsOfClass(this.MapiMessageClass, "IPM.Note.Mobile.SMS.Alert.Voicemail");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000A13B File Offset: 0x0000833B
		public bool IsAlertInfoMessage
		{
			get
			{
				return ObjectClass.IsOfClass(this.MapiMessageClass, "IPM.Note.Mobile.SMS.Alert.Info");
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000A14D File Offset: 0x0000834D
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000A155 File Offset: 0x00008355
		public MessageItem Message { get; internal set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000A15E File Offset: 0x0000835E
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000A166 File Offset: 0x00008366
		public IList<TextSendingPackage> TextSendingPackages { get; internal set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000A16F File Offset: 0x0000836F
		public ExchangePrincipal Principal
		{
			get
			{
				return this.principal;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A177 File Offset: 0x00008377
		public void SetPrincipalFromProxyAddress(string proxyAddress)
		{
			this.principal = ExchangePrincipal.FromProxyAddress(this.AgentWrapper.ADSessionSettings, proxyAddress);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000A190 File Offset: 0x00008390
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000A198 File Offset: 0x00008398
		public IDictionary<MobileRecipient, EnvelopeRecipient> Recipients { get; internal set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000A1A1 File Offset: 0x000083A1
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000A1A9 File Offset: 0x000083A9
		public TextMessagingSettingsVersion1Point0 Settings { get; internal set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000A1B2 File Offset: 0x000083B2
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000A1BA File Offset: 0x000083BA
		public IMobileService MobileService { get; internal set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000A1C3 File Offset: 0x000083C3
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000A1CB File Offset: 0x000083CB
		public CalendarNotificationType CalNotifTypeHint { get; internal set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000A1D4 File Offset: 0x000083D4
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000A1DC File Offset: 0x000083DC
		public bool IsFromOutlook { get; internal set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000A1E5 File Offset: 0x000083E5
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000A1ED File Offset: 0x000083ED
		public CultureInfo NotificationPreferredCulture { get; internal set; }

		// Token: 0x040000F1 RID: 241
		private ExchangePrincipal principal;

		// Token: 0x040000F2 RID: 242
		private string ecpTextMessagingSlab;

		// Token: 0x040000F3 RID: 243
		private string ecpEditNotificatonWizard;

		// Token: 0x040000F4 RID: 244
		private string ecpInboxRuleSlab;

		// Token: 0x040000F5 RID: 245
		private bool partnerDelivery;
	}
}
