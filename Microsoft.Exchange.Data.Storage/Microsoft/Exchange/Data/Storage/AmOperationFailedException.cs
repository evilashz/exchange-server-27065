using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000CC RID: 204
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmOperationFailedException : AmServerException
	{
		// Token: 0x06001278 RID: 4728 RVA: 0x00067B1A File Offset: 0x00065D1A
		public AmOperationFailedException(string errMessage) : base(ServerStrings.AmOperationFailedException(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00067B34 File Offset: 0x00065D34
		public AmOperationFailedException(string errMessage, Exception innerException) : base(ServerStrings.AmOperationFailedException(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00067B4F File Offset: 0x00065D4F
		protected AmOperationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00067B79 File Offset: 0x00065D79
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00067B94 File Offset: 0x00065D94
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x0400095A RID: 2394
		private readonly string errMessage;
	}
}
