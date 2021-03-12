using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200055A RID: 1370
	internal class SubjectFactory : ISubjectFactory
	{
		// Token: 0x06002218 RID: 8728 RVA: 0x000CDC88 File Offset: 0x000CBE88
		public ISubject CreateSubject(string serverName)
		{
			return new Subject(serverName);
		}
	}
}
