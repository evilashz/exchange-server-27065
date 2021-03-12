using System;
using System.Text;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200026F RID: 623
	internal class ArrayTracer<T>
	{
		// Token: 0x06001B37 RID: 6967 RVA: 0x0006FAE8 File Offset: 0x0006DCE8
		public ArrayTracer(T[] array)
		{
			this.array = array;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0006FAF8 File Offset: 0x0006DCF8
		public override string ToString()
		{
			if (this.array == null)
			{
				return "<null array>";
			}
			StringBuilder stringBuilder = new StringBuilder("Array length=");
			stringBuilder.Append(this.array.Length);
			stringBuilder.Append(" { ");
			for (int i = 0; i < this.array.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(this.DumpElement(this.array[i]));
			}
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0006FB87 File Offset: 0x0006DD87
		protected virtual string DumpElement(T element)
		{
			return element.ToString();
		}

		// Token: 0x04000CDA RID: 3290
		private T[] array;
	}
}
