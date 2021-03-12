using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E6A RID: 3690
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDSecretAnswerContainsPassword : WLCDMemberException
	{
		// Token: 0x0600A6F6 RID: 42742 RVA: 0x00287E9B File Offset: 0x0028609B
		public WLCDSecretAnswerContainsPassword(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6F7 RID: 42743 RVA: 0x00287EA4 File Offset: 0x002860A4
		public WLCDSecretAnswerContainsPassword(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6F8 RID: 42744 RVA: 0x00287EAE File Offset: 0x002860AE
		protected WLCDSecretAnswerContainsPassword(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6F9 RID: 42745 RVA: 0x00287EB8 File Offset: 0x002860B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
