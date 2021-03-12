using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000734 RID: 1844
	[Serializable]
	public class WrongServerException : ConnectionFailedPermanentException
	{
		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x001306E2 File Offset: 0x0012E8E2
		// (set) Token: 0x060047E4 RID: 18404 RVA: 0x001306EA File Offset: 0x0012E8EA
		public Guid MdbGuid { get; private set; }

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x060047E5 RID: 18405 RVA: 0x001306F3 File Offset: 0x0012E8F3
		// (set) Token: 0x060047E6 RID: 18406 RVA: 0x001306FB File Offset: 0x0012E8FB
		public string RightServerFqdn { get; private set; }

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x00130704 File Offset: 0x0012E904
		// (set) Token: 0x060047E8 RID: 18408 RVA: 0x0013070C File Offset: 0x0012E90C
		public int RightServerVersion { get; private set; }

		// Token: 0x060047E9 RID: 18409 RVA: 0x00130715 File Offset: 0x0012E915
		public WrongServerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x0013071E File Offset: 0x0012E91E
		public WrongServerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x00130728 File Offset: 0x0012E928
		public WrongServerException(LocalizedString message, Guid mdbGuid, string rightServerFqdn, int rightServerVersion, Exception innerException = null) : base(message, innerException)
		{
			this.MdbGuid = mdbGuid;
			this.RightServerFqdn = rightServerFqdn;
			this.RightServerVersion = rightServerVersion;
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x00130749 File Offset: 0x0012E949
		protected WrongServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x00130753 File Offset: 0x0012E953
		public string RightServerToString()
		{
			if (string.IsNullOrEmpty(this.RightServerFqdn))
			{
				return string.Empty;
			}
			return string.Format("{0}~{1}~{2}", this.MdbGuid, this.RightServerFqdn, this.RightServerVersion);
		}
	}
}
