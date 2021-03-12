using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000DD3 RID: 3539
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoComponentInfoFilesException : LocalizedException
	{
		// Token: 0x0600A40C RID: 41996 RVA: 0x00283966 File Offset: 0x00281B66
		public NoComponentInfoFilesException() : base(Strings.NoComponentInfoFilesException)
		{
		}

		// Token: 0x0600A40D RID: 41997 RVA: 0x00283973 File Offset: 0x00281B73
		public NoComponentInfoFilesException(Exception innerException) : base(Strings.NoComponentInfoFilesException, innerException)
		{
		}

		// Token: 0x0600A40E RID: 41998 RVA: 0x00283981 File Offset: 0x00281B81
		protected NoComponentInfoFilesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A40F RID: 41999 RVA: 0x0028398B File Offset: 0x00281B8B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
