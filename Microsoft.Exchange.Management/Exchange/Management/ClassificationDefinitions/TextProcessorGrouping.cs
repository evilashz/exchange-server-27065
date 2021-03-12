using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200084D RID: 2125
	internal class TextProcessorGrouping : IGrouping<TextProcessorType, KeyValuePair<string, ExchangeBuild>>, IEnumerable<KeyValuePair<string, ExchangeBuild>>, IEnumerable
	{
		// Token: 0x060049BD RID: 18877 RVA: 0x0012F67C File Offset: 0x0012D87C
		internal TextProcessorGrouping(IGrouping<TextProcessorType, KeyValuePair<string, ExchangeBuild>> textProcessorGrouping, IEqualityComparer<string> textProcessorIdsComparer = null)
		{
			if (textProcessorGrouping == null)
			{
				throw new ArgumentNullException("textProcessorGrouping");
			}
			TextProcessorType key = textProcessorGrouping.Key;
			ExAssert.RetailAssert(TextProcessorType.Function <= key && key <= TextProcessorType.Fingerprint, "The specified textProcessorType '{0}' is out-of-range", new object[]
			{
				key.ToString()
			});
			this.textProcessorType = key;
			this.textProcessors = textProcessorGrouping.ToDictionary((KeyValuePair<string, ExchangeBuild> textProcessor) => textProcessor.Key, (KeyValuePair<string, ExchangeBuild> textProcessor) => textProcessor.Value, textProcessorIdsComparer ?? ClassificationDefinitionConstants.TextProcessorIdComparer);
		}

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x060049BE RID: 18878 RVA: 0x0012F729 File Offset: 0x0012D929
		public TextProcessorType Key
		{
			get
			{
				return this.textProcessorType;
			}
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x0012F731 File Offset: 0x0012D931
		IEnumerator<KeyValuePair<string, ExchangeBuild>> IEnumerable<KeyValuePair<string, ExchangeBuild>>.GetEnumerator()
		{
			return this.textProcessors.GetEnumerator();
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x0012F743 File Offset: 0x0012D943
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.textProcessors.GetEnumerator();
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x0012F755 File Offset: 0x0012D955
		internal bool Contains(string textProcessorId)
		{
			return this.textProcessors.ContainsKey(textProcessorId);
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x060049C2 RID: 18882 RVA: 0x0012F763 File Offset: 0x0012D963
		internal int Count
		{
			get
			{
				return this.textProcessors.Count;
			}
		}

		// Token: 0x04002C6C RID: 11372
		private readonly TextProcessorType textProcessorType;

		// Token: 0x04002C6D RID: 11373
		private readonly Dictionary<string, ExchangeBuild> textProcessors;
	}
}
