using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management
{
	// Token: 0x0200123A RID: 4666
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class HybridConfigurationException : LocalizedException
	{
		// Token: 0x0600BBF4 RID: 48116 RVA: 0x002ABA9D File Offset: 0x002A9C9D
		public HybridConfigurationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600BBF5 RID: 48117 RVA: 0x002ABAA6 File Offset: 0x002A9CA6
		public HybridConfigurationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600BBF6 RID: 48118 RVA: 0x002ABAB0 File Offset: 0x002A9CB0
		protected HybridConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600BBF7 RID: 48119 RVA: 0x002ABABA File Offset: 0x002A9CBA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
