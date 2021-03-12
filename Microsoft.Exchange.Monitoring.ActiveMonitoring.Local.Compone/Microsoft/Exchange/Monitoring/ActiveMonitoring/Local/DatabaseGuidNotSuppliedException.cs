using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x0200059F RID: 1439
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DatabaseGuidNotSuppliedException : LocalizedException
	{
		// Token: 0x060026B7 RID: 9911 RVA: 0x000DDE3C File Offset: 0x000DC03C
		public DatabaseGuidNotSuppliedException() : base(Strings.DatabaseGuidNotSupplied)
		{
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000DDE49 File Offset: 0x000DC049
		public DatabaseGuidNotSuppliedException(Exception innerException) : base(Strings.DatabaseGuidNotSupplied, innerException)
		{
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000DDE57 File Offset: 0x000DC057
		protected DatabaseGuidNotSuppliedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000DDE61 File Offset: 0x000DC061
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
