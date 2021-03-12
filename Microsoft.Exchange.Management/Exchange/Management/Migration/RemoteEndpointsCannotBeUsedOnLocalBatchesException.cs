using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001120 RID: 4384
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoteEndpointsCannotBeUsedOnLocalBatchesException : LocalizedException
	{
		// Token: 0x0600B48E RID: 46222 RVA: 0x0029CF5C File Offset: 0x0029B15C
		public RemoteEndpointsCannotBeUsedOnLocalBatchesException(string endpointName) : base(Strings.ErrorRemoteEndpointsCannotBeUsedOnLocalBatches(endpointName))
		{
			this.endpointName = endpointName;
		}

		// Token: 0x0600B48F RID: 46223 RVA: 0x0029CF71 File Offset: 0x0029B171
		public RemoteEndpointsCannotBeUsedOnLocalBatchesException(string endpointName, Exception innerException) : base(Strings.ErrorRemoteEndpointsCannotBeUsedOnLocalBatches(endpointName), innerException)
		{
			this.endpointName = endpointName;
		}

		// Token: 0x0600B490 RID: 46224 RVA: 0x0029CF87 File Offset: 0x0029B187
		protected RemoteEndpointsCannotBeUsedOnLocalBatchesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.endpointName = (string)info.GetValue("endpointName", typeof(string));
		}

		// Token: 0x0600B491 RID: 46225 RVA: 0x0029CFB1 File Offset: 0x0029B1B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("endpointName", this.endpointName);
		}

		// Token: 0x1700392B RID: 14635
		// (get) Token: 0x0600B492 RID: 46226 RVA: 0x0029CFCC File Offset: 0x0029B1CC
		public string EndpointName
		{
			get
			{
				return this.endpointName;
			}
		}

		// Token: 0x04006291 RID: 25233
		private readonly string endpointName;
	}
}
