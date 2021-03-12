using System;

namespace Microsoft.Exchange.Data.Directory.DirSync
{
	// Token: 0x020001B1 RID: 433
	internal class ADDirSyncProperty<T>
	{
		// Token: 0x0600120A RID: 4618 RVA: 0x00057A96 File Offset: 0x00055C96
		public ADDirSyncProperty(T value) : this(value, ADDirSyncPropertyState.NewValue)
		{
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00057AA0 File Offset: 0x00055CA0
		protected ADDirSyncProperty(T value, ADDirSyncPropertyState state)
		{
			this.value = value;
			this.State = state;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00057AB8 File Offset: 0x00055CB8
		public static ADDirSyncProperty<T> NoChange
		{
			get
			{
				return new ADDirSyncProperty<T>(default(T), ADDirSyncPropertyState.NoChange);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00057AD4 File Offset: 0x00055CD4
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00057ADC File Offset: 0x00055CDC
		public ADDirSyncPropertyState State { get; private set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00057AE5 File Offset: 0x00055CE5
		public T Value
		{
			get
			{
				if (this.State != ADDirSyncPropertyState.NewValue)
				{
					throw new InvalidOperationException(DirectoryStrings.ValueNotAvailableForUnchangedProperty);
				}
				return this.value;
			}
		}

		// Token: 0x04000A7C RID: 2684
		private readonly T value;
	}
}
