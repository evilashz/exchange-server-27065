using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E3E RID: 3646
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrganizationValidationException : OrganizationTaskException
	{
		// Token: 0x0600A63F RID: 42559 RVA: 0x002875A9 File Offset: 0x002857A9
		public OrganizationValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A640 RID: 42560 RVA: 0x002875B2 File Offset: 0x002857B2
		public OrganizationValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A641 RID: 42561 RVA: 0x002875BC File Offset: 0x002857BC
		protected OrganizationValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A642 RID: 42562 RVA: 0x002875C6 File Offset: 0x002857C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
