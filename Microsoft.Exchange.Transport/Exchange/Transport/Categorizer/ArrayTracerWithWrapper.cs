using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000271 RID: 625
	internal class ArrayTracerWithWrapper<T, W> : ArrayTracer<T> where W : ITraceWrapper<T>, new()
	{
		// Token: 0x06001B3B RID: 6971 RVA: 0x0006FB96 File Offset: 0x0006DD96
		public ArrayTracerWithWrapper(T[] array) : base(array)
		{
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0006FBA0 File Offset: 0x0006DDA0
		protected override string DumpElement(T element)
		{
			W w = (default(W) == null) ? Activator.CreateInstance<W>() : default(W);
			w.Element = element;
			return w.ToString();
		}
	}
}
