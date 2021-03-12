using System;
using System.Collections;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000185 RID: 389
	[Serializable]
	internal class EnhancedLocationData : INestedData
	{
		// Token: 0x060010BC RID: 4284 RVA: 0x0005D628 File Offset: 0x0005B828
		public EnhancedLocationData(int protocolVersion)
		{
			this.subProperties = new Hashtable();
			this.ProtocolVersion = protocolVersion;
			this.Clear();
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0005D648 File Offset: 0x0005B848
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x0005D650 File Offset: 0x0005B850
		public int ProtocolVersion { get; private set; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0005D659 File Offset: 0x0005B859
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x0005D670 File Offset: 0x0005B870
		public string DisplayName
		{
			get
			{
				return (string)this.subProperties["DisplayName"];
			}
			set
			{
				this.subProperties["DisplayName"] = value;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0005D683 File Offset: 0x0005B883
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x0005D69A File Offset: 0x0005B89A
		public string Annotation
		{
			get
			{
				return (string)this.subProperties["Annotation"];
			}
			set
			{
				this.subProperties["Annotation"] = value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0005D6AD File Offset: 0x0005B8AD
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x0005D6C4 File Offset: 0x0005B8C4
		public string Street
		{
			get
			{
				return (string)this.subProperties["Street"];
			}
			set
			{
				this.subProperties["Street"] = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0005D6D7 File Offset: 0x0005B8D7
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x0005D6EE File Offset: 0x0005B8EE
		public string City
		{
			get
			{
				return (string)this.subProperties["City"];
			}
			set
			{
				this.subProperties["City"] = value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0005D701 File Offset: 0x0005B901
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x0005D718 File Offset: 0x0005B918
		public string State
		{
			get
			{
				return (string)this.subProperties["State"];
			}
			set
			{
				this.subProperties["State"] = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0005D72B File Offset: 0x0005B92B
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x0005D742 File Offset: 0x0005B942
		public string Country
		{
			get
			{
				return (string)this.subProperties["Country"];
			}
			set
			{
				this.subProperties["Country"] = value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0005D755 File Offset: 0x0005B955
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x0005D76C File Offset: 0x0005B96C
		public string PostalCode
		{
			get
			{
				return (string)this.subProperties["PostalCode"];
			}
			set
			{
				this.subProperties["PostalCode"] = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0005D77F File Offset: 0x0005B97F
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x0005D796 File Offset: 0x0005B996
		public string Latitude
		{
			get
			{
				return (string)this.subProperties["Latitude"];
			}
			set
			{
				this.subProperties["Latitude"] = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0005D7A9 File Offset: 0x0005B9A9
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0005D7C0 File Offset: 0x0005B9C0
		public string Longitude
		{
			get
			{
				return (string)this.subProperties["Longitude"];
			}
			set
			{
				this.subProperties["Longitude"] = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0005D7D3 File Offset: 0x0005B9D3
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x0005D7EA File Offset: 0x0005B9EA
		public string Accuracy
		{
			get
			{
				return (string)this.subProperties["Accuracy"];
			}
			set
			{
				this.subProperties["Accuracy"] = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0005D7FD File Offset: 0x0005B9FD
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x0005D814 File Offset: 0x0005BA14
		public string Altitude
		{
			get
			{
				return (string)this.subProperties["Altitude"];
			}
			set
			{
				this.subProperties["Altitude"] = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0005D827 File Offset: 0x0005BA27
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x0005D83E File Offset: 0x0005BA3E
		public string AltitudeAccuracy
		{
			get
			{
				return (string)this.subProperties["AltitudeAccuracy"];
			}
			set
			{
				this.subProperties["AltitudeAccuracy"] = value;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0005D851 File Offset: 0x0005BA51
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x0005D868 File Offset: 0x0005BA68
		public string LocationUri
		{
			get
			{
				return (string)this.subProperties["LocationUri"];
			}
			set
			{
				this.subProperties["LocationUri"] = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0005D87B File Offset: 0x0005BA7B
		public string[] Keys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0005D883 File Offset: 0x0005BA83
		public IDictionary SubProperties
		{
			get
			{
				return this.subProperties;
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0005D88B File Offset: 0x0005BA8B
		public void Clear()
		{
			this.subProperties.Clear();
			this.keys = EnhancedLocationData.postalAddressKeys;
		}

		// Token: 0x04000AE3 RID: 2787
		private static readonly string[] postalAddressKeys = new string[]
		{
			"DisplayName",
			"Annotation",
			"Street",
			"City",
			"State",
			"Country",
			"PostalCode",
			"Latitude",
			"Longitude",
			"Accuracy",
			"Altitude",
			"AltitudeAccuracy",
			"LocationUri"
		};

		// Token: 0x04000AE4 RID: 2788
		private string[] keys;

		// Token: 0x04000AE5 RID: 2789
		private IDictionary subProperties;
	}
}
