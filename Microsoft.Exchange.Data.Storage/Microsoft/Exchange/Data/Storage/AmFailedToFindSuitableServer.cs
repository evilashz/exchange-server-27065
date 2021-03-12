using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000D6 RID: 214
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmFailedToFindSuitableServer : AmServerException
	{
		// Token: 0x060012AC RID: 4780 RVA: 0x000680DF File Offset: 0x000662DF
		public AmFailedToFindSuitableServer() : base(ServerStrings.AmFailedToFindSuitableServer)
		{
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000680F1 File Offset: 0x000662F1
		public AmFailedToFindSuitableServer(Exception innerException) : base(ServerStrings.AmFailedToFindSuitableServer, innerException)
		{
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00068104 File Offset: 0x00066304
		protected AmFailedToFindSuitableServer(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0006810E File Offset: 0x0006630E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
