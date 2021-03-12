using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002BF RID: 703
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ManagementObjectDuplicateException : LocalizedException
	{
		// Token: 0x0600192C RID: 6444 RVA: 0x0005CD6E File Offset: 0x0005AF6E
		public ManagementObjectDuplicateException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0005CD77 File Offset: 0x0005AF77
		public ManagementObjectDuplicateException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0005CD81 File Offset: 0x0005AF81
		protected ManagementObjectDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0005CD8B File Offset: 0x0005AF8B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
