using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000DE RID: 222
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbMountNotAllowedDueToAcllErrorException : AmServerException
	{
		// Token: 0x060012D0 RID: 4816 RVA: 0x000683D8 File Offset: 0x000665D8
		public AmDbMountNotAllowedDueToAcllErrorException(string errMessage, long numLogsLost) : base(ServerStrings.AmDbMountNotAllowedDueToAcllErrorException(errMessage, numLogsLost))
		{
			this.errMessage = errMessage;
			this.numLogsLost = numLogsLost;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x000683FA File Offset: 0x000665FA
		public AmDbMountNotAllowedDueToAcllErrorException(string errMessage, long numLogsLost, Exception innerException) : base(ServerStrings.AmDbMountNotAllowedDueToAcllErrorException(errMessage, numLogsLost), innerException)
		{
			this.errMessage = errMessage;
			this.numLogsLost = numLogsLost;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00068420 File Offset: 0x00066620
		protected AmDbMountNotAllowedDueToAcllErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
			this.numLogsLost = (long)info.GetValue("numLogsLost", typeof(long));
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00068475 File Offset: 0x00066675
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
			info.AddValue("numLogsLost", this.numLogsLost);
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x000684A1 File Offset: 0x000666A1
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x000684A9 File Offset: 0x000666A9
		public long NumLogsLost
		{
			get
			{
				return this.numLogsLost;
			}
		}

		// Token: 0x0400096A RID: 2410
		private readonly string errMessage;

		// Token: 0x0400096B RID: 2411
		private readonly long numLogsLost;
	}
}
