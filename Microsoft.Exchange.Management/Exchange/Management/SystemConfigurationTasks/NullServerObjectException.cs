using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F7C RID: 3964
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NullServerObjectException : LocalizedException
	{
		// Token: 0x0600AC51 RID: 44113 RVA: 0x0029026A File Offset: 0x0028E46A
		public NullServerObjectException() : base(Strings.NullServerObject)
		{
		}

		// Token: 0x0600AC52 RID: 44114 RVA: 0x00290277 File Offset: 0x0028E477
		public NullServerObjectException(Exception innerException) : base(Strings.NullServerObject, innerException)
		{
		}

		// Token: 0x0600AC53 RID: 44115 RVA: 0x00290285 File Offset: 0x0028E485
		protected NullServerObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC54 RID: 44116 RVA: 0x0029028F File Offset: 0x0028E48F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
