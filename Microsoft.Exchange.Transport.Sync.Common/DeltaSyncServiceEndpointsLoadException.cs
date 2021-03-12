using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000023 RID: 35
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DeltaSyncServiceEndpointsLoadException : LocalizedException
	{
		// Token: 0x06000158 RID: 344 RVA: 0x000054E5 File Offset: 0x000036E5
		public DeltaSyncServiceEndpointsLoadException() : base(Strings.DeltaSyncServiceEndpointsLoadException)
		{
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000054F2 File Offset: 0x000036F2
		public DeltaSyncServiceEndpointsLoadException(Exception innerException) : base(Strings.DeltaSyncServiceEndpointsLoadException, innerException)
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005500 File Offset: 0x00003700
		protected DeltaSyncServiceEndpointsLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000550A File Offset: 0x0000370A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
