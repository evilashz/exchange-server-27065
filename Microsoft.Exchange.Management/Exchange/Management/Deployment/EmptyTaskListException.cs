using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000FCE RID: 4046
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EmptyTaskListException : LocalizedException
	{
		// Token: 0x0600ADDD RID: 44509 RVA: 0x002925A1 File Offset: 0x002907A1
		public EmptyTaskListException() : base(Strings.EmptyTaskListException)
		{
		}

		// Token: 0x0600ADDE RID: 44510 RVA: 0x002925AE File Offset: 0x002907AE
		public EmptyTaskListException(Exception innerException) : base(Strings.EmptyTaskListException, innerException)
		{
		}

		// Token: 0x0600ADDF RID: 44511 RVA: 0x002925BC File Offset: 0x002907BC
		protected EmptyTaskListException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADE0 RID: 44512 RVA: 0x002925C6 File Offset: 0x002907C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
