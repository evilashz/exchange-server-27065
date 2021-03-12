using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.DatacenterStrings
{
	// Token: 0x02000005 RID: 5
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IDSInternalException : RecipientTaskException
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x0000418E File Offset: 0x0000238E
		public IDSInternalException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004197 File Offset: 0x00002397
		public IDSInternalException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000041A1 File Offset: 0x000023A1
		protected IDSInternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000041AB File Offset: 0x000023AB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
