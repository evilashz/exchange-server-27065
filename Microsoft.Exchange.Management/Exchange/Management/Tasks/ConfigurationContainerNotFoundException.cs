using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DFD RID: 3581
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationContainerNotFoundException : LocalizedException
	{
		// Token: 0x0600A4EB RID: 42219 RVA: 0x002851DA File Offset: 0x002833DA
		public ConfigurationContainerNotFoundException() : base(Strings.ConfigurationContainerNotFoundException)
		{
		}

		// Token: 0x0600A4EC RID: 42220 RVA: 0x002851E7 File Offset: 0x002833E7
		public ConfigurationContainerNotFoundException(Exception innerException) : base(Strings.ConfigurationContainerNotFoundException, innerException)
		{
		}

		// Token: 0x0600A4ED RID: 42221 RVA: 0x002851F5 File Offset: 0x002833F5
		protected ConfigurationContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A4EE RID: 42222 RVA: 0x002851FF File Offset: 0x002833FF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
