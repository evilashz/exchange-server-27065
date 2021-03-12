using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000067 RID: 103
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LdapFilterException : ProvisioningException
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x00011568 File Offset: 0x0000F768
		public LdapFilterException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00011571 File Offset: 0x0000F771
		public LdapFilterException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001157B File Offset: 0x0000F77B
		protected LdapFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00011585 File Offset: 0x0000F785
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
