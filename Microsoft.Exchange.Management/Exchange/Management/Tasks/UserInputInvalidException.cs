using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E79 RID: 3705
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserInputInvalidException : RecipientTaskException
	{
		// Token: 0x0600A732 RID: 42802 RVA: 0x002880E4 File Offset: 0x002862E4
		public UserInputInvalidException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A733 RID: 42803 RVA: 0x002880ED File Offset: 0x002862ED
		public UserInputInvalidException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A734 RID: 42804 RVA: 0x002880F7 File Offset: 0x002862F7
		protected UserInputInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A735 RID: 42805 RVA: 0x00288101 File Offset: 0x00286301
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
