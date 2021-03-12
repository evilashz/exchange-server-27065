using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000239 RID: 569
	public class Pop3Message
	{
		// Token: 0x060012DF RID: 4831 RVA: 0x00037655 File Offset: 0x00035855
		public Pop3Message()
		{
			this.BodyComponents = new List<string>();
			this.bodyParsed = false;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00037688 File Offset: 0x00035888
		public Pop3Message(List<string> message)
		{
			List<string> headerComponents;
			List<string> bodyComponents;
			this.SplitMimeHeaderAndBody(message, out headerComponents, out bodyComponents);
			this.HeaderComponents = headerComponents;
			this.BodyComponents = bodyComponents;
			this.bodyParsed = false;
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000376D1 File Offset: 0x000358D1
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x000376D9 File Offset: 0x000358D9
		public long Number
		{
			get
			{
				return this.number;
			}
			set
			{
				this.number = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x000376E2 File Offset: 0x000358E2
		// (set) Token: 0x060012E4 RID: 4836 RVA: 0x000376EA File Offset: 0x000358EA
		public long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x000376F3 File Offset: 0x000358F3
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x0003772B File Offset: 0x0003592B
		public string Message
		{
			get
			{
				if (string.IsNullOrEmpty(this.message) && this.Components != null)
				{
					this.message = string.Join(string.Empty, this.Components.ToArray());
				}
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00037734 File Offset: 0x00035934
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x0003773C File Offset: 0x0003593C
		public List<string> Components
		{
			get
			{
				return this.components;
			}
			set
			{
				this.components = value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00037745 File Offset: 0x00035945
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x0003777D File Offset: 0x0003597D
		public string MessageHeader
		{
			get
			{
				if (string.IsNullOrEmpty(this.messageHeader) && this.HeaderComponents != null)
				{
					this.messageHeader = string.Join(string.Empty, this.HeaderComponents.ToArray());
				}
				return this.messageHeader;
			}
			set
			{
				this.messageHeader = value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00037786 File Offset: 0x00035986
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x0003778E File Offset: 0x0003598E
		public List<string> HeaderComponents { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00037797 File Offset: 0x00035997
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x0003779F File Offset: 0x0003599F
		public List<string> BodyComponents { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x000377A8 File Offset: 0x000359A8
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x000377CC File Offset: 0x000359CC
		public string BodyText
		{
			get
			{
				if (!this.bodyParsed && this.BodyComponents != null)
				{
					this.bodyParsed = this.TryParseBody();
				}
				return this.bodyText;
			}
			set
			{
				this.bodyText = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x000377D5 File Offset: 0x000359D5
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x000377F9 File Offset: 0x000359F9
		public List<Pop3Attachment> Attachments
		{
			get
			{
				if (!this.bodyParsed && this.BodyComponents != null)
				{
					this.bodyParsed = this.TryParseBody();
				}
				return this.attachments;
			}
			set
			{
				this.attachments = value;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00037802 File Offset: 0x00035A02
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x0003780A File Offset: 0x00035A0A
		public int AttachmentCount { get; set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00037814 File Offset: 0x00035A14
		public DateTime ReceivedDate
		{
			get
			{
				if (this.receivedDate == DateTime.MinValue)
				{
					string value = this.GetValue("Received:");
					if (!string.IsNullOrEmpty(value))
					{
						string pattern = "(\\w\\W)*[a-zA-Z]{3},[\\s]+[0-9]{1,2}[\\s]+[a-zA-Z]{3}[\\s]+[0-9]{4}[\\s]+[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}[\\s]+[+-]{1}[0-9]{4}(\\w\\W)*";
						Match match = Regex.Match(value, pattern);
						if (match.Success)
						{
							this.receivedDate = Convert.ToDateTime(match.ToString()).ToUniversalTime();
						}
					}
				}
				return this.receivedDate;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0003787C File Offset: 0x00035A7C
		public string From
		{
			get
			{
				if (string.IsNullOrEmpty(this.from))
				{
					this.from = this.GetValue("From:");
				}
				return this.from;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x000378A2 File Offset: 0x00035AA2
		public string To
		{
			get
			{
				if (string.IsNullOrEmpty(this.to))
				{
					this.to = this.GetValue("To:");
				}
				return this.to;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x000378C8 File Offset: 0x00035AC8
		public DateTime Date
		{
			get
			{
				if (this.date == DateTime.MinValue)
				{
					this.date = Convert.ToDateTime(this.GetValue("Date:")).ToUniversalTime();
				}
				return this.date;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0003790B File Offset: 0x00035B0B
		public string Subject
		{
			get
			{
				if (string.IsNullOrEmpty(this.subject))
				{
					this.subject = this.GetValue("Subject:");
				}
				return this.subject;
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00037931 File Offset: 0x00035B31
		private string GetValue(string param)
		{
			if (string.IsNullOrEmpty(param) || !Regex.Match(param, "^(Received:|From:|To:|Date:|Subject:|Content-Type:|FFO-ActiveMonitoring:|Message-ID:|MIME-Version:|X-FOPE-CONNECTOR:|X-IPFilteringStamp:|Return-Path:|X-SpamScore:|X-BigFish:|X-Spam-TCS-SCL:|X-Forefront-Antispam-Report:|X-MS-Exchange-Organization-SCL:|X-MS-Exchange-Organization-AVStamp-Mailbox:|X-MS-Exchange-Organization-AuthSource:|X-MS-Exchange-Organization-AuthAs:|Auto-Submitted:|X-MS-Exchange-Message-Is-Ndr:|X-MS-Exchange-Organization-AuthMechanism:|X-MS-Exchange-Organization-Network-Message-Id:|X-MS-Exchange-Organization-AVStamp-Service:|X-Forefront-Antispam-Report:|X-MS-Exchange-Organization-MessageDirectionality:|Thread-Topic:|Thread-Index:|Content-Language:|X-MS-Has-Attach:|X-MS-TNEF-Correlator:|received-spf:|Content-ID:)").Success)
			{
				return null;
			}
			return this.GetHeaderValue(param, this.HeaderComponents);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0003795C File Offset: 0x00035B5C
		private bool TryParseBody()
		{
			bool result;
			try
			{
				List<Pop3BodyText> list = new List<Pop3BodyText>();
				List<Pop3Attachment> list2 = new List<Pop3Attachment>();
				this.ExtractBodyTextsAndAttachments(this.HeaderComponents, this.BodyComponents, list, list2);
				if (list.Count > 0)
				{
					this.bodyText = this.FindBestBodyText(list);
				}
				this.attachments = list2;
				this.AttachmentCount = list2.Count;
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000379CC File Offset: 0x00035BCC
		private string FindBestBodyText(List<Pop3BodyText> bodyTexts)
		{
			if (bodyTexts == null || bodyTexts.Count == 0)
			{
				return null;
			}
			if (bodyTexts.Count == 1)
			{
				return bodyTexts[0].Text;
			}
			string[] array = new string[]
			{
				"text/plain",
				"text/html",
				"text/"
			};
			foreach (string value in array)
			{
				foreach (Pop3BodyText pop3BodyText in bodyTexts)
				{
					if (!string.IsNullOrEmpty(pop3BodyText.ContentType) && pop3BodyText.ContentType.StartsWith(value))
					{
						return pop3BodyText.Text;
					}
				}
			}
			foreach (Pop3BodyText pop3BodyText2 in bodyTexts)
			{
				if (!string.IsNullOrWhiteSpace(pop3BodyText2.Text))
				{
					return pop3BodyText2.Text;
				}
			}
			return string.Empty;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00037AF4 File Offset: 0x00035CF4
		private void ExtractBodyTextsAndAttachments(List<string> headerComponents, List<string> bodyComponents, List<Pop3BodyText> bodyTexts, List<Pop3Attachment> attachments)
		{
			ContentType contentType = this.GetContentType(headerComponents);
			if (contentType.MediaType.StartsWith("multipart/") && !string.IsNullOrEmpty(contentType.Boundary))
			{
				List<List<string>> list = this.SplitMimePartsByBoundary(bodyComponents, contentType.Boundary);
				using (List<List<string>>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						List<string> mimePart = enumerator.Current;
						this.SplitMimeHeaderAndBody(mimePart, out headerComponents, out bodyComponents);
						this.ExtractBodyTextsAndAttachments(headerComponents, bodyComponents, bodyTexts, attachments);
					}
					return;
				}
			}
			string attachmentName = this.GetAttachmentName(headerComponents);
			string headerValue = this.GetHeaderValue("Content-Transfer-Encoding:", headerComponents);
			if (string.IsNullOrEmpty(attachmentName) && !string.IsNullOrEmpty(contentType.CharSet))
			{
				string text = this.ConvertBodyComponentsToString(bodyComponents, headerValue, contentType);
				bodyTexts.Add(new Pop3BodyText(contentType.MediaType, text));
				return;
			}
			byte[] content = this.ConvertBodyComponentsToAttachmentContent(bodyComponents, headerValue, contentType);
			attachments.Add(new Pop3Attachment(contentType.MediaType, attachmentName, content));
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00037BF4 File Offset: 0x00035DF4
		private string ConvertBodyComponentsToString(List<string> bodyComponents, string contentTransferEncoding, ContentType contentType)
		{
			if (contentTransferEncoding != null)
			{
				if (contentTransferEncoding == "quoted-printable")
				{
					return Encoding.GetEncoding(contentType.CharSet).GetString(this.JoinQuotedPrintable(bodyComponents));
				}
				if (contentTransferEncoding == "base64")
				{
					byte[] bytes = Convert.FromBase64String(string.Join(string.Empty, bodyComponents));
					return Encoding.GetEncoding(contentType.CharSet).GetString(bytes);
				}
				if (contentTransferEncoding == "7bit")
				{
					return string.Join(Environment.NewLine, bodyComponents);
				}
			}
			throw new NotSupportedException("Unsupported Content-Transfer-Encoding: " + contentTransferEncoding);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00037C88 File Offset: 0x00035E88
		private byte[] ConvertBodyComponentsToAttachmentContent(List<string> bodyComponents, string contentTransferEncoding, ContentType contentType)
		{
			if (contentTransferEncoding != null)
			{
				if (contentTransferEncoding == "base64")
				{
					return Convert.FromBase64String(string.Join(string.Empty, bodyComponents));
				}
				if (contentTransferEncoding == "quoted-printable")
				{
					return this.JoinQuotedPrintable(bodyComponents);
				}
				if (contentTransferEncoding == "7bit")
				{
					return Encoding.ASCII.GetBytes(string.Join(Environment.NewLine, bodyComponents));
				}
			}
			throw new NotSupportedException("Unsupported Content-Transfer-Encoding: " + contentTransferEncoding);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00037D3C File Offset: 0x00035F3C
		private byte[] JoinQuotedPrintable(List<string> bodyComponents)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in bodyComponents)
			{
				string text2 = Regex.Replace(text, "=(?<hex>[0-9a-fA-F]{2})", delegate(Match m)
				{
					string value = m.Groups["hex"].Value;
					return Convert.ToChar(Convert.ToInt32(value, 16)).ToString();
				});
				if (text.EndsWith("="))
				{
					text2 = text2.Remove(text2.Length - 1);
					stringBuilder.Append(text2);
				}
				else
				{
					stringBuilder.Append(text2);
					stringBuilder.AppendLine();
				}
			}
			return Encoding.GetEncoding("iso-8859-1").GetBytes(stringBuilder.ToString());
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00037DFC File Offset: 0x00035FFC
		private ContentType GetContentType(List<string> headerComponents)
		{
			string headerValue = this.GetHeaderValue("Content-Type:", headerComponents);
			if (string.IsNullOrEmpty(headerValue))
			{
				return new ContentType();
			}
			return new ContentType(headerValue);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00037E2C File Offset: 0x0003602C
		private string GetAttachmentName(List<string> headerComponents)
		{
			string headerValue = this.GetHeaderValue("Content-Disposition:", headerComponents);
			if (!string.IsNullOrEmpty(headerValue))
			{
				ContentDisposition contentDisposition = new ContentDisposition(headerValue);
				if (!string.IsNullOrEmpty(contentDisposition.FileName))
				{
					return contentDisposition.FileName;
				}
			}
			string headerValue2 = this.GetHeaderValue("Content-Description:", headerComponents);
			if (!string.IsNullOrEmpty(headerValue2))
			{
				return headerValue2;
			}
			ContentType contentType = this.GetContentType(headerComponents);
			if (contentType != null && !string.IsNullOrEmpty(contentType.Name))
			{
				return contentType.Name;
			}
			return null;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00037EA0 File Offset: 0x000360A0
		private List<List<string>> SplitMimePartsByBoundary(List<string> multipartBodyComponents, string boundary)
		{
			string text = "--" + boundary;
			string b = text + "--";
			List<List<string>> list = new List<List<string>>();
			List<string> list2 = new List<string>();
			bool flag = false;
			foreach (string text2 in multipartBodyComponents)
			{
				if (text2 == text)
				{
					if (flag)
					{
						list.Add(list2);
					}
					list2 = new List<string>();
					flag = true;
				}
				else
				{
					if (text2 == b)
					{
						if (flag)
						{
							list.Add(list2);
						}
						break;
					}
					list2.Add(text2);
				}
			}
			return list;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00037F54 File Offset: 0x00036154
		private void SplitMimeHeaderAndBody(List<string> mimePart, out List<string> headerComponents, out List<string> bodyComponents)
		{
			headerComponents = new List<string>();
			bodyComponents = new List<string>();
			bool flag = true;
			foreach (string text in mimePart)
			{
				if (flag)
				{
					if (text == string.Empty)
					{
						flag = false;
					}
					else
					{
						headerComponents.Add(text);
					}
				}
				else
				{
					bodyComponents.Add(text);
				}
			}
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00037FD4 File Offset: 0x000361D4
		private string GetHeaderValue(string headerName, List<string> headerComponents)
		{
			if (string.IsNullOrEmpty(headerName))
			{
				return null;
			}
			if (!headerName.EndsWith(":"))
			{
				headerName += ':';
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (string text in headerComponents)
			{
				if (text.StartsWith(headerName))
				{
					if (flag)
					{
						break;
					}
					stringBuilder.Append(text);
					flag = true;
				}
				else if (flag)
				{
					if (text.Length <= 0 || !char.IsWhiteSpace(text[0]))
					{
						break;
					}
					stringBuilder.Append(text);
				}
			}
			string text2 = stringBuilder.ToString();
			if (!string.IsNullOrEmpty(text2))
			{
				text2 = text2.Substring(headerName.Length).TrimStart(new char[0]);
			}
			return text2;
		}

		// Token: 0x040008C5 RID: 2245
		private const string HeaderFields = "^(Received:|From:|To:|Date:|Subject:|Content-Type:|FFO-ActiveMonitoring:|Message-ID:|MIME-Version:|X-FOPE-CONNECTOR:|X-IPFilteringStamp:|Return-Path:|X-SpamScore:|X-BigFish:|X-Spam-TCS-SCL:|X-Forefront-Antispam-Report:|X-MS-Exchange-Organization-SCL:|X-MS-Exchange-Organization-AVStamp-Mailbox:|X-MS-Exchange-Organization-AuthSource:|X-MS-Exchange-Organization-AuthAs:|Auto-Submitted:|X-MS-Exchange-Message-Is-Ndr:|X-MS-Exchange-Organization-AuthMechanism:|X-MS-Exchange-Organization-Network-Message-Id:|X-MS-Exchange-Organization-AVStamp-Service:|X-Forefront-Antispam-Report:|X-MS-Exchange-Organization-MessageDirectionality:|Thread-Topic:|Thread-Index:|Content-Language:|X-MS-Has-Attach:|X-MS-TNEF-Correlator:|received-spf:|Content-ID:)";

		// Token: 0x040008C6 RID: 2246
		private long number;

		// Token: 0x040008C7 RID: 2247
		private long size;

		// Token: 0x040008C8 RID: 2248
		private string message;

		// Token: 0x040008C9 RID: 2249
		private List<string> components;

		// Token: 0x040008CA RID: 2250
		private string messageHeader;

		// Token: 0x040008CB RID: 2251
		private string from;

		// Token: 0x040008CC RID: 2252
		private string to;

		// Token: 0x040008CD RID: 2253
		private DateTime date = DateTime.MinValue;

		// Token: 0x040008CE RID: 2254
		private DateTime receivedDate = DateTime.MinValue;

		// Token: 0x040008CF RID: 2255
		private string subject;

		// Token: 0x040008D0 RID: 2256
		private string bodyText;

		// Token: 0x040008D1 RID: 2257
		private List<Pop3Attachment> attachments;

		// Token: 0x040008D2 RID: 2258
		private bool bodyParsed;
	}
}
