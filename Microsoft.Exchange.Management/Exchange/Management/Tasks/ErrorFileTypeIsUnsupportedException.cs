using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FF7 RID: 4087
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorFileTypeIsUnsupportedException : LocalizedException
	{
		// Token: 0x0600AE96 RID: 44694 RVA: 0x002932F1 File Offset: 0x002914F1
		public ErrorFileTypeIsUnsupportedException() : base(Strings.ErrorFileTypeIsUnsupported)
		{
		}

		// Token: 0x0600AE97 RID: 44695 RVA: 0x002932FE File Offset: 0x002914FE
		public ErrorFileTypeIsUnsupportedException(Exception innerException) : base(Strings.ErrorFileTypeIsUnsupported, innerException)
		{
		}

		// Token: 0x0600AE98 RID: 44696 RVA: 0x0029330C File Offset: 0x0029150C
		protected ErrorFileTypeIsUnsupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE99 RID: 44697 RVA: 0x00293316 File Offset: 0x00291516
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
