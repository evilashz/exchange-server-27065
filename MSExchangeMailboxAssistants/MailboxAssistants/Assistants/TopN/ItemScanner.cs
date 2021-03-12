using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Assistants.TopN;
using Microsoft.Exchange.InfoWorker.Common.TopN;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.TopN
{
	// Token: 0x0200018B RID: 395
	internal class ItemScanner
	{
		// Token: 0x06000FAA RID: 4010 RVA: 0x0005CA50 File Offset: 0x0005AC50
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Concat(new object[]
				{
					"ItemScanner for item ",
					this.storeObjectId.ToHexEntryId(),
					" in mailbox ",
					this.mailboxSession.MailboxOwner,
					". "
				});
			}
			return this.toString;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0005CAB2 File Offset: 0x0005ACB2
		internal ItemScanner(MailboxSession mailboxSession, StoreObjectId storeObjectId)
		{
			this.mailboxSession = mailboxSession;
			this.storeObjectId = storeObjectId;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0005CAC8 File Offset: 0x0005ACC8
		internal Dictionary<string, int> Scan()
		{
			List<string> list;
			int num;
			if (!this.GetContent(out list, out num))
			{
				return null;
			}
			ItemScanner.Tracer.Information<ItemScanner, int>((long)this.GetHashCode(), "{0}: Scanning item using locale id {1}.", this, num);
			TextBreaker textBreaker = new TextBreaker(num);
			WordFilter wordFilter = new WordFilter(num);
			Dictionary<string, int> dictionary = new Dictionary<string, int>(1000);
			foreach (string text in list)
			{
				List<string> list2 = textBreaker.BreakText(text);
				if (list2 != null && list2.Count != 0)
				{
					List<string> list3 = wordFilter.Filter(list2);
					foreach (string key in list3)
					{
						int num2;
						if (dictionary.TryGetValue(key, out num2))
						{
							dictionary[key] = num2 + 1;
						}
						else
						{
							dictionary.Add(key, 1);
						}
						if (dictionary.Count >= 1000)
						{
							return dictionary;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0005CBF0 File Offset: 0x0005ADF0
		private bool GetContent(out List<string> content, out int localeId)
		{
			content = new List<string>(10);
			localeId = 0;
			try
			{
				using (Item item = Item.Bind(this.mailboxSession, this.storeObjectId, ItemScanner.properties))
				{
					object obj;
					if ((obj = item.TryGetProperty(MessageItemSchema.MessageLocaleId)) is int)
					{
						localeId = (int)obj;
					}
					using (TextReader textReader = item.Body.OpenTextReader(BodyFormat.TextPlain))
					{
						char[] array = new char[8192];
						int length = textReader.ReadBlock(array, 0, 8192);
						content.Add(new string(array, 0, length));
					}
					object obj2;
					if ((obj2 = item.TryGetProperty(ItemSchema.Subject)) is string)
					{
						content.Add((string)obj2);
					}
					object obj3;
					if ((obj3 = item.TryGetProperty(ItemSchema.DisplayTo)) is string)
					{
						content.Add((string)obj3);
					}
					object obj4;
					if ((obj4 = item.TryGetProperty(ItemSchema.DisplayCc)) is string)
					{
						content.Add((string)obj4);
					}
					object obj5;
					if ((obj5 = item.TryGetProperty(MessageItemSchema.SenderDisplayName)) is string)
					{
						content.Add((string)obj5);
					}
				}
			}
			catch (ObjectNotFoundException arg)
			{
				ItemScanner.Tracer.TraceWarning<ItemScanner, StoreObjectId, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: Store object Id {1} was not found. Exception: {2}.", this, this.storeObjectId, arg);
			}
			return content.Count > 0;
		}

		// Token: 0x040009F5 RID: 2549
		internal const int MaxWords = 1000;

		// Token: 0x040009F6 RID: 2550
		private MailboxSession mailboxSession;

		// Token: 0x040009F7 RID: 2551
		private StoreObjectId storeObjectId;

		// Token: 0x040009F8 RID: 2552
		private static readonly PropertyDefinition[] properties = new PropertyDefinition[]
		{
			MessageItemSchema.MessageLocaleId,
			ItemSchema.Subject,
			ItemSchema.DisplayTo,
			ItemSchema.DisplayCc,
			MessageItemSchema.SenderDisplayName
		};

		// Token: 0x040009F9 RID: 2553
		private string toString;

		// Token: 0x040009FA RID: 2554
		private static readonly Trace Tracer = ExTraceGlobals.TopNAssistantTracer;

		// Token: 0x040009FB RID: 2555
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
