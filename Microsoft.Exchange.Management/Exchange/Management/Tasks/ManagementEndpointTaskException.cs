using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E3F RID: 3647
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ManagementEndpointTaskException : LocalizedException
	{
		// Token: 0x0600A643 RID: 42563 RVA: 0x002875D0 File Offset: 0x002857D0
		public ManagementEndpointTaskException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A644 RID: 42564 RVA: 0x002875D9 File Offset: 0x002857D9
		public ManagementEndpointTaskException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A645 RID: 42565 RVA: 0x002875E3 File Offset: 0x002857E3
		protected ManagementEndpointTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A646 RID: 42566 RVA: 0x002875ED File Offset: 0x002857ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
