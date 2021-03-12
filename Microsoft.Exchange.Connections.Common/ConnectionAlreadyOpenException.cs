using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000039 RID: 57
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ConnectionAlreadyOpenException : TransientException
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00003A79 File Offset: 0x00001C79
		public ConnectionAlreadyOpenException() : base(CXStrings.ConnectionAlreadyOpenError)
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003A86 File Offset: 0x00001C86
		public ConnectionAlreadyOpenException(Exception innerException) : base(CXStrings.ConnectionAlreadyOpenError, innerException)
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003A94 File Offset: 0x00001C94
		protected ConnectionAlreadyOpenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003A9E File Offset: 0x00001C9E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
