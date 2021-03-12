using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200009A RID: 154
	[DataContract(Name = "MakeMeAvailableSettings")]
	internal class MakeMeAvailableSettings : Resource
	{
		// Token: 0x060003CD RID: 973 RVA: 0x0000A618 File Offset: 0x00008818
		public MakeMeAvailableSettings(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000A621 File Offset: 0x00008821
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000A62E File Offset: 0x0000882E
		[DataMember(Name = "PhoneNumber", EmitDefaultValue = false)]
		public string PhoneNumber
		{
			get
			{
				return base.GetValue<string>("PhoneNumber");
			}
			set
			{
				base.SetValue<string>("PhoneNumber", value);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000A63C File Offset: 0x0000883C
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000A649 File Offset: 0x00008849
		[DataMember(Name = "SupportedModalities", EmitDefaultValue = false)]
		public ModalityType[] SupportedModalities
		{
			get
			{
				return base.GetValue<ModalityType[]>("SupportedModalities");
			}
			set
			{
				base.SetValue<ModalityType[]>("SupportedModalities", value);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000A657 File Offset: 0x00008857
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000A664 File Offset: 0x00008864
		[DataMember(Name = "AudioPreference", EmitDefaultValue = false)]
		public AudioPreference AudioPreference
		{
			get
			{
				return base.GetValue<AudioPreference>("AudioPreference");
			}
			set
			{
				base.SetValue<AudioPreference>("AudioPreference", value);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000A677 File Offset: 0x00008877
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000A684 File Offset: 0x00008884
		[DataMember(Name = "InactiveInterval", EmitDefaultValue = false)]
		public TimeSpan InactiveInterval
		{
			get
			{
				return base.GetValue<TimeSpan>("InactiveInterval");
			}
			set
			{
				base.SetValue<TimeSpan>("InactiveInterval", value);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000A697 File Offset: 0x00008897
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000A6A4 File Offset: 0x000088A4
		[DataMember(Name = "AwayInterval", EmitDefaultValue = false)]
		public TimeSpan AwayInterval
		{
			get
			{
				return base.GetValue<TimeSpan>("AwayInterval");
			}
			set
			{
				base.SetValue<TimeSpan>("AwayInterval", value);
			}
		}
	}
}
