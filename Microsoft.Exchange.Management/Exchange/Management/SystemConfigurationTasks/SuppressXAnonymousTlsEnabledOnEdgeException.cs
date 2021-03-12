using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F7D RID: 3965
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuppressXAnonymousTlsEnabledOnEdgeException : LocalizedException
	{
		// Token: 0x0600AC55 RID: 44117 RVA: 0x00290299 File Offset: 0x0028E499
		public SuppressXAnonymousTlsEnabledOnEdgeException() : base(Strings.SuppressXAnonymousTlsEnabledOnEdge)
		{
		}

		// Token: 0x0600AC56 RID: 44118 RVA: 0x002902A6 File Offset: 0x0028E4A6
		public SuppressXAnonymousTlsEnabledOnEdgeException(Exception innerException) : base(Strings.SuppressXAnonymousTlsEnabledOnEdge, innerException)
		{
		}

		// Token: 0x0600AC57 RID: 44119 RVA: 0x002902B4 File Offset: 0x0028E4B4
		protected SuppressXAnonymousTlsEnabledOnEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC58 RID: 44120 RVA: 0x002902BE File Offset: 0x0028E4BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
