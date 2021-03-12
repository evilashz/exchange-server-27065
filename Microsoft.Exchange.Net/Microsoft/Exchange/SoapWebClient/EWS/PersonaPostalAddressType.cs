using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001DB RID: 475
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class PersonaPostalAddressType
	{
		// Token: 0x04000C94 RID: 3220
		public string Street;

		// Token: 0x04000C95 RID: 3221
		public string City;

		// Token: 0x04000C96 RID: 3222
		public string State;

		// Token: 0x04000C97 RID: 3223
		public string Country;

		// Token: 0x04000C98 RID: 3224
		public string PostalCode;

		// Token: 0x04000C99 RID: 3225
		public string PostOfficeBox;

		// Token: 0x04000C9A RID: 3226
		public string Type;

		// Token: 0x04000C9B RID: 3227
		public double Latitude;

		// Token: 0x04000C9C RID: 3228
		[XmlIgnore]
		public bool LatitudeSpecified;

		// Token: 0x04000C9D RID: 3229
		public double Longitude;

		// Token: 0x04000C9E RID: 3230
		[XmlIgnore]
		public bool LongitudeSpecified;

		// Token: 0x04000C9F RID: 3231
		public double Accuracy;

		// Token: 0x04000CA0 RID: 3232
		[XmlIgnore]
		public bool AccuracySpecified;

		// Token: 0x04000CA1 RID: 3233
		public double Altitude;

		// Token: 0x04000CA2 RID: 3234
		[XmlIgnore]
		public bool AltitudeSpecified;

		// Token: 0x04000CA3 RID: 3235
		public double AltitudeAccuracy;

		// Token: 0x04000CA4 RID: 3236
		[XmlIgnore]
		public bool AltitudeAccuracySpecified;

		// Token: 0x04000CA5 RID: 3237
		public string FormattedAddress;

		// Token: 0x04000CA6 RID: 3238
		public string LocationUri;

		// Token: 0x04000CA7 RID: 3239
		public LocationSourceType LocationSource;

		// Token: 0x04000CA8 RID: 3240
		[XmlIgnore]
		public bool LocationSourceSpecified;
	}
}
