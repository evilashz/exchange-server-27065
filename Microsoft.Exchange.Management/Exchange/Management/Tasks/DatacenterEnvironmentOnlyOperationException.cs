using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DEE RID: 3566
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatacenterEnvironmentOnlyOperationException : LocalizedException
	{
		// Token: 0x0600A49B RID: 42139 RVA: 0x002848E5 File Offset: 0x00282AE5
		public DatacenterEnvironmentOnlyOperationException() : base(Strings.DatacenterEnvironmentOnlyOperationException)
		{
		}

		// Token: 0x0600A49C RID: 42140 RVA: 0x002848F2 File Offset: 0x00282AF2
		public DatacenterEnvironmentOnlyOperationException(Exception innerException) : base(Strings.DatacenterEnvironmentOnlyOperationException, innerException)
		{
		}

		// Token: 0x0600A49D RID: 42141 RVA: 0x00284900 File Offset: 0x00282B00
		protected DatacenterEnvironmentOnlyOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A49E RID: 42142 RVA: 0x0028490A File Offset: 0x00282B0A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
