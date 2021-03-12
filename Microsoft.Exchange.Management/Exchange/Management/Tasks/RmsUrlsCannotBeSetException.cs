using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010D8 RID: 4312
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsUrlsCannotBeSetException : LocalizedException
	{
		// Token: 0x0600B32B RID: 45867 RVA: 0x0029ADFE File Offset: 0x00298FFE
		public RmsUrlsCannotBeSetException() : base(Strings.RmsUrlsCannotBeSet)
		{
		}

		// Token: 0x0600B32C RID: 45868 RVA: 0x0029AE0B File Offset: 0x0029900B
		public RmsUrlsCannotBeSetException(Exception innerException) : base(Strings.RmsUrlsCannotBeSet, innerException)
		{
		}

		// Token: 0x0600B32D RID: 45869 RVA: 0x0029AE19 File Offset: 0x00299019
		protected RmsUrlsCannotBeSetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B32E RID: 45870 RVA: 0x0029AE23 File Offset: 0x00299023
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
