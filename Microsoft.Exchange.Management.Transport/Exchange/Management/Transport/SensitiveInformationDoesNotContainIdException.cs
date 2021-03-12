using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000180 RID: 384
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SensitiveInformationDoesNotContainIdException : InvalidContentContainsSensitiveInformationException
	{
		// Token: 0x06000F76 RID: 3958 RVA: 0x0003647D File Offset: 0x0003467D
		public SensitiveInformationDoesNotContainIdException() : base(Strings.SensitiveInformationDoesNotContainId)
		{
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003648A File Offset: 0x0003468A
		public SensitiveInformationDoesNotContainIdException(Exception innerException) : base(Strings.SensitiveInformationDoesNotContainId, innerException)
		{
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00036498 File Offset: 0x00034698
		protected SensitiveInformationDoesNotContainIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x000364A2 File Offset: 0x000346A2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
