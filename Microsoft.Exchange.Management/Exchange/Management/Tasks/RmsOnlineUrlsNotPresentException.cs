using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010E8 RID: 4328
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsOnlineUrlsNotPresentException : LocalizedException
	{
		// Token: 0x0600B376 RID: 45942 RVA: 0x0029B44C File Offset: 0x0029964C
		public RmsOnlineUrlsNotPresentException() : base(Strings.RmsOnlineUrlsNotPresent)
		{
		}

		// Token: 0x0600B377 RID: 45943 RVA: 0x0029B459 File Offset: 0x00299659
		public RmsOnlineUrlsNotPresentException(Exception innerException) : base(Strings.RmsOnlineUrlsNotPresent, innerException)
		{
		}

		// Token: 0x0600B378 RID: 45944 RVA: 0x0029B467 File Offset: 0x00299667
		protected RmsOnlineUrlsNotPresentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B379 RID: 45945 RVA: 0x0029B471 File Offset: 0x00299671
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
