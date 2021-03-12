using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002AE RID: 686
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeedPrepareException : SeederServerException
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x00073816 File Offset: 0x00071A16
		public SeedPrepareException(string errMessage) : base(ReplayStrings.SeedPrepareException(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00073830 File Offset: 0x00071A30
		public SeedPrepareException(string errMessage, Exception innerException) : base(ReplayStrings.SeedPrepareException(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0007384B File Offset: 0x00071A4B
		protected SeedPrepareException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00073875 File Offset: 0x00071A75
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x00073890 File Offset: 0x00071A90
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x04000AC1 RID: 2753
		private readonly string errMessage;
	}
}
