using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetGroupPinState : UpdateAssociationCommand
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00008A04 File Offset: 0x00006C04
		public SetGroupPinState(IExtensibleLogger logger, IAssociationReplicator associationReplicator, bool pin, IGroupAssociationAdaptor masterAdaptor, GroupMailboxLocator itemLocator, IMailboxAssociationPerformanceTracker performanceTracker = null, bool isModernGroupsNewArchitecture = false) : base(logger, masterAdaptor, new IMailboxLocator[]
		{
			itemLocator
		})
		{
			this.associationReplicator = associationReplicator;
			this.pin = pin;
			this.performanceTracker = performanceTracker;
			this.isModernGroupsNewArchitecture = isModernGroupsNewArchitecture;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008A45 File Offset: 0x00006C45
		protected override IAssociationReplicator GetAssociationReplicator()
		{
			return this.associationReplicator;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008A50 File Offset: 0x00006C50
		protected override bool UpdateAssociation(MailboxAssociation association)
		{
			if (this.pin == association.IsPin)
			{
				UpdateAssociationCommand.Tracer.TraceDebug<UserMailboxLocator, bool>((long)this.GetHashCode(), "User {0} pin state is already same {1}", association.User, this.pin);
				return false;
			}
			association.IsPin = this.pin;
			association.PinDate = (this.pin ? ExDateTime.UtcNow : default(ExDateTime));
			return this.VerifyMembershipState(association);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008AC0 File Offset: 0x00006CC0
		private bool VerifyMembershipState(MailboxAssociation association)
		{
			if (association.IsPin && !association.IsMember && (ExEnvironment.IsTest || this.isModernGroupsNewArchitecture))
			{
				UpdateAssociationCommand.Tracer.TraceDebug<UserMailboxLocator, GroupMailboxLocator>((long)this.GetHashCode(), "User {0} is trying to pin, but membership for {1} is not updated. Trying to fix it", association.User, association.Group);
				if (!this.IsUserMemberOfGroup(association))
				{
					UpdateAssociationCommand.Tracer.TraceDebug<UserMailboxLocator, string>((long)this.GetHashCode(), "User {0} is not a member for {1} in AAD, ignoring it", association.User, association.Group.ExternalId);
					throw new NotAMemberException(Strings.CannotPinGroupForNonMember);
				}
				UpdateAssociationCommand.Tracer.TraceDebug<UserMailboxLocator, string>((long)this.GetHashCode(), "User {0} is a member for {1} but not marked in EXO. Fixing it", association.User, association.Group.ExternalId);
				association.IsMember = true;
				association.JoinDate = ExDateTime.UtcNow;
				this.Logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Warning>
				{
					{
						MailboxAssociationLogSchema.Warning.Context,
						"SetGroupPinState"
					},
					{
						MailboxAssociationLogSchema.Warning.Message,
						string.Format("User {0} is a member for {1} but not marked in EXO. Fixing it", association.User.ExternalId, association.Group.ExternalId)
					}
				});
			}
			return true;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008BD8 File Offset: 0x00006DD8
		private bool IsUserMemberOfGroup(MailboxAssociation association)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			bool result;
			try
			{
				IAadClient aadclient = this.GetAADClient(association);
				if (aadclient != null)
				{
					result = aadclient.IsUserMemberOfGroup(association.User.ExternalId, association.Group.ExternalId);
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				stopwatch.Stop();
				if (this.performanceTracker != null)
				{
					this.performanceTracker.SetAADQueryLatency(stopwatch.ElapsedMilliseconds);
				}
			}
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008C50 File Offset: 0x00006E50
		private IAadClient GetAADClient(MailboxAssociation association)
		{
			if (AADClientTestHooks.GraphApi_GetAadClient != null)
			{
				return AADClientTestHooks.GraphApi_GetAadClient();
			}
			ADUser user = association.User.FindAdUser();
			return AADClientFactory.Create(user);
		}

		// Token: 0x04000079 RID: 121
		private readonly IAssociationReplicator associationReplicator;

		// Token: 0x0400007A RID: 122
		private readonly bool pin;

		// Token: 0x0400007B RID: 123
		private readonly IMailboxAssociationPerformanceTracker performanceTracker;

		// Token: 0x0400007C RID: 124
		private readonly bool isModernGroupsNewArchitecture;
	}
}
