using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B15 RID: 2837
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MservOperationException : MServPermanentException
	{
		// Token: 0x06008226 RID: 33318 RVA: 0x001A7DAF File Offset: 0x001A5FAF
		public MservOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008227 RID: 33319 RVA: 0x001A7DB8 File Offset: 0x001A5FB8
		public MservOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008228 RID: 33320 RVA: 0x001A7DC2 File Offset: 0x001A5FC2
		protected MservOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008229 RID: 33321 RVA: 0x001A7DCC File Offset: 0x001A5FCC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
