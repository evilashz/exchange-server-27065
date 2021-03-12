using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AAA RID: 2730
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DelayCacheFullException : ADTransientException
	{
		// Token: 0x0600801A RID: 32794 RVA: 0x001A4DE0 File Offset: 0x001A2FE0
		public DelayCacheFullException() : base(DirectoryStrings.DelayCacheFull)
		{
		}

		// Token: 0x0600801B RID: 32795 RVA: 0x001A4DED File Offset: 0x001A2FED
		public DelayCacheFullException(Exception innerException) : base(DirectoryStrings.DelayCacheFull, innerException)
		{
		}

		// Token: 0x0600801C RID: 32796 RVA: 0x001A4DFB File Offset: 0x001A2FFB
		protected DelayCacheFullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600801D RID: 32797 RVA: 0x001A4E05 File Offset: 0x001A3005
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
