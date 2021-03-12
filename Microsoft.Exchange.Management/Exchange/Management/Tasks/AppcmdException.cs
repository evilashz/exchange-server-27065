using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001184 RID: 4484
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AppcmdException : LocalizedException
	{
		// Token: 0x0600B687 RID: 46727 RVA: 0x002A00DA File Offset: 0x0029E2DA
		public AppcmdException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B688 RID: 46728 RVA: 0x002A00E3 File Offset: 0x0029E2E3
		public AppcmdException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B689 RID: 46729 RVA: 0x002A00ED File Offset: 0x0029E2ED
		protected AppcmdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B68A RID: 46730 RVA: 0x002A00F7 File Offset: 0x0029E2F7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
