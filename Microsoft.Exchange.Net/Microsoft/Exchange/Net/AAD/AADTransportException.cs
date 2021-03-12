using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Net.AAD
{
	// Token: 0x020000F3 RID: 243
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AADTransportException : AADException
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x000162CA File Offset: 0x000144CA
		public AADTransportException() : base(NetException.aadTransportFailureException)
		{
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000162D7 File Offset: 0x000144D7
		public AADTransportException(Exception innerException) : base(NetException.aadTransportFailureException, innerException)
		{
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000162E5 File Offset: 0x000144E5
		protected AADTransportException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000162EF File Offset: 0x000144EF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
