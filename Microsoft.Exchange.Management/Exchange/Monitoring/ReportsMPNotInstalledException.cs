using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F09 RID: 3849
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReportsMPNotInstalledException : LocalizedException
	{
		// Token: 0x0600AA16 RID: 43542 RVA: 0x0028CC52 File Offset: 0x0028AE52
		public ReportsMPNotInstalledException() : base(Strings.ReportsMPNotInstalled)
		{
		}

		// Token: 0x0600AA17 RID: 43543 RVA: 0x0028CC5F File Offset: 0x0028AE5F
		public ReportsMPNotInstalledException(Exception innerException) : base(Strings.ReportsMPNotInstalled, innerException)
		{
		}

		// Token: 0x0600AA18 RID: 43544 RVA: 0x0028CC6D File Offset: 0x0028AE6D
		protected ReportsMPNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AA19 RID: 43545 RVA: 0x0028CC77 File Offset: 0x0028AE77
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
