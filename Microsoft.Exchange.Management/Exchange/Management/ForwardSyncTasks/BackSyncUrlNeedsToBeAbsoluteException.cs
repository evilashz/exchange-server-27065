using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001135 RID: 4405
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BackSyncUrlNeedsToBeAbsoluteException : LocalizedException
	{
		// Token: 0x0600B4F2 RID: 46322 RVA: 0x0029D7F1 File Offset: 0x0029B9F1
		public BackSyncUrlNeedsToBeAbsoluteException() : base(Strings.BackSyncUrlNeedsToBeAbsolute)
		{
		}

		// Token: 0x0600B4F3 RID: 46323 RVA: 0x0029D7FE File Offset: 0x0029B9FE
		public BackSyncUrlNeedsToBeAbsoluteException(Exception innerException) : base(Strings.BackSyncUrlNeedsToBeAbsolute, innerException)
		{
		}

		// Token: 0x0600B4F4 RID: 46324 RVA: 0x0029D80C File Offset: 0x0029BA0C
		protected BackSyncUrlNeedsToBeAbsoluteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B4F5 RID: 46325 RVA: 0x0029D816 File Offset: 0x0029BA16
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
