using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010CF RID: 4303
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoRMSServersFoundException : LocalizedException
	{
		// Token: 0x0600B2FF RID: 45823 RVA: 0x0029A9E9 File Offset: 0x00298BE9
		public NoRMSServersFoundException() : base(Strings.NoRMSServersFoundException)
		{
		}

		// Token: 0x0600B300 RID: 45824 RVA: 0x0029A9F6 File Offset: 0x00298BF6
		public NoRMSServersFoundException(Exception innerException) : base(Strings.NoRMSServersFoundException, innerException)
		{
		}

		// Token: 0x0600B301 RID: 45825 RVA: 0x0029AA04 File Offset: 0x00298C04
		protected NoRMSServersFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B302 RID: 45826 RVA: 0x0029AA0E File Offset: 0x00298C0E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
