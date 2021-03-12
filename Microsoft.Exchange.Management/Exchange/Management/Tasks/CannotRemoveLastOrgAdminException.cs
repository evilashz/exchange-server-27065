using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E4B RID: 3659
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveLastOrgAdminException : RecipientTaskException
	{
		// Token: 0x0600A673 RID: 42611 RVA: 0x002877A4 File Offset: 0x002859A4
		public CannotRemoveLastOrgAdminException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A674 RID: 42612 RVA: 0x002877AD File Offset: 0x002859AD
		public CannotRemoveLastOrgAdminException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A675 RID: 42613 RVA: 0x002877B7 File Offset: 0x002859B7
		protected CannotRemoveLastOrgAdminException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A676 RID: 42614 RVA: 0x002877C1 File Offset: 0x002859C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
