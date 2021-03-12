using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006C8 RID: 1736
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuitabilityException : ADTransientException
	{
		// Token: 0x06005042 RID: 20546 RVA: 0x0012756A File Offset: 0x0012576A
		public SuitabilityException(LocalizedString message, string serverFqdn) : this(message)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("serverFqdn", serverFqdn);
			this.ServerFqdn = serverFqdn;
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x00127585 File Offset: 0x00125785
		public SuitabilityException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x0012758E File Offset: 0x0012578E
		public SuitabilityException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x00127598 File Offset: 0x00125798
		protected SuitabilityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x06005046 RID: 20550 RVA: 0x001275A2 File Offset: 0x001257A2
		// (set) Token: 0x06005047 RID: 20551 RVA: 0x001275AA File Offset: 0x001257AA
		internal string ServerFqdn { get; set; }

		// Token: 0x06005048 RID: 20552 RVA: 0x001275B3 File Offset: 0x001257B3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
