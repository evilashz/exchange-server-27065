using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001121 RID: 4385
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceEndpointRequiredException : LocalizedException
	{
		// Token: 0x0600B493 RID: 46227 RVA: 0x0029CFD4 File Offset: 0x0029B1D4
		public SourceEndpointRequiredException() : base(Strings.ErrorSourceEndpointRequired)
		{
		}

		// Token: 0x0600B494 RID: 46228 RVA: 0x0029CFE1 File Offset: 0x0029B1E1
		public SourceEndpointRequiredException(Exception innerException) : base(Strings.ErrorSourceEndpointRequired, innerException)
		{
		}

		// Token: 0x0600B495 RID: 46229 RVA: 0x0029CFEF File Offset: 0x0029B1EF
		protected SourceEndpointRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B496 RID: 46230 RVA: 0x0029CFF9 File Offset: 0x0029B1F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
