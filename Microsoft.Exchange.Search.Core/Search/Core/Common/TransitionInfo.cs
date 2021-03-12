using System;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200008C RID: 140
	internal class TransitionInfo
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
		private TransitionInfo(ConditionMethod condition, ActionMethod action, uint targetState)
		{
			this.condition = condition;
			this.action = action;
			this.targetState = targetState;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000C4BD File Offset: 0x0000A6BD
		internal uint TargetState
		{
			get
			{
				return this.targetState;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000C4C5 File Offset: 0x0000A6C5
		internal ConditionMethod Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000C4CD File Offset: 0x0000A6CD
		internal ActionMethod Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000C4D5 File Offset: 0x0000A6D5
		public override string ToString()
		{
			return string.Format("{0}::{1}, target:{1}", (this.Action == null) ? null : this.Action.Method.DeclaringType, this.TargetState);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000C507 File Offset: 0x0000A707
		internal static TransitionInfo Create(ConditionMethod condition, ActionMethod action, uint targetState)
		{
			return new TransitionInfo(condition, action, targetState);
		}

		// Token: 0x0400019C RID: 412
		private uint targetState;

		// Token: 0x0400019D RID: 413
		private ConditionMethod condition;

		// Token: 0x0400019E RID: 414
		private ActionMethod action;
	}
}
