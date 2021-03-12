using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000C3 RID: 195
	[XmlType("Region")]
	[KnownType(typeof(Region))]
	[DataContract(Name = "Region")]
	public class Region
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000BCF9 File Offset: 0x00009EF9
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x0000BD01 File Offset: 0x00009F01
		[XmlAttribute("Name")]
		[DataMember(Name = "Name", EmitDefaultValue = true)]
		public string Name { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000BD0A File Offset: 0x00009F0A
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0000BD17 File Offset: 0x00009F17
		[XmlElement("AccessNumber")]
		[DataMember(Name = "AccessNumber", EmitDefaultValue = false)]
		public AccessNumber[] AccessNumbers
		{
			get
			{
				return this.accessNumbersList.ToArray();
			}
			set
			{
				this.accessNumbersList.Clear();
				this.accessNumbersList.AddRange(value);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000BD30 File Offset: 0x00009F30
		internal List<AccessNumber> AccessNumbersList
		{
			get
			{
				return this.accessNumbersList;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000BD38 File Offset: 0x00009F38
		internal static Region[] ConvertFrom(DialInInformation dialIn, CultureInfo userCulture)
		{
			Dictionary<string, Region> dictionary = new Dictionary<string, Region>();
			int num = (userCulture != null) ? userCulture.LCID : 0;
			bool flag = false;
			foreach (DialInRegion dialInRegion in dialIn.DialInRegions)
			{
				List<AccessNumber> list = new List<AccessNumber>();
				foreach (string name in dialInRegion.Languages)
				{
					if (!string.IsNullOrWhiteSpace(dialInRegion.Number))
					{
						AccessNumber accessNumber = new AccessNumber();
						accessNumber.Number = dialInRegion.Number;
						try
						{
							accessNumber.LanguageID = new CultureInfo(name).LCID;
						}
						catch (CultureNotFoundException)
						{
						}
						if (accessNumber.LanguageID == num)
						{
							if (!flag)
							{
								list.Clear();
								flag = true;
							}
						}
						else if (flag)
						{
							continue;
						}
						list.Add(accessNumber);
					}
				}
				if (list.Any<AccessNumber>())
				{
					if (!dictionary.ContainsKey(dialInRegion.Name))
					{
						Region region = new Region();
						region.Name = dialInRegion.Name;
						dictionary.Add(dialInRegion.Name, region);
					}
					dictionary[dialInRegion.Name].AccessNumbersList.AddRange(list);
				}
			}
			return dictionary.Values.ToArray<Region>();
		}

		// Token: 0x0400031B RID: 795
		private readonly List<AccessNumber> accessNumbersList = new List<AccessNumber>();
	}
}
