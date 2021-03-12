using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F12 RID: 3858
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthTaskNotRunOnExchangeServerException : LocalizedException
	{
		// Token: 0x0600AA4C RID: 43596 RVA: 0x0028D385 File Offset: 0x0028B585
		public CasHealthTaskNotRunOnExchangeServerException() : base(Strings.CasHealthTaskNotRunOnExchangeServer)
		{
		}

		// Token: 0x0600AA4D RID: 43597 RVA: 0x0028D392 File Offset: 0x0028B592
		public CasHealthTaskNotRunOnExchangeServerException(Exception innerException) : base(Strings.CasHealthTaskNotRunOnExchangeServer, innerException)
		{
		}

		// Token: 0x0600AA4E RID: 43598 RVA: 0x0028D3A0 File Offset: 0x0028B5A0
		protected CasHealthTaskNotRunOnExchangeServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AA4F RID: 43599 RVA: 0x0028D3AA File Offset: 0x0028B5AA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
