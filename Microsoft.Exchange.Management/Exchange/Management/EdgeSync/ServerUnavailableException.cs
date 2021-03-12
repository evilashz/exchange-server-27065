using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000FD4 RID: 4052
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerUnavailableException : LocalizedException
	{
		// Token: 0x0600ADF5 RID: 44533 RVA: 0x002926A3 File Offset: 0x002908A3
		public ServerUnavailableException() : base(Strings.ServerUnavailableException)
		{
		}

		// Token: 0x0600ADF6 RID: 44534 RVA: 0x002926B0 File Offset: 0x002908B0
		public ServerUnavailableException(Exception innerException) : base(Strings.ServerUnavailableException, innerException)
		{
		}

		// Token: 0x0600ADF7 RID: 44535 RVA: 0x002926BE File Offset: 0x002908BE
		protected ServerUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADF8 RID: 44536 RVA: 0x002926C8 File Offset: 0x002908C8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
