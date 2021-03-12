using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ECF RID: 3791
	[Serializable]
	public class Emitter
	{
		// Token: 0x060082C9 RID: 33481 RVA: 0x00239F85 File Offset: 0x00238185
		public Emitter()
		{
		}

		// Token: 0x060082CA RID: 33482 RVA: 0x00239F8D File Offset: 0x0023818D
		public Emitter(EmitterType type, int priority, bool exclusive, IEnumerable<E164Number> numbers)
		{
			this.Type = type;
			if (numbers != null)
			{
				this.PhoneNumbers = new List<E164Number>(numbers);
			}
		}

		// Token: 0x170022AD RID: 8877
		// (get) Token: 0x060082CB RID: 33483 RVA: 0x00239FAD File Offset: 0x002381AD
		// (set) Token: 0x060082CC RID: 33484 RVA: 0x00239FB5 File Offset: 0x002381B5
		[XmlElement("Type")]
		public EmitterType Type { get; set; }

		// Token: 0x170022AE RID: 8878
		// (get) Token: 0x060082CD RID: 33485 RVA: 0x00239FBE File Offset: 0x002381BE
		// (set) Token: 0x060082CE RID: 33486 RVA: 0x00239FC6 File Offset: 0x002381C6
		[XmlElement("Priority")]
		public int Priority { get; set; }

		// Token: 0x170022AF RID: 8879
		// (get) Token: 0x060082CF RID: 33487 RVA: 0x00239FCF File Offset: 0x002381CF
		// (set) Token: 0x060082D0 RID: 33488 RVA: 0x00239FD7 File Offset: 0x002381D7
		[XmlElement("Exclusive")]
		public bool Exclusive { get; set; }

		// Token: 0x170022B0 RID: 8880
		// (get) Token: 0x060082D1 RID: 33489 RVA: 0x00239FE0 File Offset: 0x002381E0
		// (set) Token: 0x060082D2 RID: 33490 RVA: 0x00239FED File Offset: 0x002381ED
		[XmlElement("PhoneNumber")]
		public List<E164Number> PhoneNumbers
		{
			get
			{
				return AccessorTemplates.ListPropertyGetter<E164Number>(ref this.phoneNumbers);
			}
			set
			{
				AccessorTemplates.ListPropertySetter<E164Number>(ref this.phoneNumbers, value);
			}
		}

		// Token: 0x040057C0 RID: 22464
		private List<E164Number> phoneNumbers;
	}
}
