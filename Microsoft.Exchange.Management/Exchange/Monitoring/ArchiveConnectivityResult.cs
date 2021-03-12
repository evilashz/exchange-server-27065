using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000501 RID: 1281
	[Serializable]
	public class ArchiveConnectivityResult
	{
		// Token: 0x06002DEC RID: 11756 RVA: 0x000B7A71 File Offset: 0x000B5C71
		public ArchiveConnectivityResult()
		{
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000B7A79 File Offset: 0x000B5C79
		public ArchiveConnectivityResult(ArchiveConnectivityResultEnum result)
		{
			this.result = result;
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000B7A88 File Offset: 0x000B5C88
		public ArchiveConnectivityResultEnum Value
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000B7A90 File Offset: 0x000B5C90
		public override string ToString()
		{
			string text = string.Empty;
			switch (this.result)
			{
			case ArchiveConnectivityResultEnum.Undefined:
				text = Strings.ArchiveConnectivityResultUndefined;
				break;
			case ArchiveConnectivityResultEnum.Success:
				text = Strings.ArchiveConnectivityResultSuccess;
				break;
			case ArchiveConnectivityResultEnum.ArchiveFailure:
				text = Strings.LogonFailure;
				break;
			case ArchiveConnectivityResultEnum.PrimaryFailure:
				text = Strings.ArchiveConnectivityResultPrimaryFailure;
				break;
			}
			return text;
		}

		// Token: 0x040020EE RID: 8430
		private ArchiveConnectivityResultEnum result;
	}
}
