using System;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200019B RID: 411
	internal class OutTransition : TransitionBase
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x0003498C File Offset: 0x00032B8C
		internal OutTransition(FsmAction action, string tevent, string refid, bool heavy, ActivityManagerConfig managerConfig, ExpressionParser.Expression condition, string refInfo) : base(action, tevent, condition, heavy, false, refInfo)
		{
			ActivityManagerConfig managerConfig2 = managerConfig.ManagerConfig;
			while (managerConfig2 != null && !managerConfig2.TryGetScopedConfig(refid, out this.endpoint))
			{
				managerConfig2 = managerConfig2.ManagerConfig;
				this.hops += 1U;
			}
			if (this.endpoint == null)
			{
				throw new FsmConfigurationException(Strings.UnknownTransitionId(refid, string.Empty));
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000349F8 File Offset: 0x00032BF8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Transition type={0}, tevent={1}, action={2}, end={3}", new object[]
			{
				base.GetType().ToString(),
				base.Tevent,
				base.Action,
				this.endpoint.ToString()
			});
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00034A4C File Offset: 0x00032C4C
		protected override void DoTransition(ActivityManager manager, BaseUMCallSession vo)
		{
			manager.Dispose();
			ActivityManager manager2 = manager.Manager;
			for (uint num = 0U; num < this.hops; num += 1U)
			{
				manager2 = manager2.Manager;
			}
			manager2.LastShortcut = Shortcut.None;
			manager2.ChangeActivity(this.endpoint.CreateActivity(manager2), vo, base.RefInfo);
		}

		// Token: 0x04000A16 RID: 2582
		private ActivityConfig endpoint;

		// Token: 0x04000A17 RID: 2583
		private uint hops;
	}
}
