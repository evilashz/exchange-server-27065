using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002B3 RID: 691
	[DataContract]
	public abstract class ImapSubscriptionBaseParameter : PimSubscriptionParameter
	{
		// Token: 0x17001DA2 RID: 7586
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x00088675 File Offset: 0x00086875
		// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x00088687 File Offset: 0x00086887
		[DataMember]
		public string IncomingServer
		{
			get
			{
				return (string)base["IncomingServer"];
			}
			set
			{
				base["IncomingServer"] = value;
			}
		}

		// Token: 0x17001DA3 RID: 7587
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x00088695 File Offset: 0x00086895
		// (set) Token: 0x06002BE9 RID: 11241 RVA: 0x000886B1 File Offset: 0x000868B1
		[DataMember]
		public int IncomingPort
		{
			get
			{
				return (int)(base["IncomingPort"] ?? 0);
			}
			set
			{
				base["IncomingPort"] = value;
			}
		}

		// Token: 0x17001DA4 RID: 7588
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000886C4 File Offset: 0x000868C4
		// (set) Token: 0x06002BEB RID: 11243 RVA: 0x000886D6 File Offset: 0x000868D6
		[DataMember]
		public string IncomingSecurity
		{
			get
			{
				return (string)base["IncomingSecurity"];
			}
			set
			{
				base["IncomingSecurity"] = value;
			}
		}

		// Token: 0x17001DA5 RID: 7589
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000886E4 File Offset: 0x000868E4
		// (set) Token: 0x06002BED RID: 11245 RVA: 0x000886F6 File Offset: 0x000868F6
		[DataMember]
		public string IncomingAuth
		{
			get
			{
				return (string)base["IncomingAuth"];
			}
			set
			{
				base["IncomingAuth"] = value;
			}
		}

		// Token: 0x17001DA6 RID: 7590
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x00088704 File Offset: 0x00086904
		// (set) Token: 0x06002BEF RID: 11247 RVA: 0x00088716 File Offset: 0x00086916
		[DataMember]
		public string IncomingUserName
		{
			get
			{
				return (string)base["IncomingUserName"];
			}
			set
			{
				base["IncomingUserName"] = value;
			}
		}

		// Token: 0x17001DA7 RID: 7591
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x00088724 File Offset: 0x00086924
		protected override string PasswordParameterName
		{
			get
			{
				return "IncomingPassword";
			}
		}
	}
}
