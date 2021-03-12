using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001117 RID: 4375
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TargetDatabasesNotAllowedException : LocalizedException
	{
		// Token: 0x0600B462 RID: 46178 RVA: 0x0029CB41 File Offset: 0x0029AD41
		public TargetDatabasesNotAllowedException() : base(Strings.TargetDatabasesNotAllowed)
		{
		}

		// Token: 0x0600B463 RID: 46179 RVA: 0x0029CB4E File Offset: 0x0029AD4E
		public TargetDatabasesNotAllowedException(Exception innerException) : base(Strings.TargetDatabasesNotAllowed, innerException)
		{
		}

		// Token: 0x0600B464 RID: 46180 RVA: 0x0029CB5C File Offset: 0x0029AD5C
		protected TargetDatabasesNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B465 RID: 46181 RVA: 0x0029CB66 File Offset: 0x0029AD66
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
