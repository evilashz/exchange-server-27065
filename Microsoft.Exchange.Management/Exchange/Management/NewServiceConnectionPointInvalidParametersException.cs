using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management
{
	// Token: 0x0200112E RID: 4398
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NewServiceConnectionPointInvalidParametersException : LocalizedException
	{
		// Token: 0x0600B4D3 RID: 46291 RVA: 0x0029D5DD File Offset: 0x0029B7DD
		public NewServiceConnectionPointInvalidParametersException() : base(Strings.NewServiceConnectionPointInvalidParameters)
		{
		}

		// Token: 0x0600B4D4 RID: 46292 RVA: 0x0029D5EA File Offset: 0x0029B7EA
		public NewServiceConnectionPointInvalidParametersException(Exception innerException) : base(Strings.NewServiceConnectionPointInvalidParameters, innerException)
		{
		}

		// Token: 0x0600B4D5 RID: 46293 RVA: 0x0029D5F8 File Offset: 0x0029B7F8
		protected NewServiceConnectionPointInvalidParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B4D6 RID: 46294 RVA: 0x0029D602 File Offset: 0x0029B802
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
