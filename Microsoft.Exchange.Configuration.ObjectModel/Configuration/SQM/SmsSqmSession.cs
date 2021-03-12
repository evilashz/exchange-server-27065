using System;
using Microsoft.Exchange.Sqm;

namespace Microsoft.Exchange.Configuration.SQM
{
	// Token: 0x0200026E RID: 622
	internal class SmsSqmSession : SqmSession
	{
		// Token: 0x06001566 RID: 5478 RVA: 0x0004F074 File Offset: 0x0004D274
		private SmsSqmSession() : base(SqmAppID.SMS, SqmSession.Scope.Process)
		{
			base.Open();
			AppDomain.CurrentDomain.ProcessExit += SmsSqmSession.CloseSessionEventHandler;
			AppDomain.CurrentDomain.DomainUnload += delegate(object param0, EventArgs param1)
			{
				if (SmsSqmSession.instance != null)
				{
					SmsSqmSession.instance.Close();
					SmsSqmSession.instance = null;
				}
				AppDomain.CurrentDomain.ProcessExit -= SmsSqmSession.CloseSessionEventHandler;
			};
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0004F0D0 File Offset: 0x0004D2D0
		public static SmsSqmSession Instance
		{
			get
			{
				lock (SmsSqmSession.GetInstanceSyncObject)
				{
					if (SmsSqmSession.instance == null)
					{
						SmsSqmSession.instance = new SmsSqmSession();
					}
				}
				return SmsSqmSession.instance;
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0004F120 File Offset: 0x0004D320
		private static void CloseSessionEventHandler(object sender, EventArgs e)
		{
			if (SmsSqmSession.instance != null)
			{
				SmsSqmSession.instance.Close();
				SmsSqmSession.instance = null;
			}
		}

		// Token: 0x04000687 RID: 1671
		private static readonly object GetInstanceSyncObject = new object();

		// Token: 0x04000688 RID: 1672
		private static SmsSqmSession instance = null;
	}
}
