using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000ACE RID: 2766
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExtensionAlreadyUsedAsPilotNumberException : LocalizedException
	{
		// Token: 0x060080CA RID: 32970 RVA: 0x001A5E0D File Offset: 0x001A400D
		public ExtensionAlreadyUsedAsPilotNumberException(string phoneNumber, string dialPlan) : base(DirectoryStrings.ExtensionAlreadyUsedAsPilotNumber(phoneNumber, dialPlan))
		{
			this.phoneNumber = phoneNumber;
			this.dialPlan = dialPlan;
		}

		// Token: 0x060080CB RID: 32971 RVA: 0x001A5E2A File Offset: 0x001A402A
		public ExtensionAlreadyUsedAsPilotNumberException(string phoneNumber, string dialPlan, Exception innerException) : base(DirectoryStrings.ExtensionAlreadyUsedAsPilotNumber(phoneNumber, dialPlan), innerException)
		{
			this.phoneNumber = phoneNumber;
			this.dialPlan = dialPlan;
		}

		// Token: 0x060080CC RID: 32972 RVA: 0x001A5E48 File Offset: 0x001A4048
		protected ExtensionAlreadyUsedAsPilotNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.phoneNumber = (string)info.GetValue("phoneNumber", typeof(string));
			this.dialPlan = (string)info.GetValue("dialPlan", typeof(string));
		}

		// Token: 0x060080CD RID: 32973 RVA: 0x001A5E9D File Offset: 0x001A409D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("phoneNumber", this.phoneNumber);
			info.AddValue("dialPlan", this.dialPlan);
		}

		// Token: 0x17002EE9 RID: 12009
		// (get) Token: 0x060080CE RID: 32974 RVA: 0x001A5EC9 File Offset: 0x001A40C9
		public string PhoneNumber
		{
			get
			{
				return this.phoneNumber;
			}
		}

		// Token: 0x17002EEA RID: 12010
		// (get) Token: 0x060080CF RID: 32975 RVA: 0x001A5ED1 File Offset: 0x001A40D1
		public string DialPlan
		{
			get
			{
				return this.dialPlan;
			}
		}

		// Token: 0x040055C3 RID: 21955
		private readonly string phoneNumber;

		// Token: 0x040055C4 RID: 21956
		private readonly string dialPlan;
	}
}
