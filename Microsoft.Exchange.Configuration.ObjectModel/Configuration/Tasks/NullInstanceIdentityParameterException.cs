using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002C1 RID: 705
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NullInstanceIdentityParameterException : LocalizedException
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x0005CDC4 File Offset: 0x0005AFC4
		public NullInstanceIdentityParameterException() : base(Strings.ErrorInstanceObjectConatinsNullIdentity)
		{
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0005CDD1 File Offset: 0x0005AFD1
		public NullInstanceIdentityParameterException(Exception innerException) : base(Strings.ErrorInstanceObjectConatinsNullIdentity, innerException)
		{
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0005CDDF File Offset: 0x0005AFDF
		protected NullInstanceIdentityParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0005CDE9 File Offset: 0x0005AFE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
