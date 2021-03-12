using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000CD RID: 205
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmOperationFailedWithEcException : AmServerException
	{
		// Token: 0x0600127D RID: 4733 RVA: 0x00067B9C File Offset: 0x00065D9C
		public AmOperationFailedWithEcException(int ec) : base(ServerStrings.AmOperationFailedWithEcException(ec))
		{
			this.ec = ec;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00067BB6 File Offset: 0x00065DB6
		public AmOperationFailedWithEcException(int ec, Exception innerException) : base(ServerStrings.AmOperationFailedWithEcException(ec), innerException)
		{
			this.ec = ec;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00067BD1 File Offset: 0x00065DD1
		protected AmOperationFailedWithEcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ec = (int)info.GetValue("ec", typeof(int));
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00067BFB File Offset: 0x00065DFB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ec", this.ec);
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x00067C16 File Offset: 0x00065E16
		public int Ec
		{
			get
			{
				return this.ec;
			}
		}

		// Token: 0x0400095B RID: 2395
		private readonly int ec;
	}
}
