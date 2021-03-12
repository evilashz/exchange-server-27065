using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000258 RID: 600
	internal abstract class BaseFilterConverter<TInput, TOutput>
	{
		// Token: 0x06000FAB RID: 4011 RVA: 0x0004CC3C File Offset: 0x0004AE3C
		protected TOutput InternalConvert(TInput inputFilter)
		{
			StandbyStackEntry<TInput, TOutput> standbyStackEntry = new StandbyStackEntry<TInput, TOutput>(inputFilter);
			TOutput toutput = default(TOutput);
			int i = 0;
			while (i <= 255)
			{
				if (this.GetFilterChild(standbyStackEntry.Filter, standbyStackEntry.CurrentChild) == null)
				{
					toutput = this.ConvertSingleElement(standbyStackEntry.Filter, standbyStackEntry.WorkingStack);
					if (this.standbyStack.Count == 0)
					{
						return toutput;
					}
					standbyStackEntry = this.standbyStack.Pop();
					standbyStackEntry.WorkingStack.Push(toutput);
					standbyStackEntry.CurrentChild++;
					i++;
				}
				TInput filterChild = this.GetFilterChild(standbyStackEntry.Filter, standbyStackEntry.CurrentChild);
				if (filterChild != null)
				{
					this.standbyStack.Push(standbyStackEntry);
					standbyStackEntry = new StandbyStackEntry<TInput, TOutput>(filterChild);
				}
			}
			this.ThrowTooLongException();
			return default(TOutput);
		}

		// Token: 0x06000FAC RID: 4012
		protected abstract bool IsLeafExpression(TInput inputFilter);

		// Token: 0x06000FAD RID: 4013
		protected abstract int GetFilterChildCount(TInput parentFilter);

		// Token: 0x06000FAE RID: 4014
		protected abstract TInput GetFilterChild(TInput parentFilter, int childIndex);

		// Token: 0x06000FAF RID: 4015
		protected abstract void ThrowTooLongException();

		// Token: 0x06000FB0 RID: 4016
		protected abstract TOutput ConvertSingleElement(TInput inputFilter, Stack<TOutput> workingStack);

		// Token: 0x04000BD6 RID: 3030
		public const int MaxRestrictionNodeCount = 255;

		// Token: 0x04000BD7 RID: 3031
		private Stack<StandbyStackEntry<TInput, TOutput>> standbyStack = new Stack<StandbyStackEntry<TInput, TOutput>>();
	}
}
