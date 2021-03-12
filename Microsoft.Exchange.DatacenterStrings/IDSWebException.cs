using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.DatacenterStrings
{
	// Token: 0x02000008 RID: 8
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IDSWebException : RecipientTaskException
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00004203 File Offset: 0x00002403
		public IDSWebException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000420C File Offset: 0x0000240C
		public IDSWebException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004216 File Offset: 0x00002416
		protected IDSWebException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004220 File Offset: 0x00002420
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
