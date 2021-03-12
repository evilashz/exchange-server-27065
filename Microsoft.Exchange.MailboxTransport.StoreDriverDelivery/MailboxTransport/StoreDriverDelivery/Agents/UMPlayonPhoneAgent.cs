using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriver;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000C9 RID: 201
	internal sealed class UMPlayonPhoneAgent : StoreDriverDeliveryAgent
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x000225EC File Offset: 0x000207EC
		public UMPlayonPhoneAgent(SmtpServer server)
		{
			base.OnPromotedMessage += this.OnPromotedMessageHandler;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00022608 File Offset: 0x00020808
		public void OnPromotedMessageHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs e)
		{
			StoreDriverDeliveryEventArgsImpl storeDriverDeliveryEventArgsImpl = (StoreDriverDeliveryEventArgsImpl)e;
			if (storeDriverDeliveryEventArgsImpl.IsPublicFolderRecipient || storeDriverDeliveryEventArgsImpl.IsJournalReport)
			{
				UMPlayonPhoneAgent.Tracer.TraceError((long)this.GetHashCode(), "not supported for public folder or journal reports");
				return;
			}
			if (!storeDriverDeliveryEventArgsImpl.ReplayItem.IsRestricted || !storeDriverDeliveryEventArgsImpl.ReplayItem.ClassName.StartsWith("IPM.Note.rpmsg.Microsoft.Voicemail.UM", StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			UMMailboxPolicy ummailboxPolicy;
			if (!UMAgentUtil.TryGetUMMailboxPolicy(UMPlayonPhoneAgent.Tracer, storeDriverDeliveryEventArgsImpl.ADRecipientCache, storeDriverDeliveryEventArgsImpl.MailRecipient, out ummailboxPolicy))
			{
				return;
			}
			if (storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies == null)
			{
				storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies = new Dictionary<PropertyDefinition, object>();
			}
			if (ummailboxPolicy.RequireProtectedPlayOnPhone)
			{
				storeDriverDeliveryEventArgsImpl.PropertiesForAllMessageCopies[MessageItemSchema.RequireProtectedPlayOnPhone] = "true";
			}
		}

		// Token: 0x0400036C RID: 876
		private static readonly Trace Tracer = ExTraceGlobals.UMPlayonPhoneAgentTracer;
	}
}
