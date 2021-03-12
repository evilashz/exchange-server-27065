using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B43 RID: 2883
	[DataContract(Name = "CreateModernGroupRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateModernGroupRequest : ComposeModernGroupRequestBase
	{
		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x060051A7 RID: 20903 RVA: 0x0010AA18 File Offset: 0x00108C18
		// (set) Token: 0x060051A8 RID: 20904 RVA: 0x0010AA20 File Offset: 0x00108C20
		[DataMember(Name = "Alias", IsRequired = false)]
		public string Alias { get; set; }

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x060051A9 RID: 20905 RVA: 0x0010AA29 File Offset: 0x00108C29
		// (set) Token: 0x060051AA RID: 20906 RVA: 0x0010AA31 File Offset: 0x00108C31
		[DataMember(Name = "CollectLogs", IsRequired = false)]
		public bool CollectLogs { get; set; }

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x060051AB RID: 20907 RVA: 0x0010AA3A File Offset: 0x00108C3A
		// (set) Token: 0x060051AC RID: 20908 RVA: 0x0010AA42 File Offset: 0x00108C42
		[DataMember(Name = "GroupType", IsRequired = true)]
		public ModernGroupObjectType GroupType { get; set; }

		// Token: 0x060051AD RID: 20909 RVA: 0x0010AA4B File Offset: 0x00108C4B
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x0010AA55 File Offset: 0x00108C55
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x0010AA58 File Offset: 0x00108C58
		internal override void Validate()
		{
			if (string.IsNullOrWhiteSpace(base.Name))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			if (string.IsNullOrWhiteSpace(this.Alias))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			string text = CallContext.Current.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			if (base.AddedMembers == null)
			{
				base.AddedMembers = new string[]
				{
					text
				};
			}
			else
			{
				base.AddedMembers = base.AddedMembers.Concat(new string[]
				{
					text
				}).Distinct(StringComparer.InvariantCultureIgnoreCase).ToArray<string>();
			}
			if (base.AddedOwners == null)
			{
				base.AddedOwners = new string[]
				{
					text
				};
				return;
			}
			base.AddedOwners = base.AddedOwners.Concat(new string[]
			{
				text
			}).Distinct(StringComparer.InvariantCultureIgnoreCase).ToArray<string>();
		}
	}
}
