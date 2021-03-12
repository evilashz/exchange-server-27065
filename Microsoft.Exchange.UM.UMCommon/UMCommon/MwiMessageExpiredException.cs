using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001DA RID: 474
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MwiMessageExpiredException : MwiDeliveryException
	{
		// Token: 0x06000F63 RID: 3939 RVA: 0x000365EE File Offset: 0x000347EE
		public MwiMessageExpiredException(string userName) : base(Strings.descMwiMessageExpiredError(userName))
		{
			this.userName = userName;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00036603 File Offset: 0x00034803
		public MwiMessageExpiredException(string userName, Exception innerException) : base(Strings.descMwiMessageExpiredError(userName), innerException)
		{
			this.userName = userName;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00036619 File Offset: 0x00034819
		protected MwiMessageExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00036643 File Offset: 0x00034843
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0003665E File Offset: 0x0003485E
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x040007AB RID: 1963
		private readonly string userName;
	}
}
