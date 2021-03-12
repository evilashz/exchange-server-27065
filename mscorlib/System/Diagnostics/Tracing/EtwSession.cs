using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000400 RID: 1024
	internal class EtwSession
	{
		// Token: 0x0600343A RID: 13370 RVA: 0x000CA420 File Offset: 0x000C8620
		public static EtwSession GetEtwSession(int etwSessionId, bool bCreateIfNeeded = false)
		{
			if (etwSessionId < 0)
			{
				return null;
			}
			EtwSession etwSession;
			foreach (WeakReference<EtwSession> weakReference in EtwSession.s_etwSessions)
			{
				if (weakReference.TryGetTarget(out etwSession) && etwSession.m_etwSessionId == etwSessionId)
				{
					return etwSession;
				}
			}
			if (!bCreateIfNeeded)
			{
				return null;
			}
			if (EtwSession.s_etwSessions == null)
			{
				EtwSession.s_etwSessions = new List<WeakReference<EtwSession>>();
			}
			etwSession = new EtwSession(etwSessionId);
			EtwSession.s_etwSessions.Add(new WeakReference<EtwSession>(etwSession));
			if (EtwSession.s_etwSessions.Count > 16)
			{
				EtwSession.TrimGlobalList();
			}
			return etwSession;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000CA4CC File Offset: 0x000C86CC
		public static void RemoveEtwSession(EtwSession etwSession)
		{
			if (EtwSession.s_etwSessions == null || etwSession == null)
			{
				return;
			}
			EtwSession.s_etwSessions.RemoveAll(delegate(WeakReference<EtwSession> wrEtwSession)
			{
				EtwSession etwSession2;
				return wrEtwSession.TryGetTarget(out etwSession2) && etwSession2.m_etwSessionId == etwSession.m_etwSessionId;
			});
			if (EtwSession.s_etwSessions.Count > 16)
			{
				EtwSession.TrimGlobalList();
			}
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000CA520 File Offset: 0x000C8720
		private static void TrimGlobalList()
		{
			if (EtwSession.s_etwSessions == null)
			{
				return;
			}
			EtwSession.s_etwSessions.RemoveAll(delegate(WeakReference<EtwSession> wrEtwSession)
			{
				EtwSession etwSession;
				return !wrEtwSession.TryGetTarget(out etwSession);
			});
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000CA554 File Offset: 0x000C8754
		private EtwSession(int etwSessionId)
		{
			this.m_etwSessionId = etwSessionId;
		}

		// Token: 0x040016FF RID: 5887
		public readonly int m_etwSessionId;

		// Token: 0x04001700 RID: 5888
		public ActivityFilter m_activityFilter;

		// Token: 0x04001701 RID: 5889
		private static List<WeakReference<EtwSession>> s_etwSessions = new List<WeakReference<EtwSession>>();

		// Token: 0x04001702 RID: 5890
		private const int s_thrSessionCount = 16;
	}
}
