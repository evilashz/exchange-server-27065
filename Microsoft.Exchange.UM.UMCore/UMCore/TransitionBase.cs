using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200000E RID: 14
	internal abstract class TransitionBase
	{
		// Token: 0x060000ED RID: 237 RVA: 0x000051FF File Offset: 0x000033FF
		protected TransitionBase(FsmAction action, string tevent, ExpressionParser.Expression condition, bool heavy, bool playback, string refInfo)
		{
			this.action = action;
			this.tevent = tevent;
			this.condition = condition;
			this.heavy = heavy;
			this.isPlaybackTransition = playback;
			this.refInfo = refInfo;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005234 File Offset: 0x00003434
		internal ExpressionParser.Expression Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000523C File Offset: 0x0000343C
		internal bool BargeIn
		{
			get
			{
				return !this.isPlaybackTransition;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005247 File Offset: 0x00003447
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000524F File Offset: 0x0000344F
		protected FsmAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005258 File Offset: 0x00003458
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00005260 File Offset: 0x00003460
		protected string Tevent
		{
			get
			{
				return this.tevent;
			}
			set
			{
				this.tevent = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005269 File Offset: 0x00003469
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00005271 File Offset: 0x00003471
		protected string RefInfo
		{
			get
			{
				return this.refInfo;
			}
			set
			{
				this.refInfo = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000527A File Offset: 0x0000347A
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00005282 File Offset: 0x00003482
		protected bool IsPlaybackTransition
		{
			get
			{
				return this.isPlaybackTransition;
			}
			set
			{
				this.isPlaybackTransition = value;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000528C File Offset: 0x0000348C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Transition type={0}, tevent={1}, action={2}", new object[]
			{
				base.GetType().ToString(),
				this.tevent,
				this.action
			});
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000052D0 File Offset: 0x000034D0
		internal static TransitionBase Create(FsmAction action, string refid, string tevent, bool heavy, bool playback, ActivityManagerConfig manager, ExpressionParser.Expression condition, string refInfo)
		{
			Match match;
			if ((match = TransitionBase.outRegex.Match(refid)).Success)
			{
				string value = match.Groups["outId"].Value;
				return new OutTransition(action, tevent, value, heavy, manager, condition, refInfo);
			}
			if (TransitionBase.activityRegex.IsMatch(refid))
			{
				return new ActivityTransition(action, refid, tevent, heavy, playback, manager, condition, refInfo);
			}
			return null;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005338 File Offset: 0x00003538
		internal virtual void Execute(ActivityManager manager, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Transition.Execute entered with T={0}.", new object[]
			{
				this
			});
			TransitionBase autoEvent = null;
			bool flag = vo.IsDuringPlayback();
			bool flag2 = this.heavy;
			if (flag && !this.isPlaybackTransition)
			{
				vo.StopPlayback();
				return;
			}
			if (this.action != null)
			{
				if (!flag && flag2 && !vo.IsClosing)
				{
					HeavyBlockingOperation hbo = new HeavyBlockingOperation(manager, vo, this.action, this);
					manager.RunHeavyBlockingOperation(vo, hbo);
					return;
				}
				autoEvent = this.action.Execute(manager, vo);
			}
			this.ProcessAutoEvent(manager, vo, autoEvent);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000053CB File Offset: 0x000035CB
		internal void ProcessAutoEvent(ActivityManager manager, BaseUMCallSession vo, TransitionBase autoEvent)
		{
			if (autoEvent != null)
			{
				autoEvent.Execute(manager, vo);
				return;
			}
			if (!vo.IsDuringPlayback())
			{
				this.DoTransition(manager, vo);
			}
		}

		// Token: 0x060000FC RID: 252
		protected abstract void DoTransition(ActivityManager manager, BaseUMCallSession vo);

		// Token: 0x04000049 RID: 73
		private const string OutIdGroup = "outId";

		// Token: 0x0400004A RID: 74
		private static Regex activityRegex = new Regex("^(\\d{1,4}|[\\w]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400004B RID: 75
		private static Regex outRegex = new Regex("^out-(?<outId>\\d{1,4}|[\\w]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400004C RID: 76
		private static Regex nullRegex = new Regex("^null$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400004D RID: 77
		private bool heavy;

		// Token: 0x0400004E RID: 78
		private FsmAction action;

		// Token: 0x0400004F RID: 79
		private string tevent;

		// Token: 0x04000050 RID: 80
		private ExpressionParser.Expression condition;

		// Token: 0x04000051 RID: 81
		private string refInfo;

		// Token: 0x04000052 RID: 82
		private bool isPlaybackTransition;
	}
}
