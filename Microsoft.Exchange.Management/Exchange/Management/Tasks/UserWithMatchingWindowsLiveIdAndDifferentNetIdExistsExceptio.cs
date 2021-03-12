using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E48 RID: 3656
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserWithMatchingWindowsLiveIdAndDifferentNetIdExistsException : RecipientTaskException
	{
		// Token: 0x0600A667 RID: 42599 RVA: 0x0028772F File Offset: 0x0028592F
		public UserWithMatchingWindowsLiveIdAndDifferentNetIdExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A668 RID: 42600 RVA: 0x00287738 File Offset: 0x00285938
		public UserWithMatchingWindowsLiveIdAndDifferentNetIdExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A669 RID: 42601 RVA: 0x00287742 File Offset: 0x00285942
		protected UserWithMatchingWindowsLiveIdAndDifferentNetIdExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A66A RID: 42602 RVA: 0x0028774C File Offset: 0x0028594C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
