using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E43 RID: 3651
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RedirectionEntryManagerTransientException : RedirectionEntryManagerException
	{
		// Token: 0x0600A653 RID: 42579 RVA: 0x0028766C File Offset: 0x0028586C
		public RedirectionEntryManagerTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A654 RID: 42580 RVA: 0x00287675 File Offset: 0x00285875
		public RedirectionEntryManagerTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A655 RID: 42581 RVA: 0x0028767F File Offset: 0x0028587F
		protected RedirectionEntryManagerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A656 RID: 42582 RVA: 0x00287689 File Offset: 0x00285889
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
