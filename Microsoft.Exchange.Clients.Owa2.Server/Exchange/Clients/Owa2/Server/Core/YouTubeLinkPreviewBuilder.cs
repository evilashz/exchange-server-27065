using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002F1 RID: 753
	internal class YouTubeLinkPreviewBuilder : WebPageLinkPreviewBuilder
	{
		// Token: 0x06001960 RID: 6496 RVA: 0x00058729 File Offset: 0x00056929
		public YouTubeLinkPreviewBuilder(Dictionary<string, string> queryParmDictionary, GetLinkPreviewRequest request, string responseString, RequestDetailsLogger logger, Uri responseUri) : base(request, responseString, logger, responseUri, true)
		{
			this.youTubeId = queryParmDictionary["v"];
			this.queryParmDictionary = queryParmDictionary;
			this.autoplay = request.Autoplay;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0005875C File Offset: 0x0005695C
		protected override LinkPreview CreateLinkPreviewInstance()
		{
			return new YouTubeLinkPreview();
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00058764 File Offset: 0x00056964
		protected override void SetAdditionalProperties(LinkPreview linkPreview)
		{
			string format = this.autoplay ? "https://www.youtube.com/embed/{0}?autoplay=1" : "https://www.youtube.com/embed/{0}";
			string value = LinkPreviewBuilder.ConvertToSafeHtml(string.Format(format, this.youTubeId));
			StringBuilder stringBuilder = new StringBuilder(value);
			int startTime = this.GetStartTime();
			if (startTime > 0)
			{
				if (!this.autoplay)
				{
					stringBuilder.Append('?');
				}
				else
				{
					stringBuilder.Append('&');
				}
				stringBuilder.Append("start");
				stringBuilder.Append('=');
				stringBuilder.Append(startTime);
			}
			((YouTubeLinkPreview)linkPreview).PlayerUrl = stringBuilder.ToString();
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000587F4 File Offset: 0x000569F4
		private int GetStartTime()
		{
			int num = 0;
			string text;
			if (!this.queryParmDictionary.TryGetValue("t", out text))
			{
				return 0;
			}
			bool flag = text.EndsWith("m");
			string text2;
			if (flag || text.EndsWith("s"))
			{
				text2 = text.Substring(0, text.Length - 1);
			}
			else
			{
				text2 = text;
			}
			if (text2 == null || text2.Length == 0)
			{
				return 0;
			}
			if (!int.TryParse(text2, out num))
			{
				return 0;
			}
			if (flag)
			{
				num *= 60;
			}
			return num;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0005886B File Offset: 0x00056A6B
		protected override string GetImage(out int imageTagCount)
		{
			imageTagCount = 1;
			return LinkPreviewBuilder.ConvertToSafeHtml(string.Format("http://img.youtube.com/vi/{0}/0.jpg", this.youTubeId));
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00058888 File Offset: 0x00056A88
		internal static bool IsYoutubeUri(Uri uri)
		{
			bool flag = string.Compare(uri.Host, "www.youtube.com", true) != 0 || string.Compare(uri.LocalPath, "/watch", true) != 0;
			bool flag2 = string.Compare(uri.Host, "m.youtube.com", true) != 0 || string.Compare(uri.LocalPath, "/watch", true) != 0;
			return !flag || !flag2;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000588FC File Offset: 0x00056AFC
		internal static bool TryGetYouTubePlayerQueryParms(Uri uri, RequestDetailsLogger logger, out Dictionary<string, string> queryParmDictionary)
		{
			queryParmDictionary = null;
			bool flag = false;
			if (!YouTubeLinkPreviewBuilder.IsYoutubeUri(uri))
			{
				return false;
			}
			string text = uri.Query.TrimStart(new char[]
			{
				'?'
			});
			if (text.Length == 0)
			{
				return false;
			}
			string[] array = text.Split(new char[]
			{
				'&'
			});
			if (array.Length > 3)
			{
				logger.Set(GetLinkPreviewMetadata.YouTubeLinkValidationFailed, 1);
				return false;
			}
			queryParmDictionary = new Dictionary<string, string>(4);
			foreach (string text2 in array)
			{
				string[] array3 = text2.Split(new char[]
				{
					'='
				});
				if (array3.Length == 2)
				{
					string text3 = array3[0].ToLower();
					string a;
					if ((a = text3) != null)
					{
						if (!(a == "v") && !(a == "app") && !(a == "feature"))
						{
							if (!(a == "t"))
							{
								goto IL_EE;
							}
							flag = true;
						}
						queryParmDictionary.Add(text3, array3[1]);
						goto IL_117;
					}
					IL_EE:
					logger.Set(GetLinkPreviewMetadata.YouTubeLinkValidationFailed, 1);
					return false;
				}
				IL_117:;
			}
			if (!queryParmDictionary.ContainsKey("v"))
			{
				logger.Set(GetLinkPreviewMetadata.YouTubeLinkValidationFailed, 1);
				return false;
			}
			if (!flag && uri.Fragment != null && uri.Fragment.Length > 0 && uri.Fragment.StartsWith("#t=", StringComparison.InvariantCultureIgnoreCase))
			{
				string text4 = uri.Fragment.Substring("#t=".Length);
				if (text4.Length > 0)
				{
					queryParmDictionary.Add("t", text4);
				}
			}
			return true;
		}

		// Token: 0x04000DE6 RID: 3558
		private const string YouTubeIdPropertyName = "youtube id";

		// Token: 0x04000DE7 RID: 3559
		public const string YouTubeHost = "www.youtube.com";

		// Token: 0x04000DE8 RID: 3560
		public const string YouTubeMobileHost = "m.youtube.com";

		// Token: 0x04000DE9 RID: 3561
		public const string YouTubeLocalPath = "/watch";

		// Token: 0x04000DEA RID: 3562
		private const char QueryDelimiter = '?';

		// Token: 0x04000DEB RID: 3563
		private const char QueryParmDelimiter = '&';

		// Token: 0x04000DEC RID: 3564
		private const char QueryParmValueDelimiter = '=';

		// Token: 0x04000DED RID: 3565
		private const string YouTubeIdParm = "v";

		// Token: 0x04000DEE RID: 3566
		private const string AppParm = "app";

		// Token: 0x04000DEF RID: 3567
		private const string FeatureParm = "feature";

		// Token: 0x04000DF0 RID: 3568
		private const string StartTimeParm = "t";

		// Token: 0x04000DF1 RID: 3569
		private const string StartTimeParmMinuteSuffix = "m";

		// Token: 0x04000DF2 RID: 3570
		private const string StartTimeParmSecondSuffix = "s";

		// Token: 0x04000DF3 RID: 3571
		private const string YouTubeTimeFragmentStart = "#t=";

		// Token: 0x04000DF4 RID: 3572
		private const string StartTimeEmbeddedParm = "start";

		// Token: 0x04000DF5 RID: 3573
		public const string YouTubeImageUrlFormat = "http://img.youtube.com/vi/{0}/0.jpg";

		// Token: 0x04000DF6 RID: 3574
		public const string YouTubePlayerUrlFormat = "https://www.youtube.com/embed/{0}";

		// Token: 0x04000DF7 RID: 3575
		public const string YouTubePlayerUrlAutoplayFormat = "https://www.youtube.com/embed/{0}?autoplay=1";

		// Token: 0x04000DF8 RID: 3576
		private readonly string youTubeId;

		// Token: 0x04000DF9 RID: 3577
		private readonly Dictionary<string, string> queryParmDictionary;

		// Token: 0x04000DFA RID: 3578
		private readonly bool autoplay;
	}
}
