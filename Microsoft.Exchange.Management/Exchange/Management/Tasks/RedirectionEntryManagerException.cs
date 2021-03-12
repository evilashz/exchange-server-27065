using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E42 RID: 3650
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RedirectionEntryManagerException : LocalizedException
	{
		// Token: 0x0600A64F RID: 42575 RVA: 0x00287645 File Offset: 0x00285845
		public RedirectionEntryManagerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A650 RID: 42576 RVA: 0x0028764E File Offset: 0x0028584E
		public RedirectionEntryManagerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A651 RID: 42577 RVA: 0x00287658 File Offset: 0x00285858
		protected RedirectionEntryManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A652 RID: 42578 RVA: 0x00287662 File Offset: 0x00285862
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
