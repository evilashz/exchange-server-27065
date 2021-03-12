using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F5A RID: 3930
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotBypassDomainNameValidationException : LocalizedException
	{
		// Token: 0x0600ABB8 RID: 43960 RVA: 0x0028F748 File Offset: 0x0028D948
		public CannotBypassDomainNameValidationException() : base(Strings.CannotBypassDomainNameValidation)
		{
		}

		// Token: 0x0600ABB9 RID: 43961 RVA: 0x0028F755 File Offset: 0x0028D955
		public CannotBypassDomainNameValidationException(Exception innerException) : base(Strings.CannotBypassDomainNameValidation, innerException)
		{
		}

		// Token: 0x0600ABBA RID: 43962 RVA: 0x0028F763 File Offset: 0x0028D963
		protected CannotBypassDomainNameValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ABBB RID: 43963 RVA: 0x0028F76D File Offset: 0x0028D96D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
