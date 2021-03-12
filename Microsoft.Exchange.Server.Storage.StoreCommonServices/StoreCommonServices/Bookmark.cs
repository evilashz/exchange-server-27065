using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000021 RID: 33
	public struct Bookmark
	{
		// Token: 0x06000144 RID: 324 RVA: 0x000105BE File Offset: 0x0000E7BE
		private Bookmark(IList<object> keyValues, bool positionedOn, bool positionValid, int position)
		{
			this.keyValues = keyValues;
			this.positionedOn = positionedOn;
			this.positionValid = positionValid;
			this.position = position;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000105DD File Offset: 0x0000E7DD
		public Bookmark(IList<object> keyValues, bool positionedOn)
		{
			this = new Bookmark(keyValues, positionedOn, false, -1);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000105E9 File Offset: 0x0000E7E9
		public Bookmark(IList<object> keyValues, bool positionedOn, int position)
		{
			this = new Bookmark(keyValues, positionedOn, true, position);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000105F8 File Offset: 0x0000E7F8
		internal Bookmark(SortOrder sortOrder, Reader reader, bool positionedOn, int? position)
		{
			this.positionValid = (position != null);
			this.position = position.GetValueOrDefault(-1);
			this.positionedOn = positionedOn;
			object[] array = new object[sortOrder.Count];
			for (int i = 0; i < sortOrder.Count; i++)
			{
				array[i] = reader.GetValue(sortOrder[i].Column);
			}
			this.keyValues = array;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00010666 File Offset: 0x0000E866
		public IList<object> KeyValues
		{
			get
			{
				return this.keyValues;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0001066E File Offset: 0x0000E86E
		public bool PositionedOn
		{
			get
			{
				return this.positionedOn;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00010676 File Offset: 0x0000E876
		public bool PositionValid
		{
			get
			{
				return this.positionValid;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0001067E File Offset: 0x0000E87E
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00010686 File Offset: 0x0000E886
		public bool IsBOT
		{
			get
			{
				return this.keyValues == null && this.position == 0;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0001069B File Offset: 0x0000E89B
		public bool IsEOT
		{
			get
			{
				return this.keyValues == null && this.position == int.MaxValue;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000106B4 File Offset: 0x0000E8B4
		public override string ToString()
		{
			if (this.IsBOT)
			{
				return "BOT";
			}
			if (this.IsEOT)
			{
				return "EOT";
			}
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append(this.positionedOn ? "On " : "After ");
			if (this.positionValid)
			{
				stringBuilder.Append("Numeric Position:[");
				stringBuilder.Append(this.position);
				stringBuilder.Append("]");
			}
			else
			{
				stringBuilder.Append("Key Position:[");
				for (int i = 0; i < this.keyValues.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendAsString(this.keyValues[i]);
				}
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001E1 RID: 481
		private readonly int position;

		// Token: 0x040001E2 RID: 482
		private readonly bool positionValid;

		// Token: 0x040001E3 RID: 483
		private readonly bool positionedOn;

		// Token: 0x040001E4 RID: 484
		private IList<object> keyValues;

		// Token: 0x040001E5 RID: 485
		public static readonly Bookmark BOT = new Bookmark(null, true, true, 0);

		// Token: 0x040001E6 RID: 486
		public static readonly Bookmark EOT = new Bookmark(null, false, false, int.MaxValue);
	}
}
