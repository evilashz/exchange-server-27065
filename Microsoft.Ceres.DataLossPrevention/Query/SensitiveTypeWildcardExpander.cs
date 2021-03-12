using System;
using Microsoft.Ceres.DataLossPrevention.Common;
using Microsoft.Ceres.NlpBase.RichTypes.QueryTree;

namespace Microsoft.Ceres.DataLossPrevention.Query
{
	// Token: 0x0200001B RID: 27
	internal class SensitiveTypeWildcardExpander
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00005350 File Offset: 0x00003550
		public SensitiveTypeWildcardExpander(string encoding, FASTClassificationStore store)
		{
			string[] array = encoding.Split(ClassificationDecoding.Delimiters);
			string text = array[0].Trim();
			if (text.Length == 1 && text[0] == '*')
			{
				string[] array2 = store.AllRuleIds();
				this.decodings = new ClassificationDecoding[array2.Length];
				for (int i = 0; i < this.decodings.Length; i++)
				{
					this.decodings[i] = new ClassificationDecoding(array2[i], array, store);
				}
				return;
			}
			this.decodings = new ClassificationDecoding[1];
			this.decodings[0] = new ClassificationDecoding(array, store);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000053E4 File Offset: 0x000035E4
		public TreeNode CreateSubtree()
		{
			OrNode orNode = new OrNode();
			foreach (ClassificationDecoding classificationDecoding in this.decodings)
			{
				orNode.AddNode(classificationDecoding.CreateSubtree());
			}
			return orNode;
		}

		// Token: 0x0400007A RID: 122
		private ClassificationDecoding[] decodings;
	}
}
