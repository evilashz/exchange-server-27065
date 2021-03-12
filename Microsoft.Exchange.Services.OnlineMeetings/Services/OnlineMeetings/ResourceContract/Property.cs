using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200006E RID: 110
	[DataContract(Name = "Property")]
	internal class Property : Resource
	{
		// Token: 0x0600031A RID: 794 RVA: 0x00009881 File Offset: 0x00007A81
		public Property(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000988A File Offset: 0x00007A8A
		// (set) Token: 0x0600031C RID: 796 RVA: 0x00009897 File Offset: 0x00007A97
		[DataMember(Name = "Name", EmitDefaultValue = true)]
		public string Name
		{
			get
			{
				return base.GetValue<string>("Name");
			}
			set
			{
				base.SetValue<string>("Name", value);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000098A5 File Offset: 0x00007AA5
		// (set) Token: 0x0600031E RID: 798 RVA: 0x000098B2 File Offset: 0x00007AB2
		[DataMember(Name = "Value", EmitDefaultValue = true)]
		public string Value
		{
			get
			{
				return base.GetValue<string>("Value");
			}
			set
			{
				base.SetValue<string>("Value", value);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000098C0 File Offset: 0x00007AC0
		// (set) Token: 0x06000320 RID: 800 RVA: 0x000098CD File Offset: 0x00007ACD
		[Ignore]
		[DataMember(Name = "Values", EmitDefaultValue = false)]
		public PropertyBag Values
		{
			get
			{
				return base.GetValue<PropertyBag>("Values");
			}
			set
			{
				base.SetValue<PropertyBag>("Values", value);
			}
		}
	}
}
