using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001174 RID: 4468
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidAvailabilityAccessMethodException : LocalizedException
	{
		// Token: 0x0600B62F RID: 46639 RVA: 0x0029F699 File Offset: 0x0029D899
		public InvalidAvailabilityAccessMethodException() : base(Strings.InvalidAvailabilityAccessMethod)
		{
		}

		// Token: 0x0600B630 RID: 46640 RVA: 0x0029F6A6 File Offset: 0x0029D8A6
		public InvalidAvailabilityAccessMethodException(Exception innerException) : base(Strings.InvalidAvailabilityAccessMethod, innerException)
		{
		}

		// Token: 0x0600B631 RID: 46641 RVA: 0x0029F6B4 File Offset: 0x0029D8B4
		protected InvalidAvailabilityAccessMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B632 RID: 46642 RVA: 0x0029F6BE File Offset: 0x0029D8BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
