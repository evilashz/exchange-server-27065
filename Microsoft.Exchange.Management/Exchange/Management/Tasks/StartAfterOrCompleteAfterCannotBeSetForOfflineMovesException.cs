using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E9D RID: 3741
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class StartAfterOrCompleteAfterCannotBeSetForOfflineMovesException : RecipientTaskException
	{
		// Token: 0x0600A7EA RID: 42986 RVA: 0x00289377 File Offset: 0x00287577
		public StartAfterOrCompleteAfterCannotBeSetForOfflineMovesException() : base(Strings.StartAfterOrCompleteAfterCannotBeSetForOfflineMoves)
		{
		}

		// Token: 0x0600A7EB RID: 42987 RVA: 0x00289384 File Offset: 0x00287584
		public StartAfterOrCompleteAfterCannotBeSetForOfflineMovesException(Exception innerException) : base(Strings.StartAfterOrCompleteAfterCannotBeSetForOfflineMoves, innerException)
		{
		}

		// Token: 0x0600A7EC RID: 42988 RVA: 0x00289392 File Offset: 0x00287592
		protected StartAfterOrCompleteAfterCannotBeSetForOfflineMovesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A7ED RID: 42989 RVA: 0x0028939C File Offset: 0x0028759C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
