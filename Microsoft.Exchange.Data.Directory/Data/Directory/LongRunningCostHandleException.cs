using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AAB RID: 2731
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LongRunningCostHandleException : ADTransientException
	{
		// Token: 0x0600801E RID: 32798 RVA: 0x001A4E0F File Offset: 0x001A300F
		public LongRunningCostHandleException() : base(DirectoryStrings.LongRunningCostHandle)
		{
		}

		// Token: 0x0600801F RID: 32799 RVA: 0x001A4E1C File Offset: 0x001A301C
		public LongRunningCostHandleException(Exception innerException) : base(DirectoryStrings.LongRunningCostHandle, innerException)
		{
		}

		// Token: 0x06008020 RID: 32800 RVA: 0x001A4E2A File Offset: 0x001A302A
		protected LongRunningCostHandleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008021 RID: 32801 RVA: 0x001A4E34 File Offset: 0x001A3034
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
