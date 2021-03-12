using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009E4 RID: 2532
	[Serializable]
	public class GetFederationInformationFailedException : LocalizedException
	{
		// Token: 0x06005A79 RID: 23161 RVA: 0x0017AFD4 File Offset: 0x001791D4
		public GetFederationInformationFailedException() : base(Strings.GetFederationInformationFailed)
		{
		}

		// Token: 0x06005A7A RID: 23162 RVA: 0x0017AFE1 File Offset: 0x001791E1
		public GetFederationInformationFailedException(GetFederationInformationResult[] discoveryResults) : base(Strings.GetFederationInformationFailed)
		{
			this.discoveryResults = discoveryResults;
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x0017AFF5 File Offset: 0x001791F5
		public GetFederationInformationFailedException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
			this.discoveryResults = (GetFederationInformationResult[])serializationInfo.GetValue("discoveryResults", typeof(GetFederationInformationResult[]));
		}

		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x06005A7C RID: 23164 RVA: 0x0017B01F File Offset: 0x0017921F
		public GetFederationInformationResult[] DiscoveryResults
		{
			get
			{
				return this.discoveryResults;
			}
		}

		// Token: 0x06005A7D RID: 23165 RVA: 0x0017B027 File Offset: 0x00179227
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext context)
		{
			base.GetObjectData(serializationInfo, context);
			serializationInfo.AddValue("discoveryResults", this.discoveryResults);
		}

		// Token: 0x040033D2 RID: 13266
		private GetFederationInformationResult[] discoveryResults;
	}
}
