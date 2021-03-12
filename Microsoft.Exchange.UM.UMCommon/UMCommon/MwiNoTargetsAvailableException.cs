using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001D9 RID: 473
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MwiNoTargetsAvailableException : MwiDeliveryException
	{
		// Token: 0x06000F5E RID: 3934 RVA: 0x00036576 File Offset: 0x00034776
		public MwiNoTargetsAvailableException(string userName) : base(Strings.descMwiNoTargetsAvailableError(userName))
		{
			this.userName = userName;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0003658B File Offset: 0x0003478B
		public MwiNoTargetsAvailableException(string userName, Exception innerException) : base(Strings.descMwiNoTargetsAvailableError(userName), innerException)
		{
			this.userName = userName;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x000365A1 File Offset: 0x000347A1
		protected MwiNoTargetsAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x000365CB File Offset: 0x000347CB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x000365E6 File Offset: 0x000347E6
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x040007AA RID: 1962
		private readonly string userName;
	}
}
