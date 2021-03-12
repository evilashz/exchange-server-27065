using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200112F RID: 4399
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RidMasterConfigException : LocalizedException
	{
		// Token: 0x0600B4D7 RID: 46295 RVA: 0x0029D60C File Offset: 0x0029B80C
		public RidMasterConfigException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B4D8 RID: 46296 RVA: 0x0029D615 File Offset: 0x0029B815
		public RidMasterConfigException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B4D9 RID: 46297 RVA: 0x0029D61F File Offset: 0x0029B81F
		protected RidMasterConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B4DA RID: 46298 RVA: 0x0029D629 File Offset: 0x0029B829
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
