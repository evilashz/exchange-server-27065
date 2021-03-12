using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E47 RID: 3655
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserWithMatchingNetIdAndDifferentWindowsLiveIdExistsException : RecipientTaskException
	{
		// Token: 0x0600A663 RID: 42595 RVA: 0x00287708 File Offset: 0x00285908
		public UserWithMatchingNetIdAndDifferentWindowsLiveIdExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A664 RID: 42596 RVA: 0x00287711 File Offset: 0x00285911
		public UserWithMatchingNetIdAndDifferentWindowsLiveIdExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A665 RID: 42597 RVA: 0x0028771B File Offset: 0x0028591B
		protected UserWithMatchingNetIdAndDifferentWindowsLiveIdExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A666 RID: 42598 RVA: 0x00287725 File Offset: 0x00285925
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
