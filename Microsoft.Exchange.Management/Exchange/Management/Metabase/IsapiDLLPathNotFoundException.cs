using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000DDE RID: 3550
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IsapiDLLPathNotFoundException : LocalizedException
	{
		// Token: 0x0600A444 RID: 42052 RVA: 0x00283F15 File Offset: 0x00282115
		public IsapiDLLPathNotFoundException() : base(Strings.IsapiDLLPathNotFoundException)
		{
		}

		// Token: 0x0600A445 RID: 42053 RVA: 0x00283F22 File Offset: 0x00282122
		public IsapiDLLPathNotFoundException(Exception innerException) : base(Strings.IsapiDLLPathNotFoundException, innerException)
		{
		}

		// Token: 0x0600A446 RID: 42054 RVA: 0x00283F30 File Offset: 0x00282130
		protected IsapiDLLPathNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A447 RID: 42055 RVA: 0x00283F3A File Offset: 0x0028213A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
