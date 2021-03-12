using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E41 RID: 3649
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidUserInputException : CannotDetermineManagementEndpointException
	{
		// Token: 0x0600A64B RID: 42571 RVA: 0x0028761E File Offset: 0x0028581E
		public InvalidUserInputException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A64C RID: 42572 RVA: 0x00287627 File Offset: 0x00285827
		public InvalidUserInputException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A64D RID: 42573 RVA: 0x00287631 File Offset: 0x00285831
		protected InvalidUserInputException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A64E RID: 42574 RVA: 0x0028763B File Offset: 0x0028583B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
