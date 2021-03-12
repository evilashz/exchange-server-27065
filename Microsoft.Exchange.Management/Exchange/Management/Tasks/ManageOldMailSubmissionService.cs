using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200045F RID: 1119
	public class ManageOldMailSubmissionService : ManageMailSubmissionService
	{
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600278A RID: 10122 RVA: 0x0009C55E File Offset: 0x0009A75E
		internal string ServiceName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x0009C566 File Offset: 0x0009A766
		protected override string Name
		{
			get
			{
				return "MSExchMailSubmissionSvc";
			}
		}
	}
}
