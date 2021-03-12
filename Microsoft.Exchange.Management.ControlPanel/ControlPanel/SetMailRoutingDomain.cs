using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000418 RID: 1048
	[DataContract]
	public class SetMailRoutingDomain : SetObjectProperties
	{
		// Token: 0x170020E4 RID: 8420
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x000A5837 File Offset: 0x000A3A37
		// (set) Token: 0x06003538 RID: 13624 RVA: 0x000A5849 File Offset: 0x000A3A49
		[DataMember]
		public string DomainName
		{
			get
			{
				return (string)base["DomainName"];
			}
			set
			{
				base["DomainName"] = value;
			}
		}

		// Token: 0x170020E5 RID: 8421
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000A5857 File Offset: 0x000A3A57
		// (set) Token: 0x0600353A RID: 13626 RVA: 0x000A5869 File Offset: 0x000A3A69
		[DataMember]
		public string DomainType
		{
			get
			{
				return (string)base["DomainType"];
			}
			set
			{
				base["DomainType"] = value;
			}
		}

		// Token: 0x170020E6 RID: 8422
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x000A5877 File Offset: 0x000A3A77
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-AcceptedDomain";
			}
		}

		// Token: 0x170020E7 RID: 8423
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000A587E File Offset: 0x000A3A7E
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}
	}
}
