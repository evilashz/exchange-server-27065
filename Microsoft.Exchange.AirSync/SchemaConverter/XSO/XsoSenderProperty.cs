using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000234 RID: 564
	[Serializable]
	internal class XsoSenderProperty : XsoStringProperty
	{
		// Token: 0x060014FF RID: 5375 RVA: 0x0007B342 File Offset: 0x00079542
		public XsoSenderProperty() : base(null)
		{
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0007B34B File Offset: 0x0007954B
		public XsoSenderProperty(PropertyType type) : base(null, type)
		{
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0007B358 File Offset: 0x00079558
		public override string StringData
		{
			get
			{
				MessageItem messageItem = base.XsoItem as MessageItem;
				string text = null;
				if (messageItem == null)
				{
					PostItem postItem = base.XsoItem as PostItem;
					if (postItem == null)
					{
						throw new UnexpectedTypeException("PostItem", base.XsoItem);
					}
					try
					{
						if (postItem.Sender != null)
						{
							string participantString = EmailAddressConverter.GetParticipantString(postItem.Sender, postItem.Session.MailboxOwner);
							AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.XsoTracer, this, "PostItem Sender: {0}", participantString);
							if (postItem.From == null || !string.Equals(participantString, EmailAddressConverter.GetParticipantString(postItem.From, postItem.Session.MailboxOwner)))
							{
								AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.XsoTracer, this, "PostItem Sender and from are different. setting value for 'Sender' {0}.", participantString);
								text = participantString;
							}
						}
						goto IL_20C;
					}
					catch (PropertyErrorException ex)
					{
						if (ex.PropertyErrors[0].PropertyErrorCode == PropertyErrorCode.NotEnoughMemory)
						{
							postItem.Load();
							string participantString = EmailAddressConverter.GetParticipantString(postItem.Sender, postItem.Session.MailboxOwner);
							AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.XsoTracer, this, "After calling Load-PostItem Sender: {0}", participantString);
							if (postItem.From == null || !string.Equals(participantString, EmailAddressConverter.GetParticipantString(postItem.From, postItem.Session.MailboxOwner)))
							{
								AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.XsoTracer, this, "After calling Load-PostItem Sender and from are different. setting value for 'Sender' {0}.", participantString);
								text = participantString;
							}
						}
						goto IL_20C;
					}
				}
				try
				{
					if (messageItem.Sender != null)
					{
						string participantString = EmailAddressConverter.GetParticipantString(messageItem.Sender, messageItem.Session.MailboxOwner);
						AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.XsoTracer, this, "Message Sender: {0}", participantString);
						if (messageItem.From == null || !string.Equals(participantString, EmailAddressConverter.GetParticipantString(messageItem.From, messageItem.Session.MailboxOwner)))
						{
							text = participantString;
						}
					}
				}
				catch (PropertyErrorException ex2)
				{
					if (ex2.PropertyErrors[0].PropertyErrorCode == PropertyErrorCode.NotEnoughMemory)
					{
						messageItem.Load();
						string participantString = EmailAddressConverter.GetParticipantString(messageItem.Sender, messageItem.Session.MailboxOwner);
						AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.XsoTracer, this, "After calling Load- Message Sender: {0}", participantString);
						if (messageItem.From == null || !string.Equals(participantString, EmailAddressConverter.GetParticipantString(messageItem.From, messageItem.Session.MailboxOwner)))
						{
							text = participantString;
						}
					}
				}
				IL_20C:
				if (string.IsNullOrEmpty(text))
				{
					base.State = PropertyState.SetToDefault;
				}
				return text;
			}
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0007B5A0 File Offset: 0x000797A0
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			MessageItem messageItem = (MessageItem)base.XsoItem;
			messageItem.Sender = EmailAddressConverter.CreateParticipant(((IStringProperty)srcProperty).StringData);
		}
	}
}
