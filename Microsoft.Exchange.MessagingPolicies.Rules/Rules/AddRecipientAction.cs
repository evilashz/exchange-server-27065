using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200004E RID: 78
	internal abstract class AddRecipientAction : TransportAction
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00010D2F File Offset: 0x0000EF2F
		protected AddRecipientAction(ShortList<Argument> arguments, bool isToRecipient) : base(arguments)
		{
			this.isToRecipient = isToRecipient;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00010D3F File Offset: 0x0000EF3F
		public override Type[] ArgumentsType
		{
			get
			{
				return AddRecipientAction.addressType;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00010D46 File Offset: 0x0000EF46
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00010D49 File Offset: 0x0000EF49
		public virtual string GetDisplayName(RulesEvaluationContext baseContext)
		{
			return (string)base.Arguments[0].GetValue(baseContext);
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002ED RID: 749
		public abstract RecipientP2Type RecipientP2Type { get; }

		// Token: 0x060002EE RID: 750 RVA: 0x00010D64 File Offset: 0x0000EF64
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string text = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			string displayName = this.GetDisplayName(transportRulesEvaluationContext);
			RecipientP2Type recipientP2Type;
			EmailRecipientCollection collection;
			if (this.isToRecipient)
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceDebug<string, string>(0L, "AddToRecipient, name: {0} address : {1}", displayName, text);
				recipientP2Type = RecipientP2Type.To;
				collection = transportRulesEvaluationContext.MailItem.Message.To;
			}
			else
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceDebug<string, string>(0L, "AddCcRecipient, name: {0} address : {1}", displayName, text);
				recipientP2Type = RecipientP2Type.Cc;
				collection = transportRulesEvaluationContext.MailItem.Message.Cc;
			}
			if (!AddRecipientAction.RecipientCollectionContainsAddress(transportRulesEvaluationContext, collection, text))
			{
				transportRulesEvaluationContext.RecipientsToAdd.Add(new TransportRulesEvaluationContext.AddedRecipient(text, displayName, recipientP2Type));
			}
			return ExecutionControl.Execute;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00010E14 File Offset: 0x0000F014
		internal static bool RecipientCollectionContainsAddress(TransportRulesEvaluationContext context, EmailRecipientCollection collection, string address)
		{
			AddressBook addressBook = context.Server.AddressBook;
			foreach (EmailRecipient emailRecipient in collection)
			{
				string smtpAddress = emailRecipient.SmtpAddress;
				if (!string.IsNullOrEmpty(smtpAddress) && addressBook.IsSameRecipient((RoutingAddress)smtpAddress, (RoutingAddress)address))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040001E5 RID: 485
		private static Type[] addressType = new Type[]
		{
			typeof(string)
		};

		// Token: 0x040001E6 RID: 486
		private bool isToRecipient;
	}
}
