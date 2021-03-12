using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B3F RID: 2879
	[DataContract(Name = "ValidateModernGroupAliasRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ValidateModernGroupAliasRequest : BaseRequest
	{
		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x0600518F RID: 20879 RVA: 0x0010A93D File Offset: 0x00108B3D
		// (set) Token: 0x06005190 RID: 20880 RVA: 0x0010A945 File Offset: 0x00108B45
		[DataMember(Name = "Alias", IsRequired = true)]
		public string Alias { get; set; }

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06005191 RID: 20881 RVA: 0x0010A94E File Offset: 0x00108B4E
		// (set) Token: 0x06005192 RID: 20882 RVA: 0x0010A956 File Offset: 0x00108B56
		[DataMember(Name = "Domain", IsRequired = true)]
		public string Domain { get; set; }

		// Token: 0x06005193 RID: 20883 RVA: 0x0010A95F File Offset: 0x00108B5F
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x0010A969 File Offset: 0x00108B69
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
