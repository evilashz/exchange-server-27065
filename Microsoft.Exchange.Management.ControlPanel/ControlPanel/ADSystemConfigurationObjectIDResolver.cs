using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000319 RID: 793
	public class ADSystemConfigurationObjectIDResolver : AdObjectResolver
	{
		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x0008E9DC File Offset: 0x0008CBDC
		// (set) Token: 0x06002EA9 RID: 11945 RVA: 0x0008E9E3 File Offset: 0x0008CBE3
		private static PropertyDefinition[] Properties { get; set; }

		// Token: 0x06002EAA RID: 11946 RVA: 0x0008E9EC File Offset: 0x0008CBEC
		private ADSystemConfigurationObjectIDResolver()
		{
			ADSystemConfigurationObjectIDResolver.Properties = new PropertyDefinition[]
			{
				ADObjectSchema.Id
			};
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x0008EA14 File Offset: 0x0008CC14
		public ADObjectId ResolveObject(ADObjectId objectId)
		{
			if (objectId == null)
			{
				throw new FaultException(new ArgumentNullException("objectId").Message);
			}
			IEnumerable<ADObjectId> enumerable = this.ResolveObjects(new ADObjectId[]
			{
				objectId
			});
			if (enumerable == null)
			{
				return null;
			}
			return enumerable.FirstOrDefault<ADObjectId>();
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x0008EA64 File Offset: 0x0008CC64
		public IEnumerable<ADObjectId> ResolveObjects(IEnumerable<ADObjectId> objectIds)
		{
			return from row in base.ResolveObjects<ADObjectId>(objectIds, ADSystemConfigurationObjectIDResolver.Properties, (ADRawEntry e) => e.Id)
			select row;
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x0008EABC File Offset: 0x0008CCBC
		internal override IDirectorySession CreateAdSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, base.TenantSessionSetting, 60, "CreateAdSession", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\Pickers\\ADSystemConfigurationObjectIDResolver.cs");
		}

		// Token: 0x040022CF RID: 8911
		internal static readonly ADSystemConfigurationObjectIDResolver Instance = new ADSystemConfigurationObjectIDResolver();
	}
}
