using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x02000019 RID: 25
	public sealed class QuarantineConnector
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004911 File Offset: 0x00002B11
		public QuarantineConnector(QuarantineFlavor flavor, int retentionDays, bool shouldEncrypt)
		{
			this.Flavor = flavor;
			this.ShouldEncrypt = shouldEncrypt;
			this.RetentionDays = retentionDays;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000492E File Offset: 0x00002B2E
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00004936 File Offset: 0x00002B36
		public QuarantineFlavor Flavor { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000493F File Offset: 0x00002B3F
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00004947 File Offset: 0x00002B47
		public int RetentionDays { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004950 File Offset: 0x00002B50
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00004958 File Offset: 0x00002B58
		public bool ShouldEncrypt { get; private set; }

		// Token: 0x0600009C RID: 156 RVA: 0x00004964 File Offset: 0x00002B64
		public static QuarantineConnector CreateFromMessage(EmailMessage message)
		{
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "CreateFromMessage", new object[0]);
			if (message == null)
			{
				SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Fail, "CreateFromMessage message is null", new object[0]);
				throw new ArgumentNullException("message");
			}
			QuarantineConnector result = QuarantineConnector.CreateFromMessageHeaders(message.MimeDocument.RootPart.Headers);
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "CreateFromMessage: returning Quarantine Connector", new object[0]);
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000049D8 File Offset: 0x00002BD8
		public static QuarantineConnector CreateFromMessageHeaders(HeaderList headers)
		{
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "CreateFromMessageHeaders", new object[0]);
			if (headers == null)
			{
				SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Fail, "CreateFromMessageHeaders message is null", new object[0]);
				throw new ArgumentNullException("headers");
			}
			Header header = headers.FindFirst(QuarantineConnector.XHeaderName);
			if (header == null)
			{
				SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "CreateFromMessageHeaders: header not found", new object[0]);
				return null;
			}
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "CreateFromMessageHeaders: QuaraninteConnector header found: {0}:{1}", new object[]
			{
				header.Name,
				header.Value
			});
			QuarantineConnector result = QuarantineConnector.CreateFromHeader(header);
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "CreateFromMessageHeaders: returning Quarantine Connector", new object[0]);
			return result;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004A90 File Offset: 0x00002C90
		internal static void StripMessage(EmailMessage message)
		{
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "StripMessage", new object[0]);
			message.MimeDocument.RootPart.Headers.RemoveAll(QuarantineConnector.XHeaderName);
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "StripMessage: Stripped QuarantineConnector headers", new object[0]);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public void StampMessage(EmailMessage message)
		{
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "StampMessage", new object[0]);
			if (message == null)
			{
				SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Fail, "StampMessage: message is null", new object[0]);
				throw new ArgumentNullException("message");
			}
			QuarantineConnector quarantineConnector = QuarantineConnector.CreateFromMessage(message);
			if (quarantineConnector != null)
			{
				if (quarantineConnector.Flavor < this.Flavor)
				{
					SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "StampMessage: Existing QuarantineConnector header exists with Higher Priority {0} flavor than this {1} flavor, ignoring this stamp", new object[]
					{
						quarantineConnector.Flavor.ToString(),
						this.Flavor.ToString()
					});
					return;
				}
				SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "StampMessage: Existing QuarantineConnector header exists with Lower Priority {0} flavor than this {1} flavor, replacing with this stamp in message", new object[]
				{
					quarantineConnector.Flavor.ToString(),
					this.Flavor.ToString()
				});
				QuarantineConnector.StripMessage(message);
			}
			Header header = this.ToHeader();
			message.MimeDocument.RootPart.Headers.AppendChild(header);
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "StampMessage: Stamp QuarantineConnector header: {0}:{1}", new object[]
			{
				header.Name,
				header.Value
			});
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004C18 File Offset: 0x00002E18
		internal static QuarantineConnector CreateFromHeader(Header header)
		{
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "CreateFromHeader", new object[0]);
			if (header == null)
			{
				SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Fail, "CreateFromHeader: header is null", new object[0]);
				throw new ArgumentNullException("header");
			}
			SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "CreateFromHeader: Processing header: {0]:{1}", new object[]
			{
				header.Name,
				header.Value
			});
			QuarantineConnector result;
			try
			{
				char[] separator = new char[]
				{
					';'
				};
				string[] array = header.Value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (string text in array)
				{
					char[] separator2 = new char[]
					{
						':'
					};
					string[] array3 = text.Split(separator2, 2);
					if (array3.Length == 2)
					{
						dictionary[array3[0].Trim()] = array3[1].Trim();
					}
				}
				if (!dictionary.ContainsKey("v"))
				{
					throw new ArgumentException("Missing 'v:'");
				}
				if (dictionary["v"] != "2")
				{
					throw new ArgumentException("Unrecognized quarantine x-header version - 'v:' was not 2");
				}
				if (!dictionary.ContainsKey("f"))
				{
					throw new ArgumentException("Quarantine header missing 'f:'");
				}
				QuarantineFlavor flavor;
				if (!Enum.TryParse<QuarantineFlavor>(dictionary["f"], out flavor))
				{
					throw new ArgumentException("Quarantine header invalid 'f:'");
				}
				if (!dictionary.ContainsKey("r"))
				{
					throw new ArgumentException("Quarantine header missing 'r:'");
				}
				int retentionDays;
				if (!int.TryParse(dictionary["r"], out retentionDays))
				{
					throw new ArgumentException("Quarantine header invalid 'r:'");
				}
				if (!dictionary.ContainsKey("e"))
				{
					throw new ArgumentException("Quarantine header missing 'e:'");
				}
				bool shouldEncrypt;
				if (!bool.TryParse(dictionary["e"], out shouldEncrypt))
				{
					throw new ArgumentException("Quarantine header invalid 'e:'");
				}
				QuarantineConnector quarantineConnector = new QuarantineConnector(flavor, retentionDays, shouldEncrypt);
				SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Pass, "CreateFromHeader: returning Quarantine Connector", new object[0]);
				result = quarantineConnector;
			}
			catch (ArgumentException ex)
			{
				SystemProbe.Trace(QuarantineConnector.ComponentName, SystemProbe.Status.Fail, "CreateFromHeader: Error parsing header: {0}", new object[]
				{
					ex.Message
				});
				throw;
			}
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004E5C File Offset: 0x0000305C
		internal Header ToHeader()
		{
			string value = string.Format("v:2;f:{0};r:{1};e:{2}", this.Flavor, this.RetentionDays, this.ShouldEncrypt);
			return new TextHeader(QuarantineConnector.XHeaderName, value);
		}

		// Token: 0x04000066 RID: 102
		internal static readonly string ComponentName = "QuarantineConnector";

		// Token: 0x04000067 RID: 103
		public static readonly string XHeaderName = "X-MS-Exchange-Organization-Hygiene-PutInQuarantine";
	}
}
