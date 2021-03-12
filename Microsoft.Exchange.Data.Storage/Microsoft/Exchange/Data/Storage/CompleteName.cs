using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200049A RID: 1178
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CompleteName
	{
		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x0600341A RID: 13338
		// (set) Token: 0x0600341B RID: 13339
		public abstract string Title { get; set; }

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x0600341C RID: 13340
		// (set) Token: 0x0600341D RID: 13341
		public abstract string FirstName { get; set; }

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600341E RID: 13342
		// (set) Token: 0x0600341F RID: 13343
		public abstract string MiddleName { get; set; }

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06003420 RID: 13344
		// (set) Token: 0x06003421 RID: 13345
		public abstract string LastName { get; set; }

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06003422 RID: 13346
		// (set) Token: 0x06003423 RID: 13347
		public abstract string Suffix { get; set; }

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06003424 RID: 13348
		// (set) Token: 0x06003425 RID: 13349
		public abstract string Initials { get; set; }

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06003426 RID: 13350
		// (set) Token: 0x06003427 RID: 13351
		public abstract string FullName { get; set; }

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06003428 RID: 13352
		// (set) Token: 0x06003429 RID: 13353
		public abstract string Nickname { get; set; }

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x0600342A RID: 13354
		// (set) Token: 0x0600342B RID: 13355
		public abstract string YomiCompany { get; set; }

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x0600342C RID: 13356
		// (set) Token: 0x0600342D RID: 13357
		public abstract string YomiFirstName { get; set; }

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x0600342E RID: 13358
		// (set) Token: 0x0600342F RID: 13359
		public abstract string YomiLastName { get; set; }
	}
}
