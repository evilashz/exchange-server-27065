using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F81 RID: 3969
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NeedToSpecifyServerObjectException : LocalizedException
	{
		// Token: 0x0600AC65 RID: 44133 RVA: 0x00290355 File Offset: 0x0028E555
		public NeedToSpecifyServerObjectException() : base(Strings.NeedToSpecifyServerObject)
		{
		}

		// Token: 0x0600AC66 RID: 44134 RVA: 0x00290362 File Offset: 0x0028E562
		public NeedToSpecifyServerObjectException(Exception innerException) : base(Strings.NeedToSpecifyServerObject, innerException)
		{
		}

		// Token: 0x0600AC67 RID: 44135 RVA: 0x00290370 File Offset: 0x0028E570
		protected NeedToSpecifyServerObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC68 RID: 44136 RVA: 0x0029037A File Offset: 0x0028E57A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
