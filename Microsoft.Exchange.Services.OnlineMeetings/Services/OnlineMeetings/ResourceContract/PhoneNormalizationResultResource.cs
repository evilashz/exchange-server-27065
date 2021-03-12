using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x020000B1 RID: 177
	[DataContract(Name = "PhoneNormalizationResultResource")]
	[Get(typeof(PhoneNormalizationResultResource))]
	[Parent("Communication")]
	internal class PhoneNormalizationResultResource : Resource
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0000A8EC File Offset: 0x00008AEC
		public PhoneNormalizationResultResource() : base(string.Empty)
		{
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000A8F9 File Offset: 0x00008AF9
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000A906 File Offset: 0x00008B06
		[DataMember(Name = "NormalizedNumber", EmitDefaultValue = false)]
		public string NormalizedNumber
		{
			get
			{
				return base.GetValue<string>("NormalizedNumber");
			}
			set
			{
				base.SetValue<string>("NormalizedNumber", value);
			}
		}
	}
}
