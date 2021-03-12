using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ADC RID: 2780
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlsTransientException : TransientGlobalException
	{
		// Token: 0x06008108 RID: 33032 RVA: 0x001A622F File Offset: 0x001A442F
		public GlsTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008109 RID: 33033 RVA: 0x001A6238 File Offset: 0x001A4438
		public GlsTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600810A RID: 33034 RVA: 0x001A6242 File Offset: 0x001A4442
		protected GlsTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600810B RID: 33035 RVA: 0x001A624C File Offset: 0x001A444C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
