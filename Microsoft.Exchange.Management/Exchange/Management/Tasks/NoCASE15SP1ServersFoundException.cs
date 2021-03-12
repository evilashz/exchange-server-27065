using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200119A RID: 4506
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoCASE15SP1ServersFoundException : LocalizedException
	{
		// Token: 0x0600B6F8 RID: 46840 RVA: 0x002A0C4B File Offset: 0x0029EE4B
		public NoCASE15SP1ServersFoundException() : base(Strings.NoCASE15SP1ServersFoundException)
		{
		}

		// Token: 0x0600B6F9 RID: 46841 RVA: 0x002A0C58 File Offset: 0x0029EE58
		public NoCASE15SP1ServersFoundException(Exception innerException) : base(Strings.NoCASE15SP1ServersFoundException, innerException)
		{
		}

		// Token: 0x0600B6FA RID: 46842 RVA: 0x002A0C66 File Offset: 0x0029EE66
		protected NoCASE15SP1ServersFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B6FB RID: 46843 RVA: 0x002A0C70 File Offset: 0x0029EE70
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
