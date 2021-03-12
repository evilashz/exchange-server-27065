using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F14 RID: 3860
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthTaskCasHasNoTopologySiteException : LocalizedException
	{
		// Token: 0x0600AA55 RID: 43605 RVA: 0x0028D42C File Offset: 0x0028B62C
		public CasHealthTaskCasHasNoTopologySiteException() : base(Strings.CasHealthTaskCasHasNoTopologySite)
		{
		}

		// Token: 0x0600AA56 RID: 43606 RVA: 0x0028D439 File Offset: 0x0028B639
		public CasHealthTaskCasHasNoTopologySiteException(Exception innerException) : base(Strings.CasHealthTaskCasHasNoTopologySite, innerException)
		{
		}

		// Token: 0x0600AA57 RID: 43607 RVA: 0x0028D447 File Offset: 0x0028B647
		protected CasHealthTaskCasHasNoTopologySiteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AA58 RID: 43608 RVA: 0x0028D451 File Offset: 0x0028B651
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
