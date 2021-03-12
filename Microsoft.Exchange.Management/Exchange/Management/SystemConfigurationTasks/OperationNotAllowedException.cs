using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001168 RID: 4456
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OperationNotAllowedException : LocalizedException
	{
		// Token: 0x0600B5F4 RID: 46580 RVA: 0x0029F12C File Offset: 0x0029D32C
		public OperationNotAllowedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B5F5 RID: 46581 RVA: 0x0029F135 File Offset: 0x0029D335
		public OperationNotAllowedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B5F6 RID: 46582 RVA: 0x0029F13F File Offset: 0x0029D33F
		protected OperationNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B5F7 RID: 46583 RVA: 0x0029F149 File Offset: 0x0029D349
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
