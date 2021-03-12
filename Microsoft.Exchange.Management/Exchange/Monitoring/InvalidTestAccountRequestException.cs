using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F21 RID: 3873
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidTestAccountRequestException : LocalizedException
	{
		// Token: 0x0600AA95 RID: 43669 RVA: 0x0028DA25 File Offset: 0x0028BC25
		public InvalidTestAccountRequestException() : base(Strings.InvalidTestAccountRequest)
		{
		}

		// Token: 0x0600AA96 RID: 43670 RVA: 0x0028DA32 File Offset: 0x0028BC32
		public InvalidTestAccountRequestException(Exception innerException) : base(Strings.InvalidTestAccountRequest, innerException)
		{
		}

		// Token: 0x0600AA97 RID: 43671 RVA: 0x0028DA40 File Offset: 0x0028BC40
		protected InvalidTestAccountRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AA98 RID: 43672 RVA: 0x0028DA4A File Offset: 0x0028BC4A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
