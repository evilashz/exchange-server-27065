using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000024 RID: 36
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class HttpResponseStreamNullException : LocalizedException
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00005514 File Offset: 0x00003714
		public HttpResponseStreamNullException() : base(Strings.HttpResponseStreamNullException)
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005521 File Offset: 0x00003721
		public HttpResponseStreamNullException(Exception innerException) : base(Strings.HttpResponseStreamNullException, innerException)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000552F File Offset: 0x0000372F
		protected HttpResponseStreamNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005539 File Offset: 0x00003739
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
