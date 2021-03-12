using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000CA RID: 202
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AdUserNotFoundException : ObjectNotFoundException
	{
		// Token: 0x0600126E RID: 4718 RVA: 0x00067A4C File Offset: 0x00065C4C
		public AdUserNotFoundException(string errMessage) : base(ServerStrings.AdUserNotFoundException(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00067A61 File Offset: 0x00065C61
		public AdUserNotFoundException(string errMessage, Exception innerException) : base(ServerStrings.AdUserNotFoundException(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00067A77 File Offset: 0x00065C77
		protected AdUserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00067AA1 File Offset: 0x00065CA1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00067ABC File Offset: 0x00065CBC
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x04000959 RID: 2393
		private readonly string errMessage;
	}
}
