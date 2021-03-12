using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMService.Exceptions;

namespace Microsoft.Exchange.UM.UMService
{
	// Token: 0x02000229 RID: 553
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UMWorkerStartTimeoutException : UMServiceBaseException
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x0003A818 File Offset: 0x00038A18
		public UMWorkerStartTimeoutException(int seconds) : base(Strings.UMWorkerStartTimeoutException(seconds))
		{
			this.seconds = seconds;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0003A832 File Offset: 0x00038A32
		public UMWorkerStartTimeoutException(int seconds, Exception innerException) : base(Strings.UMWorkerStartTimeoutException(seconds), innerException)
		{
			this.seconds = seconds;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0003A84D File Offset: 0x00038A4D
		protected UMWorkerStartTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.seconds = (int)info.GetValue("seconds", typeof(int));
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0003A877 File Offset: 0x00038A77
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("seconds", this.seconds);
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0003A892 File Offset: 0x00038A92
		public int Seconds
		{
			get
			{
				return this.seconds;
			}
		}

		// Token: 0x040008C6 RID: 2246
		private readonly int seconds;
	}
}
