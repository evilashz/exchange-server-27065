using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001122 RID: 4386
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TargetEndpointRequiredException : LocalizedException
	{
		// Token: 0x0600B497 RID: 46231 RVA: 0x0029D003 File Offset: 0x0029B203
		public TargetEndpointRequiredException() : base(Strings.ErrorTargetEndpointRequired)
		{
		}

		// Token: 0x0600B498 RID: 46232 RVA: 0x0029D010 File Offset: 0x0029B210
		public TargetEndpointRequiredException(Exception innerException) : base(Strings.ErrorTargetEndpointRequired, innerException)
		{
		}

		// Token: 0x0600B499 RID: 46233 RVA: 0x0029D01E File Offset: 0x0029B21E
		protected TargetEndpointRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B49A RID: 46234 RVA: 0x0029D028 File Offset: 0x0029B228
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
