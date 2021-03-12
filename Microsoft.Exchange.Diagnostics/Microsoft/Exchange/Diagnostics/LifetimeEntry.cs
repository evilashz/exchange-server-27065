using System;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000149 RID: 329
	public class LifetimeEntry
	{
		// Token: 0x06000954 RID: 2388 RVA: 0x000234F4 File Offset: 0x000216F4
		public LifetimeEntry(IntPtr handle, int offset) : this(LifetimeEntry.GetInternalLifetimeEntry(handle, offset), handle)
		{
			this.offset = offset;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002350B File Offset: 0x0002170B
		[CLSCompliant(false)]
		public unsafe LifetimeEntry(InternalLifetimeEntry* internalLifetimeEntry, IntPtr handle)
		{
			if (null == internalLifetimeEntry)
			{
				throw new ArgumentNullException("internalLifetimeEntry");
			}
			this.internalLifetimeEntry = internalLifetimeEntry;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0002352A File Offset: 0x0002172A
		public unsafe int Type
		{
			get
			{
				return this.internalLifetimeEntry->LifetimeType;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00023537 File Offset: 0x00021737
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00023544 File Offset: 0x00021744
		public unsafe int ProcessId
		{
			get
			{
				return this.internalLifetimeEntry->ProcessId;
			}
			set
			{
				this.internalLifetimeEntry->ProcessId = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00023552 File Offset: 0x00021752
		public unsafe long StartupTime
		{
			get
			{
				return this.internalLifetimeEntry->StartupTime;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002355F File Offset: 0x0002175F
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00023568 File Offset: 0x00021768
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Type: " + this.Type);
			stringBuilder.Append(" ProcessId: " + this.ProcessId);
			stringBuilder.Append(" StartupTime: " + this.StartupTime);
			return stringBuilder.ToString();
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000235D5 File Offset: 0x000217D5
		private unsafe static InternalLifetimeEntry* GetInternalLifetimeEntry(IntPtr handle, int offset)
		{
			return (long)handle / (long)sizeof(InternalLifetimeEntry) + offset;
		}

		// Token: 0x04000664 RID: 1636
		private unsafe InternalLifetimeEntry* internalLifetimeEntry;

		// Token: 0x04000665 RID: 1637
		private int offset;
	}
}
