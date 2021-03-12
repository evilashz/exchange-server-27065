using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ObjectModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class Field
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public Field(object data)
		{
			ExTraceGlobals.FieldTracer.Information((long)this.GetHashCode(), "Field::Field - initializing Field with data {0}.", new object[]
			{
				(data == null) ? "null" : data
			});
			this.data = data;
			this.changeTypeFlags = Field.ChangeTypeFlags.Unchanged;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002DF8 File Offset: 0x00000FF8
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002E44 File Offset: 0x00001044
		public object Data
		{
			get
			{
				ExTraceGlobals.FieldTracer.Information((long)this.GetHashCode(), "Field::Data - getting data {0}.", new object[]
				{
					(this.data == null) ? "null" : this.data
				});
				return this.data;
			}
			set
			{
				ExTraceGlobals.FieldTracer.Information((long)this.GetHashCode(), "Field::Data - setting data to {0}.", new object[]
				{
					(value == null) ? "null" : value
				});
				int num = (this.data == null) ? 0 : this.data.GetHashCode();
				int num2 = (value == null) ? 0 : value.GetHashCode();
				Type left = (this.data == null) ? null : this.data.GetType();
				Type right = (value == null) ? null : value.GetType();
				bool flag = num == num2 && left == right;
				this.changeTypeFlags |= Field.ChangeTypeFlags.Modified;
				if (!flag)
				{
					this.changeTypeFlags |= Field.ChangeTypeFlags.Changed;
				}
				this.data = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002F01 File Offset: 0x00001101
		public bool IsChanged
		{
			get
			{
				ExTraceGlobals.FieldTracer.Information((long)this.GetHashCode(), "Field::IsChanged - checking if field was touched.");
				return 0 != (byte)(Field.ChangeTypeFlags.Changed & this.changeTypeFlags);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002F28 File Offset: 0x00001128
		public bool IsModified
		{
			get
			{
				ExTraceGlobals.FieldTracer.Information((long)this.GetHashCode(), "Field::IsModified - checking if data was changed.");
				return 0 != (byte)(Field.ChangeTypeFlags.Modified & this.changeTypeFlags);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002F4F File Offset: 0x0000114F
		public void ResetChangeTracking()
		{
			ExTraceGlobals.FieldTracer.Information((long)this.GetHashCode(), "Field::ResetChangeTracking - resetting change tracking.");
			this.changeTypeFlags = Field.ChangeTypeFlags.Unchanged;
		}

		// Token: 0x04000015 RID: 21
		private object data;

		// Token: 0x04000016 RID: 22
		private Field.ChangeTypeFlags changeTypeFlags;

		// Token: 0x02000007 RID: 7
		[Flags]
		private enum ChangeTypeFlags : byte
		{
			// Token: 0x04000018 RID: 24
			Unchanged = 0,
			// Token: 0x04000019 RID: 25
			Modified = 1,
			// Token: 0x0400001A RID: 26
			Changed = 2
		}
	}
}
