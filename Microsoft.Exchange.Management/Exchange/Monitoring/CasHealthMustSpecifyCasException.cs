using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F17 RID: 3863
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthMustSpecifyCasException : LocalizedException
	{
		// Token: 0x0600AA62 RID: 43618 RVA: 0x0028D502 File Offset: 0x0028B702
		public CasHealthMustSpecifyCasException() : base(Strings.CasHealthMustSpecifyCas)
		{
		}

		// Token: 0x0600AA63 RID: 43619 RVA: 0x0028D50F File Offset: 0x0028B70F
		public CasHealthMustSpecifyCasException(Exception innerException) : base(Strings.CasHealthMustSpecifyCas, innerException)
		{
		}

		// Token: 0x0600AA64 RID: 43620 RVA: 0x0028D51D File Offset: 0x0028B71D
		protected CasHealthMustSpecifyCasException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AA65 RID: 43621 RVA: 0x0028D527 File Offset: 0x0028B727
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
