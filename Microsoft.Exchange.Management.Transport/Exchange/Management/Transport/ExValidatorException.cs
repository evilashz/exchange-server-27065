using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200018A RID: 394
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ExValidatorException : LocalizedException
	{
		// Token: 0x06000FA0 RID: 4000 RVA: 0x000366BD File Offset: 0x000348BD
		public ExValidatorException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000366C6 File Offset: 0x000348C6
		public ExValidatorException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000366D0 File Offset: 0x000348D0
		protected ExValidatorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000366DA File Offset: 0x000348DA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
