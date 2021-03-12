using System;
using System.Text;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	internal class BinaryFilter : ContentFilter
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00007D5C File Offset: 0x00005F5C
		public BinaryFilter(PropertyDefinition property, byte[] binaryData, MatchOptions matchOptions, MatchFlags matchFlags) : base(property, matchOptions, matchFlags)
		{
			this.binaryData = binaryData;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00007D6F File Offset: 0x00005F6F
		public byte[] BinaryData
		{
			get
			{
				return this.binaryData;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007D77 File Offset: 0x00005F77
		public override SinglePropertyFilter CloneWithAnotherProperty(PropertyDefinition property)
		{
			base.CheckClonable(property);
			return new BinaryFilter(property, this.binaryData, base.MatchOptions, base.MatchFlags);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007D98 File Offset: 0x00005F98
		public override bool Equals(object obj)
		{
			BinaryFilter binaryFilter = obj as BinaryFilter;
			return binaryFilter != null && base.MatchFlags == binaryFilter.MatchFlags && base.MatchOptions == binaryFilter.MatchOptions && this.BinaryEquals(binaryFilter.binaryData) && base.Equals(obj);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007DE2 File Offset: 0x00005FE2
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.ComputeHashCode();
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007DF1 File Offset: 0x00005FF1
		protected override string StringValue
		{
			get
			{
				if (this.binaryData != null)
				{
					return Encoding.UTF8.GetString(this.binaryData, 0, this.binaryData.Length);
				}
				return "<null>";
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007E1C File Offset: 0x0000601C
		private bool BinaryEquals(byte[] bytes)
		{
			ArrayComparer<byte> comparer = ArrayComparer<byte>.Comparer;
			return comparer.Equals(this.binaryData, bytes);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007E3C File Offset: 0x0000603C
		private int ComputeHashCode()
		{
			ArrayComparer<byte> comparer = ArrayComparer<byte>.Comparer;
			return comparer.GetHashCode(this.binaryData);
		}

		// Token: 0x0400009C RID: 156
		private readonly byte[] binaryData;
	}
}
