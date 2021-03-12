using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000178 RID: 376
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TaskObjectIsTooAdvancedException : LocalizedException
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x000361EB File Offset: 0x000343EB
		public TaskObjectIsTooAdvancedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x000361F4 File Offset: 0x000343F4
		public TaskObjectIsTooAdvancedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x000361FE File Offset: 0x000343FE
		protected TaskObjectIsTooAdvancedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00036208 File Offset: 0x00034408
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
