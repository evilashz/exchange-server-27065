using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000054 RID: 84
	public class MimeLimits
	{
		// Token: 0x060002DD RID: 733 RVA: 0x00010208 File Offset: 0x0000E408
		internal MimeLimits(int partDepth, int embeddedDepth, int size, int headerBytes, int parts, int headers, int addressItemsPerHeader, int textValueBytesPerValue, int parametersPerHeader, int encodedWordLength)
		{
			this.partDepth = partDepth;
			this.embeddedDepth = embeddedDepth;
			this.size = size;
			this.headerBytes = headerBytes;
			this.parts = parts;
			this.headers = headers;
			this.addressItemsPerHeader = addressItemsPerHeader;
			this.textValueBytesPerValue = textValueBytesPerValue;
			this.parametersPerHeader = parametersPerHeader;
			this.encodedWordLength = encodedWordLength;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000102CC File Offset: 0x0000E4CC
		private MimeLimits()
		{
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00010342 File Offset: 0x0000E542
		public static MimeLimits Default
		{
			get
			{
				return MimeLimits.GetDefaultMimeDocumentLimits();
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00010349 File Offset: 0x0000E549
		public static MimeLimits Unlimited
		{
			get
			{
				return MimeLimits.unlimitedLimits;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00010350 File Offset: 0x0000E550
		public int MaxPartDepth
		{
			get
			{
				return this.partDepth;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00010358 File Offset: 0x0000E558
		public int MaxEmbeddedDepth
		{
			get
			{
				return this.embeddedDepth;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00010360 File Offset: 0x0000E560
		public int MaxSize
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00010368 File Offset: 0x0000E568
		public int MaxHeaderBytes
		{
			get
			{
				return this.headerBytes;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00010370 File Offset: 0x0000E570
		public int MaxParts
		{
			get
			{
				return this.parts;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00010378 File Offset: 0x0000E578
		public int MaxHeaders
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00010380 File Offset: 0x0000E580
		public int MaxAddressItemsPerHeader
		{
			get
			{
				return this.addressItemsPerHeader;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00010388 File Offset: 0x0000E588
		public int MaxTextValueBytesPerValue
		{
			get
			{
				return this.textValueBytesPerValue;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00010390 File Offset: 0x0000E590
		public int MaxParametersPerHeader
		{
			get
			{
				return this.parametersPerHeader;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00010398 File Offset: 0x0000E598
		internal int MaxEncodedWordLength
		{
			get
			{
				return this.encodedWordLength;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000103A0 File Offset: 0x0000E5A0
		internal static void RefreshConfiguration()
		{
			MimeLimits.defaultLimits = null;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000103AC File Offset: 0x0000E5AC
		private static MimeLimits GetDefaultMimeDocumentLimits()
		{
			if (MimeLimits.defaultLimits == null)
			{
				lock (MimeLimits.configurationLockObject)
				{
					if (MimeLimits.defaultLimits == null)
					{
						IList<CtsConfigurationSetting> configuration = ApplicationServices.Provider.GetConfiguration("MimeLimits");
						int defaultValue = 10;
						int defaultValue2 = 100;
						int defaultValue3 = int.MaxValue;
						int defaultValue4 = int.MaxValue;
						int defaultValue5 = 10000;
						int defaultValue6 = 100000;
						int defaultValue7 = int.MaxValue;
						int defaultValue8 = 32768;
						int defaultValue9 = int.MaxValue;
						int num = 256;
						foreach (CtsConfigurationSetting ctsConfigurationSetting in configuration)
						{
							string key;
							switch (key = ctsConfigurationSetting.Name.ToLower())
							{
							case "maximumpartdepth":
								defaultValue = ApplicationServices.ParseIntegerSetting(ctsConfigurationSetting, defaultValue, 5, false);
								break;
							case "maximumembeddeddepth":
								defaultValue2 = ApplicationServices.ParseIntegerSetting(ctsConfigurationSetting, defaultValue2, 10, false);
								break;
							case "maximumsize":
								defaultValue3 = ApplicationServices.ParseIntegerSetting(ctsConfigurationSetting, defaultValue3, 100, true);
								break;
							case "maximumtotalheaderssize":
								defaultValue4 = ApplicationServices.ParseIntegerSetting(ctsConfigurationSetting, defaultValue4, 100, true);
								break;
							case "maximumparts":
								defaultValue5 = ApplicationServices.ParseIntegerSetting(ctsConfigurationSetting, defaultValue5, 100, false);
								break;
							case "maximumheaders":
								defaultValue6 = ApplicationServices.ParseIntegerSetting(ctsConfigurationSetting, defaultValue6, 100, false);
								break;
							case "maximumaddressitemsperheader":
								defaultValue7 = ApplicationServices.ParseIntegerSetting(ctsConfigurationSetting, defaultValue7, 100, false);
								break;
							case "maximumparametersperheader":
								defaultValue9 = ApplicationServices.ParseIntegerSetting(ctsConfigurationSetting, defaultValue9, 10, false);
								break;
							case "maximumtextvaluesize":
								defaultValue8 = ApplicationServices.ParseIntegerSetting(ctsConfigurationSetting, defaultValue8, 10, true);
								break;
							}
						}
						MimeLimits.defaultLimits = new MimeLimits(defaultValue, defaultValue2, defaultValue3, defaultValue4, defaultValue5, defaultValue6, defaultValue7, defaultValue8, defaultValue9, num);
					}
				}
			}
			return MimeLimits.defaultLimits;
		}

		// Token: 0x04000261 RID: 609
		private static object configurationLockObject = new object();

		// Token: 0x04000262 RID: 610
		private static volatile MimeLimits defaultLimits;

		// Token: 0x04000263 RID: 611
		private static MimeLimits unlimitedLimits = new MimeLimits();

		// Token: 0x04000264 RID: 612
		private int partDepth = int.MaxValue;

		// Token: 0x04000265 RID: 613
		private int embeddedDepth = int.MaxValue;

		// Token: 0x04000266 RID: 614
		private int size = int.MaxValue;

		// Token: 0x04000267 RID: 615
		private int headerBytes = int.MaxValue;

		// Token: 0x04000268 RID: 616
		private int parts = int.MaxValue;

		// Token: 0x04000269 RID: 617
		private int headers = int.MaxValue;

		// Token: 0x0400026A RID: 618
		private int addressItemsPerHeader = int.MaxValue;

		// Token: 0x0400026B RID: 619
		private int textValueBytesPerValue = int.MaxValue;

		// Token: 0x0400026C RID: 620
		private int parametersPerHeader = int.MaxValue;

		// Token: 0x0400026D RID: 621
		private int encodedWordLength;
	}
}
