using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000029 RID: 41
	internal sealed class Imap4RequestIdle : Imap4Request
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000B888 File Offset: 0x00009A88
		public Imap4RequestIdle(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_IDLE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_IDLE_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000B8BC File Offset: 0x00009ABC
		public override bool NeedsStoreConnection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000B8BF File Offset: 0x00009ABF
		public override bool NeedToDelayStoreAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		public override ProtocolResponse Process()
		{
			base.Factory.SavedSessionState = base.Factory.SessionState;
			base.Factory.SessionState = Imap4State.Idle;
			base.Factory.SavedIdleTag = base.Tag;
			base.Factory.NotificationManager.InRealTimeMode = true;
			if (!base.Factory.Session.SendToClient(new StringResponseItem("+ IDLE accepted, awaiting DONE command.")))
			{
				return null;
			}
			return new Imap4Response(this, Imap4Response.Type.unknown, string.Empty);
		}

		// Token: 0x04000149 RID: 329
		private const string IDLEMoreDataResponse = "+ IDLE accepted, awaiting DONE command.";
	}
}
