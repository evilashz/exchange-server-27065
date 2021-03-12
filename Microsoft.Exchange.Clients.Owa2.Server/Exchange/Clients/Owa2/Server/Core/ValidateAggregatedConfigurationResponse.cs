using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003F6 RID: 1014
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ValidateAggregatedConfigurationResponse
	{
		// Token: 0x060020F0 RID: 8432 RVA: 0x00079371 File Offset: 0x00077571
		public ValidateAggregatedConfigurationResponse()
		{
			this.FaiUpdates = new List<string>();
			this.TypeUpdates = new List<string>();
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x0007938F File Offset: 0x0007758F
		// (set) Token: 0x060020F2 RID: 8434 RVA: 0x00079397 File Offset: 0x00077597
		public List<string> FaiUpdates { get; set; }

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x000793A0 File Offset: 0x000775A0
		// (set) Token: 0x060020F4 RID: 8436 RVA: 0x000793A8 File Offset: 0x000775A8
		public List<string> TypeUpdates { get; set; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x000793B1 File Offset: 0x000775B1
		// (set) Token: 0x060020F6 RID: 8438 RVA: 0x000793B9 File Offset: 0x000775B9
		[DataMember(Name = "IsValidated")]
		public bool IsValidated { get; set; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x000793C2 File Offset: 0x000775C2
		// (set) Token: 0x060020F8 RID: 8440 RVA: 0x000793CF File Offset: 0x000775CF
		[DataMember(Name = "FaiUpdates")]
		public string[] FaiUpdatesArray
		{
			get
			{
				return this.FaiUpdates.ToArray();
			}
			set
			{
				this.FaiUpdates = new List<string>(value);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x000793DD File Offset: 0x000775DD
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x000793EA File Offset: 0x000775EA
		[DataMember(Name = "TypeUpdates")]
		public string[] TypeUpdatesArray
		{
			get
			{
				return this.TypeUpdates.ToArray();
			}
			set
			{
				this.TypeUpdates = new List<string>(value);
			}
		}
	}
}
