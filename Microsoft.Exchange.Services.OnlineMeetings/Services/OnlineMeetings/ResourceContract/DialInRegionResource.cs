using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000057 RID: 87
	[DataContract(Name = "dialInRegion")]
	internal class DialInRegionResource : Resource
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x00009295 File Offset: 0x00007495
		public DialInRegionResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000929E File Offset: 0x0000749E
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x000092AB File Offset: 0x000074AB
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name
		{
			get
			{
				return base.GetValue<string>("name");
			}
			set
			{
				base.SetValue<string>("name", value);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000092B9 File Offset: 0x000074B9
		// (set) Token: 0x060002BA RID: 698 RVA: 0x000092C6 File Offset: 0x000074C6
		[DataMember(Name = "languages", EmitDefaultValue = false)]
		public string[] Languages
		{
			get
			{
				return base.GetValue<string[]>("languages");
			}
			set
			{
				base.SetValue<string[]>("languages", value);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000092D4 File Offset: 0x000074D4
		// (set) Token: 0x060002BC RID: 700 RVA: 0x000092E1 File Offset: 0x000074E1
		[DataMember(Name = "number", EmitDefaultValue = false)]
		public string Number
		{
			get
			{
				return base.GetValue<string>("number");
			}
			set
			{
				base.SetValue<string>("number", value);
			}
		}

		// Token: 0x040001AD RID: 429
		public const string Token = "dialInRegion";

		// Token: 0x040001AE RID: 430
		private const string NamePropertyName = "name";

		// Token: 0x040001AF RID: 431
		private const string LanguagesPropertyName = "languages";

		// Token: 0x040001B0 RID: 432
		private const string NumberPropertyName = "number";
	}
}
