using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002D5 RID: 725
	[DataContract]
	public abstract class PopSubscriptionBaseParameter : PimSubscriptionParameter
	{
		// Token: 0x17001DF3 RID: 7667
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x00089899 File Offset: 0x00087A99
		// (set) Token: 0x06002CA8 RID: 11432 RVA: 0x000898B5 File Offset: 0x00087AB5
		[DataMember]
		public bool LeaveOnServer
		{
			get
			{
				return (bool)(base["LeaveOnServer"] ?? true);
			}
			set
			{
				base["LeaveOnServer"] = value;
			}
		}

		// Token: 0x17001DF4 RID: 7668
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x000898C8 File Offset: 0x00087AC8
		// (set) Token: 0x06002CAA RID: 11434 RVA: 0x000898DA File Offset: 0x00087ADA
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

		// Token: 0x17001DF5 RID: 7669
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x000898E8 File Offset: 0x00087AE8
		// (set) Token: 0x06002CAC RID: 11436 RVA: 0x00089904 File Offset: 0x00087B04
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

		// Token: 0x17001DF6 RID: 7670
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x00089917 File Offset: 0x00087B17
		// (set) Token: 0x06002CAE RID: 11438 RVA: 0x00089929 File Offset: 0x00087B29
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

		// Token: 0x17001DF7 RID: 7671
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x00089937 File Offset: 0x00087B37
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x00089949 File Offset: 0x00087B49
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

		// Token: 0x17001DF8 RID: 7672
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x00089957 File Offset: 0x00087B57
		// (set) Token: 0x06002CB2 RID: 11442 RVA: 0x00089969 File Offset: 0x00087B69
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

		// Token: 0x17001DF9 RID: 7673
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x00089977 File Offset: 0x00087B77
		protected override string PasswordParameterName
		{
			get
			{
				return "IncomingPassword";
			}
		}
	}
}
