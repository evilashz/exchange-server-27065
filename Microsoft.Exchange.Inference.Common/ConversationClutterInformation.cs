using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000009 RID: 9
	internal class ConversationClutterInformation
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000025F4 File Offset: 0x000007F4
		public ConversationClutterInformation()
		{
			this.ClutterMessageCount = 0;
			this.UnClutterMessageCount = 0;
			this.ClutterMessageIds = null;
			this.InheritedState = null;
			this.State = ConversationClutterState.Mixed;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002634 File Offset: 0x00000834
		public void Initialize(int clutterCount, int unClutterCount, IReadOnlyList<IIdentity> messageIds, ConversationClutterState? inheritedState)
		{
			if (clutterCount == 0 && unClutterCount == 0 && inheritedState == null)
			{
				throw new ArgumentException("Both clutter and un-clutter count cannot be zero unless there is an inheritable state");
			}
			this.ClutterMessageCount = clutterCount;
			this.UnClutterMessageCount = unClutterCount;
			this.ClutterMessageIds = messageIds;
			this.InheritedState = inheritedState;
			this.State = this.GetConversationState();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002684 File Offset: 0x00000884
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000268C File Offset: 0x0000088C
		public int ClutterMessageCount { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002695 File Offset: 0x00000895
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000269D File Offset: 0x0000089D
		public int UnClutterMessageCount { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000026A6 File Offset: 0x000008A6
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000026AE File Offset: 0x000008AE
		public ConversationClutterState State { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000026B7 File Offset: 0x000008B7
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000026BF File Offset: 0x000008BF
		public IReadOnlyList<IIdentity> ClutterMessageIds { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000026C8 File Offset: 0x000008C8
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000026D0 File Offset: 0x000008D0
		public ConversationClutterState? InheritedState { get; private set; }

		// Token: 0x06000038 RID: 56 RVA: 0x000026DC File Offset: 0x000008DC
		public bool IsNewMessageClutter(bool computedClutter, out bool ShouldMarkConversationAsNotClutter)
		{
			bool result = computedClutter;
			ShouldMarkConversationAsNotClutter = false;
			if (computedClutter && this.State != ConversationClutterState.Clutter)
			{
				result = !computedClutter;
			}
			else if (!computedClutter && this.State == ConversationClutterState.Clutter)
			{
				ShouldMarkConversationAsNotClutter = (this.ClutterMessageIds != null && this.ClutterMessageIds.Any<IIdentity>());
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002724 File Offset: 0x00000924
		public virtual void MarkItemsAsNotClutter(bool userOverride)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002726 File Offset: 0x00000926
		public void MarkItemsAsNotClutter()
		{
			this.MarkItemsAsNotClutter(false);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002730 File Offset: 0x00000930
		public override string ToString()
		{
			return string.Format("ClutterMessageCount={0} UnClutterMessageCount={1} State={2} InheritedState={3}", new object[]
			{
				this.ClutterMessageCount,
				this.UnClutterMessageCount,
				this.State,
				this.InheritedState
			});
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002788 File Offset: 0x00000988
		private ConversationClutterState GetConversationState()
		{
			if (this.ClutterMessageCount > 0 && this.UnClutterMessageCount == 0)
			{
				return ConversationClutterState.Clutter;
			}
			if (this.ClutterMessageCount == 0 && this.UnClutterMessageCount > 0)
			{
				return ConversationClutterState.UnClutter;
			}
			if (this.ClutterMessageCount == 0 && this.UnClutterMessageCount == 0 && this.InheritedState != null)
			{
				return this.InheritedState.Value;
			}
			return ConversationClutterState.Mixed;
		}
	}
}
