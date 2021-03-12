using System;
using System.Collections.Generic;
using Microsoft.Exchange.SharePointSignalStore;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200034F RID: 847
	internal class LinkClickedSignalSource : IAnalyticsSignalSource
	{
		// Token: 0x06001BAF RID: 7087 RVA: 0x0006A5F8 File Offset: 0x000687F8
		public LinkClickedSignalSource(string sender, string url, string title, string desc, string imgurl, string imgdimensions, List<string> recipients)
		{
			this.sender = sender;
			this.url = url;
			this.title = string.Empty;
			this.desc = string.Empty;
			this.imgurl = string.Empty;
			this.imgwidth = string.Empty;
			this.imgheight = string.Empty;
			if (string.IsNullOrEmpty(sender))
			{
				throw new ArgumentException("Sender email address can not be null or empty.");
			}
			if (string.IsNullOrEmpty(url))
			{
				throw new ArgumentException("Url can not be null or empty.");
			}
			if (recipients == null || recipients.Count == 0)
			{
				throw new ArgumentException("Recipients can not be null or empty.");
			}
			if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(desc))
			{
				this.title = title;
				this.desc = desc;
				if (!string.IsNullOrEmpty(imgurl) && !string.IsNullOrEmpty(imgdimensions) && !imgdimensions.Equals("0x0", StringComparison.InvariantCulture))
				{
					this.imgurl = imgurl;
					string[] array = imgdimensions.Split(new char[]
					{
						'x'
					});
					this.imgwidth = array[0];
					this.imgheight = array[1];
				}
			}
			this.recipients = string.Join(";", recipients);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0006A710 File Offset: 0x00068910
		public IEnumerable<AnalyticsSignal> GetSignals()
		{
			Dictionary<string, string> properties = new Dictionary<string, string>
			{
				{
					"SharedBy",
					this.sender
				},
				{
					"Recipients",
					this.recipients
				},
				{
					"Context",
					"Click data from OWA"
				}
			};
			Dictionary<string, string> properties2 = new Dictionary<string, string>
			{
				{
					"Title",
					this.title
				},
				{
					"Description",
					this.desc
				},
				{
					"ImageUrl",
					this.imgurl
				},
				{
					"ImageWidth",
					this.imgwidth
				},
				{
					"ImageHeight",
					this.imgheight
				}
			};
			AnalyticsSignal.AnalyticsActor actor = new AnalyticsSignal.AnalyticsActor
			{
				Id = null,
				Properties = SharePointSignalRestDataProvider.CreateSignalProperties(null)
			};
			AnalyticsSignal.AnalyticsAction action = new AnalyticsSignal.AnalyticsAction
			{
				ActionType = "SharedAndClicked",
				Properties = SharePointSignalRestDataProvider.CreateSignalProperties(properties)
			};
			AnalyticsSignal.AnalyticsItem item = new AnalyticsSignal.AnalyticsItem
			{
				Id = this.url,
				Properties = SharePointSignalRestDataProvider.CreateSignalProperties(properties2)
			};
			AnalyticsSignal item2 = new AnalyticsSignal
			{
				Actor = actor,
				Action = action,
				Item = item,
				Source = this.GetSourceName()
			};
			return new List<AnalyticsSignal>
			{
				item2
			};
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0006A86A File Offset: 0x00068A6A
		public string GetSourceName()
		{
			return "OWA";
		}

		// Token: 0x04000FA3 RID: 4003
		private readonly string sender;

		// Token: 0x04000FA4 RID: 4004
		private readonly string url;

		// Token: 0x04000FA5 RID: 4005
		private readonly string title;

		// Token: 0x04000FA6 RID: 4006
		private readonly string desc;

		// Token: 0x04000FA7 RID: 4007
		private readonly string imgurl;

		// Token: 0x04000FA8 RID: 4008
		private readonly string imgwidth;

		// Token: 0x04000FA9 RID: 4009
		private readonly string imgheight;

		// Token: 0x04000FAA RID: 4010
		private readonly string recipients;
	}
}
