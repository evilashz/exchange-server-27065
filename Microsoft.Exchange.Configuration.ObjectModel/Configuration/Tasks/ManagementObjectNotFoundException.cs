using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002BC RID: 700
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ManagementObjectNotFoundException : LocalizedException
	{
		// Token: 0x06001920 RID: 6432 RVA: 0x0005CCF9 File Offset: 0x0005AEF9
		public ManagementObjectNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0005CD02 File Offset: 0x0005AF02
		public ManagementObjectNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0005CD0C File Offset: 0x0005AF0C
		protected ManagementObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x0005CD16 File Offset: 0x0005AF16
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
