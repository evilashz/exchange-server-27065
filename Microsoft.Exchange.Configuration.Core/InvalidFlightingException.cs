using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Core.LocStrings;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000034 RID: 52
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidFlightingException : WinRMDataExchangeException
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00007605 File Offset: 0x00005805
		public InvalidFlightingException() : base(Strings.InvalidFlighting)
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007612 File Offset: 0x00005812
		public InvalidFlightingException(Exception innerException) : base(Strings.InvalidFlighting, innerException)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007620 File Offset: 0x00005820
		protected InvalidFlightingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000762A File Offset: 0x0000582A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
