using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000438 RID: 1080
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederOperationFailedWithEcException : SeederServerException
	{
		// Token: 0x06002AA5 RID: 10917 RVA: 0x000BBFEB File Offset: 0x000BA1EB
		public SeederOperationFailedWithEcException(int ec, string errMessage) : base(ReplayStrings.SeederOperationFailedWithEcException(ec, errMessage))
		{
			this.ec = ec;
			this.errMessage = errMessage;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000BC00D File Offset: 0x000BA20D
		public SeederOperationFailedWithEcException(int ec, string errMessage, Exception innerException) : base(ReplayStrings.SeederOperationFailedWithEcException(ec, errMessage), innerException)
		{
			this.ec = ec;
			this.errMessage = errMessage;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000BC030 File Offset: 0x000BA230
		protected SeederOperationFailedWithEcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ec = (int)info.GetValue("ec", typeof(int));
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000BC085 File Offset: 0x000BA285
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ec", this.ec);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x000BC0B1 File Offset: 0x000BA2B1
		public int Ec
		{
			get
			{
				return this.ec;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x000BC0B9 File Offset: 0x000BA2B9
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x0400146C RID: 5228
		private readonly int ec;

		// Token: 0x0400146D RID: 5229
		private readonly string errMessage;
	}
}
