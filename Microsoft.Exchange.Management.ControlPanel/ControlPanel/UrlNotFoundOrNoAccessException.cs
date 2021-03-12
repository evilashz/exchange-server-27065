using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UrlNotFoundOrNoAccessException : AuthorizationException
	{
		// Token: 0x06001865 RID: 6245 RVA: 0x0004B731 File Offset: 0x00049931
		public UrlNotFoundOrNoAccessException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0004B73A File Offset: 0x0004993A
		public UrlNotFoundOrNoAccessException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0004B744 File Offset: 0x00049944
		protected UrlNotFoundOrNoAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0004B74E File Offset: 0x0004994E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
