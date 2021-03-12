using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AEA RID: 2794
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RealmNotFoundException : LocalizedException
	{
		// Token: 0x06008141 RID: 33089 RVA: 0x001A64C2 File Offset: 0x001A46C2
		public RealmNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008142 RID: 33090 RVA: 0x001A64CB File Offset: 0x001A46CB
		public RealmNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008143 RID: 33091 RVA: 0x001A64D5 File Offset: 0x001A46D5
		protected RealmNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008144 RID: 33092 RVA: 0x001A64DF File Offset: 0x001A46DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
