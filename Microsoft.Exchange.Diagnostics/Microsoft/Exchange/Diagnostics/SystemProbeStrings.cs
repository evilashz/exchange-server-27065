using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200040D RID: 1037
	internal static class SystemProbeStrings
	{
		// Token: 0x06001910 RID: 6416 RVA: 0x0005EF54 File Offset: 0x0005D154
		static SystemProbeStrings()
		{
			SystemProbeStrings.stringIDs.Add(3063416394U, "InvalidGuidInDecryptedText");
			SystemProbeStrings.stringIDs.Add(1270928367U, "EncryptedDataNotValidBase64String");
			SystemProbeStrings.stringIDs.Add(1976115009U, "NullEncryptedData");
			SystemProbeStrings.stringIDs.Add(3719046686U, "CertificateNotFound");
			SystemProbeStrings.stringIDs.Add(2483075962U, "InvalidTimeInDecryptedText");
			SystemProbeStrings.stringIDs.Add(4144305424U, "EncryptedDataCannotBeDecrypted");
			SystemProbeStrings.stringIDs.Add(4289353018U, "CertificateNotSigned");
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x0005F01B File Offset: 0x0005D21B
		public static LocalizedString InvalidGuidInDecryptedText
		{
			get
			{
				return new LocalizedString("InvalidGuidInDecryptedText", SystemProbeStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0005F032 File Offset: 0x0005D232
		public static LocalizedString EncryptedDataNotValidBase64String
		{
			get
			{
				return new LocalizedString("EncryptedDataNotValidBase64String", SystemProbeStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0005F049 File Offset: 0x0005D249
		public static LocalizedString NullEncryptedData
		{
			get
			{
				return new LocalizedString("NullEncryptedData", SystemProbeStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0005F060 File Offset: 0x0005D260
		public static LocalizedString CertificateTimeNotValid(string start, string end)
		{
			return new LocalizedString("CertificateTimeNotValid", SystemProbeStrings.ResourceManager, new object[]
			{
				start,
				end
			});
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0005F08C File Offset: 0x0005D28C
		public static LocalizedString CertificateNotFound
		{
			get
			{
				return new LocalizedString("CertificateNotFound", SystemProbeStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x0005F0A3 File Offset: 0x0005D2A3
		public static LocalizedString InvalidTimeInDecryptedText
		{
			get
			{
				return new LocalizedString("InvalidTimeInDecryptedText", SystemProbeStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x0005F0BA File Offset: 0x0005D2BA
		public static LocalizedString EncryptedDataCannotBeDecrypted
		{
			get
			{
				return new LocalizedString("EncryptedDataCannotBeDecrypted", SystemProbeStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x0005F0D1 File Offset: 0x0005D2D1
		public static LocalizedString CertificateNotSigned
		{
			get
			{
				return new LocalizedString("CertificateNotSigned", SystemProbeStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0005F0E8 File Offset: 0x0005D2E8
		public static LocalizedString GetLocalizedString(SystemProbeStrings.IDs key)
		{
			return new LocalizedString(SystemProbeStrings.stringIDs[(uint)key], SystemProbeStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04001DBC RID: 7612
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(7);

		// Token: 0x04001DBD RID: 7613
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Diagnostics.SystemProbeStrings", typeof(SystemProbeStrings).GetTypeInfo().Assembly);

		// Token: 0x0200040E RID: 1038
		public enum IDs : uint
		{
			// Token: 0x04001DBF RID: 7615
			InvalidGuidInDecryptedText = 3063416394U,
			// Token: 0x04001DC0 RID: 7616
			EncryptedDataNotValidBase64String = 1270928367U,
			// Token: 0x04001DC1 RID: 7617
			NullEncryptedData = 1976115009U,
			// Token: 0x04001DC2 RID: 7618
			CertificateNotFound = 3719046686U,
			// Token: 0x04001DC3 RID: 7619
			InvalidTimeInDecryptedText = 2483075962U,
			// Token: 0x04001DC4 RID: 7620
			EncryptedDataCannotBeDecrypted = 4144305424U,
			// Token: 0x04001DC5 RID: 7621
			CertificateNotSigned = 4289353018U
		}

		// Token: 0x0200040F RID: 1039
		private enum ParamIDs
		{
			// Token: 0x04001DC7 RID: 7623
			CertificateTimeNotValid
		}
	}
}
