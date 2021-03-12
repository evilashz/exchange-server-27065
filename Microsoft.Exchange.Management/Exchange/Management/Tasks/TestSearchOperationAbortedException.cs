using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F3A RID: 3898
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TestSearchOperationAbortedException : LocalizedException
	{
		// Token: 0x0600AB1D RID: 43805 RVA: 0x0028E98D File Offset: 0x0028CB8D
		public TestSearchOperationAbortedException() : base(Strings.TestSearchOperationAborted)
		{
		}

		// Token: 0x0600AB1E RID: 43806 RVA: 0x0028E99A File Offset: 0x0028CB9A
		public TestSearchOperationAbortedException(Exception innerException) : base(Strings.TestSearchOperationAborted, innerException)
		{
		}

		// Token: 0x0600AB1F RID: 43807 RVA: 0x0028E9A8 File Offset: 0x0028CBA8
		protected TestSearchOperationAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AB20 RID: 43808 RVA: 0x0028E9B2 File Offset: 0x0028CBB2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
