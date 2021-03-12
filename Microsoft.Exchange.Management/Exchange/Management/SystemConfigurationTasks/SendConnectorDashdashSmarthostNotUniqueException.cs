using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F9A RID: 3994
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorDashdashSmarthostNotUniqueException : LocalizedException
	{
		// Token: 0x0600ACD5 RID: 44245 RVA: 0x00290B58 File Offset: 0x0028ED58
		public SendConnectorDashdashSmarthostNotUniqueException() : base(Strings.SendConnectorDashdashSmarthostNotUnique)
		{
		}

		// Token: 0x0600ACD6 RID: 44246 RVA: 0x00290B65 File Offset: 0x0028ED65
		public SendConnectorDashdashSmarthostNotUniqueException(Exception innerException) : base(Strings.SendConnectorDashdashSmarthostNotUnique, innerException)
		{
		}

		// Token: 0x0600ACD7 RID: 44247 RVA: 0x00290B73 File Offset: 0x0028ED73
		protected SendConnectorDashdashSmarthostNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACD8 RID: 44248 RVA: 0x00290B7D File Offset: 0x0028ED7D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
