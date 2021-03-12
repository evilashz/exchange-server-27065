using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Live.Controls;
using Microsoft.Live.Frontend;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200001C RID: 28
	internal class LiveAssetReader : CobrandingAssetReader
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000075F8 File Offset: 0x000057F8
		internal LiveAssetReader(HttpContext context)
		{
			DateTime start = DateTime.MinValue;
			DateTime end = DateTime.MinValue;
			try
			{
				this.Initialize();
				if (LiveAssetReader.isInitialized)
				{
					start = DateTime.UtcNow;
					this.aleStringsAgent = new AleStringsAgent(context);
					end = DateTime.UtcNow;
					if (BrandingUtilities.IsBranded())
					{
						this.brandControl = BrandControl.Create(context);
					}
					this.InitializeEnvironmentQualifier();
					CobrandingAssetReader.initializeErrorLogged = false;
				}
			}
			catch (Exception e)
			{
				base.LogInitializeException(e, ClientsEventLogConstants.Tuple_LiveHeaderConfigurationError);
			}
			finally
			{
				try
				{
					context.Response.AppendToLog(this.GetDurationLogMessage("Ale", start, end));
				}
				catch
				{
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000076B8 File Offset: 0x000058B8
		public override bool IsPreviewBrand()
		{
			string brandId = CobrandingAssetReader.GetBrandId();
			return '0' == brandId[0] && char.IsNumber(brandId[1]) && brandId.Length == 8 && (int.Parse(brandId.Substring(1, 1), CultureInfo.InvariantCulture) & 2) != 0;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007706 File Offset: 0x00005906
		public override string GetString(CobrandingAssetKey assetKey)
		{
			return this.GetString(CobrandingAssetKeys.GetAssetKeyString(assetKey));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007714 File Offset: 0x00005914
		public string GetString(LiveAssetKey assetKey)
		{
			return this.GetString(LiveAssetKeys.GetAssetKeyString(assetKey));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007724 File Offset: 0x00005924
		public string GetString(string assetId)
		{
			if (string.IsNullOrEmpty(assetId))
			{
				return string.Empty;
			}
			if (assetId == "Live.Shared.GlobalSettings.Header.Tabs.Cobrand.Mail.Text" || assetId == "Live.Shared.GlobalSettings.Header.Tabs.Mail.Text")
			{
				return "Mail_Link_Text_Placeholder";
			}
			if (this.aleStringsAgent == null)
			{
				return string.Empty;
			}
			string text;
			if (this.brandControl == null || string.IsNullOrEmpty(this.brandControl.GetString(assetId, this.aleStringsAgent.Market)))
			{
				text = LiveAssetReader.liveAssetToUrlDictionary[assetId];
				if (text != null)
				{
					return string.Format(text, this.environmentQualifier, this.aleStringsAgent.Market);
				}
			}
			try
			{
				text = this.aleStringsAgent.GetString(assetId);
			}
			catch (NullReferenceException)
			{
				text = string.Empty;
			}
			if (text != null && text.StartsWith("[Error!"))
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000077FC File Offset: 0x000059FC
		public bool HasAssetValue(LiveAssetKey assetKey)
		{
			return !string.IsNullOrEmpty(this.GetString(assetKey));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007810 File Offset: 0x00005A10
		private static void InitResourceConsumer(object state)
		{
			try
			{
				LoggingUtilities.LogEvent(ClientsEventLogConstants.Tuple_LiveAssetReaderInitResourceConsumerStarted, new object[0]);
				ResourceConsumer.Init((HttpApplication)state, ResourceConsumer.ReadConfigData(LiveAssetReader.configPath));
				LiveAssetReader.isInitialized = true;
				LoggingUtilities.LogEvent(ClientsEventLogConstants.Tuple_LiveAssetReaderInitResourceConsumerSucceeded, new object[0]);
			}
			catch (Exception ex)
			{
				LiveAssetReader.isInitialized = false;
				LoggingUtilities.LogEvent(ClientsEventLogConstants.Tuple_LiveAssetReaderInitResourceConsumerError, new object[]
				{
					ex.ToString()
				});
			}
			finally
			{
				LiveAssetReader.initResourceConsumerCompleteEvent.Set();
				LiveAssetReader.isInitializing = false;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000078B8 File Offset: 0x00005AB8
		private void Initialize()
		{
			if (LiveAssetReader.isInitialized)
			{
				return;
			}
			bool flag = false;
			try
			{
				flag = Monitor.TryEnter(LiveAssetReader.syncRoot, 30000);
				if (flag && !LiveAssetReader.isInitializing && !LiveAssetReader.isInitialized)
				{
					LiveAssetReader.initResourceConsumerCompleteEvent.Reset();
					LiveAssetReader.isInitializing = true;
					HttpApplication applicationInstance = HttpContext.Current.ApplicationInstance;
					LiveAssetReader.configPath = HttpRuntime.AppDomainAppPath + "LiveHeaderConfig.xml";
					ThreadPool.QueueUserWorkItem(new WaitCallback(LiveAssetReader.InitResourceConsumer), applicationInstance);
					LiveAssetReader.initResourceConsumerCompleteEvent.WaitOne(30000);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(LiveAssetReader.syncRoot);
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000796C File Offset: 0x00005B6C
		private void InitializeEnvironmentQualifier()
		{
			bool flag = false;
			try
			{
				string text = ConfigurationManager.AppSettings["BrandConfigDomain"];
				if (!string.IsNullOrEmpty(text))
				{
					Uri uri = new Uri("http://" + text);
					if (uri.Host.ToLower().EndsWith("live-int.com"))
					{
						flag = true;
					}
				}
			}
			catch (Exception)
			{
			}
			this.environmentQualifier = (flag ? "-int" : string.Empty);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000079E8 File Offset: 0x00005BE8
		private string GetDurationLogMessage(string taskName, DateTime start, DateTime end)
		{
			string format = "&{0}{1}={2}&";
			string text = "S";
			string result;
			if (start != DateTime.MinValue)
			{
				if (end == DateTime.MinValue)
				{
					end = DateTime.UtcNow;
					text = "F";
				}
				TimeSpan timeSpan = new TimeSpan(end.Ticks - start.Ticks);
				result = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					taskName,
					text,
					timeSpan.TotalMilliseconds
				});
			}
			else
			{
				text = "F";
				result = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					taskName,
					text,
					-1
				});
			}
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007AA4 File Offset: 0x00005CA4
		public override string GetBrandVersion(CultureInfo cultureInfo)
		{
			string brandResourceUrlString = this.GetBrandResourceUrlString();
			if (string.IsNullOrEmpty(brandResourceUrlString))
			{
				return string.Empty;
			}
			if (this.IsPreviewBrand())
			{
				return "preview";
			}
			string locale = this.GetLocale(cultureInfo);
			if (string.IsNullOrEmpty(locale))
			{
				return string.Empty;
			}
			int num = brandResourceUrlString.LastIndexOf(locale, StringComparison.OrdinalIgnoreCase) - 1;
			if (num < 0)
			{
				return string.Empty;
			}
			int num2 = brandResourceUrlString.LastIndexOf('/', num - 1) + 1;
			return brandResourceUrlString.Substring(num2, num - num2);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007B17 File Offset: 0x00005D17
		public override string GetBrandResourceUrlString()
		{
			return this.GetString("Cobrand.SecureResourceUrl.Path");
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007B24 File Offset: 0x00005D24
		public override string GetLocale(CultureInfo culture)
		{
			if (this.brandControl == null)
			{
				return null;
			}
			return this.brandControl.LocaleToUse(culture.Name);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007B41 File Offset: 0x00005D41
		public override string GetThemeThumbnailUrl()
		{
			return base.GetBrandImageFileUrl(CobrandingAssetKey.OrganizationLogoPath);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007B4A File Offset: 0x00005D4A
		public override string GetThemeTitle()
		{
			return this.GetString(CobrandingAssetKey.OrganizationName);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007B53 File Offset: 0x00005D53
		public override bool ShouldEnableCustomTheme
		{
			get
			{
				return this.GetString(CobrandingAssetKey.EnableCustomTheme) == "1";
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00007B66 File Offset: 0x00005D66
		public bool IsPropertySet(LiveAssetKey property)
		{
			return this.GetString(property) == "1";
		}

		// Token: 0x0400024D RID: 589
		public const string MailLinkTextPlaceholder = "Mail_Link_Text_Placeholder";

		// Token: 0x0400024E RID: 590
		private const string PreviewBrandVersion = "preview";

		// Token: 0x0400024F RID: 591
		private const string BrandConfigDomain = "BrandConfigDomain";

		// Token: 0x04000250 RID: 592
		private const int InitWaitTime = 30000;

		// Token: 0x04000251 RID: 593
		private static readonly object syncRoot = new object();

		// Token: 0x04000252 RID: 594
		private static readonly ManualResetEvent initResourceConsumerCompleteEvent = new ManualResetEvent(false);

		// Token: 0x04000253 RID: 595
		private static volatile bool isInitialized;

		// Token: 0x04000254 RID: 596
		private static volatile bool isInitializing;

		// Token: 0x04000255 RID: 597
		private static string configPath;

		// Token: 0x04000256 RID: 598
		private static StringDictionary liveAssetToUrlDictionary = new StringDictionary
		{
			{
				"Live.Shared.GlobalSettings.Header.Tabs.Documents.Href",
				"https://office.live{0}.com/?mkt={1}"
			},
			{
				"Live.Shared.GlobalSettings.Header.Tabs.More.YourDocs.Href",
				"https://office.live{0}.com/documents.aspx?mkt={1}"
			},
			{
				"Live.Shared.GlobalSettings.Header.Tabs.More.Groups.Href",
				"http://groups.live{0}.com/?mkt={1}"
			},
			{
				"Live.Shared.GlobalSettings.Header.Tabs.NewWord.Href",
				"https://office.live{0}.com/newlivedocument.aspx?xt=docx&mkt={1}"
			},
			{
				"Live.Shared.GlobalSettings.Header.Tabs.NewExcel.Href",
				"https://office.live{0}.com/newlivedocument.aspx?xt=xlsx&mkt={1}"
			},
			{
				"Live.Shared.GlobalSettings.Header.Tabs.NewPowerPoint.Href",
				"https://office.live{0}.com/newlivedocument.aspx?xt=pptx&mkt={1}"
			},
			{
				"Live.Shared.GlobalSettings.Header.Tabs.NewOneNote.Href",
				"https://office.live{0}.com/newlivedocument.aspx?xt=one&mkt={1}"
			},
			{
				"Live.Shared.GlobalSettings.Header.Tabs.Photos.Href",
				"https://photos.live{0}.com/?mkt={1}"
			},
			{
				"Live.Shared.GlobalSettings.Header.YourAlbums.Href",
				"https://photos.live{0}.com/albums.aspx?mkt={1}"
			},
			{
				"Live.Shared.GlobalSettings.Header.Tabs.YourPhotos.Href",
				"https://photos.live{0}.com/peopletags.aspx?mkt={1}"
			},
			{
				"Live.Shared.GlobalSettings.Header.Tabs.SharePhoto.Href",
				"https://photos.live{0}.com/choosefolder.aspx?mkt={1}"
			}
		};

		// Token: 0x04000257 RID: 599
		private IAleStringsAgent aleStringsAgent;

		// Token: 0x04000258 RID: 600
		private BrandControl brandControl;

		// Token: 0x04000259 RID: 601
		private string environmentQualifier;
	}
}
