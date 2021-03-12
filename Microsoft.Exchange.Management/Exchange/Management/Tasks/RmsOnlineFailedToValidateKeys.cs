using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010F0 RID: 4336
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsOnlineFailedToValidateKeys : LocalizedException
	{
		// Token: 0x0600B39C RID: 45980 RVA: 0x0029B794 File Offset: 0x00299994
		public RmsOnlineFailedToValidateKeys() : base(Strings.RmsOnlineFailedToValidateKeys)
		{
		}

		// Token: 0x0600B39D RID: 45981 RVA: 0x0029B7A1 File Offset: 0x002999A1
		public RmsOnlineFailedToValidateKeys(Exception innerException) : base(Strings.RmsOnlineFailedToValidateKeys, innerException)
		{
		}

		// Token: 0x0600B39E RID: 45982 RVA: 0x0029B7AF File Offset: 0x002999AF
		protected RmsOnlineFailedToValidateKeys(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B39F RID: 45983 RVA: 0x0029B7B9 File Offset: 0x002999B9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
