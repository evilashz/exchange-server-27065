using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data.ApplicationLogic.TextMessaging;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000489 RID: 1161
	public sealed class SmsServiceProviders
	{
		// Token: 0x06003A0B RID: 14859 RVA: 0x000AFD01 File Offset: 0x000ADF01
		private SmsServiceProviders()
		{
			TextMessagingHostingDataCache.Instance.Changed += this.Refresh;
			this.Refresh(null);
		}

		// Token: 0x170022EF RID: 8943
		// (get) Token: 0x06003A0C RID: 14860 RVA: 0x000AFD26 File Offset: 0x000ADF26
		public static SmsServiceProviders Instance
		{
			get
			{
				return SmsServiceProviders.instance;
			}
		}

		// Token: 0x170022F0 RID: 8944
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000AFD2D File Offset: 0x000ADF2D
		public IEnumerable<RegionData> RegionList
		{
			get
			{
				return this.regionList;
			}
		}

		// Token: 0x170022F1 RID: 8945
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000AFD35 File Offset: 0x000ADF35
		public IDictionary<string, CarrierData> CarrierDictionary
		{
			get
			{
				return this.carrierList;
			}
		}

		// Token: 0x170022F2 RID: 8946
		// (get) Token: 0x06003A0F RID: 14863 RVA: 0x000AFD3D File Offset: 0x000ADF3D
		public IDictionary<string, CarrierData> VoiceMailCarrierDictionary
		{
			get
			{
				return this.voiceMailCarrierList;
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000AFD48 File Offset: 0x000ADF48
		public static string GetLocalizedName(Dictionary<string, string> localizedNames)
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			string name = currentCulture.Name;
			string twoLetterISOLanguageName = currentCulture.TwoLetterISOLanguageName;
			string result;
			if (!localizedNames.TryGetValue(name, out result) && !localizedNames.TryGetValue(twoLetterISOLanguageName, out result))
			{
				result = localizedNames.Values.First<string>();
			}
			return result;
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000AFDE4 File Offset: 0x000ADFE4
		private void Refresh(TextMessagingHostingData hostingData)
		{
			TextMessagingHostingData textMessagingHostingData = hostingData ?? TextMessagingHostingDataCache.Instance.GetHostingData();
			bool flag = false;
			try
			{
				Monitor.Enter(this, ref flag);
				this.regionList = new List<RegionData>();
				this.carrierList = new Dictionary<string, CarrierData>();
				this.voiceMailCarrierList = new Dictionary<string, CarrierData>();
				foreach (TextMessagingHostingDataCarriersCarrier textMessagingHostingDataCarriersCarrier in textMessagingHostingData.Carriers.Carrier)
				{
					string text = textMessagingHostingDataCarriersCarrier.Identity.ToString("00000");
					CarrierData carrierData = new CarrierData();
					carrierData.ID = text;
					bool flag2 = false;
					foreach (TextMessagingHostingDataServicesService textMessagingHostingDataServicesService in textMessagingHostingData.Services.Service)
					{
						if (textMessagingHostingDataServicesService.CarrierIdentity == textMessagingHostingDataCarriersCarrier.Identity)
						{
							if (TextMessagingHostingDataServicesServiceType.SmtpToSmsGateway == textMessagingHostingDataServicesService.Type && textMessagingHostingDataServicesService.SmtpToSmsGateway != null)
							{
								carrierData.HasSmtpGateway = true;
							}
							else if (textMessagingHostingDataServicesService.Type == TextMessagingHostingDataServicesServiceType.VoiceCallForwarding && textMessagingHostingDataServicesService.VoiceCallForwarding != null)
							{
								carrierData.UnifiedMessagingInfo = new UnifiedMessagingInfo(textMessagingHostingDataServicesService.VoiceCallForwarding.Enable, textMessagingHostingDataServicesService.VoiceCallForwarding.Disable, textMessagingHostingDataServicesService.VoiceCallForwarding.Type.ToString());
								flag2 = true;
							}
						}
					}
					Dictionary<string, string> dictionary = new Dictionary<string, string>(textMessagingHostingDataCarriersCarrier.LocalizedInfo.Length);
					for (int k = 0; k < textMessagingHostingDataCarriersCarrier.LocalizedInfo.Length; k++)
					{
						dictionary.Add(textMessagingHostingDataCarriersCarrier.LocalizedInfo[k].Culture, textMessagingHostingDataCarriersCarrier.LocalizedInfo[k].DisplayName);
					}
					carrierData.LocalizedNames = dictionary;
					this.carrierList.Add(text, carrierData);
					if (flag2)
					{
						this.voiceMailCarrierList.Add(text, carrierData);
					}
				}
				Dictionary<string, string> localizedCarrierNames = new Dictionary<string, string>(this.carrierList.Count);
				foreach (KeyValuePair<string, CarrierData> keyValuePair in this.carrierList)
				{
					localizedCarrierNames.Add(keyValuePair.Key, SmsServiceProviders.GetLocalizedName(keyValuePair.Value.LocalizedNames));
				}
				TextMessagingHostingDataRegionsRegion[] region2 = textMessagingHostingData.Regions.Region;
				int l = 0;
				while (l < region2.Length)
				{
					TextMessagingHostingDataRegionsRegion region = region2[l];
					RegionInfo regionInfo;
					try
					{
						regionInfo = new RegionInfo(region.Iso2);
					}
					catch (ArgumentException)
					{
						goto IL_3AD;
					}
					goto IL_26A;
					IL_3AD:
					l++;
					continue;
					IL_26A:
					int[] array = (from service in textMessagingHostingData.Services.Service
					where service.RegionIso2 == region.Iso2
					group service by service.CarrierIdentity into servicesByCarrier
					select servicesByCarrier.Key).ToArray<int>();
					if (array.Length == 0)
					{
						goto IL_3AD;
					}
					List<string> list = new List<string>(array.Length);
					foreach (int num in array)
					{
						string text2 = num.ToString("00000");
						if (this.carrierList.ContainsKey(text2))
						{
							list.Add(text2);
						}
					}
					if (list.Count != 0)
					{
						list.Sort((string x, string y) => string.Compare(localizedCarrierNames[x], localizedCarrierNames[y], StringComparison.CurrentCultureIgnoreCase));
						RegionData regionData = new RegionData();
						regionData.ID = region.Iso2.ToUpper();
						regionData.RegionInfo = regionInfo;
						regionData.CountryCode = region.CountryCode;
						regionData.CarrierIDs = list.ToArray();
						this.regionList.Add(regionData);
						goto IL_3AD;
					}
					goto IL_3AD;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x040026DB RID: 9947
		private static SmsServiceProviders instance = new SmsServiceProviders();

		// Token: 0x040026DC RID: 9948
		private List<RegionData> regionList;

		// Token: 0x040026DD RID: 9949
		private Dictionary<string, CarrierData> carrierList;

		// Token: 0x040026DE RID: 9950
		private Dictionary<string, CarrierData> voiceMailCarrierList;
	}
}
