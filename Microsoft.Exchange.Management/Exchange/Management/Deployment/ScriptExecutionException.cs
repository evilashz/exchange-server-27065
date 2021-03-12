using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020010C8 RID: 4296
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ScriptExecutionException : LocalizedException
	{
		// Token: 0x0600B2DE RID: 45790 RVA: 0x0029A726 File Offset: 0x00298926
		public ScriptExecutionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B2DF RID: 45791 RVA: 0x0029A72F File Offset: 0x0029892F
		public ScriptExecutionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B2E0 RID: 45792 RVA: 0x0029A739 File Offset: 0x00298939
		protected ScriptExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B2E1 RID: 45793 RVA: 0x0029A743 File Offset: 0x00298943
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
