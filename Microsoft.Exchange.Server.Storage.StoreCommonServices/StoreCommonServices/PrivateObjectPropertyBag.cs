using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000E6 RID: 230
	public abstract class PrivateObjectPropertyBag : ObjectPropertyBag
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x0002AACC File Offset: 0x00028CCC
		protected PrivateObjectPropertyBag(Context context, Table table, bool skipDataRowValidation, bool changeTrackingEnabled, bool newBag, bool writeThrough, params ColumnValue[] initialValues) : base(context, changeTrackingEnabled)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				DataRow dataRow = SharedObjectPropertyBagDataCache.LoadDataRow(context, newBag, table, writeThrough, initialValues);
				if (!skipDataRowValidation && dataRow != null && !this.IsValidDataRow(context, dataRow))
				{
					dataRow.Dispose();
					dataRow = null;
				}
				this.dataRow = dataRow;
				disposeGuard.Success();
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002AB40 File Offset: 0x00028D40
		protected PrivateObjectPropertyBag(Context context, Table table, bool changeTrackingEnabled, bool writeThrough, Reader reader) : base(context, changeTrackingEnabled)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				DataRow dataRow = Factory.OpenDataRow(context.Culture, context, table, writeThrough, reader);
				if (dataRow != null && !this.IsValidDataRow(context, dataRow))
				{
					dataRow.Dispose();
					dataRow = null;
				}
				this.dataRow = dataRow;
				disposeGuard.Success();
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0002ABB4 File Offset: 0x00028DB4
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0002ABBC File Offset: 0x00028DBC
		internal override DataRow DataRow
		{
			get
			{
				return this.dataRow;
			}
			set
			{
				this.dataRow = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0002ABC5 File Offset: 0x00028DC5
		protected override Dictionary<ushort, KeyValuePair<StorePropTag, object>> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0002ABCD File Offset: 0x00028DCD
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0002ABD5 File Offset: 0x00028DD5
		protected override bool PropertiesDirty
		{
			get
			{
				return this.propertiesDirty;
			}
			set
			{
				this.propertiesDirty = value;
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002ABDE File Offset: 0x00028DDE
		protected virtual bool IsValidDataRow(Context context, DataRow dataRow)
		{
			return true;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002ABE1 File Offset: 0x00028DE1
		protected override void AssignPropertiesToUse(Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties)
		{
			this.properties = properties;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002ABEA File Offset: 0x00028DEA
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.dataRow != null)
			{
				this.dataRow.Dispose();
				this.dataRow = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x04000534 RID: 1332
		private Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties;

		// Token: 0x04000535 RID: 1333
		private bool propertiesDirty;

		// Token: 0x04000536 RID: 1334
		private DataRow dataRow;
	}
}
