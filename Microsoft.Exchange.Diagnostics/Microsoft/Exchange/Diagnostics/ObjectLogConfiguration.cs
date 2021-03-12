using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001C6 RID: 454
	internal abstract class ObjectLogConfiguration
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0002EEFD File Offset: 0x0002D0FD
		public virtual bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0002EF00 File Offset: 0x0002D100
		public virtual TimeSpan MaxLogAge
		{
			get
			{
				return TimeSpan.MaxValue;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0002EF07 File Offset: 0x0002D107
		public virtual int BufferLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002EF0A File Offset: 0x0002D10A
		public virtual string Note
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0002EF11 File Offset: 0x0002D111
		public virtual bool FlushToDisk
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002EF14 File Offset: 0x0002D114
		public virtual TimeSpan StreamFlushInterval
		{
			get
			{
				return TimeSpan.MaxValue;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000CB2 RID: 3250
		public abstract string LoggingFolder { get; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000CB3 RID: 3251
		public abstract string LogComponentName { get; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000CB4 RID: 3252
		public abstract string FilenamePrefix { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000CB5 RID: 3253
		public abstract long MaxLogDirSize { get; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000CB6 RID: 3254
		public abstract long MaxLogFileSize { get; }
	}
}
