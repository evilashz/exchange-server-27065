using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200005B RID: 91
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidSyncEngineStateException : LocalizedException
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00006ACD File Offset: 0x00004CCD
		public InvalidSyncEngineStateException() : base(Strings.InvalidSyncEngineStateException)
		{
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00006ADA File Offset: 0x00004CDA
		public InvalidSyncEngineStateException(Exception innerException) : base(Strings.InvalidSyncEngineStateException, innerException)
		{
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00006AE8 File Offset: 0x00004CE8
		protected InvalidSyncEngineStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00006AF2 File Offset: 0x00004CF2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
