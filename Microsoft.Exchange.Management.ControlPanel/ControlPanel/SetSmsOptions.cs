using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000484 RID: 1156
	[DataContract]
	public class SetSmsOptions : SetObjectProperties
	{
		// Token: 0x170022DE RID: 8926
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000AF993 File Offset: 0x000ADB93
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-TextMessagingAccount";
			}
		}

		// Token: 0x170022DF RID: 8927
		// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000AF99A File Offset: 0x000ADB9A
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x170022E0 RID: 8928
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000AF9A1 File Offset: 0x000ADBA1
		// (set) Token: 0x060039E6 RID: 14822 RVA: 0x000AF9B3 File Offset: 0x000ADBB3
		[DataMember]
		public string CountryRegionId
		{
			private get
			{
				return (string)base[TextMessagingAccountSchema.CountryRegionId];
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					base[TextMessagingAccountSchema.CountryRegionId] = value;
				}
			}
		}

		// Token: 0x170022E1 RID: 8929
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x000AF9C9 File Offset: 0x000ADBC9
		// (set) Token: 0x060039E8 RID: 14824 RVA: 0x000AF9D1 File Offset: 0x000ADBD1
		[DataMember]
		public string CountryCode { get; set; }

		// Token: 0x170022E2 RID: 8930
		// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000AF9DA File Offset: 0x000ADBDA
		// (set) Token: 0x060039EA RID: 14826 RVA: 0x000AF9E2 File Offset: 0x000ADBE2
		[DataMember]
		public string VerificationCode { get; set; }

		// Token: 0x170022E3 RID: 8931
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000AF9EB File Offset: 0x000ADBEB
		// (set) Token: 0x060039EC RID: 14828 RVA: 0x000AF9FD File Offset: 0x000ADBFD
		[DataMember]
		public string MobileOperatorId
		{
			get
			{
				return (string)base[TextMessagingAccountSchema.MobileOperatorId];
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					base[TextMessagingAccountSchema.MobileOperatorId] = value;
				}
			}
		}

		// Token: 0x170022E4 RID: 8932
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x000AFA13 File Offset: 0x000ADC13
		// (set) Token: 0x060039EE RID: 14830 RVA: 0x000AFA25 File Offset: 0x000ADC25
		[DataMember]
		public string NotificationPhoneNumber
		{
			get
			{
				return (string)base[TextMessagingAccountSchema.NotificationPhoneNumber];
			}
			set
			{
				base[TextMessagingAccountSchema.NotificationPhoneNumber] = value;
			}
		}
	}
}
