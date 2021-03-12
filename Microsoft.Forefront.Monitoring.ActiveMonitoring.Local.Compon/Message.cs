using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000232 RID: 562
	public class Message
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x0003679A File Offset: 0x0003499A
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x000367A2 File Offset: 0x000349A2
		internal bool ExpectDelivery { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x000367AB File Offset: 0x000349AB
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x000367B3 File Offset: 0x000349B3
		internal string MessageId { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x000367BC File Offset: 0x000349BC
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x000367C4 File Offset: 0x000349C4
		internal string HeaderTag { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x000367CD File Offset: 0x000349CD
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x000367D5 File Offset: 0x000349D5
		internal string HeaderValue { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x000367DE File Offset: 0x000349DE
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x000367E6 File Offset: 0x000349E6
		internal string Subject { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x000367EF File Offset: 0x000349EF
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x000367F7 File Offset: 0x000349F7
		internal string Body { get; set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00036800 File Offset: 0x00034A00
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x00036808 File Offset: 0x00034A08
		internal bool UseSubjectVerbatim { get; set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00036811 File Offset: 0x00034A11
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x00036819 File Offset: 0x00034A19
		internal string SubjectOverride { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00036822 File Offset: 0x00034A22
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x0003682A File Offset: 0x00034A2A
		internal List<NameValuePair> Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00036833 File Offset: 0x00034A33
		// (set) Token: 0x0600129F RID: 4767 RVA: 0x0003683B File Offset: 0x00034A3B
		internal Dictionary<string, object> Attachments
		{
			get
			{
				return this.attachments;
			}
			set
			{
				this.attachments = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00036844 File Offset: 0x00034A44
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x0003684C File Offset: 0x00034A4C
		internal MailMessage Mail
		{
			get
			{
				return this.mail;
			}
			set
			{
				this.mail = value;
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00036858 File Offset: 0x00034A58
		public static Message FromXml(int workDefinitionId, XmlDocument workContext, ProbeDefinition probeDefinition, DelTraceDebug traceDebug)
		{
			XmlElement xmlElement = Utils.CheckXmlElement(workContext.SelectSingleNode("//Message"), "Message");
			Message message = new Message();
			message.SubjectOverride = Utils.GetOptionalXmlAttribute<string>(xmlElement, "SubjectOverride", string.Format("{0:0000000000}", workDefinitionId));
			message.MessageId = string.Format("{0}-{1:0000000000}{2:0000000000}", message.SubjectOverride, Message.rnd.Next(), Math.Abs(Message.rnd.Next() - workDefinitionId));
			message.ExpectDelivery = Utils.GetBoolean(xmlElement.GetAttribute("ExpectDelivery"), "ExpectDelivery", true);
			foreach (object obj in xmlElement.SelectNodes("Header"))
			{
				XmlElement xmlElement2 = (XmlElement)obj;
				string attribute = xmlElement2.GetAttribute("Tag");
				if (!string.IsNullOrWhiteSpace(attribute))
				{
					string attribute2 = xmlElement2.GetAttribute("Value");
					message.Headers.Add(new NameValuePair(attribute, Message.CheckMalware(attribute2)));
				}
			}
			if (Utils.GetOptionalXmlAttribute<bool>(xmlElement, "IncludeProbeIdHeader", false))
			{
				int num = new Random().Next();
				Guid guid = Guid.Parse(string.Format("{0:X8}-{1:X4}-{2:X4}-{3:X4}-{4:X12}", new object[]
				{
					workDefinitionId,
					0,
					0,
					0,
					num
				}));
				string name;
				string value;
				try
				{
					name = "X-FFOSystemProbe";
					value = SystemProbeId.EncryptProbeGuid(guid, DateTime.UtcNow);
					if (traceDebug != null)
					{
						traceDebug("Guid Encrypted", new object[0]);
					}
				}
				catch (SystemProbeException)
				{
					if (traceDebug != null)
					{
						traceDebug("Probe guid encryption cert was not found", new object[0]);
					}
					name = "X-LAMNotificationId";
					value = guid.ToString();
				}
				message.headers.Add(new NameValuePair(name, value));
			}
			message.Headers.Add(new NameValuePair("X-MS-Exchange-ActiveMonitoringProbeName", (probeDefinition == null || string.IsNullOrEmpty(probeDefinition.Name)) ? "Unknown" : probeDefinition.Name));
			string attribute3 = xmlElement.GetAttribute("HeaderTag");
			if (!string.IsNullOrWhiteSpace(attribute3))
			{
				string attribute4 = xmlElement.GetAttribute("HeaderValue");
				message.Headers.Add(new NameValuePair(attribute3, Message.CheckMalware(attribute4)));
			}
			string text = xmlElement.GetAttribute("Subject");
			text = ((text == null) ? text : text.Trim());
			message.UseSubjectVerbatim = Utils.GetOptionalXmlAttribute<bool>(xmlElement, "UseSubjectVerbatim", false);
			if (!message.UseSubjectVerbatim)
			{
				if (!string.IsNullOrEmpty(text))
				{
					message.Subject = string.Format("{0} {1}", message.MessageId, Message.CheckMalware(text));
				}
				else
				{
					message.Subject = message.MessageId;
				}
			}
			else
			{
				message.Subject = text;
				message.Headers.Add(new NameValuePair(Message.ProbeMessageIDHeaderTag, message.MessageId));
			}
			string text2 = xmlElement.GetAttribute("Body");
			text2 = ((text2 == null) ? text2 : text2.Trim());
			message.Body = Message.CheckMalware(text2);
			foreach (object obj2 in xmlElement.SelectNodes("Attachment"))
			{
				XmlElement xmlElement3 = (XmlElement)obj2;
				string attribute5 = xmlElement3.GetAttribute("Filename");
				if (!string.IsNullOrWhiteSpace(attribute5))
				{
					message.Attachments.Add(attribute5, Message.GetAttachmentObject(attribute5));
				}
			}
			return message;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00036C0C File Offset: 0x00034E0C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (NameValuePair nameValuePair in this.Headers)
			{
				stringBuilder.AppendFormat("Header:Name='{0}',Value='{1}' ", nameValuePair.Name, nameValuePair.Value);
			}
			stringBuilder.AppendFormat("Subject:'{0}' ", this.Subject);
			stringBuilder.AppendFormat("Body:'{0}' ", this.Body);
			foreach (KeyValuePair<string, object> keyValuePair in this.Attachments)
			{
				stringBuilder.AppendFormat("Attachment:'{0}' ", keyValuePair.Key);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00036CF4 File Offset: 0x00034EF4
		internal static object GetAttachmentObject(string filename)
		{
			if (string.IsNullOrWhiteSpace(filename))
			{
				return filename;
			}
			if (string.Compare(filename, "EICAR", true) == 0)
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] bytes = Encoding.UTF8.GetBytes(Message.GetEicar());
				memoryStream.Write(bytes, 0, bytes.Length);
				return memoryStream;
			}
			if (string.Compare(filename, "GTUBE", true) == 0)
			{
				MemoryStream memoryStream2 = new MemoryStream();
				byte[] bytes2 = Encoding.UTF8.GetBytes(Message.GetGtube());
				memoryStream2.Write(bytes2, 0, bytes2.Length);
				return memoryStream2;
			}
			if (File.Exists(filename))
			{
				return filename;
			}
			string text = Path.Combine(".", filename);
			if (File.Exists(text))
			{
				return text;
			}
			text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
			if (File.Exists(text))
			{
				return text;
			}
			return filename;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00036DB4 File Offset: 0x00034FB4
		internal static Attachment CreateMailAttachment(KeyValuePair<string, object> entry)
		{
			if (entry.Value is MemoryStream)
			{
				MemoryStream memoryStream = (MemoryStream)entry.Value;
				memoryStream.Seek(0L, SeekOrigin.Begin);
				return new Attachment(memoryStream, entry.Key);
			}
			return new Attachment((string)entry.Value);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00036E08 File Offset: 0x00035008
		internal void CleanupAttachment()
		{
			foreach (Attachment attachment in this.Mail.Attachments)
			{
				attachment.Dispose();
			}
			this.Mail.Dispose();
			foreach (KeyValuePair<string, object> keyValuePair in this.Attachments)
			{
				if (keyValuePair.Value is MemoryStream)
				{
					((MemoryStream)keyValuePair.Value).Dispose();
				}
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00036EC0 File Offset: 0x000350C0
		private static string CheckMalware(string content)
		{
			if (!string.IsNullOrWhiteSpace(content))
			{
				if (string.Compare(content, "EICAR", true) == 0 || content.Contains("$EICAR$"))
				{
					return content.Replace("EICAR", Message.GetEicar());
				}
				if (string.Compare(content, "GTUBE", true) == 0 || content.Contains("$GTUBE$"))
				{
					return content.Replace("GTUBE", Message.GetGtube());
				}
			}
			return content;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00036F2E File Offset: 0x0003512E
		private static string GetEicar()
		{
			return "ANTI\\VIRUSFILE!$H+H*".Replace("ANTI", "X5O!P%@AP[4").Replace("VIRUS", "PZX54(P^)7CC)7}$EICAR-STANDARD-ANTIVIRUS-TEST-");
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00036F53 File Offset: 0x00035153
		private static string GetGtube()
		{
			return "XJS*ANTISPAM".Replace("ANTI", "C4JDBQADN1.NSBN3*2IDNEN*GTUBE-STANDARD").Replace("SPAM", "-ANTI-UBE-TEST-EMAIL*C.34X");
		}

		// Token: 0x0400089D RID: 2205
		private const string ScrambledEicar = "ANTI\\VIRUSFILE!$H+H*";

		// Token: 0x0400089E RID: 2206
		private const string ScrambledGtube = "XJS*ANTISPAM";

		// Token: 0x0400089F RID: 2207
		internal static readonly string ProbeMessageIDHeaderTag = "SMTP-Probe-Message-ID";

		// Token: 0x040008A0 RID: 2208
		private static Random rnd = new Random();

		// Token: 0x040008A1 RID: 2209
		private List<NameValuePair> headers = new List<NameValuePair>();

		// Token: 0x040008A2 RID: 2210
		private MailMessage mail = new MailMessage();

		// Token: 0x040008A3 RID: 2211
		private Dictionary<string, object> attachments = new Dictionary<string, object>();
	}
}
