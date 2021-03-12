using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F4D RID: 3917
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveDefaultRemoteDomainException : LocalizedException
	{
		// Token: 0x0600AB7A RID: 43898 RVA: 0x0028F1F4 File Offset: 0x0028D3F4
		public CannotRemoveDefaultRemoteDomainException() : base(Strings.CannotRemoveDefaultRemoteDomain)
		{
		}

		// Token: 0x0600AB7B RID: 43899 RVA: 0x0028F201 File Offset: 0x0028D401
		public CannotRemoveDefaultRemoteDomainException(Exception innerException) : base(Strings.CannotRemoveDefaultRemoteDomain, innerException)
		{
		}

		// Token: 0x0600AB7C RID: 43900 RVA: 0x0028F20F File Offset: 0x0028D40F
		protected CannotRemoveDefaultRemoteDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AB7D RID: 43901 RVA: 0x0028F219 File Offset: 0x0028D419
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
