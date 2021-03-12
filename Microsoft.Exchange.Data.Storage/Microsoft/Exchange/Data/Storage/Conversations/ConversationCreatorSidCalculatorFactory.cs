using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008A8 RID: 2216
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationCreatorSidCalculatorFactory
	{
		// Token: 0x060052D3 RID: 21203 RVA: 0x00159FA2 File Offset: 0x001581A2
		public ConversationCreatorSidCalculatorFactory(IXSOFactory xsoFactory)
		{
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x00159FB1 File Offset: 0x001581B1
		public bool TryCreate(IMailboxSession mailboxSession, IExchangePrincipal exchangePrincipal, out IConversationCreatorSidCalculator calculator)
		{
			calculator = null;
			if (!this.CanSetConversationCreatorProperty(mailboxSession))
			{
				return false;
			}
			if (exchangePrincipal != null && exchangePrincipal.GetContext(null) != null)
			{
				calculator = this.Create(mailboxSession, exchangePrincipal.GetContext(null));
			}
			else
			{
				calculator = new LegacyConversationCreatorSidCalculator(mailboxSession);
			}
			return true;
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x00159FE8 File Offset: 0x001581E8
		public bool TryCreate(IMailboxSession mailboxSession, MiniRecipient miniRecipient, out IConversationCreatorSidCalculator calculator)
		{
			calculator = null;
			if (!this.CanSetConversationCreatorProperty(mailboxSession))
			{
				return false;
			}
			if (miniRecipient != null && miniRecipient.GetContext(null) != null)
			{
				calculator = this.Create(mailboxSession, miniRecipient.GetContext(null));
			}
			else
			{
				calculator = new LegacyConversationCreatorSidCalculator(mailboxSession);
			}
			return true;
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x0015A020 File Offset: 0x00158220
		private IConversationCreatorSidCalculator Create(IMailboxSession mailboxSession, IConstraintProvider constraintProvider)
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(constraintProvider, null, null);
			if (snapshot.DataStorage.DeleteGroupConversation.Enabled)
			{
				ICoreConversationFactory<IConversation> conversationFactory = new CachedConversationFactory(mailboxSession);
				return new ConversationCreatorSidCalculator(this.xsoFactory, mailboxSession, conversationFactory);
			}
			return new LegacyConversationCreatorSidCalculator(mailboxSession);
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x0015A066 File Offset: 0x00158266
		private bool CanSetConversationCreatorProperty(IMailboxSession session)
		{
			return session != null && this.IsSessionLogonTypeSupported(session.LogonType) && session.IsGroupMailbox();
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x0015A084 File Offset: 0x00158284
		private bool IsSessionLogonTypeSupported(LogonType logonType)
		{
			switch (logonType)
			{
			case LogonType.Owner:
			case LogonType.Delegated:
			case LogonType.Transport:
				return true;
			}
			return false;
		}

		// Token: 0x04002D21 RID: 11553
		private readonly IXSOFactory xsoFactory;
	}
}
