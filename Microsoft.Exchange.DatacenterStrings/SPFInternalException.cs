using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.DatacenterStrings
{
	// Token: 0x02000007 RID: 7
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SPFInternalException : RecipientTaskException
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000041DC File Offset: 0x000023DC
		public SPFInternalException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000041E5 File Offset: 0x000023E5
		public SPFInternalException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000041EF File Offset: 0x000023EF
		protected SPFInternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000041F9 File Offset: 0x000023F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
