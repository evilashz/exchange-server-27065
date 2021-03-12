using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A56 RID: 2646
	[DataContract]
	internal class LocationServicesResponse : IBingResultSet
	{
		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06004B15 RID: 19221 RVA: 0x0010545D File Offset: 0x0010365D
		// (set) Token: 0x06004B16 RID: 19222 RVA: 0x00105465 File Offset: 0x00103665
		[DataMember]
		public LocationServicesResponse.ResourceSet[] resourceSets { get; set; }

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06004B17 RID: 19223 RVA: 0x0010546E File Offset: 0x0010366E
		public IBingResult[] Results
		{
			get
			{
				if (this.resourceSets != null && this.resourceSets.Length > 0 && this.resourceSets[0] != null)
				{
					return this.resourceSets[0].resources;
				}
				return null;
			}
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x0010549C File Offset: 0x0010369C
		public IBingError[] Errors
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002AB5 RID: 10933
		private const string AddressEntityType = "Address";

		// Token: 0x02000A57 RID: 2647
		[DataContract]
		public class ResourceSet
		{
			// Token: 0x1700111B RID: 4379
			// (get) Token: 0x06004B1A RID: 19226 RVA: 0x001054A7 File Offset: 0x001036A7
			// (set) Token: 0x06004B1B RID: 19227 RVA: 0x001054AF File Offset: 0x001036AF
			[DataMember]
			public LocationServicesResponse.ResourceSet.Resource[] resources { get; set; }

			// Token: 0x02000A58 RID: 2648
			[DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1", Name = "Location")]
			public class Resource : IBingResult
			{
				// Token: 0x1700111C RID: 4380
				// (get) Token: 0x06004B1D RID: 19229 RVA: 0x001054C0 File Offset: 0x001036C0
				// (set) Token: 0x06004B1E RID: 19230 RVA: 0x001054C8 File Offset: 0x001036C8
				[DataMember(Name = "name")]
				public string OriginalName { get; set; }

				// Token: 0x1700111D RID: 4381
				// (get) Token: 0x06004B1F RID: 19231 RVA: 0x001054D1 File Offset: 0x001036D1
				// (set) Token: 0x06004B20 RID: 19232 RVA: 0x001054D9 File Offset: 0x001036D9
				[DataMember]
				public LocationServicesResponse.ResourceSet.Resource.Point point { get; set; }

				// Token: 0x1700111E RID: 4382
				// (get) Token: 0x06004B21 RID: 19233 RVA: 0x001054E2 File Offset: 0x001036E2
				public double Latitude
				{
					get
					{
						return double.Parse(this.point.coordinates[0]);
					}
				}

				// Token: 0x1700111F RID: 4383
				// (get) Token: 0x06004B22 RID: 19234 RVA: 0x001054F6 File Offset: 0x001036F6
				public double Longitude
				{
					get
					{
						return double.Parse(this.point.coordinates[1]);
					}
				}

				// Token: 0x17001120 RID: 4384
				// (get) Token: 0x06004B23 RID: 19235 RVA: 0x0010550A File Offset: 0x0010370A
				// (set) Token: 0x06004B24 RID: 19236 RVA: 0x00105512 File Offset: 0x00103712
				[DataMember]
				public LocationServicesResponse.ResourceSet.Resource.Address address { get; set; }

				// Token: 0x17001121 RID: 4385
				// (get) Token: 0x06004B25 RID: 19237 RVA: 0x0010551B File Offset: 0x0010371B
				public string Name
				{
					get
					{
						if ("Address".Equals(this.entityType, StringComparison.OrdinalIgnoreCase))
						{
							return this.address.addressLine;
						}
						return this.OriginalName;
					}
				}

				// Token: 0x17001122 RID: 4386
				// (get) Token: 0x06004B26 RID: 19238 RVA: 0x00105542 File Offset: 0x00103742
				public string StreetAddress
				{
					get
					{
						return this.address.addressLine;
					}
				}

				// Token: 0x17001123 RID: 4387
				// (get) Token: 0x06004B27 RID: 19239 RVA: 0x0010554F File Offset: 0x0010374F
				public string City
				{
					get
					{
						return this.address.locality;
					}
				}

				// Token: 0x17001124 RID: 4388
				// (get) Token: 0x06004B28 RID: 19240 RVA: 0x0010555C File Offset: 0x0010375C
				public string State
				{
					get
					{
						return this.address.adminDistrict;
					}
				}

				// Token: 0x17001125 RID: 4389
				// (get) Token: 0x06004B29 RID: 19241 RVA: 0x00105569 File Offset: 0x00103769
				public string Country
				{
					get
					{
						return this.address.countryRegion;
					}
				}

				// Token: 0x17001126 RID: 4390
				// (get) Token: 0x06004B2A RID: 19242 RVA: 0x00105576 File Offset: 0x00103776
				public string PostalCode
				{
					get
					{
						return this.address.postalCode;
					}
				}

				// Token: 0x17001127 RID: 4391
				// (get) Token: 0x06004B2B RID: 19243 RVA: 0x00105583 File Offset: 0x00103783
				public LocationSource LocationSource
				{
					get
					{
						return LocationSource.LocationServices;
					}
				}

				// Token: 0x17001128 RID: 4392
				// (get) Token: 0x06004B2C RID: 19244 RVA: 0x00105586 File Offset: 0x00103786
				public string LocationUri
				{
					get
					{
						return this.address.formattedAddress;
					}
				}

				// Token: 0x17001129 RID: 4393
				// (get) Token: 0x06004B2D RID: 19245 RVA: 0x00105593 File Offset: 0x00103793
				public string PhoneNumber
				{
					get
					{
						return null;
					}
				}

				// Token: 0x1700112A RID: 4394
				// (get) Token: 0x06004B2E RID: 19246 RVA: 0x00105596 File Offset: 0x00103796
				public string LocalHomePage
				{
					get
					{
						return null;
					}
				}

				// Token: 0x1700112B RID: 4395
				// (get) Token: 0x06004B2F RID: 19247 RVA: 0x00105599 File Offset: 0x00103799
				public string BusinessHomePage
				{
					get
					{
						return null;
					}
				}

				// Token: 0x1700112C RID: 4396
				// (get) Token: 0x06004B30 RID: 19248 RVA: 0x0010559C File Offset: 0x0010379C
				// (set) Token: 0x06004B31 RID: 19249 RVA: 0x001055A4 File Offset: 0x001037A4
				[DataMember]
				public string entityType { get; set; }

				// Token: 0x02000A59 RID: 2649
				[DataContract]
				public class Point
				{
					// Token: 0x1700112D RID: 4397
					// (get) Token: 0x06004B33 RID: 19251 RVA: 0x001055B5 File Offset: 0x001037B5
					// (set) Token: 0x06004B34 RID: 19252 RVA: 0x001055BD File Offset: 0x001037BD
					[DataMember]
					public string[] coordinates { get; set; }
				}

				// Token: 0x02000A5A RID: 2650
				[DataContract]
				public class Address
				{
					// Token: 0x1700112E RID: 4398
					// (get) Token: 0x06004B36 RID: 19254 RVA: 0x001055CE File Offset: 0x001037CE
					// (set) Token: 0x06004B37 RID: 19255 RVA: 0x001055D6 File Offset: 0x001037D6
					[DataMember]
					public string addressLine { get; set; }

					// Token: 0x1700112F RID: 4399
					// (get) Token: 0x06004B38 RID: 19256 RVA: 0x001055DF File Offset: 0x001037DF
					// (set) Token: 0x06004B39 RID: 19257 RVA: 0x001055E7 File Offset: 0x001037E7
					[DataMember]
					public string adminDistrict { get; set; }

					// Token: 0x17001130 RID: 4400
					// (get) Token: 0x06004B3A RID: 19258 RVA: 0x001055F0 File Offset: 0x001037F0
					// (set) Token: 0x06004B3B RID: 19259 RVA: 0x001055F8 File Offset: 0x001037F8
					[DataMember]
					public string countryRegion { get; set; }

					// Token: 0x17001131 RID: 4401
					// (get) Token: 0x06004B3C RID: 19260 RVA: 0x00105601 File Offset: 0x00103801
					// (set) Token: 0x06004B3D RID: 19261 RVA: 0x00105609 File Offset: 0x00103809
					[DataMember]
					public string formattedAddress { get; set; }

					// Token: 0x17001132 RID: 4402
					// (get) Token: 0x06004B3E RID: 19262 RVA: 0x00105612 File Offset: 0x00103812
					// (set) Token: 0x06004B3F RID: 19263 RVA: 0x0010561A File Offset: 0x0010381A
					[DataMember]
					public string locality { get; set; }

					// Token: 0x17001133 RID: 4403
					// (get) Token: 0x06004B40 RID: 19264 RVA: 0x00105623 File Offset: 0x00103823
					// (set) Token: 0x06004B41 RID: 19265 RVA: 0x0010562B File Offset: 0x0010382B
					[DataMember]
					public string postalCode { get; set; }
				}
			}
		}
	}
}
