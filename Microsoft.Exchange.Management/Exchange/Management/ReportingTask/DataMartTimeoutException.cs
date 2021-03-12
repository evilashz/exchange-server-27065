using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x02001167 RID: 4455
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataMartTimeoutException : ReportingException
	{
		// Token: 0x0600B5F0 RID: 46576 RVA: 0x0029F0FD File Offset: 0x0029D2FD
		public DataMartTimeoutException() : base(Strings.DataMartTimeoutException)
		{
		}

		// Token: 0x0600B5F1 RID: 46577 RVA: 0x0029F10A File Offset: 0x0029D30A
		public DataMartTimeoutException(Exception innerException) : base(Strings.DataMartTimeoutException, innerException)
		{
		}

		// Token: 0x0600B5F2 RID: 46578 RVA: 0x0029F118 File Offset: 0x0029D318
		protected DataMartTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B5F3 RID: 46579 RVA: 0x0029F122 File Offset: 0x0029D322
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
