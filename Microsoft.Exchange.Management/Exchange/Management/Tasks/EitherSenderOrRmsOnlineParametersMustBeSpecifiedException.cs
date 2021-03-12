using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010EC RID: 4332
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EitherSenderOrRmsOnlineParametersMustBeSpecifiedException : LocalizedException
	{
		// Token: 0x0600B389 RID: 45961 RVA: 0x0029B5F0 File Offset: 0x002997F0
		public EitherSenderOrRmsOnlineParametersMustBeSpecifiedException() : base(Strings.EitherSenderOrRmsOnlineParametersMustBeSpecified)
		{
		}

		// Token: 0x0600B38A RID: 45962 RVA: 0x0029B5FD File Offset: 0x002997FD
		public EitherSenderOrRmsOnlineParametersMustBeSpecifiedException(Exception innerException) : base(Strings.EitherSenderOrRmsOnlineParametersMustBeSpecified, innerException)
		{
		}

		// Token: 0x0600B38B RID: 45963 RVA: 0x0029B60B File Offset: 0x0029980B
		protected EitherSenderOrRmsOnlineParametersMustBeSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B38C RID: 45964 RVA: 0x0029B615 File Offset: 0x00299815
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
