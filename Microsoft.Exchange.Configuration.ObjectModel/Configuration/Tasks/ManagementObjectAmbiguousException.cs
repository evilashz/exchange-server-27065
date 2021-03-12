using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002BE RID: 702
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ManagementObjectAmbiguousException : LocalizedException
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x0005CD47 File Offset: 0x0005AF47
		public ManagementObjectAmbiguousException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0005CD50 File Offset: 0x0005AF50
		public ManagementObjectAmbiguousException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0005CD5A File Offset: 0x0005AF5A
		protected ManagementObjectAmbiguousException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0005CD64 File Offset: 0x0005AF64
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
