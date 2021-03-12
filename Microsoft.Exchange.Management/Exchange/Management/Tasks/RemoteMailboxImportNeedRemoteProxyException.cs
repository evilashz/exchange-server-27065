using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EBF RID: 3775
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoteMailboxImportNeedRemoteProxyException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A894 RID: 43156 RVA: 0x0028A3E7 File Offset: 0x002885E7
		public RemoteMailboxImportNeedRemoteProxyException() : base(Strings.ErrorRemoteMailboxImportNeedRemoteProxy)
		{
		}

		// Token: 0x0600A895 RID: 43157 RVA: 0x0028A3F4 File Offset: 0x002885F4
		public RemoteMailboxImportNeedRemoteProxyException(Exception innerException) : base(Strings.ErrorRemoteMailboxImportNeedRemoteProxy, innerException)
		{
		}

		// Token: 0x0600A896 RID: 43158 RVA: 0x0028A402 File Offset: 0x00288602
		protected RemoteMailboxImportNeedRemoteProxyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A897 RID: 43159 RVA: 0x0028A40C File Offset: 0x0028860C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
