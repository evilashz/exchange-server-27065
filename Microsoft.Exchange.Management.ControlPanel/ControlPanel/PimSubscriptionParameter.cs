using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200029E RID: 670
	[DataContract]
	public abstract class PimSubscriptionParameter : SelfMailboxParameters
	{
		// Token: 0x06002B70 RID: 11120 RVA: 0x00087C16 File Offset: 0x00085E16
		public PimSubscriptionParameter()
		{
		}

		// Token: 0x17001D6F RID: 7535
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x00087C1E File Offset: 0x00085E1E
		// (set) Token: 0x06002B72 RID: 11122 RVA: 0x00087C30 File Offset: 0x00085E30
		[DataMember]
		public string EmailAddress
		{
			get
			{
				return (string)base["EmailAddress"];
			}
			set
			{
				base["EmailAddress"] = value.Trim();
			}
		}

		// Token: 0x17001D70 RID: 7536
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x00087C43 File Offset: 0x00085E43
		// (set) Token: 0x06002B74 RID: 11124 RVA: 0x00087C46 File Offset: 0x00085E46
		[DataMember]
		public string IncomingPassword
		{
			private get
			{
				return null;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					base[this.PasswordParameterName] = value.ToSecureString();
				}
			}
		}

		// Token: 0x17001D71 RID: 7537
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x00087C62 File Offset: 0x00085E62
		protected virtual string PasswordParameterName
		{
			get
			{
				return "Password";
			}
		}

		// Token: 0x17001D72 RID: 7538
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x00087C69 File Offset: 0x00085E69
		// (set) Token: 0x06002B77 RID: 11127 RVA: 0x00087C7B File Offset: 0x00085E7B
		[DataMember]
		public string DisplayName
		{
			get
			{
				return (string)base["DisplayName"];
			}
			set
			{
				base["DisplayName"] = value;
			}
		}
	}
}
