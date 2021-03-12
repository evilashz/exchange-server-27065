using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FCF RID: 4047
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceValidatorException : TaskException
	{
		// Token: 0x0600ADE1 RID: 44513 RVA: 0x002925D0 File Offset: 0x002907D0
		public ServiceValidatorException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600ADE2 RID: 44514 RVA: 0x002925D9 File Offset: 0x002907D9
		public ServiceValidatorException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600ADE3 RID: 44515 RVA: 0x002925E3 File Offset: 0x002907E3
		protected ServiceValidatorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADE4 RID: 44516 RVA: 0x002925ED File Offset: 0x002907ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
