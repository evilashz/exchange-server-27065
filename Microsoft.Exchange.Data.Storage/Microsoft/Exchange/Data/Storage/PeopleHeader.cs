using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000517 RID: 1303
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleHeader
	{
		// Token: 0x060037F4 RID: 14324 RVA: 0x000E2428 File Offset: 0x000E0628
		public PeopleHeader(string displayName, string startChar)
		{
			Util.ThrowOnNullOrEmptyArgument(displayName, "displayName");
			this.DisplayName = displayName;
			this.StartChar = startChar;
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x000E2449 File Offset: 0x000E0649
		// (set) Token: 0x060037F6 RID: 14326 RVA: 0x000E2451 File Offset: 0x000E0651
		public string DisplayName { get; private set; }

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060037F7 RID: 14327 RVA: 0x000E245A File Offset: 0x000E065A
		// (set) Token: 0x060037F8 RID: 14328 RVA: 0x000E2462 File Offset: 0x000E0662
		public string StartChar { get; private set; }

		// Token: 0x060037F9 RID: 14329 RVA: 0x000E246C File Offset: 0x000E066C
		public override bool Equals(object obj)
		{
			PeopleHeader peopleHeader = obj as PeopleHeader;
			return peopleHeader != null && this.DisplayName == peopleHeader.DisplayName && this.StartChar == peopleHeader.StartChar;
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x000E24AB File Offset: 0x000E06AB
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000E24B8 File Offset: 0x000E06B8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, PeopleHeader.ToStringFormat, new object[]
			{
				this.DisplayName,
				this.StartChar
			});
		}

		// Token: 0x04001DCD RID: 7629
		private static readonly string ToStringFormat = "DiplayName={0}, StartChar:{1}";
	}
}
