using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000EA8 RID: 3752
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMNotAvailableForUserInTargetForestException : LocalizedException
	{
		// Token: 0x0600A81C RID: 43036 RVA: 0x0028974C File Offset: 0x0028794C
		public UMNotAvailableForUserInTargetForestException() : base(Strings.UMNotAvailableForUserInTargetForest)
		{
		}

		// Token: 0x0600A81D RID: 43037 RVA: 0x00289759 File Offset: 0x00287959
		public UMNotAvailableForUserInTargetForestException(Exception innerException) : base(Strings.UMNotAvailableForUserInTargetForest, innerException)
		{
		}

		// Token: 0x0600A81E RID: 43038 RVA: 0x00289767 File Offset: 0x00287967
		protected UMNotAvailableForUserInTargetForestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A81F RID: 43039 RVA: 0x00289771 File Offset: 0x00287971
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
