using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000419 RID: 1049
	internal class SmtpSessions
	{
		// Token: 0x0600304B RID: 12363 RVA: 0x000C07B7 File Offset: 0x000BE9B7
		public void StartShuttingDown()
		{
			Interlocked.Increment(ref this.shuttingDown);
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000C07C5 File Offset: 0x000BE9C5
		public void ShutdownAllSessionsAndBlockUntilComplete(bool tryHarder)
		{
			this.StartShuttingDown();
			this.ShutdownAllConnections();
			this.BlockUntilAllSessionsRemoved(tryHarder);
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000C07E4 File Offset: 0x000BE9E4
		public List<ISmtpInSession> TakeSnapshot()
		{
			List<ISmtpInSession> result;
			lock (this.sessions)
			{
				List<ISmtpInSession> list = new List<ISmtpInSession>(this.sessions.Count);
				list.AddRange(from pair in this.sessions
				select pair.Value);
				result = list;
			}
			return result;
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000C0860 File Offset: 0x000BEA60
		public bool TryAdd(long id, ISmtpInSession session)
		{
			if (this.IsShuttingDown)
			{
				return false;
			}
			bool result;
			lock (this.sessions)
			{
				result = TransportHelpers.AttemptAddToDictionary<long, ISmtpInSession>(this.sessions, id, session, null);
			}
			return result;
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000C08B4 File Offset: 0x000BEAB4
		public void Remove(long id)
		{
			lock (this.sessions)
			{
				this.sessions.Remove(id);
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000C08FC File Offset: 0x000BEAFC
		private bool IsShuttingDown
		{
			get
			{
				return 0 != Interlocked.CompareExchange(ref this.shuttingDown, 1, 1);
			}
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000C0914 File Offset: 0x000BEB14
		private void ShutdownAllConnections()
		{
			List<ISmtpInSession> list = this.TakeSnapshot();
			foreach (ISmtpInSession smtpInSession in list)
			{
				smtpInSession.ShutdownConnection();
			}
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000C0968 File Offset: 0x000BEB68
		private void BlockUntilAllSessionsRemoved(bool tryHarder)
		{
			int num = 0;
			while (this.sessions.Any<KeyValuePair<long, ISmtpInSession>>())
			{
				if (!tryHarder && num >= 15)
				{
					return;
				}
				if (num >= 60)
				{
					throw new InvalidOperationException(string.Format("There are still {0} outstanding sessions after sleeping for {1} seconds", this.sessions.Count, num));
				}
				num++;
				Thread.Sleep(1000);
			}
		}

		// Token: 0x0400179B RID: 6043
		private readonly Dictionary<long, ISmtpInSession> sessions = new Dictionary<long, ISmtpInSession>();

		// Token: 0x0400179C RID: 6044
		private int shuttingDown;
	}
}
