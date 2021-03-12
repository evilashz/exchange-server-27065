using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Inference.PeopleRelevance;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200060D RID: 1549
	[XmlType("PersonaType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "PersonaType")]
	[Serializable]
	public class Persona : ServiceObject
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002F95 RID: 12181 RVA: 0x000B431D File Offset: 0x000B251D
		// (set) Token: 0x06002F96 RID: 12182 RVA: 0x000B432F File Offset: 0x000B252F
		[XmlElement]
		[DataMember(Order = 1)]
		public ItemId PersonaId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ItemId>(PersonaSchema.PersonaId);
			}
			set
			{
				base.PropertyBag[PersonaSchema.PersonaId] = value;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x000B4342 File Offset: 0x000B2542
		// (set) Token: 0x06002F98 RID: 12184 RVA: 0x000B4354 File Offset: 0x000B2554
		[DataMember(Name = "PersonaTypeString", Order = 2)]
		[XmlElement(ElementName = "PersonaType")]
		public string PersonaType
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.PersonaType);
			}
			set
			{
				base.PropertyBag[PersonaSchema.PersonaType] = value;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000B4367 File Offset: 0x000B2567
		// (set) Token: 0x06002F9A RID: 12186 RVA: 0x000B436A File Offset: 0x000B256A
		[DataMember(Name = "PersonaObjectStatusString", EmitDefaultValue = false, Order = 3)]
		public string PersonaObjectStatus
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x000B436C File Offset: 0x000B256C
		// (set) Token: 0x06002F9C RID: 12188 RVA: 0x000B437E File Offset: 0x000B257E
		[XmlElement]
		[DataMember(Name = "CreationTimeString", EmitDefaultValue = false, Order = 4)]
		[DateTimeString]
		public string CreationTime
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.CreationTime);
			}
			set
			{
				base.PropertyBag[PersonaSchema.CreationTime] = value;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000B4391 File Offset: 0x000B2591
		// (set) Token: 0x06002F9E RID: 12190 RVA: 0x000B43A3 File Offset: 0x000B25A3
		[DataMember(Name = "BodiesArray", EmitDefaultValue = false, Order = 5)]
		[XmlArray]
		[XmlArrayItem("BodyContentAttributedValue", typeof(BodyContentAttributedValue))]
		public BodyContentAttributedValue[] Bodies
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<BodyContentAttributedValue[]>(PersonaSchema.Bodies);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Bodies] = value;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000B43B6 File Offset: 0x000B25B6
		// (set) Token: 0x06002FA0 RID: 12192 RVA: 0x000B43C8 File Offset: 0x000B25C8
		[DataMember(Name = "IsFavorite", EmitDefaultValue = false, Order = 6)]
		[XmlIgnore]
		public bool IsFavorite
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool>(PersonaSchema.IsFavorite);
			}
			set
			{
				base.PropertyBag[PersonaSchema.IsFavorite] = value;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x000B43E0 File Offset: 0x000B25E0
		// (set) Token: 0x06002FA2 RID: 12194 RVA: 0x000B43F2 File Offset: 0x000B25F2
		[DataMember(Name = "UnreadCount", EmitDefaultValue = false, Order = 7)]
		[XmlIgnore]
		public int UnreadCount
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int>(PersonaSchema.UnreadCount);
			}
			set
			{
				base.PropertyBag[PersonaSchema.UnreadCount] = value;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000B440A File Offset: 0x000B260A
		// (set) Token: 0x06002FA4 RID: 12196 RVA: 0x000B441C File Offset: 0x000B261C
		[DataMember(EmitDefaultValue = false, Order = 10)]
		[XmlElement]
		public string DisplayNameFirstLastSortKey
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.DisplayNameFirstLastSortKey);
			}
			set
			{
				base.PropertyBag[PersonaSchema.DisplayNameFirstLastSortKey] = value;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x000B442F File Offset: 0x000B262F
		// (set) Token: 0x06002FA6 RID: 12198 RVA: 0x000B4441 File Offset: 0x000B2641
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 11)]
		public string DisplayNameLastFirstSortKey
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.DisplayNameLastFirstSortKey);
			}
			set
			{
				base.PropertyBag[PersonaSchema.DisplayNameLastFirstSortKey] = value;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000B4454 File Offset: 0x000B2654
		// (set) Token: 0x06002FA8 RID: 12200 RVA: 0x000B4466 File Offset: 0x000B2666
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 12)]
		public string CompanyNameSortKey
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.CompanyNameSortKey);
			}
			set
			{
				base.PropertyBag[PersonaSchema.CompanyNameSortKey] = value;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x000B4479 File Offset: 0x000B2679
		// (set) Token: 0x06002FAA RID: 12202 RVA: 0x000B448B File Offset: 0x000B268B
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 13)]
		public string HomeCitySortKey
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.HomeCitySortKey);
			}
			set
			{
				base.PropertyBag[PersonaSchema.HomeCitySortKey] = value;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000B449E File Offset: 0x000B269E
		// (set) Token: 0x06002FAC RID: 12204 RVA: 0x000B44B0 File Offset: 0x000B26B0
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 14)]
		public string WorkCitySortKey
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.WorkCitySortKey);
			}
			set
			{
				base.PropertyBag[PersonaSchema.WorkCitySortKey] = value;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x000B44C3 File Offset: 0x000B26C3
		// (set) Token: 0x06002FAE RID: 12206 RVA: 0x000B44D5 File Offset: 0x000B26D5
		[DataMember(EmitDefaultValue = false, Order = 20)]
		[XmlElement]
		public string DisplayNameFirstLastHeader
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.DisplayNameFirstLastHeader);
			}
			set
			{
				base.PropertyBag[PersonaSchema.DisplayNameFirstLastHeader] = value;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000B44E8 File Offset: 0x000B26E8
		// (set) Token: 0x06002FB0 RID: 12208 RVA: 0x000B44FA File Offset: 0x000B26FA
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 21)]
		public string DisplayNameLastFirstHeader
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.DisplayNameLastFirstHeader);
			}
			set
			{
				base.PropertyBag[PersonaSchema.DisplayNameLastFirstHeader] = value;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000B450D File Offset: 0x000B270D
		// (set) Token: 0x06002FB2 RID: 12210 RVA: 0x000B451F File Offset: 0x000B271F
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 50)]
		public string DisplayName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.DisplayName);
			}
			set
			{
				base.PropertyBag[PersonaSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000B4532 File Offset: 0x000B2732
		// (set) Token: 0x06002FB4 RID: 12212 RVA: 0x000B4544 File Offset: 0x000B2744
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 51)]
		public string DisplayNameFirstLast
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.DisplayNameFirstLast);
			}
			set
			{
				base.PropertyBag[PersonaSchema.DisplayNameFirstLast] = value;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x000B4557 File Offset: 0x000B2757
		// (set) Token: 0x06002FB6 RID: 12214 RVA: 0x000B4569 File Offset: 0x000B2769
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 52)]
		public string DisplayNameLastFirst
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.DisplayNameLastFirst);
			}
			set
			{
				base.PropertyBag[PersonaSchema.DisplayNameLastFirst] = value;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000B457C File Offset: 0x000B277C
		// (set) Token: 0x06002FB8 RID: 12216 RVA: 0x000B458E File Offset: 0x000B278E
		[DataMember(EmitDefaultValue = false, Order = 53)]
		[XmlElement]
		public string FileAs
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.FileAs);
			}
			set
			{
				base.PropertyBag[PersonaSchema.FileAs] = value;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x000B45A1 File Offset: 0x000B27A1
		// (set) Token: 0x06002FBA RID: 12218 RVA: 0x000B45B3 File Offset: 0x000B27B3
		[DataMember(EmitDefaultValue = false, Order = 54)]
		[XmlElement]
		public string FileAsId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.FileAsId);
			}
			set
			{
				base.PropertyBag[PersonaSchema.FileAsId] = value;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000B45C6 File Offset: 0x000B27C6
		// (set) Token: 0x06002FBC RID: 12220 RVA: 0x000B45D8 File Offset: 0x000B27D8
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 55)]
		public string DisplayNamePrefix
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.DisplayNamePrefix);
			}
			set
			{
				base.PropertyBag[PersonaSchema.DisplayNamePrefix] = value;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x000B45EB File Offset: 0x000B27EB
		// (set) Token: 0x06002FBE RID: 12222 RVA: 0x000B45FD File Offset: 0x000B27FD
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 56)]
		public string GivenName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.GivenName);
			}
			set
			{
				base.PropertyBag[PersonaSchema.GivenName] = value;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x000B4610 File Offset: 0x000B2810
		// (set) Token: 0x06002FC0 RID: 12224 RVA: 0x000B4622 File Offset: 0x000B2822
		[DataMember(EmitDefaultValue = false, Order = 57)]
		[XmlElement]
		public string MiddleName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.MiddleName);
			}
			set
			{
				base.PropertyBag[PersonaSchema.MiddleName] = value;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000B4635 File Offset: 0x000B2835
		// (set) Token: 0x06002FC2 RID: 12226 RVA: 0x000B4647 File Offset: 0x000B2847
		[DataMember(EmitDefaultValue = false, Order = 58)]
		[XmlElement]
		public string Surname
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.Surname);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Surname] = value;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000B465A File Offset: 0x000B285A
		// (set) Token: 0x06002FC4 RID: 12228 RVA: 0x000B466C File Offset: 0x000B286C
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 59)]
		public string Generation
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.Generation);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Generation] = value;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000B467F File Offset: 0x000B287F
		// (set) Token: 0x06002FC6 RID: 12230 RVA: 0x000B4691 File Offset: 0x000B2891
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 60)]
		public string Nickname
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.Nickname);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Nickname] = value;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x000B46A4 File Offset: 0x000B28A4
		// (set) Token: 0x06002FC8 RID: 12232 RVA: 0x000B46B6 File Offset: 0x000B28B6
		[DataMember(EmitDefaultValue = false, Order = 61)]
		[XmlElement]
		public string YomiFirstName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.YomiFirstName);
			}
			set
			{
				base.PropertyBag[PersonaSchema.YomiFirstName] = value;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000B46C9 File Offset: 0x000B28C9
		// (set) Token: 0x06002FCA RID: 12234 RVA: 0x000B46DB File Offset: 0x000B28DB
		[DataMember(EmitDefaultValue = false, Order = 62)]
		[XmlElement]
		public string YomiLastName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.YomiLastName);
			}
			set
			{
				base.PropertyBag[PersonaSchema.YomiLastName] = value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x000B46EE File Offset: 0x000B28EE
		// (set) Token: 0x06002FCC RID: 12236 RVA: 0x000B4700 File Offset: 0x000B2900
		[DataMember(EmitDefaultValue = false, Order = 63)]
		[XmlElement]
		public string YomiCompanyName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.YomiCompanyName);
			}
			set
			{
				base.PropertyBag[PersonaSchema.YomiCompanyName] = value;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000B4713 File Offset: 0x000B2913
		// (set) Token: 0x06002FCE RID: 12238 RVA: 0x000B4725 File Offset: 0x000B2925
		[DataMember(EmitDefaultValue = false, Order = 64)]
		[XmlElement]
		public string Title
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.Title);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Title] = value;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000B4738 File Offset: 0x000B2938
		// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x000B474A File Offset: 0x000B294A
		[DataMember(EmitDefaultValue = false, Order = 65)]
		[XmlElement]
		public string Department
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.Department);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Department] = value;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000B475D File Offset: 0x000B295D
		// (set) Token: 0x06002FD2 RID: 12242 RVA: 0x000B476F File Offset: 0x000B296F
		[DataMember(EmitDefaultValue = false, Order = 66)]
		[XmlElement]
		public string CompanyName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.CompanyName);
			}
			set
			{
				base.PropertyBag[PersonaSchema.CompanyName] = value;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000B4782 File Offset: 0x000B2982
		// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x000B4794 File Offset: 0x000B2994
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 67)]
		public string Location
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.Location);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Location] = value;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000B47A7 File Offset: 0x000B29A7
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x000B47B9 File Offset: 0x000B29B9
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 68)]
		public EmailAddressWrapper EmailAddress
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressWrapper>(PersonaSchema.EmailAddress);
			}
			set
			{
				base.PropertyBag[PersonaSchema.EmailAddress] = value;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x000B47CC File Offset: 0x000B29CC
		// (set) Token: 0x06002FD8 RID: 12248 RVA: 0x000B47DE File Offset: 0x000B29DE
		[DataMember(EmitDefaultValue = false, Order = 69)]
		[XmlArray]
		[XmlArrayItem("Address", typeof(EmailAddressWrapper))]
		public EmailAddressWrapper[] EmailAddresses
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressWrapper[]>(PersonaSchema.EmailAddresses);
			}
			set
			{
				base.PropertyBag[PersonaSchema.EmailAddresses] = value;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x000B47F1 File Offset: 0x000B29F1
		// (set) Token: 0x06002FDA RID: 12250 RVA: 0x000B4803 File Offset: 0x000B2A03
		[DataMember(EmitDefaultValue = false, Order = 70)]
		[XmlElement]
		public PhoneNumber PhoneNumber
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumber>(PersonaSchema.PhoneNumber);
			}
			set
			{
				base.PropertyBag[PersonaSchema.PhoneNumber] = value;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x000B4816 File Offset: 0x000B2A16
		// (set) Token: 0x06002FDC RID: 12252 RVA: 0x000B4828 File Offset: 0x000B2A28
		[DataMember(EmitDefaultValue = false, Order = 71)]
		[XmlElement]
		public string ImAddress
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.ImAddress);
			}
			set
			{
				base.PropertyBag[PersonaSchema.ImAddress] = value;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x000B483B File Offset: 0x000B2A3B
		// (set) Token: 0x06002FDE RID: 12254 RVA: 0x000B484D File Offset: 0x000B2A4D
		[XmlElement]
		[DataMember(EmitDefaultValue = false, Order = 72)]
		public string HomeCity
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.HomeCity);
			}
			set
			{
				base.PropertyBag[PersonaSchema.HomeCity] = value;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000B4860 File Offset: 0x000B2A60
		// (set) Token: 0x06002FE0 RID: 12256 RVA: 0x000B4872 File Offset: 0x000B2A72
		[DataMember(EmitDefaultValue = false, Order = 73)]
		[XmlElement]
		public string WorkCity
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.WorkCity);
			}
			set
			{
				base.PropertyBag[PersonaSchema.WorkCity] = value;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x000B4885 File Offset: 0x000B2A85
		// (set) Token: 0x06002FE2 RID: 12258 RVA: 0x000B4897 File Offset: 0x000B2A97
		[DataMember(EmitDefaultValue = false, Order = 74)]
		[XmlIgnore]
		public string Alias
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(PersonaSchema.Alias);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Alias] = value;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000B48AA File Offset: 0x000B2AAA
		// (set) Token: 0x06002FE4 RID: 12260 RVA: 0x000B48C1 File Offset: 0x000B2AC1
		[DataMember(EmitDefaultValue = false, Order = 80)]
		[XmlElement]
		public int RelevanceScore
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int>(PersonaSchema.RelevanceScore, int.MaxValue);
			}
			set
			{
				base.PropertyBag[PersonaSchema.RelevanceScore] = value;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x000B48D9 File Offset: 0x000B2AD9
		// (set) Token: 0x06002FE6 RID: 12262 RVA: 0x000B48EB File Offset: 0x000B2AEB
		[DataMember(EmitDefaultValue = false, Order = 100)]
		[XmlArray]
		[XmlArrayItem("FolderId", typeof(FolderId))]
		public FolderId[] FolderIds
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<FolderId[]>(PersonaSchema.FolderIds);
			}
			set
			{
				base.PropertyBag[PersonaSchema.FolderIds] = value;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x000B48FE File Offset: 0x000B2AFE
		// (set) Token: 0x06002FE8 RID: 12264 RVA: 0x000B4910 File Offset: 0x000B2B10
		[XmlArray]
		[XmlArrayItem("Attribution", typeof(Attribution))]
		[DataMember(Name = "AttributionsArray", EmitDefaultValue = false, Order = 102)]
		public Attribution[] Attributions
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<Attribution[]>(PersonaSchema.Attributions);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Attributions] = value;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x000B4923 File Offset: 0x000B2B23
		// (set) Token: 0x06002FEA RID: 12266 RVA: 0x000B4935 File Offset: 0x000B2B35
		[DataMember(EmitDefaultValue = false, Order = 103)]
		[XmlIgnore]
		public EmailAddressWrapper[] Members
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressWrapper[]>(PersonaSchema.Members);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Members] = value;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x000B4948 File Offset: 0x000B2B48
		// (set) Token: 0x06002FEC RID: 12268 RVA: 0x000B495A File Offset: 0x000B2B5A
		[DataMember(Name = "ThirdPartyPhotoUrlsArray", EmitDefaultValue = false, Order = 104)]
		[XmlIgnore]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] ThirdPartyPhotoUrls
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.ThirdPartyPhotoUrls);
			}
			set
			{
				base.PropertyBag[PersonaSchema.ThirdPartyPhotoUrls] = value;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x000B496D File Offset: 0x000B2B6D
		// (set) Token: 0x06002FEE RID: 12270 RVA: 0x000B497F File Offset: 0x000B2B7F
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[XmlArray]
		[DataMember(Name = "DisplayNamesArray", EmitDefaultValue = false, Order = 200)]
		public StringAttributedValue[] DisplayNames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.DisplayNames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.DisplayNames] = value;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x000B4992 File Offset: 0x000B2B92
		// (set) Token: 0x06002FF0 RID: 12272 RVA: 0x000B49A4 File Offset: 0x000B2BA4
		[DataMember(Name = "FileAsesArray", EmitDefaultValue = false, Order = 201)]
		[XmlArray]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] FileAses
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.FileAses);
			}
			set
			{
				base.PropertyBag[PersonaSchema.FileAses] = value;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x000B49B7 File Offset: 0x000B2BB7
		// (set) Token: 0x06002FF2 RID: 12274 RVA: 0x000B49C9 File Offset: 0x000B2BC9
		[DataMember(Name = "FileAsIdsArray", EmitDefaultValue = false, Order = 202)]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[XmlArray]
		public StringAttributedValue[] FileAsIds
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.FileAsIds);
			}
			set
			{
				base.PropertyBag[PersonaSchema.FileAsIds] = value;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x000B49DC File Offset: 0x000B2BDC
		// (set) Token: 0x06002FF4 RID: 12276 RVA: 0x000B49EE File Offset: 0x000B2BEE
		[DataMember(Name = "DisplayNamePrefixesArray", EmitDefaultValue = false, Order = 203)]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[XmlArray]
		public StringAttributedValue[] DisplayNamePrefixes
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.DisplayNamePrefixes);
			}
			set
			{
				base.PropertyBag[PersonaSchema.DisplayNamePrefixes] = value;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x000B4A01 File Offset: 0x000B2C01
		// (set) Token: 0x06002FF6 RID: 12278 RVA: 0x000B4A13 File Offset: 0x000B2C13
		[DataMember(Name = "GivenNamesArray", EmitDefaultValue = false, Order = 204)]
		[XmlArray]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] GivenNames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.GivenNames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.GivenNames] = value;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002FF7 RID: 12279 RVA: 0x000B4A26 File Offset: 0x000B2C26
		// (set) Token: 0x06002FF8 RID: 12280 RVA: 0x000B4A38 File Offset: 0x000B2C38
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[XmlArray]
		[DataMember(Name = "MiddleNamesArray", EmitDefaultValue = false, Order = 205)]
		public StringAttributedValue[] MiddleNames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.MiddleNames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.MiddleNames] = value;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002FF9 RID: 12281 RVA: 0x000B4A4B File Offset: 0x000B2C4B
		// (set) Token: 0x06002FFA RID: 12282 RVA: 0x000B4A5D File Offset: 0x000B2C5D
		[XmlArray]
		[DataMember(Name = "SurnamesArray", EmitDefaultValue = false, Order = 206)]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] Surnames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Surnames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Surnames] = value;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002FFB RID: 12283 RVA: 0x000B4A70 File Offset: 0x000B2C70
		// (set) Token: 0x06002FFC RID: 12284 RVA: 0x000B4A82 File Offset: 0x000B2C82
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "GenerationsArray", EmitDefaultValue = false, Order = 207)]
		[XmlArray]
		public StringAttributedValue[] Generations
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Generations);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Generations] = value;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000B4A95 File Offset: 0x000B2C95
		// (set) Token: 0x06002FFE RID: 12286 RVA: 0x000B4AA7 File Offset: 0x000B2CA7
		[XmlArray]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "NicknamesArray", EmitDefaultValue = false, Order = 208)]
		public StringAttributedValue[] Nicknames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Nicknames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Nicknames] = value;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002FFF RID: 12287 RVA: 0x000B4ABA File Offset: 0x000B2CBA
		// (set) Token: 0x06003000 RID: 12288 RVA: 0x000B4ACC File Offset: 0x000B2CCC
		[DataMember(Name = "InitialsArray", EmitDefaultValue = false, Order = 209)]
		[XmlArray]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] Initials
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Initials);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Initials] = value;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x000B4ADF File Offset: 0x000B2CDF
		// (set) Token: 0x06003002 RID: 12290 RVA: 0x000B4AF1 File Offset: 0x000B2CF1
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "YomiFirstNamesArray", EmitDefaultValue = false, Order = 210)]
		[XmlArray]
		public StringAttributedValue[] YomiFirstNames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.YomiFirstNames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.YomiFirstNames] = value;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x000B4B04 File Offset: 0x000B2D04
		// (set) Token: 0x06003004 RID: 12292 RVA: 0x000B4B16 File Offset: 0x000B2D16
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "YomiLastNamesArray", EmitDefaultValue = false, Order = 211)]
		[XmlArray]
		public StringAttributedValue[] YomiLastNames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.YomiLastNames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.YomiLastNames] = value;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x000B4B29 File Offset: 0x000B2D29
		// (set) Token: 0x06003006 RID: 12294 RVA: 0x000B4B3B File Offset: 0x000B2D3B
		[DataMember(Name = "YomiCompanyNamesArray", EmitDefaultValue = false, Order = 212)]
		[XmlArray]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] YomiCompanyNames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.YomiCompanyNames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.YomiCompanyNames] = value;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x000B4B4E File Offset: 0x000B2D4E
		// (set) Token: 0x06003008 RID: 12296 RVA: 0x000B4B60 File Offset: 0x000B2D60
		[XmlArray]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[DataMember(Name = "BusinessPhoneNumbersArray", EmitDefaultValue = false, Order = 300)]
		public PhoneNumberAttributedValue[] BusinessPhoneNumbers
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.BusinessPhoneNumbers);
			}
			set
			{
				base.PropertyBag[PersonaSchema.BusinessPhoneNumbers] = value;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x000B4B73 File Offset: 0x000B2D73
		// (set) Token: 0x0600300A RID: 12298 RVA: 0x000B4B85 File Offset: 0x000B2D85
		[DataMember(Name = "BusinessPhoneNumbers2Array", EmitDefaultValue = false, Order = 301)]
		[XmlArray]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		public PhoneNumberAttributedValue[] BusinessPhoneNumbers2
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.BusinessPhoneNumbers2);
			}
			set
			{
				base.PropertyBag[PersonaSchema.BusinessPhoneNumbers2] = value;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x000B4B98 File Offset: 0x000B2D98
		// (set) Token: 0x0600300C RID: 12300 RVA: 0x000B4BAA File Offset: 0x000B2DAA
		[XmlArray]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[DataMember(Name = "HomePhonesArray", EmitDefaultValue = false, Order = 302)]
		public PhoneNumberAttributedValue[] HomePhones
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.HomePhones);
			}
			set
			{
				base.PropertyBag[PersonaSchema.HomePhones] = value;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x000B4BBD File Offset: 0x000B2DBD
		// (set) Token: 0x0600300E RID: 12302 RVA: 0x000B4BCF File Offset: 0x000B2DCF
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[DataMember(Name = "HomePhones2Array", EmitDefaultValue = false, Order = 304)]
		[XmlArray]
		public PhoneNumberAttributedValue[] HomePhones2
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.HomePhones2);
			}
			set
			{
				base.PropertyBag[PersonaSchema.HomePhones2] = value;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x000B4BE2 File Offset: 0x000B2DE2
		// (set) Token: 0x06003010 RID: 12304 RVA: 0x000B4BF4 File Offset: 0x000B2DF4
		[XmlArray]
		[DataMember(Name = "MobilePhonesArray", EmitDefaultValue = false, Order = 305)]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		public PhoneNumberAttributedValue[] MobilePhones
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.MobilePhones);
			}
			set
			{
				base.PropertyBag[PersonaSchema.MobilePhones] = value;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x000B4C07 File Offset: 0x000B2E07
		// (set) Token: 0x06003012 RID: 12306 RVA: 0x000B4C19 File Offset: 0x000B2E19
		[XmlArray]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[DataMember(Name = "MobilePhones2Array", EmitDefaultValue = false, Order = 306)]
		public PhoneNumberAttributedValue[] MobilePhones2
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.MobilePhones2);
			}
			set
			{
				base.PropertyBag[PersonaSchema.MobilePhones2] = value;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x000B4C2C File Offset: 0x000B2E2C
		// (set) Token: 0x06003014 RID: 12308 RVA: 0x000B4C3E File Offset: 0x000B2E3E
		[DataMember(Name = "AssistantPhoneNumbersArray", EmitDefaultValue = false, Order = 307)]
		[XmlArray]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		public PhoneNumberAttributedValue[] AssistantPhoneNumbers
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.AssistantPhoneNumbers);
			}
			set
			{
				base.PropertyBag[PersonaSchema.AssistantPhoneNumbers] = value;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x000B4C51 File Offset: 0x000B2E51
		// (set) Token: 0x06003016 RID: 12310 RVA: 0x000B4C63 File Offset: 0x000B2E63
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[XmlArray]
		[DataMember(Name = "CallbackPhonesArray", EmitDefaultValue = false, Order = 308)]
		public PhoneNumberAttributedValue[] CallbackPhones
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.CallbackPhones);
			}
			set
			{
				base.PropertyBag[PersonaSchema.CallbackPhones] = value;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x000B4C76 File Offset: 0x000B2E76
		// (set) Token: 0x06003018 RID: 12312 RVA: 0x000B4C88 File Offset: 0x000B2E88
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[DataMember(Name = "CarPhonesArray", EmitDefaultValue = false, Order = 309)]
		[XmlArray]
		public PhoneNumberAttributedValue[] CarPhones
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.CarPhones);
			}
			set
			{
				base.PropertyBag[PersonaSchema.CarPhones] = value;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x000B4C9B File Offset: 0x000B2E9B
		// (set) Token: 0x0600301A RID: 12314 RVA: 0x000B4CAD File Offset: 0x000B2EAD
		[DataMember(Name = "HomeFaxesArray", EmitDefaultValue = false, Order = 313)]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[XmlArray]
		public PhoneNumberAttributedValue[] HomeFaxes
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.HomeFaxes);
			}
			set
			{
				base.PropertyBag[PersonaSchema.HomeFaxes] = value;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x000B4CC0 File Offset: 0x000B2EC0
		// (set) Token: 0x0600301C RID: 12316 RVA: 0x000B4CD2 File Offset: 0x000B2ED2
		[DataMember(Name = "OrganizationMainPhonesArray", EmitDefaultValue = false, Order = 314)]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[XmlArray]
		public PhoneNumberAttributedValue[] OrganizationMainPhones
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.OrganizationMainPhones);
			}
			set
			{
				base.PropertyBag[PersonaSchema.OrganizationMainPhones] = value;
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x000B4CE5 File Offset: 0x000B2EE5
		// (set) Token: 0x0600301E RID: 12318 RVA: 0x000B4CF7 File Offset: 0x000B2EF7
		[DataMember(Name = "OtherFaxesArray", EmitDefaultValue = false, Order = 315)]
		[XmlArray]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		public PhoneNumberAttributedValue[] OtherFaxes
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.OtherFaxes);
			}
			set
			{
				base.PropertyBag[PersonaSchema.OtherFaxes] = value;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600301F RID: 12319 RVA: 0x000B4D0A File Offset: 0x000B2F0A
		// (set) Token: 0x06003020 RID: 12320 RVA: 0x000B4D1C File Offset: 0x000B2F1C
		[XmlArray]
		[DataMember(Name = "OtherTelephonesArray", EmitDefaultValue = false, Order = 316)]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		public PhoneNumberAttributedValue[] OtherTelephones
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.OtherTelephones);
			}
			set
			{
				base.PropertyBag[PersonaSchema.OtherTelephones] = value;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x000B4D2F File Offset: 0x000B2F2F
		// (set) Token: 0x06003022 RID: 12322 RVA: 0x000B4D41 File Offset: 0x000B2F41
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[DataMember(Name = "OtherPhones2Array", EmitDefaultValue = false, Order = 317)]
		[XmlArray]
		public PhoneNumberAttributedValue[] OtherPhones2
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.OtherPhones2);
			}
			set
			{
				base.PropertyBag[PersonaSchema.OtherPhones2] = value;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x000B4D54 File Offset: 0x000B2F54
		// (set) Token: 0x06003024 RID: 12324 RVA: 0x000B4D66 File Offset: 0x000B2F66
		[XmlArray]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[DataMember(Name = "PagersArray", EmitDefaultValue = false, Order = 318)]
		public PhoneNumberAttributedValue[] Pagers
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.Pagers);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Pagers] = value;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x000B4D79 File Offset: 0x000B2F79
		// (set) Token: 0x06003026 RID: 12326 RVA: 0x000B4D8B File Offset: 0x000B2F8B
		[DataMember(Name = "RadioPhonesArray", EmitDefaultValue = false, Order = 319)]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[XmlArray]
		public PhoneNumberAttributedValue[] RadioPhones
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.RadioPhones);
			}
			set
			{
				base.PropertyBag[PersonaSchema.RadioPhones] = value;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x000B4D9E File Offset: 0x000B2F9E
		// (set) Token: 0x06003028 RID: 12328 RVA: 0x000B4DB0 File Offset: 0x000B2FB0
		[XmlArray]
		[DataMember(Name = "TelexNumbersArray", EmitDefaultValue = false, Order = 320)]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		public PhoneNumberAttributedValue[] TelexNumbers
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.TelexNumbers);
			}
			set
			{
				base.PropertyBag[PersonaSchema.TelexNumbers] = value;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003029 RID: 12329 RVA: 0x000B4DC3 File Offset: 0x000B2FC3
		// (set) Token: 0x0600302A RID: 12330 RVA: 0x000B4DD5 File Offset: 0x000B2FD5
		[XmlArray]
		[DataMember(Name = "TTYTDDPhoneNumbersArray", EmitDefaultValue = false, Order = 321)]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		public PhoneNumberAttributedValue[] TTYTDDPhoneNumbers
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.TTYTDDPhoneNumbers);
			}
			set
			{
				base.PropertyBag[PersonaSchema.TTYTDDPhoneNumbers] = value;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x000B4DE8 File Offset: 0x000B2FE8
		// (set) Token: 0x0600302C RID: 12332 RVA: 0x000B4DFA File Offset: 0x000B2FFA
		[XmlArray]
		[XmlArrayItem("PhoneNumberAttributedValue", typeof(PhoneNumberAttributedValue))]
		[DataMember(Name = "WorkFaxesArray", EmitDefaultValue = false, Order = 322)]
		public PhoneNumberAttributedValue[] WorkFaxes
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhoneNumberAttributedValue[]>(PersonaSchema.WorkFaxes);
			}
			set
			{
				base.PropertyBag[PersonaSchema.WorkFaxes] = value;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x000B4E0D File Offset: 0x000B300D
		// (set) Token: 0x0600302E RID: 12334 RVA: 0x000B4E1F File Offset: 0x000B301F
		[DataMember(Name = "Emails1Array", EmitDefaultValue = false, Order = 400)]
		[XmlArrayItem("EmailAddressAttributedValue", typeof(EmailAddressAttributedValue))]
		[XmlArray]
		public EmailAddressAttributedValue[] Emails1
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressAttributedValue[]>(PersonaSchema.Emails1);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Emails1] = value;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x000B4E32 File Offset: 0x000B3032
		// (set) Token: 0x06003030 RID: 12336 RVA: 0x000B4E44 File Offset: 0x000B3044
		[XmlArray]
		[DataMember(Name = "Emails2Array", EmitDefaultValue = false, Order = 401)]
		[XmlArrayItem("EmailAddressAttributedValue", typeof(EmailAddressAttributedValue))]
		public EmailAddressAttributedValue[] Emails2
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressAttributedValue[]>(PersonaSchema.Emails2);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Emails2] = value;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000B4E57 File Offset: 0x000B3057
		// (set) Token: 0x06003032 RID: 12338 RVA: 0x000B4E69 File Offset: 0x000B3069
		[XmlArrayItem("EmailAddressAttributedValue", typeof(EmailAddressAttributedValue))]
		[DataMember(Name = "Emails3Array", EmitDefaultValue = false, Order = 402)]
		[XmlArray]
		public EmailAddressAttributedValue[] Emails3
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EmailAddressAttributedValue[]>(PersonaSchema.Emails3);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Emails3] = value;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x000B4E7C File Offset: 0x000B307C
		// (set) Token: 0x06003034 RID: 12340 RVA: 0x000B4E8E File Offset: 0x000B308E
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "BusinessHomePagesArray", EmitDefaultValue = false, Order = 500)]
		[XmlArray]
		public StringAttributedValue[] BusinessHomePages
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.BusinessHomePages);
			}
			set
			{
				base.PropertyBag[PersonaSchema.BusinessHomePages] = value;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x000B4EA1 File Offset: 0x000B30A1
		// (set) Token: 0x06003036 RID: 12342 RVA: 0x000B4EB3 File Offset: 0x000B30B3
		[XmlArray]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "PersonalHomePagesArray", EmitDefaultValue = false, Order = 501)]
		public StringAttributedValue[] PersonalHomePages
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.PersonalHomePages);
			}
			set
			{
				base.PropertyBag[PersonaSchema.PersonalHomePages] = value;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000B4EC6 File Offset: 0x000B30C6
		// (set) Token: 0x06003038 RID: 12344 RVA: 0x000B4ED8 File Offset: 0x000B30D8
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "OfficeLocationsArray", EmitDefaultValue = false, Order = 502)]
		[XmlArray]
		public StringAttributedValue[] OfficeLocations
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.OfficeLocations);
			}
			set
			{
				base.PropertyBag[PersonaSchema.OfficeLocations] = value;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06003039 RID: 12345 RVA: 0x000B4EEB File Offset: 0x000B30EB
		// (set) Token: 0x0600303A RID: 12346 RVA: 0x000B4EFD File Offset: 0x000B30FD
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "ImAddressesArray", EmitDefaultValue = false, Order = 503)]
		[XmlArray]
		public StringAttributedValue[] ImAddresses
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.ImAddresses);
			}
			set
			{
				base.PropertyBag[PersonaSchema.ImAddresses] = value;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x000B4F10 File Offset: 0x000B3110
		// (set) Token: 0x0600303C RID: 12348 RVA: 0x000B4F22 File Offset: 0x000B3122
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "ImAddresses2Array", EmitDefaultValue = false, Order = 504)]
		[XmlArray]
		public StringAttributedValue[] ImAddresses2
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.ImAddresses2);
			}
			set
			{
				base.PropertyBag[PersonaSchema.ImAddresses2] = value;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x000B4F35 File Offset: 0x000B3135
		// (set) Token: 0x0600303E RID: 12350 RVA: 0x000B4F47 File Offset: 0x000B3147
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[XmlArray]
		[DataMember(Name = "ImAddresses3Array", EmitDefaultValue = false, Order = 505)]
		public StringAttributedValue[] ImAddresses3
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.ImAddresses3);
			}
			set
			{
				base.PropertyBag[PersonaSchema.ImAddresses3] = value;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x000B4F5A File Offset: 0x000B315A
		// (set) Token: 0x06003040 RID: 12352 RVA: 0x000B4F6C File Offset: 0x000B316C
		[XmlArray]
		[XmlArrayItem("PostalAddressAttributedValue", typeof(PostalAddressAttributedValue))]
		[DataMember(Name = "BusinessAddressesArray", EmitDefaultValue = false, Order = 600)]
		public PostalAddressAttributedValue[] BusinessAddresses
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PostalAddressAttributedValue[]>(PersonaSchema.BusinessAddresses);
			}
			set
			{
				base.PropertyBag[PersonaSchema.BusinessAddresses] = value;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x000B4F7F File Offset: 0x000B317F
		// (set) Token: 0x06003042 RID: 12354 RVA: 0x000B4F91 File Offset: 0x000B3191
		[XmlArray]
		[XmlArrayItem("PostalAddressAttributedValue", typeof(PostalAddressAttributedValue))]
		[DataMember(Name = "HomeAddressesArray", EmitDefaultValue = false, Order = 601)]
		public PostalAddressAttributedValue[] HomeAddresses
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PostalAddressAttributedValue[]>(PersonaSchema.HomeAddresses);
			}
			set
			{
				base.PropertyBag[PersonaSchema.HomeAddresses] = value;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x000B4FA4 File Offset: 0x000B31A4
		// (set) Token: 0x06003044 RID: 12356 RVA: 0x000B4FB6 File Offset: 0x000B31B6
		[DataMember(Name = "OtherAddressesArray", EmitDefaultValue = false, Order = 602)]
		[XmlArrayItem("PostalAddressAttributedValue", typeof(PostalAddressAttributedValue))]
		[XmlArray]
		public PostalAddressAttributedValue[] OtherAddresses
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PostalAddressAttributedValue[]>(PersonaSchema.OtherAddresses);
			}
			set
			{
				base.PropertyBag[PersonaSchema.OtherAddresses] = value;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003045 RID: 12357 RVA: 0x000B4FC9 File Offset: 0x000B31C9
		// (set) Token: 0x06003046 RID: 12358 RVA: 0x000B4FDB File Offset: 0x000B31DB
		[XmlArray]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "TitlesArray", EmitDefaultValue = false, Order = 700)]
		public StringAttributedValue[] Titles
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Titles);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Titles] = value;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003047 RID: 12359 RVA: 0x000B4FEE File Offset: 0x000B31EE
		// (set) Token: 0x06003048 RID: 12360 RVA: 0x000B5000 File Offset: 0x000B3200
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "DepartmentsArray", EmitDefaultValue = false, Order = 701)]
		[XmlArray]
		public StringAttributedValue[] Departments
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Departments);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Departments] = value;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06003049 RID: 12361 RVA: 0x000B5013 File Offset: 0x000B3213
		// (set) Token: 0x0600304A RID: 12362 RVA: 0x000B5025 File Offset: 0x000B3225
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "CompanyNamesArray", EmitDefaultValue = false, Order = 702)]
		[XmlArray]
		public StringAttributedValue[] CompanyNames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.CompanyNames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.CompanyNames] = value;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600304B RID: 12363 RVA: 0x000B5038 File Offset: 0x000B3238
		// (set) Token: 0x0600304C RID: 12364 RVA: 0x000B504A File Offset: 0x000B324A
		[DataMember(Name = "ManagersArray", EmitDefaultValue = false, Order = 703)]
		[XmlArray]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] Managers
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Managers);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Managers] = value;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x000B505D File Offset: 0x000B325D
		// (set) Token: 0x0600304E RID: 12366 RVA: 0x000B506F File Offset: 0x000B326F
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "AssistantNamesArray", EmitDefaultValue = false, Order = 704)]
		[XmlArray]
		public StringAttributedValue[] AssistantNames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.AssistantNames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.AssistantNames] = value;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x000B5082 File Offset: 0x000B3282
		// (set) Token: 0x06003050 RID: 12368 RVA: 0x000B5094 File Offset: 0x000B3294
		[XmlArray]
		[DataMember(Name = "ProfessionsArray", EmitDefaultValue = false, Order = 705)]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] Professions
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Professions);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Professions] = value;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x000B50A7 File Offset: 0x000B32A7
		// (set) Token: 0x06003052 RID: 12370 RVA: 0x000B50B9 File Offset: 0x000B32B9
		[XmlArray]
		[DataMember(Name = "SpouseNamesArray", EmitDefaultValue = false, Order = 706)]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] SpouseNames
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.SpouseNames);
			}
			set
			{
				base.PropertyBag[PersonaSchema.SpouseNames] = value;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x000B50CC File Offset: 0x000B32CC
		// (set) Token: 0x06003054 RID: 12372 RVA: 0x000B50DE File Offset: 0x000B32DE
		[DataMember(Name = "ChildrenArray", EmitDefaultValue = false, Order = 707)]
		[XmlArrayItem("StringArrayAttributedValue", typeof(StringArrayAttributedValue))]
		[XmlArray]
		public StringArrayAttributedValue[] Children
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringArrayAttributedValue[]>(PersonaSchema.Children);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Children] = value;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06003055 RID: 12373 RVA: 0x000B50F1 File Offset: 0x000B32F1
		// (set) Token: 0x06003056 RID: 12374 RVA: 0x000B5103 File Offset: 0x000B3303
		[XmlArray]
		[DataMember(Name = "HobbiesArray", EmitDefaultValue = false, Order = 708)]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] Hobbies
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Hobbies);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Hobbies] = value;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x000B5116 File Offset: 0x000B3316
		// (set) Token: 0x06003058 RID: 12376 RVA: 0x000B5128 File Offset: 0x000B3328
		[DataMember(Name = "WeddingAnniversariesArray", EmitDefaultValue = false, Order = 709)]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[XmlArray]
		public StringAttributedValue[] WeddingAnniversaries
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.WeddingAnniversaries);
			}
			set
			{
				base.PropertyBag[PersonaSchema.WeddingAnniversaries] = value;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x000B513B File Offset: 0x000B333B
		// (set) Token: 0x0600305A RID: 12378 RVA: 0x000B514D File Offset: 0x000B334D
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "BirthdaysArray", EmitDefaultValue = false, Order = 710)]
		[XmlArray]
		public StringAttributedValue[] Birthdays
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Birthdays);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Birthdays] = value;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x000B5160 File Offset: 0x000B3360
		// (set) Token: 0x0600305C RID: 12380 RVA: 0x000B5172 File Offset: 0x000B3372
		[DataMember(Name = "LocationsArray", EmitDefaultValue = false, Order = 711)]
		[XmlArray]
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		public StringAttributedValue[] Locations
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Locations);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Locations] = value;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x000B5185 File Offset: 0x000B3385
		// (set) Token: 0x0600305E RID: 12382 RVA: 0x000B5197 File Offset: 0x000B3397
		[XmlArrayItem("StringAttributedValue", typeof(StringAttributedValue))]
		[DataMember(Name = "SchoolsArray", EmitDefaultValue = false, Order = 712)]
		[XmlArray]
		public StringAttributedValue[] Schools
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.Schools);
			}
			set
			{
				base.PropertyBag[PersonaSchema.Schools] = value;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000B51AA File Offset: 0x000B33AA
		// (set) Token: 0x06003060 RID: 12384 RVA: 0x000B51BC File Offset: 0x000B33BC
		[DataMember(Name = "BirthdaysLocalArray", EmitDefaultValue = false, Order = 713)]
		[XmlIgnore]
		public StringAttributedValue[] BirthdaysLocal
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.BirthdaysLocal);
			}
			set
			{
				base.PropertyBag[PersonaSchema.BirthdaysLocal] = value;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x000B51CF File Offset: 0x000B33CF
		// (set) Token: 0x06003062 RID: 12386 RVA: 0x000B51E1 File Offset: 0x000B33E1
		[XmlIgnore]
		[DataMember(Name = "WeddingAnniversariesLocalArray", EmitDefaultValue = false, Order = 714)]
		public StringAttributedValue[] WeddingAnniversariesLocal
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<StringAttributedValue[]>(PersonaSchema.WeddingAnniversariesLocal);
			}
			set
			{
				base.PropertyBag[PersonaSchema.WeddingAnniversariesLocal] = value;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x000B51F4 File Offset: 0x000B33F4
		// (set) Token: 0x06003064 RID: 12388 RVA: 0x000B5206 File Offset: 0x000B3406
		[DataMember(Name = "ExtendedPropertiesArray", EmitDefaultValue = false, Order = 900)]
		[XmlArray]
		[XmlArrayItem("ExtendedPropertyAttributedValue", typeof(ExtendedPropertyAttributedValue))]
		public ExtendedPropertyAttributedValue[] ExtendedProperties
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ExtendedPropertyAttributedValue[]>(PersonaSchema.ExtendedProperties);
			}
			set
			{
				base.PropertyBag[PersonaSchema.ExtendedProperties] = value;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x000B5219 File Offset: 0x000B3419
		// (set) Token: 0x06003066 RID: 12390 RVA: 0x000B522B File Offset: 0x000B342B
		[DataMember(EmitDefaultValue = false, Order = 901)]
		[XmlIgnore]
		public Guid ADObjectId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<Guid>(PersonaSchema.ADObjectId);
			}
			set
			{
				base.PropertyBag[PersonaSchema.ADObjectId] = value;
			}
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000B5244 File Offset: 0x000B3444
		internal static Persona LoadFromPropertyBag(StoreSession storeSession, IDictionary<PropertyDefinition, object> xsoPropertyBag, ToServiceObjectForPropertyBagPropertyList propertyList)
		{
			PersonId storeId = (PersonId)xsoPropertyBag[PersonSchema.Id];
			IdAndSession idAndSession = new IdAndSession(storeId, storeSession);
			return Persona.LoadFromPropertyBag(idAndSession, xsoPropertyBag, propertyList);
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000B5274 File Offset: 0x000B3474
		internal static Persona LoadFromPropertyBag(StoreSession storeSession, StoreId parentFolderId, IDictionary<PropertyDefinition, object> xsoPropertyBag, ToServiceObjectForPropertyBagPropertyList propertyList)
		{
			PersonId storeId = (PersonId)xsoPropertyBag[PersonSchema.Id];
			IdAndSession idAndSession = new IdAndSession(storeId, parentFolderId, storeSession);
			return Persona.LoadFromPropertyBag(idAndSession, xsoPropertyBag, propertyList);
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000B52A4 File Offset: 0x000B34A4
		private static Persona LoadFromPropertyBag(IdAndSession idAndSession, IDictionary<PropertyDefinition, object> xsoPropertyBag, ToServiceObjectForPropertyBagPropertyList propertyList)
		{
			Persona.Tracer.TraceDebug((long)idAndSession.Id.GetHashCode(), "Persona.LoadFromPropertyBag: Entering");
			Persona persona = new Persona();
			propertyList.ConvertPropertiesToServiceObject(persona, xsoPropertyBag, idAndSession);
			HashSet<PropertyPath> properties = propertyList.GetProperties();
			persona.LoadCultureBasedData(properties, idAndSession.Session.PreferedCulture);
			Persona.Tracer.TraceDebug((long)idAndSession.Id.GetHashCode(), "Persona.LoadFromPropertyBag: Exiting");
			return persona;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000B5314 File Offset: 0x000B3514
		internal static Persona LoadFromPersonaId(StoreSession storeSession, IRecipientSession adRecipientSession, ItemId personaId, PersonaResponseShape personaShape, PropertyDefinition[] extendedPropertyDefinitions = null, StoreId folderId = null)
		{
			Persona.Tracer.TraceDebug<string>(Persona.TraceIdFromItemId(personaId), "Persona.LoadFromPersonaId: Entering, with PersonaId = {0}", (personaId == null) ? "(null)" : personaId.Id);
			Persona result;
			if (IdConverter.EwsIdIsActiveDirectoryObject(personaId.GetId()))
			{
				result = Persona.LoadFromADObjectId((MailboxSession)storeSession, adRecipientSession, IdConverter.EwsIdToADObjectId(personaId.GetId()), personaShape);
			}
			else
			{
				result = Persona.LoadFromPersonId(storeSession, IdConverter.EwsIdToPersonId(personaId.GetId()), personaShape, extendedPropertyDefinitions, folderId);
			}
			return result;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000B5388 File Offset: 0x000B3588
		internal static Persona LoadFromPersonaIdWithGalAggregation(MailboxSession mailboxSession, IRecipientSession adRecipientSession, ItemId personaId, PersonaResponseShape personaShape, PropertyDefinition[] extendedPropertyDefinitions)
		{
			Persona.Tracer.TraceDebug<string>(Persona.TraceIdFromItemId(personaId), "Persona.LoadFromPersonaId: Entering, with PersonaId = {0}", (personaId == null) ? "(null)" : personaId.Id);
			Persona result;
			if (IdConverter.EwsIdIsActiveDirectoryObject(personaId.GetId()))
			{
				ADObjectId adObjectId = IdConverter.EwsIdToADObjectId(personaId.GetId());
				result = Persona.LoadFromADObjectIdWithMailboxAggregation(mailboxSession, adRecipientSession, adObjectId, personaShape);
			}
			else
			{
				result = Persona.LoadFromPersonIdWithGalAggregation(mailboxSession, IdConverter.EwsIdToPersonId(personaId.GetId()), personaShape, extendedPropertyDefinitions);
			}
			return result;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000B53F8 File Offset: 0x000B35F8
		internal static Persona LoadFromEmailAddressWithGalAggregation(MailboxSession mailboxSession, IRecipientSession adRecipientSession, EmailAddressWrapper emailAddress, PersonaResponseShape personaShape)
		{
			Persona.Tracer.TraceDebug<string, string>((long)emailAddress.GetHashCode(), "Loading Person by Email - Address:{0}, MailboxType:{1}.", emailAddress.EmailAddress, emailAddress.MailboxType);
			Persona.Tracer.TraceDebug((long)emailAddress.GetHashCode(), "Lookup Email Address in AD.");
			Persona persona = Persona.FindPersonaInADByEmailAddress(mailboxSession, adRecipientSession, emailAddress.EmailAddress, personaShape);
			if (persona != null)
			{
				Persona.Tracer.TraceDebug<string>((long)emailAddress.GetHashCode(), "Found a match in GAL, return this to user - Persona Id:{0}.", persona.PersonaId.Id);
				return persona;
			}
			PersonId personId = Person.FindPersonIdByEmailAddress(mailboxSession, new XSOFactory(), emailAddress.EmailAddress);
			if (personId == null)
			{
				Persona.Tracer.TraceDebug((long)emailAddress.GetHashCode(), "No match found in personal contacts.");
				return null;
			}
			Persona.Tracer.TraceDebug<PersonId>((long)emailAddress.GetHashCode(), "Found a match in Personal Contacts with Person Id: {0}.", personId);
			return Persona.LoadFromPersonIdWithGalAggregation(mailboxSession, personId, personaShape, null);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000B5730 File Offset: 0x000B3930
		internal static IEnumerable<Persona> LoadFromLegacyDNs(MailboxSession mailboxSession, IRecipientSession adRecipientSession, string[] legacyDNs, PersonaResponseShape personaShape)
		{
			ToServiceObjectForPropertyBagPropertyList propertyList = Persona.GetPropertyListForPersonaResponseShape(personaShape);
			PropertyDefinition[] requestedProperties = propertyList.GetPropertyDefinitions();
			ADPersonToContactConverterSet converterSet = ADPersonToContactConverterSet.FromPersonProperties(requestedProperties, null);
			Result<ADRawEntry>[] adResults = adRecipientSession.FindByLegacyExchangeDNs(legacyDNs, converterSet.ADProperties);
			for (int i = 0; i < adResults.Length; i++)
			{
				Result<ADRawEntry> result = adResults[i];
				if (result.Error == null && result.Data != null)
				{
					yield return Persona.LoadFromADRawEntry(result.Data, mailboxSession, converterSet, propertyList);
				}
				else
				{
					Persona.Tracer.TraceDebug<string, ProviderError>((long)result.GetHashCode(), "Persona.LoadFromLegacyDNs: Could not find AD entry for legacyDN {0}. Error: {1}.", legacyDNs[i], result.Error);
					yield return new Persona
					{
						EmailAddress = new EmailAddressWrapper
						{
							EmailAddress = legacyDNs[i],
							RoutingType = "EX"
						}
					};
				}
			}
			yield break;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000B59F8 File Offset: 0x000B3BF8
		internal static IEnumerable<Persona> LoadFromProxyAddresses(MailboxSession mailboxSession, IRecipientSession adRecipientSession, ProxyAddress[] proxyAddresses, PersonaResponseShape personaShape)
		{
			if (proxyAddresses != null && proxyAddresses.Any<ProxyAddress>())
			{
				ToServiceObjectForPropertyBagPropertyList propertyList = Persona.GetPropertyListForPersonaResponseShape(personaShape);
				PropertyDefinition[] requestedProperties = propertyList.GetPropertyDefinitions();
				ADPersonToContactConverterSet converterSet = ADPersonToContactConverterSet.FromPersonProperties(requestedProperties, null);
				Result<ADRecipient>[] adResults = adRecipientSession.FindByProxyAddresses(proxyAddresses);
				for (int i = 0; i < adResults.Length; i++)
				{
					Result<ADRecipient> result = adResults[i];
					if (result.Error == null && result.Data != null)
					{
						yield return Persona.LoadFromADRawEntry(result.Data, mailboxSession, converterSet, propertyList);
					}
					else
					{
						Persona.Tracer.TraceDebug<ProxyAddress, ProviderError>((long)result.GetHashCode(), "Persona.LoadFromProxyAddresses: Could not find AD entry for proxy address {0}. Error: {1}.", proxyAddresses[i], result.Error);
						EmailAddressWrapper emailWrapper = new EmailAddressWrapper
						{
							EmailAddress = proxyAddresses[i].ProxyAddressString,
							RoutingType = "SMTP"
						};
						yield return new Persona
						{
							EmailAddress = emailWrapper
						};
					}
				}
			}
			yield break;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000B5A2C File Offset: 0x000B3C2C
		internal static Persona UpdatePersona(StoreSession storeSession, PersonId personId, ItemId personaId, ICollection<StoreObjectPropertyChange> propertyChanges, PersonType personType, StoreId folderId = null)
		{
			Persona.Tracer.TraceDebug<PersonId, PersonType>(PersonId.TraceId(personId), "Persona.UpdatePersona: Entering, with PersonId = {0} and personType = {1}", personId, personType);
			ToServiceObjectForPropertyBagPropertyList toServiceObjectForPropertyBagPropertyList = Persona.FullPersonaPropertyList;
			PersonaResponseShape personaShape = Persona.FullPersonaShapeWithFolderIds;
			if (Persona.IsBodiesRequested(propertyChanges))
			{
				toServiceObjectForPropertyBagPropertyList = Persona.FullPersonaPropertyListWithBodies;
				personaShape = Persona.FullPersonaShapeWithFolderIdsAndBodies;
			}
			Person person = Person.Load(storeSession, personId, toServiceObjectForPropertyBagPropertyList.GetPropertyDefinitions(), null, folderId);
			PersonId personId2 = person.UpdatePerson(storeSession, propertyChanges, personType == PersonType.DistributionList);
			ItemId personaId2 = IdConverter.PersonaIdFromPersonId(storeSession.MailboxGuid, personId2);
			Persona result = Persona.LoadFromPersonaId(storeSession, null, personaId2, personaShape, null, folderId);
			Persona.Tracer.TraceDebug(PersonId.TraceId(personId), "Persona.UpdatePersona: Exiting with personType = {0}");
			return result;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000B5AC8 File Offset: 0x000B3CC8
		internal static void DeletePersona(StoreSession storeSession, PersonId personId, ItemId personaId, StoreId deleteInFolder)
		{
			Persona.Tracer.TraceDebug<PersonId, StoreId>(PersonId.TraceId(personId), "Persona.DeletePersona: Entering, with PersonId = {0} and DeleteInFolder = {1}", personId, deleteInFolder);
			Person person = Person.Load(storeSession, personId, null, null, deleteInFolder);
			DeleteItemFlags deleteFlags = (storeSession is PublicFolderSession) ? DeleteItemFlags.SoftDelete : DeleteItemFlags.MoveToDeletedItems;
			if (person != null)
			{
				person.Delete(storeSession, deleteFlags, deleteInFolder);
			}
			Persona.Tracer.TraceDebug(PersonId.TraceId(personId), "Persona.DeletePersona: Exiting");
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000B5B28 File Offset: 0x000B3D28
		internal static Persona CreatePersona(StoreSession storeSession, ICollection<StoreObjectPropertyChange> propertyChanges, StoreId parentFolder, ItemId personaId, PersonType personType)
		{
			Persona.Tracer.TraceDebug<PersonType>(0L, "Persona.CreatePersona: Entering with personType = {0}", personType);
			PersonaResponseShape personaShape = Persona.FullPersonaShapeWithFolderIds;
			if (Persona.IsBodiesRequested(propertyChanges))
			{
				personaShape = Persona.FullPersonaShapeWithFolderIdsAndBodies;
			}
			PersonId personId = (personaId == null) ? PersonId.CreateNew() : IdConverter.EwsIdToPersonId(personaId.GetId());
			PersonId personId2 = Person.CreatePerson(storeSession, personId, propertyChanges, parentFolder, personType == PersonType.DistributionList);
			ItemId personaId2 = IdConverter.PersonaIdFromPersonId(storeSession.MailboxGuid, personId2);
			Persona persona = Persona.LoadFromPersonaId(storeSession, null, personaId2, personaShape, null, parentFolder);
			MailboxSession mailboxSession = storeSession as MailboxSession;
			if (mailboxSession != null && persona.EmailAddresses != null && persona.EmailAddresses.Length > 0)
			{
				MdbMaskedPeopleModelDataBinder mdbMaskedPeopleModelDataBinder = MdbMaskedPeopleModelDataBinderFactory.Current.CreateInstance(mailboxSession);
				MaskedPeopleModelItem modelData = mdbMaskedPeopleModelDataBinder.GetModelData();
				if (modelData != null)
				{
					bool flag = false;
					foreach (EmailAddressWrapper emailAddressWrapper in persona.EmailAddresses)
					{
						string emailAddress = emailAddressWrapper.EmailAddress;
						if (!string.IsNullOrEmpty(emailAddress))
						{
							for (int j = 0; j < modelData.ContactList.Count; j++)
							{
								if (emailAddress.Equals(modelData.ContactList[j].EmailAddress, StringComparison.OrdinalIgnoreCase))
								{
									Persona.Tracer.TraceDebug<string>(0L, "Persona.CreatePersona: Unmasking {0}.", emailAddress);
									modelData.ContactList.RemoveAt(j);
									flag = true;
									break;
								}
							}
						}
						else
						{
							Persona.Tracer.TraceDebug(0L, "Persona.CreatePersona: Persona's email address is null or empty.");
						}
					}
					if (flag)
					{
						Persona.Tracer.TraceDebug(0L, "Persona.CreatePersona: Saving changes to masked model.");
						MdbModelUtils.WriteModelItem<MaskedPeopleModelItem, MdbMaskedPeopleModelDataBinder>(mdbMaskedPeopleModelDataBinder, modelData);
					}
				}
				else
				{
					Persona.Tracer.TraceDebug(0L, "Persona.CreatePersona: Model could not be retrieved from the Masked contact FAI.");
				}
			}
			else
			{
				Persona.Tracer.TraceDebug(0L, "Persona.CreatePersona: Persona was created with no email addresses.");
			}
			Persona.Tracer.TraceDebug<PersonType>(0L, "Persona.CreatePersona: Exiting with personType = {0}", personType);
			return persona;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000B5CF0 File Offset: 0x000B3EF0
		private static EmailAddressWrapper EWSEmailAddressFromXsoEmailAddress(EmailAddress emailAddress)
		{
			return new EmailAddressWrapper
			{
				Name = emailAddress.Name,
				EmailAddress = emailAddress.Address,
				RoutingType = emailAddress.RoutingType,
				OriginalDisplayName = emailAddress.OriginalDisplayName
			};
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000B5D34 File Offset: 0x000B3F34
		internal static ToServiceObjectForPropertyBagPropertyList GetPropertyListForPersonaResponseShape(PersonaResponseShape personaResponseShape)
		{
			if (personaResponseShape == null)
			{
				personaResponseShape = Persona.DefaultPersonaShape;
			}
			ToServiceObjectForPropertyBagPropertyList toServiceObjectForPropertyBagPropertyList = PropertyList.CreateToServiceObjectForPropertyBagPropertyList(personaResponseShape, Schema.Persona);
			if (personaResponseShape.BaseShape == ShapeEnum.AllProperties)
			{
				foreach (PropertyInformation propertyInformation in PersonaSchema.AllPropertiesExclusionList)
				{
					bool flag = false;
					if (personaResponseShape.AdditionalProperties != null)
					{
						foreach (PropertyPath propertyPath in personaResponseShape.AdditionalProperties)
						{
							if (propertyPath.Equals(propertyInformation.PropertyPath))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						toServiceObjectForPropertyBagPropertyList.Remove(propertyInformation.PropertyPath);
					}
				}
			}
			return toServiceObjectForPropertyBagPropertyList;
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000B5DEC File Offset: 0x000B3FEC
		internal static Persona LoadFromPersonIdWithGalAggregation(MailboxSession mailboxSession, PersonId personId, PersonaResponseShape personaShape, PropertyDefinition[] extendedPropertyDefinitions)
		{
			Persona.Tracer.TraceDebug<PersonId>(PersonId.TraceId(personId), "Persona.LoadFromPersonIdWithGalAggregation: Entering, with PersonId = {0}", personId);
			ToServiceObjectForPropertyBagPropertyList propertyListForPersonaResponseShape = Persona.GetPropertyListForPersonaResponseShape(personaShape);
			PropertyDefinition[] propertyDefinitions = propertyListForPersonaResponseShape.GetPropertyDefinitions();
			Person person = Person.LoadWithGALAggregation(mailboxSession, personId, propertyDefinitions, extendedPropertyDefinitions);
			Persona result = null;
			if (person != null)
			{
				Dictionary<PropertyDefinition, object> properties = Persona.GetProperties(person.PropertyBag, propertyListForPersonaResponseShape.GetPropertyDefinitions());
				result = Persona.LoadFromPropertyBag(mailboxSession, properties, propertyListForPersonaResponseShape);
			}
			Persona.Tracer.TraceDebug(PersonId.TraceId(personId), "Persona.LoadFromPersonIdWithGalAggregation: Exiting");
			return result;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000B5E60 File Offset: 0x000B4060
		private static Persona LoadFromPersonId(StoreSession storeSession, PersonId personId, PersonaResponseShape personaShape, PropertyDefinition[] extendedPropertyDefinitions, StoreId folderId = null)
		{
			Persona.Tracer.TraceDebug<PersonId>(PersonId.TraceId(personId), "Persona.LoadFromPersonId: Entering, with PersonId = {0}", personId);
			ToServiceObjectForPropertyBagPropertyList propertyListForPersonaResponseShape = Persona.GetPropertyListForPersonaResponseShape(personaShape);
			PropertyDefinition[] propertyDefinitions = propertyListForPersonaResponseShape.GetPropertyDefinitions();
			Person person = Person.Load(storeSession, personId, propertyDefinitions, extendedPropertyDefinitions, folderId);
			Persona result = null;
			if (person != null)
			{
				Dictionary<PropertyDefinition, object> properties = Persona.GetProperties(person.PropertyBag, propertyListForPersonaResponseShape.GetPropertyDefinitions());
				if (folderId != null)
				{
					result = Persona.LoadFromPropertyBag(storeSession, folderId, properties, propertyListForPersonaResponseShape);
				}
				else
				{
					result = Persona.LoadFromPropertyBag(storeSession, properties, propertyListForPersonaResponseShape);
				}
			}
			Persona.Tracer.TraceDebug(PersonId.TraceId(personId), "Persona.LoadFromPersonId: Exiting");
			return result;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000B5F04 File Offset: 0x000B4104
		private static Persona LoadFromADObjectIdWithMailboxAggregation(MailboxSession mailboxSession, IRecipientSession adRecipientSession, ADObjectId adObjectId, PersonaResponseShape personaShape)
		{
			Persona.Tracer.TraceDebug<ADObjectId>((long)adObjectId.GetHashCode(), "Persona.LoadFromADObjectIdWithMailboxAggregation: ADObjectId = {0}", adObjectId);
			return Persona.LoadFromADPersonWithMailboxAggregation(mailboxSession, (ADPropertyDefinition[] adProperties) => adRecipientSession.ReadADRawEntry(adObjectId, adProperties), personaShape);
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000B5F5C File Offset: 0x000B415C
		private static Persona LoadFromADPersonWithMailboxAggregation(MailboxSession mailboxSession, Func<ADPropertyDefinition[], ADRawEntry> adPersonLoader, PersonaResponseShape personaShape)
		{
			ToServiceObjectForPropertyBagPropertyList propertyListForPersonaResponseShape = Persona.GetPropertyListForPersonaResponseShape(personaShape);
			PropertyDefinition[] propertyDefinitions = propertyListForPersonaResponseShape.GetPropertyDefinitions();
			ADPersonToContactConverterSet adpersonToContactConverterSet = ADPersonToContactConverterSet.FromPersonProperties(propertyDefinitions, null);
			ADRawEntry adrawEntry = adPersonLoader(adpersonToContactConverterSet.ADProperties);
			if (adrawEntry == null)
			{
				Persona.Tracer.TraceDebug(0L, "No AD Person found, there is nothing else to do.");
				return null;
			}
			Persona.Tracer.TraceDebug<Guid>((long)adrawEntry.Id.GetHashCode(), "Found AD Person with Id: {0}.", adrawEntry.Id.ObjectGuid);
			Person person = Person.FindPersonLinkedToADEntry(mailboxSession, adrawEntry, propertyDefinitions);
			if (person == null)
			{
				Persona.Tracer.TraceDebug((long)adrawEntry.Id.GetHashCode(), "No Person linked to AD found, just load Persona with AD Data alone.");
				return Persona.LoadFromADRawEntry(adrawEntry, mailboxSession, adpersonToContactConverterSet, propertyListForPersonaResponseShape);
			}
			Dictionary<PropertyDefinition, object> properties = Persona.GetProperties(person.PropertyBag, propertyDefinitions);
			return Persona.LoadFromPropertyBag(mailboxSession, properties, propertyListForPersonaResponseShape);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000B6014 File Offset: 0x000B4214
		private static Persona LoadFromADObjectId(MailboxSession mailboxSession, IRecipientSession adRecipientSession, ADObjectId adObjectId, PersonaResponseShape personaShape)
		{
			Persona.Tracer.TraceDebug<ADObjectId>((long)adObjectId.GetHashCode(), "Persona.LoadFromADObjectId: Entering, with ADObjectId = {0}", adObjectId);
			ToServiceObjectForPropertyBagPropertyList propertyListForPersonaResponseShape = Persona.GetPropertyListForPersonaResponseShape(personaShape);
			PropertyDefinition[] propertyDefinitions = propertyListForPersonaResponseShape.GetPropertyDefinitions();
			ADPersonToContactConverterSet adpersonToContactConverterSet = ADPersonToContactConverterSet.FromPersonProperties(propertyDefinitions, null);
			ADRawEntry adrawEntry = adRecipientSession.ReadADRawEntry(adObjectId, adpersonToContactConverterSet.ADProperties);
			Persona result = null;
			if (adrawEntry != null)
			{
				result = Persona.LoadFromADRawEntry(adrawEntry, mailboxSession, adpersonToContactConverterSet, propertyListForPersonaResponseShape);
			}
			Persona.Tracer.TraceDebug((long)adObjectId.GetHashCode(), "Persona.LoadFromADObjectId: Exiting");
			return result;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000B60A4 File Offset: 0x000B42A4
		private static Persona FindPersonaInADByEmailAddress(MailboxSession mailboxSession, IRecipientSession adRecipientSession, string emailAddress, PersonaResponseShape personaShape)
		{
			Persona.Tracer.TraceDebug<string>((long)emailAddress.GetHashCode(), "Persona.LoadFromADWithEmailAddress: Entering, with emailAddress = {0}", emailAddress);
			ProxyAddress proxyAddress = null;
			if (!ProxyAddress.TryParse(emailAddress, out proxyAddress))
			{
				Persona.Tracer.TraceDebug((long)emailAddress.GetHashCode(), "Persona.LoadFromADWithEmailAddress: Invalid proxy address, skip looking into AD.");
				return null;
			}
			return Persona.LoadFromADPersonWithMailboxAggregation(mailboxSession, (ADPropertyDefinition[] adProperties) => adRecipientSession.FindByProxyAddress(proxyAddress, adProperties), personaShape);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000B6115 File Offset: 0x000B4315
		private static long TraceIdFromItemId(ItemId itemId)
		{
			return (long)((itemId == null || itemId.Id == null) ? 0 : itemId.Id.GetHashCode());
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000B6134 File Offset: 0x000B4334
		internal static Dictionary<PropertyDefinition, object> GetProperties(IStorePropertyBag propertyBag, PropertyDefinition[] properties)
		{
			object[] properties2 = propertyBag.GetProperties(properties);
			Dictionary<PropertyDefinition, object> dictionary = new Dictionary<PropertyDefinition, object>(properties2.Length);
			for (int i = 0; i < properties.Length; i++)
			{
				dictionary[properties[i]] = properties2[i];
			}
			return dictionary;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000B616D File Offset: 0x000B436D
		private static void LoadCustomProperty(PropertyInformation property, HashSet<PropertyPath> properties, Action propertyLoader)
		{
			if (!properties.Contains(property.PropertyPath))
			{
				return;
			}
			if (!PersonaSchema.EwsCalculatedProperties.Contains(property.PropertyPath))
			{
				throw new InvalidOperationException(string.Format("Please aggregate the property {0} in XSO. Do not add new properties to EwsCalculatedXmlElements.", property.LocalName));
			}
			propertyLoader();
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000B61AC File Offset: 0x000B43AC
		private static bool IsBodiesRequested(ICollection<StoreObjectPropertyChange> propertyChanges)
		{
			foreach (StoreObjectPropertyChange storeObjectPropertyChange in propertyChanges)
			{
				if (storeObjectPropertyChange.PropertyDefinition.Name == PersonSchema.Bodies.Name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000B6318 File Offset: 0x000B4518
		private void LoadCultureBasedData(HashSet<PropertyPath> properties, CultureInfo cultureInfo)
		{
			Persona.Tracer.TraceDebug(0L, "Persona.LoadCultureBasedData:: Loading Sort Keys ...");
			Persona.LoadCustomProperty(PersonaSchema.DisplayNameFirstLastSortKey, properties, delegate
			{
				this.DisplayNameFirstLastSortKey = PeopleStringUtils.ComputeSortKey(cultureInfo, this.DisplayNameFirstLast);
			});
			Persona.LoadCustomProperty(PersonaSchema.DisplayNameLastFirstSortKey, properties, delegate
			{
				this.DisplayNameLastFirstSortKey = PeopleStringUtils.ComputeSortKey(cultureInfo, this.DisplayNameLastFirst);
			});
			Persona.LoadCustomProperty(PersonaSchema.CompanyNameSortKey, properties, delegate
			{
				this.CompanyNameSortKey = PeopleStringUtils.ComputeSortKey(cultureInfo, this.CompanyName);
			});
			Persona.LoadCustomProperty(PersonaSchema.HomeCitySortKey, properties, delegate
			{
				this.HomeCitySortKey = PeopleStringUtils.ComputeSortKey(cultureInfo, this.HomeCity);
			});
			Persona.LoadCustomProperty(PersonaSchema.WorkCitySortKey, properties, delegate
			{
				this.WorkCitySortKey = PeopleStringUtils.ComputeSortKey(cultureInfo, this.WorkCity);
			});
			Persona.Tracer.TraceDebug(0L, "Persona.LoadCultureBasedData:: Loading Headers ...");
			Persona.LoadCustomProperty(PersonaSchema.DisplayNameFirstLastHeader, properties, delegate
			{
				this.DisplayNameFirstLastHeader = PeopleHeaderProvider.Instance.GetHeader(this.DisplayNameFirstLast, cultureInfo);
			});
			Persona.LoadCustomProperty(PersonaSchema.DisplayNameLastFirstHeader, properties, delegate
			{
				this.DisplayNameLastFirstHeader = PeopleHeaderProvider.Instance.GetHeader(this.DisplayNameLastFirst, cultureInfo);
			});
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600307F RID: 12415 RVA: 0x000B63FC File Offset: 0x000B45FC
		// (set) Token: 0x06003080 RID: 12416 RVA: 0x000B6404 File Offset: 0x000B4604
		internal float SpeechConfidence { get; set; }

		// Token: 0x06003081 RID: 12417 RVA: 0x000B6410 File Offset: 0x000B4610
		internal static Persona LoadFromADRawEntry(ADRawEntry adRawEntry, MailboxSession mailboxSession, ADPersonToContactConverterSet converterSet, ToServiceObjectForPropertyBagPropertyList propertyList)
		{
			IStorePropertyBag contactPropertyBag = converterSet.Convert(adRawEntry);
			return Persona.LoadFromADContact(contactPropertyBag, mailboxSession, converterSet, propertyList);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000B6430 File Offset: 0x000B4630
		internal static Persona LoadFromADContact(IStorePropertyBag contactPropertyBag, MailboxSession mailboxSession, ADPersonToContactConverterSet converterSet, ToServiceObjectForPropertyBagPropertyList propertyList)
		{
			PersonId value = PersonId.CreateNew();
			contactPropertyBag[ContactSchema.PersonId] = value;
			PersonPropertyAggregationContext context = new PersonPropertyAggregationContext(new IStorePropertyBag[]
			{
				contactPropertyBag
			}, mailboxSession.ContactFolders, mailboxSession.ClientInfoString);
			IStorePropertyBag propertyBag = ApplicationAggregatedProperty.Aggregate(context, converterSet.PersonProperties);
			IDictionary<PropertyDefinition, object> properties = Persona.GetProperties(propertyBag, propertyList.GetPropertyDefinitions());
			Persona persona = Persona.LoadFromPropertyBag(mailboxSession, properties, propertyList);
			persona.ADObjectId = (Guid)contactPropertyBag[ContactSchema.GALLinkID];
			persona.PersonaId = IdConverter.PersonaIdFromADObjectId(persona.ADObjectId);
			return persona;
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x000B64C0 File Offset: 0x000B46C0
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				throw new InvalidOperationException("Persona objects don't have a store object type. This method should not be called.");
			}
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000B64CC File Offset: 0x000B46CC
		internal override void AddExtendedPropertyValue(ExtendedPropertyType extendedProperty)
		{
			throw new InvalidOperationException("Persona objects don't have extended properties. This method should not be called.");
		}

		// Token: 0x04001BE2 RID: 7138
		private static readonly Trace Tracer = ExTraceGlobals.GetPersonaCallTracer;

		// Token: 0x04001BE3 RID: 7139
		private static readonly string MailboxTypeMailbox = MailboxHelper.MailboxTypeType.Mailbox.ToString();

		// Token: 0x04001BE4 RID: 7140
		internal static readonly PersonaResponseShape IdOnlyPersonaShape = new PersonaResponseShape(ShapeEnum.IdOnly);

		// Token: 0x04001BE5 RID: 7141
		internal static readonly PersonaResponseShape DefaultPersonaShape = new PersonaResponseShape(ShapeEnum.Default);

		// Token: 0x04001BE6 RID: 7142
		internal static readonly PersonaResponseShape FullPersonaShape = new PersonaResponseShape(ShapeEnum.AllProperties);

		// Token: 0x04001BE7 RID: 7143
		internal static readonly PersonaResponseShape FullPersonaShapeWithFolderIds = new PersonaResponseShape(ShapeEnum.AllProperties, new PropertyPath[]
		{
			PersonaSchema.FolderIds.PropertyPath
		});

		// Token: 0x04001BE8 RID: 7144
		internal static readonly PersonaResponseShape FullPersonaShapeWithBodies = new PersonaResponseShape(ShapeEnum.AllProperties, new PropertyPath[]
		{
			PersonaSchema.Bodies.PropertyPath
		});

		// Token: 0x04001BE9 RID: 7145
		internal static readonly PersonaResponseShape FullPersonaShapeWithFolderIdsAndBodies = new PersonaResponseShape(ShapeEnum.AllProperties, new PropertyPath[]
		{
			PersonaSchema.FolderIds.PropertyPath,
			PersonaSchema.Bodies.PropertyPath
		});

		// Token: 0x04001BEA RID: 7146
		internal static readonly PersonaResponseShape IdAndEmailPersonaShape = new PersonaResponseShape(ShapeEnum.IdOnly, new PropertyPath[]
		{
			PersonaSchema.EmailAddress.PropertyPath
		});

		// Token: 0x04001BEB RID: 7147
		internal static readonly ToServiceObjectForPropertyBagPropertyList DefaultPersonaPropertyList = Persona.GetPropertyListForPersonaResponseShape(Persona.DefaultPersonaShape);

		// Token: 0x04001BEC RID: 7148
		internal static readonly ToServiceObjectForPropertyBagPropertyList FullPersonaPropertyList = Persona.GetPropertyListForPersonaResponseShape(Persona.FullPersonaShape);

		// Token: 0x04001BED RID: 7149
		internal static readonly ToServiceObjectForPropertyBagPropertyList FullPersonaPropertyListWithBodies = Persona.GetPropertyListForPersonaResponseShape(Persona.FullPersonaShapeWithBodies);

		// Token: 0x04001BEE RID: 7150
		internal static PropertyDefinition[] DefaultPersonaProperties = Persona.DefaultPersonaPropertyList.GetPropertyDefinitions();
	}
}
