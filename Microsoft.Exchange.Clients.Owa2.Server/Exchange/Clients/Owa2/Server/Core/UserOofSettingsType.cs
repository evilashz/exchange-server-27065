using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001EA RID: 490
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UserOofSettingsType
	{
		// Token: 0x0600113E RID: 4414 RVA: 0x0004218C File Offset: 0x0004038C
		public UserOofSettingsType()
		{
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00042194 File Offset: 0x00040394
		public UserOofSettingsType(UserOofSettings userOofSettings, ExTimeZone timeZone)
		{
			this.ExternalAudience = userOofSettings.ExternalAudience;
			this.ExternalReply = UserOofSettingsType.ConvertHtmlToText(userOofSettings.ExternalReply.Message);
			this.InternalReply = UserOofSettingsType.ConvertHtmlToText(userOofSettings.InternalReply.Message);
			this.IsScheduled = (userOofSettings.OofState == OofState.Scheduled);
			if (userOofSettings.OofState == OofState.Enabled || (userOofSettings.OofState == OofState.Scheduled && DateTime.UtcNow >= userOofSettings.Duration.StartTime && DateTime.UtcNow <= userOofSettings.Duration.EndTime))
			{
				this.IsOofOn = true;
			}
			else
			{
				this.IsOofOn = false;
			}
			ExDateTime exDateTime = new ExDateTime(timeZone, userOofSettings.Duration.EndTime);
			this.EndTime = exDateTime.ToISOString();
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0004225E File Offset: 0x0004045E
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x00042266 File Offset: 0x00040466
		[DataMember(Order = 1)]
		public ExternalAudience ExternalAudience { get; set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0004226F File Offset: 0x0004046F
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x00042277 File Offset: 0x00040477
		[DataMember(Order = 2)]
		public string ExternalReply { get; set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00042280 File Offset: 0x00040480
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x00042288 File Offset: 0x00040488
		[DataMember(Order = 3)]
		public string InternalReply { get; set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00042291 File Offset: 0x00040491
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x00042299 File Offset: 0x00040499
		[DataMember(Order = 4)]
		public bool IsOofOn { get; set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x000422A2 File Offset: 0x000404A2
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x000422AA File Offset: 0x000404AA
		[DataMember(Order = 5)]
		public bool IsScheduled { get; set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x000422B3 File Offset: 0x000404B3
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x000422BB File Offset: 0x000404BB
		[DataMember(Order = 6)]
		[DateTimeString]
		public string EndTime { get; set; }

		// Token: 0x0600114C RID: 4428 RVA: 0x000422C4 File Offset: 0x000404C4
		internal static string ConvertTextToHtml(string text)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			string @string;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					try
					{
						new TextToHtml
						{
							InputEncoding = Encoding.UTF8,
							OutputEncoding = Encoding.UTF8,
							HtmlTagCallback = new HtmlTagCallback(UserOofSettingsType.RemoveLinkCallback),
							Header = null,
							Footer = null,
							OutputHtmlFragment = true
						}.Convert(memoryStream, memoryStream2);
					}
					catch (InvalidCharsetException innerException)
					{
						throw new OwaException("Convert to Html Failed", innerException);
					}
					catch (TextConvertersException innerException2)
					{
						throw new OwaException("Convert to Html Failed", innerException2);
					}
					@string = Encoding.UTF8.GetString(memoryStream2.ToArray());
				}
			}
			return @string;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x000423C0 File Offset: 0x000405C0
		private static string ConvertHtmlToText(string html)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(html);
			string @string;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					try
					{
						new HtmlToText
						{
							InputEncoding = Encoding.UTF8,
							OutputEncoding = Encoding.UTF8
						}.Convert(memoryStream, memoryStream2);
					}
					catch (InvalidCharsetException innerException)
					{
						throw new OwaException("Convert to Text Failed", innerException);
					}
					catch (TextConvertersException innerException2)
					{
						throw new OwaException("Convert to Text Failed", innerException2);
					}
					@string = Encoding.UTF8.GetString(memoryStream2.ToArray());
				}
			}
			return @string;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00042494 File Offset: 0x00040694
		private static void RemoveLinkCallback(HtmlTagContext tagContext, HtmlWriter htmlWriter)
		{
			if (tagContext.TagId == HtmlTagId.A)
			{
				tagContext.DeleteTag();
				return;
			}
			tagContext.WriteTag();
			foreach (HtmlTagContextAttribute htmlTagContextAttribute in tagContext.Attributes)
			{
				htmlTagContextAttribute.Write();
			}
		}
	}
}
