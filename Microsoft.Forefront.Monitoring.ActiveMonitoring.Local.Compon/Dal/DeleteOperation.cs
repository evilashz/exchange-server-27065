using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000063 RID: 99
	public class DeleteOperation : ConfigDataProviderOperation
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000FB4F File Offset: 0x0000DD4F
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000FB57 File Offset: 0x0000DD57
		[XmlAttribute]
		public Guid Id { get; set; }

		// Token: 0x06000285 RID: 645 RVA: 0x0000FB60 File Offset: 0x0000DD60
		protected override void ExecuteConfigDataProviderOperation(IConfigDataProvider configDataProvider, IDictionary<string, object> variables)
		{
			Type type = DalProbeOperation.ResolveDataType(base.DataType);
			IConfigurable configurable = (IConfigurable)Activator.CreateInstance(type);
			DalHelper.SetPropertyValue(new ADObjectId(this.Id), ADObjectSchema.Id, configurable);
			configDataProvider.Delete(configurable);
		}
	}
}
