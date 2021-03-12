using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000487 RID: 1159
	internal class Offset : IEquatable<Offset>
	{
		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x000D82BA File Offset: 0x000D64BA
		// (set) Token: 0x06003506 RID: 13574 RVA: 0x000D82C2 File Offset: 0x000D64C2
		public int Start { get; set; }

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x000D82CB File Offset: 0x000D64CB
		// (set) Token: 0x06003508 RID: 13576 RVA: 0x000D82D3 File Offset: 0x000D64D3
		public int End { get; set; }

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x000D82DC File Offset: 0x000D64DC
		public int Length
		{
			get
			{
				return this.End - this.Start;
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x000D82EB File Offset: 0x000D64EB
		public bool Empty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000D82F6 File Offset: 0x000D64F6
		public Offset(int start = 0, int end = 0)
		{
			Offset.ValidateOffset(start, end);
			this.Start = start;
			this.End = end;
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000D8313 File Offset: 0x000D6513
		public void Reset()
		{
			this.Start = 0;
			this.End = 0;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000D8323 File Offset: 0x000D6523
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Offset);
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000D8331 File Offset: 0x000D6531
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000D8334 File Offset: 0x000D6534
		public bool Equals(Offset offset)
		{
			return !object.ReferenceEquals(offset, null) && (object.ReferenceEquals(this, offset) || (!(base.GetType() != offset.GetType()) && this.Start == offset.Start && this.End == offset.End));
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000D838A File Offset: 0x000D658A
		private static void ValidateOffset(int start, int end)
		{
			if (start > end)
			{
				throw new ArgumentException(string.Format("The start and end values provided do not form a valid offset {0}:{1}", start, end));
			}
		}
	}
}
