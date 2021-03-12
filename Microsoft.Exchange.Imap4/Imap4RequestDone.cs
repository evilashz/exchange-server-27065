using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000025 RID: 37
	internal sealed class Imap4RequestDone : Imap4Request
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x0000B299 File Offset: 0x00009499
		public Imap4RequestDone(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = null;
			this.PerfCounterFailures = null;
			base.AllowedStates = Imap4State.Idle;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000B2B9 File Offset: 0x000094B9
		public override bool NeedsStoreConnection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000B2BC File Offset: 0x000094BC
		public override bool NeedToDelayStoreAction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000B2C0 File Offset: 0x000094C0
		public override ProtocolResponse Process()
		{
			base.Factory.SessionState = base.Factory.SavedSessionState;
			MapiNotificationManager notificationManager = base.Factory.NotificationManager;
			if (notificationManager != null)
			{
				notificationManager.InRealTimeMode = false;
			}
			return new Imap4Response(this, Imap4Response.Type.ok, "IDLE completed.");
		}

		// Token: 0x04000137 RID: 311
		internal const string DONEResponse = "IDLE completed.";
	}
}
