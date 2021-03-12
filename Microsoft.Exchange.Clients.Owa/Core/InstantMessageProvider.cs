using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200013A RID: 314
	internal abstract class InstantMessageProvider : DisposeTrackableBase
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00046433 File Offset: 0x00044633
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0004643B File Offset: 0x0004463B
		internal virtual bool IsActivityBasedPresenceSet { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000A43 RID: 2627
		internal abstract bool IsSessionStarted { get; }

		// Token: 0x06000A44 RID: 2628
		internal abstract void EstablishSession();

		// Token: 0x06000A45 RID: 2629
		internal abstract void ResetPresence();

		// Token: 0x06000A46 RID: 2630
		internal abstract int SendMessage(InstantMessageProvider.ProviderMessage message);

		// Token: 0x06000A47 RID: 2631
		internal abstract int SendNewChatMessage(InstantMessageProvider.ProviderMessage message);

		// Token: 0x06000A48 RID: 2632
		internal abstract void AddBuddy(InstantMessageBuddy buddy, InstantMessageGroup group);

		// Token: 0x06000A49 RID: 2633
		internal abstract void RemoveBuddy(InstantMessageBuddy buddy);

		// Token: 0x06000A4A RID: 2634
		internal abstract void EndChatSession(int chatSessionId, bool disconnectSession);

		// Token: 0x06000A4B RID: 2635
		internal abstract void NotifyTyping(int chatSessionId, bool typingCanceled);

		// Token: 0x06000A4C RID: 2636
		internal abstract void PublishSelfPresence(int presence);

		// Token: 0x06000A4D RID: 2637
		internal abstract void MakeEndpointMostActive();

		// Token: 0x06000A4E RID: 2638
		internal abstract void RemoveFromGroup(InstantMessageGroup group, InstantMessageBuddy buddy);

		// Token: 0x06000A4F RID: 2639
		internal abstract void MoveBuddy(InstantMessageGroup oldGroup, InstantMessageGroup newGroup, InstantMessageBuddy buddy);

		// Token: 0x06000A50 RID: 2640
		internal abstract void CopyBuddy(InstantMessageGroup group, InstantMessageBuddy buddy);

		// Token: 0x06000A51 RID: 2641
		internal abstract void CreateGroup(string groupName);

		// Token: 0x06000A52 RID: 2642
		internal abstract void RemoveGroup(InstantMessageGroup group);

		// Token: 0x06000A53 RID: 2643
		internal abstract void RenameGroup(InstantMessageGroup group, string newGroupName);

		// Token: 0x06000A54 RID: 2644
		internal abstract void AcceptBuddy(InstantMessageBuddy buddy, InstantMessageGroup group);

		// Token: 0x06000A55 RID: 2645
		internal abstract void DeclineBuddy(InstantMessageBuddy buddy);

		// Token: 0x06000A56 RID: 2646
		internal abstract void GetBuddyList();

		// Token: 0x06000A57 RID: 2647 RVA: 0x00046444 File Offset: 0x00044644
		internal virtual void BlockBuddy(InstantMessageBuddy buddy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0004644B File Offset: 0x0004464B
		internal virtual void UnblockBuddy(InstantMessageBuddy buddy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00046452 File Offset: 0x00044652
		internal virtual void AddSubscription(string[] sipUris)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00046459 File Offset: 0x00044659
		internal virtual void RemoveSubscription(string sipUri)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00046460 File Offset: 0x00044660
		internal virtual void QueryPresence(string[] sipUris)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00046467 File Offset: 0x00044667
		internal virtual void PublishResetStatus()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0004646E File Offset: 0x0004466E
		internal virtual void ParticipateInConversation(int conversationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040007AF RID: 1967
		internal InstantMessagePayload Payload;

		// Token: 0x040007B0 RID: 1968
		protected UserContext userContext;

		// Token: 0x040007B1 RID: 1969
		internal HashSet<string> ExpandedGroupIds = new HashSet<string>();

		// Token: 0x040007B2 RID: 1970
		protected bool isEarlierSignInSuccessful = true;

		// Token: 0x0200013B RID: 315
		internal struct ProviderMessage
		{
			// Token: 0x040007B4 RID: 1972
			public string Body;

			// Token: 0x040007B5 RID: 1973
			public string Format;

			// Token: 0x040007B6 RID: 1974
			public int ChatSessionId;

			// Token: 0x040007B7 RID: 1975
			public string[] Recipients;

			// Token: 0x040007B8 RID: 1976
			public int[] AddressTypes;
		}
	}
}
