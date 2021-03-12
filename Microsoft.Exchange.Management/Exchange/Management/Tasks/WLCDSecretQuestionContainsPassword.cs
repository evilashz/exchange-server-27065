using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E69 RID: 3689
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDSecretQuestionContainsPassword : WLCDMemberException
	{
		// Token: 0x0600A6F2 RID: 42738 RVA: 0x00287E74 File Offset: 0x00286074
		public WLCDSecretQuestionContainsPassword(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6F3 RID: 42739 RVA: 0x00287E7D File Offset: 0x0028607D
		public WLCDSecretQuestionContainsPassword(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6F4 RID: 42740 RVA: 0x00287E87 File Offset: 0x00286087
		protected WLCDSecretQuestionContainsPassword(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6F5 RID: 42741 RVA: 0x00287E91 File Offset: 0x00286091
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
