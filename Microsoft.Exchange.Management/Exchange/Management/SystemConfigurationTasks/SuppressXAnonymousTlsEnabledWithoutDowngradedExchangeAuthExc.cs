using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F7E RID: 3966
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuppressXAnonymousTlsEnabledWithoutDowngradedExchangeAuthException : LocalizedException
	{
		// Token: 0x0600AC59 RID: 44121 RVA: 0x002902C8 File Offset: 0x0028E4C8
		public SuppressXAnonymousTlsEnabledWithoutDowngradedExchangeAuthException() : base(Strings.SuppressXAnonymousTlsEnabledWithoutDowngradedExchangeAuth)
		{
		}

		// Token: 0x0600AC5A RID: 44122 RVA: 0x002902D5 File Offset: 0x0028E4D5
		public SuppressXAnonymousTlsEnabledWithoutDowngradedExchangeAuthException(Exception innerException) : base(Strings.SuppressXAnonymousTlsEnabledWithoutDowngradedExchangeAuth, innerException)
		{
		}

		// Token: 0x0600AC5B RID: 44123 RVA: 0x002902E3 File Offset: 0x0028E4E3
		protected SuppressXAnonymousTlsEnabledWithoutDowngradedExchangeAuthException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC5C RID: 44124 RVA: 0x002902ED File Offset: 0x0028E4ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
