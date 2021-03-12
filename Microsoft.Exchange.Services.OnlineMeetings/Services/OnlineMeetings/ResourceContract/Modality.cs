using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200009F RID: 159
	[DataContract(Name = "modality")]
	internal class Modality : Resource
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000A7F9 File Offset: 0x000089F9
		public Modality() : base("no_uri")
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A806 File Offset: 0x00008A06
		public Modality(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000A80F File Offset: 0x00008A0F
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000A81C File Offset: 0x00008A1C
		[DataMember(Name = "type", EmitDefaultValue = false)]
		public ModalityType Type
		{
			get
			{
				return base.GetValue<ModalityType>("type");
			}
			set
			{
				base.SetValue<ModalityType>("type", value);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000A82F File Offset: 0x00008A2F
		public bool Equals(Modality other)
		{
			return other != null && this.Type == other.Type;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000A844 File Offset: 0x00008A44
		public override bool Equals(object obj)
		{
			Modality modality = obj as Modality;
			return modality != null && this.Type == modality.Type;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000A86B File Offset: 0x00008A6B
		public override int GetHashCode()
		{
			return this.Type.GetHashCode();
		}
	}
}
