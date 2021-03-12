using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EAC RID: 3756
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MRSDiagnosticQueryException : LocalizedException
	{
		// Token: 0x0600A830 RID: 43056 RVA: 0x00289945 File Offset: 0x00287B45
		public MRSDiagnosticQueryException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A831 RID: 43057 RVA: 0x0028994E File Offset: 0x00287B4E
		public MRSDiagnosticQueryException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A832 RID: 43058 RVA: 0x00289958 File Offset: 0x00287B58
		protected MRSDiagnosticQueryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A833 RID: 43059 RVA: 0x00289962 File Offset: 0x00287B62
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
