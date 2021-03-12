using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000124 RID: 292
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ActiveMonitoringOperationFailedException : ActiveMonitoringServerException
	{
		// Token: 0x06001440 RID: 5184 RVA: 0x0006A706 File Offset: 0x00068906
		public ActiveMonitoringOperationFailedException(string errMessage) : base(ServerStrings.ActiveMonitoringOperationFailedException(errMessage))
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0006A720 File Offset: 0x00068920
		public ActiveMonitoringOperationFailedException(string errMessage, Exception innerException) : base(ServerStrings.ActiveMonitoringOperationFailedException(errMessage), innerException)
		{
			this.errMessage = errMessage;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0006A73B File Offset: 0x0006893B
		protected ActiveMonitoringOperationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMessage = (string)info.GetValue("errMessage", typeof(string));
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0006A765 File Offset: 0x00068965
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMessage", this.errMessage);
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0006A780 File Offset: 0x00068980
		public string ErrMessage
		{
			get
			{
				return this.errMessage;
			}
		}

		// Token: 0x040009AF RID: 2479
		private readonly string errMessage;
	}
}
