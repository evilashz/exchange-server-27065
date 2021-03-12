using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003E6 RID: 998
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FfoReportingException : LocalizedException
	{
		// Token: 0x06002343 RID: 9027 RVA: 0x0008F163 File Offset: 0x0008D363
		public FfoReportingException() : base(Strings.FfoReportingMessage)
		{
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0008F170 File Offset: 0x0008D370
		public FfoReportingException(Exception innerException) : base(Strings.FfoReportingMessage, innerException)
		{
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0008F17E File Offset: 0x0008D37E
		protected FfoReportingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0008F188 File Offset: 0x0008D388
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
