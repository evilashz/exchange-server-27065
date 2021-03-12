using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003D4 RID: 980
	[DataContract(Name = "CreateUnifiedGroupRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateUnifiedGroupRequest : BaseRequest
	{
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x0007753F File Offset: 0x0007573F
		// (set) Token: 0x06001F66 RID: 8038 RVA: 0x00077547 File Offset: 0x00075747
		[DataMember(Name = "Name", IsRequired = true)]
		public string Name { get; set; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x00077550 File Offset: 0x00075750
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x00077558 File Offset: 0x00075758
		[DataMember(Name = "Alias", IsRequired = true)]
		public string Alias { get; set; }

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x00077561 File Offset: 0x00075761
		// (set) Token: 0x06001F6A RID: 8042 RVA: 0x00077569 File Offset: 0x00075769
		[DataMember(Name = "Description", IsRequired = false)]
		public string Description { get; set; }

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x00077572 File Offset: 0x00075772
		// (set) Token: 0x06001F6C RID: 8044 RVA: 0x0007757A File Offset: 0x0007577A
		[DataMember(Name = "GroupType", IsRequired = true)]
		public ModernGroupObjectType GroupType { get; set; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x00077583 File Offset: 0x00075783
		// (set) Token: 0x06001F6E RID: 8046 RVA: 0x0007758B File Offset: 0x0007578B
		[DataMember(Name = "AutoSubscribeNewGroupMembers", IsRequired = false)]
		public bool AutoSubscribeNewGroupMembers { get; set; }

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x00077594 File Offset: 0x00075794
		// (set) Token: 0x06001F70 RID: 8048 RVA: 0x0007759C File Offset: 0x0007579C
		[DataMember(Name = "PushToken", IsRequired = false)]
		public string PushToken { get; set; }

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x000775A5 File Offset: 0x000757A5
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x000775AD File Offset: 0x000757AD
		[DataMember(Name = "CultureId", IsRequired = false)]
		public string CultureId { get; set; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x000775B6 File Offset: 0x000757B6
		// (set) Token: 0x06001F74 RID: 8052 RVA: 0x000775BE File Offset: 0x000757BE
		internal CultureInfo Language { get; private set; }

		// Token: 0x06001F75 RID: 8053 RVA: 0x000775C7 File Offset: 0x000757C7
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000775D1 File Offset: 0x000757D1
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000775D4 File Offset: 0x000757D4
		internal override void Validate()
		{
			if (string.IsNullOrWhiteSpace(this.Name))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			if (string.IsNullOrWhiteSpace(this.Alias) || !CreateUnifiedGroupRequest.ValidAliasRegex.IsMatch(this.Alias))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			if (this.GroupType != ModernGroupObjectType.Public && this.GroupType != ModernGroupObjectType.Private)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			if (this.CultureId != null)
			{
				try
				{
					this.Language = CultureInfo.CreateSpecificCulture(this.CultureId);
				}
				catch (CultureNotFoundException innerException)
				{
					throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(innerException), FaultParty.Sender);
				}
			}
		}

		// Token: 0x040011E2 RID: 4578
		private static readonly Regex ValidAliasRegex = new Regex("^[A-Za-z0-9-_]+$");
	}
}
