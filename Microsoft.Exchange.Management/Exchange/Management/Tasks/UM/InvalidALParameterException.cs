using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011E5 RID: 4581
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidALParameterException : LocalizedException
	{
		// Token: 0x0600B95A RID: 47450 RVA: 0x002A5DB1 File Offset: 0x002A3FB1
		public InvalidALParameterException() : base(Strings.InvalidALParameterException)
		{
		}

		// Token: 0x0600B95B RID: 47451 RVA: 0x002A5DBE File Offset: 0x002A3FBE
		public InvalidALParameterException(Exception innerException) : base(Strings.InvalidALParameterException, innerException)
		{
		}

		// Token: 0x0600B95C RID: 47452 RVA: 0x002A5DCC File Offset: 0x002A3FCC
		protected InvalidALParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B95D RID: 47453 RVA: 0x002A5DD6 File Offset: 0x002A3FD6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
