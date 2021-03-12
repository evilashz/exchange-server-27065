using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200102D RID: 4141
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindFirstOrgPerimeterConfigObjectException : LocalizedException
	{
		// Token: 0x0600AF9A RID: 44954 RVA: 0x002949D3 File Offset: 0x00292BD3
		public CouldNotFindFirstOrgPerimeterConfigObjectException() : base(Strings.CouldNotFindFirstOrgPerimeterConfigObjectId)
		{
		}

		// Token: 0x0600AF9B RID: 44955 RVA: 0x002949E0 File Offset: 0x00292BE0
		public CouldNotFindFirstOrgPerimeterConfigObjectException(Exception innerException) : base(Strings.CouldNotFindFirstOrgPerimeterConfigObjectId, innerException)
		{
		}

		// Token: 0x0600AF9C RID: 44956 RVA: 0x002949EE File Offset: 0x00292BEE
		protected CouldNotFindFirstOrgPerimeterConfigObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF9D RID: 44957 RVA: 0x002949F8 File Offset: 0x00292BF8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
