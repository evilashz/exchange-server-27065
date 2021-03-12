using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000061 RID: 97
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class StoreRestartedException : TransientException
	{
		// Token: 0x06000271 RID: 625 RVA: 0x00006BE7 File Offset: 0x00004DE7
		public StoreRestartedException() : base(Strings.StoreRestartedException)
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public StoreRestartedException(Exception innerException) : base(Strings.StoreRestartedException, innerException)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00006C02 File Offset: 0x00004E02
		protected StoreRestartedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00006C0C File Offset: 0x00004E0C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
