using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E4C RID: 3660
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExternalEmailAddressFormatException : RecipientTaskException
	{
		// Token: 0x0600A677 RID: 42615 RVA: 0x002877CB File Offset: 0x002859CB
		public ExternalEmailAddressFormatException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A678 RID: 42616 RVA: 0x002877D4 File Offset: 0x002859D4
		public ExternalEmailAddressFormatException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A679 RID: 42617 RVA: 0x002877DE File Offset: 0x002859DE
		protected ExternalEmailAddressFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A67A RID: 42618 RVA: 0x002877E8 File Offset: 0x002859E8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
