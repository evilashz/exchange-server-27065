using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x020000A3 RID: 163
	public struct PropertyChangeTracker<TObject, TPropertyDefinition> : IPropertyChangeTracker<TPropertyDefinition>
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x00007668 File Offset: 0x00005868
		public PropertyChangeTracker(TObject trackingObject, Func<TObject, TPropertyDefinition, bool> isPropertySet)
		{
			this = default(PropertyChangeTracker<TObject, TPropertyDefinition>);
			this.TrackingObject = trackingObject;
			this.isPropertySet = isPropertySet;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000767F File Offset: 0x0000587F
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x00007687 File Offset: 0x00005887
		public TObject TrackingObject { get; private set; }

		// Token: 0x06000414 RID: 1044 RVA: 0x00007690 File Offset: 0x00005890
		public bool IsPropertySet(TPropertyDefinition property)
		{
			return this.isPropertySet(this.TrackingObject, property);
		}

		// Token: 0x040001FA RID: 506
		private readonly Func<TObject, TPropertyDefinition, bool> isPropertySet;
	}
}
