using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x020010A5 RID: 4261
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ValidateSecretFailureException : LocalizedException
	{
		// Token: 0x0600B235 RID: 45621 RVA: 0x0029981A File Offset: 0x00297A1A
		public ValidateSecretFailureException() : base(Strings.ValidateSecretFailure)
		{
		}

		// Token: 0x0600B236 RID: 45622 RVA: 0x00299827 File Offset: 0x00297A27
		public ValidateSecretFailureException(Exception innerException) : base(Strings.ValidateSecretFailure, innerException)
		{
		}

		// Token: 0x0600B237 RID: 45623 RVA: 0x00299835 File Offset: 0x00297A35
		protected ValidateSecretFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B238 RID: 45624 RVA: 0x0029983F File Offset: 0x00297A3F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
