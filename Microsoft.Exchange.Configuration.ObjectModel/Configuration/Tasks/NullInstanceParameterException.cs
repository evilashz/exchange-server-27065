using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002C0 RID: 704
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NullInstanceParameterException : LocalizedException
	{
		// Token: 0x06001930 RID: 6448 RVA: 0x0005CD95 File Offset: 0x0005AF95
		public NullInstanceParameterException() : base(Strings.ExceptionNullInstanceParameter)
		{
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0005CDA2 File Offset: 0x0005AFA2
		public NullInstanceParameterException(Exception innerException) : base(Strings.ExceptionNullInstanceParameter, innerException)
		{
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0005CDB0 File Offset: 0x0005AFB0
		protected NullInstanceParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0005CDBA File Offset: 0x0005AFBA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
