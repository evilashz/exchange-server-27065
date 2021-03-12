using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	public class GeoCoordinates
	{
		// Token: 0x06000B2F RID: 2863 RVA: 0x000230B4 File Offset: 0x000212B4
		public GeoCoordinates(string expression)
		{
			this.ParseAndValidateGeoCoordinates(expression);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000230C3 File Offset: 0x000212C3
		public GeoCoordinates(double latitude, double longitude)
		{
			this.latitude = new double?(latitude);
			this.longitude = new double?(longitude);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000230E3 File Offset: 0x000212E3
		public GeoCoordinates(double latitude, double longitude, double altitude) : this(latitude, longitude)
		{
			this.altitude = new double?(altitude);
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x000230F9 File Offset: 0x000212F9
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x00023107 File Offset: 0x00021307
		public double Latitude
		{
			get
			{
				return this.latitude.Value;
			}
			set
			{
				if (!GeoCoordinates.IsValidLatitude(value))
				{
					throw new FormatException(DataStrings.ExceptionInvalidLatitude(value));
				}
				this.latitude = new double?(value);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0002312E File Offset: 0x0002132E
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x0002313C File Offset: 0x0002133C
		public double Longitude
		{
			get
			{
				return this.longitude.Value;
			}
			set
			{
				if (!GeoCoordinates.IsValidLongitude(value))
				{
					throw new FormatException(DataStrings.ExceptionInvalidLongitude(value));
				}
				this.longitude = new double?(value);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00023163 File Offset: 0x00021363
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0002316B File Offset: 0x0002136B
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

		// Token: 0x06000B38 RID: 2872 RVA: 0x00023174 File Offset: 0x00021374
		public override string ToString()
		{
			if (this.altitude == null)
			{
				return string.Format("{0};{1}", this.latitude, this.longitude);
			}
			return string.Format("{0};{1};{2}", this.latitude, this.longitude, this.altitude.Value);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000231DF File Offset: 0x000213DF
		private static bool IsValidLatitude(double latitude)
		{
			return latitude >= -90.0 && latitude <= 90.0;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000231FE File Offset: 0x000213FE
		private static bool IsValidLongitude(double longitude)
		{
			return longitude >= -180.0 && longitude <= 180.0;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00023220 File Offset: 0x00021420
		private void ParseAndValidateGeoCoordinates(string geoCoordinates)
		{
			geoCoordinates = geoCoordinates.Trim();
			string[] array = geoCoordinates.Split(new char[]
			{
				';'
			});
			if (array.Length != 2 && array.Length != 3)
			{
				throw new FormatException(DataStrings.ExceptionGeoCoordinatesWithWrongFormat(geoCoordinates));
			}
			double value;
			if (!double.TryParse(array[0], out value) || !GeoCoordinates.IsValidLatitude(value))
			{
				throw new FormatException(DataStrings.ExceptionGeoCoordinatesWithInvalidLatitude(geoCoordinates));
			}
			this.latitude = new double?(value);
			double value2;
			if (!double.TryParse(array[1], out value2) || !GeoCoordinates.IsValidLongitude(value2))
			{
				throw new FormatException(DataStrings.ExceptionGeoCoordinatesWithInvalidLongitude(geoCoordinates));
			}
			this.longitude = new double?(value2);
			if (array.Length == 3)
			{
				double value3;
				if (!double.TryParse(array[2], out value3))
				{
					throw new FormatException(DataStrings.ExceptionGeoCoordinatesWithInvalidAltitude(geoCoordinates));
				}
				this.altitude = new double?(value3);
			}
		}

		// Token: 0x040006FD RID: 1789
		private const double MaxLatitude = 90.0;

		// Token: 0x040006FE RID: 1790
		private const double MinLatitude = -90.0;

		// Token: 0x040006FF RID: 1791
		private const double MaxLongitude = 180.0;

		// Token: 0x04000700 RID: 1792
		private const double MinLongitude = -180.0;

		// Token: 0x04000701 RID: 1793
		private double? latitude;

		// Token: 0x04000702 RID: 1794
		private double? longitude;

		// Token: 0x04000703 RID: 1795
		private double? altitude;
	}
}
