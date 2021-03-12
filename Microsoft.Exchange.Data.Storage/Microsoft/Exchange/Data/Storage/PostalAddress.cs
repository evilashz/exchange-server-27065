using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200054F RID: 1359
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class PostalAddress : IEquatable<PostalAddress>
	{
		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x000EB534 File Offset: 0x000E9734
		// (set) Token: 0x06003952 RID: 14674 RVA: 0x000EB53C File Offset: 0x000E973C
		public string Street
		{
			get
			{
				return this.street;
			}
			set
			{
				this.street = value;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06003953 RID: 14675 RVA: 0x000EB545 File Offset: 0x000E9745
		// (set) Token: 0x06003954 RID: 14676 RVA: 0x000EB54D File Offset: 0x000E974D
		public string City
		{
			get
			{
				return this.city;
			}
			set
			{
				this.city = value;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000EB556 File Offset: 0x000E9756
		// (set) Token: 0x06003956 RID: 14678 RVA: 0x000EB55E File Offset: 0x000E975E
		public string State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x000EB567 File Offset: 0x000E9767
		// (set) Token: 0x06003958 RID: 14680 RVA: 0x000EB56F File Offset: 0x000E976F
		public string Country
		{
			get
			{
				return this.country;
			}
			set
			{
				this.country = value;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06003959 RID: 14681 RVA: 0x000EB578 File Offset: 0x000E9778
		// (set) Token: 0x0600395A RID: 14682 RVA: 0x000EB580 File Offset: 0x000E9780
		public string PostalCode
		{
			get
			{
				return this.postalCode;
			}
			set
			{
				this.postalCode = value;
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x000EB589 File Offset: 0x000E9789
		// (set) Token: 0x0600395C RID: 14684 RVA: 0x000EB591 File Offset: 0x000E9791
		public string PostOfficeBox
		{
			get
			{
				return this.postOfficeBox;
			}
			set
			{
				this.postOfficeBox = value;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x000EB59A File Offset: 0x000E979A
		// (set) Token: 0x0600395E RID: 14686 RVA: 0x000EB5A2 File Offset: 0x000E97A2
		public double? Latitude
		{
			get
			{
				return this.latitude;
			}
			set
			{
				this.latitude = value;
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000EB5AB File Offset: 0x000E97AB
		// (set) Token: 0x06003960 RID: 14688 RVA: 0x000EB5B3 File Offset: 0x000E97B3
		public double? Longitude
		{
			get
			{
				return this.longitude;
			}
			set
			{
				this.longitude = value;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x000EB5BC File Offset: 0x000E97BC
		// (set) Token: 0x06003962 RID: 14690 RVA: 0x000EB5C4 File Offset: 0x000E97C4
		public double? Accuracy
		{
			get
			{
				return this.accuracy;
			}
			set
			{
				this.accuracy = value;
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06003963 RID: 14691 RVA: 0x000EB5CD File Offset: 0x000E97CD
		// (set) Token: 0x06003964 RID: 14692 RVA: 0x000EB5D5 File Offset: 0x000E97D5
		public double? Altitude
		{
			get
			{
				return this.altitude;
			}
			set
			{
				this.altitude = value;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06003965 RID: 14693 RVA: 0x000EB5DE File Offset: 0x000E97DE
		// (set) Token: 0x06003966 RID: 14694 RVA: 0x000EB5E6 File Offset: 0x000E97E6
		public double? AltitudeAccuracy
		{
			get
			{
				return this.altitudeAccuracy;
			}
			set
			{
				this.altitudeAccuracy = value;
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x000EB5EF File Offset: 0x000E97EF
		// (set) Token: 0x06003968 RID: 14696 RVA: 0x000EB5F7 File Offset: 0x000E97F7
		public string LocationUri
		{
			get
			{
				return this.locationUri;
			}
			set
			{
				this.locationUri = value;
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06003969 RID: 14697 RVA: 0x000EB600 File Offset: 0x000E9800
		// (set) Token: 0x0600396A RID: 14698 RVA: 0x000EB608 File Offset: 0x000E9808
		public LocationSource LocationSource
		{
			get
			{
				return this.locationSource;
			}
			set
			{
				if (EnumValidator.IsValidValue<LocationSource>(value))
				{
					this.locationSource = value;
					return;
				}
				this.locationSource = LocationSource.None;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x0600396B RID: 14699 RVA: 0x000EB621 File Offset: 0x000E9821
		// (set) Token: 0x0600396C RID: 14700 RVA: 0x000EB629 File Offset: 0x000E9829
		public PostalAddressType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<PostalAddressType>(value, "Type");
				this.type = value;
			}
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000EB640 File Offset: 0x000E9840
		public bool IsEmpty()
		{
			return (this.accuracy == null || (this.accuracy != null && double.IsNaN(this.accuracy.Value))) && (this.altitude == null || (this.altitude != null && double.IsNaN(this.altitude.Value))) && (this.altitudeAccuracy == null || (this.altitudeAccuracy != null && double.IsNaN(this.altitudeAccuracy.Value))) && this.city == null && this.country == null && (this.latitude == null || (this.latitude != null && double.IsNaN(this.latitude.Value))) && this.locationUri == null && (this.longitude == null || (this.longitude != null && double.IsNaN(this.longitude.Value))) && this.postalCode == null && this.postOfficeBox == null && this.state == null && this.street == null;
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000EB77C File Offset: 0x000E997C
		public bool Equals(PostalAddress other)
		{
			return other != null && (string.Equals(this.street, other.street, StringComparison.OrdinalIgnoreCase) && string.Equals(this.city, other.city, StringComparison.OrdinalIgnoreCase) && string.Equals(this.state, other.state, StringComparison.OrdinalIgnoreCase) && string.Equals(this.country, other.country, StringComparison.OrdinalIgnoreCase) && string.Equals(this.postalCode, other.postalCode, StringComparison.OrdinalIgnoreCase)) && string.Equals(this.postOfficeBox, other.postOfficeBox, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000EB806 File Offset: 0x000E9A06
		public override bool Equals(object other)
		{
			return this.Equals(other as PostalAddress);
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000EB814 File Offset: 0x000E9A14
		public override int GetHashCode()
		{
			int num = string.IsNullOrEmpty(this.street) ? 0 : this.street.GetHashCode();
			int num2 = string.IsNullOrEmpty(this.city) ? 0 : this.city.GetHashCode();
			int num3 = string.IsNullOrEmpty(this.state) ? 0 : this.state.GetHashCode();
			int num4 = string.IsNullOrEmpty(this.country) ? 0 : this.country.GetHashCode();
			int num5 = string.IsNullOrEmpty(this.postalCode) ? 0 : this.postalCode.GetHashCode();
			if (!string.IsNullOrEmpty(this.postOfficeBox))
			{
				this.postOfficeBox.GetHashCode();
			}
			return num ^ num2 ^ num3 ^ num4 ^ num5 ^ num5;
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000EB8D4 File Offset: 0x000E9AD4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			PostalAddress.Append(stringBuilder, "street", this.street);
			PostalAddress.Append(stringBuilder, "city", this.city);
			PostalAddress.Append(stringBuilder, "state", this.state);
			PostalAddress.Append(stringBuilder, "country", this.country);
			PostalAddress.Append(stringBuilder, "postalCode", this.postalCode);
			PostalAddress.Append(stringBuilder, "postOfficeBox", this.postOfficeBox);
			PostalAddress.Append<double>(stringBuilder, "latitude", this.latitude);
			PostalAddress.Append<double>(stringBuilder, "longitude", this.longitude);
			PostalAddress.Append<double>(stringBuilder, "accuracy", this.accuracy);
			PostalAddress.Append<double>(stringBuilder, "altitude", this.altitude);
			PostalAddress.Append<double>(stringBuilder, "altitudeAccuracy", this.altitudeAccuracy);
			PostalAddress.Append(stringBuilder, "locationUri", this.locationUri);
			PostalAddress.Append(stringBuilder, "locationSource", this.locationSource.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000EB9D8 File Offset: 0x000E9BD8
		private static void Append<T>(StringBuilder text, string name, T? value) where T : struct
		{
			if (value != null)
			{
				T value2 = value.Value;
				PostalAddress.AppendNameValue(text, name, value2.ToString());
			}
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000EBA0A File Offset: 0x000E9C0A
		private static void Append(StringBuilder text, string name, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				PostalAddress.AppendNameValue(text, name, value);
			}
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000EBA1C File Offset: 0x000E9C1C
		private static void AppendNameValue(StringBuilder text, string name, string value)
		{
			if (text.Length > 0)
			{
				text.Append(", ");
			}
			text.Append(name);
			text.Append("=");
			text.Append(value);
		}

		// Token: 0x04001E99 RID: 7833
		private string street;

		// Token: 0x04001E9A RID: 7834
		private string city;

		// Token: 0x04001E9B RID: 7835
		private string state;

		// Token: 0x04001E9C RID: 7836
		private string country;

		// Token: 0x04001E9D RID: 7837
		private string postalCode;

		// Token: 0x04001E9E RID: 7838
		private string postOfficeBox;

		// Token: 0x04001E9F RID: 7839
		private double? latitude;

		// Token: 0x04001EA0 RID: 7840
		private double? longitude;

		// Token: 0x04001EA1 RID: 7841
		private double? accuracy;

		// Token: 0x04001EA2 RID: 7842
		private double? altitude;

		// Token: 0x04001EA3 RID: 7843
		private double? altitudeAccuracy;

		// Token: 0x04001EA4 RID: 7844
		private string locationUri;

		// Token: 0x04001EA5 RID: 7845
		private LocationSource locationSource;

		// Token: 0x04001EA6 RID: 7846
		private PostalAddressType type;
	}
}
