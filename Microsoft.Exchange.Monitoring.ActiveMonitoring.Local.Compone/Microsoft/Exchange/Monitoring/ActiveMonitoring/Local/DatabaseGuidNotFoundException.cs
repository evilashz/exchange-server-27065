using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x0200059E RID: 1438
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DatabaseGuidNotFoundException : LocalizedException
	{
		// Token: 0x060026B3 RID: 9907 RVA: 0x000DDE0D File Offset: 0x000DC00D
		public DatabaseGuidNotFoundException() : base(Strings.DatabaseGuidNotFound)
		{
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000DDE1A File Offset: 0x000DC01A
		public DatabaseGuidNotFoundException(Exception innerException) : base(Strings.DatabaseGuidNotFound, innerException)
		{
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000DDE28 File Offset: 0x000DC028
		protected DatabaseGuidNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000DDE32 File Offset: 0x000DC032
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
