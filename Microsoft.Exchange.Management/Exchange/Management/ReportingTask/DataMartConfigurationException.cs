using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x02001163 RID: 4451
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataMartConfigurationException : ReportingException
	{
		// Token: 0x0600B5DC RID: 46556 RVA: 0x0029EF21 File Offset: 0x0029D121
		public DataMartConfigurationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B5DD RID: 46557 RVA: 0x0029EF2A File Offset: 0x0029D12A
		public DataMartConfigurationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B5DE RID: 46558 RVA: 0x0029EF34 File Offset: 0x0029D134
		protected DataMartConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B5DF RID: 46559 RVA: 0x0029EF3E File Offset: 0x0029D13E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
