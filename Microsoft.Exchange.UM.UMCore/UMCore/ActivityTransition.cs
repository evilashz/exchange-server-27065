using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200000F RID: 15
	internal class ActivityTransition : TransitionBase
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00005427 File Offset: 0x00003627
		internal ActivityTransition(FsmAction action, string refid, string tevent, bool heavy, bool playback, ActivityManagerConfig manager, ExpressionParser.Expression condition, string refInfo) : base(action, tevent, condition, heavy, playback, refInfo)
		{
			this.endpoint = manager.GetScopedConfig(refid);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005448 File Offset: 0x00003648
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Transition type={0}, tevent={1}, action={2}, endpoint={3}", new object[]
			{
				base.GetType().ToString(),
				base.Tevent,
				base.Action,
				this.endpoint.ToString()
			});
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000549A File Offset: 0x0000369A
		protected override void DoTransition(ActivityManager manager, BaseUMCallSession vo)
		{
			manager.ChangeActivity(this.endpoint.CreateActivity(manager), vo, base.RefInfo);
		}

		// Token: 0x04000053 RID: 83
		private ActivityConfig endpoint;
	}
}
