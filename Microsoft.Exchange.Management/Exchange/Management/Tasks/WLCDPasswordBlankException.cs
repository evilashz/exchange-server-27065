using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E62 RID: 3682
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDPasswordBlankException : WLCDMemberException
	{
		// Token: 0x0600A6D6 RID: 42710 RVA: 0x00287D63 File Offset: 0x00285F63
		public WLCDPasswordBlankException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6D7 RID: 42711 RVA: 0x00287D6C File Offset: 0x00285F6C
		public WLCDPasswordBlankException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6D8 RID: 42712 RVA: 0x00287D76 File Offset: 0x00285F76
		protected WLCDPasswordBlankException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6D9 RID: 42713 RVA: 0x00287D80 File Offset: 0x00285F80
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
