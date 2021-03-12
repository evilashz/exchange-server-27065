using System;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetEscalate : UpdateAssociationCommand
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00008CBC File Offset: 0x00006EBC
		public SetEscalate(IExtensibleLogger logger, bool shouldEscalate, SmtpAddress userSmtpAddress, IUserAssociationAdaptor masterAdaptor, UserMailboxLocator itemLocator, int maxEscalatedMembers = 400) : base(logger, masterAdaptor, new IMailboxLocator[]
		{
			itemLocator
		})
		{
			this.shouldEscalate = shouldEscalate;
			this.userSmtpAddress = userSmtpAddress;
			this.maxEscalatedMembers = maxEscalatedMembers;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008CF8 File Offset: 0x00006EF8
		protected override bool UpdateAssociation(MailboxAssociation association)
		{
			if (this.shouldEscalate == association.ShouldEscalate)
			{
				UpdateAssociationCommand.Tracer.TraceDebug<UserMailboxLocator, bool>((long)this.GetHashCode(), "User {0} escalate state is already same {1}", association.User, this.shouldEscalate);
				return false;
			}
			if (this.shouldEscalate && !association.IsMember)
			{
				UpdateAssociationCommand.Tracer.TraceError<UserMailboxLocator, IMailboxLocator>((long)this.GetHashCode(), "Only Members can Subscribe to group, throwing NotAMemberException. User={0}, Group={1}", association.User, base.MasterAdaptor.MasterLocator);
				throw new NotAMemberException(Strings.CannotEscalateForNonMember);
			}
			if (this.shouldEscalate && GetEscalatedAssociations.GetEscalatedAssociationsCount(base.MasterAdaptor) >= this.maxEscalatedMembers)
			{
				throw new ExceededMaxSubscribersException(Strings.MaxSubscriptionsForGroupReached);
			}
			association.UserSmtpAddress = this.userSmtpAddress;
			association.ShouldEscalate = this.shouldEscalate;
			return true;
		}

		// Token: 0x0400007E RID: 126
		public const int DefaultMaxEscalatedMembers = 400;

		// Token: 0x0400007F RID: 127
		private readonly int maxEscalatedMembers;

		// Token: 0x04000080 RID: 128
		private readonly bool shouldEscalate;

		// Token: 0x04000081 RID: 129
		private readonly SmtpAddress userSmtpAddress;
	}
}
