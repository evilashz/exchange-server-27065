using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F57 RID: 3927
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicateInitialDomainException : ADObjectAlreadyExistsException
	{
		// Token: 0x0600ABAA RID: 43946 RVA: 0x0028F629 File Offset: 0x0028D829
		public DuplicateInitialDomainException() : base(Strings.DuplicateInitialDomain)
		{
		}

		// Token: 0x0600ABAB RID: 43947 RVA: 0x0028F636 File Offset: 0x0028D836
		public DuplicateInitialDomainException(Exception innerException) : base(Strings.DuplicateInitialDomain, innerException)
		{
		}

		// Token: 0x0600ABAC RID: 43948 RVA: 0x0028F644 File Offset: 0x0028D844
		protected DuplicateInitialDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ABAD RID: 43949 RVA: 0x0028F64E File Offset: 0x0028D84E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
