using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011D2 RID: 4562
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADError : LocalizedException
	{
		// Token: 0x0600B8F9 RID: 47353 RVA: 0x002A53F6 File Offset: 0x002A35F6
		public ADError() : base(Strings.ADError)
		{
		}

		// Token: 0x0600B8FA RID: 47354 RVA: 0x002A5403 File Offset: 0x002A3603
		public ADError(Exception innerException) : base(Strings.ADError, innerException)
		{
		}

		// Token: 0x0600B8FB RID: 47355 RVA: 0x002A5411 File Offset: 0x002A3611
		protected ADError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8FC RID: 47356 RVA: 0x002A541B File Offset: 0x002A361B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
