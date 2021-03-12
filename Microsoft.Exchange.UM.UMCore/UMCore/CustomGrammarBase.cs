using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000105 RID: 261
	internal abstract class CustomGrammarBase
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x0001D720 File Offset: 0x0001B920
		protected CustomGrammarBase(CultureInfo transcriptionLanguage)
		{
			this.transcriptionLanguage = transcriptionLanguage;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600073A RID: 1850
		internal abstract string FileName { get; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600073B RID: 1851
		internal abstract string Rule { get; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001D72F File Offset: 0x0001B92F
		internal bool IsEmpty
		{
			get
			{
				return this.ItemsXml.Length == 0;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001D73F File Offset: 0x0001B93F
		protected CultureInfo TranscriptionLanguage
		{
			get
			{
				return this.transcriptionLanguage;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001D747 File Offset: 0x0001B947
		private StringBuilder ItemsXml
		{
			get
			{
				if (this.itemsXml == null)
				{
					this.itemsXml = this.GenerateItemsXml();
				}
				return this.itemsXml;
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001D764 File Offset: 0x0001B964
		internal virtual void WriteCustomGrammar(string customGrammarDir)
		{
			if (this.IsEmpty)
			{
				return;
			}
			using (StreamWriter streamWriter = new StreamWriter(Path.Combine(customGrammarDir, this.FileName)))
			{
				streamWriter.Write(string.Format(CultureInfo.InvariantCulture, "<?xml version=\"1.0\"?>\r\n<grammar xml:lang=\"{0}\" version=\"1.0\" xmlns=\"http://www.w3.org/2001/06/grammar\" tag-format=\"semantics/1.0\">\r\n<tag>out.customGrammarWords=false;out.topNWords=false;</tag>\r\n    <rule id=\"{1}\" scope=\"public\">\r\n        <one-of>", new object[]
				{
					this.transcriptionLanguage,
					this.Rule
				}));
				streamWriter.Write(this.ItemsXml.ToString());
				streamWriter.Write("      \r\n        </one-of>\r\n    </rule>\r\n</grammar>");
			}
		}

		// Token: 0x06000740 RID: 1856
		protected abstract List<GrammarItemBase> GetItems();

		// Token: 0x06000741 RID: 1857 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		private StringBuilder GenerateItemsXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<GrammarItemBase> items = this.GetItems();
			HashSet<GrammarItemBase> hashSet = new HashSet<GrammarItemBase>();
			foreach (GrammarItemBase grammarItemBase in items)
			{
				if (!grammarItemBase.IsEmpty && !hashSet.Contains(grammarItemBase))
				{
					hashSet.Add(grammarItemBase);
					stringBuilder.Append(grammarItemBase.ToString());
				}
			}
			return stringBuilder;
		}

		// Token: 0x04000822 RID: 2082
		private readonly CultureInfo transcriptionLanguage;

		// Token: 0x04000823 RID: 2083
		private StringBuilder itemsXml;
	}
}
