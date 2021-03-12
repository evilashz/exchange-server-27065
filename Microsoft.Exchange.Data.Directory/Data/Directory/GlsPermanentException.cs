using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ADD RID: 2781
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlsPermanentException : PermanentGlobalException
	{
		// Token: 0x0600810C RID: 33036 RVA: 0x001A6256 File Offset: 0x001A4456
		public GlsPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600810D RID: 33037 RVA: 0x001A625F File Offset: 0x001A445F
		public GlsPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600810E RID: 33038 RVA: 0x001A6269 File Offset: 0x001A4469
		protected GlsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600810F RID: 33039 RVA: 0x001A6273 File Offset: 0x001A4473
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
