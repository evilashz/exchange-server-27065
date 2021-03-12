using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000C1 RID: 193
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RightsManagementException : LocalizedException
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00012E81 File Offset: 0x00011081
		public RightsManagementException() : base(Strings.EvaluationErrorsRightsManagementFailure)
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00012E8E File Offset: 0x0001108E
		public RightsManagementException(Exception innerException) : base(Strings.EvaluationErrorsRightsManagementFailure, innerException)
		{
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00012E9C File Offset: 0x0001109C
		protected RightsManagementException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00012EA6 File Offset: 0x000110A6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
