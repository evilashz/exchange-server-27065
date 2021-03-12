using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AD8 RID: 2776
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ApiNotSupportedException : ADOperationException
	{
		// Token: 0x060080F8 RID: 33016 RVA: 0x001A6183 File Offset: 0x001A4383
		public ApiNotSupportedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060080F9 RID: 33017 RVA: 0x001A618C File Offset: 0x001A438C
		public ApiNotSupportedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060080FA RID: 33018 RVA: 0x001A6196 File Offset: 0x001A4396
		protected ApiNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060080FB RID: 33019 RVA: 0x001A61A0 File Offset: 0x001A43A0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
