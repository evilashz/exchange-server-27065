using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000361 RID: 865
	[DataContract]
	public class UMDialingRuleGroupRow : BaseRow
	{
		// Token: 0x06002FD4 RID: 12244 RVA: 0x00091C58 File Offset: 0x0008FE58
		public UMDialingRuleGroupRow(string name) : base(new Identity(name, name), null)
		{
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x00091C68 File Offset: 0x0008FE68
		public UMDialingRuleGroupRow(Identity id) : base(id, null)
		{
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x00091C72 File Offset: 0x0008FE72
		// (set) Token: 0x06002FD7 RID: 12247 RVA: 0x00091C7F File Offset: 0x0008FE7F
		[DataMember]
		public string DisplayName
		{
			get
			{
				return base.Identity.RawIdentity;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
