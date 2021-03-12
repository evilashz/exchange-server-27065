using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public sealed class PushNotificationServerException<T> : LocalizedException where T : PushNotificationFault
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002F54 File Offset: 0x00001154
		public PushNotificationServerException(T faultContract, Exception error = null) : base(Strings.PushNotificationServerExceptionMessage(faultContract.Message))
		{
			this.FaultContract = faultContract;
			this.Exception = error;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F7C File Offset: 0x0000117C
		public PushNotificationServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.FaultContract = (T)((object)info.GetValue("FaultContract", typeof(T)));
			this.Exception = (Exception)info.GetValue("Exception", typeof(Exception));
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002FD1 File Offset: 0x000011D1
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002FD9 File Offset: 0x000011D9
		public T FaultContract { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002FE2 File Offset: 0x000011E2
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002FEA File Offset: 0x000011EA
		public Exception Exception { get; private set; }

		// Token: 0x0600006F RID: 111 RVA: 0x00002FF3 File Offset: 0x000011F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FaultContract", this.FaultContract);
			info.AddValue("Exception", this.Exception);
		}
	}
}
