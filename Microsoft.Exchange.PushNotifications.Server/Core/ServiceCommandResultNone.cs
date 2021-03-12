using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.PushNotifications.Server.Core
{
	// Token: 0x02000009 RID: 9
	[DataContract]
	public class ServiceCommandResultNone
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002966 File Offset: 0x00000B66
		private ServiceCommandResultNone()
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000296E File Offset: 0x00000B6E
		public static ServiceCommandResultNone Instance
		{
			get
			{
				return ServiceCommandResultNone.instance;
			}
		}

		// Token: 0x04000023 RID: 35
		private static readonly ServiceCommandResultNone instance = new ServiceCommandResultNone();
	}
}
