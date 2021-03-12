using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AD9 RID: 2777
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ApiInputNotSupportedException : ADOperationException
	{
		// Token: 0x060080FC RID: 33020 RVA: 0x001A61AA File Offset: 0x001A43AA
		public ApiInputNotSupportedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060080FD RID: 33021 RVA: 0x001A61B3 File Offset: 0x001A43B3
		public ApiInputNotSupportedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060080FE RID: 33022 RVA: 0x001A61BD File Offset: 0x001A43BD
		protected ApiInputNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060080FF RID: 33023 RVA: 0x001A61C7 File Offset: 0x001A43C7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
