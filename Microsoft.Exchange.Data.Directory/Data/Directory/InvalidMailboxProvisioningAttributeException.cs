using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B0F RID: 2831
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidMailboxProvisioningAttributeException : DataSourceOperationException
	{
		// Token: 0x0600820A RID: 33290 RVA: 0x001A7B7D File Offset: 0x001A5D7D
		public InvalidMailboxProvisioningAttributeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600820B RID: 33291 RVA: 0x001A7B86 File Offset: 0x001A5D86
		public InvalidMailboxProvisioningAttributeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600820C RID: 33292 RVA: 0x001A7B90 File Offset: 0x001A5D90
		protected InvalidMailboxProvisioningAttributeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600820D RID: 33293 RVA: 0x001A7B9A File Offset: 0x001A5D9A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
