using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002AE RID: 686
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidEndpointAddressPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002326 RID: 8998 RVA: 0x0004E0AC File Offset: 0x0004C2AC
		public InvalidEndpointAddressPermanentException(string serviceURI) : base(MrsStrings.InvalidEndpointAddressError(serviceURI))
		{
			this.serviceURI = serviceURI;
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0004E0C1 File Offset: 0x0004C2C1
		public InvalidEndpointAddressPermanentException(string serviceURI, Exception innerException) : base(MrsStrings.InvalidEndpointAddressError(serviceURI), innerException)
		{
			this.serviceURI = serviceURI;
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x0004E0D7 File Offset: 0x0004C2D7
		protected InvalidEndpointAddressPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceURI = (string)info.GetValue("serviceURI", typeof(string));
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0004E101 File Offset: 0x0004C301
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceURI", this.serviceURI);
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x0004E11C File Offset: 0x0004C31C
		public string ServiceURI
		{
			get
			{
				return this.serviceURI;
			}
		}

		// Token: 0x04000FB7 RID: 4023
		private readonly string serviceURI;
	}
}
